using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using FMOD.Enumerations;

namespace FMOD.Core
{
	[SuppressUnmanagedCodeSecurity][SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
	public partial class SoundGroup
	{
		#region Methods

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_SoundGroup_GetMaxAudible(IntPtr soundgroup, out int maxAudible);

		[DllImport(Constants.LIBRARY)]
		private static extern Result
			FMOD_SoundGroup_GetMaxAudibleBehavior(IntPtr soundgroup, out SoundGroupBehavior behavior);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_SoundGroup_GetMuteFadeSpeed(IntPtr soundgroup, out float speed);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_SoundGroup_GetName(IntPtr soundgroup, IntPtr name, int nameLength);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_SoundGroup_GetNumPlaying(IntPtr soundgroup, out int numPlaying);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_SoundGroup_GetNumSounds(IntPtr soundgroup, out int numSounds);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_SoundGroup_GetSound(IntPtr soundgroup, int index, out IntPtr sound);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_SoundGroup_GetSystemObject(IntPtr soundgroup, out IntPtr system);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_SoundGroup_GetUserData(IntPtr soundgroup, out IntPtr userData);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_SoundGroup_GetVolume(IntPtr soundgroup, out float volume);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_SoundGroup_SetMaxAudible(IntPtr soundgroup, int maxAudible);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_SoundGroup_SetMaxAudibleBehavior(IntPtr soundgroup, SoundGroupBehavior behavior);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_SoundGroup_SetMuteFadeSpeed(IntPtr soundgroup, float speed);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_SoundGroup_SetUserData(IntPtr soundgroup, IntPtr userData);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_SoundGroup_SetVolume(IntPtr soundgroup, float volume);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_SoundGroup_Stop(IntPtr soundgroup);

		#endregion
	}
}