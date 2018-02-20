#region License

// EnvelopeFollower.cs is distributed under the Microsoft Public License (MS-PL)
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
// Created 10:29 PM 02/14/2018

#endregion

#region Using Directives

using System;
using FMOD.Arguments;
using FMOD.Core;

#endregion

namespace FMOD.DSP
{
	/// <summary>
	///     This unit tracks the envelope of the input/sidechain signal.
	/// </summary>
	/// <remarks>
	///     <para>Deprecated and will be removed in a future release.</para>
	///     <para>This unit does not affect the incoming signal.</para>
	/// </remarks>
	/// <seealso cref="FMOD.Core.Dsp" />
	[Obsolete("Deprecated and will be removed in a future release.")]
	public class EnvelopeFollower : Dsp
	{
		#region Events

		/// <summary>
		///     Occurs when the <see cref="Attack" /> property is changed.
		/// </summary>
		/// <seealso cref="Attack" />
		/// <seealso cref="FloatParamEventArgs" />
		public event EventHandler<FloatParamEventArgs> AttackChanged;

		/// <summary>
		///     Occurs when the <see cref="Release" /> property is changed.
		/// </summary>
		/// <seealso cref="Release" />
		/// <seealso cref="FloatParamEventArgs" />
		public event EventHandler<FloatParamEventArgs> ReleaseChanged;

		/// <summary>
		///     Occurs when the <see cref="UseSideChain" /> property is changed.
		/// </summary>
		/// <seealso cref="UseSideChain" />
		/// <seealso cref="BoolParamEventArgs" />
		public event EventHandler<BoolParamEventArgs> UseSideChainChanged;

		#endregion

		#region Constructors

		/// <summary>
		///     Initializes a new instance of the <see cref="EnvelopeFollower" /> class.
		/// </summary>
		/// <param name="handle">The handle.</param>
		protected EnvelopeFollower(IntPtr handle) : base(handle)
		{
		}

		#endregion

		#region Properties

		/// <summary>
		///     <para>Gets or sets the attack time in milliseconds.</para>
		///     <para>Range from <c>0.1</c> through <c>1000.0</c>. Default = <c>20.0</c>.</para>
		/// </summary>
		/// <value>
		///     The attack time.
		/// </value>
		public float Attack
		{
			get => GetParameterFloat(0);
			set
			{
				var clamped = value.Clamp(0.1f, 500.0f);
				SetParameterFloat(0, clamped);
				AttackChanged?.Invoke(this, new FloatParamEventArgs(0, clamped, 0.1f, 500.0f));
			}
		}

		/// <summary>
		///     <para>Gets or sets the release time in milliseconds.</para>
		///     <para>Range from <c>10.0</c> through <c>5000.0</c>. Default = <c>10.0</c></para>
		///     0
		/// </summary>
		/// <value>
		///     The release time.
		/// </value>
		public float Release
		{
			get => GetParameterFloat(1);
			set
			{
				var clamped = value.Clamp(10.0f, 5000.0f);
				SetParameterFloat(1, clamped);
				ReleaseChanged?.Invoke(this, new FloatParamEventArgs(1, clamped, 10.0f, 5000.0f));
			}
		}

		/// <summary>
		///     <para>Gets the current value of the envelope.</para>
		///     <para>Range from <c>0.0</c> to <c>1.0</c>.</para>
		/// </summary>
		/// <value>
		///     The current envelope.
		/// </value>
		public float Envelope => GetParameterFloat(2);

		/// <summary>
		///     <para>Gets or sets a value indicating whether to analyse the sidechain signal instead of the input signal.</para>
		///     <para>Default = <c>false</c>.</para>
		/// </summary>
		/// <value>
		///     <c>true</c> if to use sidechain; otherwise, <c>false</c>.
		/// </value>
		public bool UseSideChain
		{
			get => BitConverter.ToInt32(GetParameterData(3), 0) == 1;
			set
			{
				SetParameterData(3, BitConverter.GetBytes(value ? 1 : 0));
				UseSideChainChanged?.Invoke(this, new BoolParamEventArgs(3, value));
			}
		}

		#endregion
	}
}