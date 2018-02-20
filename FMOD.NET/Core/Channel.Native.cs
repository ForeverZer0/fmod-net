#region License

// Channel.Native.cs is distributed under the Microsoft Public License (MS-PL)
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
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using FMOD.Enumerations;

#endregion

namespace FMOD.Core
{
	[SuppressUnmanagedCodeSecurity]
	[SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
	public partial class Channel
	{
		#region Native Methods

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_Channel_GetChannelGroup(IntPtr channel, out IntPtr channelGroup);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_Channel_GetCurrentSound(IntPtr channel, out IntPtr sound);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_Channel_GetFrequency(IntPtr channel, out float frequency);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_Channel_GetIndex(IntPtr channel, out int index);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_Channel_GetLoopCount(IntPtr channel, out int loopCount);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_Channel_GetLoopPoints(IntPtr channel, out uint loopStart, TimeUnit loopStartUnit,
			out uint loopEnd, TimeUnit loopEndUnit);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_Channel_GetPosition(IntPtr channel, out uint position, TimeUnit timeUnit);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_Channel_GetPriority(IntPtr channel, out int priority);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_Channel_IsVirtual(IntPtr channel, out bool isVirtual);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_Channel_SetChannelGroup(IntPtr channel, IntPtr channelGroup);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_Channel_SetFrequency(IntPtr channel, float frequency);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_Channel_SetLoopCount(IntPtr channel, int loopCount);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_Channel_SetLoopPoints(IntPtr channel, uint loopStart, TimeUnit loopStartUnit,
			uint loopEnd, TimeUnit loopEndUnit);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_Channel_SetPosition(IntPtr channel, uint position, TimeUnit timeUnit);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_Channel_SetPriority(IntPtr channel, int priority);

		#endregion
	}
}