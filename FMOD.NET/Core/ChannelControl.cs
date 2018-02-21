#region License

// ChannelControl.cs is distributed under the Microsoft Public License (MS-PL)
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
// Created 9:49 PM 02/15/2018

#endregion

#region Using Directives

using System;
using System.Runtime.InteropServices;
using FMOD.NET.Arguments;
using FMOD.NET.Data;
using FMOD.NET.Enumerations;
using FMOD.NET.Structures;

#endregion

namespace FMOD.NET.Core
{
	/// <inheritdoc />
	/// <summary>
	///     <para>The base class for both <see cref="Channel" /> and <see cref="ChannelGroup" />.</para>
	///     <para>This class must be inherited.</para>
	/// </summary>
	/// <remarks>
	///     Internally, <see cref="Channel" /> and <see cref="ChannelControl" /> objects differ from the other primary classes
	///     that inherit <see cref="HandleBase" />.
	///     <para>
	///         They are not technically "released" or "disposed", but are reused automatically as needed by <b>FMOD</b>.
	///         Because of this, extra care must be taken that the any instance you are using is still valid, as it is possible
	///         to maintain a reference to a <see cref="Channel" />, while the underlying handle it wraps is no longer valid
	///         and has been used elsewhere. There are two common circumstances that may cause a reference to Channel to be
	///         invalid:
	///     </para>
	///     <list type="ordered">
	///         <item>
	///             <para>
	///                 The sound has completed playing. Once a sound is done playing, the channel is fair-game to be taken
	///                 and used elsewhere by <b>FMOD</b>, rendering he current handle invalid.
	///             </para>
	///         </item>
	///         <item>
	///             <para>
	///                 The maximum number of "real" channels (See <see cref="F:FMOD.NET.Core.Constants.MAX_CHANNELS" />) is being used, and
	///                 a sound or channel with a higher priority is required and the channel is stolen.
	///             </para>
	///         </item>
	///     </list>
	///     <para>
	///         <b>FMOD.NET</b> automatically marks channels invalid that have become so due to a sound ending, so a quick
	///         check of <see cref="System.Runtime.InteropServices.SafeHandle.IsInvalid" /> will allow you to recognize if your reference is still valid.
	///         Invoking native wrapped functions on an invalid channel will throw an <see cref="FmodException" />.
	///     </para>
	/// </remarks>
	/// <seealso cref="HandleBase" />
	/// <seealso cref="HandleBase" />
	/// <seealso cref="Channel" />
	/// <seealso cref="ChannelGroup" />
	public abstract partial class ChannelControl : HandleBase
	{
		/// <summary>
		///     It is necessary to keep a reference (non-local variable) to the callback, otherwise it
		///     gets garbage-collected and will throw an exception when the callback is invoked by FMOD.
		/// </summary>
		// ReSharper disable once PrivateFieldCanBeConvertedToLocalVariable
		private readonly ChannelCallback _channelCallbackDelegate;

		#region Constructors

		/// <inheritdoc />
		/// <summary>
		///     Initializes a new instance of the <see cref="ChannelControl" /> class.
		/// </summary>
		/// <param name="handle">The handle.</param>
		protected ChannelControl(IntPtr handle) : base(handle)
		{
			_channelCallbackDelegate = OnChannelCallback;
			NativeInvoke(FMOD_ChannelGroup_SetCallback(this, _channelCallbackDelegate));
		}

		#endregion

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
		/// <seealso cref="O:FMOD.Core.ChannelControl.AddDsp" />
		/// <seealso cref="O:FMOD.Core.ChannelControl.GetDsp" />
		/// <seealso cref="RemoveDsp" />
		/// <seealso cref="O:FMOD.Core.ChannelControl.RemoveDspAtIndex" />
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
		///         When changing the loop mode, sounds created with <see cref="O:FMOD.Core.FmodSystem.CreateStream" /> or
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
		///         When changing the loop mode of sounds created with with <see cref="O:FMOD.Core.FmodSystem.CreateSound" /> or
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
		/// <seealso cref="O:FMOD.Core.FmodSystem.CreateSound" />
		/// <seealso cref="O:FMOD.Core.FmodSystem.CreateStream" />
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
		///     Gets or sets the user data to be stored within the <see cref="ChannelControl" />.
		/// </summary>
		/// <value>
		///     The user data.
		/// </value>
		/// <remarks>
		///     You can use this to store a pointer to any wrapper or internal class that is associated with this object. This
		///     is especially useful for callbacks where you need to get back access to your own objects.
		/// </remarks>
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
				OnUserDataChanged();
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

		#region Methods

		/// <summary>
		///     Add a pre-created DSP unit to the specified index in the DSP chain.
		/// </summary>
		/// <param name="dsp">A pre-created DSP unit to be inserted at the specified offset.</param>
		/// <param name="index">
		///     <para>Offset to add this DSP unit at in the DSP chain using one of the special named offsets.</para>
		/// </param>
		/// <seealso cref="AddDsp(Dsp, int)" />
		/// <seealso cref="DspIndex" />
		/// <seealso cref="DspAdded" />
		/// <seealso cref="RemoveDsp" />
		/// <seealso cref="GetDsp(int)" />
		/// <seealso cref="GetDsp(DspIndex)" />
		/// <seealso cref="Dsp" />
		/// <seealso cref="FmodSystem.CreateDsp" />
		/// <seealso cref="FmodSystem.CreateDspByType" />
		/// <seealso cref="FmodSystem.CreateDspByType{Type}" />
		/// <seealso cref="FmodSystem.CreateDspByPlugin" />
		public void AddDsp(Dsp dsp, DspIndex index)
		{
			AddDsp(dsp, (int) index);
		}

