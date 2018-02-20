using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;

namespace FMOD.Core
{
	public static class Factory
	{
		private const BindingFlags BINDING_FLAGS = BindingFlags.NonPublic | BindingFlags.Instance;

		private static readonly Dictionary<IntPtr, HandleBase> _handles;

		static Factory()
		{
			_handles = new Dictionary<IntPtr, HandleBase>();
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
			AddReference(obj);
			return obj;
		}

		private static void RemoveReference(IntPtr handle)
		{
			if (!_handles.ContainsKey(handle)) 
				return;
			_handles[handle] = null;
			_handles.Remove(handle);
		}

		public static void AddReference<T>(T handle) where T : HandleBase
		{
			var ptr = (IntPtr) handle;
			handle.Disposed += (s, e) => RemoveReference(ptr);
			_handles[ptr] = handle;
		}
	}
}