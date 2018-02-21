#region License

// Lowpass.cs is distributed under the Microsoft Public License (MS-PL)
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
// Created 5:21 PM 02/13/2018

#endregion

#region Using Directives

using System;
using FMOD.NET.Arguments;
using FMOD.NET.Core;

#endregion

namespace FMOD.NET.DSP
{
	/// <inheritdoc />
	/// <summary>
	///     <para>Applies a "low-pass" filter effect on a sound.</para>
	///     <para>
	///         Deprecated and will be removed in a future release, to emulate with
	///         <see cref="MultiBandEq" />.
	///     </para>
	///     <para>
	///         See "Example" section for examples on configuring <see cref="MultiBandEq" /> for same
	///         effect.
	///     </para>
	/// </summary>
	/// <example>
	///     <code language="CSharp" title="MultiBandEq Low-Pass Effect" numberLines="true">
	/// // TODO: Make example
	/// </code>
	/// </example>
	/// <remarks>
	/// </remarks>
	/// <seealso cref="Dsp" />
	/// <seealso cref="MultiBandEq" />
	[Obsolete(
		"Deprecated and will be removed in a future release, to emulate with MultiBandEq. See documentation for example.")]
	public class Lowpass : Dsp
	{
		#region Events

		/// <summary>
		///     Occurs when the <see cref="CutoffFrequency" /> property is changed.
		/// </summary>
		/// <seealso cref="FloatParamEventArgs" />
		public event EventHandler<FloatParamEventArgs> CutoffFrequencyChanged;

		/// <summary>
		///     Occurs when the <see cref="Resonance" /> property is changed.
		/// </summary>
		/// <seealso cref="FloatParamEventArgs" />
		public event EventHandler<FloatParamEventArgs> ResonanceChanged;

		#endregion

		#region Constructors

		/// <summary>
		///     Initializes a new instance of the <see cref="Lowpass" /> class.
		/// </summary>
		/// <param name="handle">The handle.</param>
		protected Lowpass(IntPtr handle) : base(handle)
		{
		}

		#endregion

		#region Properties

		/// <summary>
		///     <para>Gets or sets the lowpass cutoff frequency in Hz.</para>
		///     <para><c>1.0</c> to output <c>22000.0</c>. Default = <c>5000.0</c>. </para>
		/// </summary>
		/// <value>
		///     The cutoff frequency.
		/// </value>
		public float CutoffFrequency
		{
			get => GetParameterFloat(0);
			set
			{
				var clamped = value.Clamp(10.0f, 22000.0f);
				SetParameterFloat(0, clamped);
				CutoffFrequencyChanged?.Invoke(this, new FloatParamEventArgs(0, clamped, 10.0f, 22000.0f));
			}
		}

		/// <summary>
		///     <para>Gets or sets the lowpass resonance Q value.</para>
		///     <para><c>1.0</c> to <c>10.0</c>. Default = <c>1.0</c>.</para>
		/// </summary>
		/// <value>
		///     The resonance.
		/// </value>
		public float Resonance
		{
			get => GetParameterFloat(1);
			set
			{
				var clamped = value.Clamp(1.0f, 10.0f);
				SetParameterFloat(1, clamped);
				ResonanceChanged?.Invoke(this, new FloatParamEventArgs(1, clamped, 1.0f, 10.0f));
			}
		}

		#endregion
	}
}