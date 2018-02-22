#region License

// Dsp.Native.cs is distributed under the Microsoft Public License (MS-PL)
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
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using FMOD.NET.Enumerations;
using FMOD.NET.Structures;

#endregion

namespace FMOD.NET.Core
{
	[SuppressUnmanagedCodeSecurity]
	[SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
	public partial class Dsp
	{
		#region Native Methods

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_DSP_AddInput(IntPtr dsp, IntPtr target, out IntPtr connection,
			DspConnectionType type);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_DSP_DisconnectAll(IntPtr dsp, bool inputs, bool outputs);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_DSP_DisconnectFrom(IntPtr dsp, IntPtr target, IntPtr connection);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_DSP_GetActive(IntPtr dsp, out bool active);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_DSP_GetBypass(IntPtr dsp, out bool bypass);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_DSP_GetChannelFormat(IntPtr dsp, out ChannelMask channelMask, out int numchannels,
			out SpeakerMode sourceSpeakerMode);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_DSP_GetDataParameterIndex(IntPtr dsp, int datatype, out int index);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_DSP_GetIdle(IntPtr dsp, out bool idle);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_DSP_GetInfo(IntPtr dsp, IntPtr name, out uint version, out int channels,
			out int configwidth, out int configheight);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_DSP_GetInput(IntPtr dsp, int index, out IntPtr input, out IntPtr inputconnection);

		[DllImport(Constants.LIBRARY)]
		public static extern Result FMOD_DSP_GetMeteringEnabled(IntPtr dsp, out bool inputEnabled, out bool outputEnabled);

		[DllImport(Constants.LIBRARY)]
		public static extern Result FMOD_DSP_GetMeteringInfo(IntPtr dsp, out DspMeteringInfo inputInfo,
			[Out] DspMeteringInfo outputInfo);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_DSP_GetNumInputs(IntPtr dsp, out int numinputs);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_DSP_GetNumOutputs(IntPtr dsp, out int numoutputs);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_DSP_GetNumParameters(IntPtr dsp, out int numparams);

		[DllImport(Constants.LIBRARY)]
		private static extern Result
			FMOD_DSP_GetOutput(IntPtr dsp, int index, out IntPtr output, out IntPtr outputconnection);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_DSP_GetOutputChannelFormat(IntPtr dsp, ChannelMask inmask, int inchannels,
			SpeakerMode inSpeakerMode, out ChannelMask outmask, out int outchannels, out SpeakerMode outSpeakerMode);

		[DllImport(Constants.LIBRARY)]
		protected static extern Result FMOD_DSP_GetParameterBool(IntPtr dsp, int index, out bool value, IntPtr valuestr,
			int valuestrlen);

		[DllImport(Constants.LIBRARY)]
		protected static extern Result FMOD_DSP_GetParameterData(IntPtr dsp, int index, out IntPtr data, out uint length,
			IntPtr valuestr, int valuestrlen);

		[DllImport(Constants.LIBRARY)]
		protected static extern Result FMOD_DSP_GetParameterFloat(IntPtr dsp, int index, out float value, IntPtr valuestr,
			int valuestrlen);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_DSP_GetParameterInfo(IntPtr dsp, int index, out IntPtr desc);

		[DllImport(Constants.LIBRARY)]
		protected static extern Result FMOD_DSP_GetParameterInt(IntPtr dsp, int index, out int value, IntPtr valuestr,
			int valuestrlen);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_DSP_GetSystemObject(IntPtr dsp, out IntPtr system);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_DSP_GetType(IntPtr dsp, out DspType type);


		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_DSP_GetWetDryMix(IntPtr dsp, out float prewet, out float postwet, out float dry);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_DSP_Reset(IntPtr dsp);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_DSP_SetActive(IntPtr dsp, bool active);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_DSP_SetBypass(IntPtr dsp, bool bypass);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_DSP_SetChannelFormat(IntPtr dsp, ChannelMask channelMask, int numchannels,
			SpeakerMode sourceSpeakerMode);

		[DllImport(Constants.LIBRARY)]
		public static extern Result FMOD_DSP_SetMeteringEnabled(IntPtr dsp, bool inputEnabled, bool outputEnabled);

		[DllImport(Constants.LIBRARY)]
		protected static extern Result FMOD_DSP_SetParameterBool(IntPtr dsp, int index, bool value);

		[DllImport(Constants.LIBRARY)]
		protected static extern Result FMOD_DSP_SetParameterData(IntPtr dsp, int index, IntPtr data, uint length);

		[DllImport(Constants.LIBRARY)]
		protected static extern Result FMOD_DSP_SetParameterFloat(IntPtr dsp, int index, float value);

		[DllImport(Constants.LIBRARY)]
		protected static extern Result FMOD_DSP_SetParameterInt(IntPtr dsp, int index, int value);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_DSP_SetWetDryMix(IntPtr dsp, float prewet, float postwet, float dry);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_DSP_ShowConfigDialog(IntPtr dsp, IntPtr hwnd, bool show);

		#endregion
	}
}