		/// <summary>
		///     Add a pre-created DSP unit to the specified index in the DSP chain.
		/// </summary>
		/// <param name="dsp">A pre-created DSP unit to be inserted at the specified offset.</param>
		/// <param name="index">
		///     <para>Offset to add this DSP unit at in the DSP chain.</para>
		///     <para>See <see cref="DspIndex" /> for special named offsets.</para>
		/// </param>
		/// <seealso cref="AddDsp(Dsp, int)" />
		/// <seealso cref="DspIndex" />
		/// <seealso cref="DspAdded" />
		/// <seealso cref="RemoveDsp" />
		/// <seealso cref="GetDsp(int)" />
		/// <seealso cref="GetDsp(DspIndex)" />
		/// <seealso cref="Dsp" />
		/// <seealso cref="FmodSystem.CreateDsp" />
		/// <seealso cref="FmodSystem.CreateDspByType" />
		/// <seealso cref="FmodSystem.CreateDspByType{Type}" />
		/// <seealso cref="FmodSystem.CreateDspByPlugin" />
		public void AddDsp(Dsp dsp, int index)
		{
			NativeInvoke(FMOD_ChannelGroup_AddDSP(this, index, dsp));
			OnDspAdded();
		}

		/// <summary>
		///     Add a volume point to fade from or towards, using a clock offset and 0 to 1 volume level.
		/// </summary>
		/// <param name="fadePoint">A <see cref="FadePoint" /> object describing the parameters of the fade.</param>
		/// <remarks>
		///     <para>
		///         For every fade point, <b>FMOD</b> will do a per sample volume ramp between them.<lineBreak />
		///         It will scale with the current <see cref="Channel" /> or <see cref="ChannelGroup" />'s volume.
		///     </para>
		///     <example>
		///         <code language="CSharp" numberLines="true">
		/// // Ramp from full volume to half volume over the next 4096 samples
		/// ulong clock = channel.DspHeadClock;
		/// channel.AddFadePoints(clock, 1.0f);
		/// channel.AddFadePoints(clock + 4096, 0.5f);
		/// </code>
		///     </example>
		/// </remarks>
		/// <see cref="FadePoint" />
		/// <see cref="FadePointAdded" />
		public void AddFadePoints(FadePoint fadePoint)
		{
			AddFadePoints(fadePoint.DspClock, fadePoint.Volume);
		}

		/// <summary>
		///     Add a volume point to fade from or towards, using a clock offset and 0 to 1 volume level.
		/// </summary>
		/// <param name="dspClock">DSP clock of the parent <see cref="ChannelGroup" /> to set the fade point volume.</param>
		/// <param name="targetVolume">
		///     <para>Volume level where <c>0.0</c> is silent and <c>1.0</c> is normal volume.</para>
		///     <para>Amplification above <c>1.0</c> is supported.</para>
		/// </param>
		/// <remarks>
		///     <para>
		///         For every fade point, <b>FMOD</b> will do a per sample volume ramp between them.<lineBreak />
		///         It will scale with the current <see cref="Channel" /> or <see cref="ChannelGroup" />'s volume.
		///     </para>
		///     <example>
		///         <code language="CSharp" numberLines="true">
		/// // Ramp from full volume to half volume over the next 4096 samples
		/// ulong clock = channel.DspHeadClock;
		/// channel.AddFadePoints(clock, 1.0f);
		/// channel.AddFadePoints(clock + 4096, 0.5f);
		/// </code>
		///     </example>
		/// </remarks>
		/// <see cref="FadePoint" />
		/// <see cref="FadePointAdded" />
		public void AddFadePoints(ulong dspClock, float targetVolume)
		{
			NativeInvoke(FMOD_ChannelGroup_AddFadePoint(this, dspClock, targetVolume));
			OnFadePointAdded();
		}

		// TODO: Implement adding fadepoint via value and TimeUnit, not just samples

		/// <summary>
		///     Gets the combined volume after 3D spatialization and geometry occlusion calculations including any volumes set via
		///     the API.
		/// </summary>
		/// <returns>The calculated volume.</returns>
		/// <remarks>
		///     This does not represent the waveform, just the calculated result of all volume modifiers. This value is used by the
		///     virtual channel system to order its channels between real and virtual.
		/// </remarks>
		/// <seealso cref="Channel.IsVirtual" />
		/// <seealso cref="Volume" />
		/// <seealso cref="ReverbOcclusion3D" />
		/// <seealso cref="Position3D" />
		/// <seealso cref="Velocity3D" />
		/// <seealso cref="DspParameterOverallGain" />
		public float GetAudibility()
		{
			NativeInvoke(FMOD_ChannelGroup_GetAudibility(this, out var audibility));
			return audibility;
		}

		/// <summary>
		///     Retrieve the DSP unit at the specified index.
		/// </summary>
		/// <param name="index">
		///     <para>Offset into the DSP chain.</para>
		/// </param>
		/// <returns>The requested DSP unit.</returns>
		/// <seealso cref="AddDsp(Dsp, int)" />
		/// <seealso cref="AddDsp(Dsp, DspIndex)" />
		/// <seealso cref="GetDsp(int)" />
		/// <seealso cref="RemoveDsp" />
		/// <seealso cref="DspCount" />
		/// <seealso cref="DspIndex" />
		public Dsp GetDsp(DspIndex index)
		{
			return GetDsp((int) index);
		}

		/// <summary>
		///     Retrieve the DSP unit at the specified index.
		/// </summary>
		/// <param name="index">
		///     <para>Offset into the DSP chain.</para>
		///     <para>See <see cref="DspIndex" /> for special named offsets.</para>
		/// </param>
		/// <returns>The requested DSP unit.</returns>
		/// <seealso cref="AddDsp(Dsp, int)" />
		/// <seealso cref="AddDsp(Dsp, DspIndex)" />
		/// <seealso cref="GetDsp(DspIndex)" />
		/// <seealso cref="RemoveDsp" />
		/// <seealso cref="DspCount" />
		/// <seealso cref="DspIndex" />
		public Dsp GetDsp(int index)
		{
			NativeInvoke(FMOD_ChannelGroup_GetDSP(this, index, out var dsp));
			return Factory.Create<Dsp>(dsp);
		}

