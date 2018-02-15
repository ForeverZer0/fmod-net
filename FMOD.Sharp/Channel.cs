using System;
using FMOD.Sharp.Data;
using FMOD.Sharp.Enums;

namespace FMOD.Sharp
{
	public partial class Channel : ChannelControl
	{
		internal Channel(IntPtr handle) : base(handle)
		{
		}

		/// <summary>
		///     Gets the currently playing sound for this channel.
		/// </summary>
		/// <value>
		///     The current sound.
		/// </value>
		/// <remarks>If a sound is not playing the returned pointer will be <c>null</c>.</remarks>
		public Sound CurrentSound
		{
			get
			{
				NativeInvoke(FMOD_Channel_GetCurrentSound(this, out var sound));
				return sound == IntPtr.Zero ? null : Core.Create<Sound>(sound);
			}
		}

		/// <summary>
		///     Gets or sets the currently assigned channel group for the channel.
		/// </summary>
		/// <value>
		///     The channel group.
		/// </value>
		/// <remarks>
		///     Setting a channel to a channel group removes it from any previous group, it does not allow sharing of channel
		///     groups.
		/// </remarks>
		public ChannelGroup ChannelGroup
		{
			get
			{
				NativeInvoke(FMOD_Channel_GetChannelGroup(this, out var group));
				return Core.Create<ChannelGroup>(group);
			}
			set
			{
				NativeInvoke(FMOD_Channel_SetChannelGroup(this, value));
				ChannelGroupChanged?.Invoke(this, EventArgs.Empty);
			}
		}

		/// <summary>
		///     Gets whether the channel is virtual (emulated) or not due to the virtual channel management system.
		/// </summary>
		/// <value>
		///     <c>true</c> if this instance is virtual; otherwise, <c>false</c>.
		/// </value>
		public bool IsVirtual
		{
			get
			{
				NativeInvoke(FMOD_Channel_IsVirtual(this, out var isVirtual));
				return isVirtual;
			}
		}

		// TODO: Fix comments
		/// <summary>
		///     <para>Gets or sets the current loop count for the specified channel.</para>
		///     <para>0 = oneshot, 1 = loop once then stop, -1 = loop forever, default = -1. </para>
		/// </summary>
		/// <value>
		///     The loop count.
		/// </value>
		/// <remarks>
		///     <para>
		///         <i>Issues with streamed audio:</i>
		///     </para>
		///     <para>
		///         When changing the loop count, sounds created with <seealso cref="FmodSystem.CreateStream" /> or
		///         <seealso cref="Mode.CreateStream" /> may have already been pre-buffered and executed their loop logic ahead of
		///         time before this call was even made. This is dependant on the size of the sound versus the size of the stream
		///         decode buffer (see FMOD_CREATESOUNDEXINFO). If this happens, you may need to reflush the stream buffer by
		///         calling Channel::setPosition. Note this will usually only happen if you have sounds or loop points that are
		///         smaller than the stream decode buffer size.
		///     </para>
		/// </remarks>
		public int LoopCount
		{
			get
			{
				NativeInvoke(FMOD_Channel_GetLoopCount(this, out var count));
				return count;
			}
			set
			{
				NativeInvoke(FMOD_Channel_SetLoopCount(this, value.Clamp(-1, value)));
				LoopCountChanged?.Invoke(this, EventArgs.Empty);
			}
		}

		/// <summary>
		///     Gets the internal channel index for a channel.
		/// </summary>
		/// <value>
		///     The index.
		/// </value>
		/// <seealso cref="FmodSystem.PlaySound" />
		public int Index
		{
			get
			{
				NativeInvoke(FMOD_Channel_GetIndex(this, out var index));
				return index;
			}
		}

		/// <summary>
		///     Gets or sets the channel frequency or playback rate, in Hz.
		/// </summary>
		/// <value>
		///     The frequency.
		/// </value>
		/// <remarks>
		///     <para>
		///         When a sound is played, it plays at the default frequency of the sound which can be set by
		///         <see cref="Sound.DefaultFrequency" />.
		///     </para>
		///     <para>For most file formats, the default frequency is determined by the audio format.</para>
		/// </remarks>
		public float Frequency
		{
			get
			{
				NativeInvoke(FMOD_Channel_GetFrequency(this, out var frequency));
				return frequency;
			}
			set
			{
				NativeInvoke(FMOD_Channel_SetFrequency(this, value));
				FrequencyChanged?.Invoke(this, EventArgs.Empty);
			}
		}

