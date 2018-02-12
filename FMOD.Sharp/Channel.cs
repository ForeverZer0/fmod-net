using System;
using System.Runtime.InteropServices;
using FMOD.Sharp.Data;
using FMOD.Sharp.Enums;
using FMOD.Sharp.Structs;

namespace FMOD.Sharp
{
	public partial class Channel : Handle
	{
		#region Constants & Fields

		// ReSharper disable once PrivateFieldCanBeConvertedToLocalVariable
		private readonly ChannelCallback _channelCallbackDelegate;

		#endregion

		#region Delegates & Events

		public event EventHandler ChannelGroupChanged;

		public event EventHandler DelayChanged;

		public event EventHandler DspAdded;

		public event EventHandler DspRemoved;

		public event EventHandler FadePointAdded;

		public event EventHandler FadePointRemoved;

		public event EventHandler FrequencyChanged;

		public event EventHandler LoopCountChanged;

		public event EventHandler LoopPointAdded;

		public event EventHandler LowPassGainChanged;

		public event EventHandler MixMatrixChanged;

		public event EventHandler ModeChanged;

		public event EventHandler MuteChanged;

		public event EventHandler OcclusionCalculated;

		public event EventHandler PanChanged;

		public event EventHandler PitchChanged;

		public event EventHandler PlaybackPaused;

		public event EventHandler PlaybackResumed;

		public event EventHandler PlaybackStopped;

		public event EventHandler PositionChanged;

		public event EventHandler PriorityChanged;

		public event EventHandler ReverbChanged;

		public event EventHandler SoundEnded;

		public event EventHandler SpreadChanged;

		public event EventHandler SyncpointEncountered;

		public event EventHandler ThreeDConeOrientationChanged;

		public event EventHandler ThreeDConeSettingsChanged;

		public event EventHandler ThreeDCustomRolloffChanged;

		public event EventHandler ThreeDDistanceFilterChanged;

		public event EventHandler ThreeDDopplerLevelChanged;

		public event EventHandler ThreeDLevelChanged;

		public event EventHandler ThreeDMaxDistanceChanged;

		public event EventHandler ThreeDMinDistanceChanged;

		public event EventHandler ThreeDOcclusionChanged;

		public event EventHandler ThreeDPositionChanged;

		public event EventHandler ThreeDVelocityChanged;

		public event EventHandler UserDataChanged;

		public event EventHandler VirtualVoiceSwapped;

		public event EventHandler VolumeChanged;

		public event EventHandler VolumeRampChanged;

		#endregion

		#region Constructors & Destructor

		public Channel(IntPtr handle) : base(handle)
		{
			_channelCallbackDelegate = ChannelCallbackHandler;
			NativeInvoke(FMOD_Channel_SetCallback(this, _channelCallbackDelegate));
		}

		#endregion

		#region Properties & Indexers

		/// <summary>
		/// Gets the combined volume after 3D spatialization and geometry occlusion calculations including any volumes set via the API.
		/// </summary>
		/// <value>
		/// The audibility.
		/// </value>
		/// <remarks>This does not represent the waveform, just the calculated result of all volume modifiers. This value is used by the virtual channel system to order its channels between real and virtual.</remarks>
		public float Audibility
		{
			get
			{
				NativeInvoke(FMOD_Channel_GetAudibility(this, out var audibility));
				return audibility;
			}
		}

