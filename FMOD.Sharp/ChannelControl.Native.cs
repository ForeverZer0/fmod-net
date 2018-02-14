using System;
using System.Runtime.InteropServices;
using System.Security;
using FMOD.Sharp.Enums;
using FMOD.Sharp.Structs;

namespace FMOD.Sharp
{
	[SuppressUnmanagedCodeSecurity]
	public partial class ChannelControl
	{
		#region Methods

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_ChannelGroup_AddDSP(IntPtr channel, DspIndex index, IntPtr dsp);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_ChannelGroup_AddDSP(IntPtr channelControl, int index, IntPtr dsp);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_ChannelGroup_AddFadePoint(IntPtr channelControl, ulong dspclock, float volume);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_ChannelGroup_Get3DAttributes(IntPtr channelControl, out Vector position,
			out Vector velocity, out Vector altPanPos);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_ChannelGroup_Get3DConeOrientation(IntPtr channelControl, out Vector orientation);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_ChannelGroup_Get3DConeSettings(IntPtr channelControl, out float insideConeAngle,
			out float outsideConeAngle, out float outsideVolume);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_ChannelGroup_Get3DCustomRolloff(IntPtr channelControl, out IntPtr points,
			out int numPoints);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_ChannelGroup_Get3DDistanceFilter(IntPtr channelControl, out bool custom,
			out float customLevel, out float centerFreq);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_ChannelGroup_Get3DDopplerLevel(IntPtr channelControl, out float level);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_ChannelGroup_Get3DLevel(IntPtr channelControl, out float level);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_ChannelGroup_Get3DMinMaxDistance(IntPtr channelControl, out float minDistance,
			out float maxDistance);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_ChannelGroup_Get3DOcclusion(IntPtr channelControl, out float directOcclusion,
			out float reverbOcclusion);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_ChannelGroup_Get3DSpread(IntPtr channelControl, out float angle);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_ChannelGroup_GetAudibility(IntPtr channelControl, out float audibility);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_ChannelGroup_GetDelay(IntPtr channelControl, out ulong dspClockStart,
			out ulong dspClockEnd, out bool stopChannels);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_ChannelGroup_GetDSP(IntPtr channelControl, int index, out IntPtr dsp);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_ChannelGroup_GetDSPClock(IntPtr channelControl, out ulong dspclock,
			out ulong parentClock);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_ChannelGroup_GetDSPIndex(IntPtr channelControl, IntPtr dsp, out int index);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_ChannelGroup_GetFadePoints(IntPtr channelControl, out uint numPoints,
			ulong[] pointDspClock, float[] pointVolume);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_ChannelGroup_GetLowPassGain(IntPtr channelControl, out float gain);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_ChannelGroup_GetMixMatrix(IntPtr channelControl, float[] matrix,
			out int outChannels, out int inChannels, int inChannelHop);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_ChannelGroup_GetMode(IntPtr channelControl, out Mode mode);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_ChannelGroup_GetMute(IntPtr channelControl, out bool mute);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_ChannelGroup_GetNumDSPs(IntPtr channelControl, out int numDsps);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_ChannelGroup_GetPaused(IntPtr channelControl, out bool paused);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_ChannelGroup_GetPitch(IntPtr channelControl, out float pitch);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_ChannelGroup_GetReverbProperties(IntPtr channelControl, int instance,
			out float wet);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_ChannelGroup_GetSystemObject(IntPtr channelControl, out IntPtr system);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_ChannelGroup_GetUserData(IntPtr channelControl, out IntPtr userData);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_ChannelGroup_GetVolume(IntPtr channelControl, out float volume);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_ChannelGroup_GetVolumeRamp(IntPtr channelControl, out bool ramp);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_ChannelGroup_IsPlaying(IntPtr channelControl, out bool isPlaying);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_ChannelGroup_RemoveDSP(IntPtr channelControl, IntPtr dsp);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_ChannelGroup_RemoveFadePoints(IntPtr channelControl, ulong dspClockStart,
			ulong dspClockEnd);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_ChannelGroup_Set3DAttributes(IntPtr channel, ref Vector position,
			ref Vector velocity, IntPtr altPanPosition);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_ChannelGroup_Set3DAttributes(IntPtr channel, IntPtr position,
			ref Vector velocity, IntPtr altPanPosition);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_ChannelGroup_Set3DAttributes(IntPtr channel, ref Vector position,
			IntPtr velocity, IntPtr altPanPosition);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_ChannelGroup_Set3DConeOrientation(IntPtr channelControl, ref Vector orientation);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_ChannelGroup_Set3DConeSettings(IntPtr channelControl, float insideConeAngle,
			float outsideConeAngle, float outsideVolume);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_ChannelGroup_Set3DCustomRolloff(IntPtr channelControl, ref Vector[] points,
			int numPoints);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_ChannelGroup_Set3DDistanceFilter(IntPtr channelControl, bool custom,
			float customLevel, float centerFreq);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_ChannelGroup_Set3DDopplerLevel(IntPtr channelControl, float level);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_ChannelGroup_Set3DLevel(IntPtr channelControl, float level);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_ChannelGroup_Set3DMinMaxDistance(IntPtr channelControl, float minDistance,
			float maxDistance);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_ChannelGroup_Set3DOcclusion(IntPtr channelControl, float directOcclusion,
			float reverbOcclusion);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_ChannelGroup_Set3DSpread(IntPtr channelControl, float angle);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_ChannelGroup_SetCallback(IntPtr channelControl, ChannelCallback callback);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_ChannelGroup_SetDelay(IntPtr channelControl, ulong dspClockStart,
			ulong dspClockEnd, bool stopChannels);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_ChannelGroup_SetDSPIndex(IntPtr channel, IntPtr dsp, DspIndex index);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_ChannelGroup_SetDSPIndex(IntPtr channelControl, IntPtr dsp, int index);

		[DllImport(Core.LIBRARY)]
		private static extern Result
			FMOD_ChannelGroup_SetFadePointRamp(IntPtr channelControl, ulong dspClock, float volume);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_ChannelGroup_SetLowPassGain(IntPtr channelControl, float gain);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_ChannelGroup_SetMixLevelsInput(IntPtr channelControl, float[] levels,
			int numLevels);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_ChannelGroup_SetMixLevelsOutput(IntPtr channelControl, float frontLeft,
			float frontRight, float center, float lowFreq, float surroundLeft, float surroundRight, float backLeft, float backRight);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_ChannelGroup_SetMixMatrix(IntPtr channelControl, float[] matrix, int outChannels,
			int inChannels, int inChannelHop);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_ChannelGroup_SetMode(IntPtr channelControl, Mode mode);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_ChannelGroup_SetMute(IntPtr channelControl, bool mute);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_ChannelGroup_SetPan(IntPtr channelControl, float pan);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_ChannelGroup_SetPaused(IntPtr channelControl, bool paused);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_ChannelGroup_SetPitch(IntPtr channelControl, float pitch);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_ChannelGroup_SetReverbProperties(IntPtr channelControl, int instance, float wet);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_ChannelGroup_SetUserData(IntPtr channelControl, IntPtr userData);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_ChannelGroup_SetVolume(IntPtr channelControl, float volume);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_ChannelGroup_SetVolumeRamp(IntPtr channelControl, bool ramp);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_ChannelGroup_Stop(IntPtr channelControl);

		#endregion
	}
}