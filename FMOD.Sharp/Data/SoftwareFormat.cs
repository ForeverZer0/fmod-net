using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FMOD.Sharp.Enums;

namespace FMOD.Sharp.Data
{
	public class SoftwareFormat
	{
		private int _sampleRate;

		public int SampleRate
		{
			get { return _sampleRate; }
			set { _sampleRate = value.Clamp(8000, 192000); }
		}

		public int RawSpeakerCount { get; set; } = 0;

		public SpeakerMode SpeakerMode { get; set; }
	}
}




