#region License

// DspInfo.cs is distributed under the Microsoft Public License (MS-PL)
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
// Created 3:21 PM 02/05/2018

#endregion

#region Using Directives

using System;
using System.Drawing;

#endregion

namespace FMOD.Data
{
	public class DspInfo
	{
		/// <summary>
		///     Gets the name of the unit.
		/// </summary>
		/// <value>
		///     The name.
		/// </value>
		public string Name { get; internal set; }

		/// <summary>
		///     Gets the version number of the DSP unit.
		/// </summary>
		/// <value>
		///     The version.
		/// </value>
		public Version Version { get; internal set; }

		/// <summary>
		///     <para>Gets the number of channels the unit was initialized with.</para>
		///     <para><c>0</c> means the plugin will process whatever number of channels is currently in the network.</para>
		///     <para>
		///         Greater than <c>0</c> would be mostly used if the unit is a unit that only generates sound, or is not
		///         flexible enough to take any number of input channels.
		///     </para>
		/// </summary>
		/// <value>
		///     The channel count.
		/// </value>
		public int ChannelCount { get; internal set; }

		/// <summary>
		///     Gets the size of the optional configuration window.
		/// </summary>
		/// <value>
		///     The size of the configuration window.
		/// </value>
		public Size ConfigWindowSize { get; internal set; }

		/// <summary>
		///     Returns a <see cref="System.String" /> that represents this instance.
		/// </summary>
		/// <returns>
		///     A <see cref="System.String" /> that represents this instance.
		/// </returns>
		public override string ToString()
		{
			return $"{Name} v.{Version}";
		}
	}
}