using System;
using FMOD.Core;

namespace FMOD.Arguments
{
	public class AddChannelGroupEventArgs : EventArgs
	{
		public ChannelGroup ChannelGroup { get; }

		public DspConnection Connection { get; }

		public AddChannelGroupEventArgs(ChannelGroup group, DspConnection connection)
		{
			ChannelGroup = group;
			Connection = connection;
		}
	}
}