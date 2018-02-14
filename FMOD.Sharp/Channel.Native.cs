using System;
using System.Runtime.InteropServices;
using System.Security;
using FMOD.Sharp.Enums;

namespace FMOD.Sharp
{
	[SuppressUnmanagedCodeSecurity]
	public partial class Channel
	{
		#region Native Methods

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_Channel_GetChannelGroup(IntPtr channel, out IntPtr channelGroup);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_Channel_GetCurrentSound(IntPtr channel, out IntPtr sound);

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
		private static extern Result FMOD_Channel_GetPosition(IntPtr channel, out uint position, TimeUnit timeUnit);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_Channel_GetPriority(IntPtr channel, out int priority);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_Channel_IsVirtual(IntPtr channel, out bool isVirtual);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_Channel_SetChannelGroup(IntPtr channel, IntPtr channelGroup);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_Channel_SetFrequency(IntPtr channel, float frequency);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_Channel_SetLoopCount(IntPtr channel, int loopCount);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_Channel_SetLoopPoints(IntPtr channel, uint loopStart, TimeUnit loopStartUnit,
			uint loopEnd, TimeUnit loopEndUnit);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_Channel_SetPosition(IntPtr channel, uint position, TimeUnit timeUnit);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_Channel_SetPriority(IntPtr channel, int priority);

		#endregion
	}
}