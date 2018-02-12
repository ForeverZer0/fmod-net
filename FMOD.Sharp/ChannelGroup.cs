using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FMOD.Sharp
{
	public partial class ChannelGroup : Handle
	{
		public ChannelGroup(IntPtr handle) : base(handle)
		{
			
		}

		public override void Dispose()
		{
			base.Dispose();
			throw new NotImplementedException();
		}
	}
}
