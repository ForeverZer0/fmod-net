#region License

// Tremolo.cs is distributed under the Microsoft Public License (MS-PL)
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
// Created 2:12 PM 02/13/2018

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
	///     Applies a "tremelo" effect on the sound.
	/// </summary>
	/// <remarks>
	///     <para>
	///         The tremolo effect varies the amplitude of a sound. Depending on the settings, this unit can produce a
	///         tremolo, chopper or auto-pan effect.
	///     </para>
	///     <para>
	///         The shape of the LFO (low freq. oscillator) can morphed between sine, triangle and sawtooth waves using the
	///         <see cref="Shape" /> and <see cref="Skew" /> properties.
	///     </para>
	///     <para>
	///         <see cref="Duty" /> and <see cref="Square" /> are useful
	///         for a chopper-type effect where the first controls the on-time duration and second controls the flatness of the
	///         envelope.
	///     </para>
	///     <para>
	///         <see cref="Spread" /> varies the LFO phase between channels to get an auto-pan
	///         effect. This works best with a sine shape LFO.
	///     </para>
	///     <para>
	///         The LFO can be synchronized using the <see cref="Phase" /> parameter which sets its
	///         instantaneous phase.
	///     </para>
	/// </remarks>
	/// <seealso cref="FMOD.Core.Dsp" />
	public class Tremolo : Dsp
	{
		#region Events

		/// <summary>
		///     Occurs when <see cref="Depth" /> property has changed.
		/// </summary>
		/// <seealso cref="FloatParamEventArgs" />
		public event EventHandler<FloatParamEventArgs> DepthChanged;

		/// <summary>
		///     Occurs when <see cref="Duty" /> property has changed.
		/// </summary>
		/// <seealso cref="FloatParamEventArgs" />
		public event EventHandler<FloatParamEventArgs> DutyChanged;

		/// <summary>
		///     Occurs when <see cref="Frequency" /> property has changed.
		/// </summary>
		/// <seealso cref="FloatParamEventArgs" />
		public event EventHandler<FloatParamEventArgs> FrequencyChanged;

		/// <summary>
		///     Occurs when <see cref="Phase" /> property has changed.
		/// </summary>
		/// <seealso cref="FloatParamEventArgs" />
		public event EventHandler<FloatParamEventArgs> PhaseChanged;

		/// <summary>
		///     Occurs when <see cref="Shape" /> property has changed.
		/// </summary>
		/// <seealso cref="FloatParamEventArgs" />
		public event EventHandler<FloatParamEventArgs> ShapeChanged;

		/// <summary>
		///     Occurs when <see cref="Skew" /> property has changed.
		/// </summary>
		/// <seealso cref="FloatParamEventArgs" />
		public event EventHandler<FloatParamEventArgs> SkewChanged;

		/// <summary>
		///     Occurs when <see cref="Spread" /> property has changed.
		/// </summary>
		/// <seealso cref="FloatParamEventArgs" />
		public event EventHandler<FloatParamEventArgs> SpreadChanged;

		/// <summary>
		///     Occurs when <see cref="Square" /> property has changed.
		/// </summary>
		/// <seealso cref="FloatParamEventArgs" />
		public event EventHandler<FloatParamEventArgs> SquareChanged;

		#endregion

		#region Constructors

		/// <summary>
		///     Initializes a new instance of the <see cref="Tremolo" /> class.
		/// </summary>
		/// <param name="handle">The handle.</param>
		protected Tremolo(IntPtr handle) : base(handle)
		{
		}

		#endregion

		#region Properties

		/// <summary>
		///     <para>Gets or sets the LFO frequency in Hz. </para>
		///     <para><c>0.1</c> to <c>20.0</c>. Default = <c>5.0</c>. </para>
		/// </summary>
		/// <value>
		///     The frequency.
		/// </value>
		public float Frequency
		{
			get => GetParameterFloat(0);
			set
			{
				var clamped = value.Clamp(0.01f, 20.0f);
				SetParameterFloat(0, clamped);
				FrequencyChanged?.Invoke(this, new FloatParamEventArgs(0, clamped, 0.01f, 20.0f));
			}
		}

		/// <summary>
		///     <para>Gets or sets the tremolo depth.</para>
		///     <para><c>0.0</c> to <c>1.0</c>. Default = <c>1.0</c>.</para>
		/// </summary>
		/// <value>
		///     The depth.
		/// </value>
		public float Depth
		{
			get => GetParameterFloat(1);
			set
			{
				var clamped = value.Clamp(0.0f, 1.0f);
				SetParameterFloat(1, clamped);
				DepthChanged?.Invoke(this, new FloatParamEventArgs(1, clamped, 0.0f, 1.0f));
			}
		}

		/// <summary>
		///     <para>Gets or sets the LFO shape morph between triangle and sine.</para>
		///     <para><c>0.0</c> to <c>1.0</c>. Default = <c>1.0</c>.</para>
		/// </summary>
		/// <value>
		///     The shape.
		/// </value>
		public float Shape
		{
			get => GetParameterFloat(2);
			set
			{
				var clamped = value.Clamp(0.0f, 1.0f);
				SetParameterFloat(2, clamped);
				ShapeChanged?.Invoke(this, new FloatParamEventArgs(2, clamped, 0.0f, 1.0f));
			}
		}

		/// <summary>
		///     <para>Gets or sets the time-skewing of LFO cycle.</para>
		///     <para><c>-1.0</c> to <c>1.0</c>. Default = <c>0.0</c>.</para>
		/// </summary>
		/// <value>
		///     The skew.
		/// </value>
		public float Skew
		{
			get => GetParameterFloat(3);
			set
			{
				var clamped = value.Clamp(-1.0f, 1.0f);
				SetParameterFloat(3, clamped);
				SkewChanged?.Invoke(this, new FloatParamEventArgs(3, clamped, -1.0f, 1.0f));
			}
		}

		/// <summary>
		///     <para>Gets or sets the LFO on-time.</para>
		///     <para><c>0.0</c> to <c>1.0</c>. Default = <c>0.5</c>.</para>
		/// </summary>
		/// <value>
		///     The duty.
		/// </value>
		public float Duty
		{
			get => GetParameterFloat(4);
			set
			{
				var clamped = value.Clamp(0.0f, 1.0f);
				SetParameterFloat(4, clamped);
				DutyChanged?.Invoke(this, new FloatParamEventArgs(4, clamped, 0.0f, 1.0f));
			}
		}

		/// <summary>
		///     <para>Gets or sets the flatness of the LFO shape.</para>
		///     <para><c>0.0</c> to <c>1.0</c>. Default = <c>0.0</c>.</para>
		/// </summary>
		/// <value>
		///     The square.
		/// </value>
		public float Square
		{
			get => GetParameterFloat(5);
			set
			{
				var clamped = value.Clamp(0.0f, 1.0f);
				SetParameterFloat(5, clamped);
				SquareChanged?.Invoke(this, new FloatParamEventArgs(5, clamped, 0.0f, 1.0f));
			}
		}

		/// <summary>
		///     <para>Gets or sets the instantaneous LFO phase. </para>
		///     <para><c>0.0</c> to <c>1.0</c>. Default = <c>0.0</c>.</para>
		/// </summary>
		/// <value>
		///     The phase.
		/// </value>
		public float Phase
		{
			get => GetParameterFloat(6);
			set
			{
				var clamped = value.Clamp(0.0f, 1.0f);
				SetParameterFloat(6, clamped);
				PhaseChanged?.Invoke(this, new FloatParamEventArgs(6, clamped, 0.0f, 1.0f));
			}
		}

		/// <summary>
		///     Gets or sets the rotation / auto-pan effect.
		///     <para><c>-1.0</c> to <c>1.0</c>. Default = <c>0.0</c>.</para>
		/// </summary>
		/// <value>
		///     The spread.
		/// </value>
		public float Spread
		{
			get => GetParameterFloat(7);
			set
			{
				var clamped = value.Clamp(-1.0f, 1.0f);
				SetParameterFloat(7, clamped);
				SpreadChanged?.Invoke(this, new FloatParamEventArgs(7, clamped, -1.0f, 1.0f));
			}
		}

		#endregion
	}
}