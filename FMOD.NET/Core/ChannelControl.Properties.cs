#region License

// ChannelControl.Properties.cs is distributed under the Microsoft Public License (MS-PL)
// 
// Copyright (c) 2018,  Eric Freed
// All Rights Reserved.
// 
// This license governs use of the accompanying software. If you use the software, you
// accept this license. If you do not accept the license, do not use the software.
// 
// 1. Definitions
// The terms "reproduce," "reproduction," "derivative works," and "distribution" have the
// same meaning here as under U.S. copyright law.
// A "contribution" is the original software, or any additions or changes to the software.
// A "contributor" is any person that distributes its contribution under this license.
// "Licensed patents" are a contributor's patent claims that read directly on its contribution.
// 
// 2. Grant of Rights
// (A) Copyright Grant- Subject to the terms of this license, including the license conditions 
// and limitations in section 3, each contributor grants you a non-exclusive, worldwide, royalty-free 
// copyright license to reproduce its contribution, prepare derivative works of its contribution, and 
// distribute its contribution or any derivative works that you create.
// 
// (B) Patent Grant- Subject to the terms of this license, including the license conditions and 
// limitations in section 3, each contributor grants you a non-exclusive, worldwide, royalty-free license
//  under its licensed patents to make, have made, use, sell, offer for sale, import, and/or otherwise 
// dispose of its contribution in the software or derivative works of the contribution in the software.
// 
// 3. Conditions and Limitations
// (A) No Trademark License- This license does not grant you rights to use any contributors' name, 
// logo, or trademarks.
// 
// (B) If you bring a patent claim against any contributor over patents that you claim are infringed by 
// the software, your patent license from such contributor to the software ends automatically.
// 
// (C) If you distribute any portion of the software, you must retain all copyright, patent, trademark, and
//  attribution notices that are present in the software.
// 
// (D) If you distribute any portion of the software in source code form, you may do so only under this 
// license by including a complete copy of this license with your distribution. If you distribute any portion
//  of the software in compiled or object code form, you may only do so under a license that complies 
// with this license.
// 
// (E) The software is licensed "as-is." You bear the risk of using it. The contributors give no express 
// warranties, guarantees or conditions. You may have additional consumer rights under your local laws 
// which this license cannot change. To the extent permitted under your local laws, the contributors 
// exclude the implied warranties of merchantability, fitness for a particular purpose and non-infringement.
// 
// Created 11:59 PM 02/21/2018

#endregion

#region Using Directives

using System;
using System.Runtime.InteropServices;
using FMOD.NET.Data;
using FMOD.NET.Enumerations;
using FMOD.NET.Structures;

#endregion

namespace FMOD.NET.Core
{
	public partial class ChannelControl
	{
		#region Properties

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
		/// ///
		/// <seealso cref="SetDelay" />
		/// <seealso cref="DspDelay" />
		/// <seealso cref="IsPlaying" />
		/// <seealso cref="DelayChanged" />
		/// <seealso cref="DspHeadClock" />
		/// <seealso cref="DspTailClock" />
		/// <seealso cref="GetDspClocks" />
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
			set => SetDelay(value.ClockStart, value.ClockEnd, value.StopChannels);
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
		///     <para>
		///         Use result with <see cref="SetDelay" /> or <see cref="Delay" /> to play a sound on an exact tick in the
		///         future, or stop it in the future.
		///     </para>
		///     <para>
		///         When delaying a channel or channel group you want to sync it to the parent channel group DSP clock value, not
		///         its own DSP clock value.
		///     </para>
		/// </remarks>
		/// <seealso cref="DspTailClock" />
		/// <seealso cref="GetDspClocks" />
		/// <seealso cref="Delay" />
		/// <seealso cref="DspDelay" />
		/// <seealso cref="SetDelay" />
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
		///     <para>
		///         Use result with <see cref="SetDelay" /> or <see cref="Delay" /> to play a sound on an exact tick in the
		///         future, or stop it in the future.
		///     </para>
		///     <para>
		///         When delaying a channel or channel group you want to sync it to the parent channel group DSP clock value, not
		///         its own DSP clock value.
		///     </para>
		/// </remarks>
		/// <seealso cref="DspHeadClock" />
		/// <seealso cref="GetDspClocks" />
		/// <seealso cref="Delay" />
		/// <seealso cref="DspDelay" />
		/// <seealso cref="SetDelay" />
		public ulong DspTailClock
		{
			get
			{
				NativeInvoke(FMOD_ChannelGroup_GetDSPClock(this, out var dummy, out var tail));
				return tail;
			}
		}

