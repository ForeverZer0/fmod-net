using System;

namespace FMOD.Sharp.DSP
{
	/// <inheritdoc />
	/// <summary>
	///     Multichannel software limiter that is uniform across the whole spectrum.
	/// </summary>
	/// <seealso cref="T:FMOD.Sharp.DspBase" />
	/// <remarks>
	///     The limiter is not guaranteed to catch every peak above the threshold level, because it cannot apply gain
	///     reduction instantaneously - the time delay is determined by the attack time. However setting the attack time too
	///     short will distort the sound, so it is a compromise. High level peaks can be avoided by using a short attack time -
	///     but not too short, and setting the threshold a few decibels below the critical level.
	/// </remarks>
	public class Compressor : DspBase
	{
		/// <summary>
		///     Initializes a new instance of the <see cref="Compressor" /> class.
		/// </summary>
		/// <param name="handle">The handle.</param>
		internal Compressor(IntPtr handle) : base(handle)
		{
		}

		/// <summary>
		///     <para>Gets or sets the threshold level in dB.</para>
		///     <para>Range from <c>-80.0</c> to <c>0.0</c>. Default = <c>0.0</c>.</para>
		/// </summary>
		/// <value>
		///     The threshold.
		/// </value>
		public float Threshold
		{
			get => GetParameterFloat(0);
			set
			{
				var clamped = value.Clamp(-60.0f, 0.0f);
				SetParameterFloat(0, clamped);
				ThresholdChanged?.Invoke(this, new DspFloatParamChangedEventArgs(0, clamped, -60.0f, 0.0f));
			}
		}

		/// <summary>
		///     <para>Gets or sets the compression ratio (dB/dB).</para>
		///     <para>Range from <c>1.0</c> to <c>50.0</c>. Default = <c>2.5</c>.</para>
		/// </summary>
		/// <value>
		///     The ratio.
		/// </value>
		public float Ratio
		{
			get => GetParameterFloat(1);
			set
			{
				var clamped = value.Clamp(1.0f, 50.0f);
				SetParameterFloat(1, clamped);
				RatioChanged?.Invoke(this, new DspFloatParamChangedEventArgs(1, clamped, 1.0f, 50.0f));
			}
		}

		/// <summary>
		///     <para>Gets or sets the attack time in milliseconds.</para>
		///     <para>Range from <c>0.1</c> to <c>1000.0</c>. Default = <c>20.0</c>.</para>
		/// </summary>
		/// <value>
		///     The attack.
		/// </value>
		public float Attack
		{
			get => GetParameterFloat(2);
			set
			{
				var clamped = value.Clamp(0.1f, 500.0f);
				SetParameterFloat(2, clamped);
				AttackChanged?.Invoke(this, new DspFloatParamChangedEventArgs(2, clamped, 0.1f, 500.0f));
			}
		}

		/// <summary>
		///     <para>Gets or sets the release time in milliseconds.</para>
		///     <para>Range from <c>10.0</c> through <c>5000.0</c>. Default = <c>100.0</c></para>
		/// </summary>
		/// <value>
		///     The release.
		/// </value>
		public float Release
		{
			get => GetParameterFloat(3);
			set
			{
				var clamped = value.Clamp(10.0f, 5000.0f);
				SetParameterFloat(3, clamped);
				ReleaseChanged?.Invoke(this, new DspFloatParamChangedEventArgs(3, clamped, 10.0f, 5000.0f));
			}
		}

		/// <summary>
		///     <para>Gets or sets the make-up gain (dB) applied after limiting.</para>
		///     <para>Range from <c>0.0</c> through <c>30.0</c>. Default = <c>0.0</c>.</para>
		/// </summary>
		/// <value>
		///     The make-up gain.
		/// </value>
		public float MakeUpGain
		{
			get => GetParameterFloat(4);
			set
			{
				var clamped = value.Clamp(-30.0f, 30.0f);
				SetParameterFloat(4, clamped);
				MakeUpGainChanged?.Invoke(this, new DspFloatParamChangedEventArgs(4, clamped, -30.0f, 30.0f));
			}
		}

		/// <summary>
		///     <para>Gets or sets a value indicating whether to analyse the sidechain signal instead of the input signal. </para>
		///     <para>Default = <c>false</c>.</para>
		/// </summary>
		/// <value>
		///     <c>true</c> if side chain is to be used; otherwise, <c>false</c>.
		/// </value>
		public bool UseSideChain
		{
			get => BitConverter.ToInt32(GetParameterData(5), 0) == 1;
			set
			{
				var bytes = BitConverter.GetBytes(value ? 1 : 0);
				SetParameterData(5, bytes);
				UseSideChainChanged?.Invoke(this, new DspBoolParamChangedEventArgs(5, value));
			}
		}

		/// <summary>
		///     Gets a value indicating whether this <see cref="Compressor" /> is linked.
		///     <para><c>false</c> = Independent (compressor per channel), <c>true</c> = Linked. Default = <c>true</c>.</para>
		/// </summary>
		/// <value>
		///     <c>true</c> if linked; otherwise, <c>false</c>.
		/// </value>
		public bool Linked
		{
			get => GetParameterBool(6);
			set
			{
				SetParameterBool(6, value);
				LinkedChanged?.Invoke(this, new DspBoolParamChangedEventArgs(6, value));
			}
		}

		/// <summary>
		///     Occurs when <see cref="Threshold" /> property has changed.
		/// </summary>
		/// <seealso cref="DspFloatParamChangedEventArgs" />
		public event EventHandler<DspFloatParamChangedEventArgs> ThresholdChanged;

		/// <summary>
		///     Occurs when <see cref="Ratio" /> property has changed.
		/// </summary>
		/// <seealso cref="DspFloatParamChangedEventArgs" />
		public event EventHandler<DspFloatParamChangedEventArgs> RatioChanged;

		/// <summary>
		///     Occurs when <see cref="Attack" /> property has changed.
		/// </summary>
		/// <seealso cref="DspFloatParamChangedEventArgs" />
		public event EventHandler<DspFloatParamChangedEventArgs> AttackChanged;

		/// <summary>
		///     Occurs when <see cref="Release" /> property has changed.
		/// </summary>
		/// <seealso cref="DspFloatParamChangedEventArgs" />
		public event EventHandler<DspFloatParamChangedEventArgs> ReleaseChanged;

		/// <summary>
		///     Occurs when <see cref="MakeUpGain" /> property has changed.
		/// </summary>
		/// <seealso cref="DspFloatParamChangedEventArgs" />
		public event EventHandler<DspFloatParamChangedEventArgs> MakeUpGainChanged;

		/// <summary>
		///     Occurs when <see cref="UseSideChain" /> property has changed.
		/// </summary>
		/// <seealso cref="DspBoolParamChangedEventArgs" />
		public event EventHandler<DspBoolParamChangedEventArgs> UseSideChainChanged;

		/// <summary>
		///     Occurs when <see cref="Linked" /> property has changed.
		/// </summary>
		/// <seealso cref="DspBoolParamChangedEventArgs" />
		public event EventHandler<DspBoolParamChangedEventArgs> LinkedChanged;
	}
}