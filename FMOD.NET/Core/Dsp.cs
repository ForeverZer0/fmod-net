#region License

// Dsp.cs is distributed under the Microsoft Public License (MS-PL)
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
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using FMOD.Arguments;
using FMOD.Data;
using FMOD.DSP;
using FMOD.Enumerations;
using FMOD.Structures;

#endregion

namespace FMOD.Core
{
	/// <inheritdoc />
	/// <summary>
	///     <para>Describes a Digital Signal Processing unit for applying effects on sounds.</para>
	///     <para>This class must be inherited.</para>
	/// </summary>
	/// <seealso cref="T:FMOD.NET.HandleBase" />
	public partial class Dsp : HandleBase
	{
		#region Constructors & Destructor

		/// <summary>
		///     Initializes a new instance of the <see cref="Dsp" /> class.
		/// </summary>
		/// <param name="handle">The handle.</param>
		internal Dsp(IntPtr handle) : base(handle)
		{
		}

		#endregion

		#region Properties & Indexers

		/// <summary>
		///     Occurs when the <see cref="Active" /> property has changed.
		/// </summary>
		/// <seealso cref="Active" />
		public event EventHandler ActiveChanged;

		/// <summary>
		///     Occurs when the <see cref="Bypass" /> property has changed.
		/// </summary>
		/// <seealso cref="Bypass" />
		public event EventHandler BypassChanged;

		/// <summary>
		///     Occurs when the <see cref="ChannelFormat" /> property has changed.
		/// </summary>
		/// <seealso cref="ChannelFormat" />
		/// <seealso cref="FMOD.Data.ChannelFormat" />
		public event EventHandler ChannelFormatChanged;

		/// <summary>
		///     Occurs when the <see cref="UserData" /> property has changed.
		/// </summary>
		/// <seealso cref="UserData" />
		public event EventHandler UserDataChanged;

		/// <summary>
		///     Occurs when the <see cref="WetDryMix" /> property has changed.
		/// </summary>
		/// <seealso cref="WetDryMix" />
		/// <seealso cref="Data.WetDryMix" />
		public event EventHandler WetDryMixChanged;

		/// <summary>
		///     Gets or sets a value indicating whether this <see cref="Dsp" /> is active.
		///     <para>Default = <c>true</c>.</para>
		/// </summary>
		/// <value>
		///     <c>true</c> if active; otherwise, <c>false</c>.
		/// </value>
		/// <seealso cref="ActiveChanged" />
		/// <seealso cref="Bypass" />
		public bool Active
		{
			get
			{
				NativeInvoke(FMOD_DSP_GetActive(this, out var active));
				return active;
			}
			set
			{
				NativeInvoke(FMOD_DSP_SetActive(this, value));
				ActiveChanged?.Invoke(this, EventArgs.Empty);
			}
		}

		/// <summary>
		///     <para>
		///         Gets or sets the enabled state of the read callback of a DSP unit so that it does or doesn't process the data
		///         coming into it.
		///     </para>
		///     <para>A DSP unit that is disabled still processes its inputs, it will just be "dry".</para>
		///     <para>Default = <c>false</c>.</para>
		/// </summary>
		/// <value>
		///     <c>true</c> if bypass; otherwise, <c>false</c>.
		/// </value>
		/// <remarks>
		///     <para>If a unit is bypassed, it will still process its inputs.</para>
		///     <para>To disable the unit and all of its inputs, use <see cref="Active" /> instead.</para>
		/// </remarks>
		/// <seealso cref="BypassChanged" />
		/// <seealso cref="Active" />
		public bool Bypass
		{
			get
			{
				NativeInvoke(FMOD_DSP_GetBypass(this, out var bypass));
				return bypass;
			}
			set
			{
				NativeInvoke(FMOD_DSP_SetBypass(this, value));
				BypassChanged?.Invoke(this, EventArgs.Empty);
			}
		}

		/// <summary>
		///     <para>
		///         Gets or sets the signal format of a <see cref="Dsp" /> unit so that the signal is processed on the speakers
		///         specified.
		///     </para>
		///     <para>
		///         Also defines the number of channels in the unit that a read callback will process, and the output signal of
		///         the unit.
		///     </para>
		/// </summary>
		/// <value>
		///     The channel format.
		/// </value>
		/// <remarks>
		///     See <see cref="Data.ChannelFormat" /> for explanation on parameters.
		/// </remarks>
		/// <seealso cref="ChannelFormatChanged" />
		/// <seealso cref="Data.ChannelFormat" />
		/// <seealso cref="SpeakerMode" />
		/// <seealso cref="ChannelMask" />
		public ChannelFormat ChannelFormat
		{
			get
			{
				NativeInvoke(FMOD_DSP_GetChannelFormat(this, out var mask, out var count, out var mode));
				return new ChannelFormat
				{
					ChannelMask = mask,
					ChannelCount = count,
					SpeakerMode = mode
				};
			}
			set
			{
				NativeInvoke(FMOD_DSP_SetChannelFormat(this, value.ChannelMask,
					value.ChannelCount, value.SpeakerMode));
				ChannelFormatChanged?.Invoke(this, EventArgs.Empty);
			}
		}

		/// <summary>
		///     Gets the pre-defined type of a <b>FMOD</b> registered DSP unit.
		///     <para>
		///         This is only valid for built in <b>FMOD</b> effects. Any user plugins will simply return
		///         <see cref="DspType.Unknown" />.
		///     </para>
		/// </summary>
		/// <value>
		///     The type of the DSP.
		/// </value>
		/// <seealso cref="DspType" />
		/// <seealso cref="FmodSystem.CreateDspByType" />
		public DspType Type
		{
			get
			{
				NativeInvoke(FMOD_DSP_GetType(this, out var type));
				return type;
			}
		}

		/// <summary>
		///     Gets the number of inputs connected to the DSP unit.
		///     <para>Performance Warning! See remarks.</para>
		/// </summary>
		/// <value>
		///     The input count.
		/// </value>
		/// <remarks>
		///     <para>Inputs are units that feed data to this unit. When there are multiple inputs, they are mixed together.</para>
		///     <alert class="warning">
		///         <para>
		///             Because this function needs to flush the <see cref="Dsp" /> queue before it can determine how many units
		///             are available, this function may block significantly while the background mixer thread operates.
		///         </para>
		///     </alert>
		/// </remarks>
		/// <seealso cref="OutputCount" />
		public int InputCount
		{
			get
			{
				NativeInvoke(FMOD_DSP_GetNumInputs(this, out var count));
				return count;
			}
		}

