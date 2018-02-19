using System;
using FMOD.Arguments;
using FMOD.Core;

namespace FMOD.DSP
{
	/// <inheritdoc />
	/// <summary>
	///     Applies a basic "echo" effect to a sound.
	/// </summary>
	/// <seealso cref="T:FMOD.Sharp.Dsp" />
	public class Echo : Dsp
	{
		/// <summary>
		///     Initializes a new instance of the <see cref="Echo" /> class.
		/// </summary>
		/// <param name="handle">The handle.</param>
		internal Echo(IntPtr handle) : base(handle)
		{
		}

		/// <summary>
		///     <para>Gets or sets the echo delay in ms.</para>
		///     <para><c>10.0</c> to <c>5000.0</c>. Default = <c>500.0</c>.</para>
		/// </summary>
		/// <value>
		///     The delay.
		/// </value>
		/// <remarks>
		///     <alert class="note">
		///         <para>
		///             Every time the delay is changed, the plugin re-allocates the echo buffer. This means the echo will
		///             dissapear at that time while it refills its new buffer.
		///         </para>
		///     </alert>
		///     <para>Larger echo delays result in larger amounts of memory allocated.</para>
		/// </remarks>
		public float Delay
		{
			get => GetParameterFloat(0);
			set
			{
				var clamped = value.Clamp(5.0f, 5000.0f);
				SetParameterFloat(0, clamped);
				DelayChanged?.Invoke(this, new FloatParamEventArgs(0, clamped, 5.0f, 5000.0f));
			}
		}

		/// <summary>
		///     <para>Gets or sets the echo decay per delay.</para>
		///     <para><c>0</c> to <c>100</c>. </para>
		///     <para><c>100.0</c> = No decay, <c>0.0</c> = total decay (ie simple 1 line delay). Default = <c>50.0</c>.</para>
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
				FeedbackChanged?.Invoke(this, new FloatParamEventArgs(1, clamped, 0.0f, 100.0f));
			}
		}

		/// <summary>
		///     <para>Gets or sets the original sound volume in dB.</para>
		///     <para><c>-80.0</c> to <c>10.0</c>. Default = <c>0</c>.</para>
		/// </summary>
		/// <value>
		///     The dry level.
		/// </value>
		public float DryLevel
		{
			get => GetParameterFloat(2);
			set
			{
				var clamped = value.Clamp(-80.0f, 10.0f);
				SetParameterFloat(2, clamped);
				DryLevelChanged?.Invoke(this, new FloatParamEventArgs(2, clamped, -80.0f, 10.0f));
			}
		}

		/// <summary>
		///     <para>Gets or sets the volume of echo signal to pass to output in dB. </para>
		///     <para><c>-80.0</c> to <c>10.0</c>. Default = <c>0</c>.</para>
		/// </summary>
		/// <value>
		///     The wet level.
		/// </value>
		public float WetLevel
		{
			get => GetParameterFloat(3);
			set
			{
				var clamped = value.Clamp(-80.0f, 10.0f);
				SetParameterFloat(3, clamped);
				WetLevelChanged?.Invoke(this, new FloatParamEventArgs(3, clamped, -80.0f, 10.0f));
			}
		}

		/// <summary>
		///     Occurs when <see cref="Delay" /> property is changed.
		/// </summary>
		/// <seealso cref="FloatParamEventArgs"/>
		public event EventHandler<FloatParamEventArgs> DelayChanged;

		/// <summary>
		///     Occurs when <see cref="Feedback" /> property is changed.
		/// </summary>
		/// <seealso cref="FloatParamEventArgs"/>
		public event EventHandler<FloatParamEventArgs> FeedbackChanged;

		/// <summary>
		///     Occurs when <see cref="DryLevel" /> property is changed.
		/// </summary>
		/// <seealso cref="FloatParamEventArgs"/>
		public event EventHandler<FloatParamEventArgs> DryLevelChanged;

		/// <summary>
		///     Occurs when <see cref="WetLevel" /> property is changed.
		/// </summary>
		/// <seealso cref="FloatParamEventArgs"/>
		public event EventHandler<FloatParamEventArgs> WetLevelChanged;
	}
}