using System;
using FMOD.Core;

namespace FMOD.Arguments
{
	/// <inheritdoc />
	/// <summary>
	/// Provides data for <see cref="E:FMOD.Core.Channel.SoundEnded"/> events.
	/// </summary>
	/// <seealso cref="T:System.EventArgs" />
	/// <seealso cref="T:FMOD.Core.Channel" />
	/// <seealso cref="E:FMOD.Core.Channel.SoundEnded"/>
	public class SoundEndedEventArgs : EventArgs
	{
		/// <summary>
		/// Gets the sound that has ended.
		/// </summary>
		/// <value>
		/// The sound.
		/// </value>
		public Sound Sound { get; }

		/// <inheritdoc />
		/// <summary>
		/// Initializes a new instance of the <see cref="T:FMOD.Arguments.ChannelSoundEndEventArgs" /> class.
		/// </summary>
		/// <param name="sound">The <see cref="P:FMOD.Arguments.ChannelSoundEndEventArgs.Sound" /> instance that has ended.</param>
		public SoundEndedEventArgs(Sound sound)
		{
			Sound = sound;
		}
	}
}