		/// <summary>
		///     <para>Gets or sets a value indicating whether input metering is enabled.</para>
		///     <para>
		///         When enabled, the <see cref="Dsp" /> will return metering information, and is required for the FMOD Studio
		///         profiler tool to visualize the levels.
		///     </para>
		///     <para><c>true</c> to enable metering for the input signal (pre-processing), ,<c>false</c> to turn it off. </para>
		/// </summary>
		/// <value>
		///     <c>true</c> to enable metering for the input signal (pre-processing), ,<c>false</c> to turn it off.
		/// </value>
		/// <remarks>
		///     <see cref="InitFlags.ProfileMeterAll" /> with <see cref="O:FMOD.Core.FmodSystem.Initialize" /> will automatically
		///     turn on metering for all DSP units inside the <b>FMOD</b> mixer graph.
		/// </remarks>
		/// <seealso cref="InitFlags" />
		/// <seealso cref="O:FMOD.Core.FmodSystem.Initialize" />
		/// <seealso cref="OutputMeteringEnabled" />
		/// <seealso cref="EnableMetering" />
		/// <seealso cref="DisableMetering" />
		public bool InputMeteringEnabled
		{
			get
			{
				NativeInvoke(FMOD_DSP_GetMeteringEnabled(this, out var input, out var dummy));
				return input;
			}
			set
			{
				var output = OutputMeteringEnabled;
				NativeInvoke(FMOD_DSP_SetMeteringEnabled(this, value, output));
			}
		}

		/// <summary>
		///     <para>Gets or sets a value indicating whether output metering is enabled.</para>
		///     <para>
		///         When enabled, the <see cref="Dsp" /> will return metering information, and is required for the FMOD Studio
		///         profiler tool to visualize the levels.
		///     </para>
		///     <para><c>true</c> to enable metering for the output signal (post-processing), ,<c>false</c> to turn it off. </para>
		/// </summary>
		/// <value>
		///     <c>true</c> to enable metering for the output signal (pre-processing), ,<c>false</c> to turn it off.
		/// </value>
		/// <remarks>
		///     <see cref="InitFlags.ProfileMeterAll" /> with <see cref="O:FMOD.Core.FmodSystem.Initialize" /> will automatically
		///     turn on metering for all DSP units inside the <b>FMOD</b> mixer graph.
		/// </remarks>
		/// <seealso cref="InitFlags" />
		/// <seealso cref="O:FMOD.Core.FmodSystem.Initialize" />
		/// <seealso cref="InputMeteringEnabled" />
		/// <seealso cref="EnableMetering" />
		/// <seealso cref="DisableMetering" />
		public bool OutputMeteringEnabled
		{
			get
			{
				NativeInvoke(FMOD_DSP_GetMeteringEnabled(this, out var dummy, out var output));
				return output;
			}
			set
			{
				var input = InputMeteringEnabled;
				NativeInvoke(FMOD_DSP_SetMeteringEnabled(this, input, value));
			}
		}

		/// <summary>
		///     Helper function to enable the both input and output metering.
		/// </summary>
		/// <seealso cref="DisableMetering" />
		/// <seealso cref="InputMeteringEnabled" />
		/// <seealso cref="OutputMeteringEnabled" />
		public void EnableMetering()
		{
			NativeInvoke(FMOD_DSP_SetMeteringEnabled(this, true, true));
		}

		/// <summary>
		///     Helper function to disable the both input and output metering.
		/// </summary>
		/// <seealso cref="EnableMetering" />
		/// <seealso cref="InputMeteringEnabled" />
		/// <seealso cref="OutputMeteringEnabled" />
		public void DisableMetering()
		{
			NativeInvoke(FMOD_DSP_SetMeteringEnabled(this, false, false));
		}

		/// <summary>
		///     <para>Gets the idle state of a DSP.</para>
		///     <para>A DSP is idle when no signal is coming into it. </para>
		///     <para>
		///         This can be a useful method of determining if a DSP sub branch is finished processing, so it can be
		///         disconnected for example.
		///     </para>
		/// </summary>
		/// <value>
		///     <c>true</c> if this instance is idle; otherwise, <c>false</c>.
		/// </value>
		public bool IsIdle
		{
			get
			{
				NativeInvoke(FMOD_DSP_GetIdle(this, out var idle));
				return idle;
			}
		}

		/// <summary>
		///     <para>Gets the the number of outputs connected to the DSP unit.</para>
		///     <para>Performance Warning! See remarks.</para>
		/// </summary>
		/// <value>
		///     The output count.
		/// </value>
		/// <remarks>
		///     <para>
		///         Outputs are units that this unit feeds data to. When there are multiple outputs, the data is split and sent
		///         to each unit individually.
		///     </para>
		///     <alert class="warning">
		///         Because this function needs to flush the <see cref="Dsp" /> queue before it can determine how many units are
		///         available, this function may block significantly while the background mixer thread operates.
		///     </alert>
		/// </remarks>
		/// <seealso cref="InputCount" />
		public int OutputCount
		{
			get
			{
				NativeInvoke(FMOD_DSP_GetNumOutputs(this, out var count));
				return count;
			}
		}

		/// <summary>
		///     <para>Gets the number of parameters a DSP unit has to control its behaviour.</para>
		///     <para>Use this to enumerate all parameters of a DSP unit with <see cref="GetParameterInfo" />.</para>
		/// </summary>
		/// <value>
		///     The parameter count.
		/// </value>
		/// <seealso cref="GetParameterInfo" />
		/// <seealso cref="GetParameterBool" />
		/// <seealso cref="GetParameterInt" />
		/// <seealso cref="GetParameterFloat" />
		/// <seealso cref="GetParameterData" />
		/// <seealso cref="SetParameterBool" />
		/// <seealso cref="SetParameterInt" />
		/// <seealso cref="SetParameterFloat" />
		/// <seealso cref="SetParameterData" />
		public int ParameterCount
		{
			get
			{
				NativeInvoke(FMOD_DSP_GetNumParameters(this, out var count));
				return count;
			}
		}