		/// <summary>
		/// Gets or sets the currently assigned channel group for the channel.
		/// </summary>
		/// <value>
		/// The channel group.
		/// </value>
		/// <remarks>Setting a channel to a channel group removes it from any previous group, it does not allow sharing of channel groups.</remarks>
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
		/// Gets the currently playing sound for this channel.
		/// </summary>
		/// <value>
		/// The current sound.
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
		/// Gets or sets a start (and/or stop) time relative to the parent channel group DSP clock, with sample accuracy.
		/// </summary>
		/// <value>
		/// The delay.
		/// </value>
		/// <remarks>
		/// <para>Every channel and channel group has its own DSP Clock. A channel or channel group can be delayed relatively against its parent, with sample accurate positioning. To delay a sound, use the 'parent' channel group DSP clock to reference against when passing values into this function.</para>
		/// <para>If a parent channel group changes its pitch, the start and stop times will still be correct as the parent clock is rate adjusted by that pitch.</para>
		/// </remarks>
		public DspDelay Delay
		{
			get
			{
				NativeInvoke(FMOD_Channel_GetDelay(this, out var start, out var end, out var stop));
				return new DspDelay
				{
					ClockStart = start,
					ClockEnd = end,
					StopChannels = stop
				};
			}
			set
			{
				var start = value.ClockStart.Clamp<uint>(0, value.ClockStart);
				var end = value.ClockStart.Clamp<uint>(0, value.ClockEnd);
				NativeInvoke(FMOD_Channel_SetDelay(this, start, end, value.StopChannels));
				DelayChanged?.Invoke(this, EventArgs.Empty);
			}
		}

		/// <summary>
		/// Gets the number of DSP units in the DSP chain.
		/// </summary>
		/// <value>
		/// The DSP count.
		/// </value>
		public int DspCount
		{
			get
			{
				NativeInvoke(FMOD_Channel_GetNumDSPs(this, out var count));
				return count;
			}
		}

		/// <summary>
		/// Gets the DSP clock value for the head DSP node. 
		/// </summary>
		/// <value>
		/// The DSP head clock.
		/// </value>
		/// <remarks>The DSP clocks count up by the number of samples per second in the software mixer, i.e. if the default sample rate is 48KHz, the DSP clock increments by 48000 per second.</remarks>
		public ulong DspHeadClock
		{
			get
			{
				NativeInvoke(FMOD_Channel_GetDSPClock(this, out var head, out var dummy));
				return head;
			}
		}

		/// <summary>
		/// Gets the DSP clock value for the tail DSP node. 
		/// </summary>
		/// <value>
		/// The DSP tail clock.
		/// </value>
		/// <remarks>The DSP clocks count up by the number of samples per second in the software mixer, i.e. if the default sample rate is 48KHz, the DSP clock increments by 48000 per second.</remarks>
		public ulong DspTailClock
		{
			get
			{
				NativeInvoke(FMOD_Channel_GetDSPClock(this, out var dummy, out var tail));
				return tail;
			}
		}

		/// <summary>
		/// Gets the number of fade points stored within the channel.
		/// </summary>
		/// <value>
		/// The fade point count.
		/// </value>
		public int FadePointCount
		{
			get
			{
				NativeInvoke(FMOD_Channel_GetFadePoints(this, out var numPoints, null, null));
				return (int) numPoints;
			}
		}

		/// <summary>
		/// Gets or sets the channel frequency or playback rate, in Hz.
		/// </summary>
		/// <value>
		/// The frequency.
		/// </value>
		/// <remarks>
		/// <para>When a sound is played, it plays at the default frequency of the sound which can be set by <see cref="Sound.DefaultFrequency"/>.</para>
		/// <para>For most file formats, the default frequency is determined by the audio format.</para>
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

		/// <summary>
		/// Gets the internal channel index for a channel.
		/// </summary>
		/// <value>
		/// The index.
		/// </value>
		/// <seealso cref="FmodSystem.PlaySound"/>
		public int Index
		{
			get
			{
				NativeInvoke(FMOD_Channel_GetIndex(this, out var index));
				return index;
			}
		}

		/// <summary>
		/// Gets a value indicating whether this instance is disposed.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is disposed; otherwise, <c>false</c>.
		/// </value>
		public override bool IsDisposed
		{
			get
			{
				if (base.IsDisposed)
					return true;
				var result = FMOD_Channel_IsPlaying(this, out var dummy);
				if (result != Result.InvalidHandle)
					return false;
				SetDisposed();
				return true;
			}
		}

