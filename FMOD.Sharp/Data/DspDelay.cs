using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FMOD.Sharp.Data
{
	public class DspDelay
	{
		public ulong ClockStart { get; set; }

		public ulong ClockEnd { get; set; }

		public bool StopChannels { get; set; }
	}
}
