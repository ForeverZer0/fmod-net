using System;

namespace FMOD.Sharp.DSP
{
#pragma warning disable 618
	/// <summary>
	///     This unit filters sound using a resonant lowpass filter algorithm that is used in
	///     <see href="http://www.users.on.net/~jtlim/ImpulseTracker/">Impulse Tracker</see>, but with limited cutoff range (0
	///     to 8060hz).
	/// </summary>
	/// <remarks>
	///     <para>
	///         This is different to the default <see cref="T:FMOD.Sharp.DSP.Lowpass" /> filter in that it uses a different
	///         quality algorithm and is the filter used to produce the correct sounding playback in .IT files.
	///     </para>
	///     <para><b>FMOD Studio</b>'s .IT playback uses this filter.</para>
	///     <alert class="note">
	///         <para>
	///             This filter actually has a limited cutoff frequency below the specified maximum, due to its limited
	///             design, so for a more open range filter use <see cref="!:LowPass" /> or if you don't mind not having
	///             resonance, <see cref="T:FMOD.Sharp.DSP.LowpassSimple" />.
	///         </para>
	///         <para>The effective maximum cutoff is about 8060 Hz.</para>
	///     </alert>
	/// </remarks>
	/// <seealso cref="T:FMOD.Sharp.DSP.DspBase" />
	/// <seealso cref="T:FMOD.Sharp.DSP.Lowpass" />
	/// <seealso cref="T:FMOD.Sharp.DSP.LowpassSimple" />
#pragma warning restore 618 
	public class ItLowpass : DspBase
	{
		/// <summary>
		///     Initializes a new instance of the <see cref="ItLowpass" /> class.
		/// </summary>
		/// <param name="handle">The handle.</param>
		internal ItLowpass(IntPtr handle) : base(handle)
		{
		}

		/// <summary>
		///     <para>Gets or sets the lowpass cutoff frequency in Hz.</para>
		///     <para><c>1.0</c> to <c>22000.0.</c> Default = <c>5000.0</c></para>
		/// </summary>
		/// <value>
		///     The cutoff frequency.
		/// </value>
		/// <seealso cref="CutoffFrequencyChanged" />
		public float CutoffFrequency
		{
			get => GetParameterFloat(0);
			set
			{
				var clamped = value.Clamp(1.0f, 22000.0f);
				SetParameterFloat(0, clamped);
				CutoffFrequencyChanged?.Invoke(this, new DspFloatParamChangedEventArgs(0, clamped, 1.0f, 22000.0f));
			}
		}

		/// <summary>
		///     <para>Gets or sets the lowpass resonance Q value.</para>
		///     <para><c>0.0</c> to <c>127.0</c>. Default = <c>1.0</c>.</para>
		/// </summary>
		/// <value>
		///     The resonance.
		/// </value>
		/// <seealso cref="ResonanceChanged" />
		public float Resonance
		{
			get => GetParameterFloat(1);
			set
			{
				var clamped = value.Clamp(1.0f, 127.0f);
				SetParameterFloat(1, clamped);
				ResonanceChanged?.Invoke(this, new DspFloatParamChangedEventArgs(1, clamped, 1.0f, 127.0f));
			}
		}

		/// <summary>
		///     Occurs when <see cref="CutoffFrequency" /> property has changed.
		/// </summary>
		/// <seealso cref="CutoffFrequency" />
		/// <seealso cref="DspFloatParamChangedEventArgs" />
		public event EventHandler<DspFloatParamChangedEventArgs> CutoffFrequencyChanged;

		/// <summary>
		///     Occurs when <see cref="Resonance" /> property has changed.
		/// </summary>
		/// <seealso cref="Resonance" />
		/// <seealso cref="DspFloatParamChangedEventArgs" />
		public event EventHandler<DspFloatParamChangedEventArgs> ResonanceChanged;
	}
}