		/// <summary>
		///     Gets the parent <see cref="FmodSystem" /> object that was used to create this object.
		/// </summary>
		/// <value>
		///     The parent <b>FMOD</b> system instance.
		/// </value>
		/// <seealso cref="FmodSystem" />
		public FmodSystem ParentFmodSystem
		{
			get
			{
				NativeInvoke(FMOD_DSP_GetSystemObject(this, out var system));
				return Factory.Create<FmodSystem>(system);
			}
		}

		/// <summary>
		///     Gets or sets a user value that the DSP object will store internally.
		/// </summary>
		/// <value>
		///     The user data.
		/// </value>
		/// <remarks>
		///     <para>This function is primarily used in case the user wishes to "attach" data to an <b>FMOD</b> object.</para>
		///     <para>
		///         It can be useful if an FMOD callback passes an object of this type as a parameter, and the user does not know
		///         which object it is (if many of these types of objects exist).
		///     </para>
		/// </remarks>
		/// <seealso cref="UserDataChanged" />
		public IntPtr UserData
		{
			get
			{
				NativeInvoke(FMOD_DSP_GetUserData(this, out var data));
				return data;
			}
			set
			{
				NativeInvoke(FMOD_DSP_SetUserData(this, value));
				UserDataChanged?.Invoke(this, EventArgs.Empty);
			}
		}

		/// <summary>
		///     Gets or sets the wet dry mix.
		///     <para>
		///         Allows the user to scale the affect of a DSP effect, through control of the "wet" mix, which is the
		///         post-processed signal and the "dry" which is the pre-processed signal.
		///     </para>
		///     <para>See <see cref="Data.WetDryMix" /> for details on how the values affect the DSP.</para>
		/// </summary>
		/// <value>
		///     The wet-dry mix.
		/// </value>
		/// <seealso cref="WetDryMixChanged" />
		/// <seealso cref="Data.WetDryMix" />
		public WetDryMix WetDryMix
		{
			get
			{
				NativeInvoke(FMOD_DSP_GetWetDryMix(this, out var prewet, out var postwet, out var dry));
				return new WetDryMix
				{
					PreWet = prewet,
					PostWet = postwet,
					Dry = dry
				};
			}
			set
			{
				NativeInvoke(FMOD_DSP_SetWetDryMix(this, value.PreWet, value.PostWet, value.Dry));
				WetDryMixChanged?.Invoke(this, EventArgs.Empty);
			}
		}

		#endregion

		#region Methods

		/// <summary>
		///     Occurs when a <see cref="Dsp" /> is added to the input.
		/// </summary>
		/// <seealso cref="AddInput" />
		/// <seealso cref="DspInputEventArgs" />
		public event EventHandler<DspInputEventArgs> InputAdded;

		/// <summary>
		///     Adds the specified DSP unit as an input of the DSP object.
		/// </summary>
		/// <param name="dsp">The DSP unit to add as an input of the current unit.</param>
		/// <param name="type">
		///     <para>The type of connection between the two units.</para>
		///     <para>See <see cref="DspConnectionType" /> for details.</para>
		/// </param>
		/// <returns>The newly created connection between the two units.</returns>
		/// <remarks>
		///     <para>
		///         If you want to add a unit as an output of another unit, then add "this" unit as an input of that unit
		///         instead.
		///     </para>
		///     <para>
		///         Inputs are automatically mixed together, then the mixed data is sent to the unit's output(s).<lineBreak />
		///         To find the number of inputs or outputs a unit has use <see cref="InputCount" /> or <see cref="OutputCount" />.
		///     </para>
		///     <alert class="caution">
		///         The returned <see cref="DspConnection" /> will become invalid if the DSP is disconnected, and could cause and
		///         exception if an attempt is made to use a reference to it.
		///     </alert>
		/// </remarks>
		/// <seealso cref="InputAdded" />
		/// <seealso cref="DspConnection" />
		/// <seealso cref="DspConnectionType" />
		/// <seealso cref="InputCount" />
		/// <seealso cref="OutputCount" />
		/// <seealso cref="DisconnectFrom(FMOD.Core.Dsp, DspConnection)" />
		public DspConnection AddInput(Dsp dsp, DspConnectionType type = DspConnectionType.Standard)
		{
			NativeInvoke(FMOD_DSP_AddInput(this, dsp, out var connection, type));
			var dspConnection = Factory.Create<DspConnection>(connection);
			InputAdded?.Invoke(this, new DspInputEventArgs(dspConnection, type));
			return dspConnection;
		}

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
		///     <para>Helper function to disconnect all inputs and outputs of a DSP unit.</para>
		///     <para>This function is optimized to be faster than disconnecting inputs and outputs manually one by one.</para>
		/// </summary>
		/// <remarks>
		///     <alert class="note">This method <b>DOES NOT</b> raise a <see cref="DspDisconnected" /> event.</alert>
		///     <alert class="warning">
		///         If you have a reference to a <see cref="DspConnection" /> that bind any of the inputs or outputs to this DSP
		///         unit, then they will become invalid. The connections are sent back to a freelist to be re-used again by a later
		///         <see cref="AddInput" /> command.
		///     </alert>
		/// </remarks>
		/// <seealso cref="AllInputsDisconnected" />
		/// <seealso cref="AllOutputsDisconnected" />
		/// <seealso cref="DspConnection" />
		/// <seealso cref="DisconnectFrom" />
		/// <seealso cref="DisconnectInputs" />
		/// <seealso cref="DisconnectOutputs" />
		/// <seealso cref="AddInput" />
		public void DisconnectAll()
		{
			NativeInvoke(FMOD_DSP_DisconnectAll(this, true, true));
			AllInputsDisconnected?.Invoke(this, EventArgs.Empty);
			AllOutputsDisconnected?.Invoke(this, EventArgs.Empty);
		}

