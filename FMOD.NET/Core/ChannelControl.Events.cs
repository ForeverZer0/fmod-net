#region License

// ChannelControl.Events.cs is distributed under the Microsoft Public License (MS-PL)
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
// Created 12:02 AM 02/20/2018

#endregion

#region Using Directives

using System;
using FMOD.NET.Arguments;
using FMOD.NET.Data;
using FMOD.NET.Structures;

#endregion

namespace FMOD.NET.Core
{
	public partial class ChannelControl
	{
		#region Events

		/// <summary>
		///     Occurs when the cone orientation for 3D sound is changed.
		/// </summary>
		/// <seealso cref="ConeOrientation3D" />
		public event EventHandler ConeOrientation3DChanged;

		/// <summary>
		///     Occurs when the cone settings for 3D sound have changed.
		/// </summary>
		/// <seealso cref="ConeSettings3D" />
		/// <seealso cref="SetConeSettings" />
		/// <seealso cref="ConeSettings" />
		public event EventHandler ConeSettings3DChanged;

		/// <summary>
		///     Occurs when the custom rolloff settings for 3D sound have changed.
		/// </summary>
		/// <seealso cref="CustomRolloff3D" />
		/// <seealso cref="Vector" />
		public event EventHandler CustomRolloff3DChanged;

		/// <summary>
		///     Occurs when the delay is changed.
		/// </summary>
		/// <seealso cref="Delay" />
		/// <seealso cref="SetDelay" />
		/// <seealso cref="DspDelay" />
		public event EventHandler DelayChanged;

		/// <summary>
		///     Occurs when the minimum or maximum distance for 3D sound has changed.
		/// </summary>
		/// <seealso cref="MinDistance3D" />
		/// <seealso cref="MaxDistance3D" />
		/// <seealso cref="SetMinMaxDistance" />
		public event EventHandler Distance3DChanged;

		/// <summary>
		///     Occurs when the distance filter for 3D sound has changed.
		/// </summary>
		/// <seealso cref="DistanceFilter3D" />
		public event EventHandler DistanceFilter3DChanged;

		/// <summary>
		///     Occurs when the doppler level for 3D sound has changed.
		/// </summary>
		/// <seealso cref="DopplerLevel3D" />
		public event EventHandler DopplerLevel3DChanged;

		/// <summary>
		///     Occurs when a <see cref="Dsp" /> is added to the DSP chain.
		/// </summary>
		/// <seealso cref="AddDsp(FMOD.NET.Core.Dsp,FMOD.NET.Enumerations.DspIndex)" />
		/// <seealso cref="AddDsp(Dsp, int)" />
		/// <seealso cref="Dsp" />
		public event EventHandler DspAdded;

		/// <summary>
		///     Occurs when a <see cref="Dsp" /> is removed from the DSP chain.
		/// </summary>
		/// <seealso cref="RemoveDsp" />
		/// <seealso cref="RemoveDspAtIndex(int)" />
		/// <seealso cref="RemoveDspAtIndex(FMOD.NET.Enumerations.DspIndex)" />
		/// <seealso cref="Dsp" />
		public event EventHandler DspRemoved;

		/// <summary>
		///     Occurs when a <see cref="FadePoint" /> is added.
		/// </summary>
		/// <seealso cref="AddFadePoints(FadePoint)" />
		/// <seealso cref="AddFadePoints(ulong,float)" />
		/// <seealso cref="FadePoint" />
		public event EventHandler FadePointAdded;

		/// <summary>
		///     Occurs when the <seealso cref="FadePoint" /> is removed.
		/// </summary>
		/// <seealso cref="RemoveFadePoints" />
		/// <seealso cref="FadePoint" />
		public event EventHandler FadePointRemoved;

		/// <summary>
		///     Occurs when the level value for 3D sound has changed.
		/// </summary>
		/// <seealso cref="Level3D" />
		public event EventHandler Level3DChanged;

		/// <summary>
		///     Occurs when the low-pass gain value is changed.
		/// </summary>
		/// <seealso cref="LowPassGain" />
		public event EventHandler LowPassGainChanged;

		/// <summary>
		///     Occurs when the mix matrix is changed.
		/// </summary>
		/// <seealso cref="SetMixMatrix" />
		public event EventHandler MixMatrixChanged;

