#region License

// Factory.cs is distributed under the Microsoft Public License (MS-PL)
// 
// Copyright (c) 2018,  Eric Freed
// All Rights Reserved.
// 
// This license governs use of the accompanying software. If you use the software, you
// accept this license. If you do not accept the license, do not use the software.
// 
// 1. Definitions
// The terms "reproduce," "reproduction," "derivative works," and "distribution" have the
// same meaning here as under U.S. copyright law.
// A "contribution" is the original software, or any additions or changes to the software.
// A "contributor" is any person that distributes its contribution under this license.
// "Licensed patents" are a contributor's patent claims that read directly on its contribution.
// 
// 2. Grant of Rights
// (A) Copyright Grant- Subject to the terms of this license, including the license conditions 
// and limitations in section 3, each contributor grants you a non-exclusive, worldwide, royalty-free 
// copyright license to reproduce its contribution, prepare derivative works of its contribution, and 
// distribute its contribution or any derivative works that you create.
// 
// (B) Patent Grant- Subject to the terms of this license, including the license conditions and 
// limitations in section 3, each contributor grants you a non-exclusive, worldwide, royalty-free license
//  under its licensed patents to make, have made, use, sell, offer for sale, import, and/or otherwise 
// dispose of its contribution in the software or derivative works of the contribution in the software.
// 
// 3. Conditions and Limitations
// (A) No Trademark License- This license does not grant you rights to use any contributors' name, 
// logo, or trademarks.
// 
// (B) If you bring a patent claim against any contributor over patents that you claim are infringed by 
// the software, your patent license from such contributor to the software ends automatically.
// 
// (C) If you distribute any portion of the software, you must retain all copyright, patent, trademark, and
//  attribution notices that are present in the software.
// 
// (D) If you distribute any portion of the software in source code form, you may do so only under this 
// license by including a complete copy of this license with your distribution. If you distribute any portion
//  of the software in compiled or object code form, you may only do so under a license that complies 
// with this license.
// 
// (E) The software is licensed "as-is." You bear the risk of using it. The contributors give no express 
// warranties, guarantees or conditions. You may have additional consumer rights under your local laws 
// which this license cannot change. To the extent permitted under your local laws, the contributors 
// exclude the implied warranties of merchantability, fitness for a particular purpose and non-infringement.
// 
// Created 9:49 PM 02/15/2018

#endregion

#region Using Directives

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using FMOD.NET.Enumerations;

#endregion

namespace FMOD.NET.Core
{
	/// <summary>
	///     Static factory class for creating the classes that inherit from <see cref="HandleBase" />.
	/// </summary>
	/// <remarks>
	///     <para><b>ALL</b> wrapper objects should be created via the <see cref="Create{T}" /> method.</para>
	///     <para>
	///         Although it is possible to simple create a new <see cref="HandleBase" /> object pointing to the same object,
	///         subscription to events would be lost by creating a new object.
	///     </para>
	///     <para>
	///         This class solves that problem by maintaining reference to each created object, using the underlying handle
	///         as an identifier. When the <see cref="Create{T}" /> method is called with a native pointer, it is first looked
	///         up in a dictionary of existing handles, and returns the existing object if found, not create a new handle.
	///     </para>
	///     <para>
	///         When a new object is created via <see cref="Create{T}" />, it is automatically bound to that object's
	///         <see cref="HandleBase.Disposed" /> event, so that the object can be de-referenced when it is no longer valid.
	///     </para>
	/// </remarks>
	[SuppressUnmanagedCodeSecurity]
	[SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
	public static class Factory
	{
		private const BindingFlags BINDING_FLAGS = BindingFlags.NonPublic | BindingFlags.Instance;

		// ReSharper disable once NotAccessedField.Local
		private static readonly FactoryDestroyer _destroyer;

		private static readonly Dictionary<IntPtr, HandleBase> _handles;

		#region Constructors

		/// <summary>
		///     Initializes the <see cref="Factory" /> class.
		/// </summary>
		static Factory()
		{
			_handles = new Dictionary<IntPtr, HandleBase>();
			_destroyer = new FactoryDestroyer();
		}

		#endregion

		#region Native Methods

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_System_Create(out IntPtr system);

		#endregion

		#region Methods

		/// <summary>
		///     <para>Adds the and stores a reference to a native handle. </para>
		///     <para>
		///         This function is automatically called via the <see cref="Create{T}" /> method, and should only be manually
		///         invoked in special circumstances where an object is created by other means.
		///     </para>
		/// </summary>
		/// <typeparam name="T">A type derived from <see cref="HandleBase" />.</typeparam>
		/// <param name="handle">The platform specific handle to the object.</param>
		public static void AddReference<T>(T handle) where T : HandleBase
		{
			var ptr = (IntPtr) handle;
			#if DEBUG
			// ReSharper disable once LocalizableElement
			Console.WriteLine($"Created ${typeof(T)} object with handle {ptr}.");
			#endif
			handle.Disposed += (s, e) => RemoveReference(ptr);
			_handles[ptr] = handle;
		}

		/// <summary>
		///     Creates the an object representing the specified handle or returns an existing one if it already exists.
		/// </summary>
		/// <typeparam name="T">A type derived from <see cref="HandleBase" />.</typeparam>
		/// <param name="handle">The platform specific handle to the object.</param>
		/// <returns>A managed type representing an unmanaged type specified by the <paramref name="handle" />.</returns>
		public static T Create<T>(IntPtr handle) where T : HandleBase
		{
			if (handle == IntPtr.Zero)
				return null;
			T obj;
			if (_handles.ContainsKey(handle))
			{
				obj = (T) _handles[handle];
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
		///     <para>Creates and returns a new <see cref="FmodSystem" /> object.</para>
		///     <para>
		///         This must be called to create an <see cref="FmodSystem" /> object before you can do anything else. Use this
		///         function to create a single, or multiple instances of system objects.
		///     </para>
		/// </summary>
		/// <returns>A newly created <see cref="FmodSystem" /> object.</returns>
		/// <remarks>
		///     <alert class="warning">
		///         Calls to this method and <see cref="FmodSystem.Dispose" /> are not thread-safe. Do not call these functions
		///         simultaneously from multiple threads at once.
		///     </alert>
		/// </remarks>
		/// <seealso cref="FmodSystem.Initialize()" />
		/// <seealso cref="FmodSystem.Dispose" />
		public static FmodSystem CreateSystem()
		{
			var result = FMOD_System_Create(out var system);
			if (result != Result.OK)
				throw new FmodException(result);
			return Create<FmodSystem>(system);
		}

		/// <summary>
		///     Releases all referenced instances of <see cref="HandleBase" /> tracked by the <see cref="Factory" /> class by
		///     invoking the <see cref="HandleBase.Dispose" /> method.
		/// </summary>
		public static void ReleaseAll()
		{
			foreach (var handle in _handles.Values)
				handle.Dispose();
		}

		/// <summary>
		/// <para>Removes an already released reference.</para>
		/// <para>This method is called automatically when an instance of <see cref="HandleBase"/> is disposed.</para>
		/// </summary>
		/// <param name="handle">The platform specific handle to the object.</param>
		private static void RemoveReference(IntPtr handle)
		{
			if (!_handles.ContainsKey(handle))
				return;
			_handles[handle] = null;
			_handles.Remove(handle);
		}

		#endregion

		/// <summary>
		/// Uses singleton method to implement a destructor on the static class.
		/// </summary>
		private sealed class FactoryDestroyer
		{
			~FactoryDestroyer()
			{
				ReleaseAll();
			}
		}
	}
}