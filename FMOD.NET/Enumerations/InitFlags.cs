#region License

// InitFlags.cs is distributed under the Microsoft Public License (MS-PL)
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
using FMOD.NET.Core;

#endregion

namespace FMOD.NET.Enumerations
{
	/// <summary>
	///     <para>
	///         Initialization flags. Use them with <see cref="O:FMOD.Core.FmodSystem.Initialize" /> in the flags parameter
	///         to change various behavior.
	///     </para>
	///     <para>
	///         Use <see cref="FmodSystem.AdvancedSettings" /> to adjust settings for some of the features that are enabled
	///         by these flags.
	///     </para>
	/// </summary>
	/// <seealso cref="O:FMOD.Core.FmodSystem.Initialize" />
	/// <seealso cref="FmodSystem.Update" />
	/// <seealso cref="FmodSystem.AdvancedSettings" />
	/// <seealso cref="AdvancedSettings" />
	[Flags]
	public enum InitFlags : uint
	{
		/// <summary>
		///     Initialize normally.
		/// </summary>
		Normal = 0x00000000,

		/// <summary>
		///     <para>No stream thread is created internally. Streams are driven from <see cref="FmodSystem.Update" />.</para>
		///     <para>Mainly used with non-realtime outputs.</para>
		/// </summary>
		StreamFromUpdate = 0x00000001,

		/// <summary>
		///     <para>No mixer thread is created internally. </para>
		///     <para>Mixing is driven from <see cref="FmodSystem.Update" />.</para>
		///     <para>
		///         Only applies to polling based output modes such as <see cref="OutputType.NoSound" />,
		///         <see cref="OutputType.WavWriter" />, <see cref="OutputType.DSound" />, <see cref="OutputType.Winmm" />,
		///         <see cref="OutputType.Xaudio" />.
		///     </para>
		/// </summary>
		MixFromUpdate = 0x00000002,

		/// <summary>
		///     3D calculations will be performed in right-handed coordinates.
		/// </summary>
		RightHanded3D = 0x00000004,

		/// <summary>
		///     All <see cref="Mode.ThreeD" /> based voices will add a software lowpass filter effect into the DSP chain which is
		///     automatically used when <see cref="ChannelControl.ReverbOcclusion3D" /> is used or the <see cref="Geometry" /> API.
		///     <para>This also causes sounds to sound duller when the sound goes behind the listener, as a fake HRTF style effect.</para>
		///     <para>Use <see cref="FmodSystem.AdvancedSettings" /> to disable or adjust cutoff frequency for this feature.</para>
		/// </summary>
		ChannelLowpass = 0x00000100,

		/// <summary>
		///     <para>
		///         All <see cref="Mode.ThreeD" /> based voices will add a software lowpass and highpass filter effect into the
		///         DSP chain which will act as a distance-automated bandpass filter.
		///     </para>
		///     <para>Use <see cref="FmodSystem.AdvancedSettings" /> to adjust the center frequency. </para>
		/// </summary>
		ChannelDistanceFilter = 0x00000200,

		/// <summary>
		///     Enable TCP/IP based host which allows FMOD Designer or FMOD Profiler to connect to it, and view memory, CPU and the
		///     DSP network graph in real-time.
		/// </summary>
		ProfileEnable = 0x00010000,

		/// <summary>
		///     <para>
		///         Any sounds that are 0 volume will go virtual and not be processed except for having their positions updated
		///         virtually.
		///     </para>
		///     <para>Use <see cref="FmodSystem.AdvancedSettings" /> to adjust what volume besides zero to switch to virtual at. </para>
		/// </summary>
		Vol0BecomesVirtual = 0x00020000,

		/// <summary>
		///     With the geometry engine, only process the closest polygon rather than accumulating all polygons the sound to
		///     listener line intersects.
		/// </summary>
		GeometryUseClosest = 0x00040000,

		/// <summary>
		///     When using <see cref="SpeakerMode.FivePointOne" /> with a stereo output device, use the Dolby Pro Logic II downmix
		///     algorithm instead of the SRS Circle Surround algorithm.
		/// </summary>
		PreferDolbyDownMix = 0x00080000,

		/// <summary>
		///     Disables thread safety for API calls. Only use this if <b>FMOD</b> low level is being called from a single thread,
		///     and if Studio API is not being used!
		/// </summary>
		ThreadUnsafe = 0x00100000,

		/// <summary>
		///     <para>Slower, but adds level metering for every single DSP unit in the graph. </para>
		///     <para>Use <see cref="Dsp.EnableMetering" /> to turn meters off individually.</para>
		/// </summary>
		ProfileMeterAll = 0x00200000,

		/// <summary>
		///     Using <see cref="SpeakerMode.FivePointOne" /> with a stereo output device will enable the SRS Circle Surround
		///     downmixer.
		///     <para>By default the SRS downmixer applies a high pass filter with a cutoff frequency of 80Hz. </para>
		///     <para>
		///         Use this flag to diable the high pass fitler, or use <see cref="PreferDolbyDownMix" /> to use the Dolby Pro
		///         Logic II downmix algorithm instead.
		///     </para>
		/// </summary>
		DisableSrsHighpassFilter = 0x00400000
	}
}