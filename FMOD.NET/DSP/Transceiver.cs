#region License

// Transceiver.cs is distributed under the Microsoft Public License (MS-PL)
// 
// Copyright (c) 2018,  Eric Freed
// All Rights Reserved.
// 
// This license governs use of the accompanying software. If you use the software, you
// accept this license. If you do not accept the license, do not use the software.
// 
// 1. Definitions
// The terms "reproduce," "reproduction," "derivative works," and "distribution" have the
// same meaning here as under U.S. copyright law.
// A "contribution" is the original software, or any additions or changes to the software.
// A "contributor" is any person that distributes its contribution under this license.
// "Licensed patents" are a contributor's patent claims that read directly on its contribution.
// 
// 2. Grant of Rights
// (A) Copyright Grant- Subject to the terms of this license, including the license conditions 
// and limitations in section 3, each contributor grants you a non-exclusive, worldwide, royalty-free 
// copyright license to reproduce its contribution, prepare derivative works of its contribution, and 
// distribute its contribution or any derivative works that you create.
// 
// (B) Patent Grant- Subject to the terms of this license, including the license conditions and 
// limitations in section 3, each contributor grants you a non-exclusive, worldwide, royalty-free license
//  under its licensed patents to make, have made, use, sell, offer for sale, import, and/or otherwise 
// dispose of its contribution in the software or derivative works of the contribution in the software.
// 
// 3. Conditions and Limitations
// (A) No Trademark License- This license does not grant you rights to use any contributors' name, 
// logo, or trademarks.
// 
// (B) If you bring a patent claim against any contributor over patents that you claim are infringed by 
// the software, your patent license from such contributor to the software ends automatically.
// 
// (C) If you distribute any portion of the software, you must retain all copyright, patent, trademark, and
//  attribution notices that are present in the software.
// 
// (D) If you distribute any portion of the software in source code form, you may do so only under this 
// license by including a complete copy of this license with your distribution. If you distribute any portion
//  of the software in compiled or object code form, you may only do so under a license that complies 
// with this license.
// 
// (E) The software is licensed "as-is." You bear the risk of using it. The contributors give no express 
// warranties, guarantees or conditions. You may have additional consumer rights under your local laws 
// which this license cannot change. To the extent permitted under your local laws, the contributors 
// exclude the implied warranties of merchantability, fitness for a particular purpose and non-infringement.
// 
// Created 10:47 PM 02/14/2018

#endregion

#region Using Directives

using System;
using FMOD.Arguments;
using FMOD.Core;

#endregion

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
	/// <seealso cref="FMOD.Core.Dsp" />
	/// <seealso cref="Send" />
	/// <seealso cref="Return" />
	public class Transceiver : Dsp
	{
		#region Events

		/// <summary>
		///     Occurs when <see cref="Channel" /> property is changed.
		/// </summary>
		/// <seealso cref="Channel" />
		/// <seealso cref="IntParamEventArgs" />
		public event EventHandler<IntParamEventArgs> ChannelChanged;

		/// <summary>
		///     Occurs when <see cref="Level" /> property is changed.
		/// </summary>
		/// <seealso cref="Level" />
		/// <seealso cref="FloatParamEventArgs" />
		public event EventHandler<FloatParamEventArgs> LevelChanged;

		/// <summary>
		///     Occurs when <see cref="SpeakerMode" /> property is changed.
		/// </summary>
		/// <seealso cref="SpeakerMode" />
		public event EventHandler SpeakerModeChanged;

		/// <summary>
		///     Occurs when <see cref="TransmitMode" /> property is changed.
		/// </summary>
		/// <seealso cref="TransmitMode" />
		/// <seealso cref="BoolParamEventArgs" />
		public event EventHandler<BoolParamEventArgs> TransmitModeChanged;

		#endregion

		#region Constructors

		/// <summary>
		///     Initializes a new instance of the <see cref="Transceiver" /> class.
		/// </summary>
		/// <param name="handle">The handle.</param>
		protected Transceiver(IntPtr handle) : base(handle)
		{
		}

		#endregion

		#region Properties

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
				TransmitModeChanged?.Invoke(this, new BoolParamEventArgs(0, value));
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
				LevelChanged?.Invoke(this, new FloatParamEventArgs(1, clamped, -80.0f, 10.0f));
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
				ChannelChanged?.Invoke(this, new IntParamEventArgs(2, clamped, 0, 32));
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

		#endregion

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
	}
}