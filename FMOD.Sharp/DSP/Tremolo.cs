using System;
using FMOD.Core;

namespace FMOD.DSP
{
	/// <inheritdoc />
	/// <summary>
	///     Applies a "tremelo" effect on the sound.
	/// </summary>
	/// <remarks>
	///     <para>
	///         The tremolo effect varies the amplitude of a sound. Depending on the settings, this unit can produce a
	///         tremolo, chopper or auto-pan effect.
	///     </para>
	///     <para>
	///         The shape of the LFO (low freq. oscillator) can morphed between sine, triangle and sawtooth waves using the
	///         <see cref="P:FMOD.Sharp.Dsps.Tremolo.Shape" /> and <see cref="P:FMOD.Sharp.Dsps.Tremolo.Skew" /> properties.
	///     </para>
	///     <para>
	///         <see cref="P:FMOD.Sharp.Dsps.Tremolo.Duty" /> and <see cref="P:FMOD.Sharp.Dsps.Tremolo.Square" /> are useful
	///         for a chopper-type effect where the first controls the on-time duration and second controls the flatness of the
	///         envelope.
	///     </para>
	///     <para>
	///         <see cref="P:FMOD.Sharp.Dsps.Tremolo.Spread" /> varies the LFO phase between channels to get an auto-pan
	///         effect. This works best with a sine shape LFO.
	///     </para>
	///     <para>
	///         The LFO can be synchronized using the <see cref="P:FMOD.Sharp.Dsps.Tremolo.Phase" /> parameter which sets its
	///         instantaneous phase.
	///     </para>
	/// </remarks>
	/// <seealso cref="T:FMOD.Sharp.Dsp" />
	public class Tremolo : Dsp
	{
		/// <summary>
		///     Initializes a new instance of the <see cref="Tremolo" /> class.
		/// </summary>
		/// <param name="handle">The handle.</param>
		internal Tremolo(IntPtr handle) : base(handle)
		{
		}

		/// <summary>
		///     <para>Gets or sets the LFO frequency in Hz. </para>
		///     <para><c>0.1</c> to <c>20.0</c>. Default = <c>5.0</c>. </para>
		/// </summary>
		/// <value>
		///     The frequency.
		/// </value>
		public float Frequency
		{
			get => GetParameterFloat(0);
			set
			{
				var clamped = value.Clamp(0.01f, 20.0f);
				SetParameterFloat(0, clamped);
				FrequencyChanged?.Invoke(this, new DspFloatParamChangedEventArgs(0, clamped, 0.01f, 20.0f));
			}
		}

		/// <summary>
		///     <para>Gets or sets the tremolo depth.</para>
		///     <para><c>0.0</c> to <c>1.0</c>. Default = <c>1.0</c>.</para>
		/// </summary>
		/// <value>
		///     The depth.
		/// </value>
		public float Depth
		{
			get => GetParameterFloat(1);
			set
			{
				var clamped = value.Clamp(0.0f, 1.0f);
				SetParameterFloat(1, clamped);
				DepthChanged?.Invoke(this, new DspFloatParamChangedEventArgs(1, clamped, 0.0f, 1.0f));
			}
		}

		/// <summary>
		///     <para>Gets or sets the LFO shape morph between triangle and sine.</para>
		///     <para><c>0.0</c> to <c>1.0</c>. Default = <c>1.0</c>.</para>
		/// </summary>
		/// <value>
		///     The shape.
		/// </value>
		public float Shape
		{
			get => GetParameterFloat(2);
			set
			{
				var clamped = value.Clamp(0.0f, 1.0f);
				SetParameterFloat(2, clamped);
				ShapeChanged?.Invoke(this, new DspFloatParamChangedEventArgs(2, clamped, 0.0f, 1.0f));
			}
		}

		/// <summary>
		///     <para>Gets or sets the time-skewing of LFO cycle.</para>
		///     <para><c>-1.0</c> to <c>1.0</c>. Default = <c>0.0</c>.</para>
		/// </summary>
		/// <value>
		///     The skew.
		/// </value>
		public float Skew
		{
			get => GetParameterFloat(3);
			set
			{
				var clamped = value.Clamp(-1.0f, 1.0f);
				SetParameterFloat(3, clamped);
				SkewChanged?.Invoke(this, new DspFloatParamChangedEventArgs(3, clamped, -1.0f, 1.0f));
			}
		}