		/// <summary>
		/// Gets a value indicating whether this instance is playing.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is playing; otherwise, <c>false</c>.
		/// </value>
		public bool IsPlaying
		{
			get
			{
				NativeInvoke(FMOD_Channel_IsPlaying(this, out var isPlaying));
				return isPlaying;
			}
		}

		/// <summary>
		/// Gets whether the channel is virtual (emulated) or not due to the virtual channel management system.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is virtual; otherwise, <c>false</c>.
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
		/// <para>Gets or sets the current loop count for the specified channel.</para>
		/// <para>0 = oneshot, 1 = loop once then stop, -1 = loop forever, default = -1. </para>
		/// </summary>
		/// <value>
		/// The loop count.
		/// </value>
		/// <remarks>
		/// <para><i>Issues with streamed audio:</i></para>
		/// <para>When changing the loop count, sounds created with <seealso cref="FmodSystem.CreateStream"/> or <seealso cref="Mode.CreateStream"/> may have already been pre-buffered and executed their loop logic ahead of time before this call was even made. This is dependant on the size of the sound versus the size of the stream decode buffer (see FMOD_CREATESOUNDEXINFO). If this happens, you may need to reflush the stream buffer by calling Channel::setPosition. Note this will usually only happen if you have sounds or loop points that are smaller than the stream decode buffer size.</para>
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
		/// <para>Gets or sets the gain of the dry signal when lowpass filtering is applied.</para>
		/// <para>Linear gain level, from 0 (silent, full filtering) to 1.0 (full volume, no filtering), default = 1.0. </para>
		/// </summary>
		/// <value>
		/// The low pass gain.
		/// </value>
		/// <remarks>Requires the built in lowpass to be created with <see cref="InitFlags.ChannelLowpass"/> or <see cref="InitFlags.ChannelDistanceFilter"/>.</remarks>
		public float LowPassGain
		{
			get
			{
				NativeInvoke(FMOD_Channel_GetLowPassGain(this, out var gain));
				return gain;
			}
			set
			{
				NativeInvoke(FMOD_Channel_SetLowPassGain(this, value.Clamp(0.0f, 1.0f)));
				LowPassGainChanged?.Invoke(this, EventArgs.Empty);
			}
		}

		// TODO: Finish remarks in comments
		/// <summary>
		/// <para>Gets or sets maximum audible distance.</para>
		/// <para>Default = 10000.0.</para>
		/// </summary>
		/// <value>
		/// The 3D maximum distance.
		/// </value>
		/// <remarks>
		/// <para>When the listener is in-between the minimum distance and the sound source the volume will be at its maximum. As the listener moves from the minimum distance to the maximum distance the sound will attenuate following the rolloff curve set. When outside the maximum distance the sound will no longer attenuate.</para>
		/// <para>Minimum distance is useful to give the impression that the sound is loud or soft in 3D space. An example of this is a small quiet object, such as a bumblebee, which you could set a small mindistance such as 0.1. This would cause it to attenuate quickly and dissapear when only a few meters away from the listener. Another example is a jumbo jet, which you could set to a mindistance of 100.0 causing the volume to stay at its loudest until the listener was 100 meters away, then it would be hundreds of meters more before it would fade out.</para>
		/// <para>Maximum distance is effectively obsolete unless you need the sound to stop fading out at a certain point. Do not adjust this from the default if you dont need to. Some people have the confusion that maxdistance is the point the sound will fade out to zero, this is not the case.</para>
		/// 
		/// </remarks>
		public float ThreeDMaxDistance
		{
			get
			{
				NativeInvoke(FMOD_Channel_Get3DMinMaxDistance(this, out var dummy, out var max));
				return max;
			}
			set
			{
				NativeInvoke(FMOD_Channel_Get3DMinMaxDistance(this, out var min, out var dummy));
				NativeInvoke(FMOD_Channel_Set3DMinMaxDistance(this, min, value));
				ThreeDMaxDistanceChanged?.Invoke(this, EventArgs.Empty);
			}
		}