		/// <summary>
		///     Retrieves the DSP clock values which count up by the number of samples per second in the software mixer, i.e. if
		///     the default sample rate is 48KHz, the DSP clock increments by 48000 per second.
		/// </summary>
		/// <param name="head">The DSP clock value for the head DSP node.</param>
		/// <param name="tail">The DSP clock value for the tail DSP node.</param>
		/// <remarks>
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
		/// <seealso cref="DspTailClock" />
		/// <seealso cref="Delay" />
		/// <seealso cref="DspDelay" />
		/// <seealso cref="SetDelay" />
		public void GetDspClocks(out ulong head, out ulong tail)
		{
			NativeInvoke(FMOD_ChannelGroup_GetDSPClock(this, out head, out tail));
		}

		/// <summary>
		///     Retrieves the index in the DSP chain of the provided DSP.
		/// </summary>
		/// <param name="dsp">An existing <see cref="Dsp" /> in the DSP chain.</param>
		/// <returns>The offset in the DSP chain of the specified DSP.</returns>
		/// <seealso cref="SetDspIndex(Dsp, DspIndex)" />
		/// <seealso cref="SetDspIndex(Dsp, int)" />
		public int GetDspIndex(Dsp dsp)
		{
			NativeInvoke(FMOD_ChannelGroup_GetDSPIndex(this, dsp, out var index));
			return index;
		}

		/// <summary>
		///     Retrieve the <see cref="FadePoint" /> instances stored within a <see cref="Channel" /> or
		///     <see cref="ChannelGroup" />.
		/// </summary>
		/// <seealso cref="FadePoint" />
		/// <seealso cref="AddFadePoints(FadePoint)" />
		/// <seealso cref="AddFadePoints(ulong,float)" />
		/// <seealso cref="RemoveFadePoints" />
		/// <seealso cref="FadePointCount" />
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

		/// <summary>
		///     Retrieves a 2D pan matrix that maps input channels (columns) to output speakers (rows).
		/// </summary>
		/// <param name="matrix">
		///     An array of volume levels in row-major order. Each row represents an output speaker, each column
		///     represents an input channel.
		/// </param>
		/// <param name="outChannels">A variable to receive the number of output channels (rows) in the matrix being passed in.</param>
		/// <param name="inChannels">A variable to receive the number of input channels (columns) in the matrix being passed in.</param>
		/// <param name="matrixHop">
		///     <para>The width (total number of columns) of the matrix.</para>
		///     <para>
		///         Optional. If this is <c>0</c>, <paramref name="inChannels" /> will be taken as the width of the matrix.
		///         Maximum of <see cref="Constants.MAX_CHANNELS" />.
		///     </para>
		/// </param>
		/// <remarks>
		///     <para>The gain for input channel "s" to output channel "t" is <c>matrix[t * matrixHop + s]</c>.</para>
		///     <para>
		///         Levels can be below <c>0.0</c> to invert a signal and above <c>1.0</c> to amplify the signal. Note that
		///         increasing the signal level too far may cause audible distortion.
		///     </para>
		///     <para>
		///         The matrix size will generally be the size of the number of channels in the current speaker mode. Use
		///         <see cref="FmodSystem.SoftwareFormat" /> to determine this.
		///     </para>
		///     <para>
		///         Passing <c>null</c> for <paramref name="matrix" /> will allow you to query <paramref name="outChannels" />
		///         and <paramref name="inChannels" /> without copying any data.
		///     </para>
		/// </remarks>
		/// <seealso cref="SetMixMatrix" />
		/// <seealso cref="SetPan" />
		/// <seealso cref="FmodSystem.SoftwareFormat" />
		public void GetMixMatrix(float[] matrix, out int outChannels, out int inChannels, int matrixHop)
		{
			NativeInvoke(FMOD_ChannelGroup_GetMixMatrix(this, matrix, out outChannels, out inChannels, matrixHop));
		}

		/// <summary>
		///     Retrieves the wet level (or send level) for a particular reverb instance.
		/// </summary>
		/// <param name="reverbIndex">
		///     Index of the particular reverb instance to target, from <c>0</c> to
		///     <see cref="Constants.MAX_REVERBS" /> inclusive.
		/// </param>
		/// <returns>The send level for the signal to the reverb, from <c>0.0</c> (none) to <c>1.0</c> (full).</returns>
		/// <seealso cref="Reverb" />
		/// <seealso cref="ReverbProperties" />
		public float GetReverbProperties(int reverbIndex)
		{
			NativeInvoke(FMOD_ChannelGroup_GetReverbProperties(this, reverbIndex, out var wet));
			return wet;
		}

		/// <summary>
		///     <para>Sets the mute state, effectively silencing it.</para>
		///     <para>Same as <c>channel.Muted = true;</c></para>
		/// </summary>
		/// <value>
		///     <c>true</c> if muted; otherwise, <c>false</c>.
		/// </value>
		/// <remarks>
		///     Each <see cref="Channel" /> and <see cref="ChannelGroup" /> has its own mute state, muting a channel group will
		///     mute all child channels but will not affect their individual setting.
		/// </remarks>
		/// <seealso cref="Muted" />
		/// <seealso cref="Unmute" />
		/// <seealso cref="MuteChanged" />
		public void Mute()
		{
			Muted = true;
		}

