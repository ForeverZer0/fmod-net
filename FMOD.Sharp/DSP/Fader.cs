using System;
using FMOD.Core;

namespace FMOD.DSP
{
	/// <inheritdoc />
	/// <summary>
	/// This unit pans and scales the volume of a unit.
	/// </summary>
	/// <seealso cref="T:FMOD.Sharp.Dsp" />
	public class Fader : Dsp
	{
		/// <summary>
		/// Occurs when <see cref="Gain"/> property has changed.
		/// </summary>
		/// <seealso cref="DspFloatParamChangedEventArgs"/>
		public event EventHandler<DspFloatParamChangedEventArgs> GainChanged;

		/// <summary>
		/// Initializes a new instance of the <see cref="Fader"/> class.
		/// </summary>
		/// <param name="handle">The handle.</param>
		internal Fader(IntPtr handle) : base(handle)
		{
		}

		/// <summary>
		/// <para>Gets or sets the signal gain in dB.</para> 
		/// <para><c>-80.0</c> to <c>10.0</c>. Default = <c>0.0</c>.</para> 
		/// </summary>
		/// <value>
		/// The gain.
		/// </value>
		public float Gain
		{
			get => GetParameterFloat(0);
			set
			{
				var clamped = value.Clamp(-80.0f, 10.0f);
				SetParameterFloat(0, clamped);
				GainChanged?.Invoke(this, new DspFloatParamChangedEventArgs(0, clamped, -80.0f, 10.0f));
			}
		}
	}
}