		// TODO: Finish remarks in comments
		/// <summary>
		/// <para>Gets or sets minimum audible distance.</para>
		/// <para>Default = 1.0.</para>
		/// </summary>
		/// <value>
		/// The 3D minimum distance.
		/// </value>
		/// <remarks>
		/// <para>When the listener is in-between the minimum distance and the sound source the volume will be at its maximum. As the listener moves from the minimum distance to the maximum distance the sound will attenuate following the rolloff curve set. When outside the maximum distance the sound will no longer attenuate.</para>
		/// <para>Minimum distance is useful to give the impression that the sound is loud or soft in 3D space. An example of this is a small quiet object, such as a bumblebee, which you could set a small mindistance such as 0.1. This would cause it to attenuate quickly and dissapear when only a few meters away from the listener. Another example is a jumbo jet, which you could set to a mindistance of 100.0 causing the volume to stay at its loudest until the listener was 100 meters away, then it would be hundreds of meters more before it would fade out.</para>
		/// <para>Maximum distance is effectively obsolete unless you need the sound to stop fading out at a certain point. Do not adjust this from the default if you dont need to. Some people have the confusion that maxdistance is the point the sound will fade out to zero, this is not the case.</para>
		/// 
		/// </remarks>
		public float ThreeMinDistance
		{
			get
			{
				NativeInvoke(FMOD_Channel_Get3DMinMaxDistance(this, out var min, out var dummy));
				return min;
			}
			set
			{
				NativeInvoke(FMOD_Channel_Get3DMinMaxDistance(this, out var dummy, out var max));
				NativeInvoke(FMOD_Channel_Set3DMinMaxDistance(this, value, max));
				ThreeDMinDistanceChanged?.Invoke(this, EventArgs.Empty);
			}
		}

		public Mode Mode
		{
			get
			{
				NativeInvoke(FMOD_Channel_GetMode(this, out var mode));
				return mode;
			}
			set
			{
				NativeInvoke(FMOD_Channel_SetMode(this, value));
				ModeChanged?.Invoke(this, EventArgs.Empty);
			}
		}

		public bool Muted
		{
			get
			{
				NativeInvoke(FMOD_Channel_GetMute(this, out var mute));
				return mute;
			}
			set
			{
				NativeInvoke(FMOD_Channel_SetMute(this, value));
				MuteChanged?.Invoke(this, EventArgs.Empty);
			}
		}

		public FmodSystem ParentSystem
		{
			get
			{
				NativeInvoke(FMOD_Channel_GetSystemObject(this, out var system));
				return Core.Create<FmodSystem>(system);
			}
		}

		public bool Paused
		{
			get
			{
				NativeInvoke(FMOD_Channel_GetPaused(this, out var paused));
				return paused;
			}
			set
			{
				NativeInvoke(FMOD_Channel_SetPaused(this, value));
				if (value)
					PlaybackPaused?.Invoke(this, EventArgs.Empty);
				else
					PlaybackResumed?.Invoke(this, EventArgs.Empty);
			}
		}

