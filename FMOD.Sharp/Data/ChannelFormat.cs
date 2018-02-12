using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FMOD.Sharp.Enums;

namespace FMOD.Sharp.Data
{
	public class ChannelFormat
	{
		public ChannelMask ChannelMask { get; set; }
		public int ChannelCount { get; set; }
		public SpeakerMode SpeakerMode { get; set; }
	}
}
