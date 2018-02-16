using System;
using System.Runtime.InteropServices;
using FMOD.Data;
using FMOD.Enumerations;
using FMOD.Structures;

namespace FMOD.Core
{
	/// <summary>
	/// <para>The base class for both <see cref="Channel"/> and <see cref="ChannelGroup"/>.</para>
	/// <para>This class must be inherited.</para>
	/// </summary>
	/// <seealso cref="HandleBase" />
	/// <seealso cref="Channel"/>
	/// <seealso cref="ChannelGroup"/>
	public abstract partial class ChannelControl : HandleBase
	{
		#region Constants & Fields

		/// <summary>
		/// It is necessary to keep a reference (non-local variable) to the callback, otherwise it 
		/// gets garbage-collected and will throw an exception when the callback is invoked by FMOD.
		/// </summary>
		// ReSharper disable once PrivateFieldCanBeConvertedToLocalVariable
		private readonly ChannelCallback _channelCallbackDelegate;

		#endregion

		internal ChannelControl(IntPtr handle) : base(handle)
		{
			_channelCallbackDelegate = OnChannelCallback;
			NativeInvoke(FMOD_ChannelGroup_SetCallback(this, _channelCallbackDelegate));
		}

		protected virtual Result OnChannelCallback(IntPtr channelControl, ChannelControlType controlType,
			ChannelControlCallbackType type, IntPtr commandData1, IntPtr commandData2)
		{
			return Result.OK;
		}


		public void AddDsp(Dsp dsp, DspIndex index)
		{
			NativeInvoke(FMOD_ChannelGroup_AddDSP(this, index, dsp));
			DspAdded?.Invoke(this, EventArgs.Empty);
		}

		public void AddDsp(Dsp dsp, int index)
		{
			NativeInvoke(FMOD_ChannelGroup_AddDSP(this, index, dsp));
			DspAdded?.Invoke(this, EventArgs.Empty);
		}

		public void AddFadePoint(FadePoint fadePoint)
		{
			AddFadePoint(fadePoint.DspClock, fadePoint.Volume);
		}

		public void AddFadePoint(ulong dspClock, float targetVolume)
		{
			NativeInvoke(FMOD_ChannelGroup_AddFadePoint(this, dspClock, targetVolume));
			FadePointAdded?.Invoke(this, EventArgs.Empty);
		}

		/// <summary>
		///     Gets the combined volume after 3D spatialization and geometry occlusion calculations including any volumes set via
		///     the API.
		/// </summary>
		/// <returns>The calculated volume.</returns>
		/// <remarks>
		///     This does not represent the waveform, just the calculated result of all volume modifiers. This value is used by the
		///     virtual channel system to order its channels between real and virtual.
		/// </remarks>
		public float GetAudibility()
		{
			NativeInvoke(FMOD_ChannelGroup_GetAudibility(this, out var audibility));
			return audibility;
		}

		public Dsp GetDsp(int index)
		{
			NativeInvoke(FMOD_ChannelGroup_GetDSP(this, index, out var dsp));
			return CoreHelper.Create<Dsp>(dsp);
		}

		public void GetDspClocks(out ulong head, out ulong tail)
		{
			NativeInvoke(FMOD_ChannelGroup_GetDSPClock(this, out head, out tail));
		}

		public int GetDspIndex(Dsp dsp)
		{
			NativeInvoke(FMOD_ChannelGroup_GetDSPIndex(this, dsp, out var index));
			return index;
		}

		public FadePoint[] GetFadePoints()
		{
			NativeInvoke(FMOD_ChannelGroup_GetFadePoints(this, out var numPoints, null, null));
			var dspClocks = new ulong[numPoints];
			var volumes = new float[numPoints];
			NativeInvoke(FMOD_ChannelGroup_GetFadePoints(this, out var dummy, dspClocks, volumes));
			var points = new FadePoint[numPoints];
			for (var i = 0; i < numPoints; i++)
				points[i] = new FadePoint { DspClock = dspClocks[i], Volume = volumes[i] };
			return points;
		}

		public void GetMixMatrix(float[] matrix, out int outChannels, out int inChannels, int inChannelHop)
		{
			NativeInvoke(FMOD_ChannelGroup_GetMixMatrix(this, matrix, out outChannels, out inChannels, inChannelHop));
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
			NativeInvoke(FMOD_ChannelGroup_RemoveDSP(this, dsp));
			DspRemoved?.Invoke(this, EventArgs.Empty);
		}

