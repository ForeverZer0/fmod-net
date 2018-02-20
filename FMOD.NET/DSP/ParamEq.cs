#region License

// ParamEq.cs is distributed under the Microsoft Public License (MS-PL)
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
// Created 10:35 AM 02/14/2018

#endregion

#region Using Directives

using System;
using FMOD.Arguments;
using FMOD.Core;

#endregion

namespace FMOD.DSP
{
	/// <inheritdoc />
	/// <summary>
	///     Single band peaking EQ filter that attenuates or amplifies a selected frequency and its neighbouring frequencies.
	/// </summary>
	/// <example>
	///     <code language="CSharp" title="MultiBandEq ParamEq Effect" numberLines="true">
	/// // TODO: Make example
	/// </code>
	/// </example>
	/// <remarks>
	///     When a frequency has its gain set to <c>1.0</c>, the sound will be unaffected and represents the original
	///     signal exactly.
	/// </remarks>
	/// <seealso cref="FMOD.Core.Dsp" />
	/// <seealso cref="FMOD.DSP.MultiBandEq" />
	[Obsolete("Deprecated and will be removed in a future release, to emulate with MultiBandEq.")]
	public class ParamEq : Dsp
	{
		#region Events

		/// <summary>
		///     Occurs when <see cref="Bandwidth" /> property has changed.
		///     <seealso cref="FloatParamEventArgs" />
		/// </summary>
		public event EventHandler<FloatParamEventArgs> BandwidthChanged;

		/// <summary>
		///     Occurs when <see cref="Center" /> property has changed.
		///     <seealso cref="FloatParamEventArgs" />
		/// </summary>
		public event EventHandler<FloatParamEventArgs> CenterChanged;

		/// <summary>
		///     Occurs when <see cref="Gain" /> property has changed.
		///     <seealso cref="FloatParamEventArgs" />
		/// </summary>
		public event EventHandler<FloatParamEventArgs> GainChanged;

		#endregion

		#region Constructors

		/// <summary>
		///     Initializes a new instance of the <see cref="ParamEq" /> class.
		/// </summary>
		/// <param name="handle">The handle.</param>
		protected ParamEq(IntPtr handle) : base(handle)
		{
		}

		#endregion

		#region Properties

		/// <summary>
		///     <para>Gets or sets the frequency center.</para>
		///     <para><c>20.0</c> to <c>22000.0</c>. Default = <c>8000.0</c>.</para>
		/// </summary>
		/// <value>
		///     The center.
		/// </value>
		public float Center
		{
			get => GetParameterFloat(0);
			set
			{
				var clamped = value.Clamp(20.0f, 22000.0f);
				SetParameterFloat(0, clamped);
				CenterChanged?.Invoke(this, new FloatParamEventArgs(0, clamped, 20.0f, 22000.0f));
			}
		}

		/// <summary>
		///     <para>Gets or sets the octave range around the center frequency to filter.</para>
		///     <para><c>0.2</c> to <c>5.0</c>. Default = <c>1.0</c>.</para>
		/// </summary>
		/// <value>
		///     The bandwidth.
		/// </value>
		public float Bandwidth
		{
			get => GetParameterFloat(1);
			set
			{
				var clamped = value.Clamp(0.2f, 5.0f);
				SetParameterFloat(1, clamped);
				BandwidthChanged?.Invoke(this, new FloatParamEventArgs(1, clamped, 0.2f, 5.0f));
			}
		}

		/// <summary>
		///     Gets or sets the frequency gain in dB.
		///     <para><c>-30.0</c> to <c>30.0</c>. Default = <c>0.0</c>.</para>
		/// </summary>
		/// <value>
		///     The gain.
		/// </value>
		public float Gain
		{
			get => GetParameterFloat(2);
			set
			{
				var clamped = value.Clamp(-30.0f, 30.0f);
				SetParameterFloat(2, clamped);
				GainChanged?.Invoke(this, new FloatParamEventArgs(2, clamped, -30.0f, 30.0f));
			}
		}

		#endregion
	}
}