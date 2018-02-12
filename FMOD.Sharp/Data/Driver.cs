using System;
using FMOD.Sharp.Enums;

namespace FMOD.Sharp.Data
{
	public class Driver
	{
		public int Id { get; set; }

		public string Name  { get; set; }

		public Guid Guid { get; set; }

		public int SystemRate { get; set; }

		public SpeakerMode SpeakerMode { get; set; }

		public int SpeakerModeChannels { get; set; }

		public DriverState State { get; set; }
	}
}
