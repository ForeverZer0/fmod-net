using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using FMOD.Sharp.Enums;

namespace FMOD.Sharp.Data
{
	public class SoundInfo
	{
		public SoundType Type { get; set; }

		public SoundFormat Format { get; set; }

		public int ChannelCount { get; set; }

		public int BitsPerSample  { get; set; }
	}
}
