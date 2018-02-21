#region License

// DspConnection.Native.cs is distributed under the Microsoft Public License (MS-PL)
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
// Created 9:57 PM 02/15/2018

#endregion

#region Using Directives

using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using FMOD.NET.Enumerations;

#endregion

namespace FMOD.NET.Core
{
	[SuppressUnmanagedCodeSecurity]
	[SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
	public partial class DspConnection
	{
		#region Native Methods

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_DSPConnection_GetInput(IntPtr dspconnection, out IntPtr input);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_DSPConnection_GetMix(IntPtr dspconnection, out float volume);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_DSPConnection_GetMixMatrix(IntPtr dspconnection, float[] matrix,
			out int outchannels, out int inchannels, int inChannelHop);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_DSPConnection_GetOutput(IntPtr dspconnection, out IntPtr output);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_DSPConnection_GetType(IntPtr dspconnection, out DspConnectionType type);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_DSPConnection_GetUserData(IntPtr dspconnection, out IntPtr userdata);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_DSPConnection_SetMix(IntPtr dspconnection, float volume);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_DSPConnection_SetMixMatrix(IntPtr dspconnection, float[] matrix, int outchannels,
			int inchannels, int inChannelHop);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_DSPConnection_SetUserData(IntPtr dspconnection, IntPtr userdata);

		#endregion
	}
}