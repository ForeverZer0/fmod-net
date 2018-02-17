#region License

// SpeakerMode.cs is distributed under the Microsoft Public License (MS-PL)
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
// Created 12:08 AM 02/04/2018

#endregion

#region Using Directives

using FMOD.Core;

#endregion
// TODO: Double-check XML comments
namespace FMOD.Enumerations
{
	/// <summary>
	///     These are speaker types defined for use with the <see cref="FmodSystem.SetSoftwareFormat" /> command.
	/// </summary>
	/// <remarks>
	///     <para>
	///         Note below the phrase <i>"sound channels"</i> is used. These are the subchannels inside a sound, they are not
	///         related and have nothing to do with the FMOD# class "Channel".
	///     </para>
	///     <para>
	///         For example a mono sound has one sound channel, a stereo sound has two sound channels, and an AC3 or six
	///         channel wav file have six <i>"sound channels"</i>.
	///     </para>
	///     <list type="nobullet">
	///         <item>
	///             <see cref="Raw" />
	///             <para>
	///                 This mode is for output devices that are not specifically mono/stereo/quad/surround/5.1 or 7.1, but
	///                 are multichannel.
	///             </para>
	///             <list type="bullet">
	///                 <item>
	///                     <para>
	///                         Use <see cref="FmodSystem.SetSoftwareFormat" /> to specify the number of speakers you want to
	///                         address, otherwise it will default to two (stereo).
	///                     </para>
	///                 </item>
	///                 <item>
	///                     <para>
	///                         Sound channels map to speakers sequentially, so a mono sound maps to output speaker 0, stereo
	///                         sound maps to output speaker 0 & 1.
	///                     </para>
	///                 </item>
	///                 <item>
	///                     <para>
	///                         The user assumes knowledge of the speaker order. <see cref="Speaker" /> enumerations may not
	///                         apply, so raw channel indices should be used.
	///                     </para>
	///                 </item>
	///                 <item>
	///                     <para>
	///                         Multichannel sounds map input channels to output channels 1:1.
	///                     </para>
	///                 </item>
	///                 <item>
	///                     <para>
	///                         <see cref="Channel.SetPan" /> does not work.
	///                     </para>
	///                 </item>
	///                 <item>
	///                     <para>
	///                         Speaker levels must be manually set with <see cref="ChannelControl.SetMixMatrix" />.
	///                     </para>
	///                 </item>
	///             </list>
	///         </item>
	///         <item>
	///             <see cref="Mono" />
	///             <para>This mode is for a single speaker arrangement.</para>
	///             <list type="bullet">
	///                 <item>
	///                     <para>
	///                         Panning does not work in this speaker mode.
	///                     </para>
	///                 </item>
	///                 <item>
	///                     <para>
	///                         Mono, stereo and multichannel sounds have each sound channel played on the one speaker unity.
	///                     </para>
	///                 </item>
	///                 <item>
	///                     <para>
	///                         Mix behavior for multichannel sounds can be set with <see cref="ChannelControl.SetMixMatrix" />
	///                         .
	///                     </para>
	///                 </item>
	///                 <item>
	///                     <para>
	///                         <see cref="ChannelControl.SetPan" /> does not work.
	///                     </para>
	///                 </item>
	///             </list>
	///         </item>
	///         <item>
	///             <see cref="Stereo" />
	///             <para>This mode is for two speaker arrangements that have a left and right speaker.</para>
	///             <list type="bullet">
	///                 <item>
	///                     <para>
	///                         Mono sounds default to an even distribution between left and right. They can be panned with
	///                         <see cref="ChannelControl.SetPan" />.
	///                     </para>
	///                 </item>
	///                 <item>
	///                     <para>
	///                         Stereo sounds default to the middle, or full left in the left speaker and full right in the
	///                         right speaker.
	///                     </para>
	///                 </item>
	///                 <item>
	///                     <para>
	///                         They can be cross faded with <see cref="ChannelControl.SetPan" />.
	///                     </para>
	///                 </item>
	///                 <item>
	///                     <para>
	///                         Multichannel sounds have each sound channel played on each speaker at unity.
	///                     </para>
	///                 </item>
	///                 <item>
	///                     <para>
	///                         Mix behavior for multichannel sounds can be set with <see cref="ChannelControl.SetMixMatrix" />
	///                         .
	///                     </para>
	///                 </item>
	///             </list>
	///         </item>
	///         <item>
	///             <see cref="Quad" />
	///             <para>
	///                 This mode is for four speaker arrangements that have a front left, front right, surround left and a
	///                 surround right speaker.
	///             </para>
	///             <list type="bullet">
	///                 <item>
	///                     <para>
	///                         Mono sounds default to an even distribution between front left and front right. They can be
	///                         panned with <see cref="ChannelControl.SetPan" />.
	///                     </para>
	///                 </item>
	///                 <item>
	///                     <para>
	///                         Stereo sounds default to the left sound channel played on the front left, and the right sound
	///                         channel played on the front right.
	///                     </para>
	///                 </item>
	///                 <item>
	///                     <para>
	///                         They can be cross faded with <see cref="ChannelControl.SetPan" />.
	///                     </para>
	///                 </item>
	///                 <item>
	///                     <para>
	///                         Multichannel sounds default to all of their sound channels being played on each speaker in
	///                         order of input.
	///                     </para>
	///                 </item>
	///                 <item>
	///                     <para>
	///                         Mix behavior for multichannel sounds can be set with <see cref="ChannelControl.SetMixMatrix" />
	///                         .
	///                     </para>
	///                 </item>
	///             </list>
	///         </item>
	///         <item>
	///             <see cref="Surround" />
	///             <para>This mode is for 5 speaker arrangements that have a left/right/center/surround left/surround right.</para>
	///             <list type="bullet">
	///                 <item>
	///                     <para>
	///                         Mono sounds default to the center speaker. They can be panned with
	///                         <see cref="ChannelControl.SetPan" />.
	///                     </para>
	///                 </item>
	///                 <item>
	///                     <para>
	///                         Stereo sounds default to the left sound channel played on the front left, and the right sound
	///                         channel played on the front right.
	///                     </para>
	///                 </item>
	///                 <item>
	///                     <para>
	///                         They can be cross faded with <see cref="ChannelControl.SetPan" />.
	///                     </para>
	///                 </item>
	///                 <item>
	///                     <para>
	///                         Multichannel sounds default to all of their sound channels being played on each speaker in
	///                         order of input.
	///                     </para>
	///                 </item>
	///                 <item>
	///                     <para>
	///                         Mix behavior for multichannel sounds can be set with <see cref="ChannelControl.SetMixMatrix" />
	///                         .
	///                     </para>
	///                 </item>
	///             </list>
	///         </item>
	///         <item>
	///             <see cref="FivePointOne" />
	///             <para>
	///                 This mode is for 5.1 speaker arrangements that have a left/right/center/surround left/surround right
	///                 and a subwoofer speaker.
	///             </para>
	///             <list type="bullet">
	///                 <item>
	///                     <para>
	///                         Mono sounds default to the center speaker. They can be panned with
	///                         <see cref="ChannelControl.SetPan" />.
	///                     </para>
	///                 </item>
	///                 <item>
	///                     <para>
	///                         Stereo sounds default to the left sound channel played on the front left, and the right sound
	///                         channel played on the front right.
	///                     </para>
	///                 </item>
	///                 <item>
	///                     <para>
	///                         They can be cross faded with <see cref="ChannelControl.SetPan" />.
	///                     </para>
	///                 </item>
	///                 <item>
	///                     <para>
	///                         Multichannel sounds default to all of their sound channels being played on each speaker in
	///                         order of input.
	///                     </para>
	///                 </item>
	///                 <item>
	///                     <para>
	///                         Mix behavior for multichannel sounds can be set with <see cref="ChannelControl.SetMixMatrix" />
	///                     </para>
	///                 </item>
	///             </list>
	///         </item>
	///         <item>
	///             <see cref="SevenPointOne" />
	///             <para>
	///                 This mode is for 7.1 speaker arrangements that have a left/right/center/surround left/surround
	///                 right/rear left/rear right and a subwoofer speaker.
	///             </para>
	///             <list type="bullet">
	///                 <item>
	///                     <para>
	///                         Mono sounds default to the center speaker. They can be panned with
	///                         <see cref="ChannelControl.SetPan" />.
	///                     </para>
	///                 </item>
	///                 <item>
	///                     <para>
	///                         Stereo sounds default to the left sound channel played on the front left, and the right sound
	///                         channel played on the front right.
	///                     </para>
	///                 </item>
	///                 <item>
	///                     <para>
	///                         They can be cross faded with <see cref="ChannelControl.SetPan" />.
	///                     </para>
	///                 </item>
	///                 <item>
	///                     <para>
	///                         Multichannel sounds default to all of their sound channels being played on each speaker in
	///                         order of input.
	///                     </para>
	///                 </item>
	///                 <item>
	///                     <para>
	///                         Mix behavior for multichannel sounds can be set with <see cref="ChannelControl.SetMixMatrix" />
	///                     </para>
	///                 </item>
	///             </list>
	///         </item>
	///     </list>
	/// </remarks>
	/// <seealso cref="FmodSystem.SoftwareFormat" />
	/// <seealso cref="Dsp.ChannelFormat" />
	/// <seealso cref="ChannelControl.SetPan" />
	/// <seealso cref="ChannelControl.SetMixMatrix" />
	public enum SpeakerMode
	{
		/// <summary>
		///     Default speaker mode for the chosen output mode which will resolve after
		///     <see cref="O:FMOD.Core.FmodSystem.Initialize" />.
		/// </summary>
		Default,

