using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using FMOD.Enumerations;

namespace FMOD.Core
{
	[SuppressUnmanagedCodeSecurity][SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
	public partial class Channel
	{
		#region Native Methods

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_Channel_GetChannelGroup(IntPtr channel, out IntPtr channelGroup);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_Channel_GetCurrentSound(IntPtr channel, out IntPtr sound);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_Channel_GetFrequency(IntPtr channel, out float frequency);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_Channel_GetIndex(IntPtr channel, out int index);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_Channel_GetLoopCount(IntPtr channel, out int loopCount);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_Channel_GetLoopPoints(IntPtr channel, out uint loopStart, TimeUnit loopStartUnit,
			out uint loopEnd, TimeUnit loopEndUnit);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_Channel_GetPosition(IntPtr channel, out uint position, TimeUnit timeUnit);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_Channel_GetPriority(IntPtr channel, out int priority);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_Channel_IsVirtual(IntPtr channel, out bool isVirtual);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_Channel_SetChannelGroup(IntPtr channel, IntPtr channelGroup);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_Channel_SetFrequency(IntPtr channel, float frequency);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_Channel_SetLoopCount(IntPtr channel, int loopCount);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_Channel_SetLoopPoints(IntPtr channel, uint loopStart, TimeUnit loopStartUnit,
			uint loopEnd, TimeUnit loopEndUnit);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_Channel_SetPosition(IntPtr channel, uint position, TimeUnit timeUnit);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_Channel_SetPriority(IntPtr channel, int priority);

		#endregion
	}
}