		/// <summary>
		///     <para>Helper function to disconnect all inputs of a DSP unit.</para>
		///     <para>This function is optimized to be faster than disconnecting inputs and outputs manually one by one.</para>
		/// </summary>
		/// <remarks>
		///     <alert class="note">This method <b>DOES NOT</b> raise a <see cref="DspDisconnected" /> event.</alert>
		///     <alert class="warning">
		///         If you have a reference to a <see cref="DspConnection" /> that bind any of the inputs or outputs to this DSP
		///         unit, then they will become invalid. The connections are sent back to a freelist to be re-used again by a later
		///         <see cref="AddInput" /> command.
		///     </alert>
		/// </remarks>
		/// <seealso cref="AllInputsDisconnected" />
		/// <seealso cref="DspConnection" />
		/// <seealso cref="DisconnectFrom" />
		/// <seealso cref="DisconnectAll" />
		/// <seealso cref="DisconnectOutputs" />
		/// <seealso cref="AddInput" />
		public void DisconnectInputs()
		{
			NativeInvoke(FMOD_DSP_DisconnectAll(this, true, false));
			AllInputsDisconnected?.Invoke(this, EventArgs.Empty);
		}

		/// <summary>
		///     <para>Helper function to disconnect all outputs of a DSP unit.</para>
		///     <para>This function is optimized to be faster than disconnecting inputs and outputs manually one by one.</para>
		/// </summary>
		/// <remarks>
		///     <alert class="note">This method <b>DOES NOT</b> raise a <see cref="DspDisconnected" /> event.</alert>
		///     <alert class="warning">
		///         If you have a reference to a <see cref="DspConnection" /> that bind any of the inputs or outputs to this DSP
		///         unit, then they will become invalid. The connections are sent back to a freelist to be re-used again by a later
		///         <see cref="AddInput" /> command.
		///     </alert>
		/// </remarks>
		/// <seealso cref="AllOutputsDisconnected" />
		/// <seealso cref="DspConnection" />
		/// <seealso cref="DisconnectFrom" />
		/// <seealso cref="DisconnectAll" />
		/// <seealso cref="DisconnectInputs" />
		/// <seealso cref="AddInput" />
		public void DisconnectOutputs()
		{
			NativeInvoke(FMOD_DSP_DisconnectAll(this, false, true));
			AllOutputsDisconnected?.Invoke(this, EventArgs.Empty);
		}

		/// <summary>
		///     Disconnect the DSP unit from the specified input.
		/// </summary>
		/// <param name="dsp">
		///     <para>The input unit that this unit is to be disconnected from.</para>
		///     <para>Specify <c>null</c> to disconnect the unit from all outputs and inputs.</para>
		/// </param>
		/// <param name="connection">
		///     If there is more than one connection between two <see cref="Dsp" /> units, this can be used to
		///     define which of the connections should be disconnected.
		/// </param>
		/// <remarks>
		///     <para>
		///         Note that when you disconnect a unit, it is up to you to reconnect the network so that data flow can
		///         continue.
		///     </para>
		///     <alert class="warning">
		///         If you have a reference to a <see cref="DspConnection" /> that bind any of the inputs or outputs to this DSP
		///         unit, then they will become invalid. The connections are sent back to a freelist to be re-used again by a later
		///         <see cref="AddInput" /> command.
		///     </alert>
		/// </remarks>
		/// <seealso cref="DspDisconnected" />
		/// <seealso cref="DspConnection" />
		/// <seealso cref="DisconnectAll" />
		/// <seealso cref="DisconnectInputs" />
		/// <seealso cref="DisconnectOutputs" />
		/// <seealso cref="AddInput" />
		public void DisconnectFrom(Dsp dsp, DspConnection connection = null)
		{
			NativeInvoke(FMOD_DSP_DisconnectFrom(this, dsp ?? IntPtr.Zero, connection ?? IntPtr.Zero));
			DspDisconnected?.Invoke(this, new DspDisconnectEventArgs(dsp, connection));
		}

		/// <summary>
		///     Occurs when an individual DSP is disconnected.
		/// </summary>
		/// <seealso cref="DspDisconnectEventArgs" />
		/// <seealso cref="DisconnectFrom" />
		public event EventHandler<DspDisconnectEventArgs> DspDisconnected;

		/// <summary>
		///     Internal factory method for creating DSPs from a <see cref="DspType" /> enumeration.
		/// </summary>
		/// <param name="dspHandle">The DSP handle.</param>
		/// <param name="dspType">Type of the DSP.</param>
		/// <returns>A newly created DSP.</returns>
		internal static Dsp FromType(IntPtr dspHandle, DspType dspType)
		{
			if (dspHandle == IntPtr.Zero)
				return null;
#pragma warning disable 618
			switch (dspType)
			{
				case DspType.Unknown:
					return null;
				case DspType.Mixer:
					return Factory.Create<Mixer>(dspHandle);
				case DspType.Oscillator:
					return Factory.Create<Oscillator>(dspHandle);
				case DspType.Lowpass:
					return Factory.Create<Lowpass>(dspHandle);
				case DspType.ItLowpass:
					return Factory.Create<ItLowpass>(dspHandle);
				case DspType.Highpass:
					return Factory.Create<Highpass>(dspHandle);
				case DspType.Echo:
					return Factory.Create<Echo>(dspHandle);
				case DspType.Fader:
					return Factory.Create<Fader>(dspHandle);
				case DspType.Flange:
					return Factory.Create<Flange>(dspHandle);
				case DspType.Distortion:
					return Factory.Create<Distortion>(dspHandle);
				case DspType.Normalize:
					return Factory.Create<Normalize>(dspHandle);
				case DspType.Limiter:
					return Factory.Create<Limiter>(dspHandle);
				case DspType.ParamEq:
					return Factory.Create<ParamEq>(dspHandle);
				case DspType.PitchShift:
					return Factory.Create<PitchShift>(dspHandle);
				case DspType.Chorus:
					return Factory.Create<Chorus>(dspHandle);
				case DspType.VstPlugin:
					return null;
				case DspType.WinampPlugin:
					return null;
				case DspType.ItEcho:
					return Factory.Create<ItEcho>(dspHandle);
				case DspType.Compressor:
					return Factory.Create<Compressor>(dspHandle);
				case DspType.SfxReverb:
					return Factory.Create<SfxReverb>(dspHandle);
				case DspType.LowpassSimple:
					return Factory.Create<LowpassSimple>(dspHandle);
				case DspType.Delay:
					return Factory.Create<Delay>(dspHandle);
				case DspType.Tremolo:
					return Factory.Create<Tremolo>(dspHandle);
				case DspType.LadspaPlugin:
					return null;
				case DspType.Send:
					return Factory.Create<Send>(dspHandle);
				case DspType.Return:
					return Factory.Create<Return>(dspHandle);
				case DspType.HighpassSimple:
					return Factory.Create<HighpassSimple>(dspHandle);
				case DspType.Pan:
					return Factory.Create<Pan>(dspHandle);
				case DspType.ThreeEq:
					return Factory.Create<ThreeEq>(dspHandle);
				case DspType.Fft:
					return Factory.Create<Fft>(dspHandle);
				case DspType.LoudnessMeter:
					return Factory.Create<LoudnessMeter>(dspHandle);
				case DspType.EnvelopeFollower:
					return Factory.Create<EnvelopeFollower>(dspHandle);
				case DspType.ConvolutionReverb:
					return Factory.Create<ConvolutionReverb>(dspHandle);
				case DspType.ChannelMix:
					return Factory.Create<ChannelMix>(dspHandle);
				case DspType.Transceiver:
					return Factory.Create<Transceiver>(dspHandle);
				case DspType.ObjectPan:
					return Factory.Create<ObjectPan>(dspHandle);
				case DspType.MultiBandEq:
					return Factory.Create<MultiBandEq>(dspHandle);
				case DspType.Max:
					return null;
				default:
					return null;
			}
#pragma warning restore 618
		}

