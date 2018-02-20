#region License

// Flange.cs is distributed under the Microsoft Public License (MS-PL)
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
// Created 1:42 PM 02/13/2018

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
	///     Applies a "flange" effect on the sound.
	/// </summary>
	/// <remarks>
	///     <para>
	///         Flange is an effect where the signal is played twice at the same time, and one copy slides back and forth
	///         creating a whooshing or flanging effect.
	///     </para>
	///     <para>
	///         As there are 2 copies of the same signal, by default each signal is given 50% mix, so that the total is not
	///         louder than the original unaffected signal.
	///     </para>
	///     <para>
	///         Flange depth is a percentage of a 10ms shift from the original signal. Anything above 10ms is not considered
	///         flange because to the ear it begins to 'echo' so 10ms is the highest value possible.
	///     </para>
	/// </remarks>
	/// <seealso cref="FMOD.Core.Dsp" />
	public class Flange : Dsp
	{
		#region Events

		/// <summary>
		///     Occurs when <see cref="Depth" /> property is changed.
		/// </summary>
		/// <seealso cref="FloatParamEventArgs" />
		public event EventHandler<FloatParamEventArgs> DepthChanged;

		/// <summary>
		///     Occurs when <see cref="Mix" /> property is changed.
		/// </summary>
		/// <seealso cref="FloatParamEventArgs" />
		public event EventHandler<FloatParamEventArgs> MixChanged;

		/// <summary>
		///     Occurs when <see cref="Rate" /> property is changed.
		/// </summary>
		/// <seealso cref="FloatParamEventArgs" />
		public event EventHandler<FloatParamEventArgs> RateChanged;

		#endregion

		#region Constructors

		/// <summary>
		///     Initializes a new instance of the <see cref="Flange" /> class.
		/// </summary>
		/// <param name="handle">The handle.</param>
		protected Flange(IntPtr handle) : base(handle)
		{
		}

		#endregion

		#region Properties

		/// <summary>
		///     <para>Gets or sets the percentage of wet signal in mix.</para>
		///     <para><c>0</c> to <c>100</c>. Default = <c>50</c>.</para>
		/// </summary>
		/// <value>
		///     The mix.
		/// </value>
		public float Mix
		{
			get => GetParameterFloat(0);
			set
			{
				var clamped = value.Clamp(0.0f, 100.0f);
				SetParameterFloat(0, clamped);
				MixChanged?.Invoke(this, new FloatParamEventArgs(0, clamped, 0.0f, 100.0f));
			}
		}

		/// <summary>
		///     Gets or sets the flange depth (percentage of 40ms delay).
		///     <para><c>0.01</c> to <c>1.0</c>. Default = <c>1.0</c>. </para>
		/// </summary>
		/// <value>
		///     The depth.
		/// </value>
		public float Depth
		{
			get => GetParameterFloat(1);
			set
			{
				var clamped = value.Clamp(0.01f, 1.0f);
				SetParameterFloat(1, clamped);
				DepthChanged?.Invoke(this, new FloatParamEventArgs(1, clamped, 0.01f, 1.0f));
			}
		}

		/// <summary>
		///     Gets or sets the flange speed in Hz.
		///     <para><c>0.0</c> to <c>20.0</c>. Default = <c>0.1</c>.</para>
		/// </summary>
		/// <value>
		///     The rate.
		/// </value>
		public float Rate
		{
			get => GetParameterFloat(2);
			set
			{
				var clamped = value.Clamp(0.0f, 20.0f);
				SetParameterFloat(2, clamped);
				RateChanged?.Invoke(this, new FloatParamEventArgs(2, clamped, 0.0f, 20.0f));
			}
		}

		#endregion
	}
}