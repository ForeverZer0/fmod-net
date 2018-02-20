#region License

// AdvancedSettings.cs is distributed under the Microsoft Public License (MS-PL)
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
// Created 7:23 PM 02/11/2018

#endregion

#region Using Directives

using System;
using System.Runtime.InteropServices;
using FMOD.Core;
using FMOD.Enumerations;

#endregion

namespace FMOD.Structures
{
	/// <summary>
	///     Settings for advanced features like configuring memory and CPU usage for the
	///     <see cref="Mode.CreateCompressedSample" /> feature.
	/// </summary>
	/// <remarks>
	///     <para>
	///         <see cref="MaxMPEGCodecs" /> / <see cref="MaxADPCMCodecs" /> / <see cref="MaxXMACodecs" /> will determine the
	///         maximum CPU usage of playing realtime samples. Use this to lower potential excess CPU usage and also control
	///         memory usage.
	///     </para>
	///     <para>
	///         <see cref="MaxPCMCodecs" /> is for use with PS3 only. It will determine the maximum number of PCM voices that
	///         can be played at once. This includes streams of any format and all sounds created without the
	///         <see cref="Mode.CreateCompressedSample" /> flag.<lineBreak />
	///         Memory will be allocated for codecs "up front" (during <see cref="O:FMOD.Core.FmodSystem.Initialize" />) if
	///         these values are specified as non zero. If any are zero, it allocates memory for the codec whenever a file of
	///         the type in question is loaded. So if <see cref="MaxMPEGCodecs" /> is <c>0</c> for example, it will allocate
	///         memory for the mpeg codecs the first time an MP3 is loaded or an mp3 based .FSB file is loaded.
	///     </para>
	///     <para>
	///         Due to inefficient encoding techniques on certain .wav based ADPCM files, <b>FMOD</b> can can need an extra
	///         29720 bytes per codec. This means for lowest memory consumption. Use FSB as it uses an optimal/small ADPCM
	///         block size.
	///     </para>
	/// </remarks>
	/// <seealso cref="O:FMOD.Core.FmodSystem.Initialize" />
	/// <seealso cref="FmodSystem.AdvancedSettings" />
	/// <seealso cref="AdvancedSettings" />
	/// <seealso cref="Mode" />
	[StructLayout(LayoutKind.Sequential)]
	public struct AdvancedSettings
	{
		/// <summary>
		///     <para>
		///         Size of this structure. Use <c>sizeof</c> or <see cref="O:System.Runtime.InteropServices.Marshal.SizeOf" />
		///         to set this value.
		///     </para>
		///     <para><b>NOTE:</b> This must be set before calling <see cref="FmodSystem.AdvancedSettings" />! </para>
		/// </summary>
		public int CbSize;

		/// <summary>
		///     Optional. Specify <c>0</c> to ignore.
		///     <para>
		///         For use with <see cref="Mode.CreateCompressedSample" /> only. MPEG codecs consume 22,216 bytes per instance
		///         and this number will determine how many MPEG channels can be played simultaneously.
		///     </para>
		///     <para>Default = <c>32</c>.</para>
		/// </summary>
		public int MaxMPEGCodecs;

		/// <summary>
		///     <para>Optional. Specify <c>0</c> to ignore.</para>
		///     <para>
		///         For use with <see cref="Mode.CreateCompressedSample" /> only. ADPCM codecs consume 2,480 bytes per instance
		///         and this number will determine how many ADPCM channels can be played simultaneously.
		///     </para>
		///     <para>Default = <c>32</c>.</para>
		/// </summary>
		public int MaxADPCMCodecs;

		/// <summary>
		///     <para>Optional. Specify <c>0</c> to ignore.</para>
		///     <para>
		///         For use with <see cref="Mode.CreateCompressedSample" /> only. XMA codecs consume 6,263 bytes per instance and
		///         this number will determine how many XMA channels can be played simultaneously.
		///     </para>
		///     <para>Default = <c>32</c>.</para>
		/// </summary>
		public int MaxXMACodecs;

		/// <summary>
		///     <para>Optional. Specify <c>0</c> to ignore.</para>
		///     <para>
		///         For use with <see cref="Mode.CreateCompressedSample" /> only. Vorbis codecs consume 16,512 bytes per instance
		///         and this number will determine how many Vorbis channels can be played simultaneously.
		///     </para>
		///     <para>Default = <c>32</c>.</para>
		/// </summary>
		public int mMxVorbisCodecs;

