#region License

// Mode.cs is distributed under the Microsoft Public License (MS-PL)
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
// Created 11:21 PM 02/03/2018

#endregion

#region Using Directives

using System;
using System.ComponentModel;
using FMOD.NET.Core;
using FMOD.NET.Structures;

#endregion

namespace FMOD.NET.Enumerations
{
	/// <summary>
	///     <see cref="Sound" /> description bitfields, bitwise OR them together for loading and describing sounds.
	/// </summary>
	/// <remarks>
	///     <para>
	///         By default a sound will open as a static sound that is decompressed fully into memory to PCM. (ie equivalent of
	///         <see cref="CreateSample" />).<lineBreak />
	///         To have a sound stream instead, use <see cref="CreateSample" />, or use the wrapper function
	///         <see cref="O:FMOD.NET.Core.FmodSystem.CreateStream" />. Some opening modes (ie <see cref="OpenUser" />,
	///         <see cref="OpenMemory" />, <see cref="OpenMemoryPoint" />, <see cref="OpenRaw" />) will need extra information.
	///         <lineBreak />
	///         This can be provided using the <see cref="CreateSoundExInfo" /> structure.<lineBreak />
	///         Specifying <see cref="OpenMemoryPoint" /> will POINT to your memory rather allocating its own sound buffers and
	///         duplicating it internally.<lineBreak />
	///         <b>
	///             <u>
	///                 This means you cannot free the memory while FMOD is using it, until after <see cref="Sound.Dispose" />
	///                 is called.
	///             </u>
	///         </b>
	///         <lineBreak />
	///         With <see cref="OpenMemoryPoint" />, for PCM formats, only WAV, FSB, and RAW are supported. For compressed
	///         formats, only those formats supported by <see cref="CreateCompressedSample" /> are supported.<lineBreak />
	///         With <see cref="OpenMemoryPoint" /> and <see cref="OpenRaw" /> or PCM, if using them together, note that you
	///         must pad the data on each side by 16 bytes. This is so fmod can modify the ends of the data for
	///         looping/interpolation/mixing purposes. If a wav file, you will need to insert silence, and then reset loop
	///         points to stop the playback from playing that silence.
	///     </para>
	///     <para>
	///         <b>Xbox 360 Memory:</b> On Xbox 360 Specifying <see cref="OpenMemoryPoint" /> to a virtual memory address will
	///         cause <see cref="Result.InvalidHandle" /> to be returned. Use physical memory only for this functionality.
	///     </para>
	///     <para>
	///         <see cref="LowMemory" /> is used on a sound if you want to minimize the memory overhead, by having <b>FMOD</b>
	///         not allocate memory for certain features that are not likely to be used in a game environment. An example is
	///         <see cref="Sound.Name" />, which saves 256 bytes per sound.
	///     </para>
	/// </remarks>
	/// <seealso cref="O:FMOD.NET.Core.FmodSystem.CreateSound" />
	/// <seealso cref="O:FMOD.NET.Core.FmodSystem.CreateStream" />
	/// <seealso cref="Sound.Mode" />
	/// <seealso cref="ChannelControl.Mode" />
	/// <seealso cref="Sound.CustomRolloff3D" />
	/// <seealso cref="ChannelControl.CustomRolloff3D" />
	/// <seealso cref="O:FMOD.NET.Core.Sound.GetOpenState" />
	[Flags]
	public enum Mode : uint
	{
		/// <summary>
		///     Default for all modes in enumeration.
		///     <para>
		///         <see cref="LoopOff" />, <see cref="TwoD" />, <see cref="WorldRelative3D" />, <see cref="InverseRolloff3D" />
		///     </para>
		/// </summary>
		Default = 0x00000000,

		/// <summary>
		///     For non looping sounds. (DEFAULT). Overrides <see cref="LoopNormal" /> / <see cref="LoopBidi" />.
		/// </summary>
		LoopOff = 0x00000001,

		/// <summary>
		///     For forward looping sounds.
		/// </summary>
		LoopNormal = 0x00000002,