		public void RemoveFadePoints(ulong dspClockStart, ulong dspClockEnd)
		{
			NativeInvoke(FMOD_ChannelGroup_RemoveFadePoints(this, dspClockStart, dspClockEnd));
			FadePointRemoved?.Invoke(this, EventArgs.Empty);
		}

		public void Resume()
		{
			Paused = false;
		}

		public void SetConeSettings(float insideAngle, float outsideAngle, float outsideVolume)
		{
			NativeInvoke(FMOD_ChannelGroup_Set3DConeSettings(this, insideAngle, outsideAngle, outsideVolume));
			ThreeDConeSettingsChanged?.Invoke(this, EventArgs.Empty);
		}

		public void SetDelay(uint dspClockStart, uint dspClockEnd, bool stopChannels = false)
		{
			NativeInvoke(FMOD_ChannelGroup_SetDelay(this, dspClockStart, dspClockEnd, stopChannels));
			DelayChanged?.Invoke(this, EventArgs.Empty);
		}

		public void SetDistanceFilter(bool custom, float customLevel, float centerFrequency)
		{
			NativeInvoke(FMOD_ChannelGroup_Set3DDistanceFilter(this, custom, customLevel.Clamp(0.0f, 1.0f),
				centerFrequency.Clamp(10.0f, 22050.0f)));
			ThreeDDistanceFilterChanged?.Invoke(this, EventArgs.Empty);
		}

		public void SetDspIndex(Dsp dsp, DspIndex index)
		{
			NativeInvoke(FMOD_ChannelGroup_SetDSPIndex(this, dsp, index));
		}

		public void SetDspIndex(Dsp dsp, int index)
		{
			NativeInvoke(FMOD_ChannelGroup_SetDSPIndex(this, dsp, Math.Max(-3, index)));
		}

		public void SetFadePointRamp(ulong dspClock, float targetVolume)
		{
			NativeInvoke(FMOD_ChannelGroup_SetFadePointRamp(this, dspClock, targetVolume));
		}

		public void SetFadePointRamp(FadePoint fadePoint)
		{
			NativeInvoke(FMOD_ChannelGroup_SetFadePointRamp(this, fadePoint.DspClock, fadePoint.Volume));
		}


		public void SetMinMaxDistance(float min, float max)
		{
			NativeInvoke(FMOD_ChannelGroup_Set3DMinMaxDistance(this, min, max));
		}


		public void SetMixLevelsInput(float[] levels)
		{
			NativeInvoke(FMOD_ChannelGroup_SetMixLevelsInput(this, levels, levels.Length));
		}

		public void SetCallback(ChannelCallback callback)
		{
			NativeInvoke(FMOD_ChannelGroup_SetCallback(this, callback));
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
			NativeInvoke(FMOD_ChannelGroup_SetMixLevelsOutput(this, frontLeft, frontRight, center, lowFreq, surroundLeft,
				surroundRight, backLeft, backRight));
		}

		public void SetMixMatrix(float[] matrix, int outChannels, int inChannels, int inChannelHop)
		{
			NativeInvoke(FMOD_ChannelGroup_SetMixMatrix(this, matrix, outChannels, inChannels, inChannelHop));
			MixMatrixChanged?.Invoke(this, EventArgs.Empty);
		}

		public void SetPan(float pan)
		{
			NativeInvoke(FMOD_ChannelGroup_SetPan(this, pan.Clamp(-1.0f, 1.0f)));
			PanChanged?.Invoke(this, EventArgs.Empty);
		}


		public void SetThreeDAttributes(Vector position, Vector velocity)
		{
			NativeInvoke(FMOD_ChannelGroup_Set3DAttributes(this, ref position, ref velocity, IntPtr.Zero));
			ThreeDPositionChanged?.Invoke(this, EventArgs.Empty);
			ThreeDVelocityChanged?.Invoke(this, EventArgs.Empty);
		}

		public void Stop()
		{
			NativeInvoke(FMOD_ChannelGroup_Stop(this));
			PlaybackStopped?.Invoke(this, EventArgs.Empty);
		}

		public void Unmute()
		{
			Muted = false;
		}

		#region Delegates & Events

		public event EventHandler DelayChanged;

		public event EventHandler DspAdded;

		public event EventHandler DspRemoved;

