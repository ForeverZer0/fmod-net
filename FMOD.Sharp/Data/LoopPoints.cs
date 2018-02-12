using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FMOD.Sharp.Enums;

namespace FMOD.Sharp.Data
{
	public class LoopPoints
	{
		public uint LoopStart { get; set; }

		public uint LoopEnd { get; set; }

		public TimeUnit StartTimeUnit { get; set; }

		public TimeUnit EndTimeUnit { get; set; }
	}
}
