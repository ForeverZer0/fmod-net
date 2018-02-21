#region License

// Send.cs is distributed under the Microsoft Public License (MS-PL)
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
// Created 8:22 PM 02/14/2018

#endregion

#region Using Directives

using System;
using FMOD.NET.Arguments;
using FMOD.NET.Core;

#endregion

namespace FMOD.NET.DSP
{
	/// <summary>
	///     This unit sends a copy of the signal to a return DSP anywhere in the DSP tree.
	/// </summary>
	/// <seealso cref="Dsp" />
	/// <seealso cref="Return" />
	public class Send : Dsp
	{
		#region Events

		/// <summary>
		///     Occurs when <see cref="SendLevel" /> property is changed.
		/// </summary>
		/// <seealso cref="SendLevel" />
		/// <seealso cref="FloatParamEventArgs" />
		public event EventHandler<FloatParamEventArgs> SendLevelChanged;

		#endregion

		#region Constructors

		/// <summary>
		///     Initializes a new instance of the <see cref="Send" /> class.
		/// </summary>
		/// <param name="handle">The handle.</param>
		protected Send(IntPtr handle) : base(handle)
		{
		}

		#endregion

		#region Properties

		/// <summary>
		///     <para>Gets the ID of the <see cref="Return" /> DSP this send is connected to (integer values only). </para>
		///     <para><c>-1</c> indicates no connected <see cref="Return" /> DSP. Default = <c>-1</c>.</para>
		/// </summary>
		/// <value>
		///     The return identifier.
		/// </value>
		public int ReturnId => GetParameterInt(0);

		/// <summary>
		///     <para>Gets or sets the send level.</para>
		///     <para><c>0.0</c> to <c>1.0</c>. Default = <c>1.0</c></para>
		/// </summary>
		/// <value>
		///     The send level.
		/// </value>
		public float SendLevel
		{
			get => GetParameterFloat(1);
			set
			{
				var clamped = value.Clamp(0.0f, 10.0f);
				SetParameterFloat(1, clamped);
				SendLevelChanged?.Invoke(this, new FloatParamEventArgs(1, clamped, 0.0f, 10.0f));
			}
		}

		[Obsolete("This property is not used.", true)]
		public byte[] OverallGain
		{
			get => GetParameterData(2);
			set => SetParameterData(2, value);
		}

		#endregion
	}
}