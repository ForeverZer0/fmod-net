#region License

// ChannelFormat.cs is distributed under the Microsoft Public License (MS-PL)
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
// Created 4:52 PM 02/05/2018

#endregion

#region Using Directives

using FMOD.Core;
using FMOD.Enumerations;

#endregion

namespace FMOD.Data
{
	/// <summary>
	///     <para>
	///         Describes the signal format of a <see cref="Dsp" /> unit so that the signal is processed on the speakers
	///         specified.
	///     </para>
	///     <para>
	///         Also defines the number of channels in the unit that a read callback will process, and the output signal of
	///         the unit.
	///     </para>
	/// </summary>
	/// <seealso cref="Dsp" />
	/// <seealso cref="Dsp.ChannelFormat" />
	/// <seealso cref="FMOD.Enumerations.ChannelMask" />
	/// <seealso cref="FMOD.Enumerations.SpeakerMode" />
	public class ChannelFormat
	{
		/// <summary>
		///     Gets or sets the channel mask, series of flags specified by <see cref="FMOD.Enumerations.ChannelMask" /> to
		///     determine which speakers are represented by the channels in the signal. .
		/// </summary>
		/// <value>
		///     The channel mask.
		/// </value>
		public ChannelMask ChannelMask { get; set; }

		/// <summary>
		///     <para>Gets or sets number of channels to be processed on this unit and sent to the outputs connected to it. </para>
		///     <para>Maximum of <see cref="Constants.MAX_CHANNELS" />.</para>
		/// </summary>
		/// <value>
		///     The channel count.
		/// </value>
		/// <remarks>
		///     Setting the number of channels on a unit will force a down or up mix to that channel count before processing the
		///     DSP read callback. This channel count is then sent to the outputs of the unit.
		/// </remarks>
		public int ChannelCount { get; set; }

		/// <summary>
		///     Gets or sets the source speaker mode where the signal came from. See remarks.
		/// </summary>
		/// <value>
		///     The speaker mode.
		/// </value>
		/// <remarks>
		///     <para>
		///         This property is informational, when <see cref="ChannelMask" /> describes what bits are active, and
		///         <see cref="ChannelCount" /> describes how many channels are in a buffer, this describes where the channels
		///         originated from. For example if <see cref="ChannelCount" /> = <c>2</c> then this could describe for the DSP if
		///         the original signal started from a stereo signal or a 5.1 signal.
		///     </para>
		///     <para>
		///         It could also describe the signal as all monaural, for example if <see cref="ChannelCount" /> was 16 and the
		///         speakermode was <see cref="FMOD.Enumerations.SpeakerMode.Mono" />.
		///     </para>
		/// </remarks>
		public SpeakerMode SpeakerMode { get; set; }
	}
}