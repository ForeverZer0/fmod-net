#region License

// ChannelMix.cs is distributed under the Microsoft Public License (MS-PL)
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
// Created 12:18 PM 02/13/2018

#endregion

#region Using Directives

using System;
using FMOD.Arguments;
using FMOD.Core;
using FMOD.Enumerations;

#endregion

namespace FMOD.DSP
{
	/// <inheritdoc />
	/// <summary>
	///     This unit provides per signal channel gain, and output channel mapping to allow one multichannel signal made up of many groups of signals to map to a single output signal.
	/// </summary>
	/// <seealso cref="FMOD.Core.Dsp" />
	/// <seealso cref="ChannelMix.Output" />
	public class ChannelMix : Dsp
	{
		#region Events

		/// <summary>
		///     Occurs when a channel's gain value is changed.
		/// </summary>
		/// <seealso cref="SetChannelGain" />
		/// <seealso cref="DspChannelMixGainChangedEventArgs" />
		public event EventHandler<DspChannelMixGainChangedEventArgs> ChannelGainChanged;

		/// <summary>
		///     Occurs when the <see cref="OutputGrouping" /> property is changed.
		/// </summary>
		public event EventHandler OutputFormatChanged;

		#endregion

		#region Constructors

		/// <summary>
		///     Initializes a new instance of the <see cref="ChannelMix" /> class.
		/// </summary>
		/// <param name="handle">The handle.</param>
		protected ChannelMix(IntPtr handle) : base(handle)
		{
		}

		#endregion

		#region Properties

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

		#endregion

		#region Methods

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

		#endregion

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
	}
}