using FMOD.Enumerations;

namespace FMOD.Data
{
	public class SoundInfo
	{
		public SoundType Type { get; set; }

		public SoundFormat Format { get; set; }

		public int ChannelCount { get; set; }

		public int BitsPerSample  { get; set; }
	}
}