		/// <summary>
		///     Pauses this <see cref="ChannelControl" /> instance.
		///     <para>Same as <c>channel.Paused = true;</c></para>
		///     .
		/// </summary>
		/// <remarks>
		///     Each <see cref="Channel" /> and <see cref="ChannelGroup" /> has its own paused state, pausing a channel group
		///     will pause all contained channels but will not affect their individual setting.
		/// </remarks>
		/// <seealso cref="Paused" />
		/// <seealso cref="Resume" />
		/// <seealso cref="PauseChanged" />
		public void Pause()
		{
			Paused = true;
		}

		/// <summary>
		///     Remove a particular DSP unit from the DSP chain.
		/// </summary>
		/// <param name="dsp">A DSP unit (that exists in the DSP chain) you wish to remove.</param>
		/// <seealso cref="DspRemoved" />
		/// <seealso cref="AddDsp(Dsp, DspIndex)" />
		/// <seealso cref="AddDsp(Dsp, int)" />
		/// <seealso cref="GetDsp(DspIndex)" />
		/// <seealso cref="GetDsp(int)" />
		/// <seealso cref="DspCount" />
		public void RemoveDsp(Dsp dsp)
		{
			NativeInvoke(FMOD_ChannelGroup_RemoveDSP(this, dsp));
			OnDspRemoved();
		}

		/// <summary>
		///     Remove a DSP unit at a  particular index from the DSP chain.
		/// </summary>
		/// <param name="index">
		///     <para>An index of DSP unit (that exists in the DSP chain) you wish to remove.</para>
		///     <para>See <see cref="DspIndex" /> for special named offsets.</para>
		/// </param>
		/// <seealso cref="AddDsp(Dsp, DspIndex)" />
		/// <seealso cref="AddDsp(Dsp, int)" />
		/// <seealso cref="GetDsp(DspIndex)" />
		/// <seealso cref="GetDsp(int)" />
		/// <seealso cref="DspCount" />
		/// <seealso cref="RemoveDsp" />
		/// <seealso cref="RemoveDspAtIndex(DspIndex)" />
		/// <seealso cref="DspRemoved" />
		public void RemoveDspAtIndex(int index)
		{
			if (index >= DspCount)
				throw new IndexOutOfRangeException();
			RemoveDsp(GetDsp(index));
		}

		/// <summary>
		///     Remove a DSP unit at a  particular index from the DSP chain.
		/// </summary>
		/// <param name="index">
		///     <para>An index of DSP unit (that exists in the DSP chain) you wish to remove.</para>
		/// </param>
		/// <seealso cref="AddDsp(Dsp, DspIndex)" />
		/// <seealso cref="AddDsp(Dsp, int)" />
		/// <seealso cref="GetDsp(DspIndex)" />
		/// <seealso cref="GetDsp(int)" />
		/// <seealso cref="DspCount" />
		/// <seealso cref="RemoveDsp" />
		/// <seealso cref="RemoveDspAtIndex(int)" />
		/// <seealso cref="DspRemoved" />
		public void RemoveDspAtIndex(DspIndex index)
		{
			RemoveDspAtIndex((int) index);
		}

		/// <summary>
		///     <para>Remove volume fade points on the timeline. </para>
		///     <para>
		///         This function will remove multiple fade points with a single call if the points lay between the 2 specified
		///         clock values (inclusive).
		///     </para>
		/// </summary>
		/// <param name="dspClockStart">DSP clock of the parent channel group to start removing fade points from.</param>
		/// <param name="dspClockEnd">DSP clock of the parent channel group to start removing fade points to.</param>
		/// <seealso cref="FadePoint" />
		/// <seealso cref="AddFadePoints(FadePoint)" />
		/// <seealso cref="AddFadePoints(ulong,float)" />
		/// <seealso cref="GetFadePoints" />
		/// <seealso cref="FadePointCount" />
		public void RemoveFadePoints(ulong dspClockStart, ulong dspClockEnd)
		{
			NativeInvoke(FMOD_ChannelGroup_RemoveFadePoints(this, dspClockStart, dspClockEnd));
			OnFadePointRemoved();
		}

		/// <summary>
		///     Resumes this <see cref="ChannelControl" /> instance.
		///     <para>Same as <c>channel.Paused = false;</c></para>
		///     .
		/// </summary>
		/// <remarks>
		///     Each <see cref="Channel" /> and <see cref="ChannelGroup" /> has its own paused state, pausing a channel group
		///     will pause all contained channels but will not affect their individual setting.
		/// </remarks>
		/// <seealso cref="Paused" />
		/// <seealso cref="Pause" />
		/// <seealso cref="PauseChanged" />
		public void Resume()
		{
			Paused = false;
		}

		/// <summary>
		///     Sets the position and velocity used to apply panning, attenuation and doppler.
		/// </summary>
		/// <param name="position">Position in 3D space used for panning and attenuation.</param>
		/// <param name="velocity">Velocity in "distance units per second" (see remarks) in 3D space.</param>
		/// <remarks>
		///     <para>
		///         A "distance unit" is specified by <see cref="FmodSystem.Set3DSettings" />. By default this is set to meters
		///         which is a distance scale of <c>1.0</c>.
		///     </para>
		///     <para>
		///         For a stereo 3D sound, you can set the spread of the left/right parts in speaker space by using
		///         <see cref="Spread3D" />.
		///     </para>
		/// </remarks>
		/// <seealso cref="Position3DChanged" />
		/// <seealso cref="Velocity3DChanged" />
		/// <seealso cref="Vector" />
		/// <seealso cref="Position3D" />
		/// <seealso cref="Velocity3D" />
		/// <seealso cref="FmodSystem.Set3DSettings" />
		/// <seealso cref="Spread3D" />
		public void SetAttributes3D(Vector position, Vector velocity)
		{
			NativeInvoke(FMOD_ChannelGroup_Set3DAttributes(this, ref position, ref velocity, IntPtr.Zero));
			OnPosition3DChanged();
			OnVelocity3DChanged();
		}