		/// <summary>
		///     For bidirectional looping sounds. (only works on software mixed static sounds).
		/// </summary>
		LoopBidi = 0x00000004,

		/// <summary>
		///     Ignores any 3d processing. (DEFAULT).
		/// </summary>
		TwoD = 0x00000008,

		/// <summary>
		///     Makes the sound positionable in 3D. Overrides <see cref="TwoD" />.
		/// </summary>
		ThreeD = 0x00000010,

		/// <summary>
		///     Decompress at runtime, streaming from the source provided (ie from disk). Overrides <see cref="CreateSample" /> and
		///     <see cref="CreateCompressedSample" />. Note a stream can only be played once at a time due to a stream only having
		///     1 stream buffer and file handle. Open multiple streams to have them play concurrently.
		/// </summary>
		CreateStream = 0x00000080,

		/// <summary>
		///     Decompress at loadtime, decompressing or decoding whole file into memory as the target sample format (ie PCM).
		///     Fastest for playback and most flexible.
		/// </summary>
		CreateSample = 0x00000100,

		/// <summary>
		///     Load MP2/MP3/FADPCM/IMAADPCM/Vorbis/AT9 or XMA into memory and leave it compressed. Vorbis/AT9/FADPCM encoding only
		///     supported in the .FSB container format. During playback the <b>FMOD</b> software mixer will decode it in realtime
		///     as a "compressed sample". Overrides <see cref="CreateSample" />. If the sound data is not one of the supported
		///     formats, it will behave as if it was created with <see cref="CreateSample" /> and decode the sound into PCM.
		/// </summary>
		CreateCompressedSample = 0x00000200,

		/// <summary>
		///     <para>
		///         Opens a user created static sample or stream. Use <see cref="CreateSoundExInfo" /> to specify format and/or
		///         read callbacks.
		///     </para>
		///     <para>If a user created "sample" is created with no read callback, the sample will be empty.</para>
		///     <para>
		///         Use <see cref="Sound.Lock" /> and <see cref="Sound.Unlock" /> to place sound data into the sound if this is
		///         the case.
		///     </para>
		/// </summary>
		OpenUser = 0x00000400,

		/// <summary>
		///     The <i>source</i> argument will be interpreted as a pointer to memory instead of filename for creating sounds. Use
		///     <see cref="CreateSoundExInfo" /> to specify length. If used with <see cref="CreateSample" /> or
		///     <see cref="CreateCompressedSample" />, <b>FMOD</b> duplicates the memory into its own buffers. Your own buffer can
		///     be freed after open.
		///     <para>
		///         If used with <see cref="CreateStream" />, <b>FMOD</b> will stream out of the buffer whose pointer you passed
		///         in. In this case, your own buffer should not be freed until you have finished with and released the stream.
		///     </para>
		/// </summary>
		OpenMemory = 0x00000800,

		/// <summary>
		///     <para>The <i>source</i> will be interpreted as a pointer to memory instead of filename for creating sounds.</para>
		///     <para>
		///         Use <see cref="CreateSoundExInfo" /> to specify length. This differs to <see cref="OpenMemory" /> in that it
		///         uses the memory as is, without duplicating the memory into its own buffers. Cannot be freed after open, only
		///         after <see cref="Sound.Dispose" />.
		///     </para>
		///     <para>Will not work if the data is compressed and <see cref="CreateCompressedSample" /> is not used.</para>
		/// </summary>
		OpenMemoryPoint = 0x10000000,

		/// <summary>
		///     Will ignore file format and treat as raw PCM. Use <see cref="CreateSoundExInfo" /> to specify format.
		///     <para>
		///         Requires at least <see cref="CreateSoundExInfo.DefaultFrequency" />,
		///         <see cref="CreateSoundExInfo.ChannelCount" />, and <see cref="CreateSoundExInfo.Format" /> to be specified
		///         before it will open.
		///     </para>
		///     <para>Must be little endian data.</para>
		/// </summary>
		OpenRaw = 0x00001000,

