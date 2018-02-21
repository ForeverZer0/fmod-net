#region License

// SystemCallbackType.cs is distributed under the Microsoft Public License (MS-PL)
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
// Created 12:50 AM 02/04/2018

#endregion

#region Using Directives

using System;
using FMOD.NET.Core;

#endregion

namespace FMOD.NET.Enumerations
{
	/// <summary>
	///     These callback types are used with <see cref="FmodSystem.SetCallback" />.
	/// </summary>
	/// <remarks>
	///     <para>Each callback has <i>commanddata</i> parameters passed as <c>void*</c> unique to the type of callback.</para>
	///     <para>See reference to <see cref="SystemCallback" /> to determine what they might mean for each type of callback.</para>
	///     <alert class="note">
	///         <para>
	///             Using <see cref="DeviceListChanged" /> (Windows only) will disable any automated device
	///             ejection/insertion handling by FMOD. Use this callback to control the behaviour yourself.
	///         </para>
	///         <para>
	///             Using <see cref="DeviceListChanged" /> (on Mac only) requires the application to be running an event loop
	///             which will allow external changes to device list to be detected by <b>FMOD</b>.
	///         </para>
	///         <para>
	///             The "system" object pointer will be<see cref="IntPtr.Zero" /> for <see cref="MemoryAllocationFailed" />
	///             callback.
	///         </para>
	///     </alert>
	/// </remarks>
	/// .
	/// <seealso cref="SystemCallback" />
	/// <seealso cref="FmodSystem.SetCallback" />
	/// <seealso cref="FmodSystem.Update" />
	/// <seealso cref="Dsp.AddInput" />
	[Flags]
	public enum SystemCallbackType : uint
	{
		/// <summary>
		///     Called from <see cref="FmodSystem.Update" /> when the enumerated list of devices has changed.
		/// </summary>
		DeviceListChanged = 0x00000001,

		/// <summary>
		///     Called from <see cref="FmodSystem.Update" /> when an output device has been lost due to control panel parameter
		///     changes and <b>FMOD</b> cannot automatically recover.
		/// </summary>
		DeviceLost = 0x00000002,

		/// <summary>
		///     Called directly when a memory allocation fails somewhere in <b>FMOD</b>. (NOTE - "system" will be
		///     <see cref="IntPtr.Zero" /> in this callback type.)
		/// </summary>
		MemoryAllocationFailed = 0x00000004,

		/// <summary>
		///     Called directly when a thread is created.
		/// </summary>
		ThreadCreated = 0x00000008,

		/// <summary>
		///     <para>Called when a bad connection was made with <see cref="Dsp.AddInput" />.</para>
		///     <para>Usually called from mixer thread because that is where the connections are made. </para>
		/// </summary>
		BadDspConnection = 0x00000010,

		/// <summary>
		///     Called each tick before a mix update happens.
		/// </summary>
		PreMix = 0x00000020,

		/// <summary>
		///     Called each tick after a mix update happens.
		/// </summary>
		PostMix = 0x00000040,

		/// <summary>
		///     Called when each API function returns an error code, including delayed async functions.
		/// </summary>
		Error = 0x00000080,

		/// <summary>
		///     Called each tick in mix update after clocks have been updated before the main mix occurs.
		/// </summary>
		MidMix = 0x00000100,

		/// <summary>
		///     Called directly when a thread is destroyed.
		/// </summary>
		ThreadDestroyed = 0x00000200,

		/// <summary>
		///     Called at start of <see cref="FmodSystem.Update" /> function.
		/// </summary>
		PreUpdate = 0x00000400,

		/// <summary>
		///     Called at end of <see cref="FmodSystem.Update" /> function.
		/// </summary>
		PostUpdate = 0x00000800,

		/// <summary>
		///     Called from <see cref="FmodSystem.Update" /> when the enumerated list of recording devices has changed.
		/// </summary>
		RecordListChanged = 0x00001000,

		/// <summary>
		///     Pass this mask to <see cref="FmodSystem.SetCallback" /> to receive all callback types.
		/// </summary>
		All = 0xFFFFFFFF
	}
}