		/// <summary>
		///     <para>Optional. Specify <c>0</c> to ignore.</para>
		///     <para>
		///         For use with <see cref="Mode.CreateCompressedSample" /> only. AT9 codecs consume 20,664 bytes per instance
		///         and this number will determine how many AT9 channels can be played simultaneously.
		///     </para>
		///     <para>Default = <c>32</c>.</para>
		/// </summary>
		public int MaxAT9Codecs;

		/// <summary>
		///     <para>Optional. Specify <c>0</c> to ignore.</para>
		///     <para>
		///         For use with <see cref="Mode.CreateCompressedSample" /> only. FADPCM codecs consume 2,232 bytes per instance
		///         and this number will determine how many FADPCM channels can be played simultaneously.
		///     </para>
		///     <para>Default = <c>32</c>.</para>
		/// </summary>
		public int MaxFADPCMCodecs;

		/// <summary>
		///     <para>Optional. Specify <c>0</c> to ignore.</para>
		///     <para>
		///         For use with PS3 only. PCM codecs consume 2,536 bytes per instance and this number will determine how many
		///         streams and PCM voices can be played simultaneously.
		///     </para>
		///     <para>Default = <c>32</c>.</para>
		/// </summary>
		public int MaxPCMCodecs;

		/// <summary>
		///     <para>Optional. Specify 0 to ignore.  </para>
		///     <para>Number of channels available on the ASIO device.</para>
		/// </summary>
		public int ASIONumChannels;

		/// <summary>
		///     <para>Optional. Specify 0 to ignore.</para>
		///     <para>
		///         Pointer to an array of strings (number of entries defined by <see cref="ASIONumChannels" />) with ASIO
		///         channel names.
		///     </para>
		/// </summary>
		public IntPtr ASIOChannelList;

		/// <summary>
		///     <para>Optional. Specify 0 to ignore.</para>
		///     <para>Pointer to a list of speakers that the ASIO channels map to.</para>
		///     <para>This can be called after <see cref="O:FMOD.Core.FmodSystem.Initialize" /> to remap ASIO output.</para>
		/// </summary>
		public IntPtr ASIOSpeakerList;

		/// <summary>
		///     <para>Optional. For use with <see cref="InitFlags.ChannelLowpass" />.</para>
		///     <para>
		///         The angle range (<c>0</c> to <c>360</c>) of a 3D sound in relation to the listener, at which the HRTF
		///         function begins to have an effect. <c>0</c> = in front of the listener. <c>180</c> = from <c>90</c> degrees to
		///         the left of the listener to <c>90</c> degrees to the right. <c>360</c> = behind the listener.
		///     </para>
		///     <para>Default = <c>180.0</c>.</para>
		/// </summary>
		public float HRTFMinAngle;

		/// <summary>
		///     <para>Optional. For use with <see cref="InitFlags.ChannelLowpass" />. </para>
		///     <para>
		///         The angle range (<c>0</c> to <c>360</c>) of a 3D sound in relation to the listener, at which the HRTF
		///         function has maximum effect. <c>0</c> = front of the listener. <c>180</c> = from <c>90</c> degrees to the left
		///         of the listener to <c>90</c> degrees to the right. <c>360</c> = behind the listener.
		///     </para>
		///     <para>Default = <c>360.0</c>. </para>
		/// </summary>
		public float HRTFMaxAngle;

		/// <summary>
		///     <para>Optional. Specify <c>0</c> to ignore.</para>
		///     <para>
		///         For use with <see cref="InitFlags.ChannelLowpass" />. The cutoff frequency of the HRTF's lowpass filter
		///         function when at maximum effect. (i.e. at <see cref="HRTFMaxAngle" />).
		///     </para>
		///     <para>Default = <c>4000.0</c>. </para>
		/// </summary>
		public float HRTFFreq;

		/// <summary>
		///     <para>Optional. Specify <c>0</c> to ignore. </para>
		///     <para>
		///         For use with <see cref="InitFlags.Vol0BecomesVirtual" />. If this flag is used, and the volume is below this,
		///         then the sound will become virtual.
		///     </para>
		///     <para>Use this value to raise the threshold to a different point where a sound goes virtual.</para>
		/// </summary>
		public float Vol0VirtualVol;

