#region License

// Delay.cs is distributed under the Microsoft Public License (MS-PL)
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
// Created 11:07 AM 02/14/2018

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
	///     This unit produces different delays on individual channels of the sound.
	/// </summary>
	/// <remarks>
	///     <alert class="note">
	///         <para>
	///             Every time <see cref="MaximumDelay" /> is changed, the plugin re-allocates the delay buffer. This means the
	///             delay will
	///             dissapear at that time while it refills its new buffer.
	///         </para>
	///         <para>A larger <see cref="MaximumDelay" /> results in larger amounts of memory allocated.</para>
	///         <para>
	///             Channel delays above MaxDelay will be clipped to <see cref="MaximumDelay" /> and the delay buffer will
	///             not be resized.
	///         </para>
	///     </alert>
	/// </remarks>
	/// <seealso cref="FMOD.Core.Dsp" />
	public class Delay : Dsp
	{
		#region Events

		/// <summary>
		///     Occurs when the delay is changed on a channel.
		/// </summary>
		/// <seealso cref="DspDelayChangedEventArgs" />
		/// <seealso cref="SetDelay" />
		public event EventHandler<DspDelayChangedEventArgs> DelayChanged;

		/// <summary>
		///     Occurs when the <see cref="MaximumDelay" /> proeprty is changed.
		/// </summary>
		/// <seealso cref="FloatParamEventArgs" />
		public event EventHandler<FloatParamEventArgs> MaximumDelayChanged;

		#endregion

		#region Constructors

		/// <summary>
		///     Initializes a new instance of the <see cref="Delay" /> class.
		/// </summary>
		/// <param name="handle">The handle.</param>
		protected Delay(IntPtr handle) : base(handle)
		{
		}

		#endregion

		#region Properties

		/// <summary>
		///     Gets or sets the maximum delay, in milliseconds.
		///     <para>Range from <c>0.0</c> to <c>10000.0</c>.</para>
		/// </summary>
		/// <value>
		///     The maximum delay.
		/// </value>
		public float MaximumDelay
		{
			get => GetParameterFloat(16);
			set
			{
				var clamped = value.Clamp(0.0f, 10000.0f);
				SetParameterFloat(16, clamped);
				MaximumDelayChanged?.Invoke(this, new FloatParamEventArgs(16, clamped, 0.0f, 10000.0f));
			}
		}

		#endregion

		#region Methods

		/// <summary>
		///     Gets the delay valeu for the specified channel.
		/// </summary>
		/// <param name="channel">The channel. <c>0</c> to <c>15</c>.</param>
		/// <returns>The delay value.</returns>
		public float GetDelay(int channel)
		{
			return GetParameterFloat(channel.Clamp(0, 15));
		}

		/// <summary>
		///     Sets the delay on the specified channel, in milliseconds.
		///     <para>Range from <c>0.0</c> to <c>10000.0</c>.</para>
		/// </summary>
		/// <param name="channel">The channel to set delay of.</param>
		/// <param name="delay">The delay.</param>
		public void SetDelay(int channel, float delay)
		{
			var clamped = delay.Clamp(0.0f, 10000.0f);
			SetParameterFloat(channel, clamped);
			DelayChanged?.Invoke(this, new DspDelayChangedEventArgs(channel, clamped));
		}

		#endregion
	}
}