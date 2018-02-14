using System;

namespace FMOD.Sharp.DSP
{
	/// <inheritdoc />
	/// <summary>
	///     Applies a "chorus" effect on a sound.
	/// </summary>
	/// <remarks>
	///     Chorus is an effect where the sound is more "spacious" due to 1 to 3 versions of the sound being played along
	///     side the original signal but with the pitch of each copy modulating on a sine wave.
	/// </remarks>
	/// <seealso cref="T:FMOD.Sharp.DspBase" />
	public class Chorus : DspBase
	{
		/// <summary>
		///     Initializes a new instance of the <see cref="Chorus" /> class.
		/// </summary>
		/// <param name="handle">The handle.</param>
		internal Chorus(IntPtr handle) : base(handle)
		{
		}

		/// <summary>
		///     <para>Gets or sets the volume of original signal to pass to output.</para>
		///     <para><c>0.0</c> to <c>100.0</c>. Default = <c>50.0</c>.</para>
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
		///     <para>Gets or sets the chorus modulation rate in Hz.</para>
		///     <para><c>0.0</c> to <c>20.0</c>. Default = <c>0.8</c> Hz. </para>
		/// </summary>
		/// <value>
		///     The rate.
		/// </value>
		public float Rate
		{
			get => GetParameterFloat(1);
			set
			{
				var clamped = value.Clamp(0.0f, 20.0f);
				SetParameterFloat(1, clamped);
				RateChanged?.Invoke(this, new DspFloatParamChangedEventArgs(1, clamped, 0.0f, 20.0f));
			}
		}

		/// <summary>
		///     <c>Gets or sets the chorus modulation depth.</c>
		///     <para><c>0.0</c> to <c>100.0</c>. Default = <c>3.0</c>.</para>
		/// </summary>
		/// <value>
		///     The depth.
		/// </value>
		public float Depth
		{
			get => GetParameterFloat(2);
			set
			{
				var clamped = value.Clamp(0.0f, 100.0f);
				SetParameterFloat(2, clamped);
				DepthChanged?.Invoke(this, new DspFloatParamChangedEventArgs(2, clamped, 0.0f, 100.0f));
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