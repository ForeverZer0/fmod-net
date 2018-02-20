#region License

// ItEcho.cs is distributed under the Microsoft Public License (MS-PL)
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
// Created 1:51 AM 02/14/2018

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
	///     <para>This is effectively a software based echo filter that emulates the DirectX DMO echo effect.</para>
	///     <para>
	///         Impulse tracker files can support this, and <b>FMOD</b> will produce the effect on ANY platform, not just those
	///         that
	///         support DirectX effects!
	///     </para>
	/// </summary>
	/// <seealso cref="FMOD.Core.Dsp" />
	public class ItEcho : Dsp
	{
		#region Events

		/// <summary>
		///     Occurs when the <see cref="ItEcho.Feedback" /> property is changed.
		/// </summary>
		/// <seealso cref="FloatParamEventArgs" />
		public event EventHandler<FloatParamEventArgs> FeedbackChanged;

		/// <summary>
		///     Occurs when the <see cref="ItEcho.LeftDelay" /> property is changed.
		/// </summary>
		/// <seealso cref="FloatParamEventArgs" />
		public event EventHandler<FloatParamEventArgs> LeftDelayChanged;

		/// <summary>
		///     Occurs when the PanDelay property is changed.
		/// </summary>
		/// <seealso cref="FloatParamEventArgs" />
		[Obsolete("CURRENTLY NOT SUPPORTED.", true)]
		public event EventHandler<FloatParamEventArgs> PanDelayChanged;

		/// <summary>
		///     Occurs when the <see cref="ItEcho.RightDelay" /> property is changed.
		/// </summary>
		/// <seealso cref="FloatParamEventArgs" />
		public event EventHandler<FloatParamEventArgs> RightDelayChanged;

		/// <summary>
		///     Occurs when the <see cref="ItEcho.WetDryMix" /> property is changed.
		/// </summary>
		/// <seealso cref="FloatParamEventArgs" />
		public new event EventHandler<FloatParamEventArgs> WetDryMixChanged;

		#endregion

		#region Constructors

		/// <summary>
		///     Initializes a new instance of the <see cref="ItEcho" /> class.
		/// </summary>
		/// <param name="handle">The handle.</param>
		protected ItEcho(IntPtr handle) : base(handle)
		{
		}

		#endregion

		#region Properties

		/// <summary>
		///     <para>Gets or sets the ratio of wet (processed) signal to dry (unprocessed) signal.</para>
		///     <para>Range from <c>0.0</c> through <c>100.0</c> (all wet). Default = <c>50.0</c>.</para>
		/// </summary>
		/// <value>
		///     The wet dry mix.
		/// </value>
		public new float WetDryMix
		{
			get => GetParameterFloat(0);
			set
			{
				var clamped = value.Clamp(0.0f, 100.0f);
				SetParameterFloat(0, clamped);
				WetDryMixChanged?.Invoke(this, new FloatParamEventArgs(0, clamped, 0.0f, 100.0f));
			}
		}

		/// <summary>
		///     <para>Gets or sets the percentage of output fed back into input.</para>
		///     <para>Range from <c>0.0 </c> through <c>100.0</c>. Default = <c>50.0</c>.</para>
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
		///     <para>Gets or sets the delay for left channel, in milliseconds.</para>
		///     <para>Range from <c>1.0</c> through <c>2000.0</c>. Default = <c>500.0</c> ms.</para>
		/// </summary>
		/// <value>
		///     The left delay.
		/// </value>
		public float LeftDelay
		{
			get => GetParameterFloat(2);
			set
			{
				var clamped = value.Clamp(1.0f, 2000.0f);
				SetParameterFloat(2, clamped);
				LeftDelayChanged?.Invoke(this, new FloatParamEventArgs(2, clamped, 1.0f, 2000.0f));
			}
		}

		/// <summary>
		///     <para>Gets or sets the delay for right channel, in milliseconds.</para>
		///     <para>Range from <c>1.0</c> through <c>2000.0</c>. Default = <c>500.0</c> ms.</para>
		/// </summary>
		/// <value>
		///     The right delay.
		/// </value>
		public float RightDelay
		{
			get => GetParameterFloat(3);
			set
			{
				var clamped = value.Clamp(1.0f, 2000.0f);
				SetParameterFloat(3, clamped);
				RightDelayChanged?.Invoke(this, new FloatParamEventArgs(3, clamped, 1.0f, 2000.0f));
			}
		}

		/// <summary>
		///     Gets or sets the Value that specifies whether to swap left and right delays with each successive echo.
		///     <para>
		///         Ranges from <c>0.0</c> (equivalent to <c>false</c>) to <c>1.0</c> (equivalent to <c>true</c>), meaning no
		///         swap. Default = <c>0.0</c>.
		///     </para>
		/// </summary>
		/// <value>
		///     The pan delay.
		/// </value>
		[Obsolete("CURRENTLY NOT SUPPORTED.", true)]
		public float PanDelay
		{
			get => GetParameterFloat(4);
			set
			{
				var clamped = value.Clamp(0.0f, 1.0f);
				SetParameterFloat(4, clamped);
				PanDelayChanged?.Invoke(this, new FloatParamEventArgs(4, clamped, 0.0f, 1.0f));
			}
		}

		#endregion
	}
}