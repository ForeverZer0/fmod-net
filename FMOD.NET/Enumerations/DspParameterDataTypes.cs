#region License

// DspParameterDataTypes.cs is distributed under the Microsoft Public License (MS-PL)
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
// Created 11:15 AM 02/07/2018

#endregion

#region Using Directives

using FMOD.Core;
using FMOD.Structures;

#endregion

namespace FMOD.Enumerations
{
	/// <summary>
	///     <para>Built-in types for the "datatype" member of <see cref="DspParameterDescData" />. </para>
	///     <para>Data parameters of type other than <see cref="User" /> will be treated specially by the system.</para>
	/// </summary>
	/// <seealso cref="DspParameterDescData" />
	/// <seealso cref="DspParameterOverallGain" />
	/// <seealso cref="DspParameterAttributes3D" />
	/// <seealso cref="DspParameterSideChain" />
	/// <seealso cref="DspParameterFft" />
	/// <seealso cref="DspParameterAttributes3DMulti" />
	/// <seealso cref="Dsp.GetParameterData" />
	/// <seealso cref="Dsp.SetParameterData" />
	public enum DspParameterDataTypes
	{
		/// <summary>
		///     The default data type. All user data types should be 0 or above.
		/// </summary>
		User = 0,

		/// <summary>
		///     <para>The data type for <see cref="DspParameterOverallGain" /> parameters.</para>
		///     <para>There should a maximum of one per DSP.</para>
		/// </summary>
		OverallGain = -1,

		/// <summary>
		///     <para>The data type for <see cref="DspParameterAttributes3D" /> parameters.</para>
		///     <para>There should a maximum of one per DSP.</para>
		/// </summary>
		Attributes3D = -2,

		/// <summary>
		///     <para>The data type for <see cref="DspParameterSideChain" /> parameters</para>
		///     .
		///     <para>There should a maximum of one per DSP.</para>
		/// </summary>
		SideChain = -3,

		/// <summary>
		///     <para>The data type for <see cref="DspParameterFft" /> parameters.</para>
		///     <para>There should a maximum of one per DSP.</para>
		/// </summary>
		Fft = -4,

		/// <summary>
		///     <para>The data type for <see cref="DspParameterAttributes3DMulti" /> parameters.</para>
		///     <para>There should a maximum of one per DSP.</para>
		/// </summary>
		Attributes3DMulti = -5
	}
}