#region License

// DspParameterDesc.cs is distributed under the Microsoft Public License (MS-PL)
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
// Created 6:24 PM 02/04/2018

#endregion

#region Using Directives

using System;
using System.Runtime.InteropServices;
using FMOD.NET.Core;
using FMOD.NET.Enumerations;

#endregion

namespace FMOD.NET.Structures
{
	/// <summary>
	///     Base structure for DSP parameter descriptions.
	/// </summary>
	/// <seealso cref="FmodSystem.CreateDsp" />
	/// <seealso cref="Dsp.GetParameterFloat" />
	/// <seealso cref="Dsp.SetParameterFloat" />
	/// <seealso cref="Dsp.GetParameterInt" />
	/// <seealso cref="Dsp.SetParameterInt" />
	/// <seealso cref="Dsp.GetParameterBool" />
	/// <seealso cref="Dsp.SetParameterBool" />
	/// <seealso cref="Dsp.GetParameterData" />
	/// <seealso cref="Dsp.SetParameterData" />
	/// <seealso cref="DspParameterDescFloat" />
	/// <seealso cref="DspParameterDescInt" />
	/// <seealso cref="DspParameterDescBool" />
	/// <seealso cref="DspParameterDescData" />
	[StructLayout(LayoutKind.Explicit)]
	public struct DspParameterDesc
	{
		/// <summary>
		///     Type of this parameter.
		/// </summary>
		[FieldOffset(0)] public DspParameterType Type;

		[FieldOffset(4)] [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)] private readonly char[] _name;

		[FieldOffset(20)] [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)] private readonly char[] _label;

		[FieldOffset(36)] private readonly string _description;

		/// <summary>
		///     <para>Struct containing information about the parameter in floating point format.</para>
		///     <para>Use when <see cref="Type" /> is <see cref="DspParameterType.Float" />.</para>
		/// </summary>
		[FieldOffset(40)] public DspParameterDescFloat FloatDescription;

		/// <summary>
		///     <para>Struct containing information about the parameter in integer format.</para>
		///     <para>Use when <see cref="Type" /> is <see cref="DspParameterType.Int" />.</para>
		/// </summary>
		[FieldOffset(40)] public DspParameterDescInt IntDescription;

		/// <summary>
		///     <para>Struct containing information about the parameter in boolean format.</para>
		///     <para>Use when <see cref="Type" /> is <see cref="DspParameterType.Bool" />.</para>
		/// </summary>
		[FieldOffset(40)] public DspParameterDescBool BoolDescription;

		/// <summary>
		///     <para>Struct containing information about the parameter in data format.</para>
		///     <para>Use when <see cref="Type" /> is <see cref="DspParameterType.Data" />.</para>
		/// </summary>
		[FieldOffset(40)] public DspParameterDescData DataDescription;

		/// <summary>
		///     Gets the name of the parameter to be displayed (ie "Cutoff frequency").
		/// </summary>
		/// <value>
		///     The name.
		/// </value>
		public string Name => new String(_name).Trim('\0');

		/// <summary>
		///     Gets a short string to be put next to value to denote the unit type (ie "hz").
		/// </summary>
		/// <value>
		///     The label.
		/// </value>
		public string Label => new String(_label).Trim('\0');

		/// <summary>
		///     Gets a description of the parameter to be displayed as a help item or tooltip for this parameter.
		/// </summary>
		/// <value>
		///     The description.
		/// </value>
		public string Description => _description;
	}
}