		public event EventHandler FadePointAdded;

		public event EventHandler FadePointRemoved;

		public event EventHandler LowPassGainChanged;

		public event EventHandler MixMatrixChanged;

		public event EventHandler ModeChanged;

		public event EventHandler MuteChanged;

		public event EventHandler PanChanged;

		public event EventHandler PitchChanged;

		public event EventHandler PlaybackPaused;

		public event EventHandler PlaybackResumed;

		public event EventHandler PlaybackStopped;

		public event EventHandler ReverbChanged;

		public event EventHandler SpreadChanged;

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

		public event EventHandler VolumeChanged;

		public event EventHandler VolumeRampChanged;

		#endregion

		#region Properties & Indexers

		/// <summary>
		///     Gets or sets a start (and/or stop) time relative to the parent channel group DSP clock, with sample accuracy.
		/// </summary>
		/// <value>
		///     The delay.
		/// </value>
		/// <remarks>
		///     <para>
		///         Every channel and channel group has its own DSP Clock. A channel or channel group can be delayed relatively
		///         against its parent, with sample accurate positioning. To delay a sound, use the 'parent' channel group DSP
		///         clock to reference against when passing values into this function.
		///     </para>
		///     <para>
		///         If a parent channel group changes its pitch, the start and stop times will still be correct as the parent
		///         clock is rate adjusted by that pitch.
		///     </para>
		/// </remarks>
		public DspDelay Delay
		{
			get
			{
				NativeInvoke(FMOD_ChannelGroup_GetDelay(this, out var start, out var end, out var stop));
				return new DspDelay
				{
					ClockStart = start,
					ClockEnd = end,
					StopChannels = stop
				};
			}
			set
			{
				var start = value.ClockStart.Clamp<ulong>(0, value.ClockStart);
				var end = value.ClockStart.Clamp<ulong>(0, value.ClockEnd);
				NativeInvoke(FMOD_ChannelGroup_SetDelay(this, start, end, value.StopChannels));
				DelayChanged?.Invoke(this, EventArgs.Empty);
			}
		}

		/// <summary>
		///     Gets the number of DSP units in the DSP chain.
		/// </summary>
		/// <value>
		///     The DSP count.
		/// </value>
		public int DspCount
		{
			get
			{
				NativeInvoke(FMOD_ChannelGroup_GetNumDSPs(this, out var count));
				return count;
			}
		}

		/// <summary>
		///     Gets the DSP clock value for the head DSP node.
		/// </summary>
		/// <value>
		///     The DSP head clock.
		/// </value>
		/// <remarks>
		///     The DSP clocks count up by the number of samples per second in the software mixer, i.e. if the default sample
		///     rate is 48KHz, the DSP clock increments by 48000 per second.
		/// </remarks>
		public ulong DspHeadClock
		{
			get
			{
				NativeInvoke(FMOD_ChannelGroup_GetDSPClock(this, out var head, out var dummy));
				return head;
			}
		}

		/// <summary>
		///     Gets the DSP clock value for the tail DSP node.
		/// </summary>
		/// <value>
		///     The DSP tail clock.
		/// </value>
		/// <remarks>
		///     The DSP clocks count up by the number of samples per second in the software mixer, i.e. if the default sample
		///     rate is 48KHz, the DSP clock increments by 48000 per second.
		/// </remarks>
		public ulong DspTailClock
		{
			get
			{
				NativeInvoke(FMOD_ChannelGroup_GetDSPClock(this, out var dummy, out var tail));
				return tail;
			}
		}

		/// <summary>
		///     Gets the number of fade points stored within the channel.
		/// </summary>
		/// <value>
		///     The fade point count.
		/// </value>
		public int FadePointCount
		{
			get
			{
				NativeInvoke(FMOD_ChannelGroup_GetFadePoints(this, out var numPoints, null, null));
				return (int) numPoints;
			}
		}

		/// <summary>
		///     Gets a value indicating whether this instance is playing.
		/// </summary>
		/// <value>
		///     <c>true</c> if this instance is playing; otherwise, <c>false</c>.
		/// </value>
		public bool IsPlaying
		{
			get
			{
				NativeInvoke(FMOD_ChannelGroup_IsPlaying(this, out var isPlaying));
				return isPlaying;
			}
		}


