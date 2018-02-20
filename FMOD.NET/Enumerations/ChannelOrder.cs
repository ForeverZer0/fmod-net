#region License

// ChannelOrder.cs is distributed under the Microsoft Public License (MS-PL)
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
// Created 12:51 AM 02/04/2018

#endregion

#region Using Directives

using FMOD.Core;
using FMOD.Structures;

#endregion

namespace FMOD.Enumerations
{
	/// <summary>
	///     <para>
	///         When creating a multichannel sound, <b>FMOD</b> will pan them to their default speaker locations, for example a
	///         6 channel sound will default to one channel per 5.1 output speaker.<lineBreak />
	///         Another example is a stereo sound. It will default to left = front left, right = front right.
	///     </para>
	///     <para>
	///         This is for sounds that are not "default". For example you might have a sound that is 6 channels but actually
	///         made up of 3 stereo pairs, that should all be located in front left, front right only.
	///     </para>
	/// </summary>
	/// <seealso cref="CreateSoundExInfo" />
	/// <seealso cref="Speaker" />
	/// <seealso cref="Constants.MAX_CHANNELS" />
	public enum ChannelOrder
	{
		/// <summary>
		///     Left, Right, Center, LFE, Surround Left, Surround Right, Back Left, Back Right (see <see cref="Speaker" />
		///     enumeration)
		/// </summary>
		Default,

		/// <summary>
		///     Left, Right, Center, LFE, Back Left, Back Right, Surround Left, Surround Right (as per Microsoft .wav WAVEFORMAT
		///     structure master order)
		/// </summary>
		Waveformat,

		/// <summary>
		///     Left, Center, Right, Surround Left, Surround Right, LFE
		/// </summary>
		Protools,

		/// <summary>
		///     <i>Mono, Mono, Mono, Mono, Mono, Mono, ..</i>. (each channel all the way up to
		///     <see cref="Constants.MAX_CHANNELS" /> channels are treated as if they were mono)
		/// </summary>
		AllMono,

		/// <summary>
		///     <i>Left, Right, Left, Right, Left, Right, ...</i> (each pair of channels is treated as stereo all the way up to
		///     <see cref="Constants.MAX_CHANNELS" /> channels)
		/// </summary>
		AllStereo,

		/// <summary>
		///     Left, Right, Surround Left, Surround Right, Center, LFE (as per Linux ALSA channel order)
		/// </summary>
		Alsa,

		/// <summary>
		///     Maximum number of channel orderings supported.
		/// </summary>
		Max
	}
}