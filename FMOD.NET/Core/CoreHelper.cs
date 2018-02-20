using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Resources;

namespace FMOD.Core
{
	public static class CoreHelper
	{
		private const BindingFlags BINDING_FLAGS = BindingFlags.NonPublic | BindingFlags.Instance;
		private static readonly ResourceManager _resxManager;
		private static readonly Dictionary<IntPtr, HandleBase> _handles;

		static CoreHelper()
		{
			_handles = new Dictionary<IntPtr, HandleBase>();
			_resxManager = new ResourceManager("FMOD.NET.ResultStrings", Assembly.GetExecutingAssembly());
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


	}
}