		/// <summary>
		///     <para>Gets or sets the gain of the dry signal when lowpass filtering is applied.</para>
		///     <para>Linear gain level, from 0 (silent, full filtering) to 1.0 (full volume, no filtering), default = 1.0. </para>
		/// </summary>
		/// <value>
		///     The low pass gain.
		/// </value>
		/// <remarks>
		///     Requires the built in lowpass to be created with <see cref="InitFlags.ChannelLowpass" /> or
		///     <see cref="InitFlags.ChannelDistanceFilter" />.
		/// </remarks>
		public float LowPassGain
		{
			get
			{
				NativeInvoke(FMOD_ChannelGroup_GetLowPassGain(this, out var gain));
				return gain;
			}
			set
			{
				NativeInvoke(FMOD_ChannelGroup_SetLowPassGain(this, value.Clamp(0.0f, 1.0f)));
				LowPassGainChanged?.Invoke(this, EventArgs.Empty);
			}
		}

		// TODO: Finish remarks in comments
		/// <summary>
		///     <para>Gets or sets maximum audible distance.</para>
		///     <para>Default = 10000.0.</para>
		/// </summary>
		/// <value>
		///     The 3D maximum distance.
		/// </value>
		/// <remarks>
		///     <para>
		///         When the listener is in-between the minimum distance and the sound source the volume will be at its maximum.
		///         As the listener moves from the minimum distance to the maximum distance the sound will attenuate following the
		///         rolloff curve set. When outside the maximum distance the sound will no longer attenuate.
		///     </para>
		///     <para>
		///         Minimum distance is useful to give the impression that the sound is loud or soft in 3D space. An example of
		///         this is a small quiet object, such as a bumblebee, which you could set a small mindistance such as 0.1. This
		///         would cause it to attenuate quickly and dissapear when only a few meters away from the listener. Another
		///         example is a jumbo jet, which you could set to a mindistance of 100.0 causing the volume to stay at its loudest
		///         until the listener was 100 meters away, then it would be hundreds of meters more before it would fade out.
		///     </para>
		///     <para>
		///         Maximum distance is effectively obsolete unless you need the sound to stop fading out at a certain point. Do
		///         not adjust this from the default if you dont need to. Some people have the confusion that maxdistance is the
		///         point the sound will fade out to zero, this is not the case.
		///     </para>
		/// </remarks>
		public float MaxDistance3D
		{
			get
			{
				NativeInvoke(FMOD_ChannelGroup_Get3DMinMaxDistance(this, out var dummy, out var max));
				return max;
			}
			set
			{
				NativeInvoke(FMOD_ChannelGroup_Get3DMinMaxDistance(this, out var min, out var dummy));
				NativeInvoke(FMOD_ChannelGroup_Set3DMinMaxDistance(this, min, value));
				ThreeDMaxDistanceChanged?.Invoke(this, EventArgs.Empty);
			}
		}

		// TODO: Finish remarks in comments
		/// <summary>
		///     <para>Gets or sets minimum audible distance.</para>
		///     <para>Default = 1.0.</para>
		/// </summary>
		/// <value>
		///     The 3D minimum distance.
		/// </value>
		/// <remarks>
		///     <para>
		///         When the listener is in-between the minimum distance and the sound source the volume will be at its maximum.
		///         As the listener moves from the minimum distance to the maximum distance the sound will attenuate following the
		///         rolloff curve set. When outside the maximum distance the sound will no longer attenuate.
		///     </para>
		///     <para>
		///         Minimum distance is useful to give the impression that the sound is loud or soft in 3D space. An example of
		///         this is a small quiet object, such as a bumblebee, which you could set a small mindistance such as 0.1. This
		///         would cause it to attenuate quickly and dissapear when only a few meters away from the listener. Another
		///         example is a jumbo jet, which you could set to a mindistance of 100.0 causing the volume to stay at its loudest
		///         until the listener was 100 meters away, then it would be hundreds of meters more before it would fade out.
		///     </para>
		///     <para>
		///         Maximum distance is effectively obsolete unless you need the sound to stop fading out at a certain point. Do
		///         not adjust this from the default if you dont need to. Some people have the confusion that maxdistance is the
		///         point the sound will fade out to zero, this is not the case.
		///     </para>
		/// </remarks>
		public float MinDistance3D
		{
			get
			{
				NativeInvoke(FMOD_ChannelGroup_Get3DMinMaxDistance(this, out var min, out var dummy));
				return min;
			}
			set
			{
				NativeInvoke(FMOD_ChannelGroup_Get3DMinMaxDistance(this, out var dummy, out var max));
				NativeInvoke(FMOD_ChannelGroup_Set3DMinMaxDistance(this, value, max));
				ThreeDMinDistanceChanged?.Invoke(this, EventArgs.Empty);
			}
		}

