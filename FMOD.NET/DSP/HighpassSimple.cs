using System;
using FMOD.Arguments;
using FMOD.Core;

namespace FMOD.DSP
{
	/// <inheritdoc />
	/// <summary>
	///     <para>Applies a "high-pass" filter effect on a sound.</para>
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
	///     <code language="CSharp" title="MultiBandEq High-Pass Effect" numberLines="true">
	/// // TODO: Make example
	/// </code>
	/// </example>
	/// <remarks>
	/// <para>This is a very simple single-order high pass filter.</para>
	/// <para>The emphasis is on speed rather than accuracy, so this should not be used for task requiring critical filtering.</para>
	/// </remarks>
	/// <seealso cref="T:FMOD.NET.Dsp" />
	/// <seealso cref="T:FMOD.NET.Dsps.MultiBandEq"/>
	[Obsolete("Deprecated and will be removed in a future release, to emulate with MultiBandEq. See documentation for example.")]
	public class HighpassSimple : Dsp
	{
		/// <summary>
		///     Occurs when the <see cref="CutoffFrequency" /> property is changed.
		/// </summary>
		/// <seealso cref="FloatParamEventArgs" />
		public event EventHandler<FloatParamEventArgs> CutoffFrequencyChanged;

		/// <summary>
		/// Initializes a new instance of the <see cref="HighpassSimple"/> class.
		/// </summary>
		/// <param name="handle">The handle.</param>
		internal HighpassSimple(IntPtr handle) : base(handle)
		{
		}

		/// <summary>
		///     <para>Gets or sets the highpass cutoff frequency in Hz.</para>
		///     <para><c>10.0</c> to output <c>22000.0</c>. Default = <c>1000.0</c>.</para>
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
	}
}
