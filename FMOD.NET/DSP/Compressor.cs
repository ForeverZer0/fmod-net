#region License

// Compressor.cs is distributed under the Microsoft Public License (MS-PL)
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
// Created 12:46 PM 02/14/2018

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
	///     <para>This unit implements dynamic compression (linked/unlinked multichannel, wideband). </para>
	///     <para>This is a multichannel software limiter that is uniform across the whole spectrum.</para>
	/// </summary>
	/// <seealso cref="Dsp" />
	/// <remarks>
	///     The limiter is not guaranteed to catch every peak above the threshold level, because it cannot apply gain
	///     reduction instantaneously - the time delay is determined by the attack time. However setting the attack time too
	///     short will distort the sound, so it is a compromise. High level peaks can be avoided by using a short attack time -
	///     but not too short, and setting the threshold a few decibels below the critical level.
	/// </remarks>
	public class Compressor : Dsp
	{
		#region Events

		/// <summary>
		///     Occurs when <see cref="Attack" /> property has changed.
		/// </summary>
		/// <seealso cref="FloatParamEventArgs" />
		public event EventHandler<FloatParamEventArgs> AttackChanged;

		/// <summary>
		///     Occurs when <see cref="Linked" /> property has changed.
		/// </summary>
		/// <seealso cref="BoolParamEventArgs" />
		public event EventHandler<BoolParamEventArgs> LinkedChanged;

		/// <summary>
		///     Occurs when <see cref="MakeUpGain" /> property has changed.
		/// </summary>
		/// <seealso cref="FloatParamEventArgs" />
		public event EventHandler<FloatParamEventArgs> MakeUpGainChanged;

		/// <summary>
		///     Occurs when <see cref="Ratio" /> property has changed.
		/// </summary>
		/// <seealso cref="FloatParamEventArgs" />
		public event EventHandler<FloatParamEventArgs> RatioChanged;

		/// <summary>
		///     Occurs when <see cref="Release" /> property has changed.
		/// </summary>
		/// <seealso cref="FloatParamEventArgs" />
		public event EventHandler<FloatParamEventArgs> ReleaseChanged;

		/// <summary>
		///     Occurs when <see cref="Threshold" /> property has changed.
		/// </summary>
		/// <seealso cref="FloatParamEventArgs" />
		public event EventHandler<FloatParamEventArgs> ThresholdChanged;

		/// <summary>
		///     Occurs when <see cref="UseSideChain" /> property has changed.
		/// </summary>
		/// <seealso cref="BoolParamEventArgs" />
		public event EventHandler<BoolParamEventArgs> UseSideChainChanged;

		#endregion

		#region Constructors

		/// <summary>
		///     Initializes a new instance of the <see cref="Compressor" /> class.
		/// </summary>
		/// <param name="handle">The handle.</param>
		protected Compressor(IntPtr handle) : base(handle)
		{
		}

		#endregion

		#region Properties

		/// <summary>
		///     <para>Gets or sets the threshold level in dB.</para>
		///     <para>Range from <c>-80.0</c> to <c>0.0</c>. Default = <c>0.0</c>.</para>
		/// </summary>
		/// <value>
		///     The threshold.
		/// </value>
		public float Threshold
		{
			get => GetParameterFloat(0);
			set
			{
				var clamped = value.Clamp(-60.0f, 0.0f);
				SetParameterFloat(0, clamped);
				ThresholdChanged?.Invoke(this, new FloatParamEventArgs(0, clamped, -60.0f, 0.0f));
			}
		}

		/// <summary>
		///     <para>Gets or sets the compression ratio (dB/dB).</para>
		///     <para>Range from <c>1.0</c> to <c>50.0</c>. Default = <c>2.5</c>.</para>
		/// </summary>
		/// <value>
		///     The ratio.
		/// </value>
		public float Ratio
		{
			get => GetParameterFloat(1);
			set
			{
				var clamped = value.Clamp(1.0f, 50.0f);
				SetParameterFloat(1, clamped);
				RatioChanged?.Invoke(this, new FloatParamEventArgs(1, clamped, 1.0f, 50.0f));
			}
		}

		/// <summary>
		///     <para>Gets or sets the attack time in milliseconds.</para>
		///     <para>Range from <c>0.1</c> to <c>1000.0</c>. Default = <c>20.0</c>.</para>
		/// </summary>
		/// <value>
		///     The attack.
		/// </value>
		public float Attack
		{
			get => GetParameterFloat(2);
			set
			{
				var clamped = value.Clamp(0.1f, 500.0f);
				SetParameterFloat(2, clamped);
				AttackChanged?.Invoke(this, new FloatParamEventArgs(2, clamped, 0.1f, 500.0f));
			}
		}

		/// <summary>
		///     <para>Gets or sets the release time in milliseconds.</para>
		///     <para>Range from <c>10.0</c> through <c>5000.0</c>. Default = <c>100.0</c></para>
		/// </summary>
		/// <value>
		///     The release.
		/// </value>
		public float Release
		{
			get => GetParameterFloat(3);
			set
			{
				var clamped = value.Clamp(10.0f, 5000.0f);
				SetParameterFloat(3, clamped);
				ReleaseChanged?.Invoke(this, new FloatParamEventArgs(3, clamped, 10.0f, 5000.0f));
			}
		}

		/// <summary>
		///     <para>Gets or sets the make-up gain (dB) applied after limiting.</para>
		///     <para>Range from <c>0.0</c> through <c>30.0</c>. Default = <c>0.0</c>.</para>
		/// </summary>
		/// <value>
		///     The make-up gain.
		/// </value>
		public float MakeUpGain
		{
			get => GetParameterFloat(4);
			set
			{
				var clamped = value.Clamp(-30.0f, 30.0f);
				SetParameterFloat(4, clamped);
				MakeUpGainChanged?.Invoke(this, new FloatParamEventArgs(4, clamped, -30.0f, 30.0f));
			}
		}

		/// <summary>
		///     <para>Gets or sets a value indicating whether to analyse the sidechain signal instead of the input signal. </para>
		///     <para>Default = <c>false</c>.</para>
		/// </summary>
		/// <value>
		///     <c>true</c> if side chain is to be used; otherwise, <c>false</c>.
		/// </value>
		public bool UseSideChain
		{
			get => BitConverter.ToInt32(GetParameterData(5), 0) == 1;
			set
			{
				var bytes = BitConverter.GetBytes(value ? 1 : 0);
				SetParameterData(5, bytes);
				UseSideChainChanged?.Invoke(this, new BoolParamEventArgs(5, value));
			}
		}

		/// <summary>
		///     Gets a value indicating whether this <see cref="Compressor" /> is linked.
		///     <para><c>false</c> = Independent (compressor per channel), <c>true</c> = Linked. Default = <c>true</c>.</para>
		/// </summary>
		/// <value>
		///     <c>true</c> if linked; otherwise, <c>false</c>.
		/// </value>
		public bool Linked
		{
			get => GetParameterBool(6);
			set
			{
				SetParameterBool(6, value);
				LinkedChanged?.Invoke(this, new BoolParamEventArgs(6, value));
			}
		}

		#endregion
	}
}