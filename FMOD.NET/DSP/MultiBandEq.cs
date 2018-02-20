#region License

// MultiBandEq.cs is distributed under the Microsoft Public License (MS-PL)
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
// Created 5:01 PM 02/13/2018

#endregion

#region Using Directives

using System;
using FMOD.Arguments;
using FMOD.Core;

#endregion

namespace FMOD.DSP
{
	/// <inheritdoc />
	/// <summary>
	///     A flexible five band parametric equalizer.
	/// </summary>
	/// <seealso cref="FMOD.Core.Dsp" />
	public class MultiBandEq : Dsp
	{
		#region Events

		/// <summary>
		///     Occurs when the <see cref="Filter" /> is changed for a <see cref="Band" />.
		/// </summary>
		/// <seealso cref="Filter" />
		/// <seealso cref="Band" />
		/// <seealso cref="MultiBandEqFilterEventArgs" />
		/// <seealso cref="SetFilter" />
		public event EventHandler<MultiBandEqFilterEventArgs> FilterChanged;

		/// <summary>
		///     Occurs when the frequency for a <see cref="Band" /> is changed.
		/// </summary>
		/// <seealso cref="Band" />
		/// <seealso cref="MultiBandEqEventArgs" />
		/// <seealso cref="SetFrequency" />
		public event EventHandler<MultiBandEqEventArgs> FrequencyChanged;

		/// <summary>
		///     Occurs when the gain for a <see cref="Band" /> is changed.
		/// </summary>
		/// <seealso cref="Band" />
		/// <seealso cref="MultiBandEqEventArgs" />
		/// <seealso cref="SetGain" />
		public event EventHandler<MultiBandEqEventArgs> GainChanged;

		/// <summary>
		///     Occurs when the quality for a <see cref="Band" /> is changed.
		/// </summary>
		/// <seealso cref="Band" />
		/// <seealso cref="MultiBandEqEventArgs" />
		/// <seealso cref="SetQuality" />
		public event EventHandler<MultiBandEqEventArgs> QualityChanged;

		#endregion

		#region Constructors

		/// <summary>
		///     Initializes a new instance of the <see cref="MultiBandEq" /> class.
		/// </summary>
		/// <param name="handle">The handle.</param>
		protected MultiBandEq(IntPtr handle) : base(handle)
		{
		}

		#endregion

		#region Methods

		/// <summary>
		///     <para>
		///         Gets the <see cref="Filter" /> used to interpret the behavior of the remaining parameters of the specified
		///         <see cref="Band" />.
		///     </para>
		///     <para>Default Band A = <see cref="Filter.Lowpass12dB" />, Default Band B-E = <see cref="Filter.Disabled" /></para>
		/// </summary>
		/// <param name="band">The band to get the filter of.</param>
		/// <returns>The current <see cref="Filter" /> for the specified <see cref="Band" />.</returns>
		public Filter GetFilter(Band band)
		{
			var index = (int) band * 4;
			return (Filter) GetParameterInt(index);
		}

		/// <summary>
		///     <para>Gets the significant frequency in Hz for the specified <see cref="Band" />.</para>
		///     <para>Cutoff if [low/high pass, low/high shelf] <see cref="Filter" />.</para>
		///     <para>Center if [notch, peaking, band-pass] <see cref="Filter" />.</para>
		///     <para>Phase transition point if [all-pass] <see cref="Filter" />.</para>
		///     <para><c>0.0</c> to <c>22000.0</c>. Default = <c>8000.0</c>.</para>
		/// </summary>
		/// <param name="band">The band to get frquency of.</param>
		/// <returns>The frequency.</returns>
		public float GetFrequency(Band band)
		{
			var index = (int) band * 4 + 1;
			return GetParameterFloat(index);
		}

