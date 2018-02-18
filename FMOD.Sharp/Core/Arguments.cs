using System;
using FMOD.Data;
using FMOD.DSP;

namespace FMOD.Core
{

	public class PolygonEventArgs : EventArgs
	{
		/// <summary>
		/// Gets the index of the polygon within the <see cref="Geometry"/> object.
		/// </summary>
		/// <value>
		/// The index.
		/// </value>
		public int Index { get; }

		public PolygonEventArgs(int index)
		{
			Index = index;
		}
	}


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

	public class SoundSyncPointEventArgs : EventArgs
	{
		public IntPtr SyncPoint { get; }

		public SoundSyncPointEventArgs(IntPtr syncPoint)
		{
			SyncPoint = syncPoint;
		}
	}


	/// <inheritdoc />
	/// <summary>
	/// Provides data for <see cref="E:FMOD.Core.Channel.SoundEnded"/> events.
	/// </summary>
	/// <seealso cref="T:System.EventArgs" />
	/// <seealso cref="T:FMOD.Core.Channel" />
	/// <seealso cref="E:FMOD.Core.Channel.SoundEnded"/>
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
		/// Initializes a new instance of the <see cref="T:FMOD.Core.ChannelSoundEndEventArgs" /> class.
		/// </summary>
		/// <param name="sound">The <see cref="P:FMOD.Core.ChannelSoundEndEventArgs.Sound" /> instance that has ended.</param>
		public ChannelSoundEndEventArgs(Sound sound)
		{
			Sound = sound;
		}
	}

	/// <inheritdoc />
	/// <summary>
	/// Provides data for <see cref="E:FMOD.Core.Channel.SyncPointEncountered" /> events.
	/// </summary>
	/// <seealso cref="T:System.EventArgs" />
	/// <seealso cref="T:FMOD.Core.Channel" />
	/// <seealso cref="T:FMOD.Data.SyncPointInfo" />
	/// <seealso cref="E:FMOD.Core.Channel.SyncPointEncountered" />
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
		/// Initializes a new instance of the <see cref="T:FMOD.Core.ChannelSyncPointEncounteredEventArgs" /> class.
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
	/// Provides data for <see cref="E:FMOD.Core.Channel.OcclusionCalculated" /> events.
	/// </summary>
	/// <seealso cref="T:System.EventArgs" />
	/// <seealso cref="T:FMOD.Core.Channel" />
	/// <seealso cref="E:FMOD.Core.Channel.OcclusionCalculated"/>
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
		/// Initializes a new instance of the <see cref="T:FMOD.Core.ChannelOcclusionCalculatedEventArgs" /> class.
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
	/// Provides data for <see cref="E:FMOD.Core.Channel.VirtualVoiceSwapped" /> events.
	/// </summary>
	/// <seealso cref="T:System.EventArgs" />
	/// <seealso cref="T:FMOD.Core.Channel" />
	/// <seealso cref="E:FMOD.Core.Channel.VirtualVoiceSwapped"/>
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
		/// Initializes a new instance of the <see cref="T:FMOD.Core.ChannelVoiceSwappedEventArgs" /> class.
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

	public class DspIntParamChangedEventArgs : DspParamChangedEventArgs
	{
		public int Value { get; }

		public int MinValue { get; }

		public int MaxValue { get; }

		public DspIntParamChangedEventArgs(int index, int value, int min = int.MinValue, int max = int.MaxValue) : base(index)
		{
			Value = value;
			MinValue = min;
			MaxValue = max;
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

	public class DspDelayChangedEventArgs : DspFloatParamChangedEventArgs
	{
		public int Channel { get; }

		public DspDelayChangedEventArgs(int parameterIndex, float value) :
			base(parameterIndex, value, 0.0f, 10000.0f)
		{
			Channel = parameterIndex;
		}
	}
}