		/// <summary>
		///     Occurs when the <see cref="Mode" /> is changed.
		/// </summary>
		/// <seealso cref="Enumerations.Mode" />
		/// <seealso cref="Mode" />
		public event EventHandler ModeChanged;

		/// <summary>
		///     Occurs when the mute state is changed.
		/// </summary>
		/// <seealso cref="Muted" />
		/// <seealso cref="Mute" />
		/// <seealso cref="Unmute" />
		public event EventHandler MuteChanged;

		/// <summary>
		///     Occurs when the direct or reverb occlusion for 3D sound has changed.
		/// </summary>
		/// <seealso cref="DirectOcclusion3D" />
		/// <seealso cref="ReverbOcclusion3D" />
		public event EventHandler Occlusion3DChanged;

		/// <summary>
		///     Occurs when the geometry engine has calculated occlusion values.
		/// </summary>
		/// <seealso cref="OcclusionEventArgs" />
		public event EventHandler<OcclusionEventArgs> OcclusionCalculated;

		/// <summary>
		///     Occurs when the pan value is changed.
		/// </summary>
		/// <seealso cref="SetPan" />
		/// <seealso cref="SetMixMatrix" />
		public event EventHandler PanChanged;

		/// <summary>
		///     Occurs when the paused state is changed.
		/// </summary>
		/// <seealso cref="Paused" />
		/// <seealso cref="Pause" />
		/// <seealso cref="Resume" />
		public event EventHandler PauseChanged;

		/// <summary>
		///     Occurs when the pitch is changed.
		/// </summary>
		/// <seealso cref="Pitch" />
		public event EventHandler PitchChanged;

		/// <summary>
		///     Occurs when the position for 3D sound has changed.
		/// </summary>
		/// <seealso cref="Position3D" />
		/// <seealso cref="Vector" />
		public event EventHandler Position3DChanged;

		/// <summary>
		///     Occurs when the reverb properties are changed.
		/// </summary>
		/// <seealso cref="SetReverbProperties" />
		public event EventHandler ReverbChanged;

		/// <summary>
		///     Occurs when a sound has completed playing and ends.
		/// </summary>
		/// <alert class="note">
		///     This event is not fired when a sound is currently looping or stopped via
		///     <see cref="ChannelControl.Stop" />.
		/// </alert>
		/// <seealso cref="ChannelControl.Stop" />
		public event EventHandler SoundEnded;

		/// <summary>
		///     Occurs when the spread value for 3D sound is changed.
		/// </summary>
		/// <seealso cref="Spread3D" />
		public event EventHandler Spread3DChanged;

		/// <summary>
		///     Occurs when the <see cref="ChannelControl" /> playback is stopped.
		/// </summary>
		/// <seealso cref="Stop" />
		public event EventHandler Stopped;

		/// <summary>
		///     Occurs when sync-point is encountered.
		/// </summary>
		/// <seealso cref="SyncPointEncounteredEventArgs" />
		public event EventHandler<SyncPointEncounteredEventArgs> SyncPointEncountered;

		/// <summary>
		///     Occurs when the velocity for 3D sound has changed.
		/// </summary>
		/// <seealso cref="Velocity3D" />
		/// <seealso cref="Vector" />
		public event EventHandler Velocity3DChanged;

		/// <summary>
		///     Occurs when a voice is swapped from emulated to real or from real to emulated.
		/// </summary>
		/// <seealso cref="VoiceSwapEventArgs" />
		public event EventHandler<VoiceSwapEventArgs> VirtualVoiceSwapped;

		/// <summary>
		///     Occurs when the volume has changed.
		/// </summary>
		/// <seealso cref="Volume" />
		public event EventHandler VolumeChanged;

		/// <summary>
		///     Occurs when the volume ramp has changed.
		/// </summary>
		/// <seealso cref="VolumeRamp" />
		public event EventHandler VolumeRampChanged;

		#endregion

		#region Event Invokers

		/// <summary>
		///     Raises the <see cref="ConeOrientation3DChanged" /> event.
		/// </summary>
		/// <seealso cref="EventArgs" />
		protected virtual void OnConeOrientation3DChanged()
		{
			ConeOrientation3DChanged?.Invoke(this, EventArgs.Empty);
		}

		/// <summary>
		///     Raises the <see cref="ConeSettings3DChanged" /> event.
		/// </summary>
		/// <seealso cref="EventArgs" />
		protected virtual void OnConeSettings3DChanged()
		{
			ConeSettings3DChanged?.Invoke(this, EventArgs.Empty);
		}

