using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using FMOD.Sharp.Enums;

namespace FMOD.Sharp
{
	[SuppressUnmanagedCodeSecurity][SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
	public partial class SoundGroup
	{
		#region Methods

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_SoundGroup_GetMaxAudible(IntPtr soundgroup, out int maxAudible);

		[DllImport(Core.LIBRARY)]
		private static extern Result
			FMOD_SoundGroup_GetMaxAudibleBehavior(IntPtr soundgroup, out SoundGroupBehavior behavior);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_SoundGroup_GetMuteFadeSpeed(IntPtr soundgroup, out float speed);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_SoundGroup_GetName(IntPtr soundgroup, IntPtr name, int nameLength);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_SoundGroup_GetNumPlaying(IntPtr soundgroup, out int numPlaying);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_SoundGroup_GetNumSounds(IntPtr soundgroup, out int numSounds);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_SoundGroup_GetSound(IntPtr soundgroup, int index, out IntPtr sound);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_SoundGroup_GetSystemObject(IntPtr soundgroup, out IntPtr system);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_SoundGroup_GetUserData(IntPtr soundgroup, out IntPtr userData);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_SoundGroup_GetVolume(IntPtr soundgroup, out float volume);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_SoundGroup_SetMaxAudible(IntPtr soundgroup, int maxAudible);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_SoundGroup_SetMaxAudibleBehavior(IntPtr soundgroup, SoundGroupBehavior behavior);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_SoundGroup_SetMuteFadeSpeed(IntPtr soundgroup, float speed);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_SoundGroup_SetUserData(IntPtr soundgroup, IntPtr userData);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_SoundGroup_SetVolume(IntPtr soundgroup, float volume);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_SoundGroup_Stop(IntPtr soundgroup);

		#endregion
	}
}