		// TODO
		public void SetCallback(ChannelCallback callback)
		{
			NativeInvoke(FMOD_ChannelGroup_SetCallback(this, callback));
		}

		public void SetConeSettings(float insideAngle, float outsideAngle, float outsideVolume)
		{
			insideAngle = insideAngle.Clamp(0.0f, outsideAngle);
			outsideAngle = outsideAngle.Clamp(insideAngle, 360.0f);
			outsideVolume = outsideVolume.Clamp(0.0f, 1.0f);
			NativeInvoke(FMOD_ChannelGroup_Set3DConeSettings(this, insideAngle, outsideAngle, outsideVolume));
			OnConeSettings3DChanged();
		}

		/// <summary>
		///     Sets a start (and/or stop) time relative to the parent channel group DSP clock, with sample accuracy.
		/// </summary>
		/// <param name="dspClockStart">
		///     DSP clock of the parent channel group to audibly start playing sound at, a value of 0
		///     indicates no delay.
		/// </param>
		/// <param name="dspClockEnd">
		///     DSP clock of the parent channel group to audibly stop playing sound at, a value of 0
		///     indicates no delay.
		/// </param>
		/// <param name="stopChannels">
		///     <para><c>true</c> = stop according to <see cref="IsPlaying" />.</para>
		///     <para><c>false</c> = remain "active" and a new start delay could start playback again at a later time.</para>
		/// </param>
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
		/// <seealso cref="Delay" />
		/// <seealso cref="DspDelay" />
		/// <seealso cref="IsPlaying" />
		/// <seealso cref="DelayChanged" />
		/// <seealso cref="DspHeadClock" />
		/// <seealso cref="DspTailClock" />
		/// <seealso cref="GetDspClocks" />
		public void SetDelay(ulong dspClockStart, ulong dspClockEnd, bool stopChannels = false)
		{
			var start = Math.Min(dspClockStart, dspClockEnd - 1);
			var end = Math.Max(dspClockStart + 1, dspClockEnd);
			NativeInvoke(FMOD_ChannelGroup_SetDelay(this, start, end, stopChannels));
			OnDelayChanged();
		}

		/// <summary>
		///     Control the behaviour of a 3D distance filter, whether to enable or disable it, and frequency characteristics.
		/// </summary>
		/// <param name="custom">
		///     <para>Specify true to disable FMOD distance rolloff calculation. </para>
		///     <para>Default = <c>false</c>.</para>
		/// </param>
		/// <param name="customLevel">
		///     <para>
		///         Specify a attenuation factor manually here, where <c>1.0</c> = no attenuation and <c>0.0</c> = complete
		///         attenuation.
		///     </para>
		///     <para>Default = <c>1.0</c>.</para>
		/// </param>
		/// <param name="centerFrequency">
		///     <para>
		///         Specify a center frequency in Hz for the high-pass filter used to simulate distance attenuation, from
		///         <c>10.0</c> to <c>22050.0</c>.
		///     </para>
		///     <para>Default = <c>1500.0</c></para>
		/// </param>
		/// <seealso cref="DistanceFilter3DChanged" />
		/// <seealso cref="DistanceFilter3D" />
		/// <seealso cref="DistanceFilter" />
		public void SetDistanceFilter(bool custom, float customLevel, float centerFrequency)
		{
			NativeInvoke(FMOD_ChannelGroup_Set3DDistanceFilter(this, custom, customLevel.Clamp(0.0f, 1.0f),
				centerFrequency.Clamp(10.0f, 22050.0f)));
			OnDistanceFilter3DChanged();
		}

		/// <summary>
		///     Moves the position in the DSP chain of a specified DSP unit.
		/// </summary>
		/// <param name="dsp">A DSP unit that exists in the DSP chain.</param>
		/// <param name="index">
		///     <para>Offset in the DSP chain to move the DSP to.</para>
		/// </param>
		/// <remarks>
		///     <para>
		///         This function is useful for reordering DSP units inside a <see cref="Channel" /> or
		///         <see cref="ChannelGroup" /> so that processing can happen in the desired order.
		///     </para>
		///     <para>
		///         You can verify the order of the DSP chain using iteration via <see cref="DspCount" /> and
		///         <see cref="GetDsp(int)" /> or with the FMOD Profiler tool.
		///     </para>
		/// </remarks>
		/// <seealso cref="GetDspIndex" />
		/// <seealso cref="GetDsp(int)" />
		/// <seealso cref="GetDsp(DspIndex)" />
		/// <seealso cref="DspCount" />
		/// <seealso cref="DspIndex" />
		public void SetDspIndex(Dsp dsp, DspIndex index)
		{
			NativeInvoke(FMOD_ChannelGroup_SetDSPIndex(this, dsp, index));
		}

		/// <summary>
		///     Moves the position in the DSP chain of a specified DSP unit.
		/// </summary>
		/// <param name="dsp">A DSP unit that exists in the DSP chain.</param>
		/// <param name="index">
		///     <para>Offset in the DSP chain to move the DSP to.</para>
		///     <para>see <see cref="DspIndex" /> for special named offsets.</para>
		/// </param>
		/// <remarks>
		///     <para>
		///         This function is useful for reordering DSP units inside a <see cref="Channel" /> or
		///         <see cref="ChannelGroup" /> so that processing can happen in the desired order.
		///     </para>
		///     <para>
		///         You can verify the order of the DSP chain using iteration via <see cref="DspCount" /> and
		///         <see cref="GetDsp(int)" /> or with the FMOD Profiler tool.
		///     </para>
		/// </remarks>
		/// <seealso cref="GetDspIndex" />
		/// <seealso cref="GetDsp(int)" />
		/// <seealso cref="GetDsp(DspIndex)" />
		/// <seealso cref="DspCount" />
		/// <seealso cref="DspIndex" />
		public void SetDspIndex(Dsp dsp, int index)
		{
			NativeInvoke(FMOD_ChannelGroup_SetDSPIndex(this, dsp, Math.Max(-3, index)));
		}