		public Mode Mode
		{
			get
			{
				NativeInvoke(FMOD_ChannelGroup_GetMode(this, out var mode));
				return mode;
			}
			set
			{
				NativeInvoke(FMOD_ChannelGroup_SetMode(this, value));
				ModeChanged?.Invoke(this, EventArgs.Empty);
			}
		}

		public bool Muted
		{
			get
			{
				NativeInvoke(FMOD_ChannelGroup_GetMute(this, out var mute));
				return mute;
			}
			set
			{
				NativeInvoke(FMOD_ChannelGroup_SetMute(this, value));
				MuteChanged?.Invoke(this, EventArgs.Empty);
			}
		}

		public FmodSystem ParentSystem
		{
			get
			{
				NativeInvoke(FMOD_ChannelGroup_GetSystemObject(this, out var system));
				return CoreHelper.Create<FmodSystem>(system);
			}
		}

		public bool Paused
		{
			get
			{
				NativeInvoke(FMOD_ChannelGroup_GetPaused(this, out var paused));
				return paused;
			}
			set
			{
				NativeInvoke(FMOD_ChannelGroup_SetPaused(this, value));
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
				NativeInvoke(FMOD_ChannelGroup_GetPitch(this, out var pitch));
				return pitch;
			}
			set
			{
				NativeInvoke(FMOD_ChannelGroup_SetPitch(this, value.Clamp(0.5f, 2.0f)));
				PitchChanged?.Invoke(this, EventArgs.Empty);
			}
		}


		public float ReverbOcclusion
		{
			get
			{
				NativeInvoke(FMOD_ChannelGroup_Get3DOcclusion(this, out var dummy, out var reverb));
				return reverb;
			}
			set
			{
				NativeInvoke(FMOD_ChannelGroup_Get3DOcclusion(this, out var direct, out var dummy));
				NativeInvoke(FMOD_ChannelGroup_Set3DOcclusion(this, direct, value.Clamp(0.0f, 1.0f)));
				ThreeDOcclusionChanged?.Invoke(this, EventArgs.Empty);
			}
		}


		public void SetReverbProperties(int reverbIndex, float wet)
		{
			NativeInvoke(FMOD_ChannelGroup_SetReverbProperties(this, reverbIndex, wet));
			ReverbChanged?.Invoke(this, EventArgs.Empty);
		}

		public float GetReverbProperties(int reverbIndex)
		{
			NativeInvoke(FMOD_ChannelGroup_GetReverbProperties(this, reverbIndex, out var wet));
			return wet;
		}

		public float Spread
		{
			get
			{
				NativeInvoke(FMOD_ChannelGroup_Get3DSpread(this, out var angle));
				return angle;
			}
			set
			{
				NativeInvoke(FMOD_ChannelGroup_Set3DSpread(this, value.Clamp(0.0f, 360.0f)));
				SpreadChanged?.Invoke(this, EventArgs.Empty);
			}
		}

		/// <summary>
		///     Gets or sets the orientation of the sound projection cone.
		/// </summary>
		/// <value>
		///     The cone orientation.
		/// </value>
		public Vector ConeOrientation3D
		{
			get
			{
				NativeInvoke(FMOD_ChannelGroup_Get3DConeOrientation(this, out var vector));
				return vector;
			}
			set
			{
				NativeInvoke(FMOD_ChannelGroup_Set3DConeOrientation(this, ref value));
				ThreeDConeOrientationChanged?.Invoke(this, EventArgs.Empty);
			}
		}

		public ConeSettings ConeSettings3D
		{
			get
			{
				NativeInvoke(FMOD_ChannelGroup_Get3DConeSettings(this, out var insideAngle, out var outsideAngle, out var volume));
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
				NativeInvoke(FMOD_ChannelGroup_Set3DConeSettings(this, insideAngle, outsideAngle, volume));
				ThreeDConeSettingsChanged?.Invoke(this, EventArgs.Empty);
			}
		}