		/// <summary>
		///     <para>Gets the boost or attenuation in dB.</para>
		///     <para>Only if [peaking, high/low shelf only] <see cref="Filter" />.</para>
		///     <para><c>-30.0 </c>to <c>30.0</c>. Default = <c>0.0</c>.</para>
		/// </summary>
		/// <param name="band">The band to get the gain of.</param>
		/// <returns>The gain.</returns>
		public float GetGain(Band band)
		{
			var index = (int) band * 4 + 3;
			return GetParameterFloat(index);
		}

		/// <summary>
		///     <para>Gets the Quality factor.</para>
		///     <para>Resonance if [low/high pass] <see cref="Filter" />.</para>
		///     <para>Bandwidth if [notch, peaking, band-pass] <see cref="Filter" />.</para>
		///     <para>Phase transition sharpness if [all-pass] <see cref="Filter" />.</para>
		///     <para>Unused if [low/high shelf] <see cref="Filter" />.</para>
		///     <para><c>0.1</c> to <c>10.0</c>. Default = <c>0.707</c>.</para>
		/// </summary>
		/// <param name="band">The band to get quality factor to.</param>
		/// <returns>The quality factor.</returns>
		public float GetQuality(Band band)
		{
			var index = (int) band * 4 + 2;
			return GetParameterFloat(index);
		}

		/// <summary>
		///     <para>
		///         Sets the <see cref="Filter" /> used to interpret the behavior of the remaining parameters of the specified
		///         <see cref="Band" />.
		///     </para>
		///     <para>Default Band A = <see cref="Filter.Lowpass12dB" />, Default Band B-E = <see cref="Filter.Disabled" /></para>
		/// </summary>
		/// <param name="band">The band to apply the filter to.</param>
		public void SetFilter(Band band, Filter filter)
		{
			var index = (int) band * 4;
			SetParameterInt(index, (int) filter);
			FilterChanged?.Invoke(this, new MultiBandEqFilterEventArgs(index, band, filter));
		}

		/// <summary>
		///     <para>Sets the significant frequency in Hz for the specified <see cref="Band" />.</para>
		///     <para>Cutoff if [low/high pass, low/high shelf] <see cref="Filter" />.</para>
		///     <para>Center if [notch, peaking, band-pass] <see cref="Filter" />.</para>
		///     <para>Phase transition point if [all-pass] <see cref="Filter" />.</para>
		///     <para><c>0.0</c> to <c>22000.0</c>. Default = <c>8000.0</c>.</para>
		/// </summary>
		/// <param name="band">The band to set frquency of.</param>
		/// <param name="frequency">The frequency.</param>
		public void SetFrequency(Band band, float frequency)
		{
			var index = (int) band * 4 + 1;
			var clamped = frequency.Clamp(20.0f, 22000.0f);
			SetParameterFloat(index, clamped);
			FrequencyChanged?.Invoke(this, new MultiBandEqEventArgs(index, band, clamped, 20.0f, 22000.0f));
		}

		/// <summary>
		///     <para>Sets the boost or attenuation in dB.</para>
		///     <para>Only if [peaking, high/low shelf only] <see cref="Filter" />.</para>
		///     <para><c>-30.0 </c>to <c>30.0</c>. Default = <c>0.0</c>.</para>
		/// </summary>
		/// <param name="band">The band to apply the gain to.</param>
		/// <param name="gain">The gain.</param>
		public void SetGain(Band band, float gain)
		{
			var index = (int) band * 4 + 3;
			var clamped = gain.Clamp(-30.0f, 30.0f);
			SetParameterFloat(index, clamped);
			GainChanged?.Invoke(this, new MultiBandEqEventArgs(index, band, clamped, -30.0f, 30.0f));
		}