		/// <summary>
		///     <para>Assume there is no special mapping from a given channel to a speaker, channels map 1:1 in order.</para>
		///     <para>Use <see cref="FmodSystem.SetSoftwareFormat" /> to specify the speaker count. </para>
		/// </summary>
		Raw,

		/// <summary>
		///     Single speaker setup (monaural).
		/// </summary>
		Mono,

		/// <summary>
		///     Two speaker setup (stereo) front left, front right.
		/// </summary>
		Stereo,

		/// <summary>
		///     Four speaker setup (4.0) front left, front right, surround left, surround right.
		/// </summary>
		Quad,

		/// <summary>
		///     Five speaker setup (5.0) front left, front right, center, surround left, surround right.
		/// </summary>
		Surround,

		/// <summary>
		///     Six speaker setup (5.1) front left, front right, center, low-frequency, surround left, surround right.
		/// </summary>
		FivePointOne,

		/// <summary>
		///     Eight speaker setup (7.1) front left, front right, center, low-frequency, surround left, surround right, back left,
		///     back right.
		/// </summary>
		SevenPointOne,

		/// <summary>
		///     Twelve speaker setup (7.1.4) front left, front right, center, low-frequency, surround left, surround right, back
		///     left, back right, top front left, top front right, top back left, top back right.
		/// </summary>
		SevenPointOnePointFour,

		/// <summary>
		///     Maximum number of speaker modes supported.
		/// </summary>
		Max
	}
}