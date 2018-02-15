using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using FMOD.Sharp.Enums;

namespace FMOD.Sharp
{
	[SuppressUnmanagedCodeSecurity][SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
	public partial class Sound
	{
		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_Sound_GetSystemObject(IntPtr sound, out IntPtr system);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_Sound_SetMode(IntPtr sound, Mode mode);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_Sound_GetMode(IntPtr sound, out Mode mode);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_Sound_SetLoopCount(IntPtr sound, int loopcount);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_Sound_GetLoopCount(IntPtr sound, out int loopcount);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_Sound_SetLoopPoints(IntPtr sound, uint loopstart, TimeUnit loopstarttype,
			uint loopend, TimeUnit loopendtype);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_Sound_GetLoopPoints(IntPtr sound, out uint loopstart, TimeUnit loopstarttype,
			out uint loopend, TimeUnit loopendtype);
	}
}