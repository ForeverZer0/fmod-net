using System;
using FMOD.Arguments;
using FMOD.Core;

namespace FMOD.DSP
{
	/// <inheritdoc />
	/// <summary>
	///     Single band peaking EQ filter that attenuates or amplifies a selected frequency and its neighbouring frequencies.
	/// </summary>
	/// <example>
	///     <code language="CSharp" title="MultiBandEq ParamEq Effect" numberLines="true">
	/// // TODO: Make example
	/// </code>
	/// </example>
	/// <remarks>
	///     When a frequency has its gain set to <c>1.0</c>, the sound will be unaffected and represents the original
	///     signal exactly.
	/// </remarks>
	/// <seealso cref="T:FMOD.Sharp.Dsp" />
	/// <seealso cref="T:FMOD.Sharp.Dsps.MultiBandEq" />
	[Obsolete("Deprecated and will be removed in a future release, to emulate with MultiBandEq.")]
	public class ParamEq : Dsp
	{
		/// <summary>
		///     Initializes a new instance of the <see cref="ParamEq" /> class.
		/// </summary>
		/// <param name="handle">The handle.</param>
		internal ParamEq(IntPtr handle) : base(handle)
		{
		}

		/// <summary>
		///     <para>Gets or sets the frequency center.</para>
		///     <para><c>20.0</c> to <c>22000.0</c>. Default = <c>8000.0</c>.</para>
		/// </summary>
		/// <value>
		///     The center.
		/// </value>
		public float Center
		{
			get => GetParameterFloat(0);
			set
			{
				var clamped = value.Clamp(20.0f, 22000.0f);
				SetParameterFloat(0, clamped);
				CenterChanged?.Invoke(this, new DspFloatParamChangedEventArgs(0, clamped, 20.0f, 22000.0f));
			}
		}

		/// <summary>
		///     <para>Gets or sets the octave range around the center frequency to filter.</para>
		///     <para><c>0.2</c> to <c>5.0</c>. Default = <c>1.0</c>.</para>
		/// </summary>
		/// <value>
		///     The bandwidth.
		/// </value>
		public float Bandwidth
		{
			get => GetParameterFloat(1);
			set
			{
				var clamped = value.Clamp(0.2f, 5.0f);
				SetParameterFloat(1, clamped);
				BandwidthChanged?.Invoke(this, new DspFloatParamChangedEventArgs(1, clamped, 0.2f, 5.0f));
			}
		}

		/// <summary>
		///     Gets or sets the frequency gain in dB.
		///     <para><c>-30.0</c> to <c>30.0</c>. Default = <c>0.0</c>.</para>
		/// </summary>
		/// <value>
		///     The gain.
		/// </value>
		public float Gain
		{
			get => GetParameterFloat(2);
			set
			{
				var clamped = value.Clamp(-30.0f, 30.0f);
				SetParameterFloat(2, clamped);
				GainChanged?.Invoke(this, new DspFloatParamChangedEventArgs(2, clamped, -30.0f, 30.0f));
			}
		}

		/// <summary>
		///     Occurs when <see cref="Center" /> property has changed.
		///     <seealso cref="DspFloatParamChangedEventArgs" />
		/// </summary>
		public event EventHandler<DspFloatParamChangedEventArgs> CenterChanged;

		/// <summary>
		///     Occurs when <see cref="Bandwidth" /> property has changed.
		///     <seealso cref="DspFloatParamChangedEventArgs" />
		/// </summary>
		public event EventHandler<DspFloatParamChangedEventArgs> BandwidthChanged;

		/// <summary>
		///     Occurs when <see cref="Gain" /> property has changed.
		///     <seealso cref="DspFloatParamChangedEventArgs" />
		/// </summary>
		public event EventHandler<DspFloatParamChangedEventArgs> GainChanged;
	}
}