		public Vector[] CustomRolloff3D
		{
			get
			{
				NativeInvoke(FMOD_ChannelGroup_Get3DCustomRolloff(this, out var points, out var count));
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
				NativeInvoke(FMOD_ChannelGroup_Set3DCustomRolloff(this, ref value, value.Length));
				ThreeDCustomRolloffChanged?.Invoke(this, EventArgs.Empty);
			}
		}

		public float DopplerLevel3D
		{
			get
			{
				NativeInvoke(FMOD_ChannelGroup_Get3DDopplerLevel(this, out var doppler));
				return doppler;
			}
			set
			{
				NativeInvoke(FMOD_ChannelGroup_Set3DDopplerLevel(this, value.Clamp(0.0f, 5.0f)));
				ThreeDDopplerLevelChanged?.Invoke(this, EventArgs.Empty);
			}
		}

		public float DirectOcclusion3D
		{
			get
			{
				NativeInvoke(FMOD_ChannelGroup_Get3DOcclusion(this, out var direct, out var dummy));
				return direct;
			}
			set
			{
				NativeInvoke(FMOD_ChannelGroup_Get3DOcclusion(this, out var dummy, out var reverb));
				NativeInvoke(FMOD_ChannelGroup_Set3DOcclusion(this, value.Clamp(0.0f, 1.0f), reverb));
				ThreeDOcclusionChanged?.Invoke(this, EventArgs.Empty);
			}
		}

		/// <summary>
		///     Gets or sets the settings for the 3D distance filter properties for the channel.
		/// </summary>
		/// <value>
		///     The three d distance filter.
		/// </value>
		public DistanceFilter DistanceFilter3D
		{
			get
			{
				NativeInvoke(FMOD_ChannelGroup_Get3DDistanceFilter(this, out var custom, out var level, out var freq));
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
				NativeInvoke(FMOD_ChannelGroup_Set3DDistanceFilter(this, value.Custom, level, freq));
				ThreeDDistanceFilterChanged?.Invoke(this, EventArgs.Empty);
			}
		}


		public float Level3D
		{
			get
			{
				NativeInvoke(FMOD_ChannelGroup_Get3DLevel(this, out var level));
				return level;
			}
			set
			{
				NativeInvoke(FMOD_ChannelGroup_Set3DLevel(this, value.Clamp(0.0f, 1.0f)));
				ThreeDLevelChanged?.Invoke(this, EventArgs.Empty);
			}
		}

		public Vector Position3D
		{
			get
			{
				NativeInvoke(FMOD_ChannelGroup_Get3DAttributes(this, out var position, out var dummy1, out var dummy2));
				return position;
			}
			set
			{
				NativeInvoke(FMOD_ChannelGroup_Set3DAttributes(this, ref value, IntPtr.Zero, IntPtr.Zero));
				ThreeDPositionChanged?.Invoke(this, EventArgs.Empty);
			}
		}

		public Vector Velocity3D
		{
			get
			{
				NativeInvoke(FMOD_ChannelGroup_Get3DAttributes(this, out var dummy1, out var velocity, out var dummy2));
				return velocity;
			}
			set
			{
				NativeInvoke(FMOD_ChannelGroup_Set3DAttributes(this, IntPtr.Zero, ref value, IntPtr.Zero));
				ThreeDVelocityChanged?.Invoke(this, EventArgs.Empty);
			}
		}

		public IntPtr UserData
		{
			get
			{
				NativeInvoke(FMOD_ChannelGroup_GetUserData(this, out var data));
				return data;
			}
			set
			{
				NativeInvoke(FMOD_ChannelGroup_SetUserData(this, value));
				UserDataChanged?.Invoke(this, EventArgs.Empty);
			}
		}

		public float Volume
		{
			get
			{
				NativeInvoke(FMOD_ChannelGroup_GetVolume(this, out var volume));
				return volume;
			}
			set
			{
				NativeInvoke(FMOD_ChannelGroup_SetVolume(this, value));
				VolumeChanged?.Invoke(this, EventArgs.Empty);
			}
		}

		public bool VolumeRamp
		{
			get
			{
				NativeInvoke(FMOD_ChannelGroup_GetVolumeRamp(this, out var ramp));
				return ramp;
			}
			set
			{
				NativeInvoke(FMOD_ChannelGroup_SetVolumeRamp(this, value));
				VolumeRampChanged?.Invoke(this, EventArgs.Empty);
			}
		}

		#endregion
	}
}