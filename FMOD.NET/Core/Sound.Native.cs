using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using FMOD.Enumerations;
using FMOD.Structures;

namespace FMOD.Core
{
	[SuppressUnmanagedCodeSecurity]
	[SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
	public partial class Sound
	{
		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_Sound_AddSyncPoint(IntPtr sound, uint offset, TimeUnit offsettype, string name,
			out IntPtr point);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_Sound_DeleteSyncPoint(IntPtr sound, IntPtr point);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_Sound_Get3DConeSettings(IntPtr sound, out float insideconeangle,
			out float outsideconeangle, out float outsidevolume);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_Sound_Get3DCustomRolloff(IntPtr sound, out IntPtr points, out int numpoints);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_Sound_Get3DMinMaxDistance(IntPtr sound, out float min, out float max);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_Sound_GetDefaults(IntPtr sound, out float frequency, out int priority);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_Sound_GetFormat(IntPtr sound, out SoundType type, out SoundFormat format,
			out int channels, out int bits);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_Sound_GetLength(IntPtr sound, out uint length, TimeUnit lengthtype);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_Sound_GetLoopCount(IntPtr sound, out int loopcount);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_Sound_GetLoopPoints(IntPtr sound, out uint loopstart, TimeUnit loopstarttype,
			out uint loopend, TimeUnit loopendtype);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_Sound_GetMode(IntPtr sound, out Mode mode);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_Sound_GetMusicChannelVolume(IntPtr sound, int channel, out float volume);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_Sound_GetMusicNumChannels(IntPtr sound, out int numchannels);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_Sound_GetMusicSpeed(IntPtr sound, out float speed);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_Sound_GetName(IntPtr sound, IntPtr name, int namelen);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_Sound_GetNumSubSounds(IntPtr sound, out int numsubsounds);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_Sound_GetNumSyncPoints(IntPtr sound, out int numsyncpoints);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_Sound_GetNumTags(IntPtr sound, out int numtags, out int numtagsupdated);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_Sound_GetOpenState(IntPtr sound, out OpenState openstate, out uint percentbuffered,
			out bool starving, out bool diskbusy);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_Sound_GetSoundGroup(IntPtr sound, out IntPtr soundgroup);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_Sound_GetSubSound(IntPtr sound, int index, out IntPtr subsound);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_Sound_GetSubSoundParent(IntPtr sound, out IntPtr parentsound);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_Sound_GetSyncPoint(IntPtr sound, int index, out IntPtr point);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_Sound_GetSyncPointInfo(IntPtr sound, IntPtr point, IntPtr name, int namelen,
			out uint offset, TimeUnit offsettype);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_Sound_GetSystemObject(IntPtr sound, out IntPtr system);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_Sound_GetTag(IntPtr sound, string name, int index, out Tag tag);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_Sound_GetUserData(IntPtr sound, out IntPtr userdata);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_Sound_Lock(IntPtr sound, uint offset, uint length, out IntPtr ptr1, out IntPtr ptr2,
			out uint len1, out uint len2);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_Sound_ReadData(IntPtr sound, IntPtr buffer, uint length, out uint read);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_Sound_SeekData(IntPtr sound, uint pcm);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_Sound_Set3DConeSettings(IntPtr sound, float insideconeangle, float outsideconeangle,
			float outsidevolume);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_Sound_Set3DCustomRolloff(IntPtr sound, ref Vector[] points, int numpoints);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_Sound_Set3DMinMaxDistance(IntPtr sound, float min, float max);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_Sound_SetDefaults(IntPtr sound, float frequency, int priority);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_Sound_SetLoopCount(IntPtr sound, int loopcount);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_Sound_SetLoopPoints(IntPtr sound, uint loopstart, TimeUnit loopstarttype,
			uint loopend, TimeUnit loopendtype);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_Sound_SetMode(IntPtr sound, Mode mode);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_Sound_SetMusicChannelVolume(IntPtr sound, int channel, float volume);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_Sound_SetMusicSpeed(IntPtr sound, float speed);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_Sound_SetSoundGroup(IntPtr sound, IntPtr soundgroup);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_Sound_SetUserData(IntPtr sound, IntPtr userdata);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_Sound_Unlock(IntPtr sound, IntPtr ptr1, IntPtr ptr2, uint len1, uint len2);
	}
}