		/// <summary>
		///     <para>Sets the Quality factor.</para>
		///     <para>Resonance if [low/high pass] <see cref="Filter" />.</para>
		///     <para>Bandwidth if [notch, peaking, band-pass] <see cref="Filter" />.</para>
		///     <para>Phase transition sharpness if [all-pass] <see cref="Filter" />.</para>
		///     <para>Unused if [low/high shelf] <see cref="Filter" />.</para>
		///     <para><c>0.1</c> to <c>10.0</c>. Default = <c>0.707</c>.</para>
		/// </summary>
		/// <param name="band">The band to apply quality factor to.</param>
		/// <param name="quality">The quality factor.</param>
		public void SetQuality(Band band, float quality)
		{
			var index = (int) band * 4 + 2;
			var clamped = quality.Clamp(0.1f, 10.0f);
			SetParameterFloat(index, clamped);
			QualityChanged?.Invoke(this, new MultiBandEqEventArgs(index, band, clamped, 0.1f, 10.0f));
		}

		#endregion

		/// <summary>
		///     Describes a single band in a multi-band equalizer.
		/// </summary>
		/// <seealso cref="MultiBandEq" />
		public enum Band
		{
			/// <summary>
			///     The A band.
			/// </summary>
			A,

			/// <summary>
			///     The B band.
			/// </summary>
			B,

			/// <summary>
			///     The C band.
			/// </summary>
			C,

			/// <summary>
			///     The D band.
			/// </summary>
			D,

			/// <summary>
			///     The E band.
			/// </summary>
			E
		}

		/// <summary>
		///     Describes various filters to be used within the <see cref="MultiBandEq" />.
		/// </summary>
		/// <seealso cref="MultiBandEq" />
		/// <seealso cref="MultiBandEq.GetFilter" />
		/// <seealso cref="MultiBandEq.SetFilter" />
		public enum Filter
		{
			/// <summary>
			///     Disabled filter, no processing.
			/// </summary>
			Disabled,

			/// <summary>
			///     Resonant low-pass filter, attenuates frequencies (12dB per octave) above a given point (with specificed resonance)
			///     while allowing the rest to pass.
			/// </summary>
			Lowpass12dB,

			/// <summary>
			///     Resonant low-pass filter, attenuates frequencies (24dB per octave) above a given point (with specificed resonance)
			///     while allowing the rest to pass.
			/// </summary>
			Lowpass24dB,

			/// <summary>
			///     Resonant low-pass filter, attenuates frequencies (48dB per octave) above a given point (with specificed resonance)
			///     while allowing the rest to pass.
			/// </summary>
			Lowpass48dB,

			/// <summary>
			///     Resonant low-pass filter, attenuates frequencies (12dB per octave) below a given point (with specificed resonance)
			///     while allowing the rest to pass.
			/// </summary>
			Highpass12dB,

			/// <summary>
			///     Resonant low-pass filter, attenuates frequencies (24dB per octave) below a given point (with specificed resonance)
			///     while allowing the rest to pass.
			/// </summary>
			Highpass24dB,

			/// <summary>
			///     Resonant low-pass filter, attenuates frequencies (48dB per octave) below a given point (with specificed resonance)
			///     while allowing the rest to pass.
			/// </summary>
			Highpass48dB,

			/// <summary>
			///     Low-shelf filter, boosts or attenuates frequencies (with specified gain) below a given point while allowing the
			///     rest to pass.
			/// </summary>
			LowShelf,

			/// <summary>
			///     High-shelf filter, boosts or attenuates frequencies (with specified gain) above a given point while allowing the
			///     rest to pass.
			/// </summary>
			HighShelf,

			/// <summary>
			///     Peaking filter, boosts or attenuates frequencies (with specified gain) at a given point (with specificed bandwidth)
			///     while allowing the rest to pass.
			/// </summary>
			Peaking,

			/// <summary>
			///     Band-pass filter, allows frequencies at a given point (with specificed bandwidth) to pass while attenuating
			///     frequencies outside this range.
			/// </summary>
			BandPass,

			/// <summary>
			///     Notch or band-reject filter, attenuates frequencies at a given point (with specificed bandwidth) while allowing
			///     frequencies outside this range to pass.
			/// </summary>
			Notch,

			/// <summary>
			///     All-pass filter, allows all frequencies to pass, but changes the phase response at a given point (with specified
			///     sharpness).
			/// </summary>
			AllPass
		}
	}
}