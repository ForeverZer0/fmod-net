#region License

// SfxReverb.cs is distributed under the Microsoft Public License (MS-PL)
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
// Created 7:25 PM 02/13/2018

#endregion

#region Using Directives

using System;
using FMOD.NET.Arguments;
using FMOD.NET.Core;
using FMOD.NET.Structures;

#endregion

namespace FMOD.NET.DSP
{
	/// <inheritdoc />
	/// <summary>
	///     Applies a high quality "reverb" effect on a sound.
	/// </summary>
	/// <remarks>
	///     <para>This is a high quality I3DL2 based reverb.</para>
	///     <para>On top of the I3DL2 property set, "Dry Level" is also included to allow the dry mix to be changed.</para>
	/// </remarks>
	/// <seealso cref="Dsp" />
	/// <seealso cref="ReverbProperties" />
	public class SfxReverb : Dsp
	{
		#region Events

		/// <summary>
		///     <para>Occurs when a property is changed.</para>
		///     <para>
		///         When changing the properties all at once with <see cref="SetProperties" />, the
		///         <see cref="FloatParamEventArgs.ParameterIndex" /> will be <c>-1</c> with all other values set to
		///         <c>0.0</c>.
		///     </para>
		/// </summary>
		/// <seealso cref="FloatParamEventArgs" />
		public event EventHandler<FloatParamEventArgs> PropertyChanged;

		#endregion

		#region Constructors

		/// <summary>
		///     Initializes a new instance of the <see cref="SfxReverb" /> class.
		/// </summary>
		/// <param name="handle">The handle.</param>
		protected SfxReverb(IntPtr handle) : base(handle)
		{
		}

		#endregion

		#region Properties

		/// <summary>
		///     <para>Gets or sets the reverberation decay time at low-frequencies in milliseconds.</para>
		///     <para>Ranges from <c>100.0</c> to <c>20000.0</c>. Default is <c>1500.0</c>.</para>
		/// </summary>
		/// <value>
		///     The decay time.
		/// </value>
		public float DecayTime
		{
			get => GetParameterFloat(0);
			set
			{
				var clamped = value.Clamp(100.0f, 20000.0f);
				SetParameterFloat(0, clamped);
				PropertyChanged?.Invoke(this, new FloatParamEventArgs(0, clamped, 100.0f, 20000.0f));
			}
		}

		/// <summary>
		///     <para>Gets or sets the delay time of first reflection in milliseconds. </para>
		///     <para>Ranges from <c>0.0</c> to <c>300.0</c>. Default is <c>20.0</c>.</para>
		/// </summary>
		/// <value>
		///     The early delay.
		/// </value>
		public float EarlyDelay
		{
			get => GetParameterFloat(1);
			set
			{
				var clamped = value.Clamp(0.0f, 300.0f);
				SetParameterFloat(1, clamped);
				PropertyChanged?.Invoke(this, new FloatParamEventArgs(1, clamped, 0.0f, 300.0f));
			}
		}

		/// <summary>
		///     <para>Gets or sets the late reverberation delay time relative to first reflection in milliseconds.</para>
		///     <para>Ranges from <c>0.0</c> to <c>100.0</c>. Default is <c>40.0</c>. </para>
		/// </summary>
		/// <value>
		///     The late delay.
		/// </value>
		public float LateDelay
		{
			get => GetParameterFloat(2);
			set
			{
				var clamped = value.Clamp(0.0f, 100.0f);
				SetParameterFloat(2, clamped);
				PropertyChanged?.Invoke(this, new FloatParamEventArgs(2, clamped, 0.0f, 100.0f));
			}
		}

		/// <summary>
		///     <para>Gets or sets the reference frequency for high-frequency decay in Hz.</para>
		///     <para>Ranges from <c>20.0</c> to <c>20000.0</c>. Default is <c>5000.0</c>.</para>
		/// </summary>
		/// <value>
		///     The high-frequency reference.
		/// </value>
		public float HfReference
		{
			get => GetParameterFloat(3);
			set
			{
				var clamped = value.Clamp(20.0f, 20000.0f);
				SetParameterFloat(3, clamped);
				PropertyChanged?.Invoke(this, new FloatParamEventArgs(3, clamped, 20.0f, 20000.0f));
			}
		}

