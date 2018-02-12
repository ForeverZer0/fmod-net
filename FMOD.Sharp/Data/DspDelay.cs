using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FMOD.Sharp.Data
{
	public class DspDelay
	{
		public uint ClockStart { get; set; }

		public uint ClockEnd { get; set; }

		public bool StopChannels { get; set; }
	}
}