		public float Pitch
		{
			get
			{
				NativeInvoke(FMOD_Channel_GetPitch(this, out var pitch));
				return pitch;
			}
			set
			{
				NativeInvoke(FMOD_Channel_SetPitch(this, value.Clamp(0.5f, 2.0f)));
				PitchChanged?.Invoke(this, EventArgs.Empty);
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

		public float ReverbOcclusion
		{
			get
			{
				NativeInvoke(FMOD_Channel_Get3DOcclusion(this, out var dummy, out var reverb));
				return reverb;
			}
			set
			{
				NativeInvoke(FMOD_Channel_Get3DOcclusion(this, out var direct, out var dummy));
				NativeInvoke(FMOD_Channel_Set3DOcclusion(this, direct, value.Clamp(0.0f, 1.0f)));
				ThreeDOcclusionChanged?.Invoke(this, EventArgs.Empty);
			}
		}

		public ReverbProperties ReverbProperties
		{
			get
			{
				NativeInvoke(FMOD_Channel_GetReverbProperties(this, out var properties));
				return properties;
			}
			set
			{
				NativeInvoke(FMOD_Channel_SetReverbProperties(this, ref value));
				ReverbChanged?.Invoke(this, EventArgs.Empty);
			}
		}

		public float Spread
		{
			get
			{
				NativeInvoke(FMOD_Channel_Get3DSpread(this, out var angle));
				return angle;
			}
			set
			{
				NativeInvoke(FMOD_Channel_Set3DSpread(this, value.Clamp(0.0f, 360.0f)));
				SpreadChanged?.Invoke(this, EventArgs.Empty);
			}
		}

		/// <summary>
		/// Gets or sets the orientation of the sound projection cone.
		/// </summary>
		/// <value>
		/// The cone orientation.
		/// </value>
		public Vector ThreeDConeOrientation
		{
			get
			{
				NativeInvoke(FMOD_Channel_Get3DConeOrientation(this, out var vector));
				return vector;
			}
			set
			{
				NativeInvoke(FMOD_Channel_Set3DConeOrientation(this, ref value));
				ThreeDConeOrientationChanged?.Invoke(this, EventArgs.Empty);
			}
		}

		public ConeSettings ThreeDConeSettings
		{
			get
			{
				NativeInvoke(FMOD_Channel_Get3DConeSettings(this, out var insideAngle, out var outsideAngle, out var volume));
				return new ConeSettings
				{
					InsideAngle = insideAngle,
					OutsideAngle = outsideAngle,
					OutsideVolume = volume
				};
			}
			set
			{
				var insideAngle = value.InsideAngle.Clamp(0.0f, value.OutsideAngle);
				var outsideAngle = value.OutsideAngle.Clamp(value.InsideAngle, 360.0f);
				var volume = value.OutsideVolume.Clamp(0.0f, 1.0f);
				NativeInvoke(FMOD_Channel_Set3DConeSettings(this, insideAngle, outsideAngle, volume));
				ThreeDConeSettingsChanged?.Invoke(this, EventArgs.Empty);
			}
		}

		public Vector[] ThreeDCustomRolloff
		{
			get
			{
				NativeInvoke(FMOD_Channel_Get3DCustomRolloff(this, out var points, out var count));
				var vectors = new Vector[count];
				var size = Marshal.SizeOf(typeof(Vector));
				for (var i = 0; i < count; i++)
				{
					var ptr = points + i * size;
					vectors[i] = (Vector) Marshal.PtrToStructure(ptr, typeof(Vector));
				}
				return vectors;
			}
			set
			{
				NativeInvoke(FMOD_Channel_Set3DCustomRolloff(this, ref value, value.Length));
				ThreeDCustomRolloffChanged?.Invoke(this, EventArgs.Empty);
			}
		}

		public float ThreeDDopplerLevel
		{
			get
			{
				NativeInvoke(FMOD_Channel_Get3DDopplerLevel(this, out var doppler));
				return doppler;
			}
			set
			{
				NativeInvoke(FMOD_Channel_Set3DDopplerLevel(this, value.Clamp(0.0f, 5.0f)));
				ThreeDDopplerLevelChanged?.Invoke(this, EventArgs.Empty);
			}
		}

		public float ThreeDDirectOcclusion
		{
			get
			{
				NativeInvoke(FMOD_Channel_Get3DOcclusion(this, out var direct, out var dummy));
				return direct;
			}
			set
			{
				NativeInvoke(FMOD_Channel_Get3DOcclusion(this, out var dummy, out var reverb));
				NativeInvoke(FMOD_Channel_Set3DOcclusion(this, value.Clamp(0.0f, 1.0f), reverb));
				ThreeDOcclusionChanged?.Invoke(this, EventArgs.Empty);
			}
		}

		/// <summary>
		/// Gets or sets the settings for the 3D distance filter properties for the channel.
		/// </summary>
		/// <value>
		/// The three d distance filter.
		/// </value>
		public DistanceFilter ThreeDDistanceFilter
		{
			get
			{
				NativeInvoke(FMOD_Channel_Get3DDistanceFilter(this, out var custom, out var level, out var freq));
				return new DistanceFilter
				{
					Custom = custom,
					CustomLevel = level,
					CenterFrequency = freq
				};
			}
			set
			{
				var level = value.CustomLevel.Clamp(0.0f, 1.0f);
				var freq = value.CenterFrequency.Clamp(10.0f, 22050.0f);
				NativeInvoke(FMOD_Channel_Set3DDistanceFilter(this, value.Custom, level, freq));
				ThreeDDistanceFilterChanged?.Invoke(this, EventArgs.Empty);
			}
		}


		public float ThreeDLevel
		{
			get
			{
				NativeInvoke(FMOD_Channel_Get3DLevel(this, out var level));
				return level;
			}
			set
			{
				NativeInvoke(FMOD_Channel_Set3DLevel(this, value.Clamp(0.0f, 1.0f)));
				ThreeDLevelChanged?.Invoke(this, EventArgs.Empty);
			}
		}

		public Vector ThreeDPosition
		{
			get
			{
				NativeInvoke(FMOD_Channel_Get3DAttributes(this, out var position, out var dummy1, out var dummy2));
				return position;
			}
			set
			{
				NativeInvoke(FMOD_Channel_Set3DAttributes(this, ref value, IntPtr.Zero, IntPtr.Zero));
				ThreeDPositionChanged?.Invoke(this, EventArgs.Empty);
			}
		}

		public Vector ThreeDVelocity
		{
			get
			{
				NativeInvoke(FMOD_Channel_Get3DAttributes(this, out var dummy1, out var velocity, out var dummy2));
				return velocity;
			}
			set
			{
				NativeInvoke(FMOD_Channel_Set3DAttributes(this, IntPtr.Zero, ref value, IntPtr.Zero));
				ThreeDVelocityChanged?.Invoke(this, EventArgs.Empty);
			}
		}

		public IntPtr UserData
		{
			get
			{
				NativeInvoke(FMOD_Channel_GetUserData(this, out var data));
				return data;
			}
			set
			{
				NativeInvoke(FMOD_Channel_SetUserData(this, value));
				UserDataChanged?.Invoke(this, EventArgs.Empty);
			}
		}

		public float Volume
		{
			get
			{
				NativeInvoke(FMOD_Channel_GetVolume(this, out var volume));
				return volume;
			}
			set
			{
				NativeInvoke(FMOD_Channel_SetVolume(this, value));
				VolumeChanged?.Invoke(this, EventArgs.Empty);
			}
		}

		public bool VolumeRamp
		{
			get
			{
				NativeInvoke(FMOD_Channel_GetVolumeRamp(this, out var ramp));
				return ramp;
			}
			set
			{
				NativeInvoke(FMOD_Channel_SetVolumeRamp(this, value));
				VolumeRampChanged?.Invoke(this, EventArgs.Empty);
			}
		}

		#endregion

		#region Methods

		public void AddDsp(Dsp dsp, DspIndex index)
		{
			NativeInvoke(FMOD_Channel_AddDSP(this, index, dsp));
			DspAdded?.Invoke(this, EventArgs.Empty);
		}

		public void AddFadePoint(FadePoint fadePoint)
		{
			AddFadePoint(fadePoint.DspClock, fadePoint.Volume);
		}

		public void AddFadePoint(ulong dspClock, float targetVolume)
		{
			NativeInvoke(FMOD_Channel_AddFadePoint(this, dspClock, targetVolume));
			FadePointAdded?.Invoke(this, EventArgs.Empty);
		}

		public Dsp GetDsp(int index)
		{
			NativeInvoke(FMOD_Channel_GetDSP(this, index, out var dsp));
			return Core.Create<Dsp>(dsp);
		}

		public void GetDspClocks(out ulong head, out ulong tail)
		{
			NativeInvoke(FMOD_Channel_GetDSPClock(this, out head, out tail));
		}

		public int GetDspIndex(Dsp dsp)
		{
			NativeInvoke(FMOD_Channel_GetDSPIndex(this, dsp, out var index));
			return index;
		}

		public FadePoint[] GetFadePoints()
		{
			NativeInvoke(FMOD_Channel_GetFadePoints(this, out var numPoints, null, null));
			var dspClocks = new ulong[numPoints];
			var volumes = new float[numPoints];
			NativeInvoke(FMOD_Channel_GetFadePoints(this, out var dummy, dspClocks, volumes));
			var points = new FadePoint[numPoints];
			for (var i = 0; i < numPoints; i++)
				points[i] = new FadePoint { DspClock = dspClocks[i], Volume = volumes[i] };
			return points;
		}

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

		public void GetMixMatrix(float[] matrix, out int outChannels, out int inChannels, int inChannelHop)
		{
			NativeInvoke(FMOD_Channel_GetMixMatrix(this, matrix, out outChannels, out inChannels, inChannelHop));
		}

		public uint GetPosition(TimeUnit timeUnits = TimeUnit.Ms)
		{
			NativeInvoke(FMOD_Channel_GetPosition(this, out var position, timeUnits));
			return position;
		}

		public void Mute()
		{
			Muted = true;
		}

		public void Pause()
		{
			Paused = true;
		}

		public void RemoveDsp(Dsp dsp)
		{
			NativeInvoke(FMOD_Channel_RemoveDSP(this, dsp));
			DspRemoved?.Invoke(this, EventArgs.Empty);
		}

		public void RemoveFadePoints(ulong dspClockStart, ulong dspClockEnd)
		{
			NativeInvoke(FMOD_Channel_RemoveFadePoints(this, dspClockStart, dspClockEnd));
			FadePointRemoved?.Invoke(this, EventArgs.Empty);
		}

		public void Resume()
		{
			Paused = false;
		}

		public void SetConeSettings(float insideAngle, float outsideAngle, float outsideVolume)
		{
			NativeInvoke(FMOD_Channel_Set3DConeSettings(this, insideAngle, outsideAngle, outsideVolume));
			ThreeDConeSettingsChanged?.Invoke(this, EventArgs.Empty);
		}

		public void SetDelay(uint dspClockStart, uint dspClockEnd, bool stopChannels = false)
		{
			NativeInvoke(FMOD_Channel_SetDelay(this, dspClockStart, dspClockEnd, stopChannels));
			DelayChanged?.Invoke(this, EventArgs.Empty);
		}

		public void SetDistanceFilter(bool custom, float customLevel, float centerFrequency)
		{
			NativeInvoke(FMOD_Channel_Set3DDistanceFilter(this, custom, customLevel.Clamp(0.0f, 1.0f),
				centerFrequency.Clamp(10.0f, 22050.0f)));
			ThreeDDistanceFilterChanged?.Invoke(this, EventArgs.Empty);
		}

		public void SetDspIndex(Dsp dsp, int index)
		{
			NativeInvoke(FMOD_Channel_SetDSPIndex(this, dsp, index));
		}

		public void SetFadePointRamp(ulong dspClock, float targetVolume)
		{
			NativeInvoke(FMOD_Channel_SetFadePointRamp(this, dspClock, targetVolume));
		}

		public void SetFadePointRamp(FadePoint fadePoint)
		{
			NativeInvoke(FMOD_Channel_SetFadePointRamp(this, fadePoint.DspClock, fadePoint.Volume));
		}

		public void SetLoopPoints(LoopPoints points)
		{
			SetLoopPoints(points.LoopStart, points.LoopEnd, points.StartTimeUnit, points.EndTimeUnit);
		}

		public void SetLoopPoints(uint loopStart, uint loopEnd, TimeUnit timeUnit = TimeUnit.Ms)
		{
			SetLoopPoints(loopStart, loopEnd, timeUnit, timeUnit);
		}

		public void SetLoopPoints(uint loopStart, uint loopEnd, TimeUnit startUnit, TimeUnit endUnit)
		{
			NativeInvoke(FMOD_Channel_SetLoopPoints(this, loopStart, startUnit, loopEnd, endUnit));
			LoopPointAdded?.Invoke(this, EventArgs.Empty);
		}

		public void SetMinMaxDistance(float min, float max)
		{
			NativeInvoke(FMOD_Channel_Set3DMinMaxDistance(this, min, max));
		}


		public void SetMixLevelsInput(float[] levels)
		{
			NativeInvoke(FMOD_Channel_SetMixLevelsInput(this, levels, levels.Length));
		}

		public void SetMixLevelsOutput(float frontLeft, float frontRight,
			float center, float lowFreq, float surroundLeft, float surroundRight, float backLeft, float backRight)
		{
			frontLeft = frontLeft.Clamp(0.0f, 1.0f);
			frontRight = frontRight.Clamp(0.0f, 1.0f);
			backLeft = backLeft.Clamp(0.0f, 1.0f);
			backRight = backRight.Clamp(0.0f, 1.0f);
			surroundLeft = surroundLeft.Clamp(0.0f, 1.0f);
			surroundRight = surroundRight.Clamp(0.0f, 1.0f);
			lowFreq = lowFreq.Clamp(0.0f, 1.0f);
			center = center.Clamp(0.0f, 1.0f);
			NativeInvoke(FMOD_Channel_SetMixLevelsOutput(this, frontLeft, frontRight, center, lowFreq, surroundLeft,
				surroundRight, backLeft, backRight));
		}

		public void SetMixMatrix(float[] matrix, int outChannels, int inChannels, int inChannelHop)
		{
			NativeInvoke(FMOD_Channel_SetMixMatrix(this, matrix, outChannels, inChannels, inChannelHop));
			MixMatrixChanged?.Invoke(this, EventArgs.Empty);
		}

		public void SetPan(float pan)
		{
			NativeInvoke(FMOD_Channel_SetPan(this, pan.Clamp(-1.0f, 1.0f)));
			PanChanged?.Invoke(this, EventArgs.Empty);
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

		public void SetThreeDAttributes(Vector position, Vector velocity)
		{
			NativeInvoke(FMOD_Channel_Set3DAttributes(this, ref position, ref velocity, IntPtr.Zero));
			ThreeDPositionChanged?.Invoke(this, EventArgs.Empty);
			ThreeDVelocityChanged?.Invoke(this, EventArgs.Empty);
		}

		public void Stop()
		{
			NativeInvoke(FMOD_Channel_Stop(this));
			PlaybackStopped?.Invoke(this, EventArgs.Empty);
		}

		public void Unmute()
		{
			Muted = false;
		}

		protected Result ChannelCallbackHandler(IntPtr channelRaw, ChannelControlType controlType,
			ChannelControlCallbackType type, IntPtr commandData1, IntPtr commandData2)
		{
			if (controlType != ChannelControlType.Channel)
				return Result.OK;
			switch (type)
			{
				case ChannelControlCallbackType.End:
					SoundEnded?.Invoke(this, EventArgs.Empty);
					break;
				case ChannelControlCallbackType.Virtualvoice:
					VirtualVoiceSwapped?.Invoke(this, EventArgs.Empty);
					break;
				case ChannelControlCallbackType.Syncpoint:
					SyncpointEncountered?.Invoke(this, EventArgs.Empty);
					break;
				case ChannelControlCallbackType.Occlusion:
					OcclusionCalculated?.Invoke(this, EventArgs.Empty);
					break;
			}
			return Result.OK;
		}

		#endregion
	}
}