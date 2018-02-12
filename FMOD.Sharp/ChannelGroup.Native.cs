using System;
using System.Runtime.InteropServices;
using FMOD.Sharp.Enums;
using FMOD.Sharp.Structs;

namespace FMOD.Sharp
{
	public partial class ChannelGroup
	{

        [DllImport(Core.LIBRARY)]
        private static extern Result FMOD_ChannelGroup_Stop(IntPtr channelgroup);
        [DllImport(Core.LIBRARY)]
        private static extern Result FMOD_ChannelGroup_SetPaused(IntPtr channelgroup, bool paused);
        [DllImport(Core.LIBRARY)]
        private static extern Result FMOD_ChannelGroup_GetPaused(IntPtr channelgroup, out bool paused);
        [DllImport(Core.LIBRARY)]
        private static extern Result FMOD_ChannelGroup_GetVolume(IntPtr channelgroup, out float volume);
        [DllImport(Core.LIBRARY)]
        private static extern Result FMOD_ChannelGroup_SetVolumeRamp(IntPtr channelgroup, bool ramp);
        [DllImport(Core.LIBRARY)]
        private static extern Result FMOD_ChannelGroup_GetVolumeRamp(IntPtr channelgroup, out bool ramp);
        [DllImport(Core.LIBRARY)]
        private static extern Result FMOD_ChannelGroup_GetAudibility(IntPtr channelgroup, out float audibility);
        [DllImport(Core.LIBRARY)]
        private static extern Result FMOD_ChannelGroup_SetPitch(IntPtr channelgroup, float pitch);
        [DllImport(Core.LIBRARY)]
        private static extern Result FMOD_ChannelGroup_GetPitch(IntPtr channelgroup, out float pitch);
        [DllImport(Core.LIBRARY)]
        private static extern Result FMOD_ChannelGroup_SetMute(IntPtr channelgroup, bool mute);
        [DllImport(Core.LIBRARY)]
        private static extern Result FMOD_ChannelGroup_GetMute(IntPtr channelgroup, out bool mute);
        [DllImport(Core.LIBRARY)]
        private static extern Result FMOD_ChannelGroup_SetReverbProperties(IntPtr channelgroup, int instance, float wet);
        [DllImport(Core.LIBRARY)]
        private static extern Result FMOD_ChannelGroup_GetReverbProperties(IntPtr channelgroup, int instance, out float wet);
        [DllImport(Core.LIBRARY)]
        private static extern Result FMOD_ChannelGroup_SetLowPassGain(IntPtr channelgroup, float gain);
        [DllImport(Core.LIBRARY)]
        private static extern Result FMOD_ChannelGroup_GetLowPassGain(IntPtr channelgroup, out float gain);
        [DllImport(Core.LIBRARY)]
        private static extern Result FMOD_ChannelGroup_SetMode(IntPtr channelgroup, Mode mode);
        [DllImport(Core.LIBRARY)]
        private static extern Result FMOD_ChannelGroup_GetMode(IntPtr channelgroup, out Mode mode);
        [DllImport(Core.LIBRARY)]
        private static extern Result FMOD_ChannelGroup_SetCallback(IntPtr channelgroup, ChannelCallback callback);
        [DllImport(Core.LIBRARY)]
        private static extern Result FMOD_ChannelGroup_IsPlaying(IntPtr channelgroup, out bool isplaying);
        [DllImport(Core.LIBRARY)]
        private static extern Result FMOD_ChannelGroup_SetPan(IntPtr channelgroup, float pan);
        [DllImport(Core.LIBRARY)]
        private static extern Result FMOD_ChannelGroup_SetMixLevelsOutput(IntPtr channelgroup, float frontleft, float frontright, float center, float lfe, float surroundleft, float surroundright, float backleft, float backright);
        [DllImport(Core.LIBRARY)]
        private static extern Result FMOD_ChannelGroup_SetMixLevelsInput(IntPtr channelgroup, float[] levels, int numlevels);
        [DllImport(Core.LIBRARY)]
        private static extern Result FMOD_ChannelGroup_SetMixMatrix(IntPtr channelgroup, float[] matrix, int outchannels, int inchannels, int inchannel_hop);
        [DllImport(Core.LIBRARY)]
        private static extern Result FMOD_ChannelGroup_GetMixMatrix(IntPtr channelgroup, float[] matrix, out int outchannels, out int inchannels, int inchannel_hop);
        [DllImport(Core.LIBRARY)]
        private static extern Result FMOD_ChannelGroup_GetDSPClock(IntPtr channelgroup, out ulong dspclock, out ulong parentclock);
        [DllImport(Core.LIBRARY)]
        private static extern Result FMOD_ChannelGroup_SetDelay(IntPtr channelgroup, ulong dspclock_start, ulong dspclock_end, bool stopchannels);
        [DllImport(Core.LIBRARY)]
        private static extern Result FMOD_ChannelGroup_GetDelay(IntPtr channelgroup, out ulong dspclock_start, out ulong dspclock_end, out bool stopchannels);
        [DllImport(Core.LIBRARY)]
        private static extern Result FMOD_ChannelGroup_AddFadePoint(IntPtr channelgroup, ulong dspclock, float volume);
        [DllImport(Core.LIBRARY)]
        private static extern Result FMOD_ChannelGroup_SetFadePointRamp(IntPtr channelgroup, ulong dspclock, float volume);
        [DllImport(Core.LIBRARY)]
        private static extern Result FMOD_ChannelGroup_RemoveFadePoints(IntPtr channelgroup, ulong dspclock_start, ulong dspclock_end);
        [DllImport(Core.LIBRARY)]
        private static extern Result FMOD_ChannelGroup_GetFadePoints(IntPtr channelgroup, ref uint numpoints, ulong[] point_dspclock, float[] point_volume);
        [DllImport(Core.LIBRARY)]
        private static extern Result FMOD_ChannelGroup_Set3DAttributes(IntPtr channelgroup, ref Vector pos, ref Vector vel, ref Vector alt_pan_pos);
        [DllImport(Core.LIBRARY)]
        private static extern Result FMOD_ChannelGroup_Get3DAttributes(IntPtr channelgroup, out Vector pos, out Vector vel, out Vector alt_pan_pos);
        [DllImport(Core.LIBRARY)]
        private static extern Result FMOD_ChannelGroup_Set3DMinMaxDistance(IntPtr channelgroup, float mindistance, float maxdistance);
        [DllImport(Core.LIBRARY)]
        private static extern Result FMOD_ChannelGroup_Get3DMinMaxDistance(IntPtr channelgroup, out float mindistance, out float maxdistance);
        [DllImport(Core.LIBRARY)]
        private static extern Result FMOD_ChannelGroup_Set3DConeSettings(IntPtr channelgroup, float insideconeangle, float outsideconeangle, float outsidevolume);
        [DllImport(Core.LIBRARY)]
        private static extern Result FMOD_ChannelGroup_Get3DConeSettings(IntPtr channelgroup, out float insideconeangle, out float outsideconeangle, out float outsidevolume);
        [DllImport(Core.LIBRARY)]
        private static extern Result FMOD_ChannelGroup_Set3DConeOrientation(IntPtr channelgroup, ref Vector orientation);
        [DllImport(Core.LIBRARY)]
        private static extern Result FMOD_ChannelGroup_Get3DConeOrientation(IntPtr channelgroup, out Vector orientation);
        [DllImport(Core.LIBRARY)]
        private static extern Result FMOD_ChannelGroup_Set3DCustomRolloff(IntPtr channelgroup, ref Vector points, int numpoints);
        [DllImport(Core.LIBRARY)]
        private static extern Result FMOD_ChannelGroup_Get3DCustomRolloff(IntPtr channelgroup, out IntPtr points, out int numpoints);
        [DllImport(Core.LIBRARY)]
        private static extern Result FMOD_ChannelGroup_Set3DOcclusion(IntPtr channelgroup, float directocclusion, float reverbocclusion);
        [DllImport(Core.LIBRARY)]
        private static extern Result FMOD_ChannelGroup_Get3DOcclusion(IntPtr channelgroup, out float directocclusion, out float reverbocclusion);
        [DllImport(Core.LIBRARY)]
        private static extern Result FMOD_ChannelGroup_Set3DSpread(IntPtr channelgroup, float angle);
        [DllImport(Core.LIBRARY)]
        private static extern Result FMOD_ChannelGroup_Get3DSpread(IntPtr channelgroup, out float angle);
        [DllImport(Core.LIBRARY)]
        private static extern Result FMOD_ChannelGroup_Set3DLevel(IntPtr channelgroup, float level);
        [DllImport(Core.LIBRARY)]
        private static extern Result FMOD_ChannelGroup_Get3DLevel(IntPtr channelgroup, out float level);
        [DllImport(Core.LIBRARY)]
        private static extern Result FMOD_ChannelGroup_Set3DDopplerLevel(IntPtr channelgroup, float level);
        [DllImport(Core.LIBRARY)]
        private static extern Result FMOD_ChannelGroup_Get3DDopplerLevel(IntPtr channelgroup, out float level);
        [DllImport(Core.LIBRARY)]
        private static extern Result FMOD_ChannelGroup_Set3DDistanceFilter(IntPtr channelgroup, bool custom, float customLevel, float centerFreq);
        [DllImport(Core.LIBRARY)]
        private static extern Result FMOD_ChannelGroup_Get3DDistanceFilter(IntPtr channelgroup, out bool custom, out float customLevel, out float centerFreq);
        [DllImport(Core.LIBRARY)]
        private static extern Result FMOD_ChannelGroup_GetSystemObject(IntPtr channelgroup, out IntPtr system);
        [DllImport(Core.LIBRARY)]
        private static extern Result FMOD_ChannelGroup_SetVolume(IntPtr channelgroup, float volume);
        [DllImport(Core.LIBRARY)]
        private static extern Result FMOD_ChannelGroup_GetDSP(IntPtr channelgroup, int index, out IntPtr dsp);
        [DllImport(Core.LIBRARY)]
        private static extern Result FMOD_ChannelGroup_AddDSP(IntPtr channelgroup, int index, IntPtr dsp);
        [DllImport(Core.LIBRARY)]
        private static extern Result FMOD_ChannelGroup_RemoveDSP(IntPtr channelgroup, IntPtr dsp);
        [DllImport(Core.LIBRARY)]
        private static extern Result FMOD_ChannelGroup_GetNumDSPs(IntPtr channelgroup, out int numdsps);
        [DllImport(Core.LIBRARY)]
        private static extern Result FMOD_ChannelGroup_SetDSPIndex(IntPtr channelgroup, IntPtr dsp, int index);
        [DllImport(Core.LIBRARY)]
        private static extern Result FMOD_ChannelGroup_GetDSPIndex(IntPtr channelgroup, IntPtr dsp, out int index);
        [DllImport(Core.LIBRARY)]
        private static extern Result FMOD_ChannelGroup_SetUserData(IntPtr channelgroup, IntPtr userdata);
        [DllImport(Core.LIBRARY)]
        private static extern Result FMOD_ChannelGroup_GetUserData(IntPtr channelgroup, out IntPtr userdata);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_ChannelGroup_Release          (IntPtr channelgroup);
		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_ChannelGroup_AddGroup         (IntPtr channelgroup, IntPtr group, bool propagatedspclock, out IntPtr connection);
		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_ChannelGroup_GetNumGroups     (IntPtr channelgroup, out int numgroups);
		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_ChannelGroup_GetGroup         (IntPtr channelgroup, int index, out IntPtr group);
		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_ChannelGroup_GetParentGroup   (IntPtr channelgroup, out IntPtr group);
		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_ChannelGroup_GetName          (IntPtr channelgroup, IntPtr name, int namelen);
		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_ChannelGroup_GetNumChannels   (IntPtr channelgroup, out int numchannels);
		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_ChannelGroup_GetChannel       (IntPtr channelgroup, int index, out IntPtr channel);

	}
}
