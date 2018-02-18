using System;

namespace FMOD.Arguments
{
	public class SoundMusicVolumeChangedEventArgs : EventArgs
	{
		public int Channel { get; }

		public float Volume { get; }

		public SoundMusicVolumeChangedEventArgs(int channel, float volume)
		{
			Channel = channel;
			Volume = volume;
		}
	}
}