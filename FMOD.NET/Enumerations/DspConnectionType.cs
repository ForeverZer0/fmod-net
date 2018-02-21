#region License

// DspConnectionType.cs is distributed under the Microsoft Public License (MS-PL)
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
// Created 12:49 AM 02/04/2018

#endregion

#region Using Directives

using FMOD.NET.Core;
using FMOD.NET.Structures;

#endregion

namespace FMOD.NET.Enumerations
{
	/// <summary>
	///     List of connection types between two <see cref="Dsp" /> nodes.
	/// </summary>
	/// <seealso cref="Dsp.AddInput" />
	/// <seealso cref="DspConnection.Type" />
	public enum DspConnectionType
	{
		/// <summary>
		///     <para>Specifies the <b>Default</b> connection type.</para>
		///     <para>Audio is mixed from the input to the output <see cref="Dsp" />'s audible buffer.</para>
		/// </summary>
		/// <remarks>
		///     <para>
		///         Default <see cref="DspConnection" /> type. Audio is mixed from the input to the output <see cref="Dsp" />'s
		///         audible buffer, meaning it will be part of the audible signal.
		///     </para>
		///     <para>A standard connection will execute its input DSP if it has not been executed before.</para>
		/// </remarks>
		Standard,

		/// <summary>
		///     <para>Specifies the <b>Sidechain</b> connection type.</para>
		///     <para>Audio is mixed from the input to the output <see cref="Dsp" />'s sidechain buffer.</para>
		/// </summary>
		/// <remarks>
		///     <para>
		///         Sidechain <see cref="DspConnection" /> type. Audio is mixed from the input to the output <see cref="Dsp" />'s
		///         sidechain buffer, meaning it will NOT be part of the audible signal. A sidechain connection will execute its
		///         input DSP if it has not been executed before.
		///     </para>
		///     <para>
		///         The purpose of the seperate sidechain buffer in a DSP, is so that the DSP effect can privately access for
		///         analysis purposes. An example of use in this case, could be a compressor which analyzes the signal, to control
		///         its own effect parameters (ie a compression level or gain).
		///     </para>
		///     <para>
		///         For the effect developer, to accept sidechain data, the sidechain data will appear in the
		///         <see cref="DspState" /> struct which is passed into the read callback of a DSP unit.
		///     </para>
		///     <para>
		///         <see cref="DspState.SideChainData" /> and <see cref="DspState.SideChainChannels" /> will hold the mixed
		///         result of any sidechain data flowing into it.
		///     </para>
		/// </remarks>
		SideChain,

		/// <summary>
		///     <para>Specifies the <b>Send</b> connection type.</para>
		///     <para>
		///         Audio is mixed from the input to the output <see cref="Dsp" />'s audible buffer, but the input is <b>NOT</b>
		///         executed, only copied from. A standard connection or sidechain needs to make an input execute to generate data.
		///     </para>
		/// </summary>
		/// <remarks>
		///     <para>
		///         Send <see cref="DspConnection" /> type. Audio is mixed from the input to the output <see cref="Dsp" />'s
		///         audible buffer, meaning it will be part of the audible signal. A send connection will <b>NOT</b> execute its
		///         input DSP if it has not been executed before
		///     </para>
		///     <para>
		///         A send connection will only read what exists at the input's buffer at the time of executing the output DSP
		///         unit (which can be considered the "return")
		///     </para>
		/// </remarks>
		Send,

		/// <summary>
		///     <para>Specifies the <b>Send Sidechain</b> connection type. </para>
		///     <para>
		///         Audio is mixed from the input to the output <see cref="Dsp" />'s sidechain buffer, but the input is
		///         <b>NOT</b> executed, only copied from. A standard connection or sidechain needs to make an input execute to
		///         generate data.
		///     </para>
		/// </summary>
		/// <remarks>
		///     <para>
		///         Send sidechain <see cref="DspConnection" />  type. Audio is mixed from the input to the output
		///         <see cref="Dsp" />'s sidechain buffer, meaning it will <b>NOT</b> be part of the audible signal. A send
		///         sidechain connection will <b>NOT</b> execute its input DSP if it has not been executed before.
		///     </para>
		///     <para>
		///         A send sidechain connection will only read what exists at the input's buffer at the time of executing the
		///         output DSP unit (which can be considered the "sidechain return").
		///     </para>
		///     <para>
		///         For the effect developer, to accept sidechain data, the sidechain data will appear in the
		///         <see cref="DspState" /> struct which is passed into the read callback of a DSP unit.
		///     </para>
		///     <para>
		///         <see cref="DspState.SideChainData" /> and <see cref="DspState.SideChainChannels" /> will hold the mixed
		///         result of any sidechain data flowing into it.
		///     </para>
		/// </remarks>
		SendSideChain,

		/// <summary>
		///     Maximum number of DSP connection types supported.
		/// </summary>
		Max
	}
}