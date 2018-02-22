#region License

// MemoryType.cs is distributed under the Microsoft Public License (MS-PL)
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
// Created 12:52 AM 02/04/2018

#endregion

#region Using Directives

using System;
using System.Runtime.InteropServices;
using FMOD.NET.Structures;

#endregion

namespace FMOD.NET.Enumerations
{
	/// <summary>
	///     <para>Bit fields for memory allocation type being passed into FMOD memory callbacks.</para>
	///     <para>
	///         Remember this is a bitfield. You may get more than 1 bit set (ie physical + persistent) so do not simply
	///         switch on the types! You must check each bit individually or clear out the bits that you do not want within the
	///         callback.
	///     </para>
	///     Bits can be excluded if you want during <see cref="O:FMOD.NET.Core.MemoryManager.Initialize" /> so that you never get
	///     them.
	/// </summary>
	/// <seealso cref="MemoryAllocCallback" />
	/// <seealso cref="MemoryReallocCallback" />
	/// <seealso cref="MemoryFreeCallback" />
	/// <seealso cref="O:FMOD.NET.Core.MemoryManager.Initialize" />
	[Flags]
	public enum MemoryType : uint
	{
		/// <summary>
		///     Standard memory.
		/// </summary>
		Normal = 0x00000000,

		/// <summary>
		///     Stream file buffer, size controllable with <see cref="O:FMOD.NET.Core.FmodSystem.SetStreamBufferSize" />.
		/// </summary>
		StreamFile = 0x00000001,

		/// <summary>
		///     Stream decode buffer, size controllable with <see cref="CreateSoundExInfo.DecodeBufferSize" />.
		/// </summary>
		StreamDecode = 0x00000002,

		/// <summary>
		///     Sample data buffer. Raw audio data, usually PCM/MPEG/ADPCM/XMA data.
		/// </summary>
		SampleData = 0x00000004,

		/// <summary>
		///     DSP memory block allocated when more than one output exists on a DSP node.
		/// </summary>
		DspBuffer = 0x00000008,

		/// <summary>
		///     Memory allocated by a third party plugin.
		/// </summary>
		Plugin = 0x00000010,

		/// <summary>
		///     Requires XPhysicalAlloc / XPhysicalFree.
		/// </summary>
		Xbox360Physical = 0x00100000,

		/// <summary>
		///     Persistent memory. Memory will be freed when <see cref="SafeHandle.Dispose" /> is called.
		/// </summary>
		Persistent = 0x00200000,

		/// <summary>
		///     Secondary memory. Allocation should be in secondary memory. For example RSX on the PS3.
		/// </summary>
		Secondary = 0x00400000,

		/// <summary>
		///     All
		/// </summary>
		All = 0xFFFFFFFF
	}
}