		/// <summary>
		///     <para>Gets or sets the high-frequency decay time relative to decay time in percent.</para>
		///     <para>Ranges from <c>10.0</c> to <c>100.0</c>. Default is <c>50.0</c>.</para>
		/// </summary>
		/// <value>
		///     The high-frequency decay ratio.
		/// </value>
		public float HfDecayRatio
		{
			get => GetParameterFloat(4);
			set
			{
				var clamped = value.Clamp(10.0f, 200.0f);
				SetParameterFloat(4, clamped);
				PropertyChanged?.Invoke(this, new FloatParamEventArgs(4, clamped, 10.0f, 200.0f));
			}
		}

		/// <summary>
		///     <para>Gets or sets the reverberation diffusion (echo density) in percent.</para>
		///     <para>Ranges from <c>10.0</c> to <c>100.0</c>. Default is <c>100.0</c>.</para>
		/// </summary>
		/// <value>
		///     The diffusion.
		/// </value>
		public float Diffusion
		{
			get => GetParameterFloat(5);
			set
			{
				var clamped = value.Clamp(0.0f, 100.0f);
				SetParameterFloat(5, clamped);
				PropertyChanged?.Invoke(this, new FloatParamEventArgs(5, clamped, 0.0f, 100.0f));
			}
		}

		/// <summary>
		///     <para>Gets or sets the reverberation density (modal density) in percent.</para>
		///     <para>Ranges from <c>10.0</c> to <c>100.0</c>. Default is <c>100.0</c>.</para>
		/// </summary>
		/// <value>
		///     The density.
		/// </value>
		public float Density
		{
			get => GetParameterFloat(6);
			set
			{
				var clamped = value.Clamp(0.0f, 100.0f);
				SetParameterFloat(6, clamped);
				PropertyChanged?.Invoke(this, new FloatParamEventArgs(6, clamped, 0.0f, 100.0f));
			}
		}

		/// <summary>
		///     <para>Gets or sets the transition frequency of low-shelf filter in Hz.</para>
		///     <para>Ranges from <c>20.0</c> to <c>1000.0</c>. Default is <c>250.0</c>.</para>
		/// </summary>
		/// <value>
		///     The low shelf frequency.
		/// </value>
		public float LowShelfFrequency
		{
			get => GetParameterFloat(7);
			set
			{
				var clamped = value.Clamp(20.0f, 1000.0f);
				SetParameterFloat(7, clamped);
				PropertyChanged?.Invoke(this, new FloatParamEventArgs(7, clamped, 20.0f, 1000.0f));
			}
		}

		/// <summary>
		///     <para>Gets or sets the gain of low-shelf filter in dB.</para>
		///     <para>Ranges from <c>-36.0</c> to <c>12.0</c>. Default is <c>0.0</c>.</para>
		/// </summary>
		/// <value>
		///     The low shelf gain.
		/// </value>
		public float LowShelfGain
		{
			get => GetParameterFloat(8);
			set
			{
				var clamped = value.Clamp(-48.0f, 12.0f);
				SetParameterFloat(8, clamped);
				PropertyChanged?.Invoke(this, new FloatParamEventArgs(8, clamped, -48.0f, 12.0f));
			}
		}

		/// <summary>
		///     <para>Gets or sets the cutoff frequency of low-pass filter in Hz.</para>
		///     <para>Ranges from <c>20.0</c> to <c>20000.0</c>. Default is <c>20000.0</c>.</para>
		/// </summary>
		/// <value>
		///     The high cut.
		/// </value>
		public float HighCut
		{
			get => GetParameterFloat(9);
			set
			{
				var clamped = value.Clamp(20.0f, 20000.0f);
				SetParameterFloat(9, clamped);
				PropertyChanged?.Invoke(this, new FloatParamEventArgs(9, clamped, 20.0f, 20000.0f));
			}
		}

		/// <summary>
		///     <para>Gets or sets the blend ratio of late reverb to early reflections in percent.</para>
		///     <para>Ranges from <c>0.0</c> to <c>100.0</c>. Default is <c>50.0</c>.</para>
		/// </summary>
		/// <value>
		///     The early late mix.
		/// </value>
		public float EarlyLateMix
		{
			get => GetParameterFloat(10);
			set
			{
				var clamped = value.Clamp(0.0f, 100.0f);
				SetParameterFloat(10, clamped);
				PropertyChanged?.Invoke(this, new FloatParamEventArgs(10, clamped, 0.0f, 100.0f));
			}
		}

