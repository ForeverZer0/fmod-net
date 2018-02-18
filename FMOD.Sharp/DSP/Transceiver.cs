using System;
using FMOD.Arguments;
using FMOD.Core;

namespace FMOD.DSP
{
	/// <summary>
	///     <para>This unit 'sends' and 'receives' from a selection of up to 32 different slots.</para>
	///     <para>
	///         It is like a <see cref="Send" />/<see cref="Return" /> but it uses global slots rather than returns as the
	///         destination. It also has other features. Multiple transceivers can receive from a single channel, or multiple
	///         transceivers can send to a single channel, or a combination of both.
	///     </para>
	/// </summary>
	/// <remarks>
	///     <para>
	///         The transceiver only transmits and receives to a global array of 32 channels. The transceiver can be set to
	///         receiver mode (like a return) and can receive the signal at a variable gain (<see cref="Level" />). The
	///         transceiver can also be set to transmit to a channel (like a send) and can transmit the signal with a variable
	///         gain (<see cref="Transceiver.Level" />).
	///     </para>
	///     <para>
	///         The <see cref="TransmitMode" /> is only applicable to the transmission format, not the receive format. This
	///         means this parameter is ignored in 'receive mode'. This allows receivers to receive at the speaker mode of the
	///         user's choice. Receiving from a mono channel, is cheaper than receiving from a surround channel for example.
	///         The 3 speaker modes <see cref="TransceiverSpeakerMode.Mono" />, <see cref="TransceiverSpeakerMode.Stereo" />,
	///         <see cref="TransceiverSpeakerMode.Surround" /> are stored as seperate buffers in memory for a tranmitter
	///         channel. To save memory, use 1 common speaker mode for a transmitter.
	///     </para>
	///     <para>
	///         The transceiver is double buffered to avoid desyncing of transmitters and receivers. This means there will be
	///         a 1 block delay on a receiver, compared to the data sent from a transmitter.
	///     </para>
	///     <para>Multiple transmitters sending to the same channel will be mixed together.</para>
	/// </remarks>
	/// <seealso cref="FMOD.Sharp.DSP.DspBase" />
	/// <seealso cref="Send" />
	/// <seealso cref="Return" />
	public class Transceiver : Dsp
	{
		/// <summary>
		///     Describes the speaker modes used with a <see cref="Transceiver" />.
		/// </summary>
		/// <remarks>
		///     <para>
		///         The speaker mode of a <see cref="Transceiver" /> buffer (of which there are up to 32 of) is determined
		///         automatically depending on the signal flowing through the transceiver effect, or it can be forced. Use a
		///         smaller fixed speaker mode buffer to save memory.
		///     </para>
		///     <para>Only relevant for transmitter dsps, as they control the format of the transceiver channel's buffer.</para>
		///     <para>
		///         If multiple transceivers transmit to a single buffer in different speaker modes, it will allocate memory for
		///         each speaker mode. This uses more memory than a single speaker mode. If there are multiple receivers reading
		///         from a channel with multiple speaker modes, it will read them all and mix them together.
		///     </para>
		///     <para>
		///         If the system's speaker mode is stereo or mono, it will not create a 3rd buffer, it will just use the
		///         mono/stereo speaker mode buffer.
		///     </para>
		/// </remarks>
		public enum TransceiverSpeakerMode
		{
			/// <summary>
			///     A transmitter will use whatever signal channel count coming in to the transmitter, to determine which speaker mode
			///     is allocated for the transceiver channel.
			/// </summary>
			Auto,

			/// <summary>
			///     A transmitter will always down-mix to a mono channel buffer.
			/// </summary>
			Mono,

			/// <summary>
			///     A transmitter will always upmix or downmix to a stereo channel buffer.
			/// </summary>
			Stereo,

			/// <summary>
			///     <para>A transmitter will always upmix or downmix to a surround channel buffer. </para>
			///     <para>Surround is the speaker mode of the system above stereo, so could be quad/surround/5.1/7.1.</para>
			/// </summary>
			Surround
		}

		/// <summary>
		///     Initializes a new instance of the <see cref="Transceiver" /> class.
		/// </summary>
		/// <param name="handle">The handle.</param>
		internal Transceiver(IntPtr handle) : base(handle)
		{
		}

