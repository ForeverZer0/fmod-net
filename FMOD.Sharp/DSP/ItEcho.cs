using System;
using FMOD.Core;

namespace FMOD.DSP
{
	/// <inheritdoc />
	/// <summary>
	///     <para>This is effectively a software based echo filter that emulates the DirectX DMO echo effect.</para>
	///     <para>
	///         Impulse tracker files can support this, and FMOD will produce the effect on ANY platform, not just those that
	///         support DirectX effects!
	///     </para>
	/// </summary>
	/// <seealso cref="T:FMOD.Sharp.Dsp" />
	public class ItEcho : Dsp
	{
		/// <summary>
		///     Initializes a new instance of the <see cref="ItEcho" /> class.
		/// </summary>
		/// <param name="handle">The handle.</param>
		internal ItEcho(IntPtr handle) : base(handle)
		{
		}

		/// <summary>
		///     <para>Gets or sets the ratio of wet (processed) signal to dry (unprocessed) signal.</para>
		///     <para>Range from <c>0.0</c> through <c>100.0</c> (all wet). Default = <c>50.0</c>.</para>
		/// </summary>
		/// <value>
		///     The wet dry mix.
		/// </value>
		public new float WetDryMix
		{
			get => GetParameterFloat(0);
			set
			{
				var clamped = value.Clamp(0.0f, 100.0f);
				SetParameterFloat(0, clamped);
				WetDryMixChanged?.Invoke(this, new DspFloatParamChangedEventArgs(0, clamped, 0.0f, 100.0f));
			}
		}

		/// <summary>
		///     <para>Gets or sets the percentage of output fed back into input.</para>
		///     <para>Range from <c>0.0 </c> through <c>100.0</c>. Default = <c>50.0</c>.</para>
		/// </summary>
		/// <value>
		///     The feedback.
		/// </value>
		public float Feedback
		{
			get => GetParameterFloat(1);
			set
			{
				var clamped = value.Clamp(0.0f, 100.0f);
				SetParameterFloat(1, clamped);
				FeedbackChanged?.Invoke(this, new DspFloatParamChangedEventArgs(1, clamped, 0.0f, 100.0f));
			}
		}

		/// <summary>
		///     <para>Gets or sets the delay for left channel, in milliseconds.</para>
		///     <para>Range from <c>1.0</c> through <c>2000.0</c>. Default = <c>500.0</c> ms.</para>
		/// </summary>
		/// <value>
		///     The left delay.
		/// </value>
		public float LeftDelay
		{
			get => GetParameterFloat(2);
			set
			{
				var clamped = value.Clamp(1.0f, 2000.0f);
				SetParameterFloat(2, clamped);
				LeftDelayChanged?.Invoke(this, new DspFloatParamChangedEventArgs(2, clamped, 1.0f, 2000.0f));
			}
		}

		/// <summary>
		///     <para>Gets or sets the delay for right channel, in milliseconds.</para>
		///     <para>Range from <c>1.0</c> through <c>2000.0</c>. Default = <c>500.0</c> ms.</para>
		/// </summary>
		/// <value>
		///     The right delay.
		/// </value>
		public float RightDelay
		{
			get => GetParameterFloat(3);
			set
			{
				var clamped = value.Clamp(1.0f, 2000.0f);
				SetParameterFloat(3, clamped);
				RightDelayChanged?.Invoke(this, new DspFloatParamChangedEventArgs(3, clamped, 1.0f, 2000.0f));
			}
		}

		/// <summary>
		///     Gets or sets the Value that specifies whether to swap left and right delays with each successive echo.
		///     <para>
		///         Ranges from <c>0.0</c> (equivalent to <c>false</c>) to <c>1.0</c> (equivalent to <c>true</c>), meaning no
		///         swap. Default = <c>0.0</c>.
		///     </para>
		/// </summary>
		/// <value>
		///     The pan delay.
		/// </value>
		[Obsolete("CURRENTLY NOT SUPPORTED.", true)]
		public float PanDelay
		{
			get => GetParameterFloat(4);
			set
			{
				var clamped = value.Clamp(0.0f, 1.0f);
				SetParameterFloat(4, clamped);
				PanDelayChanged?.Invoke(this, new DspFloatParamChangedEventArgs(4, clamped, 0.0f, 1.0f));
			}
		}

		/// <summary>
		///     Occurs when the <see cref="ItEcho.WetDryMix" /> property is changed.
		/// </summary>
		/// <seealso cref="DspFloatParamChangedEventArgs" />
		public event EventHandler<DspFloatParamChangedEventArgs> WetDryMixChanged;

		/// <summary>
		///     Occurs when the <see cref="ItEcho.Feedback" /> property is changed.
		/// </summary>
		/// <seealso cref="DspFloatParamChangedEventArgs" />
		public event EventHandler<DspFloatParamChangedEventArgs> FeedbackChanged;

		/// <summary>
		///     Occurs when the <see cref="ItEcho.LeftDelay" /> property is changed.
		/// </summary>
		/// <seealso cref="DspFloatParamChangedEventArgs" />
		public event EventHandler<DspFloatParamChangedEventArgs> LeftDelayChanged;

		/// <summary>
		///     Occurs when the <see cref="ItEcho.RightDelay" /> property is changed.
		/// </summary>
		/// <seealso cref="DspFloatParamChangedEventArgs" />
		public event EventHandler<DspFloatParamChangedEventArgs> RightDelayChanged;

		/// <summary>
		///     Occurs when the PanDelay property is changed.
		/// </summary>
		/// <seealso cref="DspFloatParamChangedEventArgs" />
		[Obsolete("CURRENTLY NOT SUPPORTED.", true)]
		public event EventHandler<DspFloatParamChangedEventArgs> PanDelayChanged;
	}
}