		/// <summary>
		///     Add a short 64 sample volume ramp to the specified time in the future using fade points.
		/// </summary>
		/// <param name="dspClock">DSP clock of the parent channel group when the volume will be ramped to.</param>
		/// <param name="targetVolume">
		///     Volume level where <c>0.0</c> is silent and <c>1.0</c> is normal volume. Amplification is
		///     supported.
		/// </param>
		/// <remarks>
		///     This is a helper function that automatically ramps from the current fade volume to the newly provided volume at a
		///     specified time. It will clear any fade points set after this time. Use in conjunction with <see cref="SetDelay" />
		///     stop delay, to ramp down volume before it stops. The user would specify the same clock value for the fade ramp and
		///     stop delay. This can also be used as a way to provide sample accurate delayed volume changes without clicks.
		/// </remarks>
		/// <seealso cref="SetFadePointRamp(FadePoint)" />
		/// <seealso cref="SetDelay" />
		/// <seealso cref="RemoveFadePoints" />
		/// <seealso cref="AddFadePoints(FadePoint)" />
		/// <seealso cref="AddFadePoints(ulong, float)" />
		/// <seealso cref="GetFadePoints" />
		public void SetFadePointRamp(ulong dspClock, float targetVolume)
		{
			NativeInvoke(FMOD_ChannelGroup_SetFadePointRamp(this, dspClock, targetVolume));
		}

		/// <summary>
		///     Add a short 64 sample volume ramp to the specified time in the future using fade points.
		/// </summary>
		/// <param name="fadePoint">A <see cref="FadePoint" /> instance describing the point to ramp.</param>
		/// <remarks>
		///     This is a helper function that automatically ramps from the current fade volume to the newly provided volume at a
		///     specified time. It will clear any fade points set after this time. Use in conjunction with <see cref="SetDelay" />
		///     stop delay, to ramp down volume before it stops. The user would specify the same clock value for the fade ramp and
		///     stop delay. This can also be used as a way to provide sample accurate delayed volume changes without clicks.
		/// </remarks>
		/// <seealso cref="SetFadePointRamp(ulong, float)" />
		/// <seealso cref="SetDelay" />
		/// <seealso cref="RemoveFadePoints" />
		/// <seealso cref="AddFadePoints(FadePoint)" />
		/// <seealso cref="AddFadePoints(ulong, float)" />
		/// <seealso cref="GetFadePoints" />
		public void SetFadePointRamp(FadePoint fadePoint)
		{
			NativeInvoke(FMOD_ChannelGroup_SetFadePointRamp(this, fadePoint.DspClock, fadePoint.Volume));
		}

		/// <summary>
		///     Sets the minimum and maximum audible distance.
		/// </summary>
		/// <param name="min">
		///     <para>Minimum volume distance in "units" (see remarks).</para>
		///     <para>default = <c>1.0</c>.</para>
		/// </param>
		/// <param name="max">
		///     <para>Maximum volume distance in "units" (see remarks)</para>
		///     <para>Default = <c>10000.0</c>.</para>
		/// </param>
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
		public void SetMinMaxDistance(float min, float max)
		{
			NativeInvoke(FMOD_ChannelGroup_Set3DMinMaxDistance(this, min, max));
			OnDistance3DChanged();
		}

		/// <summary>
		///     <para>
		///         Sets the incoming volume level for each channel of a multi-channel sound. This is a helper to avoid calling
		///         <see cref="SetMixMatrix" />.
		///     </para>
		///     <para>
		///         A multi-channel sound is a single sound that contains from 1 to 32 channels of sound data, in an interleaved
		///         fashion. If in the extreme case, a 32ch wave file was used, an array of 32 floating point numbers denoting
		///         their volume levels would be passed in to the levels parameter.
		///     </para>
		/// </summary>
		/// <param name="levels">Array of volume levels for each incoming channel.</param>
		/// <remarks>
		///     <para>
		///         An example use case for this function is if the sound file has multiple channels in it with different musical
		///         parts to it, but they are all in sync with each other. This function can be used to fade in and out different
		///         tracks of the sound or to solo/mute tracks within it.
		///     </para>
		///     <alert class="note">
		///         <para>
		///             This function overwrites any pan/output mixlevel by overwriting the <see cref="ChannelControl" />'s
		///             matrix if it exists. It will create an NxN matrix where the output levels are the same as the input levels.
		///             If you wish to fold this down to a lower channel count mix rather than staying at the input channel count,
		///             either create a custom matrix instead and use <see cref="SetMixMatrix" />, or add a new DSP after the
		///             fader, that has a different channel format (ie with <see cref="O:FMOD.Core.ChannelControl.GetDsp" /> and
		///             <see cref="Dsp.ChannelFormat" />).
		///         </para>
		///     </alert>
		///     <alert class="note">
		///         <para>
		///             Levels can be below 0 to invert a signal and above 1 to amplify the signal. Note that increasing the
		///             signal level too far may cause audible distortion
		///         </para>
		///     </alert>
		/// </remarks>
		/// <seealso cref="GetMixMatrix" />
		/// <seealso cref="SetMixMatrix" />
		/// <seealso cref="SetPan" />
		/// <seealso cref="Dsp.ChannelFormat" />
		public void SetMixLevelsInput(float[] levels)
		{
			NativeInvoke(FMOD_ChannelGroup_SetMixLevelsInput(this, levels, levels.Length));
		}

