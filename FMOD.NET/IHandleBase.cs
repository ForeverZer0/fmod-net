#region License

// IHandleBase.cs is distributed under the Microsoft Public License (MS-PL)
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
// Created 2:11 AM 02/22/2018

#endregion

#region Using Directives

using System;
using FMOD.NET.Core;

#endregion

namespace FMOD.NET
{
	public interface IHandleBase : IEquatable<HandleBase>, IDisposable
	{
		#region Events

		/// <summary>
		///     Occurs when the handle is disposed, and the underlying <b>FMOD</b> handle has been released.
		/// </summary>
		event EventHandler Disposed;

		/// <summary>
		///     Occurs when the user-data has changed.
		/// </summary>
		/// <seealso cref="UserData" />
		event EventHandler UserDataChanged;

		#endregion

		#region Properties

		/// <summary>
		///     Gets or sets the user data to be stored within the object.
		/// </summary>
		/// <value>
		///     The user data.
		/// </value>
		/// <remarks>
		///     You can use this to store a pointer to any wrapper or internal class that is associated with this object. This
		///     is especially useful for callbacks where you need to get back access to your own objects.
		/// </remarks>
		/// <seealso cref="UserDataChanged" />
		IntPtr UserData { get; set; }

		/// <summary>
		///     Gets a value that indicates whether the handle is invalid.
		/// </summary>
		bool IsInvalid { get; }

		/// <summary>
		///     Gets a value indicating whether the handle is closed.
		/// </summary>
		/// <value>
		///     <c>true</c> if this instance is closed; otherwise, <c>false</c>.
		/// </value>
		/// <remarks>
		///     <para>
		///         The IsClosed method returns a value indicating whether the object's handle is no longer associated with a
		///         native resource. This differs from the definition of the <see cref="IsInvalid" /> property, which computes
		///         whether a given handle is always considered invalid. The IsClosed method returns a <c>true</c> value in the
		///         following cases:
		///     </para>
		///     <list type="bullet">
		///         <item>
		///             <para>The <see cref="SetHandleAsInvalid" /> method was called.</para>
		///         </item>
		///         <item>
		///             <para>
		///                 The Dispose method or Close method was called and there are no references to the object on other
		///                 threads.
		///             </para>
		///         </item>
		///     </list>
		/// </remarks>
		bool IsClosed { get; }

		#endregion

		#region Methods

		/// <summary>
		///     Manually increments the reference counter on instances.
		/// </summary>
		/// <param name="success"><c>true</c> if the reference counter was successfully incremented; otherwise, <c>false</c>.</param>
		/// <remarks>
		///     The DangerousAddRef method prevents the common language runtime from reclaiming memory used by a handle (which
		///     occurs when the runtime calls the ReleaseHandle method). You can use this method to manually increment the
		///     reference count on an instance. DangerousAddRef returns a <see cref="Boolean" /> value using a <c>ref</c> parameter
		///     (success) that indicates whether the reference count was incremented successfully. This allows your program logic
		///     to back out in case of failure. You should set success to <c>false</c> before calling DangerousAddRef. If success
		///     is <c>true</c>, avoid resource leaks by matching the call to DangerousAddRef with a corresponding call to
		///     <see cref="DangerousRelease" />.
		/// </remarks>
		void DangerousAddRef(ref bool success);

		/// <summary>
		///     Gets an <see cref="IntPtr" /> representing the value of the handle. If the handle has been marked invalid with
		///     <see cref="SetHandleAsInvalid" />, this method still returns the original handle value, which can be a stale value.
		/// </summary>
		/// <returns>An <see cref="IntPtr" /> representing the value of the handle.</returns>
		/// <remarks>
		///     You can use this method to retrieve the actual handle value from an instance of the SafeHandle derived class.
		///     This method is needed for backwards compatibility because many properties in the .NET Framework return IntPtr
		///     handle types. IntPtr handle types are platform-specific types used to represent a pointer or a handle.
		/// </remarks>
		IntPtr DangerousGetHandle();

		/// <summary>
		///     Manually decrements the reference counter on a SafeHandle instance.
		/// </summary>
		/// <remarks>
		///     The DangerousRelease method is the counterpart to <see cref="DangerousAddRef" />. You should always match a call to
		///     the DangerousRelease method with a successful call to <see cref="DangerousAddRef" />.
		/// </remarks>
		void DangerousRelease();

		/// <summary>
		///     Marks a handle as no longer used.
		/// </summary>
		/// <remarks>
		///     <para>
		///         Call the SetHandleAsInvalid method only when you know that your handle no longer references a resource. Doing
		///         so does not change the value of the handle field; it only marks the handle as closed. The handle might then
		///         contain a potentially stale value. The effect of this call is that no attempt is made to free the resources.
		///     </para>
		///     <para>Use SetHandleAsInvalid only if you need to support a pre-existing handle.</para>
		/// </remarks>
		void SetHandleAsInvalid();

		#endregion
	}
}