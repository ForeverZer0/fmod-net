#region License

// DspType.cs is distributed under the Microsoft Public License (MS-PL)
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
// Created 1:34 AM 02/04/2018

#endregion

#region Using Directives

using System;
using FMOD.Core;

#endregion

namespace FMOD.Enumerations
{
	/// <summary>
	///     These definitions can be used for creating <b>FMOD</b> defined special effects or DSP units.
	/// </summary>
	/// <remarks>
	///     To get them to be active, first create the unit, then add it somewhere into the DSP network, either at the front of
	///     the network near the soundcard unit to affect the global output, or on a single channel.
	/// </remarks>
	/// <seealso cref="FmodSystem.CreateDspByType" />
	public enum DspType
	{
		/// <summary>
		///     This unit was created via a non <b>FMOD</b> plugin so has an unknown purpose.
		/// </summary>
		Unknown,

		/// <summary>
		///     This unit does nothing but take inputs and mix them together then feed the result to the soundcard unit.
		/// </summary>
		Mixer,

		/// <summary>
		///     This unit generates sine/square/saw/triangle or noise tones.
		/// </summary>
		Oscillator,

		/// <summary>
		///     <para>This unit filters sound using a high quality, resonant lowpass filter algorithm but consumes more CPU time. </para>
		///     <para>
		///         Deprecated and will be removed in a future release (see <see cref="FMOD.DSP.Lowpass" /> remarks for
		///         alternatives).
		///     </para>
		/// </summary>
		[Obsolete("Deprecated and will be removed in a future release.")]
		Lowpass,

		/// <summary>
		///     This unit filters sound using a resonant lowpass filter algorithm that is used in Impulse Tracker, but with limited
		///     cutoff range (0 to 8060hz).
		/// </summary>
		ItLowpass,

		/// <summary>
		///     <para>This unit filters sound using a resonant highpass filter algorithm. </para>
		///     <para>
		///         Deprecated and will be removed in a future release (see <see cref="FMOD.DSP.Highpass" /> remarks for
		///         alternatives).
		///     </para>
		/// </summary>
		[Obsolete("Deprecated and will be removed in a future release.")] 
		Highpass,

		/// <summary>
		///     This unit produces an echo on the sound and fades out at the desired rate.
		/// </summary>
		Echo,

		/// <summary>
		///     This unit pans and scales the volume of a unit.
		/// </summary>
		Fader,

		/// <summary>
		///     This unit produces a flange effect on the sound.
		/// </summary>
		Flange,

		/// <summary>
		///     This unit distorts the sound.
		/// </summary>
		Distortion,

		/// <summary>
		///     This unit normalizes or amplifies the sound to a certain level.
		/// </summary>
		Normalize,

		/// <summary>
		///     This unit limits the sound to a certain level.
		/// </summary>
		Limiter,

		/// <summary>
		///     <para>This unit attenuates or amplifies a selected frequency range</para>
		///     .
		///     <para>
		///         Deprecated and will be removed in a future release (see <see cref="FMOD.DSP.ParamEq" /> remarks for
		///         alternatives).
		///     </para>
		/// </summary>
		[Obsolete("Deprecated and will be removed in a future release.")] 
		ParamEq,

		/// <summary>
		///     This unit bends the pitch of a sound without changing the speed of playback.
		/// </summary>
		PitchShift,

		/// <summary>
		///     This unit produces a chorus effect on the sound.
		/// </summary>
		Chorus,

		/// <summary>
		///     This unit allows the use of Steinberg VST plugins.
		/// </summary>
		VstPlugin,

		/// <summary>
		///     This unit allows the use of Nullsoft Winamp plugins
		/// </summary>
		WinampPlugin,

		/// <summary>
		///     This unit produces an echo on the sound and fades out at the desired rate as is used in Impulse Tracker.
		/// </summary>
		ItEcho,

		/// <summary>
		///     This unit implements dynamic compression (linked/unlinked multichannel, wideband)
		/// </summary>
		Compressor,

		/// <summary>
		///     This unit implements SFX reverb.
		/// </summary>
		SfxReverb,

		/// <summary>
		///     <para>This unit filters sound using a simple lowpass with no resonance, but has flexible cutoff and is fast.</para>
		///     <para>
		///         Deprecated and will be removed in a future release (see <see cref="FMOD.DSP.LowpassSimple" /> remarks for
		///         alternatives).
		///     </para>
		/// </summary>
		[Obsolete("Deprecated and will be removed in a future release.")] 
		LowpassSimple,

		/// <summary>
		///     This unit produces different delays on individual channels of the sound.
		/// </summary>
		Delay,

		/// <summary>
		///     This unit produces a tremolo/chopper effect on the sound.
		/// </summary>
		Tremolo,

		/// <summary>
		///     The ladspa plugin. Unsupported / Deprecated.
		/// </summary>
		[Obsolete("Unsupported / Deprecated.")] 
		LadspaPlugin,

		/// <summary>
		///     This unit sends a copy of the signal to a return DSP anywhere in the DSP tree.
		/// </summary>
		Send,

		/// <summary>
		///     This unit receives signals from a number of send DSPs.
		/// </summary>
		Return,

		/// <summary>
		///     <para>This unit filters sound using a simple highpass with no resonance, but has flexible cutoff and is fast.</para>
		///     <para>
		///         Deprecated and will be removed in a future release (see <see cref="FMOD.DSP.LowpassSimple" /> remarks for
		///         alternatives).
		///     </para>
		/// </summary>
		[Obsolete("Deprecated and will be removed in a future release.")] 
		HighpassSimple,

		/// <summary>
		///     This unit pans the signal, possibly upmixing or downmixing as well.
		/// </summary>
		Pan,

		/// <summary>
		///     This unit is a three-band equalizer.
		/// </summary>
		ThreeEq,

		/// <summary>
		///     This unit simply analyzes the signal and provides spectrum information.
		/// </summary>
		Fft,

		/// <summary>
		///     This unit analyzes the loudness and true peak of the signal.
		/// </summary>
		LoudnessMeter,

		/// <summary>
		///     <para>This unit tracks the envelope of the input/sidechain signal.</para>
		///     <para>Deprecated and will be removed in a future release.</para>
		/// </summary>
		[Obsolete("Deprecated and will be removed in a future release.")] 
		EnvelopeFollower,

		/// <summary>
		///     This unit implements convolution reverb.
		/// </summary>
		ConvolutionReverb,

		/// <summary>
		///     This unit provides per signal channel gain, and output channel mapping to allow 1 multichannel signal made up of
		///     many groups of signals to map to a single output signal.
		/// </summary>
		ChannelMix,

		/// <summary>
		///     This unit 'sends' and 'receives' from a selection of up to 32 different slots. It is like a send/return but it uses
		///     global slots rather than returns as the destination. It also has other features. Multiple transceivers can receive
		///     from a single channel, or multiple transceivers can send to a single channel, or a combination of both.
		/// </summary>
		Transceiver,

		/// <summary>
		///     This unit sends the signal to a 3d object encoder like Dolby Atmos.
		/// </summary>
		ObjectPan,

		/// <summary>
		///     This unit is a flexible five band parametric equalizer.
		/// </summary>
		MultiBandEq,

		/// <summary>
		///     Maximum number of pre-defined DSP types.
		/// </summary>
		Max
	}
}