		/// <summary>
		///     <para>Gets or sets the reverb signal level in dB.</para>
		///     <para>Ranges from <c>-80.0</c> to <c>20.0</c>. Default is <c>-6.0</c>.</para>
		/// </summary>
		/// <value>
		///     The wet level.
		/// </value>
		public float WetLevel
		{
			get => GetParameterFloat(11);
			set
			{
				var clamped = value.Clamp(-80.0f, 20.0f);
				SetParameterFloat(11, clamped);
				PropertyChanged?.Invoke(this, new FloatParamEventArgs(11, clamped, -80.0f, 20.0f));
			}
		}

		/// <summary>
		///     <para>Gets or sets the dry signal level in dB.</para>
		///     <para>Ranges from <c>-80.0</c> to <c>20.0</c>. Default is <c>0.0</c>.</para>
		/// </summary>
		/// <value>
		///     The dry level.
		/// </value>
		public float DryLevel
		{
			get => GetParameterFloat(12);
			set
			{
				var clamped = value.Clamp(-80.0f, 20.0f);
				SetParameterFloat(12, clamped);
				PropertyChanged?.Invoke(this, new FloatParamEventArgs(12, clamped, -80.0f, 20.0f));
			}
		}

		#endregion

		#region Methods

		/// <summary>
		///     Gets the properties all at once.
		/// </summary>
		/// <param name="dryLevel">The dry level.</param>
		/// <returns>The reverb properties.</returns>
		public ReverbProperties GetProperties(out float dryLevel)
		{
			dryLevel = GetParameterFloat(12);
			return new ReverbProperties
			{
				DecayTime = GetParameterFloat(0),
				EarlyDelay = GetParameterFloat(1),
				LateDelay = GetParameterFloat(2),
				HFReference = GetParameterFloat(3),
				HFDecayRatio = GetParameterFloat(4),
				Diffusion = GetParameterFloat(5),
				Density = GetParameterFloat(6),
				LowShelfFrequency = GetParameterFloat(7),
				LowShelfGain = GetParameterFloat(8),
				HighCut = GetParameterFloat(9),
				EarlyLateMix = GetParameterFloat(10),
				WetLevel = GetParameterFloat(11)
			};
		}

		/// <summary>
		///     Sets the properties all at once.
		/// </summary>
		/// <param name="properties">The properties to apply.</param>
		/// <param name="dryLevel">The dry level to apply.</param>
		/// <remarks>
		///     When changing the properties all at once with this method, the
		///     <see cref="FloatParamEventArgs.ParameterIndex" /> will be <c>-1</c> with all other values set to
		///     <c>0.0</c>.
		/// </remarks>
		public void SetProperties(ReverbProperties properties, float dryLevel)
		{
			SetParameterFloat(0, properties.DecayTime.Clamp(100.0f, 20000.0f));
			SetParameterFloat(1, properties.EarlyDelay.Clamp(0.0f, 300.0f));
			SetParameterFloat(2, properties.LateDelay.Clamp(0.0f, 100.0f));
			SetParameterFloat(3, properties.HFReference.Clamp(20.0f, 20000.0f));
			SetParameterFloat(4, properties.HFDecayRatio.Clamp(10.0f, 200.0f));
			SetParameterFloat(5, properties.Diffusion.Clamp(0.0f, 100.0f));
			SetParameterFloat(6, properties.Density.Clamp(0.0f, 100.0f));
			SetParameterFloat(7, properties.LowShelfFrequency.Clamp(20.0f, 1000.0f));
			SetParameterFloat(8, properties.LowShelfGain.Clamp(-48.0f, 12.0f));
			SetParameterFloat(9, properties.HighCut.Clamp(20.0f, 20000.0f));
			SetParameterFloat(10, properties.EarlyLateMix.Clamp(0.0f, 100.0f));
			SetParameterFloat(11, properties.WetLevel.Clamp(-80.0f, 20.0f));
			SetParameterFloat(12, dryLevel.Clamp(-80.0f, 20.0f));
			PropertyChanged?.Invoke(this, new FloatParamEventArgs(-1, 0, 0, 0));
		}

		#endregion
	}
}