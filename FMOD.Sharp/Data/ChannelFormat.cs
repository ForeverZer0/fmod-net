using FMOD.Enumerations;

namespace FMOD.Data
{
	public class ChannelFormat
	{
		public ChannelMask ChannelMask { get; set; }
		public int ChannelCount { get; set; }
		public SpeakerMode SpeakerMode { get; set; }
	}
}
