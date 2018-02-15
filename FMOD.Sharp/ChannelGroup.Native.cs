using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using FMOD.Sharp.Enums;

namespace FMOD.Sharp
{
	[SuppressUnmanagedCodeSecurity][SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
	public partial class ChannelGroup
	{
		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_ChannelGroup_AddGroup(IntPtr channelGroup, IntPtr group, bool propagateDspClock,
			out IntPtr connection);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_ChannelGroup_GetGroup(IntPtr channelGroup, int index, out IntPtr group);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_ChannelGroup_GetParentGroup(IntPtr channelGroup, out IntPtr group);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_ChannelGroup_GetName(IntPtr channelGroup, IntPtr name, int nameLength);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_ChannelGroup_GetNumChannels(IntPtr channelGroup, out int numChannels);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_ChannelGroup_GetChannel(IntPtr channelGroup, int index, out IntPtr channel);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_ChannelGroup_GetNumGroups(IntPtr channelGroup, out int numGroups);
	}
}