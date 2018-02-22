#region License

// Dsp.Events.cs is distributed under the Microsoft Public License (MS-PL)
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
// Created 12:02 AM 02/20/2018

#endregion

#region Using Directives

using System;
using FMOD.NET.Arguments;

#endregion

namespace FMOD.NET.Core
{
	public partial class Dsp
	{
		#region Events

		/// <summary>
		///     Occurs when the <see cref="Active" /> property has changed.
		/// </summary>
		/// <seealso cref="Active" />
		public event EventHandler ActiveChanged;

		/// <summary>
		///     Occurs when all input DSPs are disconnected.
		/// </summary>
		/// <seealso cref="DisconnectAll" />
		/// <seealso cref="DisconnectInputs" />
		public event EventHandler AllInputsDisconnected;

		/// <summary>
		///     Occurs when all output DSPs are disconnected.
		/// </summary>
		/// <seealso cref="DisconnectAll" />
		/// <seealso cref="DisconnectOutputs" />
		public event EventHandler AllOutputsDisconnected;

		/// <summary>
		///     Occurs when the <see cref="Bypass" /> property has changed.
		/// </summary>
		/// <seealso cref="Bypass" />
		public event EventHandler BypassChanged;

		/// <summary>
		///     Occurs when the <see cref="ChannelFormat" /> property has changed.
		/// </summary>
		/// <seealso cref="ChannelFormat" />
		/// <seealso cref="Data.ChannelFormat" />
		public event EventHandler ChannelFormatChanged;

		/// <summary>
		///     Occurs when an individual DSP is disconnected.
		/// </summary>
		/// <seealso cref="DspDisconnectEventArgs" />
		/// <seealso cref="DisconnectFrom" />
		public event EventHandler<DspDisconnectEventArgs> DspDisconnected;

		/// <summary>
		///     Occurs when a <see cref="Dsp" /> is added to the input.
		/// </summary>
		/// <seealso cref="AddInput" />
		/// <seealso cref="DspInputEventArgs" />
		public event EventHandler<DspInputEventArgs> InputAdded;

		/// <summary>
		///     Occurs when any parameter is changed.
		/// </summary>
		/// <seealso cref="DspParameterEventArgs" />
		/// <seealso cref="SetParameterFloat" />
		/// <seealso cref="SetParameterInt" />
		/// <seealso cref="SetParameterBool" />
		/// <seealso cref="SetParameterData" />
		public event EventHandler<DspParameterEventArgs> ParameterChanged;

		/// <summary>
		///     Occurs when the <see cref="WetDryMix" /> property has changed.
		/// </summary>
		/// <seealso cref="WetDryMix" />
		/// <seealso cref="Data.WetDryMix" />
		public event EventHandler WetDryMixChanged;

		#endregion

		#region Event Invokers

		/// <summary>
		///     Raises the <see cref="ActiveChanged" /> event.
		/// </summary>
		/// <seealso cref="EventArgs" />
		protected virtual void OnActiveChanged()
		{
			ActiveChanged?.Invoke(this, EventArgs.Empty);
		}

		/// <summary>
		///     Raises the <see cref="AllInputsDisconnected" /> event.
		/// </summary>
		/// <seealso cref="EventArgs" />
		protected virtual void OnAllInputsDisconnected()
		{
			AllInputsDisconnected?.Invoke(this, EventArgs.Empty);
		}

		/// <summary>
		///     Raises the <see cref="AllOutputsDisconnected" /> event.
		/// </summary>
		/// <seealso cref="EventArgs" />
		protected virtual void OnAllOutputsDisconnected()
		{
			AllOutputsDisconnected?.Invoke(this, EventArgs.Empty);
		}

		/// <summary>
		///     Raises the <see cref="BypassChanged" /> event.
		/// </summary>
		/// <seealso cref="EventArgs" />
		protected virtual void OnBypassChanged()
		{
			BypassChanged?.Invoke(this, EventArgs.Empty);
		}

		/// <summary>
		///     Raises the <see cref="ChannelFormatChanged" /> event.
		/// </summary>
		/// <seealso cref="EventArgs" />
		protected virtual void OnChannelFormatChanged()
		{
			ChannelFormatChanged?.Invoke(this, EventArgs.Empty);
		}

		/// <summary>
		///     Raises the <see cref="DspDisconnected" /> event.
		/// </summary>
		/// <param name="e">The <see cref="DspDisconnectEventArgs" /> instance containing the event data.</param>
		protected virtual void OnDspDisconnected(DspDisconnectEventArgs e)
		{
			DspDisconnected?.Invoke(this, e);
		}

		/// <summary>
		///     Raises the <see cref="InputAdded" /> event.
		/// </summary>
		/// <param name="e">The <see cref="DspInputEventArgs" /> instance containing the event data.</param>
		protected virtual void OnInputAdded(DspInputEventArgs e)
		{
			InputAdded?.Invoke(this, e);
		}

		/// <summary>
		///     Raises the <see cref="ParameterChanged" /> event.
		/// </summary>
		/// <param name="e">The <see cref="DspParameterEventArgs" /> instance containing the event data.</param>
		protected virtual void OnParameterChanged(DspParameterEventArgs e)
		{
			ParameterChanged?.Invoke(this, e);
		}

		/// <summary>
		///     Raises the <see cref="WetDryMixChanged" /> event.
		/// </summary>
		/// <seealso cref="EventArgs" />
		protected virtual void OnWetDryMixChanged()
		{
			WetDryMixChanged?.Invoke(this, EventArgs.Empty);
		}

		#endregion
	}
}