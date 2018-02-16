using System;
using FMOD.Core;
using FMOD.Enumerations;

namespace FMOD.DSP
{
	/// <inheritdoc />
	/// <summary>
	///     Provides functions for mixing gain levels and output grouping on speakers.
	/// </summary>
	/// <seealso cref="T:FMOD.Sharp.Dsp" />
	/// <seealso cref="T:FMOD.Sharp.Dsps.ChannelMix.Output" />
	public class ChannelMix : Dsp
	{
		/// <summary>
		///     Parameter types for the <see cref="ChannelMix.OutputGrouping" /> parameter for <see cref="ChannelMix" />.
		/// </summary>
		/// <seealso cref="ChannelMix" />
		/// <seealso cref="ChannelMix.OutputGrouping" />
		public enum Output
		{
			/// <summary>
			///     <para>Output channel count = input channel count.</para>
			///     <para><b>Mapping:</b> See <see cref="Speaker" /> enumeration.</para>
			/// </summary>
			Default,

			/// <summary>
			///     Output channel count = <c>1</c>.
			///     <para>
			///         <b>Mapping:</b> <i>Mono, Mono, Mono, Mono, Mono, Mono, ...</i> (each channel all the way up to
			///         <see cref="Constants.MAX_CHANNELS" /> channels are treated as if they were mono).
			///     </para>
			/// </summary>
			AllMono,

			/// <summary>
			///     Output channel count = <c>2</c>.
			///     <para>
			///         <b>Mapping:</b> <i>Left, Right, Left, Right, Left, Right, ...</i> (each pair of channels is treated as stereo
			///         all the way up to <see cref="Constants.MAX_CHANNELS" /> channels).
			///     </para>
			/// </summary>
			AllStereo,

			/// <summary>
			///     <para>Output channel count = <c>4</c>.</para>
			///     <para><b>Mapping:</b> Repeating pattern of <i>Front Left, Front Right, Surround Left, Surround Right.</i></para>
			/// </summary>
			AllQuad,

			/// <summary>
			///     <para>Output channel count = <c>6</c>.</para>
			///     <para>
			///         <b>Mapping:</b> Repeating pattern of
			///         <i>Front Left, Front Right, Center, Low-Frequency, Surround Left, Surround Right.</i>
			///     </para>
			/// </summary>
			All5Point1,

			/// <summary>
			///     <para>Output channel count = <c>8</c>. </para>
			///     <para>
			///         <b>Mapping:</b> Repeating pattern of
			///         <i>Front Left, Front Right, Center, Low-Frequency, Surround Left, Surround Right, Back Left, Back Right. </i>
			///     </para>
			/// </summary>
			All7Point1,

			/// <summary>
			///     <para>Output channel count = <c>6</c>.</para>
			///     <para><b>Mapping:</b> Repeating pattern of <i>Low_Frequency</i> in a 5.1 output signal.</para>
			/// </summary>
			AllLowFrequency
		}

		/// <summary>
		///     Initializes a new instance of the <see cref="ChannelMix" /> class.
		/// </summary>
		/// <param name="handle">The handle.</param>
		internal ChannelMix(IntPtr handle) : base(handle)
		{
		}

		/// <summary>
		///     Gets or sets the output grouping.
		/// </summary>
		/// <value>
		///     The output grouping.
		/// </value>
		/// <seealso cref="Output" />
		public Output OutputGrouping
		{
			get => (Output) GetParameterInt(0);
			set
			{
				SetParameterInt(0, (int) value);
				OutputFormatChanged?.Invoke(this, EventArgs.Empty);
			}
		}

		/// <summary>
		///     Occurs when the <see cref="OutputGrouping" /> property is changed.
		/// </summary>
		public event EventHandler OutputFormatChanged;

		/// <summary>
		///     Occurs when a channel's gain value is changed.
		/// </summary>
		/// <seealso cref="SetChannelGain" />
		/// <seealso cref="DspChannelMixGainChangedEventArgs"/>
		public event EventHandler<DspChannelMixGainChangedEventArgs> ChannelGainChanged;

		/// <summary>
		///     Gets the specified channel's gain in dB.
		/// </summary>
		/// <param name="channelIndex">Index of the channel.</param>
		/// <returns>The channel's gain in dB, a value between <c>-80.0</c> and <c>10.0</c>.</returns>
		public float GetChannelGain(int channelIndex)
		{
			return GetParameterFloat(channelIndex + 1);
		}

		/// <summary>
		///     <para>Sets the specified channel's gain in dB.</para>
		///     <para>Valid values range from <c>-80.0</c> to <c>10.0</c>.</para>
		///     <para>Out of range values will be automatically clamped.</para>
		/// </summary>
		/// <param name="channelIndex">Index of the channel.</param>
		/// <param name="gain">The gain. (<c>-80.0 ... 10.0</c>)</param>
		public void SetChannelGain(int channelIndex, float gain)
		{
			var clamped = gain.Clamp(-80.0f, 10.0f);
			SetParameterFloat(channelIndex + 1, clamped);
			ChannelGainChanged?.Invoke(this, new DspChannelMixGainChangedEventArgs(channelIndex + 1, channelIndex, clamped));
		}
	}
}