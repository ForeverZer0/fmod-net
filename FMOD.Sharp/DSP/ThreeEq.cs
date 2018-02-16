using System;
using FMOD.Core;

namespace FMOD.DSP
{
	/// <inheritdoc />
	/// <summary>
	///     Basic three-band equalizer.
	/// </summary>
	/// <seealso cref="T:FMOD.Sharp.Dsp" />
	public class ThreeEq : Dsp
	{
		/// <summary>
		///     Describes a slope used for crossovers in the <see cref="ThreeEq" /> unit.
		/// </summary>
		/// <seealso cref="ThreeEq" />
		/// <seealso cref="ThreeEq.Slope" />
		public enum CrossoverSlope
		{
			/// <summary>
			///     12dB/Octave
			/// </summary>
			Twelve,

			/// <summary>
			///     24dB/Octave
			/// </summary>
			TwentyFour,

			/// <summary>
			///     48dB/Octave
			/// </summary>
			FortyEight
		}

		/// <summary>
		///     Initializes a new instance of the <see cref="ThreeEq" /> class.
		/// </summary>
		/// <param name="handle">The handle.</param>
		internal ThreeEq(IntPtr handle) : base(handle)
		{
		}

		/// <summary>
		///     <para>Gets or sets the low-frequency gain in dB. </para>
		///     <para><c>-80.0</c> to <c>10.0</c>. Default = <c>0.0</c>.</para>
		/// </summary>
		/// <value>
		///     The low-frequency gain.
		/// </value>
		public float LowGain
		{
			get => GetParameterFloat(0);
			set
			{
				var clamped = value.Clamp(-80.0f, 10.0f);
				SetParameterFloat(0, clamped);
				LowGainChanged?.Invoke(this, new DspFloatParamChangedEventArgs(0, clamped, -80.0f, 10.0f));
			}
		}

		/// <summary>
		///     <para>Gets or sets the mid-frequency gain in dB. </para>
		///     <para><c>-80.0</c> to <c>10.0</c>. Default = <c>0.0</c>.</para>
		/// </summary>
		/// <value>
		///     The mid-frequency gain.
		/// </value>
		public float MidGain
		{
			get => GetParameterFloat(1);
			set
			{
				var clamped = value.Clamp(-80.0f, 10.0f);
				SetParameterFloat(1, clamped);
				MidGainChanged?.Invoke(this, new DspFloatParamChangedEventArgs(1, clamped, -80.0f, 10.0f));
			}
		}

		/// <summary>
		///     <para>Gets or sets the high-frequency gain in dB. </para>
		///     <para><c>-80.0</c> to <c>10.0</c>. Default = <c>0.0</c>.</para>
		/// </summary>
		/// <value>
		///     The high-frequency gain.
		/// </value>
		public float HighGain
		{
			get => GetParameterFloat(2);
			set
			{
				var clamped = value.Clamp(-80.0f, 10.0f);
				SetParameterFloat(2, clamped);
				HighGainChanged?.Invoke(this, new DspFloatParamChangedEventArgs(2, clamped, -80.0f, 10.0f));
			}
		}

		/// <summary>
		///     <para>Gets or sets the low-to-mid crossover frequency in Hz. </para>
		///     <para><c>10.0</c> to <c>22000.0</c>. Default = <c>400.0</c>.</para>
		/// </summary>
		/// <value>
		///     The low crossover.
		/// </value>
		public float LowCrossover
		{
			get => GetParameterFloat(3);
			set
			{
				var clamped = value.Clamp(10.0f, 22000.0f);
				SetParameterFloat(3, clamped);
				LowCrossoverChanged?.Invoke(this, new DspFloatParamChangedEventArgs(3, clamped, 10.0f, 22000.0f));
			}
		}

		/// <summary>
		///     <para>Gets or sets the mid-to-high crossover frequency in Hz.</para>
		///     <para><c>10.0</c> to <c>22000.0</c>. Default = <c>4000.0</c>.</para>
		/// </summary>
		/// <value>
		///     The high crossover.
		/// </value>
		public float HighCrossover
		{
			get => GetParameterFloat(4);
			set
			{
				var clamped = value.Clamp(10.0f, 22000.0f);
				SetParameterFloat(4, clamped);
				HighCrossoverChanged?.Invoke(this, new DspFloatParamChangedEventArgs(4, clamped, 10.0f, 22000.0f));
			}
		}

		/// <summary>
		///     <para>Gets or sets the slope for crossovers.</para>
		///     <para>Default = <see cref="ThreeEq.CrossoverSlope.TwentyFour" />.</para>
		/// </summary>
		/// <value>
		///     The slope.
		/// </value>
		public CrossoverSlope Slope
		{
			get => (CrossoverSlope) GetParameterInt(5);
			set => SetParameterInt(5, (int) value);
		}

		/// <summary>
		///     Occurs when the <see cref="LowGain" /> property has changed.
		/// </summary>
		/// <see cref="DspFloatParamChangedEventArgs" />
		public event EventHandler<DspFloatParamChangedEventArgs> LowGainChanged;

		/// <summary>
		///     Occurs when the <see cref="MidGain" /> property has changed.
		/// </summary>
		/// <see cref="DspFloatParamChangedEventArgs" />
		public event EventHandler<DspFloatParamChangedEventArgs> MidGainChanged;

		/// <summary>
		///     Occurs when the <see cref="HighGain" /> property has changed.
		/// </summary>
		/// <see cref="DspFloatParamChangedEventArgs" />
		public event EventHandler<DspFloatParamChangedEventArgs> HighGainChanged;

		/// <summary>
		///     Occurs when the <see cref="LowCrossover" /> property has changed.
		/// </summary>
		/// <see cref="DspFloatParamChangedEventArgs" />
		public event EventHandler<DspFloatParamChangedEventArgs> LowCrossoverChanged;

		/// <summary>
		///     Occurs when the <see cref="HighCrossover" /> property has changed.
		/// </summary>
		/// <see cref="DspFloatParamChangedEventArgs" />
		public event EventHandler<DspFloatParamChangedEventArgs> HighCrossoverChanged;
	}
}