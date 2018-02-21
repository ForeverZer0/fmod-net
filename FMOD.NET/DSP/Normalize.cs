#region License

// Normalize.cs is distributed under the Microsoft Public License (MS-PL)
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
// Created 1:08 PM 02/14/2018

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
	///     This unit amplifies the sound based on the maximum peaks within the signal.
	/// </summary>
	/// <remarks>
	///     <para>
	///         For example if the maximum peaks in the signal were 50% of the bandwidth, it would scale the whole sound by
	///         2.
	///     </para>
	///     <para>
	///         The lower threshold value makes the normalizer ignores peaks below a certain point, to avoid
	///         over-amplification if a loud signal suddenly came in, and also to avoid amplifying to maximum things like
	///         background hiss.
	///     </para>
	///     <para>
	///         Because FMOD is a realtime audio processor, it doesn't have the luxury of knowing the peak for the whole
	///         sound (ie it can't see into the future), so it has to process data as it comes in.
	///     </para>
	///     <para>
	///         To avoid very sudden changes in volume level based on small samples of new data, fmod fades towards the
	///         desired amplification which makes for smooth gain control. The fadetime parameter can control this.
	///     </para>
	/// </remarks>
	/// <seealso cref="Dsp" />
	public class Normalize : Dsp
	{
		#region Events

		/// <summary>
		///     Occurs when the <see cref="FadeInTime" /> property has changed.
		/// </summary>
		/// <seealso cref="FloatParamEventArgs" />
		public event EventHandler<FloatParamEventArgs> FadeInTimeChanged;

		/// <summary>
		///     Occurs when the <see cref="LowestVolume" /> property has changed.
		/// </summary>
		/// <seealso cref="FloatParamEventArgs" />
		public event EventHandler<FloatParamEventArgs> LowestVolumeChanged;

		/// <summary>
		///     Occurs when the <see cref="MaximumAmp" /> property has changed.
		/// </summary>
		/// <seealso cref="FloatParamEventArgs" />
		public event EventHandler<FloatParamEventArgs> MaximumAmpChanged;

		#endregion

		#region Constructors

		/// <summary>
		///     Initializes a new instance of the <see cref="Normalize" /> class.
		/// </summary>
		/// <param name="handle">The handle.</param>
		protected Normalize(IntPtr handle) : base(handle)
		{
		}

		#endregion

		#region Properties

		/// <summary>
		///     <para>Gets or sets the time to ramp the silence to full in ms.</para>
		///     <para><c>0.0</c> to <c>20000.0</c>. Default = <c>5000.0</c>.</para>
		/// </summary>
		/// <value>
		///     The fade-in time.
		/// </value>
		public float FadeInTime
		{
			get => GetParameterFloat(0);
			set
			{
				var clamped = value.Clamp(0.0f, 20000.0f);
				SetParameterFloat(0, clamped);
				FadeInTimeChanged?.Invoke(this, new FloatParamEventArgs(0, clamped, 0.0f, 20000.0f));
			}
		}

		/// <summary>
		///     Gets or sets the lower volume range threshold to ignore.
		///     <para>Raise higher to stop amplification of very quiet signals.</para>
		///     <para><c>0.0</c> to <c>1.0</c>. Default = <c>0.1</c>.</para>
		/// </summary>
		/// <value>
		///     The lowest volume.
		/// </value>
		public float LowestVolume
		{
			get => GetParameterFloat(1);
			set
			{
				var clamped = value.Clamp(0.0f, 1.0f);
				SetParameterFloat(1, clamped);
				LowestVolumeChanged?.Invoke(this, new FloatParamEventArgs(1, clamped, 0.0f, 1.0f));
			}
		}

		/// <summary>
		///     <para>Gets or sets the maximum amplification allowed.</para>
		///     <para><c>1.0</c> = no amplifaction, higher values allow more boost.</para>
		///     <para><c>1.0</c> to <c>100000.0</c>. Default = <c>20.0</c>.</para>
		/// </summary>
		/// <value>
		///     The maximum amplification.
		/// </value>
		public float MaximumAmp
		{
			get => GetParameterFloat(2);
			set
			{
				var clamped = value.Clamp(0.0f, 100000.0f);
				SetParameterFloat(2, clamped);
				MaximumAmpChanged?.Invoke(this, new FloatParamEventArgs(2, clamped, 0.0f, 100000.0f));
			}
		}

		#endregion
	}
}