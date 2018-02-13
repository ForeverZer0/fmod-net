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
		public const int MAX_CHANNELS = 32;
		public const int MAX_LISTENERS = 8;
		public const int MAX_REVERBS = 4;
		public const int MAX_SYSTEMS = 8;

		private const BindingFlags BINDING_FLAGS = BindingFlags.NonPublic | BindingFlags.Instance;
		private static ResourceManager _resxManager;
		private static readonly Dictionary<IntPtr, Handle> _handles;

#if X64
		public const string LIBRARY = "fmod64";
#elif X86
		public const string LIBRARY = "fmod";
#endif

		static Core()
		{
			_handles = new Dictionary<IntPtr, Handle>();
		}

		public static T Create<T>(IntPtr handle) where T : Handle
		{
			if (handle == IntPtr.Zero)
				return null;
			if (_handles.ContainsKey(handle))
				return (T) _handles[handle];
			var obj = (T) Activator.CreateInstance(typeof(T), BINDING_FLAGS, null, 
				new object[] { handle }, CultureInfo.InvariantCulture);
			obj.Disposed += (s, e) => Destroy(handle);
			_handles[handle] = obj;
			return obj;
		}

		private static void Destroy(IntPtr handle)
		{
			
		}

		public static void AddReference<T>(IntPtr pointer, T handle) where T : Handle
		{
			_handles[pointer] = handle;
			handle.Disposed += (s, e) => _handles.Remove(pointer);
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