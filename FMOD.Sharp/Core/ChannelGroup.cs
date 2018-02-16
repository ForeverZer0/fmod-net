using System;
using System.Text;

namespace FMOD.Core
{
	public partial class ChannelGroup : ChannelControl
	{
		internal ChannelGroup(IntPtr handle) : base(handle)
		{
		}

		public ChannelGroup ParentGroup
		{
			get
			{
				NativeInvoke(FMOD_ChannelGroup_GetParentGroup(this, out var group));
				return CoreHelper.Create<ChannelGroup>(group);
			}
		}

		public string Name
		{
			get
			{
				using (var buffer = new MemoryBuffer(512))
				{
					NativeInvoke(FMOD_ChannelGroup_GetName(this, buffer.Pointer, 512));
					return buffer.ToString(Encoding.UTF8);
				}
			}
		}

		public int ChannelCount
		{
			get
			{
				NativeInvoke(FMOD_ChannelGroup_GetNumChannels(this, out var count));
				return count;
			}
		}

		public int GroupCount
		{
			get
			{
				NativeInvoke(FMOD_ChannelGroup_GetNumGroups(this, out var count));
				return count;
			}
		}

		public event EventHandler<ChannelGroupAddEventArgs> ChannelGroupAdded;


		public DspConnection AddGroup(ChannelGroup group, bool propagateDspClock = true)
		{
			NativeInvoke(FMOD_ChannelGroup_AddGroup(this, group, propagateDspClock, out var connection));
			var dspConn = CoreHelper.Create<DspConnection>(connection);
			ChannelGroupAdded?.Invoke(this, new ChannelGroupAddEventArgs(group, dspConn));
			return dspConn;
		}

		public ChannelGroup GetGroup(int index)
		{
			NativeInvoke(FMOD_ChannelGroup_GetGroup(this, index, out var group));
			return CoreHelper.Create<ChannelGroup>(group);
		}

		public Channel GetChannel(int index)
		{
			NativeInvoke(FMOD_ChannelGroup_GetChannel(this, index, out var channel));
			return CoreHelper.Create<Channel>(channel);
		}
	}
}