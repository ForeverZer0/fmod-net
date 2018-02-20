#region License

// Limiter.cs is distributed under the Microsoft Public License (MS-PL)
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
// Created 8:32 PM 02/14/2018

#endregion

#region Using Directives

using System;
using FMOD.Arguments;
using FMOD.Core;

#endregion

namespace FMOD.DSP
{
	/// <summary>
	///     This unit limits the sound to a certain level.
	/// </summary>
	/// <seealso cref="FMOD.Core.Dsp" />
	public class Limiter : Dsp
	{
		#region Events

		/// <summary>
		///     Occurs when the <see cref="Ceiling" /> property is changed.
		/// </summary>
		/// <seealso cref="Ceiling" />
		/// <seealso cref="FloatParamEventArgs" />
		public event EventHandler<FloatParamEventArgs> CeilingChanged;

		/// <summary>
		///     Occurs when the <see cref="Linked" /> property is changed.
		/// </summary>
		/// <seealso cref="Linked" />
		/// <seealso cref="BoolParamEventArgs" />
		public event EventHandler<BoolParamEventArgs> LinkedChanged;

		/// <summary>
		///     Occurs when the <see cref="MaximizerGain" /> property is changed.
		/// </summary>
		/// <seealso cref="MaximizerGain" />
		/// <seealso cref="FloatParamEventArgs" />
		public event EventHandler<FloatParamEventArgs> MaximizerGainChanged;

		/// <summary>
		///     Occurs when the <see cref="ReleaseTime" /> property is changed.
		/// </summary>
		/// <seealso cref="ReleaseTime" />
		/// <seealso cref="FloatParamEventArgs" />
		public event EventHandler<FloatParamEventArgs> ReleaseTimeChanged;

		#endregion

		#region Constructors

		/// <summary>
		///     Initializes a new instance of the <see cref="Limiter" /> class.
		/// </summary>
		/// <param name="handle">The handle.</param>
		protected Limiter(IntPtr handle) : base(handle)
		{
		}

		#endregion

		#region Properties

		/// <summary>
		///     <para>Gets or sets the time to ramp the silence to full in ms.</para>
		///     <para><c>1.0</c> to <c>1000.0</c>. Default = <c>10.0</c>.</para>
		/// </summary>
		/// <value>
		///     The release time.
		/// </value>
		/// <seealso cref="ReleaseTimeChanged" />
		public float ReleaseTime
		{
			get => GetParameterFloat(0);
			set
			{
				var clamped = value.Clamp(1.0f, 1000.0f);
				SetParameterFloat(0, clamped);
				ReleaseTimeChanged?.Invoke(this, new FloatParamEventArgs(0, clamped, 1.0f, 1000.0f));
			}
		}

		/// <summary>
		///     <para>Gets or sets the maximum level of the output signal in dB.</para>
		///     <para><c>-12.0</c> to <c>0.0</c>. Default = <c>0.0</c>.</para>
		/// </summary>
		/// <value>
		///     The ceiling.
		/// </value>
		/// <seealso cref="CeilingChanged" />
		public float Ceiling
		{
			get => GetParameterFloat(1);
			set
			{
				var clamped = value.Clamp(-12.0f, 0.0f);
				SetParameterFloat(1, clamped);
				CeilingChanged?.Invoke(this, new FloatParamEventArgs(1, clamped, -12.0f, 0.0f));
			}
		}

		/// <summary>
		///     <para>Gets or sets the maximum amplification allowed in dB.</para>
		///     <para><c>0.0</c> = no amplifaction, higher values allow more boost.</para>
		///     <para><c>0.0</c> to <c>12.0</c>. Default = <c>0.0</c>.</para>
		/// </summary>
		/// <value>
		///     The maximizer gain.
		/// </value>
		/// <seealso cref="MaximizerGainChanged" />
		public float MaximizerGain
		{
			get => GetParameterFloat(2);
			set
			{
				var clamped = value.Clamp(0.0f, 12.0f);
				SetParameterFloat(2, clamped);
				MaximizerGainChanged?.Invoke(this, new FloatParamEventArgs(2, clamped, 0.0f, 12.0f));
			}
		}

		/// <summary>
		///     <para>Gets or sets a value indicating whether channel processing mode is linked or independent. </para>
		///     <para>Default = <c>false</c>.</para>
		/// </summary>
		/// <value>
		///     <c>true</c> if linked; otherwise, <c>false</c>.
		/// </value>
		public bool Linked
		{
			get => GetParameterBool(3);
			set
			{
				SetParameterBool(3, value);
				LinkedChanged?.Invoke(this, new BoolParamEventArgs(3, value));
			}
		}

		#endregion
	}
}