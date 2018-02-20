using System;
using FMOD.Arguments;
using FMOD.Core;

namespace FMOD.DSP
{
	/// <inheritdoc />
	/// <summary>
	///     <para>Applies a "low-pass" filter effect on a sound.</para>
	///     <para>
	///         Deprecated and will be removed in a future release, to emulate with
	///         <see cref="T:FMOD.NET.Dsps.MultiBandEq" />.
	///     </para>
	///     <para>
	///         See "Example" section for examples on configuring <see cref="T:FMOD.NET.Dsps.MultiBandEq" /> for same
	///         effect.
	///     </para>
	/// </summary>
	/// <example>
	///     <code language="CSharp" title="MultiBandEq Low-Pass Effect" numberLines="true">
	/// // TODO: Make example
	/// </code>
	/// </example>
	/// <remarks>
	/// </remarks>
	/// <seealso cref="T:FMOD.NET.Dsp" />
	/// <seealso cref="T:FMOD.NET.Dsps.MultiBandEq"/>
	[Obsolete("Deprecated and will be removed in a future release, to emulate with MultiBandEq. See documentation for example.")]
	public class Lowpass : Dsp
	{

		/// <summary>
		///     Occurs when the <see cref="CutoffFrequency" /> property is changed.
		/// </summary>
		/// <seealso cref="FloatParamEventArgs" />
		public event EventHandler<FloatParamEventArgs> CutoffFrequencyChanged;

		/// <summary>
		///     Occurs when the <see cref="Resonance" /> property is changed.
		/// </summary>
		/// <seealso cref="FloatParamEventArgs" />
		public event EventHandler<FloatParamEventArgs> ResonanceChanged;

		/// <summary>
		/// Initializes a new instance of the <see cref="Lowpass"/> class.
		/// </summary>
		/// <param name="handle">The handle.</param>
		internal Lowpass(IntPtr handle) : base(handle)
		{
		}

		/// <summary>
		///     <para>Gets or sets the lowpass cutoff frequency in Hz.</para>
		///     <para><c>1.0</c> to output <c>22000.0</c>. Default = <c>5000.0</c>. </para>
		/// </summary>
		/// <value>
		///     The cutoff frequency.
		/// </value>
		public float CutoffFrequency
		{
			get => GetParameterFloat(0);
			set
			{
				var clamped = value.Clamp(10.0f, 22000.0f);
				SetParameterFloat(0, clamped);
				CutoffFrequencyChanged?.Invoke(this, new FloatParamEventArgs(0, clamped, 10.0f, 22000.0f));
			}
		}

		/// <summary>
		///     <para>Gets or sets the lowpass resonance Q value.</para>
		///     <para><c>1.0</c> to <c>10.0</c>. Default = <c>1.0</c>.</para>
		/// </summary>
		/// <value>
		///     The resonance.
		/// </value>
		public float Resonance
		{
			get => GetParameterFloat(1);
			set
			{
				var clamped = value.Clamp(1.0f, 10.0f);
				SetParameterFloat(1, clamped);
				ResonanceChanged?.Invoke(this, new FloatParamEventArgs(1, clamped, 1.0f, 10.0f));
			}
		}
	}
}