		/// <summary>
		///     Gets or sets the mute state effectively silencing it or returning it to its normal volume.
		/// </summary>
		/// <value>
		///     <c>true</c> if muted; otherwise, <c>false</c>.
		/// </value>
		/// <remarks>
		///     Each <see cref="Channel" /> and <see cref="ChannelGroup" /> has its own mute state, muting a channel group will
		///     mute all child channels but will not affect their individual setting.
		/// </remarks>
		/// <seealso cref="Mute" />
		/// <seealso cref="Unmute" />
		/// <seealso cref="MuteChanged" />
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
				OnMuteChanged();
			}
		}

		/// <summary>
		///     Gets or sets a value indicating whether this <see cref="ChannelControl" /> is paused.
		/// </summary>
		/// <value>
		///     <c>true</c> if paused; otherwise, <c>false</c>.
		/// </value>
		/// <remarks>
		///     Each <see cref="Channel" /> and <see cref="ChannelGroup" /> has its own paused state, pausing a channel group
		///     will pause all contained channels but will not affect their individual setting.
		/// </remarks>
		/// <seealso cref="Pause" />
		/// <seealso cref="Resume" />
		/// <seealso cref="PauseChanged" />
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
				OnPauseChanged();
			}
		}

		/// <summary>
		///     Gets the parent <see cref="FmodSystem" /> instance that created the <see cref="Channel" /> or
		///     <see cref="ChannelGroup" />.
		/// </summary>
		/// <value>
		///     The parent system.
		/// </value>
		/// <seealso cref="FmodSystem" />
		public FmodSystem ParentSystem
		{
			get
			{
				NativeInvoke(FMOD_ChannelGroup_GetSystemObject(this, out var system));
				return Factory.Create<FmodSystem>(system);
			}
		}

		/// <summary>
		///     Gets the number of <see cref="Dsp" /> units in the DSP chain.
		/// </summary>
		/// <value>
		///     The DSP count.
		/// </value>
		/// <seealso cref="Dsp" />
		/// <seealso cref="O:FMOD.NET.Core.ChannelControl.AddDsp" />
		/// <seealso cref="O:FMOD.NET.Core.ChannelControl.GetDsp" />
		/// <seealso cref="RemoveDsp" />
		/// <seealso cref="O:FMOD.NET.Core.ChannelControl.RemoveDspAtIndex" />
		public int DspCount
		{
			get
			{
				NativeInvoke(FMOD_ChannelGroup_GetNumDSPs(this, out var count));
				return count;
			}
		}

		/// <summary>
		///     Gets the number of fade points stored within the channel.
		/// </summary>
		/// <value>
		///     The fade point count.
		/// </value>
		/// <seealso cref="FadePoint" />
		/// <seealso cref="AddFadePoints(FadePoint)" />
		/// <seealso cref="AddFadePoints(ulong,float)" />
		/// <seealso cref="RemoveFadePoints" />
		/// <seealso cref="GetFadePoints" />
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
		/// <seealso cref="FmodSystem.PlaySound" />
		/// <seealso cref="FmodSystem.PlayDsp" />
		public bool IsPlaying
		{
			get
			{
				NativeInvoke(FMOD_ChannelGroup_IsPlaying(this, out var isPlaying));
				return isPlaying;
			}
		}

		/// <summary>
		///     Gets or sets the orientation of the sound projection cone.
		/// </summary>
		/// <value>
		///     The cone orientation.
		/// </value>
		/// <remarks>
		///     Setting this has no effect unless the cone angle and cone outside volume have also been set to values other than
		///     the default.
		/// </remarks>
		/// <seealso cref="ConeSettings3D" />
		/// <seealso cref="ConeSettings" />
		/// <seealso cref="SetConeSettings" />
		/// <seealso cref="Vector" />
		/// <seealso cref="ConeOrientation3DChanged" />
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
				OnConeOrientation3DChanged();
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
			set => SetConeSettings(value.InsideAngle, value.OutsideAngle, value.OutsideVolume);
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
		/// <seealso cref="LowPassGainChanged" />
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
				OnLowPassGainChanged();
			}
		}

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
		///     <para>
		///         A "distance unit" is specified by <see cref="FmodSystem.Settings3D" />. By default this is set to meters which
		///         is a distance scale of <c>1.0</c>.
		///     </para>
		///     <para>
		///         To define the minimum and maximum distance per sound use <see cref="Sound.MinDistance3D" /> and
		///         <see cref="Sound.MaxDistance3D" />.
		///     </para>
		/// </remarks>
		/// <para>
		///     If <see cref="Enumerations.Mode.CustomRolloff3D" /> is used, then these values are stored, but ignored in 3D
		///     processing.
		/// </para>
		/// <seealso cref="Sound.MinDistance3D" />
		/// <seealso cref="Sound.MaxDistance3D" />
		/// <seealso cref="FmodSystem.Settings3D" />
		/// <seealso cref="Enumerations.Mode.CustomRolloff3D" />
		/// <seealso cref="Distance3DChanged" />
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
				OnDistance3DChanged();
			}
		}

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
		///     <para>
		///         A "distance unit" is specified by <see cref="FmodSystem.Settings3D" />. By default this is set to meters which
		///         is a distance scale of <c>1.0</c>.
		///     </para>
		///     <para>
		///         To define the minimum and maximum distance per sound use <see cref="Sound.MinDistance3D" /> and
		///         <see cref="Sound.MaxDistance3D" />.
		///     </para>
		/// </remarks>
		/// <para>
		///     If <see cref="Enumerations.Mode.CustomRolloff3D" /> is used, then these values are stored, but ignored in 3D
		///     processing.
		/// </para>
		/// <seealso cref="Sound.MinDistance3D" />
		/// <seealso cref="Sound.MaxDistance3D" />
		/// <seealso cref="FmodSystem.Settings3D" />
		/// <seealso cref="Enumerations.Mode.CustomRolloff3D" />
		/// <seealso cref="Distance3DChanged" />
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
				OnDistance3DChanged();
			}
		}

		/// <summary>
		///     Gets or sets the attributes for a <see cref="Channel" /> or <see cref="ChannelGroup" /> based on the mode passed
		///     in.
		/// </summary>
		/// <value>
		///     The mode.
		/// </value>
		/// <remarks>
		///     <para>Not all flags are supported. Following is a list of supported flags for a <see cref="ChannelControl" />.</para>
		///     <list type="bullet">
		///         <item>
		///             <para>
		///                 <see cref="Enumerations.Mode.LoopOff" />
		///             </para>
		///         </item>
		///         <item>
		///             <para>
		///                 <see cref="Enumerations.Mode.LoopNormal" />
		///             </para>
		///         </item>
		///         <item>
		///             <para>
		///                 <see cref="Enumerations.Mode.LoopBidi" />
		///             </para>
		///         </item>
		///         <item>
		///             <para>
		///                 <see cref="Enumerations.Mode.TwoD" />
		///             </para>
		///         </item>
		///         <item>
		///             <para>
		///                 <see cref="Enumerations.Mode.ThreeD" />
		///             </para>
		///         </item>
		///         <item>
		///             <para>
		///                 <see cref="Enumerations.Mode.HeadRelative3D" />
		///             </para>
		///         </item>
		///         <item>
		///             <para>
		///                 <see cref="Enumerations.Mode.WorldRelative3D" />
		///             </para>
		///         </item>
		///         <item>
		///             <para>
		///                 <see cref="Enumerations.Mode.InverseRolloff3D" />
		///             </para>
		///         </item>
		///         <item>
		///             <para>
		///                 <see cref="Enumerations.Mode.LinearRolloff3D" />
		///             </para>
		///         </item>
		///         <item>
		///             <para>
		///                 <see cref="Enumerations.Mode.LinearSquareRolloff3D" />
		///             </para>
		///         </item>
		///         <item>
		///             <para>
		///                 <see cref="Enumerations.Mode.CustomRolloff3D" />
		///             </para>
		///         </item>
		///         <item>
		///             <para>
		///                 <see cref="Enumerations.Mode.IgnoreGeometry3D" />
		///             </para>
		///         </item>
		///         <item>
		///             <para>
		///                 <see cref="Enumerations.Mode.VirtualPlayFromStart" />
		///             </para>
		///         </item>
		///     </list>
		///     <para>
		///         <i>ssues with streamed audio:</i>
		///     </para>
		///     <para>
		///         When changing the loop mode, sounds created with <see cref="O:FMOD.NET.Core.FmodSystem.CreateStream" /> or
		///         <see cref="Enumerations.Mode.CreateStream" /> may have already been pre-buffered and executed their loop logic
		///         ahead of time before this call was even made. This is dependant on the size of the sound versus the size of the
		///         stream decode buffer (see <see cref="CreateSoundExInfo" />). If this happens, you may need to reflush the
		///         stream buffer by calling <see cref="Channel.SetPosition" />. Note this will usually only happen if
		///         you have sounds or loop points that are smaller than the stream decode buffer size.
		///     </para>
		///     <para>
		///         <i>ssues with PCM samples:</i>
		///     </para>
		///     <para>
		///         When changing the loop mode of sounds created with with <see cref="O:FMOD.NET.Core.FmodSystem.CreateSound" /> or
		///         <see cref="Enumerations.Mode.CreateSample" />, if the sound was set up as
		///         <see cref="Enumerations.Mode.LoopOff" />, then set to <see cref="Enumerations.Mode.LoopNormal" /> with this
		///         function, the sound may click when playing the end of the sound. This is because the sound needs to be
		///         pre-prepared for looping using <see cref="Sound.Mode" />, by modifying the content of the PCM data (i.e. data
		///         past the end of the actual sample data) to allow the interpolators to read ahead without clicking. If you use
		///         <see cref="ChannelControl.Mode" /> it will not do this (because different channels may have different loop
		///         modes for the same sound) and may click if you try to set it to looping on an unprepared sound. If you want to
		///         change the loop mode at runtime it may be better to load the sound as looping first (or use
		///         <see cref="Sound.Mode" />), to let it pre-prepare the data as if it was looping so that it does not click
		///         whenever <see cref="Mode" /> is used to turn looping on.
		///     </para>
		///     <para>
		///         If <see cref="Enumerations.Mode.IgnoreGeometry3D" /> or <see cref="Enumerations.Mode.VirtualPlayFromStart" />
		///         is not specified, the flag will be cleared if it was specified previously.
		///     </para>
		/// </remarks>
		/// <seealso cref="Enumerations.Mode" />
		/// <seealso cref="Channel.SetPosition" />
		/// <seealso cref="Sound.Mode" />
		/// <seealso cref="O:FMOD.NET.Core.FmodSystem.CreateSound" />
		/// <seealso cref="O:FMOD.NET.Core.FmodSystem.CreateStream" />
		/// <seealso cref="CreateSoundExInfo" />
		/// <seealso cref="ModeChanged" />
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
				OnModeChanged();
			}
		}

		/// <summary>
		///     <para>Gets or sets the pitch value.</para>
		///     <para><c>0.5</c> = one octave down, <c>2.0</c> = one octave up, Default = <c>1.0</c>.</para>
		/// </summary>
		/// <value>
		///     The pitch.
		/// </value>
		/// <remarks>This function scales existing frequency values by the pitch.</remarks>
		/// <seealso cref="PitchChanged" />
		/// <seealso cref="Channel.Frequency" />
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
				OnPitchChanged();
			}
		}

		/// <summary>
		///     Gets or sets the volume level linearly.
		///     <para>Linear volume level, default = <c>1.0</c>.</para>
		/// </summary>
		/// <value>
		///     The volume.
		/// </value>
		/// <remarks>
		///     <para>
		///         Volume level can be below 0 to invert a signal and above 1 to amplify the signal. Note that increasing the
		///         signal level too far may cause audible distortion.
		///     </para>
		/// </remarks>
		/// <seealso cref="VolumeChanged" />
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
				OnVolumeChanged();
			}
		}

		/// <summary>
		///     <para>Gets or sets whether the channel automatically ramps when setting volumes.</para>
		///     <para>Default = <c>true</c>.</para>
		/// </summary>
		/// <value>
		///     <c>true</c> if volume ramps; otherwise, <c>false</c>.
		/// </value>
		/// <remarks>
		///     When changing volumes on a non-paused channel, FMOD normally adds a small ramp to avoid a pop sound. This
		///     function allows that setting to be overriden and volume changes to be applied immediately.
		/// </remarks>
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
				OnVolumeRampChanged();
			}
		}

		/// <summary>
		///     Gets or sets the position used to apply panning, attenuation and doppler.
		/// </summary>
		/// <value>
		///     The position.
		/// </value>
		/// <seealso cref="Position3DChanged" />
		/// <seealso cref="Vector" />
		/// <seealso cref="Velocity3D" />
		/// <seealso cref="SetAttributes3D" />
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
				OnPosition3DChanged();
			}
		}

		/// <summary>
		///     Gets or sets the velocity used to apply panning, attenuation and doppler.
		/// </summary>
		/// <value>
		///     The position.
		/// </value>
		/// <seealso cref="Velocity3DChanged" />
		/// <seealso cref="Vector" />
		/// <seealso cref="Position3D" />
		/// <seealso cref="SetAttributes3D" />
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
				OnVelocity3DChanged();
			}
		}

		/// <summary>
		///     <para>Gets or sets how much the 3D engine has an effect on the channel, versus that set by 2D panning functions.</para>
		///     <para>
		///         3D pan level from <c>0.0</c> (attenuation is ignored and panning as set by 2D panning functions) to
		///         <c>1.0</c> (pan and attenuate according to 3D position), Default = <c>1.0</c>.
		///     </para>
		/// </summary>
		/// <value>
		///     The 3D pan level.
		/// </value>
		/// <remarks>
		///     <para>Only affects sounds created with <see cref="Enumerations.Mode.ThreeD" />.</para>
		///     <para>
		///         2D panning functions include <see cref="SetPan" />, <see cref="SetMixLevelsOutput" />,
		///         <see cref="SetMixLevelsInput" />, <see cref="SetMixMatrix" />, etc
		///     </para>
		///     <para>
		///         Useful for morhping a sound between 3D and 2D. This is most common in volumetric sound, when the sound goes
		///         from directional, to "all around you" (and doesn't pan according to listener position / direction).
		///     </para>
		/// </remarks>
		/// <seealso cref="Enumerations.Mode" />
		/// <seealso cref="SetPan" />
		/// <seealso cref="SetMixLevelsOutput" />
		/// <seealso cref="SetMixLevelsInput" />
		/// <seealso cref="SetMixMatrix" />
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
				OnLevel3DChanged();
			}
		}

		/// <summary>
		///     Gets or sets the amount by which doppler is scaled.
		///     <para>
		///         Doppler scale from <c>0.0</c> (none), to <c>1.0</c> (normal) to <c>5.0</c> (exaggerated), Default =
		///         <c>1.0</c>.
		///     </para>
		/// </summary>
		/// <value>
		///     The doppler level.
		/// </value>
		/// <seealso cref="DopplerLevel3DChanged" />
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
				OnDopplerLevel3DChanged();
			}
		}

		/// <summary>
		///     Gets or sets the spread of a 3D sound in speaker space.
		///     <para>
		///         Speaker spread angle. <c>0</c> = all sound channels are located at the same speaker location and is "mono".
		///         <c>360</c> = all sound channels are located at the opposite speaker location to the speaker location that it
		///         should be according to 3D position. Default = <c>0.0</c>.
		///     </para>
		/// </summary>
		/// <value>
		///     The spread angle.
		/// </value>
		/// <remarks>
		///     <para>
		///         Normally a 3D sound is aimed at one position in a speaker array depending on the 3D position to give it
		///         direction. Left and right parts of a stereo sound for example are consequently summed together and become
		///         "mono". When increasing the "spread" of a sound, the left and right parts of a stereo sound rotate away from
		///         their original position, to give it more "stereoness". The rotation of the sound channels are done in "speaker
		///         space".
		///     </para>
		///     <para>
		///         Multichannel sounds with channel counts greater than stereo have their sub-channels spread evently through
		///         the specified angle. For example a 6 channel sound over a 90 degree spread has each channel located 15 degrees
		///         apart from each other in the speaker array.
		///     </para>
		///     <para>
		///         Mono sounds are spread as if they were a stereo signal, i.e. the signal is split into 2. The power will
		///         remain the same as it spreads around the speakers.
		///     </para>
		///     <para>To summarize (for a stereo sound).</para>
		///     <list type="ordered">
		///         <item>
		///             <para>A spread angle of 0 makes the stereo sound mono at the point of the 3D emitter. </para>
		///         </item>
		///         <item>
		///             <para>
		///                 A spread angle of 90 makes the left part of the stereo sound place itself at 45 degrees to the left
		///                 and the right part 45 degrees to the right.
		///             </para>
		///         </item>
		///         <item>
		///             <para>
		///                 A spread angle of 180 makes the left part of the stero sound place itself at 90 degrees to the left
		///                 and the right part 90 degrees to the right.
		///             </para>
		///         </item>
		///         <item>
		///             <para>
		///                 A spread angle of 360 makes the stereo sound mono at the opposite speaker location to where the 3D
		///                 emitter should be located (by moving the left part 180 degrees left and the right part 180 degrees
		///                 right). So in this case, behind you when the sound should be in front of you!
		///             </para>
		///         </item>
		///     </list>
		/// </remarks>
		/// <seealso cref="Spread3DChanged" />
		public float Spread3D
		{
			get
			{
				NativeInvoke(FMOD_ChannelGroup_Get3DSpread(this, out var angle));
				return angle;
			}
			set
			{
				NativeInvoke(FMOD_ChannelGroup_Set3DSpread(this, value.Clamp(0.0f, 360.0f)));
				OnSpread3DChanged();
			}
		}

		/// <summary>
		///     Gets or sets the behaviour of a 3D distance filter, whether to enable or disable it, and frequency characteristics.
		/// </summary>
		/// <value>
		///     The distance filter.
		/// </value>
		/// <seealso cref="DistanceFilter3DChanged" />
		/// <seealso cref="SetDistanceFilter" />
		/// <seealso cref="DistanceFilter" />
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
			set => SetDistanceFilter(value.Custom, value.CustomLevel, value.CenterFrequency);
		}

		/// <summary>
		///     Sets a custom rolloff curve to define how audio will attenuate over distance.
		///     <para>Must be used in conjunction with <see cref="Enumerations.Mode.CustomRolloff3D" /> flag to be activated.</para>
		///     <para>
		///         Each <see cref="Vector" /> structure in the array, where <i>X</i> = distance and <i>Y</i> = volume from
		///         <c>0.0</c> to <c>1.0</c>. <i>Z</i> should be set to <c>0.0</c>.
		///     </para>
		/// </summary>
		/// <value>
		///     The custom rolloff <see cref="Vector" /> array.
		/// </value>
		/// <remarks>
		///     <alert class="note">
		///         This function does not duplicate the memory for the points internally. The pointer you pass to <b>FMOD</b> must
		///         remain valid until there is no more use for it. Do not free the memory while in use, or use a local variable
		///         that goes out of scope while in use.
		///     </alert>
		///     <para>Points must be sorted by distance! Passing an unsorted list to FMOD will result in an error.</para>
		///     <para>
		///         Set to <c>null</c> to disable the points. If <see cref="Enumerations.Mode.CustomRolloff3D" /> is set and the
		///         rolloff curve is 0, FMOD will revert to inverse curve rolloff.
		///     </para>
		///     <para>
		///         Values set with <see cref="SetMinMaxDistance" /> are meaningless when
		///         <see cref="Enumerations.Mode.CustomRolloff3D" /> is used, their values are ignored.
		///     </para>
		///     <para>Distances between points are linearly interpolated.</para>
		///     <para>
		///         Note that after the highest distance specified, the volume in the last entry is used from that distance
		///         onwards.
		///     </para>
		///     <para>To define the parameters per sound use <see cref="Sound.CustomRolloff3D" />.</para>
		/// </remarks>
		/// <example>
		///     Here is an example of a custom array of points.
		///     <code language="CSharp">
		/// Vector[] curve = 
		/// {
		/// 	new Vector(0.0f, 1.0f, 0.0f),
		/// 	new Vector(2.0f,  0.2f, 0.0f),
		/// 	new Vector(20.0f, 0.0f, 0.0f )
		/// };
		/// channel.CustomRolloff3D = curve;
		/// </code>
		/// </example>
		/// <seealso cref="SetMinMaxDistance" />
		/// <seealso cref="Vector" />
		/// <seealso cref="Enumerations.Mode.CustomRolloff3D" />
		/// <seealso cref="CustomRolloff3DChanged" />
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
				OnCustomRolloff3DChanged();
			}
		}

		/// <summary>
		///     Gets or sets reverb the occlusion factor manually for when the <b>FMOD</b> geometry engine is not being used.
		///     <para>From <c>0.0</c> (not occluded) to <c>1.0</c> (fully occluded)</para>
		///     <para>Default = <c>0.0</c>.</para>
		/// </summary>
		/// <value>
		///     The reverb occlusion.
		/// </value>
		/// <remarks>
		///     Normally the volume is simply attenuated by the <see cref="DirectOcclusion3D" /> factor however if
		///     <see cref="InitFlags.ChannelLowpass" /> is specified frequency filtering will be used with a very small CPU hit.
		/// </remarks>
		/// <seealso cref="DirectOcclusion3D" />
		/// <seealso cref="Occlusion3DChanged" />
		/// <seealso cref="InitFlags.ChannelLowpass" />
		public float ReverbOcclusion3D
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
				OnOcclusion3DChanged();
			}
		}

		/// <summary>
		///     Gets or sets direct the occlusion factor manually for when the <b>FMOD</b> geometry engine is not being used.
		///     <para>From <c>0.0</c> (not occluded) to <c>1.0</c> (fully occluded)</para>
		///     <para>Default = <c>0.0</c>.</para>
		/// </summary>
		/// <value>
		///     The direct occlusion.
		/// </value>
		/// <remarks>
		///     Normally the volume is simply attenuated by this factor however if <see cref="InitFlags.ChannelLowpass" /> is
		///     specified frequency filtering will be used with a very small CPU hit.
		/// </remarks>
		/// <seealso cref="ReverbOcclusion3D" />
		/// <seealso cref="Occlusion3DChanged" />
		/// <seealso cref="InitFlags.ChannelLowpass" />
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
				OnOcclusion3DChanged();
			}
		}

		#endregion
	}
}