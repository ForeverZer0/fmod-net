#region License

// Chorus.cs is distributed under the Microsoft Public License (MS-PL)
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
// Created 12:49 PM 02/13/2018

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
	///     This unit produces a chorus effect on the sound.
	/// </summary>
	/// <remarks>
	///     Chorus is an effect where the sound is more "spacious" due to one to three versions of the sound being played along
	///     side the original signal but with the pitch of each copy modulating on a sine wave.
	/// </remarks>
	/// <seealso cref="FMOD.Core.Dsp" />
	public class Chorus : Dsp
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
		///     Initializes a new instance of the <see cref="Chorus" /> class.
		/// </summary>
		/// <param name="handle">The handle.</param>
		protected Chorus(IntPtr handle) : base(handle)
		{
		}

		#endregion

		#region Properties

		/// <summary>
		///     <para>Gets or sets the volume of original signal to pass to output.</para>
		///     <para><c>0.0</c> to <c>100.0</c>. Default = <c>50.0</c>.</para>
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
		///     <para>Gets or sets the chorus modulation rate in Hz.</para>
		///     <para><c>0.0</c> to <c>20.0</c>. Default = <c>0.8</c> Hz. </para>
		/// </summary>
		/// <value>
		///     The rate.
		/// </value>
		public float Rate
		{
			get => GetParameterFloat(1);
			set
			{
				var clamped = value.Clamp(0.0f, 20.0f);
				SetParameterFloat(1, clamped);
				RateChanged?.Invoke(this, new FloatParamEventArgs(1, clamped, 0.0f, 20.0f));
			}
		}

		/// <summary>
		///     <c>Gets or sets the chorus modulation depth.</c>
		///     <para><c>0.0</c> to <c>100.0</c>. Default = <c>3.0</c>.</para>
		/// </summary>
		/// <value>
		///     The depth.
		/// </value>
		public float Depth
		{
			get => GetParameterFloat(2);
			set
			{
				var clamped = value.Clamp(0.0f, 100.0f);
				SetParameterFloat(2, clamped);
				DepthChanged?.Invoke(this, new FloatParamEventArgs(2, clamped, 0.0f, 100.0f));
			}
		}

		#endregion
	}
}