		/// <summary>
		///     Retrieve the index of the first data parameter of a particular data type.
		/// </summary>
		/// <param name="dataType">
		///     <para>The type of data to find.</para>
		///     <para>
		///         This would usually be set to a value defined in <see cref="DspParameterDataTypes" /> (cast to
		///         <see cref="int" />) but can be any value for custom types.
		///     </para>
		/// </param>
		/// <returns>
		///     Contains the index of the first data parameter of type <paramref name="dataType" /> after the function is
		///     called. Will be <c>-1</c> if no matches were found.
		/// </returns>
		/// <remarks>
		///     <alert class="note">
		///         This function will be largely unused in <b>FMOD.NET</b>, as all built-in DSP types are
		///         implemented individually with named classes and methods, but it is here for custom DSPs and the sake of
		///         completeness.
		///     </alert>
		/// </remarks>
		public int GetDataParameterIndex(int dataType)
		{
			NativeInvoke(FMOD_DSP_GetDataParameterIndex(this, dataType, out var index));
			return index;
		}

		/// <summary>
		///     Retrieves information about the current DSP unit, including name, version, default channels and size of
		///     configuration dialog box if it exists.
		/// </summary>
		/// <returns>A <see cref="DspInfo" /> object containing the information.</returns>
		/// <seealso cref="DspInfo" />
		/// <seealso cref="ShowConfigDialog()" />
		/// <seealso cref="ShowConfigDialog(IntPtr, bool)" />
		public DspInfo GetInfo()
		{
			var namePtr = Marshal.StringToHGlobalAnsi(new String(' ', 32));
			NativeInvoke(FMOD_DSP_GetInfo(this, namePtr, out var version, out var channels,
				out var width, out var height));
			return new DspInfo
			{
				Name = Marshal.PtrToStringAnsi(namePtr, 32).Trim(),
				Version = Util.UInt32ToVersion(version),
				ChannelCount = channels,
				ConfigWindowSize = new Size(width, height)
			};
		}

		/// <summary>
		///     <para>Retrieves a DSP unit which is acting as an input to this unit.</para>
		///     <para>Performance Warning! See remarks.</para>
		/// </summary>
		/// <param name="index">Index of the input unit to retrieve.</param>
		/// <returns>The desired input DSP.</returns>
		/// <remarks>
		///     <para>
		///         An input is a unit which feeds audio data to this unit.<lineBreak />
		///         If there are more than 1 input to this unit, the inputs will be mixed, and the current unit processes the mixed
		///         result.<lineBreak />
		///         Find out the number of input units to this unit by checking <see cref="InputCount" />.
		///     </para>
		///     <alert class="note">
		///         Because this function needs to flush the <see cref="Dsp" /> queue before it can determine if
		///         the specified numerical input is available or not, this function may block significantly while the background
		///         mixer thread operates.
		///     </alert>
		/// </remarks>
		/// <seealso cref="GetInputConnection" />
		/// <seealso cref="InputCount" />
		/// <seealso cref="GetOutput" />
		/// <seealso cref="DspConnection.Mix" />
		public Dsp GetInput(int index)
		{
			NativeInvoke(FMOD_DSP_GetInput(this, index, out var input, out var dummy));
			return Factory.Create<Dsp>(input);
		}

		/// <summary>
		///     <para>Retrieves the connection to the DSP unit which is acting as an input to this unit.</para>
		///     <para>Performance Warning! See remarks.</para>
		/// </summary>
		/// <param name="index">Index of the input unit connection to retrieve.</param>
		/// <returns>The desired input DSP connection.</returns>
		/// <seealso cref="GetInput" />
		/// <seealso cref="InputCount" />
		/// <seealso cref="DspConnection.Mix" />
		public DspConnection GetInputConnection(int index)
		{
			NativeInvoke(FMOD_DSP_GetInput(this, index, out var dummy, out var connection));
			return Factory.Create<DspConnection>(connection);
		}

		/// <summary>
		///     <para>Retrieves the DSP unit which is acting as an output to this unit.</para>
		///     <para>Performance Warning! See remarks.</para>
		/// </summary>
		/// <param name="index">Index of the output unit to retrieve.</param>
		/// <returns>The desired output unit. </returns>
		/// <remarks>
		///     <para>An output is a unit which this unit will feed data too once it has processed its data.</para>
		///     <para>Find out the number of output units to this unit by calling <see cref="OutputCount" />.</para>
		///     <alert class="note">
		///         Because this function needs to flush the <see cref="Dsp" /> queue before it can determine if
		///         the specified numerical input is available or not, this function may block significantly while the background
		///         mixer thread operates.
		///     </alert>
		/// </remarks>
		/// <seealso cref="GetOutputConnection" />
		/// <seealso cref="OutputCount" />
		/// <seealso cref="GetInput" />
		/// <seealso cref="DspConnection.Mix" />
		public Dsp GetOutput(int index)
		{
			NativeInvoke(FMOD_DSP_GetOutput(this, index, out var output, out var dummy));
			return Factory.Create<Dsp>(output);
		}