		/// <summary>
		///     <para>Sets the speaker volume levels for each speaker individually.</para>
		///     <para>This is a helper to avoid calling <see cref="SetMixMatrix" />.</para>
		/// </summary>
		/// <param name="frontLeft">
		///     Volume level for the front left speaker of a multichannel speaker setup, <c>0.0</c> (silent),
		///     <c>1.0</c> (normal volume).
		/// </param>
		/// <param name="frontRight">
		///     Volume level for the front right speaker of a multichannel speaker setup, <c>0.0</c> (silent),
		///     <c>1.0</c> (normal volume).
		/// </param>
		/// <param name="center">
		///     Volume level for the center speaker of a multichannel speaker setup, <c>0.0</c> (silent),
		///     <c>1.0</c> (normal volume).
		/// </param>
		/// <param name="lowFreq">
		///     Volume level for the low-frequency speaker of a multichannel speaker setup, <c>0.0</c> (silent),
		///     <c>1.0</c> (normal volume).
		/// </param>
		/// <param name="surroundLeft">
		///     Volume level for the surround left speaker of a multichannel speaker setup, <c>0.0</c>
		///     (silent), <c>1.0</c> (normal volume).
		/// </param>
		/// <param name="surroundRight">
		///     Volume level for the surround right speaker of a multichannel speaker setup, <c>0.0</c>
		///     (silent), <c>1.0</c> (normal volume).
		/// </param>
		/// <param name="backLeft">
		///     Volume level for the back left speaker of a multichannel speaker setup, <c>0.0</c> (silent),
		///     <c>1.0</c> (normal volume).
		/// </param>
		/// <param name="backRight">
		///     Volume level for the back right speaker of a multichannel speaker setup, <c>0.0</c> (silent),
		///     <c>1.0</c> (normal volume).
		/// </param>
		/// <remarks>
		///     <alert class="note">
		///         This function overwrites any pan/mix-level by overwriting the <see cref="ChannelControl" />'s mix-matrix.
		///     </alert>
		///     Levels can be below <c>0.0</c> to invert a signal and above <c>1.0</c> to amplify the signal. Note that increasing
		///     the signal level too far may cause audible distortion. Speakers specified that don't exist will simply be ignored.
		///     For more advanced speaker control, including sending the different channels of a stereo sound to arbitrary
		///     speakers, see <see cref="SetMixMatrix" />.
		/// </remarks>
		/// <seealso cref="GetMixMatrix" />
		/// <seealso cref="SetMixMatrix" />
		/// <seealso cref="SetPan" />
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

		/// <summary>
		///     Sets a 2D pan matrix that maps input channels (columns) to output speakers (rows).
		/// </summary>
		/// <param name="matrix">
		///     An array of volume levels in row-major order. Each row represents an output speaker, each column
		///     represents an input channel.
		/// </param>
		/// <param name="outChannels">
		///     Number of output channels (rows) in the matrix being passed in, from <c>0.0</c> to
		///     <see cref="Constants.MAX_CHANNELS" /> inclusive.
		/// </param>
		/// <param name="inChannels">
		///     Number of input channels (columns) in the matrix being passed in, from <c>0.0</c> to
		///     <see cref="Constants.MAX_CHANNELS" /> inclusive.
		/// </param>
		/// <param name="matrixHop">
		///     <para>The width (total number of columns) of the matrix. </para>
		///     <para>
		///         Optional. If this is <c>0.0</c>, inchannels will be taken as the width of the matrix. Maximum of
		///         <see cref="Constants.MAX_CHANNELS" />.
		///     </para>
		/// </param>
		/// <remarks>
		///     <para>The gain for input channel "s" to output channel "t" is <c>matrix[t * matrixHop + s]</c>.</para>
		///     <para>
		///         Levels can be below <c>0.0</c> to invert a signal and above <c>1.0</c> to amplify the signal. Note that
		///         increasing the signal level too far may cause audible distortion.
		///     </para>
		///     <para>
		///         The matrix size will generally be the size of the number of channels in the current speaker mode. Use
		///         <see cref="FmodSystem.SoftwareFormat" /> to determine this.
		///     </para>
		///     <para>
		///         If a matrix already exists then the matrix passed in will applied over the top of it. The input matrix can be
		///         smaller than the existing matrix.
		///     </para>
		///     <example>
		///         <para>
		///             A "unit" matrix allows a signal to pass through unchanged. For example for a 5.1 matrix a unit matrix
		///             would look like this:
		///         </para>
		///         <code>
		/// [ 1 0 0 0 0 0 ]
		/// [ 0 1 0 0 0 0 ]
		/// [ 0 0 1 0 0 0 ]
		/// [ 0 0 0 1 0 0 ]
		/// [ 0 0 0 0 1 0 ]
		/// [ 0 0 0 0 0 1 ]
		/// </code>
		///     </example>
		/// </remarks>
		/// <seealso cref="GetMixMatrix" />
		/// <seealso cref="SetPan" />
		/// <seealso cref="FmodSystem.SoftwareFormat" />
		/// <seealso cref="SetMixLevelsOutput" />
		public void SetMixMatrix(float[] matrix, int outChannels, int inChannels, int matrixHop)
		{
			NativeInvoke(FMOD_ChannelGroup_SetMixMatrix(this, matrix, outChannels, inChannels, matrixHop));
			OnMixMatrixChanged();
		}

