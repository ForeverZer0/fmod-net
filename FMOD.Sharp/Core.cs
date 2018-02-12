using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Text.RegularExpressions;
using FMOD.Sharp.Enums;

namespace FMOD.Sharp
{
	public static class Core
	{
		public const int VERSION = 0x00011002;
		public const int MAX_CHANNEL_WIDTH = 32;
		public const int MAX_LISTENERS = 8;
		public const int REVERB_MAXINSTANCES = 4;
		public const int MAX_SYSTEMS = 8;

		private static ResourceManager _resxManager;

#if X64
		public const string LIBRARY = "fmod64.dll";

		public static Dictionary<long, Handle> ValidHandles { get; }
#elif X86
		public const string LIBRARY = "fmod.dll";

		public static Dictionary<int, Handle> ValidHandles { get; }
#endif

		static Core()
		{
#if X64
			ValidHandles = new Dictionary<long, Handle>();
#elif X86
			ValidHandles = new Dictionary<int, Handle>();
#endif
		}

		public static T Create<T>(IntPtr pointer) where T : Handle
		{
#if X64
			var ptrValue = pointer.ToInt64();
#elif X86
			var ptrValue = pointer.ToInt32();
#endif
			if (pointer == IntPtr.Zero)
				return null;
			if (ValidHandles.ContainsKey(ptrValue))
				return (T) ValidHandles[ptrValue];
			var obj = (T) Activator.CreateInstance(typeof(T), pointer);
			obj.Disposed += (s, e) => ValidHandles.Remove(ptrValue);
			ValidHandles[ptrValue] = obj;
			return obj;
		}

		public static void AddReference<T>(IntPtr pointer, T handle) where T : Handle
		{
#if X64
			var ptrValue = pointer.ToInt64();
#elif X86
			var ptrValue = pointer.ToInt32();
#endif
			ValidHandles[ptrValue] = handle;
			handle.Disposed += (s, e) => ValidHandles.Remove(ptrValue);
		}

		public static string GetResultString(string resultName)
		{
			if (_resxManager == null)
				_resxManager = new ResourceManager("FMOD.Sharp.ResultStrings", Assembly.GetExecutingAssembly());
			var str = _resxManager.GetString(resultName, CultureInfo.CurrentCulture);
			return String.IsNullOrEmpty(str) ? "Unknown Error" : str;
		}

		public static string GetResultString(Result result)
		{
			return GetResultString(Enum.GetName(typeof(Result), result));
		}

		public static Version Uint32ToVersion(uint version)
		{
			var str = version.ToString("X8");
			var major = Int32.Parse(str.Substring(0, 4));
			var minor = Int32.Parse(str.Substring(4, 2));
			var build = Int32.Parse(str.Substring(6, 2));
			return new Version(major, minor, build);
		}
	}
}