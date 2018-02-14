using System;

namespace FMOD.Sharp.DSP
{
	/// <inheritdoc />
	/// <summary>
	///     Applies a "flange" effect on the sound.
	/// </summary>
	/// <remarks>
	///     <para>
	///         Flange is an effect where the signal is played twice at the same time, and one copy slides back and forth
	///         creating a whooshing or flanging effect.
	///     </para>
	///     <para>
	///         As there are 2 copies of the same signal, by default each signal is given 50% mix, so that the total is not
	///         louder than the original unaffected signal.
	///     </para>
	///     <para>
	///         Flange depth is a percentage of a 10ms shift from the original signal. Anything above 10ms is not considered
	///         flange because to the ear it begins to 'echo' so 10ms is the highest value possible.
	///     </para>
	/// </remarks>
	/// <seealso cref="T:FMOD.Sharp.DspBase" />
	public class Flange : DspBase
	{
		/// <summary>
		///     Initializes a new instance of the <see cref="Flange" /> class.
		/// </summary>
		/// <param name="handle">The handle.</param>
		internal Flange(IntPtr handle) : base(handle)
		{
		}

		/// <summary>
		///     <para>Gets or sets the percentage of wet signal in mix.</para>
		///     <para><c>0</c> to <c>100</c>. Default = <c>50</c>.</para>
		/// </summary>
		/// <value>
		///     The mix.
		/// </value>
		public float Mix
		{
			get => GetParameterFloat(0);
			set
			{
				var clamped = value.Clamp(0.0f, 100.0f);
				SetParameterFloat(0, clamped);
				MixChanged?.Invoke(this, new DspFloatParamChangedEventArgs(0, clamped, 0.0f, 100.0f));
			}
		}

		/// <summary>
		///     Gets or sets the flange depth (percentage of 40ms delay).
		///     <para><c>0.01</c> to <c>1.0</c>. Default = <c>1.0</c>. </para>
		/// </summary>
		/// <value>
		///     The depth.
		/// </value>
		public float Depth
		{
			get => GetParameterFloat(1);
			set
			{
				var clamped = value.Clamp(0.01f, 1.0f);
				SetParameterFloat(1, clamped);
				DepthChanged?.Invoke(this, new DspFloatParamChangedEventArgs(1, clamped, 0.01f, 1.0f));
			}
		}

		/// <summary>
		///     Gets or sets the flange speed in Hz.
		///     <para><c>0.0</c> to <c>20.0</c>. Default = <c>0.1</c>.</para>
		/// </summary>
		/// <value>
		///     The rate.
		/// </value>
		public float Rate
		{
			get => GetParameterFloat(2);
			set
			{
				var clamped = value.Clamp(0.0f, 20.0f);
				SetParameterFloat(2, clamped);
				RateChanged?.Invoke(this, new DspFloatParamChangedEventArgs(2, clamped, 0.0f, 20.0f));
			}
		}

		/// <summary>
		///     Occurs when <see cref="Mix" /> property is changed.
		/// </summary>
		/// <seealso cref="DspFloatParamChangedEventArgs"/>
		public event EventHandler<DspFloatParamChangedEventArgs> MixChanged;

		/// <summary>
		///     Occurs when <see cref="Rate" /> property is changed.
		/// </summary>
		/// <seealso cref="DspFloatParamChangedEventArgs"/>
		public event EventHandler<DspFloatParamChangedEventArgs> RateChanged;

		/// <summary>
		///     Occurs when <see cref="Depth" /> property is changed.
		/// </summary>
		/// <seealso cref="DspFloatParamChangedEventArgs"/>
		public event EventHandler<DspFloatParamChangedEventArgs> DepthChanged;
	}
}