		/// <summary>
		///     Just open the file, don't prebuffer or read. Good for fast opens for info, or when
		///     <see cref="O:FMOD.NET.Core.Sound.ReadData" /> is to be used.
		/// </summary>
		OpenOnly = 0x00002000,

		/// <summary>
		///     For <see cref="O:FMOD.NET.Core.FmodSystem.CreateSound" /> - for accurate <see cref="Sound.GetLength" /> /
		///     <see cref="Channel.SetPosition" /> on VBR MP3, and MOD/S3M/XM/IT/MIDI files. Scans file first, so takes
		///     longer to open. <see cref="OpenOnly" /> does not affect this.
		/// </summary>
		AccurateTime = 0x00004000,

		/// <summary>
		///     For corrupted / bad MP3 files. This will search all the way through the file until it hits a valid MPEG header.
		///     Normally only searches for 4k.
		/// </summary>
		MpegSearch = 0x00008000,

		/// <summary>
		///     <para>For opening sounds and getting streamed subsounds (seeking) asyncronously.</para>
		///     <para>
		///         Use <see cref="O:FMOD.NET.Core.Sound.GetOpenState" /> to poll the state of the sound as it opens or retrieves the
		///         subsound in the background.
		///     </para>
		/// </summary>
		NonBlocking = 0x00010000,

		/// <summary>
		///     Unique sound, can only be played one at a time.
		/// </summary>
		Unique = 0x00020000,

		/// <summary>
		///     Make the sound's position, velocity and orientation relative to the listener.
		/// </summary>
		HeadRelative3D = 0x0004000,

		/// <summary>
		///     Make the sound's position, velocity and orientation absolute (relative to the world). (DEFAULT)
		/// </summary>
		WorldRelative3D = 0x00080000,

		/// <summary>
		///     This sound will follow the inverse rolloff model where mindistance = full volume, maxdistance = where sound stops
		///     attenuating, and rolloff is fixed according to the global rolloff factor. (DEFAULT)
		/// </summary>
		InverseRolloff3D = 0x00100000,

		/// <summary>
		///     This sound will follow a linear rolloff model where mindistance = full volume, maxdistance = silence.
		/// </summary>
		LinearRolloff3D = 0x00200000,

		/// <summary>
		///     This sound will follow a linear-square rolloff model where mindistance = full volume, maxdistance = silence.
		/// </summary>
		LinearSquareRolloff3D = 0x00400000,

		/// <summary>
		///     This sound will follow the inverse rolloff model at distances close to mindistance and a linear-square rolloff
		///     close to maxdistance.
		/// </summary>
		InverseTaperedRolloff3D = 0x00800000,

		/// <summary>
		///     This sound will follow a rolloff model defined by <see cref="Sound.CustomRolloff3D" /> /
		///     <see cref="ChannelControl.CustomRolloff3D" />.
		/// </summary>
		CustomRolloff3D = 0x04000000,

		/// <summary>
		///     <para>Is not affect by geometry occlusion.</para>
		///     <para>
		///         If not specified in <see cref="Sound.Mode" />, or <see cref="ChannelControl.Mode" />, the flag is cleared and
		///         it is affected by geometry again.
		///     </para>
		/// </summary>
		IgnoreGeometry3D = 0x40000000,

		/// <summary>
		///     Skips id3v2/asf/etc tag checks when opening a sound, to reduce seek/read overhead when opening files (helps with CD
		///     performance).
		/// </summary>
		IgnoreTags = 0x02000000,

		/// <summary>
		///     Removes some features from samples to give a lower memory overhead, like <see cref="Sound.Name" />. See remarks.
		/// </summary>
		LowMemory = 0x08000000,

		/// <summary>
		///     Load sound into the secondary RAM of supported platform. On PS3, sounds will be loaded into RSX/VRAM.
		/// </summary>
		LoadSecondaryRam = 0x20000000,

		/// <summary>
		///     For sounds that start virtual (due to being quiet or low importance), instead of swapping back to audible, and
		///     playing at the correct offset according to time, this flag makes the sound play from the start.
		/// </summary>
		VirtualPlayFromStart = 0x80000000
	}
}