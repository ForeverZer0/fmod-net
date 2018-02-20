#region License

// TimeUnit.cs is distributed under the Microsoft Public License (MS-PL)
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
// Created 11:22 PM 02/03/2018

#endregion

#region Using Directives

using System;

#endregion

namespace FMOD.Enumerations
{
	/// <summary>
	///     List of time types that can be returned by <see cref="M:FMOD.Core.Sound.GetLength" /> and used with
	///     <see cref="M:FMOD.Core.Channel.SetPosition" /> or <see cref="M:FMOD.Core.Channel.GetPosition" />.
	/// </summary>
	/// <seealso cref="M:FMOD.Core.Sound.GetLength" />
	/// <seealso cref="M:FMOD.Core.Channel.GetPosition" />
	/// <seealso cref="M:FMOD.Core.Channel.SetPosition" />
	[Flags]
	public enum TimeUnit : uint
	{
		/// <summary>
		///     Milliseconds.
		/// </summary>
		Ms = 0x00000001,

		/// <summary>
		///     PCM samples, related to milliseconds * samplerate / 1000.
		/// </summary>
		Pcm = 0x00000002,

		/// <summary>
		///     Bytes, related to PCM samples * channels * datawidth (ie 16bit = 2 bytes).
		/// </summary>
		PcmBytes = 0x00000004,

		/// <summary>
		///     <para>Raw file bytes of (compressed) sound data (does not include headers). </para>
		///     <para>Only used by <see cref="M:FMOD.Core.Sound.GetLength" /> and <see cref="M:FMOD.Core.Channel.GetPosition" />. </para>
		/// </summary>
		RawBytes = 0x00000008,

		/// <summary>
		///     <para>Fractions of 1 PCM sample. <seealso cref="System.UInt32" /> range <c>0</c> to <c>0xFFFFFFFF</c>. </para>
		///     <para>Used for sub-sample granularity for DSP purposes.</para>
		/// </summary>
		PcmFraction = 0x00000010,

		/// <summary>
		///     MOD/S3M/XM/IT. Order in a sequenced module format. Use <see cref="FMOD.Core.Sound.Format" /> to determine the PCM
		///     format being decoded to.
		/// </summary>
		ModOrder = 0x00000100,

		/// <summary>
		///     <para>MOD/S3M/XM/IT. Current row in a sequenced module format. </para>
		///     <para>Cannot use with <see cref="M:FMOD.Core.Channel.SetPosition" />.</para>
		///     <para>
		///         <see cref="O:FMOD.Core.Sound.GetLength" /> will return the number of rows in the currently playing or seeked
		///         to pattern.
		///     </para>
		/// </summary>
		ModRow = 0x00000200,

		/// <summary>
		///     MOD/S3M/XM/IT. Current pattern in a sequenced module format.
		///     <para>Cannot use with <see cref="M:FMOD.Core.Channel.SetPosition" />.</para>
		///     <para>
		///         <see cref="M:FMOD.Core.Sound.GetLength" /> will return the number of patterns in the song and
		///         <see cref="M:FMOD.Core.Channel.GetPosition" /> will return the currently playing pattern.
		///     </para>
		/// </summary>
		ModPattern = 0x00000400
	}
}