#region License

// DspState.cs is distributed under the Microsoft Public License (MS-PL)
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
// Created 3:42 AM 02/11/2018

#endregion

#region Using Directives

using System;
using System.Runtime.InteropServices;
using FMOD.NET.Core;

#endregion

namespace FMOD.NET.Structures
{
	/// <summary>
	///     DSP plugin structure that is passed into each callback.
	/// </summary>
	/// <remarks>
	///     <see cref="SystemObject" /> is an integer that relates to the <see cref="FmodSystem" /> object that created the DSP
	///     or registered the DSP plugin. If only one System object is created then it should be 0. A second object would be 1
	///     and so on.
	/// </remarks>
	/// <seealso cref="DspDescription" />
	/// <seealso cref="DspStateFunctions" />
	/// <seealso cref="DspConnection" />
	[StructLayout(LayoutKind.Sequential)]
	public struct DspState
	{
		/// <summary>
		///     Internal instance pointer, should not be used or written to.
		/// </summary>
		public readonly IntPtr Instance;

		/// <summary>
		///     Plugin writer created data the output author wants to attach to this object.
		/// </summary>
		public IntPtr PluginData;

		/// <summary>
		///     Specifies which speakers the DSP effect is active on.
		/// </summary>
		public readonly uint ChannelMask;

		/// <summary>
		///     Specifies which speaker mode the signal originated for information purposes, ie in case panning needs to be done
		///     differently.
		/// </summary>
		public readonly int SourceSpeakerMode;

		/// <summary>
		///     The mixed result of all incoming sidechains is stored at this pointer address.
		/// </summary>
		public readonly IntPtr SideChainData;

		/// <summary>
		///     The number of channels of pcm data stored within the sidechain buffer.
		/// </summary>
		public readonly int SideChainChannels;

		/// <summary>
		///     Struct containing functions to give plugin developers the ability to query system state, access system level
		///     functionality and helpers.
		/// </summary>
		public readonly IntPtr Functions;

		/// <summary>
		///     <see cref="FmodSystem" /> object index, relating to the System object that created this DSP.
		/// </summary>
		public readonly int SystemObject;
	}
}