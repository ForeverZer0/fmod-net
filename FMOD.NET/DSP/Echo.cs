#region License

// Echo.cs is distributed under the Microsoft Public License (MS-PL)
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
// Created 1:51 PM 02/13/2018

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
	///     Applies a basic "echo" effect to a sound.
	/// </summary>
	/// <seealso cref="Dsp" />
	public class Echo : Dsp
	{
		#region Events

		/// <summary>
		///     Occurs when <see cref="Delay" /> property is changed.
		/// </summary>
		/// <seealso cref="FloatParamEventArgs" />
		public event EventHandler<FloatParamEventArgs> DelayChanged;

		/// <summary>
		///     Occurs when <see cref="DryLevel" /> property is changed.
		/// </summary>
		/// <seealso cref="FloatParamEventArgs" />
		public event EventHandler<FloatParamEventArgs> DryLevelChanged;

		/// <summary>
		///     Occurs when <see cref="Feedback" /> property is changed.
		/// </summary>
		/// <seealso cref="FloatParamEventArgs" />
		public event EventHandler<FloatParamEventArgs> FeedbackChanged;

		/// <summary>
		///     Occurs when <see cref="WetLevel" /> property is changed.
		/// </summary>
		/// <seealso cref="FloatParamEventArgs" />
		public event EventHandler<FloatParamEventArgs> WetLevelChanged;

		#endregion

		#region Constructors

		/// <summary>
		///     Initializes a new instance of the <see cref="Echo" /> class.
		/// </summary>
		/// <param name="handle">The handle.</param>
		protected Echo(IntPtr handle) : base(handle)
		{
		}

		#endregion

		#region Properties

		/// <summary>
		///     <para>Gets or sets the echo delay in ms.</para>
		///     <para><c>10.0</c> to <c>5000.0</c>. Default = <c>500.0</c>.</para>
		/// </summary>
		/// <value>
		///     The delay.
		/// </value>
		/// <remarks>
		///     <alert class="note">
		///         <para>
		///             Every time the delay is changed, the plugin re-allocates the echo buffer. This means the echo will
		///             dissapear at that time while it refills its new buffer.
		///         </para>
		///     </alert>
		///     <para>Larger echo delays result in larger amounts of memory allocated.</para>
		/// </remarks>
		public float Delay
		{
			get => GetParameterFloat(0);
			set
			{
				var clamped = value.Clamp(5.0f, 5000.0f);
				SetParameterFloat(0, clamped);
				DelayChanged?.Invoke(this, new FloatParamEventArgs(0, clamped, 5.0f, 5000.0f));
			}
		}

		/// <summary>
		///     <para>Gets or sets the echo decay per delay.</para>
		///     <para><c>0</c> to <c>100</c>. </para>
		///     <para><c>100.0</c> = No decay, <c>0.0</c> = total decay (ie simple 1 line delay). Default = <c>50.0</c>.</para>
		/// </summary>
		/// <value>
		///     The feedback.
		/// </value>
		public float Feedback
		{
			get => GetParameterFloat(1);
			set
			{
				var clamped = value.Clamp(0.0f, 100.0f);
				SetParameterFloat(1, clamped);
				FeedbackChanged?.Invoke(this, new FloatParamEventArgs(1, clamped, 0.0f, 100.0f));
			}
		}

		/// <summary>
		///     <para>Gets or sets the original sound volume in dB.</para>
		///     <para><c>-80.0</c> to <c>10.0</c>. Default = <c>0</c>.</para>
		/// </summary>
		/// <value>
		///     The dry level.
		/// </value>
		public float DryLevel
		{
			get => GetParameterFloat(2);
			set
			{
				var clamped = value.Clamp(-80.0f, 10.0f);
				SetParameterFloat(2, clamped);
				DryLevelChanged?.Invoke(this, new FloatParamEventArgs(2, clamped, -80.0f, 10.0f));
			}
		}

		/// <summary>
		///     <para>Gets or sets the volume of echo signal to pass to output in dB. </para>
		///     <para><c>-80.0</c> to <c>10.0</c>. Default = <c>0</c>.</para>
		/// </summary>
		/// <value>
		///     The wet level.
		/// </value>
		public float WetLevel
		{
			get => GetParameterFloat(3);
			set
			{
				var clamped = value.Clamp(-80.0f, 10.0f);
				SetParameterFloat(3, clamped);
				WetLevelChanged?.Invoke(this, new FloatParamEventArgs(3, clamped, -80.0f, 10.0f));
			}
		}

		#endregion
	}
}