		/// <summary>
		///     <para>Retrieves the connection to the DSP unit which is acting as an output to this unit.</para>
		///     <para>Performance Warning! See remarks.</para>
		/// </summary>
		/// <param name="index">Index of the output unit connection to retrieve.</param>
		/// <returns>The desired output DSP connection.</returns>
		/// <seealso cref="GetOutput" />
		/// <seealso cref="OutputCount" />
		/// <seealso cref="DspConnection.Mix" />
		public DspConnection GetOutputConnection(int index)
		{
			NativeInvoke(FMOD_DSP_GetOutput(this, index, out var dummy, out var connection));
			return Factory.Create<DspConnection>(connection);
		}

		/// <summary>
		///     Call the DSP process function to retrieve the output signal format for a DSP based on input values, automatically
		///     applying the input signal format as an argument.
		/// </summary>
		/// <returns>A <see cref="ChannelFormat" /> instance describing the output signal format.</returns>
		/// <remarks>
		///     <para>
		///         A DSP unit may be an up mixer or down mixer for example. In this case if you specified 6 in for a downmixer,
		///         it may provide you with 2 out for example.
		///     </para>
		///     <para>
		///         Generally the input values will be reproduced for the output values, but some DSP units will want to alter
		///         the output format.
		///     </para>
		/// </remarks>
		/// <seealso cref="ChannelFormat" />
		/// <seealso cref="Data.ChannelFormat" />
		/// <seealso cref="GetOutputChannelFormat(Data.ChannelFormat)" />
		/// <seealso cref="GetOutputChannelFormat(ChannelMask, int, SpeakerMode)" />
		public ChannelFormat GetOutputChannelFormat()
		{
			return GetOutputChannelFormat(ChannelFormat);
		}

		/// <summary>
		///     Call the DSP process function to retrieve the output signal format for a DSP based on input values.
		/// </summary>
		/// <param name="inputFormat">A <see cref="Data.ChannelFormat" /> instance describing the input signal format.</param>
		/// <returns>A <see cref="ChannelFormat" /> instance describing the output signal format.</returns>
		/// <remarks>
		///     <para>
		///         A DSP unit may be an up mixer or down mixer for example. In this case if you specified 6 in for a downmixer,
		///         it may provide you with 2 out for example.
		///     </para>
		///     <para>
		///         Generally the input values will be reproduced for the output values, but some DSP units will want to alter
		///         the output format.
		///     </para>
		/// </remarks>
		/// <seealso cref="ChannelFormat" />
		/// <seealso cref="Data.ChannelFormat" />
		/// <seealso cref="GetOutputChannelFormat()" />
		/// <seealso cref="GetOutputChannelFormat(ChannelMask, int, SpeakerMode)" />
		public ChannelFormat GetOutputChannelFormat(ChannelFormat inputFormat)
		{
			return GetOutputChannelFormat(inputFormat.ChannelMask, inputFormat.ChannelCount, inputFormat.SpeakerMode);
		}

		/// <summary>
		///     Call the DSP process function to retrieve the output signal format for a DSP based on input values.
		/// </summary>
		/// <param name="inputMask">
		///     <para>Channel bitmask representing the speakers enabled for the incoming signal.</para>
		///     <para>
		///         For example a 5.1 signal could have <paramref name="inputChannelCount" /> <c>2</c> that represent
		///         <see cref="ChannelMask.SurroundLeft" /> and <see cref="ChannelMask.SurroundRight" />.
		///     </para>
		/// </param>
		/// <param name="inputChannelCount">Number of channels for the incoming signal. </param>
		/// <param name="inputSpeakerMode">Speaker mode for the incoming signal.</param>
		/// <returns>A <see cref="ChannelFormat" /> instance describing the output signal format.</returns>
		/// <remarks>
		///     <para>
		///         A DSP unit may be an up mixer or down mixer for example. In this case if you specified 6 in for a downmixer,
		///         it may provide you with 2 out for example.
		///     </para>
		///     <para>
		///         Generally the input values will be reproduced for the output values, but some DSP units will want to alter
		///         the output format.
		///     </para>
		/// </remarks>
		/// <seealso cref="ChannelFormat" />
		/// <seealso cref="Data.ChannelFormat" />
		/// <seealso cref="GetOutputChannelFormat()" />
		/// <seealso cref="GetOutputChannelFormat(Data.ChannelFormat)" />
		public ChannelFormat GetOutputChannelFormat(ChannelMask inputMask, int inputChannelCount,
			SpeakerMode inputSpeakerMode)
		{
			NativeInvoke(FMOD_DSP_GetOutputChannelFormat(this, inputMask, inputChannelCount, inputSpeakerMode,
				out var chanMask, out var chanCount, out var speakerMode));
			return new ChannelFormat
			{
				ChannelMask = chanMask,
				ChannelCount = chanCount,
				SpeakerMode = speakerMode
			};
		}

		/// <summary>
		///     Retrieve information about a specified parameter within the DSP unit.
		/// </summary>
		/// <param name="parameterIndex">
		///     Parameter index for this unit. Find the number of parameters with
		///     <see cref="ParameterCount" />.
		/// </param>
		/// <returns>A <see cref="DspParameterDesc" /> structure containing the relevant parameter information.</returns>
		/// <seealso cref="DspParameterDesc" />
		/// <seealso cref="ParameterCount" />
		/// <seealso cref="GetParameterBool" />
		/// <seealso cref="GetParameterData" />
		/// <seealso cref="GetParameterFloat" />
		/// <seealso cref="GetParameterInt" />
		/// <seealso cref="SetParameterBool" />
		/// <seealso cref="SetParameterData" />
		/// <seealso cref="SetParameterFloat" />
		/// <seealso cref="SetParameterInt" />
		public DspParameterDesc GetParameterInfo(int parameterIndex)
		{
			NativeInvoke(FMOD_DSP_GetParameterInfo(this, parameterIndex, out var desc));
			return (DspParameterDesc) Marshal.PtrToStructure(desc, typeof(DspParameterDesc));
		}