		/// <summary>
		///     Sets the pan level, this is a helper to avoid calling <see cref="SetMixMatrix" />.
		/// </summary>
		/// <param name="pan">Pan level, from <c>-1.0</c> (left) to <c>1.0</c> (right), Default = <c>0.0</c> (center).</param>
		/// <remarks>
		///     <para>
		///         Mono sounds are panned from left to right using constant power panning (non linear fade). This means when pan
		///         = <c>0.0</c>, the balance for the sound in each speaker is 71% left and 71% right, not 50% left and 50% right.
		///         This gives (audibly) smoother pans.
		///     </para>
		///     <para>
		///         Stereo sounds heave each left/right value faded up and down according to the specified pan position. This
		///         means when pan is <c>0.0</c>, the balance for the sound in each speaker is 100% left and 100% right. When pan =
		///         <c>-1.0</c>, only the left channel of the stereo sound is audible, when pan = <c>1.0</c>, only the right
		///         channel of the stereo sound is audible.
		///     </para>
		///     <alert class="note">
		///         <para>Panning does not work if the speaker mode is <see cref="SpeakerMode.Raw" />.</para>
		///     </alert>
		/// </remarks>
		/// <seealso cref="GetMixMatrix" />
		/// <seealso cref="SetMixMatrix" />
		public void SetPan(float pan)
		{
			NativeInvoke(FMOD_ChannelGroup_SetPan(this, pan.Clamp(-1.0f, 1.0f)));
			OnPanChanged();
		}

		/// <summary>
		///     Sets the wet level (or send level) of a particular reverb instance.
		/// </summary>
		/// <param name="reverbIndex">
		///     Index of the particular reverb instance to target, from <c>0</c> to
		///     <see cref="Constants.MAX_REVERBS" /> inclusive.
		/// </param>
		/// <param name="wet">
		///     Send level for the signal to the reverb, from <c>0.0</c> (none) to <c>1.0</c> (full), Default =
		///     <c>1.0</c> for <see cref="Channel" />, <c>0.0</c> for a <see cref="ChannelGroup" />. See remarks.
		/// </param>
		/// <remarks>
		///     <para>
		///         A <see cref="Channel" /> is automatically connected to all existing reverb instances due to the default wet
		///         level of <c>1.0</c>. A ChannelGroup however will not send to any reverb by default requiring an explicit call
		///         to this function.
		///     </para>
		///     <para>
		///         A <see cref="ChannelGroup" /> reverb is optimal for the case where you want to send one mixed signal to the
		///         reverb, rather than a lot of individual channel reverb sends. It is advisable to do this to reduce CPU if you
		///         have many Channels inside a ChannelGroup.
		///     </para>
		///     <para>
		///         Keep in mind when setting a wet level for a ChannelGroup, any Channels under that ChannelGroup will still
		///         have their existing sends to the reverb. To avoid this doubling up you should explicitly set the Channel wet
		///         levels to <c>0.0</c>.
		///     </para>
		/// </remarks>
		/// <seealso cref="Reverb" />
		/// <seealso cref="ReverbProperties" />
		/// <seealso cref="ReverbChanged" />
		public void SetReverbProperties(int reverbIndex, float wet)
		{
			NativeInvoke(FMOD_ChannelGroup_SetReverbProperties(this, reverbIndex, wet));
			OnReverbChanged();
		}

		/// <summary>
		///     <para>Stops the <see cref="Channel" /> (or all channels in the <see cref="ChannelGroup" />) from playing.</para>
		///     <para>Makes it available for re-use by the priority system.</para>
		/// </summary>
		/// <seealso cref="O:FMOD.Core.FmodSystem.PlaySOund" />
		/// <seealso cref="Stopped" />
		public void Stop()
		{
			NativeInvoke(FMOD_ChannelGroup_Stop(this));
			OnStopped();
		}

		/// <summary>
		///     <para>Sets the mute state, returning it to its normal volume.</para>
		///     <para>Same as <c>channel.Muted = false;</c></para>
		/// </summary>
		/// <value>
		///     <c>true</c> if muted; otherwise, <c>false</c>.
		/// </value>
		/// <remarks>
		///     Each <see cref="Channel" /> and <see cref="ChannelGroup" /> has its own mute state, muting a channel group will
		///     mute all child channels but will not affect their individual setting.
		/// </remarks>
		/// <seealso cref="Mute" />
		/// <seealso cref="Muted" />
		/// <seealso cref="MuteChanged" />
		public void Unmute()
		{
			Muted = false;
		}

		/// <summary>
		///     <para>Invoked when a callback is received from the native library.</para>
		///     <para>Routes which functions should be invoke dependent on callback type and derived class.</para>
		/// </summary>
		/// <param name="channelControl">The handle to the channel control.</param>
		/// <param name="controlType">The type that is invoking this function..</param>
		/// <param name="type">The type of callback this is.</param>
		/// <param name="commandData1">Misc. data, dependent on <paramref name="type" /></param>
		/// <param name="commandData2">Misc. data, dependent on <paramref name="type" />.</param>
		/// <returns>The result.</returns>
		/// <seealso cref="Result" />
		/// <seealso cref="ChannelControlType" />
		/// <seealso cref="ChannelControlCallbackType" />
		protected virtual Result OnChannelCallback(IntPtr channelControl, ChannelControlType controlType,
			ChannelControlCallbackType type, IntPtr commandData1, IntPtr commandData2)
		{
			switch (controlType)
			{
				case ChannelControlType.Channel when this is Channel:
				case ChannelControlType.ChannelGroup when this is ChannelGroup:
					switch (type)
					{
						case ChannelControlCallbackType.End:
							OnSoundEnded();
							break;
						case ChannelControlCallbackType.Virtualvoice:
							OnVirtualVoiceSwapped(new VoiceSwapEventArgs(commandData1.ToInt32() == 1));
							break;
						case ChannelControlCallbackType.SyncPoint:
							OnSyncPointEncountered(new SyncPointEncounteredEventArgs(commandData1.ToInt32()));
							break;
						case ChannelControlCallbackType.Occlusion:
							OnOcclusionCalculated(new OcclusionEventArgs(commandData1, commandData2));
							break;
					}
					break;
			}
			return Result.OK;
		}

		#endregion
	}
}