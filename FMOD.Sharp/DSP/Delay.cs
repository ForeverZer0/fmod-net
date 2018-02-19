using System;
using FMOD.Arguments;
using FMOD.Core;

namespace FMOD.DSP
{
	/// <inheritdoc />
	/// <summary>
	///     Simple delay filter.
	/// </summary>
	/// <remarks>
	///     <alert class="note">
	///         <para>
	///             Every time <see cref="MaximumDelay" /> is changed, the plugin re-allocates the delay buffer. This means the
	///             delay will
	///             dissapear at that time while it refills its new buffer.
	///         </para>
	///         <para>A larger <see cref="MaximumDelay" /> results in larger amounts of memory allocated.</para>
	///         <para>
	///             Channel delays above MaxDelay will be clipped to <see cref="MaximumDelay" /> and the delay buffer will
	///             not be resized.
	///         </para>
	///     </alert>
	/// </remarks>
	/// <seealso cref="T:FMOD.Sharp.Dsp" />
	public class Delay : Dsp
	{
		/// <summary>
		///     Initializes a new instance of the <see cref="Delay" /> class.
		/// </summary>
		/// <param name="handle">The handle.</param>
		internal Delay(IntPtr handle) : base(handle)
		{
		}

		/// <summary>
		///     Gets or sets the maximum delay, in milliseconds.
		///     <para>Range from <c>0.0</c> to <c>10000.0</c>.</para>
		/// </summary>
		/// <value>
		///     The maximum delay.
		/// </value>
		public float MaximumDelay
		{
			get => GetParameterFloat(16);
			set
			{
				var clamped = value.Clamp(0.0f, 10000.0f);
				SetParameterFloat(16, clamped);
				MaximumDelayChanged?.Invoke(this, new FloatParamEventArgs(16, clamped, 0.0f, 10000.0f));
			}
		}

		/// <summary>
		///     Occurs when the delay is changed on a channel.
		/// </summary>
		/// <seealso cref="DspDelayChangedEventArgs" />
		/// <seealso cref="SetDelay" />
		public event EventHandler<DspDelayChangedEventArgs> DelayChanged;

		/// <summary>
		///     Occurs when the <see cref="MaximumDelay" /> proeprty is changed.
		/// </summary>
		/// <seealso cref="FloatParamEventArgs" />
		public event EventHandler<FloatParamEventArgs> MaximumDelayChanged;

		/// <summary>
		///     Gets the delay valeu for the specified channel.
		/// </summary>
		/// <param name="channel">The channel. <c>0</c> to <c>15</c>.</param>
		/// <returns>The delay value.</returns>
		public float GetDelay(int channel)
		{
			return GetParameterFloat(channel.Clamp(0, 15));
		}

		/// <summary>
		///     Sets the delay on the specified channel, in milliseconds.
		///     <para>Range from <c>0.0</c> to <c>10000.0</c>.</para>
		/// </summary>
		/// <param name="channel">The channel to set delay of.</param>
		/// <param name="delay">The delay.</param>
		public void SetDelay(int channel, float delay)
		{
			var clamped = delay.Clamp(0.0f, 10000.0f);
			SetParameterFloat(channel, clamped);
			DelayChanged?.Invoke(this, new DspDelayChangedEventArgs(channel, clamped));
		}
	}
}