		/// <summary>
		///     Calls the DSP unit's reset function, which will clear internal buffers and reset the unit back to an initial state.
		/// </summary>
		/// <remarks>
		///     <para>Calling this function is useful if the DSP unit relies on a history to process itself (ie an echo filter).</para>
		///     <para>
		///         If you disconnected the unit and reconnected it to a different part of the network with a different sound,
		///         you would want to call this to reset the units state (ie clear and reset the echo filter) so that you dont get
		///         left over artifacts from the place it used to be connected.
		///     </para>
		/// </remarks>
		public void Reset()
		{
			NativeInvoke(FMOD_DSP_Reset(this));
		}

		/// <summary>
		///     Display or hide a DSP unit configuration in a newly created window and returns it.
		/// </summary>
		/// <returns>The window containing the configuration.</returns>
		public Form ShowConfigDialog()
		{
			var info = GetInfo();
			var form = new Form
			{
				Size = info.ConfigWindowSize,
				MaximizeBox = false,
				MinimizeBox = false,
				ShowIcon = false,
				Text = info.Name
			};
			form.Show();
			ShowConfigDialog(form.Handle);
			return form;
		}

		/// <summary>
		///     Display or hide a DSP unit configuration dialog box inside the target window.
		/// </summary>
		/// <param name="hwnd">Handle tothe target window to display configuration dialog.</param>
		/// <param name="show">
		///     <para><c>true</c> = show dialog box inside target <paramref name="hwnd" />.</para>
		///     <para><c>false</c> = remove dialog from target <paramref name="hwnd" />.</para>
		/// </param>
		public void ShowConfigDialog(IntPtr hwnd, bool show = true)
		{
			NativeInvoke(FMOD_DSP_ShowConfigDialog(this, hwnd, show));
		}

		/// <summary>
		///     <para>Gets a string containing a formatted or more meaningful representation of the DSP parameter's value.</para>
		///     <para>
		///         For example if a switch parameter has on and off (<c>0.0</c> or <c>1.0</c>) it will display "ON" or "OFF" by
		///         using this parameter.
		///     </para>
		/// </summary>
		/// <param name="index">Index for desired parameter.</param>
		/// <returns>A formatted string representation of the value.</returns>
		public string GetValueString(int index)
		{
			var type = GetParameterInfo(index).Type;
			using (var buffer = new MemoryBuffer(16))
			{
				switch (type)
				{
					case DspParameterType.Float:
						NativeInvoke(FMOD_DSP_GetParameterFloat(this, index, out var dummy1, buffer.Pointer, 16));
						break;
					case DspParameterType.Int:
						NativeInvoke(FMOD_DSP_GetParameterInt(this, index, out var dummy2, buffer.Pointer, 16));
						break;
					case DspParameterType.Bool:
						NativeInvoke(FMOD_DSP_GetParameterBool(this, index, out var dummy3, buffer.Pointer, 16));
						break;
					case DspParameterType.Data:
						NativeInvoke(FMOD_DSP_GetParameterData(this, index, out var dummy4, out var dummy5, buffer.Pointer, 16));
						break;
				}
				return buffer.ToString(Encoding.UTF8);
			}
		}

		/// <summary>
		///     Retrieves a DSP unit's boolean parameter by index.
		/// </summary>
		/// <param name="index">The index.</param>
		/// <returns>The value of the specified parameter.</returns>
		/// <alert class="note">
		///     This function will be largely unused in <b>FMOD.NET</b>, as all built-in DSP types are
		///     implemented individually with named classes and methods, but it is here for custom DSPs and the sake of
		///     completeness.
		/// </alert>
		/// <seealso cref="ParameterCount" />
		/// <seealso cref="GetParameterData" />
		/// <seealso cref="GetParameterFloat" />
		/// <seealso cref="GetParameterInt" />
		/// <seealso cref="SetParameterBool" />
		/// <seealso cref="SetParameterData" />
		/// <seealso cref="SetParameterFloat" />
		/// <seealso cref="SetParameterInt" />
		/// <seealso cref="GetValueString" />
		public bool GetParameterBool(int index)
		{
			NativeInvoke(FMOD_DSP_GetParameterBool(this, index, out var value, IntPtr.Zero, 0));
			return value;
		}

		/// <summary>
		///     Retrieves a DSP unit's boolean parameter by index.
		/// </summary>
		/// <param name="index">The index.</param>
		/// <returns>The value of the specified parameter.</returns>
		/// <alert class="note">
		///     This function will be largely unused in <b>FMOD.NET</b>, as all built-in DSP types are
		///     implemented individually with named classes and methods, but it is here for custom DSPs and the sake of
		///     completeness.
		/// </alert>
		/// <seealso cref="ParameterCount" />
		/// <seealso cref="GetParameterBool" />
		/// <seealso cref="GetParameterFloat" />
		/// <seealso cref="GetParameterInt" />
		/// <seealso cref="SetParameterBool" />
		/// <seealso cref="SetParameterData" />
		/// <seealso cref="SetParameterFloat" />
		/// <seealso cref="SetParameterInt" />
		/// <seealso cref="GetValueString" />
		public byte[] GetParameterData(int index)
		{
			NativeInvoke(FMOD_DSP_GetParameterData(this, index, out var ptr, out var size, IntPtr.Zero, 0));
			var bytes = new byte[size];
			Marshal.Copy(ptr, bytes, 0, (int) size);
			return bytes;
		}

