#region License

// DspConnection.cs is distributed under the Microsoft Public License (MS-PL)
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
// Created 9:57 PM 02/15/2018

#endregion

#region Using Directives

using System;
using FMOD.Enumerations;

#endregion

namespace FMOD.Core
{
	/// <summary>
	///     <para>Represents a connection between two <see cref="HandleBase" /> units.</para>
	///     <para>Think of it as the line between two circles.</para>
	/// </summary>
	/// <seealso cref="FMOD.Core.HandleBase" />
	public partial class DspConnection : HandleBase
	{
		#region Constructors & Destructor

		/// <summary>
		///     Initializes a new instance of the <see cref="DspConnection" /> class.
		/// </summary>
		/// <param name="handle">The handle to the object.</param>
		internal DspConnection(IntPtr handle) : base(handle)
		{
		}

		#endregion

		#region Delegates & Events

		/// <summary>
		///     Occurs when <seealso cref="UserData" /> property has been changed.
		/// </summary>
		/// <seealso cref="UserData" />
		public event EventHandler UserDataChanged;

		/// <summary>
		///     Occurs when <see cref="Mix" /> level has changed.
		/// </summary>
		/// <seealso cref="Mix" />
		public event EventHandler MixChanged;

		/// <summary>
		///     Occurs when mix matrix has changed.
		/// </summary>
		/// <seealso cref="SetMixMatrix" />
		public event EventHandler MixMatrixChanged;

		#endregion

		#region Properties & Indexers

		/// <summary>
		///     Gets the <see cref="DspConnection">DSP</see> unit that is the input of this connection.
		/// </summary>
		/// <value>
		///     The input DSP.
		/// </value>
		/// <remarks>
		///     <para>
		///         A <see cref="DspConnection" /> joins two <see cref="Dsp.AddInput" /> units together (think of it as the line
		///         between two circles).
		///     </para>
		///     <para>Each <see cref="Dsp" /> has one input and one output.</para>
		///     <alert class="note">
		///         <para>
		///             If a <see cref="Result.NotReady" /> just occurred, the connection might not be ready because the
		///             <see cref="Dsp.AddInput" /> system is still queued to connect in the background. If so the function will
		///             return <see cref="Dsp" /> and the input will be <c>null</c>. Poll until it is ready.
		///         </para>
		///     </alert>
		/// </remarks>
		/// <seealso cref="DspConnection" />
		/// <seealso cref="Dsp" />
		/// <seealso cref="Output" />
		/// <seealso cref="Dsp.AddInput" />
		public Dsp Input
		{
			get
			{
				NativeInvoke(FMOD_DSPConnection_GetInput(this, out var dsp));
				return CoreHelper.Create<Dsp>(dsp);
			}
		}

		/// <summary>
		///     <para>
		///         Gets or sets the volume of the connection. The input is scaled by this value before being passed to the
		///         output.
		///     </para>
		///     <para><c>0.0</c> = Silent, <c>1.0</c> = Full Volume.</para>
		/// </summary>
		/// <value>
		///     The mix.
		/// </value>
		/// <seealso cref="Dsp.GetInput" />
		/// <seealso cref="Dsp.GetOutput" />
		/// <seealso cref="MixChanged" />
		public float Mix
		{
			get
			{
				NativeInvoke(FMOD_DSPConnection_GetMix(this, out var volume));
				return volume;
			}
			set
			{
				NativeInvoke(FMOD_DSPConnection_SetMix(this, value.Clamp(0.0f, 1.0f)));
				MixChanged?.Invoke(this, EventArgs.Empty);
			}
		}

		/// <summary>
		///     Gets the <see cref="DspConnection" /> unit that is the output of this connection.
		/// </summary>
		/// <value>
		///     The output.
		/// </value>
		/// <remarks>
		///     <para>
		///         A <see cref="DspConnection" /> joins two <see cref="Dsp.AddInput" /> units together (think of it as the line
		///         between two circles).
		///     </para>
		///     <para>Each <see cref="Dsp" /> has one input and one output.</para>
		///     <alert class="note">
		///         <para>
		///             If a <see cref="Result.NotReady" /> just occurred, the connection might not be ready because the
		///             <see cref="Dsp.AddInput">DSP</see> system is still queued to connect in the background.
		///         </para>
		///         <para>
		///             If so the function will return <see cref="Dsp" /> and the input will be <c>null</c>. Poll until it is
		///             ready.
		///         </para>
		///     </alert>
		/// </remarks>
		/// <seealso cref="DspConnection" />
		/// <seealso cref="Dsp" />
		/// <seealso cref="Input" />
		/// <seealso cref="Dsp.AddInput" />
		public Dsp Output
		{
			get
			{
				NativeInvoke(FMOD_DSPConnection_GetOutput(this, out var dsp));
				return CoreHelper.Create<Dsp>(dsp);
			}
		}