		/// <summary>
		///     Raises the <see cref="CustomRolloff3DChanged" /> event.
		/// </summary>
		/// <seealso cref="EventArgs" />
		protected virtual void OnCustomRolloff3DChanged()
		{
			CustomRolloff3DChanged?.Invoke(this, EventArgs.Empty);
		}

		/// <summary>
		///     Raises the <see cref="DelayChanged" /> event.
		/// </summary>
		/// <seealso cref="EventArgs" />
		protected virtual void OnDelayChanged()
		{
			DelayChanged?.Invoke(this, EventArgs.Empty);
		}

		/// <summary>
		///     Raises the <see cref="Distance3DChanged" /> event.
		/// </summary>
		/// <seealso cref="EventArgs" />
		protected virtual void OnDistance3DChanged()
		{
			Distance3DChanged?.Invoke(this, EventArgs.Empty);
		}

		/// <summary>
		///     Raises the <see cref="DistanceFilter3DChanged" /> event.
		/// </summary>
		/// <seealso cref="EventArgs" />
		protected virtual void OnDistanceFilter3DChanged()
		{
			DistanceFilter3DChanged?.Invoke(this, EventArgs.Empty);
		}

		/// <summary>
		///     Raises the <see cref="DopplerLevel3DChanged" /> event.
		/// </summary>
		/// <seealso cref="EventArgs" />
		protected virtual void OnDopplerLevel3DChanged()
		{
			DopplerLevel3DChanged?.Invoke(this, EventArgs.Empty);
		}

		/// <summary>
		///     Raises the <see cref="DspAdded" /> event.
		/// </summary>
		/// <seealso cref="EventArgs" />
		protected virtual void OnDspAdded()
		{
			DspAdded?.Invoke(this, EventArgs.Empty);
		}

		/// <summary>
		///     Raises the <see cref="DspRemoved" /> event.
		/// </summary>
		/// <seealso cref="EventArgs" />
		protected virtual void OnDspRemoved()
		{
			DspRemoved?.Invoke(this, EventArgs.Empty);
		}

		/// <summary>
		///     Raises the <see cref="FadePointAdded" /> event.
		/// </summary>
		/// <seealso cref="EventArgs" />
		protected virtual void OnFadePointAdded()
		{
			FadePointAdded?.Invoke(this, EventArgs.Empty);
		}

		/// <summary>
		///     Raises the <see cref="FadePointRemoved" /> event.
		/// </summary>
		/// <seealso cref="EventArgs" />
		protected virtual void OnFadePointRemoved()
		{
			FadePointRemoved?.Invoke(this, EventArgs.Empty);
		}

		/// <summary>
		///     Raises the <see cref="Level3DChanged" /> event.
		/// </summary>
		/// <seealso cref="EventArgs" />
		protected virtual void OnLevel3DChanged()
		{
			Level3DChanged?.Invoke(this, EventArgs.Empty);
		}

		/// <summary>
		///     Raises the <see cref="LowPassGainChanged" /> event.
		/// </summary>
		/// <seealso cref="EventArgs" />
		protected virtual void OnLowPassGainChanged()
		{
			LowPassGainChanged?.Invoke(this, EventArgs.Empty);
		}

		/// <summary>
		///     Raises the <see cref="MixMatrixChanged" /> event.
		/// </summary>
		/// <seealso cref="EventArgs" />
		protected virtual void OnMixMatrixChanged()
		{
			MixMatrixChanged?.Invoke(this, EventArgs.Empty);
		}

		/// <summary>
		///     Raises the <see cref="ModeChanged" /> event.
		/// </summary>
		/// <seealso cref="EventArgs" />
		protected virtual void OnModeChanged()
		{
			ModeChanged?.Invoke(this, EventArgs.Empty);
		}

		/// <summary>
		///     Raises the <see cref="MuteChanged" /> event.
		/// </summary>
		/// <seealso cref="EventArgs" />
		protected virtual void OnMuteChanged()
		{
			MuteChanged?.Invoke(this, EventArgs.Empty);
		}

		/// <summary>
		///     Raises the <see cref="Occlusion3DChanged" /> event.
		/// </summary>
		/// <seealso cref="EventArgs" />
		protected virtual void OnOcclusion3DChanged()
		{
			Occlusion3DChanged?.Invoke(this, EventArgs.Empty);
		}

