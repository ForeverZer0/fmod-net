using System;
using System.Runtime.InteropServices;
using FMOD.Sharp.Enums;
using FMOD.Sharp.Structs;

namespace FMOD.Sharp
{
	public partial class Channel
	{
		#region Native Methods

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_Channel_AddDSP(IntPtr channel, DspIndex index, IntPtr dsp);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_Channel_AddFadePoint(IntPtr channel, ulong dspClock, float volume);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_Channel_Get3DAttributes(IntPtr channel, out Vector position,
			out Vector velocity, out Vector altPanPosition);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_Channel_Get3DConeOrientation(IntPtr channel, out Vector orientation);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_Channel_Get3DConeSettings(IntPtr channel, out float insidecConeAngle,
			out float outsideConeAngle, out float outsideVolume);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_Channel_Get3DCustomRolloff(IntPtr channel, out IntPtr points, out int numpoints);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_Channel_Get3DDistanceFilter(IntPtr channel, out bool custom,
			out float customLevel, out float centerFreq);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_Channel_Get3DDopplerLevel(IntPtr channel, out float level);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_Channel_Get3DLevel(IntPtr channel, out float level);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_Channel_Get3DMinMaxDistance(IntPtr channel, out float minDistance,
			out float maxDistance);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_Channel_Get3DOcclusion(IntPtr channel, out float directOcclusion,
			out float reverbOcclusion);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_Channel_Get3DSpread(IntPtr channel, out float angle);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_Channel_GetAudibility(IntPtr channel, out float audibility);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_Channel_GetChannelGroup(IntPtr channel, out IntPtr channelGroup);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_Channel_GetCurrentSound(IntPtr channel, out IntPtr sound);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_Channel_GetDelay(IntPtr channel, out uint dspClockStart, out uint dspClockEnd,
			out bool stopChannels);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_Channel_GetDSP(IntPtr channel, int index, out IntPtr dsp);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_Channel_GetDSPClock(IntPtr channel, out ulong head, out ulong tail);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_Channel_GetDSPIndex(IntPtr channel, IntPtr dsp, out int index);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_Channel_GetFadePoints(IntPtr channel, out uint numPoints,
			[MarshalAs(UnmanagedType.LPArray)] ulong[] dspClocks, [MarshalAs(UnmanagedType.LPArray)] float[] volumes);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_Channel_GetFrequency(IntPtr channel, out float frequency);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_Channel_GetIndex(IntPtr channel, out int index);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_Channel_GetLoopCount(IntPtr channel, out int loopCount);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_Channel_GetLoopPoints(IntPtr channel, out uint loopStart, TimeUnit loopStartUnit,
			out uint loopEnd, TimeUnit loopEndUnit);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_Channel_GetLowPassGain(IntPtr channel, out float gain);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_Channel_GetMixMatrix(IntPtr channel, float[] matrix,
			out int outChannels, out int inChannels, int inChannelHop);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_Channel_GetMode(IntPtr channel, out Mode mode);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_Channel_GetMute(IntPtr channel, out bool mute);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_Channel_GetNumDSPs(IntPtr channel, out int numDsps);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_Channel_GetPaused(IntPtr channel, out bool paused);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_Channel_GetPitch(IntPtr channel, out float pitch);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_Channel_GetPosition(IntPtr channel, out uint position, TimeUnit timeUnit);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_Channel_GetPriority(IntPtr channel, out int priority);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_Channel_GetReverbProperties(IntPtr channel, out ReverbProperties prop);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_Channel_GetSystemObject(IntPtr channel, out IntPtr system);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_Channel_GetUserData(IntPtr channel, out IntPtr userData);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_Channel_GetVolume(IntPtr channel, out float volume);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_Channel_GetVolumeRamp(IntPtr channel, out bool volumeRamp);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_Channel_IsPlaying(IntPtr channel, out bool isPlaying);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_Channel_IsVirtual(IntPtr channel, out bool isVirtual);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_Channel_RemoveDSP(IntPtr channel, IntPtr dsp);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_Channel_RemoveFadePoints(IntPtr channel, ulong dspClockStart, ulong dspClockEnd);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_Channel_Set3DAttributes(IntPtr channel, ref Vector position,
			ref Vector velocity, IntPtr altPanPosition);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_Channel_Set3DAttributes(IntPtr channel, IntPtr position,
			ref Vector velocity, IntPtr altPanPosition);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_Channel_Set3DAttributes(IntPtr channel, ref Vector position,
			IntPtr velocity, IntPtr altPanPosition);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_Channel_Set3DConeOrientation(IntPtr channel, ref Vector orientation);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_Channel_Set3DConeSettings(IntPtr channel, float insideConeAngle,
			float outsideConeAngle, float outsideVolume);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_Channel_Set3DCustomRolloff(IntPtr channel, ref Vector[] points, int numpoints);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_Channel_Set3DDistanceFilter(IntPtr channel, bool custom,
			float customLevel, float centerFreq);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_Channel_Set3DDopplerLevel(IntPtr channel, float level);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_Channel_Set3DLevel(IntPtr channel, float level);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_Channel_Set3DMinMaxDistance(IntPtr channel, float minDistance, float maxDistance);

		[DllImport(Core.LIBRARY)]
		private static extern Result
			FMOD_Channel_Set3DOcclusion(IntPtr channel, float directOcclusion, float reverbOcclusion);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_Channel_Set3DSpread(IntPtr channel, float angle);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_Channel_SetCallback(IntPtr channel, ChannelCallback callback);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_Channel_SetChannelGroup(IntPtr channel, IntPtr channelGroup);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_Channel_SetDelay(IntPtr channel, uint dspClockStart, uint dspClockEnd,
			bool stopChannels);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_Channel_SetDSPIndex(IntPtr channel, IntPtr dsp, int index);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_Channel_SetFadePointRamp(IntPtr channel, ulong dspClock, float volume);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_Channel_SetFrequency(IntPtr channel, float frequency);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_Channel_SetLoopCount(IntPtr channel, int loopCount);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_Channel_SetLoopPoints(IntPtr channel, uint loopStart, TimeUnit loopStartUnit,
			uint loopEnd, TimeUnit loopEndUnit);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_Channel_SetLowPassGain(IntPtr channel, float gain);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_Channel_SetMixLevelsInput(IntPtr channel, float[] levels, int numLevels);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_Channel_SetMixLevelsOutput(IntPtr channel, float frontLeft, float frontRight,
			float center, float lowFreq, float surroundLeft, float surroundRight, float backLeft, float backRight);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_Channel_SetMixMatrix(IntPtr channel, float[] matrix,
			int outChannels, int inChannels, int inChannelHop);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_Channel_SetMode(IntPtr channel, Mode mode);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_Channel_SetMute(IntPtr channel, bool mute);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_Channel_SetPan(IntPtr channel, float pan);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_Channel_SetPaused(IntPtr channel, bool paused);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_Channel_SetPitch(IntPtr channel, float pitch);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_Channel_SetPosition(IntPtr channel, uint position, TimeUnit timeUnit);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_Channel_SetPriority(IntPtr channel, int priority);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_Channel_SetReverbProperties(IntPtr channel, ref ReverbProperties prop);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_Channel_SetUserData(IntPtr channel, IntPtr userData);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_Channel_SetVolume(IntPtr channel, float volume);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_Channel_SetVolumeRamp(IntPtr channel, bool volumeRamp);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_Channel_Stop(IntPtr channel);

		#endregion
	}
}