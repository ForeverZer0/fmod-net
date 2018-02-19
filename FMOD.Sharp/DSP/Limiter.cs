﻿using System;
using FMOD.Arguments;
using FMOD.Core;

namespace FMOD.DSP
{
	/// <summary>
	/// This unit limits the sound to a certain level.
	/// </summary>
	/// <seealso cref="FMOD.Sharp.DSP.DspBase" />
	public class Limiter : Dsp
	{
		/// <summary>
		/// Occurs when the <see cref="ReleaseTime"/> property is changed.
		/// </summary>
		/// <seealso cref="ReleaseTime"/>
		/// <seealso cref="FloatParamEventArgs"/>
		public event EventHandler<FloatParamEventArgs> ReleaseTimeChanged;

		/// <summary>
		/// Occurs when the <see cref="Ceiling"/> property is changed.
		/// </summary>
		/// <seealso cref="Ceiling"/>
		/// <seealso cref="FloatParamEventArgs"/>
		public event EventHandler<FloatParamEventArgs> CeilingChanged;

		/// <summary>
		/// Occurs when the <see cref="MaximizerGain"/> property is changed.
		/// </summary>
		/// <seealso cref="MaximizerGain"/>
		/// <seealso cref="FloatParamEventArgs"/>
		public event EventHandler<FloatParamEventArgs> MaximizerGainChanged;

		/// <summary>
		/// Occurs when the <see cref="Linked"/> property is changed.
		/// </summary>
		/// <seealso cref="Linked"/>
		/// <seealso cref="BoolParamEventArgs"/>
		public event EventHandler<BoolParamEventArgs> LinkedChanged;

		/// <summary>
		/// Initializes a new instance of the <see cref="Limiter"/> class.
		/// </summary>
		/// <param name="handle">The handle.</param>
		internal Limiter(IntPtr handle) : base(handle)
		{
		}

		/// <summary>
		/// <para>Gets or sets the time to ramp the silence to full in ms.</para> 
		/// <para><c>1.0</c> to <c>1000.0</c>. Default = <c>10.0</c>.</para> 
		/// </summary>
		/// <value>
		/// The release time.
		/// </value>
		/// <seealso cref="ReleaseTimeChanged"/>
		public float ReleaseTime
		{
			get => GetParameterFloat(0);
			set
			{
				var clamped = value.Clamp(1.0f, 1000.0f);
				SetParameterFloat(0, clamped);
				ReleaseTimeChanged?.Invoke(this, new FloatParamEventArgs(0, clamped, 1.0f, 1000.0f));
			}
		}

		/// <summary>
		/// <para>Gets or sets the maximum level of the output signal in dB.</para> 
		/// <para><c>-12.0</c> to <c>0.0</c>. Default = <c>0.0</c>.</para>
		/// </summary>
		/// <value>
		/// The ceiling.
		/// </value>
		/// <seealso cref="CeilingChanged"/>
		public float Ceiling
		{
			get => GetParameterFloat(1);
			set
			{
				var clamped = value.Clamp(-12.0f, 0.0f);
				SetParameterFloat(1, clamped);
				CeilingChanged?.Invoke(this, new FloatParamEventArgs(1, clamped, -12.0f, 0.0f));
			}
		}

		/// <summary>
		/// <para>Gets or sets the maximum amplification allowed in dB.</para>
		/// <para><c>0.0</c> = no amplifaction, higher values allow more boost.</para>
		/// <para><c>0.0</c> to <c>12.0</c>. Default = <c>0.0</c>.</para>
		/// </summary>
		/// <value>
		/// The maximizer gain.
		/// </value>
		/// <seealso cref="MaximizerGainChanged"/>
		public float MaximizerGain
		{
			get => GetParameterFloat(2);
			set
			{
				var clamped = value.Clamp(0.0f, 12.0f);
				SetParameterFloat(2, clamped);
				MaximizerGainChanged?.Invoke(this, new FloatParamEventArgs(2, clamped, 0.0f, 12.0f));
			}
		}

		/// <summary>
		/// <para>Gets or sets a value indicating whether channel processing mode is linked or independent. </para>
		/// <para>Default = <c>false</c>.</para>
		/// </summary>
		/// <value>
		///   <c>true</c> if linked; otherwise, <c>false</c>.
		/// </value>
		public bool Linked
		{
			get => GetParameterBool(3);
			set
			{
				SetParameterBool(3, value);
				LinkedChanged?.Invoke(this, new BoolParamEventArgs(3, value));
			}
		}
	}
}