		/// <summary>
		///     <para>Gets or sets the LFO on-time.</para>
		///     <para><c>0.0</c> to <c>1.0</c>. Default = <c>0.5</c>.</para>
		/// </summary>
		/// <value>
		///     The duty.
		/// </value>
		public float Duty
		{
			get => GetParameterFloat(4);
			set
			{
				var clamped = value.Clamp(0.0f, 1.0f);
				SetParameterFloat(4, clamped);
				DutyChanged?.Invoke(this, new DspFloatParamChangedEventArgs(4, clamped, 0.0f, 1.0f));
			}
		}

		/// <summary>
		///     <para>Gets or sets the flatness of the LFO shape.</para>
		///     <para><c>0.0</c> to <c>1.0</c>. Default = <c>0.0</c>.</para>
		/// </summary>
		/// <value>
		///     The square.
		/// </value>
		public float Square
		{
			get => GetParameterFloat(5);
			set
			{
				var clamped = value.Clamp(0.0f, 1.0f);
				SetParameterFloat(5, clamped);
				SquareChanged?.Invoke(this, new DspFloatParamChangedEventArgs(5, clamped, 0.0f, 1.0f));
			}
		}

		/// <summary>
		///     <para>Gets or sets the instantaneous LFO phase. </para>
		///     <para><c>0.0</c> to <c>1.0</c>. Default = <c>0.0</c>.</para>
		/// </summary>
		/// <value>
		///     The phase.
		/// </value>
		public float Phase
		{
			get => GetParameterFloat(6);
			set
			{
				var clamped = value.Clamp(0.0f, 1.0f);
				SetParameterFloat(6, clamped);
				PhaseChanged?.Invoke(this, new DspFloatParamChangedEventArgs(6, clamped, 0.0f, 1.0f));
			}
		}

		/// <summary>
		///     Gets or sets the rotation / auto-pan effect.
		///     <para><c>-1.0</c> to <c>1.0</c>. Default = <c>0.0</c>.</para>
		/// </summary>
		/// <value>
		///     The spread.
		/// </value>
		public float Spread
		{
			get => GetParameterFloat(7);
			set
			{
				var clamped = value.Clamp(-1.0f, 1.0f);
				SetParameterFloat(7, clamped);
				SpreadChanged?.Invoke(this, new DspFloatParamChangedEventArgs(7, clamped, -1.0f, 1.0f));
			}
		}

		/// <summary>
		///     Occurs when <see cref="Frequency" /> property has changed.
		/// </summary>
		/// <seealso cref="DspFloatParamChangedEventArgs" />
		public event EventHandler<DspFloatParamChangedEventArgs> FrequencyChanged;

		/// <summary>
		///     Occurs when <see cref="Depth" /> property has changed.
		/// </summary>
		/// <seealso cref="DspFloatParamChangedEventArgs" />
		public event EventHandler<DspFloatParamChangedEventArgs> DepthChanged;

		/// <summary>
		///     Occurs when <see cref="Shape"/> property has changed.
		/// 
		/// </summary>
		/// <seealso cref="DspFloatParamChangedEventArgs" />
		public event EventHandler<DspFloatParamChangedEventArgs> ShapeChanged;

		/// <summary>
		///     Occurs when <see cref="Skew" /> property has changed.
		/// </summary>
		/// <seealso cref="DspFloatParamChangedEventArgs" />
		public event EventHandler<DspFloatParamChangedEventArgs> SkewChanged;

		/// <summary>
		///     Occurs when <see cref="Duty" /> property has changed.
		/// </summary>
		/// <seealso cref="DspFloatParamChangedEventArgs" />
		public event EventHandler<DspFloatParamChangedEventArgs> DutyChanged;

		/// <summary>
		///     Occurs when <see cref="Square" /> property has changed.
		/// </summary>
		/// <seealso cref="DspFloatParamChangedEventArgs" />
		public event EventHandler<DspFloatParamChangedEventArgs> SquareChanged;

		/// <summary>
		///     Occurs when <see cref="Phase" /> property has changed.
		/// </summary>
		/// <seealso cref="DspFloatParamChangedEventArgs" />
		public event EventHandler<DspFloatParamChangedEventArgs> PhaseChanged;

		/// <summary>
		///     Occurs when <see cref="Spread" /> property has changed.
		/// </summary>
		/// <seealso cref="DspFloatParamChangedEventArgs" />
		public event EventHandler<DspFloatParamChangedEventArgs> SpreadChanged;
	}
}