		/// <summary>
		///     Retrieves a DSP unit's float parameter by index.
		/// </summary>
		/// <param name="index">The index.</param>
		/// <returns>The value of the specified parameter.</returns>
		/// <seealso cref="ParameterCount" />
		/// <seealso cref="GetParameterBool" />
		/// <seealso cref="GetParameterData" />
		/// <seealso cref="GetParameterInt" />
		/// <seealso cref="SetParameterBool" />
		/// <seealso cref="SetParameterData" />
		/// <seealso cref="SetParameterFloat" />
		/// <seealso cref="SetParameterInt" />
		/// <seealso cref="GetValueString" />
		public float GetParameterFloat(int index)
		{
			NativeInvoke(FMOD_DSP_GetParameterFloat(this, index, out var value, IntPtr.Zero, 0));
			return value;
		}

		/// <summary>
		///     Retrieves a DSP unit's integer parameter by index.
		/// </summary>
		/// <param name="index">The index.</param>
		/// <returns>The value of the specified parameter.</returns>
		/// <alert class="note">
		///     This function will be largely unused in <b>FMOD.NET</b>, as all built-in DSP types are
		///     implemented individually with named classes and methods, but it is here for custom DSPs and the sake of
		///     completeness.
		/// </alert>
		/// <seealso cref="ParameterCount" />
		/// <seealso cref="GetParameterBool" />
		/// <seealso cref="GetParameterData" />
		/// <seealso cref="GetParameterFloat" />
		/// <seealso cref="SetParameterBool" />
		/// <seealso cref="SetParameterData" />
		/// <seealso cref="SetParameterFloat" />
		/// <seealso cref="SetParameterInt" />
		/// <seealso cref="GetValueString" />
		public int GetParameterInt(int index)
		{
			NativeInvoke(FMOD_DSP_GetParameterInt(this, index, out var value, IntPtr.Zero, 0));
			return value;
		}

		/// <summary>
		///     Sets a DSP unit's boolean parameter by index.
		/// </summary>
		/// <param name="index">Parameter index for this unit. Find the number of parameters with <see cref="ParameterCount" />.</param>
		/// <param name="value">
		///     <para>Boolean parameter value to be passed to the DSP unit.</para>
		///     <para><c>true</c> or <c>false</c>.</para>
		/// </param>
		/// <alert class="note">
		///     This function will be largely unused in <b>FMOD.NET</b>, as all built-in DSP types are
		///     implemented individually with named classes and methods, but it is here for custom DSPs and the sake of
		///     completeness.
		/// </alert>
		/// <seealso cref="ParameterCount" />
		/// <seealso cref="GetParameterBool" />
		/// <seealso cref="GetParameterData" />
		/// <seealso cref="GetParameterFloat" />
		/// <seealso cref="GetParameterInt" />
		/// <seealso cref="SetParameterData" />
		/// <seealso cref="SetParameterFloat" />
		/// <seealso cref="SetParameterInt" />
		public void SetParameterBool(int index, bool value)
		{
			NativeInvoke(FMOD_DSP_SetParameterBool(this, index, value));
		}

		/// <summary>
		///     Sets a DSP unit's data parameter by index.
		/// </summary>
		/// <param name="index">Parameter index for this unit. Find the number of parameters with <see cref="ParameterCount" />.</param>
		/// <param name="data">
		///     <para>The data.</para>
		///     <para>This will be raw binary data to be passed to the DSP unit.</para>
		/// </param>
		/// <alert class="note">
		///     This function will be largely unused in <b>FMOD.NET</b>, as all built-in DSP types are
		///     implemented individually with named classes and methods, but it is here for custom DSPs and the sake of
		///     completeness.
		/// </alert>
		/// <seealso cref="ParameterCount" />
		/// <seealso cref="GetParameterBool" />
		/// <seealso cref="GetParameterData" />
		/// <seealso cref="GetParameterFloat" />
		/// <seealso cref="GetParameterInt" />
		/// <seealso cref="SetParameterBool" />
		/// <seealso cref="SetParameterFloat" />
		/// <seealso cref="SetParameterInt" />
		public void SetParameterData(int index, byte[] data)
		{
			var gcHandle = GCHandle.Alloc(data, GCHandleType.Pinned);
			NativeInvoke(FMOD_DSP_SetParameterData(this, index, gcHandle.AddrOfPinnedObject(), (uint) data.Length));
			gcHandle.Free();
		}

		/// <summary>
		///     Sets a DSP unit's float parameter by index.
		/// </summary>
		/// <param name="index">Parameter index for this unit. Find the number of parameters with <see cref="ParameterCount" />.</param>
		/// <param name="value">Floating point parameter value to be passed to the DSP unit. </param>
		/// <alert class="note">
		///     This function will be largely unused in <b>FMOD.NET</b>, as all built-in DSP types are
		///     implemented individually with named classes and methods, but it is here for custom DSPs and the sake of
		///     completeness.
		/// </alert>
		/// <seealso cref="ParameterCount" />
		/// <seealso cref="GetParameterBool" />
		/// <seealso cref="GetParameterData" />
		/// <seealso cref="GetParameterFloat" />
		/// <seealso cref="GetParameterInt" />
		/// <seealso cref="SetParameterBool" />
		/// <seealso cref="SetParameterData" />
		/// <seealso cref="SetParameterInt" />
		public void SetParameterFloat(int index, float value)
		{
			NativeInvoke(FMOD_DSP_SetParameterFloat(this, index, value));
		}

		/// <summary>
		///     Sets a DSP unit's integer parameter by index.
		/// </summary>
		/// <param name="index">Parameter index for this unit. Find the number of parameters with <see cref="ParameterCount" />.</param>
		/// <param name="value">Integer parameter value to be passed to the DSP unit. </param>
		/// <alert class="note">
		///     This function will be largely unused in <b>FMOD.NET</b>, as all built-in DSP types are
		///     implemented individually with named classes and methods, but it is here for custom DSPs and the sake of
		///     completeness.
		/// </alert>
		/// <seealso cref="ParameterCount" />
		/// <seealso cref="GetParameterBool" />
		/// <seealso cref="GetParameterData" />
		/// <seealso cref="GetParameterFloat" />
		/// <seealso cref="GetParameterInt" />
		/// <seealso cref="SetParameterBool" />
		/// <seealso cref="SetParameterData" />
		/// <seealso cref="SetParameterFloat" />
		public void SetParameterInt(int index, int value)
		{
			NativeInvoke(FMOD_DSP_SetParameterInt(this, index, value));
		}

		#endregion
	}
}