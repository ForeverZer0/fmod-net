#region License

// ChannelControl.Native.cs is distributed under the Microsoft Public License (MS-PL)
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
using FMOD.NET.Enumerations;
using FMOD.NET.Structures;

#endregion

namespace FMOD.NET.Core
{
	[SuppressUnmanagedCodeSecurity]
	[SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
	public partial class ChannelControl
	{
		#region Native Methods

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_ChannelGroup_AddDSP(IntPtr channelControl, int index, IntPtr dsp);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_ChannelGroup_AddFadePoint(IntPtr channelControl, ulong dspclock, float volume);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_ChannelGroup_Get3DAttributes(IntPtr channelControl, out Vector position,
			out Vector velocity, out Vector altPanPos);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_ChannelGroup_Get3DConeOrientation(IntPtr channelControl, out Vector orientation);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_ChannelGroup_Get3DConeSettings(IntPtr channelControl, out float insideConeAngle,
			out float outsideConeAngle, out float outsideVolume);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_ChannelGroup_Get3DCustomRolloff(IntPtr channelControl, out IntPtr points,
			out int numPoints);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_ChannelGroup_Get3DDistanceFilter(IntPtr channelControl, out bool custom,
			out float customLevel, out float centerFreq);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_ChannelGroup_Get3DDopplerLevel(IntPtr channelControl, out float level);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_ChannelGroup_Get3DLevel(IntPtr channelControl, out float level);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_ChannelGroup_Get3DMinMaxDistance(IntPtr channelControl, out float minDistance,
			out float maxDistance);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_ChannelGroup_Get3DOcclusion(IntPtr channelControl, out float directOcclusion,
			out float reverbOcclusion);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_ChannelGroup_Get3DSpread(IntPtr channelControl, out float angle);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_ChannelGroup_GetAudibility(IntPtr channelControl, out float audibility);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_ChannelGroup_GetDelay(IntPtr channelControl, out ulong dspClockStart,
			out ulong dspClockEnd, out bool stopChannels);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_ChannelGroup_GetDSP(IntPtr channelControl, int index, out IntPtr dsp);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_ChannelGroup_GetDSPClock(IntPtr channelControl, out ulong dspclock,
			out ulong parentClock);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_ChannelGroup_GetDSPIndex(IntPtr channelControl, IntPtr dsp, out int index);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_ChannelGroup_GetFadePoints(IntPtr channelControl, out uint numPoints,
			ulong[] pointDspClock, float[] pointVolume);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_ChannelGroup_GetLowPassGain(IntPtr channelControl, out float gain);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_ChannelGroup_GetMixMatrix(IntPtr channelControl, float[] matrix,
			out int outChannels, out int inChannels, int inChannelHop);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_ChannelGroup_GetMode(IntPtr channelControl, out Mode mode);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_ChannelGroup_GetMute(IntPtr channelControl, out bool mute);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_ChannelGroup_GetNumDSPs(IntPtr channelControl, out int numDsps);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_ChannelGroup_GetPaused(IntPtr channelControl, out bool paused);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_ChannelGroup_GetPitch(IntPtr channelControl, out float pitch);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_ChannelGroup_GetReverbProperties(IntPtr channelControl, int instance,
			out float wet);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_ChannelGroup_GetSystemObject(IntPtr channelControl, out IntPtr system);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_ChannelGroup_GetVolume(IntPtr channelControl, out float volume);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_ChannelGroup_GetVolumeRamp(IntPtr channelControl, out bool ramp);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_ChannelGroup_IsPlaying(IntPtr channelControl, out bool isPlaying);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_ChannelGroup_RemoveDSP(IntPtr channelControl, IntPtr dsp);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_ChannelGroup_RemoveFadePoints(IntPtr channelControl, ulong dspClockStart,
			ulong dspClockEnd);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_ChannelGroup_Set3DAttributes(IntPtr channel, ref Vector position,
			ref Vector velocity, IntPtr altPanPosition);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_ChannelGroup_Set3DAttributes(IntPtr channel, IntPtr position,
			ref Vector velocity, IntPtr altPanPosition);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_ChannelGroup_Set3DAttributes(IntPtr channel, ref Vector position,
			IntPtr velocity, IntPtr altPanPosition);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_ChannelGroup_Set3DConeOrientation(IntPtr channelControl, ref Vector orientation);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_ChannelGroup_Set3DConeSettings(IntPtr channelControl, float insideConeAngle,
			float outsideConeAngle, float outsideVolume);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_ChannelGroup_Set3DCustomRolloff(IntPtr channelControl, ref Vector[] points,
			int numPoints);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_ChannelGroup_Set3DDistanceFilter(IntPtr channelControl, bool custom,
			float customLevel, float centerFreq);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_ChannelGroup_Set3DDopplerLevel(IntPtr channelControl, float level);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_ChannelGroup_Set3DLevel(IntPtr channelControl, float level);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_ChannelGroup_Set3DMinMaxDistance(IntPtr channelControl, float minDistance,
			float maxDistance);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_ChannelGroup_Set3DOcclusion(IntPtr channelControl, float directOcclusion,
			float reverbOcclusion);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_ChannelGroup_Set3DSpread(IntPtr channelControl, float angle);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_ChannelGroup_SetCallback(IntPtr channelControl, ChannelCallback callback);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_ChannelGroup_SetDelay(IntPtr channelControl, ulong dspClockStart,
			ulong dspClockEnd, bool stopChannels);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_ChannelGroup_SetDSPIndex(IntPtr channel, IntPtr dsp, DspIndex index);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_ChannelGroup_SetDSPIndex(IntPtr channelControl, IntPtr dsp, int index);

		[DllImport(Constants.LIBRARY)]
		private static extern Result
			FMOD_ChannelGroup_SetFadePointRamp(IntPtr channelControl, ulong dspClock, float volume);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_ChannelGroup_SetLowPassGain(IntPtr channelControl, float gain);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_ChannelGroup_SetMixLevelsInput(IntPtr channelControl, float[] levels,
			int numLevels);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_ChannelGroup_SetMixLevelsOutput(IntPtr channelControl, float frontLeft,
			float frontRight, float center, float lowFreq, float surroundLeft, float surroundRight, float backLeft,
			float backRight);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_ChannelGroup_SetMixMatrix(IntPtr channelControl, float[] matrix, int outChannels,
			int inChannels, int inChannelHop);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_ChannelGroup_SetMode(IntPtr channelControl, Mode mode);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_ChannelGroup_SetMute(IntPtr channelControl, bool mute);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_ChannelGroup_SetPan(IntPtr channelControl, float pan);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_ChannelGroup_SetPaused(IntPtr channelControl, bool paused);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_ChannelGroup_SetPitch(IntPtr channelControl, float pitch);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_ChannelGroup_SetReverbProperties(IntPtr channelControl, int instance, float wet);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_ChannelGroup_SetVolume(IntPtr channelControl, float volume);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_ChannelGroup_SetVolumeRamp(IntPtr channelControl, bool ramp);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_ChannelGroup_Stop(IntPtr channelControl);

		#endregion
	}
}