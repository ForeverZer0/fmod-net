#region License

// Constants.cs is distributed under the Microsoft Public License (MS-PL)
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
// Created 9:49 PM 02/15/2018

#endregion

namespace FMOD.NET.Core
{
	/// <summary>
	///     Static class containing constant values for reference.
	/// </summary>
	public static class Constants
	{
		/// <summary>
		///     The platform specific name of the native library to link to.
		/// </summary>
#if X64 
		public const string LIBRARY = "fmod64";
#elif X86
		public const string LIBRARY = "fmod";
#endif

		/// <summary>
		///     Array containing file extensions, with leading "." character, supported by <b>FMOD</b>.
		/// </summary>
		public static readonly string[] SUPPORTED_EXTENSIONS =
		{
			".aiff", ".asf", ".asx", ".dls", ".flac", ".fsb", ".it", ".m3u", ".midi", ".mod", ".mp2", ".mp3", ".ogg", ".pls",
			".s3m", ".vag", ".wav", ".wax", ".wma", ".xm", ".xma"
		};

		/// <summary>
		///     <para>Filename filtering string for opening audio files supported by <b>FMOD</b>.</para>
		///     <para>This could be used for a <see cref="System.Windows.Forms.FileDialog.Filter" /> property.</para>
		/// </summary>
		public const string FILE_FILTER =
			"All Supported Formats|*.aiff;*.asf;*.asx;*.dls;*.flac;*.fsb;*.it;*.m3u;*.mid;*.midi;*.mod;*.mp2;*.mp3;*.ogg;*.pls;*.s3m;*.vag;*.wav;*.wax;*.wma;*.xm;*.xma|" +
			"Audio Interchange File Format (*.aiff)|*.aiff|" +
			"Advanced Systems Format (*.asf)|*.asf|" +
			"Advanced Stream Redirector (*.asx)|*.asx|" +
			"Downloadable Sounds (*.dls)|*.dls|" +
			"Free Lossless Audio Codec (*.flac)|*.flac|" +
			"FMOD Sound Bank (*.fsb)|*.fsb|" +
			"Impulse Tracker (*.it)|*.it|" +
			"MPEG Audio Layer 3 URL (*.m3u)|*.m3u|" +
			"Musical Instrument Digital Interface (*.mid, *.midi)|*.mid;*.midi|" +
			"Module Format (*.mod)|*.mod|" +
			"MPEG Audio Layer 2 (*.mp2)|*.mp2|" +
			"MPEG Audio Layer 3 (*.mp3)|*.mp3|" +
			"OGG Vorbis (*.ogg)|*.ogg|" +
			"Playlist (*.pls)|*.pls|" +
			"ScreamTracker 3 Module (*.s3m)|*.s3m|" +
			"PS2/PSP Format (*.vag)|*.vag|" +
			"Waveform Audio File Format (*.wav)|*.wav|" +
			"Windows Media Audio Redirector (*.wax)|*.wax|" +
			"Windows Media Audio (*.wma)|*.wma|" +
			"Extended Module (*.xm)|*.xm|" +
			"Windows Media Audio (Xbox 360) (*.xma)|*.xma|" +
			"All Documents (*.*)|*.*";

		/// <summary>
		///     The version is a 32-bit hexadecimal value formated as 16:8:8, with the upper 16-bits being the major version, the
		///     middle 8-bits being the minor version and the bottom 8-bits being the development version.
		/// </summary>
		public const int VERSION = 0x00011002;

		/// <summary>
		///     The maximum number of "real" <see cref="Channel" /> objects that can play simultaneously.
		/// </summary>
		public const int MAX_CHANNELS = 32;

		/// <summary>
		///     The maximum number of listeners for 3D sound.
		/// </summary>
		public const int MAX_LISTENERS = 8;

		/// <summary>
		///     The maximum number of <see cref="Reverb" /> objects.
		/// </summary>
		public const int MAX_REVERBS = 4;

		/// <summary>
		///     The maximum number of <seealso cref="FmodSystem" /> objects.
		/// </summary>
		public const int MAX_SYSTEMS = 8;
	}
}