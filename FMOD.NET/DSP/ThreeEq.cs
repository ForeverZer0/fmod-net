#region License

// ThreeEq.cs is distributed under the Microsoft Public License (MS-PL)
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
// Created 2:08 AM 02/14/2018

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
	///     Basic three-band equalizer.
	/// </summary>
	/// <seealso cref="Dsp" />
	public class ThreeEq : Dsp
	{
		#region Events

		/// <summary>
		///     Occurs when the <see cref="HighCrossover" /> property has changed.
		/// </summary>
		/// <see cref="FloatParamEventArgs" />
		public event EventHandler<FloatParamEventArgs> HighCrossoverChanged;

		/// <summary>
		///     Occurs when the <see cref="HighGain" /> property has changed.
		/// </summary>
		/// <see cref="FloatParamEventArgs" />
		public event EventHandler<FloatParamEventArgs> HighGainChanged;

		/// <summary>
		///     Occurs when the <see cref="LowCrossover" /> property has changed.
		/// </summary>
		/// <see cref="FloatParamEventArgs" />
		public event EventHandler<FloatParamEventArgs> LowCrossoverChanged;

		/// <summary>
		///     Occurs when the <see cref="LowGain" /> property has changed.
		/// </summary>
		/// <see cref="FloatParamEventArgs" />
		public event EventHandler<FloatParamEventArgs> LowGainChanged;

		/// <summary>
		///     Occurs when the <see cref="MidGain" /> property has changed.
		/// </summary>
		/// <see cref="FloatParamEventArgs" />
		public event EventHandler<FloatParamEventArgs> MidGainChanged;

		#endregion

		#region Constructors

		/// <summary>
		///     Initializes a new instance of the <see cref="ThreeEq" /> class.
		/// </summary>
		/// <param name="handle">The handle.</param>
		protected ThreeEq(IntPtr handle) : base(handle)
		{
		}

		#endregion

		#region Properties

		/// <summary>
		///     <para>Gets or sets the low-frequency gain in dB. </para>
		///     <para><c>-80.0</c> to <c>10.0</c>. Default = <c>0.0</c>.</para>
		/// </summary>
		/// <value>
		///     The low-frequency gain.
		/// </value>
		public float LowGain
		{
			get => GetParameterFloat(0);
			set
			{
				var clamped = value.Clamp(-80.0f, 10.0f);
				SetParameterFloat(0, clamped);
				LowGainChanged?.Invoke(this, new FloatParamEventArgs(0, clamped, -80.0f, 10.0f));
			}
		}

		/// <summary>
		///     <para>Gets or sets the mid-frequency gain in dB. </para>
		///     <para><c>-80.0</c> to <c>10.0</c>. Default = <c>0.0</c>.</para>
		/// </summary>
		/// <value>
		///     The mid-frequency gain.
		/// </value>
		public float MidGain
		{
			get => GetParameterFloat(1);
			set
			{
				var clamped = value.Clamp(-80.0f, 10.0f);
				SetParameterFloat(1, clamped);
				MidGainChanged?.Invoke(this, new FloatParamEventArgs(1, clamped, -80.0f, 10.0f));
			}
		}

		/// <summary>
		///     <para>Gets or sets the high-frequency gain in dB. </para>
		///     <para><c>-80.0</c> to <c>10.0</c>. Default = <c>0.0</c>.</para>
		/// </summary>
		/// <value>
		///     The high-frequency gain.
		/// </value>
		public float HighGain
		{
			get => GetParameterFloat(2);
			set
			{
				var clamped = value.Clamp(-80.0f, 10.0f);
				SetParameterFloat(2, clamped);
				HighGainChanged?.Invoke(this, new FloatParamEventArgs(2, clamped, -80.0f, 10.0f));
			}
		}

		/// <summary>
		///     <para>Gets or sets the low-to-mid crossover frequency in Hz. </para>
		///     <para><c>10.0</c> to <c>22000.0</c>. Default = <c>400.0</c>.</para>
		/// </summary>
		/// <value>
		///     The low crossover.
		/// </value>
		public float LowCrossover
		{
			get => GetParameterFloat(3);
			set
			{
				var clamped = value.Clamp(10.0f, 22000.0f);
				SetParameterFloat(3, clamped);
				LowCrossoverChanged?.Invoke(this, new FloatParamEventArgs(3, clamped, 10.0f, 22000.0f));
			}
		}

		/// <summary>
		///     <para>Gets or sets the mid-to-high crossover frequency in Hz.</para>
		///     <para><c>10.0</c> to <c>22000.0</c>. Default = <c>4000.0</c>.</para>
		/// </summary>
		/// <value>
		///     The high crossover.
		/// </value>
		public float HighCrossover
		{
			get => GetParameterFloat(4);
			set
			{
				var clamped = value.Clamp(10.0f, 22000.0f);
				SetParameterFloat(4, clamped);
				HighCrossoverChanged?.Invoke(this, new FloatParamEventArgs(4, clamped, 10.0f, 22000.0f));
			}
		}

		/// <summary>
		///     <para>Gets or sets the slope for crossovers.</para>
		///     <para>Default = <see cref="ThreeEq.CrossoverSlope.TwentyFour" />.</para>
		/// </summary>
		/// <value>
		///     The slope.
		/// </value>
		public CrossoverSlope Slope
		{
			get => (CrossoverSlope) GetParameterInt(5);
			set => SetParameterInt(5, (int) value);
		}

		#endregion

		/// <summary>
		///     Describes a slope used for crossovers in the <see cref="ThreeEq" /> unit.
		/// </summary>
		/// <seealso cref="ThreeEq" />
		/// <seealso cref="ThreeEq.Slope" />
		public enum CrossoverSlope
		{
			/// <summary>
			///     12dB/Octave
			/// </summary>
			Twelve,

			/// <summary>
			///     24dB/Octave
			/// </summary>
			TwentyFour,

			/// <summary>
			///     48dB/Octave
			/// </summary>
			FortyEight
		}
	}
}