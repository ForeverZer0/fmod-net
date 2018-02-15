using System;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using FMOD.Sharp.DSP;

namespace FMOD.Sharp
{
	using Enums;
	using Structs;

	internal class Program
	{
		[DllImport(Core.LIBRARY, EntryPoint="FMOD_System_CreateDSPByType")]
		private static extern Result CreateDSPByType(IntPtr system, DspType type, out IntPtr dsp);

		private static StringBuilder _buffer;

		private const string NAMESPACE = "FMOD.Sharp.Dsps";
		private const string OUTPUT_DIRECTORY = "Generated";

		private static int  _succeeded;
		private static int _failed;

		private static void Main()
		{
			Console.Title = "FMOD# DSP Factory";
			Console.ForegroundColor = ConsoleColor.Cyan;
			Console.WriteLine("\r\n ________ _____ ______   ________  ________       ___    ___           \r\n|\\  _____\\\\   _ \\  _   \\|\\   __  \\|\\   ___ \\     |\\  \\  |\\  \\          \r\n\\ \\  \\__/\\ \\  \\\\\\__\\ \\  \\ \\  \\|\\  \\ \\  \\_|\\ \\  __\\_\\  \\_\\_\\  \\_____    \r\n \\ \\   __\\\\ \\  \\\\|__| \\  \\ \\  \\\\\\  \\ \\  \\ \\\\ \\|\\____    ___    ____\\   \r\n  \\ \\  \\_| \\ \\  \\    \\ \\  \\ \\  \\\\\\  \\ \\  \\_\\\\ \\|___| \\  \\__|\\  \\___|   \r\n   \\ \\__\\   \\ \\__\\    \\ \\__\\ \\_______\\ \\_______\\  __\\_\\  \\_\\_\\  \\_____ \r\n    \\|__|    \\|__|     \\|__|\\|_______|\\|_______| |\\____    ____   ____\\\r\n                                                 \\|___| \\  \\__|\\  \\___|\r\n                                                       \\ \\__\\ \\ \\__\\   \r\n                                                        \\|__|  \\|__|   \r\n");
			Console.ForegroundColor = ConsoleColor.DarkYellow;
			Console.WriteLine("(c) 2018 Eric Freed. All Rights Reserved.");
			Console.WriteLine();
			Console.ForegroundColor = ConsoleColor.White;
			Console.WriteLine("Press <Enter> to generate.");
			Console.ReadLine();
			_buffer = new StringBuilder();
			using (var system = FmodSystem.Create())
			{
				system.Initialize(InitFlags.Normal);
				foreach (DspType dspType in Enum.GetValues(typeof(DspType)))
				{
					var className = Enum.GetName(typeof(DspType), dspType);
					CreateDSPByType(system, dspType, out var handle);
					if (handle.ToInt32() == 0)
					{
						Console.ForegroundColor = ConsoleColor.Yellow;
						Console.WriteLine($"Failed to generate class \"{className}\".");
						_failed++;
						continue;
					}
					using (var dsp = Core.Create<DspBase>(handle))
						GenerateClass(className, dsp);
					FlushBuffer(className);
				}
			}
			Console.WriteLine();
			Console.ForegroundColor = ConsoleColor.Magenta;
			Console.WriteLine($"Operation complete.\r\nGenerated {_succeeded} classes successfully.\r\nFailed to generate {_failed} classes.");
			Console.WriteLine();
			Console.ForegroundColor = ConsoleColor.White;
			Console.WriteLine("Press <Enter> to exit.");
			Console.ReadLine();
		}

		
		private static void FlushBuffer(string className)
		{
			if (!Directory.Exists(OUTPUT_DIRECTORY))
			{
				try
				{
					Directory.CreateDirectory(OUTPUT_DIRECTORY);
				}
				catch (FmodException e)
				{
					Console.ForegroundColor = ConsoleColor.Red;
					Console.WriteLine(e.Message);
					Console.ReadLine();
					throw;
				}
			}
			var path = Path.Combine(OUTPUT_DIRECTORY, $"{className}.cs");
			try
			{
				var text = _buffer.ToString();
				File.WriteAllText(path, text, Encoding.UTF8);
				Console.ForegroundColor = ConsoleColor.DarkGreen;
				Console.WriteLine($"Successfully generated \"{className}\".");
				_succeeded++;
			}
			catch (FmodException e)
			{
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine(e.Message);
				Console.ReadLine();
				_failed++;
			}
			_buffer.Clear();
		}

		private static string FormatName(char[] name)
		{
			var nameStr = new String(name).ToLower();
			var textInfo = new CultureInfo("en-US", false).TextInfo;
			nameStr = textInfo.ToTitleCase(nameStr);
			var regex = new Regex(@"\W");
			if (nameStr.StartsWith("3D", StringComparison.InvariantCulture))
				nameStr = nameStr.Replace("3D", "ThreeD");
			if (nameStr.StartsWith("2D", StringComparison.InvariantCulture))
				nameStr = nameStr.Replace("2D", "TwoD");
			return regex.Replace(nameStr, "");
		}
		