		/// <summary>
		///     <para>Gets the type of the connection between two <see cref="DspConnectionType.Standard" /> units.</para>
		///     <para>
		///         This can be <see cref="DspConnectionType" />, <see cref="DspConnectionType" />,
		///         <see cref="DspConnectionType" />, or <see cref="DspConnectionType" />.
		///     </para>
		/// </summary>
		/// <value>
		///     The type.
		/// </value>
		/// <seealso cref="Dsp" />
		/// <seealso cref="DspConnectionType" />
		public DspConnectionType Type
		{
			get
			{
				NativeInvoke(FMOD_DSPConnection_GetType(this, out var type));
				return type;
			}
		}

		/// <summary>
		///     Gets or sets a user value that the <see cref="DspConnection" /> object will store internally.
		/// </summary>
		/// <value>
		///     The user data.
		/// </value>
		/// <remarks>This function is primarily used in case the user wishes to "attach" data to an <b>FMOD</b> object.</remarks>
		/// <seealso cref="UserDataChanged" />
		public IntPtr UserData
		{
			get
			{
				NativeInvoke(FMOD_DSPConnection_GetUserData(this, out var data));
				return data;
			}
			set
			{
				NativeInvoke(FMOD_DSPConnection_SetUserData(this, value));
				UserDataChanged?.Invoke(this, EventArgs.Empty);
			}
		}

		#endregion

		#region Methods

		/// <summary>
		///     Returns the panning matrix set by the user, for a connection.
		/// </summary>
		/// <param name="matrix">
		///     An array of floating point matrix data, where rows represent output speakers, and columns
		///     represent input channels.
		/// </param>
		/// <param name="outChannels">The number of output channels in the set matrix.</param>
		/// <param name="inChannels">The number of input channels in the set matrix.</param>
		/// <param name="inChannelHop">
		///     The number of floating point values available in the destination memory for a row, so that
		///     the destination memory can be skipped through correctly to write the correct values, if the intended matrix memory
		///     to be written to is wider than the matrix stored in the <see cref="DspConnection" />.
		/// </param>
		/// <seealso cref="DspConnection.SetMixMatrix" />
		public void GetMixMatrix(float[] matrix, out int outChannels, out int inChannels, int inChannelHop)
		{
			NativeInvoke(FMOD_DSPConnection_GetMixMatrix(this, matrix, out outChannels, out inChannels, inChannelHop));
		}

		/// <summary>
		///     <para>Sets a <math>NxN</math> panning matrix on a <see cref="Dsp">DSP</see> connection. </para>
		///     <para>
		///         Skipping/hop is supported, so memory for the matrix can be wider than the width of the
		///         <paramref name="inChannels" /> parameter.
		///     </para>
		/// </summary>
		/// <param name="matrix">
		///     An array of floating point matrix data, where rows represent output speakers, and columns
		///     represent input channels.
		/// </param>
		/// <param name="outChannels">Number of output channels in the matrix being specified. </param>
		/// <param name="inChannels">Number of input channels in the matrix being specified. </param>
		/// <param name="inChannelHop">
		///     Number of floating point values stored in memory for a row, so that the memory can be
		///     skipped through correctly to read the right values, if the intended matrix memory to be read from is wider than the
		///     matrix stored in the <see cref="DspConnection" />.
		/// </param>
		/// <seealso cref="GetMixMatrix" />
		/// <seealso cref="MixMatrixChanged" />
		public void SetMixMatrix(float[] matrix, int outChannels, int inChannels, int inChannelHop)
		{
			NativeInvoke(FMOD_DSPConnection_SetMixMatrix(this, matrix, outChannels, inChannels, inChannelHop));
			MixMatrixChanged?.Invoke(this, EventArgs.Empty);
		}

		#endregion
	}
}