		/// <summary>
		///     <para>
		///         Gets or sets a value indicating whether the <see cref="Transceiver" /> will behave like a "receiver" (
		///         <see cref="Return" />), and accepts data from a channel, or as a "trasmitter" (<see cref="Send" />).
		///     </para>
		///     <para><c>true</c> to be like a "receiver.</para>
		///     <para><c>false</c> to be like a "transmitter".</para>
		///     <para>Default = <c>false</c>.</para>
		/// </summary>
		/// <value>
		///     <c>true</c> if reciever; otherwise, <c>false</c>.
		/// </value>
		/// <seealso cref="TransmitModeChanged" />
		public bool TransmitMode
		{
			get => GetParameterBool(0);
			set
			{
				SetParameterBool(0, value);
				TransmitModeChanged?.Invoke(this, new DspBoolParamChangedEventArgs(0, value));
			}
		}

		/// <summary>
		///     <para>Gets or sets the gain to receive or transmit at in dB.</para>
		///     <para><c>-80.0</c> to <c>10.0</c>. Default = <c>0.0</c>.</para>
		/// </summary>
		/// <value>
		///     The level.
		/// </value>
		/// <seealso cref="LevelChanged" />
		public float Level
		{
			get => GetParameterFloat(1);
			set
			{
				var clamped = value.Clamp(-80.0f, 10.0f);
				SetParameterFloat(1, clamped);
				LevelChanged?.Invoke(this, new DspFloatParamChangedEventArgs(1, clamped, -80.0f, 10.0f));
			}
		}

		/// <summary>
		///     <para>
		///         Gets or sets the current global slot, shared by all <see cref="Transceiver" /> instances, that can be
		///         transmitted to or received from.
		///     </para>
		///     <para><c>0</c> to <c>31</c>. Default = <c>0</c>.</para>
		/// </summary>
		/// <value>
		///     The channel.
		/// </value>
		/// <seealso cref="ChannelChanged" />
		public int Channel
		{
			get => GetParameterInt(2);
			set
			{
				var clamped = value.Clamp(0, 31);
				SetParameterInt(2, clamped);
				ChannelChanged?.Invoke(this, new DspIntParamChangedEventArgs(2, clamped, 0, 32));
			}
		}

		/// <summary>
		///     <para>Gets or sets the speaker mode (transmitter mode only).</para>
		///     <para>Default = <see cref="TransceiverSpeakerMode.Auto" />.</para>
		/// </summary>
		/// <value>
		///     The speaker mode.
		/// </value>
		/// <seealso cref="SpeakerModeChanged" />
		public TransceiverSpeakerMode SpeakerMode
		{
			get => (TransceiverSpeakerMode) GetParameterInt(3);
			set
			{
				SetParameterInt(3, (int) value);
				SpeakerModeChanged?.Invoke(this, EventArgs.Empty);
			}
		}

		[Obsolete("DO NOT USE", true)]
		public byte[] OverallGain
		{
			get => GetParameterData(4);
			set => SetParameterData(4, value);
		}

		/// <summary>
		///     Occurs when <see cref="TransmitMode" /> property is changed.
		/// </summary>
		/// <seealso cref="TransmitMode" />
		/// <seealso cref="DspBoolParamChangedEventArgs" />
		public event EventHandler<DspBoolParamChangedEventArgs> TransmitModeChanged;

		/// <summary>
		///     Occurs when <see cref="Level" /> property is changed.
		/// </summary>
		/// <seealso cref="Level" />
		/// <seealso cref="DspFloatParamChangedEventArgs" />
		public event EventHandler<DspFloatParamChangedEventArgs> LevelChanged;

		/// <summary>
		///     Occurs when <see cref="Channel" /> property is changed.
		/// </summary>
		/// <seealso cref="Channel" />
		/// <seealso cref="DspIntParamChangedEventArgs" />
		public event EventHandler<DspIntParamChangedEventArgs> ChannelChanged;

		/// <summary>
		///     Occurs when <see cref="SpeakerMode" /> property is changed.
		/// </summary>
		/// <seealso cref="SpeakerMode" />
		public event EventHandler SpeakerModeChanged;
	}
}