		/// <summary>
		///     <para>Optional. Specify <c>0</c> to ignore.</para>
		///     <para>For streams. This determines the default size of the double buffer (in milliseconds) that a stream uses.</para>
		///     <para>Default = 400ms </para>
		/// </summary>
		public uint DefaultDecodeBufferSize;

		/// <summary>
		///     <para>Optional. Specify <c>0</c> to ignore. </para>
		///     <para>
		///         For use with <see cref="InitFlags.ProfileEnable" />. Specify the port to listen on for connections by the
		///         profiler application.
		///     </para>
		/// </summary>
		public ushort ProfilePort;

		/// <summary>
		///     <para>Optional. Specify <c>0</c> to ignore. </para>
		///     <para>The maximum time in miliseconds it takes for a channel to fade to the new level when its occlusion changes. </para>
		/// </summary>
		public uint GeometryMaxFadeTime;

		/// <summary>
		///     <para>Optional. Specify <c>0.0</c> to ignore. </para>
		///     <para>
		///         For use with <see cref="InitFlags.ChannelDistanceFilter" />. The default center frequency in Hz for the
		///         distance filtering effect.
		///     </para>
		///     <para>Default = <c>1500.0</c>.</para>
		/// </summary>
		public float DistanceFilterCenterFreq;

		/// <summary>
		///     <para>Optional. Specify <c>0</c> to ignore.  </para>
		///     <para>
		///         Out of 0 to 3, 3D reverb spheres will create a phyical reverb unit on this instance slot. See
		///         <see cref="ReverbProperties" />.
		///     </para>
		/// </summary>
		public int ReverbInstance;

		/// <summary>
		///     <para>Optional. Specify <c>0</c> to ignore. </para>
		///     <para>
		///         Number of buffers in DSP buffer pool. Each buffer will be DSPBlockSize * sizeof(float) *
		///         SpeakerModeChannelCount. ie 7.1 @ 1024 DSP block size = 8 * 1024 * 4 = 32kb.
		///     </para>
		///     <para>Default = <c>8</c></para>
		/// </summary>
		public int DSPBufferPoolSize;

		/// <summary>
		///     <para>Optional. Specify <c>0</c> to ignore. </para>
		///     <para>
		///         Specify the stack size for the <b>FMOD</b> Stream thread in bytes. Useful for custom codecs that use excess
		///         stack.
		///     </para>
		///     <para>Default 49,152 (48kb) </para>
		/// </summary>
		public uint StackSizeStream;

		/// <summary>
		///     <para>Optional. Specify <c>0</c> to ignore. </para>
		///     <para>
		///         Specify the stack size for the <see cref="Mode.NonBlocking" /> loading thread. Useful for custom codecs that
		///         use excess stack.
		///     </para>
		///     <para>Default 65,536 (64kb)</para>
		/// </summary>
		public uint StackSizeNonBlocking;

		/// <summary>
		///     <para>Optional. Specify <c>0</c> to ignore. </para>
		///     <para>Specify the stack size for the <b>FMOD</b> mixer thread. Useful for custom DSPs that use excess stack. </para>
		///     <para>Default 49,152 (48kb)</para>
		/// </summary>
		public uint StackSizeMixer;

		/// <summary>
		///     <para>Optional. Specify <c>0</c> to ignore.</para>
		///     <para>
		///         Resampling method used with <b>FMOD</b>'s software mixer. See <see cref="DspResampler" /> for details on
		///         methods.
		///     </para>
		/// </summary>
		public DspResampler ResamplerMethod;

		/// <summary>
		///     <para>Optional. Specify <c>0</c> to ignore. </para>
		///     <para>Specify the command queue size for thread safe processing. </para>
		///     <para>Default 2048 (2kb) </para>
		/// </summary>
		public uint CommandQueueSize;

		/// <summary>
		///     <para>Optional. Specify <c>0</c> to ignore.  </para>
		///     <para>Seed value that <b>FMOD</b> will use to initialize its internal random number generators.</para>
		/// </summary>
		public uint RandomSeed;
	}
}