		public int Priority
		{
			get
			{
				NativeInvoke(FMOD_Channel_GetPriority(this, out var priority));
				return priority;
			}
			set
			{
				NativeInvoke(FMOD_Channel_SetPriority(this, value.Clamp(0, 256)));
				PriorityChanged?.Invoke(this, EventArgs.Empty);
			}
		}

		public event EventHandler PositionChanged;

		public event EventHandler PriorityChanged;

		public event EventHandler FrequencyChanged;

		public event EventHandler LoopCountChanged;

		public event EventHandler LoopPointAdded;

		public event EventHandler ChannelGroupChanged;

		public event EventHandler<ChannelOcclusionCalculatedEventArgs> OcclusionCalculated;

		public event EventHandler<ChannelSoundEndEventArgs> SoundEnded;

		public event EventHandler<ChannelSyncPointEncounteredEventArgs> SyncPointEncountered;

		public event EventHandler<ChannelVoiceSwappedEventArgs> VirtualVoiceSwapped;

		/// <summary>
		/// Gets the loop points for the channel.
		/// </summary>
		/// <param name="timeUnit">Time format used for the loop start and end point.</param>
		/// <returns></returns>
		public LoopPoints GetLoopPoints(TimeUnit timeUnit = TimeUnit.Ms)
		{
			return GetLoopPoints(timeUnit, timeUnit);
		}

		public LoopPoints GetLoopPoints(TimeUnit startUnit, TimeUnit endUnit)
		{
			NativeInvoke(FMOD_Channel_GetLoopPoints(this, out var start, startUnit,
				out var end, endUnit));
			return new LoopPoints
			{
				LoopStart = start,
				StartTimeUnit = startUnit,
				LoopEnd = end,
				EndTimeUnit = startUnit
			};
		}

		public void SetLoopPoints(LoopPoints points)
		{
			SetLoopPoints(points.LoopStart, points.LoopEnd, points.StartTimeUnit, points.EndTimeUnit);
		}

		public void SetLoopPoints(uint loopStart, uint loopEnd, TimeUnit timeUnit = TimeUnit.Ms)
		{
			SetLoopPoints(loopStart, loopEnd, timeUnit, timeUnit);
		}

		public uint GetPosition(TimeUnit timeUnits = TimeUnit.Ms)
		{
			NativeInvoke(FMOD_Channel_GetPosition(this, out var position, timeUnits));
			return position;
		}

		public void SetLoopPoints(uint loopStart, uint loopEnd, TimeUnit startUnit, TimeUnit endUnit)
		{
			NativeInvoke(FMOD_Channel_SetLoopPoints(this, loopStart, startUnit, loopEnd, endUnit));
			LoopPointAdded?.Invoke(this, EventArgs.Empty);
		}

		public void SetPosition(int position, TimeUnit timeUnits = TimeUnit.Ms)
		{
			NativeInvoke(FMOD_Channel_SetPosition(this, Convert.ToUInt32(position), timeUnits));
			PositionChanged?.Invoke(this, EventArgs.Empty);
		}

		public void SetPosition(uint position, TimeUnit timeUnits = TimeUnit.Ms)
		{
			NativeInvoke(FMOD_Channel_SetPosition(this, position, timeUnits));
			PositionChanged?.Invoke(this, EventArgs.Empty);
		}

		protected override Result OnChannelCallback(IntPtr channelControl, ChannelControlType controlType,
			ChannelControlCallbackType type,
			IntPtr commandData1, IntPtr commandData2)
		{
			if (controlType != ChannelControlType.Channel)
				return base.OnChannelCallback(channelControl, controlType, type, commandData1, commandData2);
			switch (type)
			{
				case ChannelControlCallbackType.End:
					SetHandleAsInvalid();
					SoundEnded?.Invoke(this, new ChannelSoundEndEventArgs(CurrentSound));
					break;
				case ChannelControlCallbackType.Virtualvoice:
					VirtualVoiceSwapped?.Invoke(this, new ChannelVoiceSwappedEventArgs(commandData1.ToInt32() == 1));
					break;
				case ChannelControlCallbackType.SyncPoint:
					if (SyncPointEncountered == null)
						break;
					var sound = CurrentSound;
					var index = commandData1.ToInt32();
					var syncpoint = sound.GetSyncPoint(index);
					var info = sound.GetSyncpointInfo(syncpoint);
					SyncPointEncountered.Invoke(this, new ChannelSyncPointEncounteredEventArgs(index, syncpoint, info));
					break;
				case ChannelControlCallbackType.Occlusion:
					OcclusionCalculated?.Invoke(this, new ChannelOcclusionCalculatedEventArgs(commandData1, commandData2));
					break;
			}
			return Result.OK;
		}
	}
}