using System;
using FMOD.Sharp.Data;
using FMOD.Sharp.Dsps;

namespace FMOD.Sharp
{
	/// <inheritdoc />
	/// <summary>
	/// Provides data for <see cref="E:FMOD.Sharp.Channel.SoundEnded"/> events.
	/// </summary>
	/// <seealso cref="T:System.EventArgs" />
	/// <seealso cref="T:FMOD.Sharp.Channel" />
	/// <seealso cref="E:FMOD.Sharp.Channel.SoundEnded"/>
	public class ChannelSoundEndEventArgs : EventArgs
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
		/// Initializes a new instance of the <see cref="T:FMOD.Sharp.ChannelSoundEndEventArgs" /> class.
		/// </summary>
		/// <param name="sound">The <see cref="P:FMOD.Sharp.ChannelSoundEndEventArgs.Sound" /> instance that has ended.</param>
		public ChannelSoundEndEventArgs(Sound sound)
		{
			Sound = sound;
		}
	}

	/// <inheritdoc />
	/// <summary>
	/// Provides data for <see cref="E:FMOD.Sharp.Channel.SyncPointEncountered" /> events.
	/// </summary>
	/// <seealso cref="T:System.EventArgs" />
	/// <seealso cref="T:FMOD.Sharp.Channel" />
	/// <seealso cref="T:FMOD.Sharp.Data.SyncPointInfo" />
	/// <seealso cref="E:FMOD.Sharp.Channel.SyncPointEncountered" />
	public class ChannelSyncPointEncounteredEventArgs : EventArgs
	{
		/// <summary>
		/// Gets the index of the sync point.
		/// </summary>
		/// <value>
		/// The index.
		/// </value>
		public int Index { get; }

		/// <summary>
		/// Gets the synchronize point.
		/// </summary>
		/// <value>
		/// The synchronize point.
		/// </value>
		public IntPtr SyncPoint { get; }

		/// <summary>
		/// Gets the synchronize point information.
		/// </summary>
		/// <value>
		/// The synchronize point information.
		/// </value>
		public SyncPointInfo SyncPointInfo { get; }

		/// <inheritdoc />
		/// <summary>
		/// Initializes a new instance of the <see cref="T:FMOD.Sharp.ChannelSyncPointEncounteredEventArgs" /> class.
		/// </summary>
		/// <param name="index">The index.</param>
		/// <param name="syncPoint">The sync point.</param>
		/// <param name="syncPointInfo">The sync point info.</param>
		public ChannelSyncPointEncounteredEventArgs(int index, IntPtr syncPoint, SyncPointInfo syncPointInfo)
		{
			Index = index;
			SyncPoint = syncPoint;
			SyncPointInfo = syncPointInfo;
		}
	}

	/// <inheritdoc />
	/// <summary>
	/// Provides data for <see cref="E:FMOD.Sharp.Channel.OcclusionCalculated" /> events.
	/// </summary>
	/// <seealso cref="T:System.EventArgs" />
	/// <seealso cref="T:FMOD.Sharp.Channel" />
	/// <seealso cref="E:FMOD.Sharp.Channel.OcclusionCalculated"/>
	public class ChannelOcclusionCalculatedEventArgs : EventArgs
	{
		/// <summary>
		/// Gets the pointer to a <see cref="float"/> direct value that can be read (dereferenced) and modified after the geometry engine has calculated it for this channel.
		/// </summary>
		/// <value>
		/// The pointer to the direct occlusion value.
		/// </value>
		public IntPtr DirectOcclusion { get; }

		/// <summary>
		/// Gets the pointer to a <see cref="float"/> reverb value that can be read (dereferenced) and modified after the geometry engine has calculated it for this channel.
		/// </summary>
		/// <value>
		/// The pointer to the reverb occlusion value.
		/// </value>
		public IntPtr ReverbOcclusion { get; }

		/// <inheritdoc />
		/// <summary>
		/// Initializes a new instance of the <see cref="T:FMOD.Sharp.ChannelOcclusionCalculatedEventArgs" /> class.
		/// </summary>
		/// <param name="direct">The pointer to the direct occlusion value.</param>
		/// <param name="reverb">The pointer to the reverb occlusion value.</param>
		public ChannelOcclusionCalculatedEventArgs(IntPtr direct, IntPtr reverb)
		{
			DirectOcclusion = direct;
			ReverbOcclusion = reverb;
		}
	}

	/// <inheritdoc />
	/// <summary>
	/// Provides data for <see cref="E:FMOD.Sharp.Channel.VirtualVoiceSwapped" /> events.
	/// </summary>
	/// <seealso cref="T:System.EventArgs" />
	/// <seealso cref="T:FMOD.Sharp.Channel" />
	/// <seealso cref="E:FMOD.Sharp.Channel.VirtualVoiceSwapped"/>
	public class ChannelVoiceSwappedEventArgs : EventArgs
	{
		/// <summary>
		/// <para>Gets a value indicating whether this instance is emulated. </para>
		/// <para><c>true</c> when voice is swapped from real to emulated, otherwise <c>false</c> and swapped from emulated to real.</para>
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is emulated; otherwise, <c>false</c>.
		/// </value>
		/// <remarks><c>true</c> when voice is swapped from real to emulated, otherwise <c>false</c> and swapped from emulated to real.</remarks>
		public bool IsEmulated { get; }

		/// <inheritdoc />
		/// <summary>
		/// Initializes a new instance of the <see cref="T:FMOD.Sharp.ChannelVoiceSwappedEventArgs" /> class.
		/// </summary>
		/// <param name="isEmulated">If set to <c>true</c> if emulated, otherwise <c>false</c> and real.</param>
		public ChannelVoiceSwappedEventArgs(bool isEmulated)
		{
			IsEmulated = isEmulated;
		}
	}

	public class ChannelGroupAddEventArgs : EventArgs
	{
		public ChannelGroup ChannelGroup { get; }

		public DspConnection Connection { get; }

		public ChannelGroupAddEventArgs(ChannelGroup group, DspConnection connection)
		{
			ChannelGroup = group;
			Connection = connection;
		}
	}




	public class DspParamChangedEventArgs : EventArgs
	{
		public int ParameterIndex { get; }

		public DspParamChangedEventArgs(int parameterIndex)
		{
			ParameterIndex = parameterIndex;
		}
	}

	public class DspFloatParamChangedEventArgs : DspParamChangedEventArgs
	{
		public float Value { get; }

		public float MinValue { get; }

		public float MaxValue { get; }

		public DspFloatParamChangedEventArgs(int index, float value, float min = float.MinValue, float max = float.MaxValue) : base(index)
		{
			Value = value;
			MinValue = min;
			MaxValue = max;
		}
	}

	public class DspBoolParamChangedEventArgs : DspParamChangedEventArgs
	{
		public bool Value { get; }

		public DspBoolParamChangedEventArgs(int index, bool value) : base(index)
		{
			Value = value;
		}
	}

	public class DspChannelMixGainChangedEventArgs : DspFloatParamChangedEventArgs
	{
		public int ChannelIndex { get; }

		public DspChannelMixGainChangedEventArgs(int parameterIndex, int channelIndex, float gain) : base(parameterIndex, gain, -80.0f, 10.0f)
		{
			ChannelIndex = channelIndex;
		}
	}

	public class DspPitchFftChangedEventArgs : DspParamChangedEventArgs
	{
		public PitchShift.FftWindowSize FftWindow { get; }

		public DspPitchFftChangedEventArgs(int parameterIndex, PitchShift.FftWindowSize windowSize) : base(parameterIndex)
		{
			FftWindow = windowSize;
		}
	}

	public class DspMultiBandEqFilterChangedEventArgs : DspParamChangedEventArgs
	{
		public MultiBandEq.Band Band { get; }

		public MultiBandEq.Filter Filter { get; }

		public DspMultiBandEqFilterChangedEventArgs(int parameterIndex, MultiBandEq.Band band, MultiBandEq.Filter filter) :
			base(parameterIndex)
		{
			Band = band;
			Filter = filter;
		}
	}

	public class DspMultiBandEqFloatChangedEventArgs : DspFloatParamChangedEventArgs
	{
		public MultiBandEq.Band Band { get; }

		public DspMultiBandEqFloatChangedEventArgs(int parameterIndex, MultiBandEq.Band band, float value, float max, float min) :
			base(parameterIndex, value, min, max)
		{
			Band = band;
		}
	}

	public class DspOscillatorTypeChangedEventArgs : DspParamChangedEventArgs
	{
		public Oscillator.WaveformType OscillatorType { get; }

		public DspOscillatorTypeChangedEventArgs(int parameterIndex, Oscillator.WaveformType type) :
			base(parameterIndex)
		{
			OscillatorType = type;
		}
	}

	public class DspFftWindowChangedEventArgs : DspParamChangedEventArgs
	{
		public Fft.WindowSize WindowSize { get; }

		public Fft.WindowType WindowType { get; }

		public DspFftWindowChangedEventArgs(int parameterIndex, Fft.WindowSize size, Fft.WindowType type) :
			base(parameterIndex)
		{
			WindowSize = size;
			WindowType = type;
		}
	}
}

