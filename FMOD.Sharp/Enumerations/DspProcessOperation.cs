#region License

// DspProcessOperation.cs is distributed under the Microsoft Public License (MS-PL)
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
// Created 7:11 PM 02/11/2018

#endregion

#region Using Directives

using FMOD.Core;
using FMOD.Structures;

#endregion

namespace FMOD.Enumerations
{
	/// <summary>
	///     Operation type for <see cref="DspProcessCallback" />.
	/// </summary>
	/// <remarks>
	///     <para>
	///         A process callback will be called twice per mix for a DSP unit. Once with the <see cref="ProcessQuery" />
	///         command, then conditionally, <see cref="ProcessPerform" />.<lineBreak />
	///         <see cref="ProcessQuery" /> is to be handled only by filling out the outputarray information, and returning a
	///         relevant return code.
	///         It should not really do any logic besides checking and returning one of the following codes:
	///         <list type="bullet">
	///             <item>
	///                 <para>
	///                     <see cref="Result.OK" /> - Meaning yes, it should execute the DSP process function with
	///                     <see cref="ProcessPerform" />.
	///                 </para>
	///             </item>
	///             <item>
	///                 <para>
	///                     <see cref="Result.DspDontProcess" /> - Meaning no, it should skip the process function and not call
	///                     it with <see cref="ProcessPerform" />.
	///                 </para>
	///             </item>
	///             <item>
	///                 <para>
	///                     <see cref="Result.DspSilence" /> - Meaning no, it should skip the process function and not call it
	///                     with <see cref="ProcessPerform" />, AND, tell the signal chain to follow that it is now idle, so
	///                     that no more processing happens down the chain.
	///                 </para>
	///             </item>
	///         </list>
	///     </para>
	///     <para>
	///         If audio is to be processed, <i>"outbufferarray"</i> must be filled with the expected output format, channel
	///         count and mask. Mask can be <c>0</c>.
	///     </para>
	///     <para>
	///         <see cref="ProcessPerform" /> is to be handled by reading the data from the input, processing it, and writing
	///         it to the output. Always write to the output buffer and fill it fully to avoid unpredictable audio output.
	///     </para>
	///     <para>Always return <see cref="Result.OK" />, the return value is ignored from the process stage.</para>
	/// </remarks>
	/// <seealso cref="DspDescription" />
	public enum DspProcessOperation
	{
		/// <summary>
		///     Process the incoming audio in <i>"inbufferarray"</i> and output to <i>"outbufferarray"</i>.
		/// </summary>
		ProcessPerform,

		/// <summary>
		///     <para>
		///         The DSP is being queried for the expected output format and whether it needs to process audio or should be
		///         bypassed.
		///     </para>
		///     <para>
		///         The function should return <see cref="Result.OK" />, or <see cref="Result.DspDontProcess" /> or
		///         <see cref="Result.DspSilence" /> if audio can pass through unprocessed.
		///     </para>
		///     <para>
		///         See remarks for more. If audio is to be processed, <i>"outbufferarray"</i> must be filled with the expected
		///         output format, channel count and mask.
		///     </para>
		/// </summary>
		ProcessQuery
	}
}