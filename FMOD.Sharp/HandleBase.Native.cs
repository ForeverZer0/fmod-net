using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using FMOD.Sharp.Enums;

namespace FMOD.Sharp
{
	[SuppressUnmanagedCodeSecurity]
	[SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
	public abstract partial class HandleBase
	{
		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_System_Release(IntPtr system);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_ChannelGroup_Release(IntPtr channelGroup);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_Sound_Release(IntPtr sound);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_SoundGroup_Release(IntPtr soundGroup);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_DSP_Release(IntPtr dsp);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_Geometry_Release(IntPtr geometry);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_Reverb3D_Release(IntPtr reverb);
	}
}