		/// <summary>
		///     Raises the <see cref="OcclusionCalculated" /> event.
		/// </summary>
		/// <param name="e">The <see cref="OcclusionEventArgs" /> instance containing the event data.</param>
		protected virtual void OnOcclusionCalculated(OcclusionEventArgs e)
		{
			OcclusionCalculated?.Invoke(this, e);
		}

		/// <summary>
		///     Raises the <see cref="PanChanged" /> event.
		/// </summary>
		/// <seealso cref="EventArgs" />
		protected virtual void OnPanChanged()
		{
			PanChanged?.Invoke(this, EventArgs.Empty);
		}

		/// <summary>
		///     Raises the <see cref="PauseChanged" /> event.
		/// </summary>
		/// <seealso cref="EventArgs" />
		protected virtual void OnPauseChanged()
		{
			PauseChanged?.Invoke(this, EventArgs.Empty);
		}

		/// <summary>
		///     Raises the <see cref="PitchChanged" /> event.
		/// </summary>
		/// <seealso cref="EventArgs" />
		protected virtual void OnPitchChanged()
		{
			PitchChanged?.Invoke(this, EventArgs.Empty);
		}

		/// <summary>
		///     Raises the <see cref="Position3DChanged" /> event.
		/// </summary>
		/// <seealso cref="EventArgs" />
		protected virtual void OnPosition3DChanged()
		{
			Position3DChanged?.Invoke(this, EventArgs.Empty);
		}

		/// <summary>
		///     Raises the <see cref="ReverbChanged" /> event.
		/// </summary>
		/// <seealso cref="EventArgs" />
		protected virtual void OnReverbChanged()
		{
			ReverbChanged?.Invoke(this, EventArgs.Empty);
		}

		/// <summary>
		///     Raises the <see cref="SoundEnded" /> event.
		/// </summary>
		/// <seealso cref="EventArgs" />
		protected virtual void OnSoundEnded()
		{
			SoundEnded?.Invoke(this, EventArgs.Empty);
		}

		/// <summary>
		///     Raises the <see cref="Spread3DChanged" /> event.
		/// </summary>
		/// <seealso cref="EventArgs" />
		protected virtual void OnSpread3DChanged()
		{
			Spread3DChanged?.Invoke(this, EventArgs.Empty);
		}

		/// <summary>
		///     Raises the <see cref="Stopped" /> event.
		/// </summary>
		/// <seealso cref="EventArgs" />
		protected virtual void OnStopped()
		{
			Stopped?.Invoke(this, EventArgs.Empty);
		}

		/// <summary>
		///     Raises the <see cref="SyncPointEncountered" /> event.
		/// </summary>
		/// <param name="e">The <see cref="SyncPointEncounteredEventArgs" /> instance containing the event data.</param>
		protected virtual void OnSyncPointEncountered(SyncPointEncounteredEventArgs e)
		{
			SyncPointEncountered?.Invoke(this, e);
		}

		/// <summary>
		///     Raises the <see cref="Velocity3DChanged" /> event.
		/// </summary>
		/// <seealso cref="EventArgs" />
		protected virtual void OnVelocity3DChanged()
		{
			Velocity3DChanged?.Invoke(this, EventArgs.Empty);
		}

		/// <summary>
		///     Raises the <see cref="VirtualVoiceSwapped" /> event.
		/// </summary>
		/// <param name="e">The <see cref="VoiceSwapEventArgs" /> instance containing the event data.</param>
		protected virtual void OnVirtualVoiceSwapped(VoiceSwapEventArgs e)
		{
			VirtualVoiceSwapped?.Invoke(this, e);
		}

		/// <summary>
		///     Raises the <see cref="VolumeChanged" /> event.
		/// </summary>
		/// <seealso cref="EventArgs" />
		protected virtual void OnVolumeChanged()
		{
			VolumeChanged?.Invoke(this, EventArgs.Empty);
		}

		/// <summary>
		///     Raises the <see cref="VolumeRampChanged" /> event.
		/// </summary>
		/// <seealso cref="EventArgs" />
		protected virtual void OnVolumeRampChanged()
		{
			VolumeRampChanged?.Invoke(this, EventArgs.Empty);
		}

		#endregion
	}
}