using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using FMOD.Enumerations;

namespace FMOD.Core
{
	/// <summary>
	/// Static factory class for creating the classes that inherit from <see cref="HandleBase"/>.
	/// </summary>
	/// <remarks>
	/// <para><b>ALL</b> wrapper objects should be created via the <see cref="Create{T}"/> method.</para>
	/// <para>Although it is possible to simple create a new <see cref="HandleBase"/> object pointing to the same object, subscription to events would be lost by creating a new object.</para>
	/// <para>This class solves that problem by maintaining reference to each created object, using the underlying handle as an identifier. When the <see cref="Create{T}"/> method is called with a native pointer, it is first looked up in a dictionary of existing handles, and returns the existing object if found, not create a new handle.</para>
	/// <para>When a new object is created via <see cref="Create{T}"/>, it is automatically bound to that object's <see cref="HandleBase.Disposed"/> event, so that the object can be de-referenced when it is no longer valid.</para>
	/// </remarks>
	[SuppressUnmanagedCodeSecurity][SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
	public static class Factory
	{
		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_System_Create(out IntPtr system);

		private const BindingFlags BINDING_FLAGS = BindingFlags.NonPublic | BindingFlags.Instance;

		private static readonly Dictionary<IntPtr, HandleBase> _handles;

		static Factory()
		{
			_handles = new Dictionary<IntPtr, HandleBase>();
		}

		/// <summary>
		/// Creates the specified handle.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="handle">The handle.</param>
		/// <returns></returns>
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

		/// <summary>
		/// <para>Creates and returns a new <see cref="FmodSystem"/> object.</para>
		/// <para>This must be called to create an <see cref="FmodSystem"/> object before you can do anything else. Use this function to create a single, or multiple instances of system objects.</para>
		/// </summary>
		/// <returns>A newly created <see cref="FmodSystem"/> object.</returns>
		/// <remarks><alert class="warning">
		/// Calls to this method and <see cref="FmodSystem.Dispose"/> are not thread-safe. Do not call these functions simultaneously from multiple threads at once.
		/// </alert></remarks>
		/// <seealso cref="FmodSystem.Initialize()"/>
		/// <seealso cref="FmodSystem.Dispose"/>
		public static FmodSystem CreateSystem()
		{
			var result = FMOD_System_Create(out var system);
			if (result != Result.OK)
				throw new FmodException(result);
			return Create<FmodSystem>(system);
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