		private static void GenerateClass(string className, DspBase dspBase)
		{
			Console.ForegroundColor = ConsoleColor.Blue;
			Console.WriteLine($"Discovered {dspBase.GetInfo()}... ");
			_buffer.AppendLine("using System;");
			_buffer.AppendLine();
			_buffer.AppendLine($"namespace {NAMESPACE}");
			_buffer.AppendLine("{");
			_buffer.AppendLine($"\tpublic class {className} : DspBase");
			_buffer.AppendLine("\t{");

			var count = dspBase.ParameterCount;
			for (var i = 0; i < count; i++)
			{
				_buffer.AppendLine();
				var info = dspBase.GetParameterInfo(i);
				GenerateEvent(i, info);
			}
			_buffer.AppendLine();
			GenerateConstructor(className);
			for (var i = 0; i < count; i++)
			{
				_buffer.AppendLine();
				var info = dspBase.GetParameterInfo(i);
				GenerateParameter(i, info);
			}
			_buffer.AppendLine("\t}");
			_buffer.AppendLine("}");
			_buffer.AppendLine($"// Code generated by FMOD# DSP Factory  {DateTime.Now:F}");
		}

		private static void GenerateConstructor(string className)
		{
			_buffer.AppendLine($"\t\tinternal {className}(IntPtr handle) : base(handle)");
			_buffer.AppendLine("\t\t{");
			_buffer.AppendLine("\t\t}");
		}

		private static void GenerateEvent(int index, DspParameterDesc desc)
		{
			var eventName = $"{FormatName(desc.Name)}Changed";

			switch (desc.Type)
			{
				case DspParameterType.Float:
					_buffer.AppendLine($"\t\tpublic event EventHandler<DspFloatParamChangedEventArgs> {eventName};");
					break;
				case DspParameterType.Int:
					break;
				case DspParameterType.Bool:
					_buffer.AppendLine($"\t\tpublic event EventHandler<DspBoolParamChangedEventArgs> {eventName};");
					break;
				case DspParameterType.Data:
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		private static void GenerateParameter(int index, DspParameterDesc desc)
		{
			switch (desc.Type)
			{
				case DspParameterType.Float:
				{
					var info = desc.desc.FloatDescription;
					GenerateFloatParameter(index, FormatName(desc.Name), info.Minimum, info.Maximum);
					break;
				}
				case DspParameterType.Int:
				{
					var info = desc.desc.IntDescription;
					GenerateIntParameter(index, FormatName(desc.Name), info.Minimum, info.Maximum);
					break;
				}
				case DspParameterType.Bool:
				{
					GenerateBoolParameter(index, FormatName(desc.Name));
					break;
				}
				case DspParameterType.Data:
				{
					GenerateDataParameter(index, FormatName(desc.Name));
					break;
				}
			}
		}


		

		private static void GenerateFloatParameter(int index, string name, float min, float max)
		{
			_buffer.AppendLine($"\t\tpublic float {name}");
			_buffer.AppendLine("\t\t{");
			_buffer.AppendLine($"\t\t\tget => GetParameterFloat({index});");
			_buffer.AppendLine("\t\t\tset");
			_buffer.AppendLine("\t\t\t{");
			_buffer.AppendLine($"\t\t\t\tvar clamped = value.Clamp({min:0.0###}f, {max:0.0###}f);");
			_buffer.AppendLine($"\t\t\t\tSetParameterFloat({index}, clamped);");
			_buffer.AppendLine($"\t\t\t\t{name}Changed?.Invoke(this, new DspFloatParamChangedEventArgs({index}, clamped, {min:0.0###}f, {max:0.0###}f));");
			_buffer.AppendLine("\t\t\t}");
			_buffer.AppendLine("\t\t}");
		}

		private static void GenerateIntParameter(int index, string name, int min, int max)
		{
			_buffer.AppendLine($"\t\tpublic int {name}");
			_buffer.AppendLine("\t\t{");
			_buffer.AppendLine($"\t\t\tget => GetParameterInt({index});");
			_buffer.AppendLine($"\t\t\tset {{ SetParameterInt({index}, value.Clamp({min}, {max})); }}");
			_buffer.AppendLine("\t\t}");
		}

		private static void GenerateBoolParameter(int index, string name)
		{
			_buffer.AppendLine($"\t\tpublic bool {name}");
			_buffer.AppendLine("\t\t{");
			_buffer.AppendLine($"\t\t\tget => GetParameterBool({index});");
			_buffer.AppendLine("\t\t\t{");
			_buffer.AppendLine($"\t\t\t\tSetParameterBool({index}, value);");
			_buffer.AppendLine($"\t\t\t\t{name}Changed?.Invoke(this, new DspBoolParamChangedEventArgs({index}, value));");
			_buffer.AppendLine("\t\t\t}");
			_buffer.AppendLine("\t\t}");
		}
		private static void GenerateDataParameter(int index, string name)
		{
			_buffer.AppendLine($"\t\tpublic byte[] {name}");
			_buffer.AppendLine("\t\t{");
			_buffer.AppendLine($"\t\t\tget => GetParameterData({index});");
			_buffer.AppendLine($"\t\t\tset {{ SetParameterData({index}, value); }}");
			_buffer.AppendLine("\t\t}");
		}
	}
}