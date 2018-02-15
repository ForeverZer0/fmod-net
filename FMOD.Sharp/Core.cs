﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Resources;
using FMOD.Sharp.Enums;

namespace FMOD.Sharp
{
	public static class Core
	{
#if X64
		public const string LIBRARY = "fmod64";
#elif X86
		public const string LIBRARY = "fmod";
#endif

		public const int VERSION = 0x00011002;
		public const int MAX_CHANNELS = 32;
		public const int MAX_LISTENERS = 8;
		public const int MAX_REVERBS = 4;
		public const int MAX_SYSTEMS = 8;

		private const BindingFlags BINDING_FLAGS = BindingFlags.NonPublic | BindingFlags.Instance;
		private static readonly ResourceManager _resxManager;
		private static readonly Dictionary<IntPtr, HandleBase> _handles;

		static Core()
		{
			_handles = new Dictionary<IntPtr, HandleBase>();
			_resxManager = new ResourceManager("FMOD.Sharp.ResultStrings", Assembly.GetExecutingAssembly());
		}

		public static T Create<T>(IntPtr handle) where T : HandleBase
		{
			if (handle == IntPtr.Zero)
				return null;
			T obj;
			if (_handles.ContainsKey(handle))
			{
				obj = (T)_handles[handle];
				if (!obj.IsInvalid)
					return obj;
				RemoveReference(handle);
			}
			obj = (T) Activator.CreateInstance(typeof(T), BINDING_FLAGS, null, 
				new object[] { handle }, CultureInfo.InvariantCulture);
			AddReference(handle, obj);
			return obj;
		}

		private static void RemoveReference(IntPtr handle)
		{
			if (!_handles.ContainsKey(handle)) 
				return;
			_handles[handle] = null;
			_handles.Remove(handle);
		}

		public static void AddReference<T>(IntPtr pointer, T handle) where T : HandleBase
		{
			handle.Disposed += (s, e) => RemoveReference(pointer);
			_handles[pointer] = handle;
		}

		public static string GetResultString(string resultName)
		{
			var str = _resxManager.GetString(resultName, CultureInfo.CurrentCulture);
			return String.IsNullOrEmpty(str) ? "Unknown Error" : str;
		}

		public static string GetResultString(Result result)
		{
			return GetResultString(Enum.GetName(typeof(Result), result));
		}

		public static Version UInt32ToVersion(uint version)
		{
			var str = version.ToString("X8");
			var major = Int32.Parse(str.Substring(0, 4));
			var minor = Int32.Parse(str.Substring(4, 2));
			var build = Int32.Parse(str.Substring(6, 2));
			return new Version(major, minor, build);
		}
	}
}