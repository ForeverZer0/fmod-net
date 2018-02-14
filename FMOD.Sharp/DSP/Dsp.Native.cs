using System;
using System.Runtime.InteropServices;
using System.Security;
using FMOD.Sharp.Enums;
using FMOD.Sharp.Structs;

namespace FMOD.Sharp.DSP
{
	[SuppressUnmanagedCodeSecurity]
	public partial class DspBase
	{
		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_DSP_GetNumInputs(IntPtr dsp, out int numinputs);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_DSP_GetNumOutputs(IntPtr dsp, out int numoutputs);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_DSP_GetSystemObject(IntPtr dsp, out IntPtr system);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_DSP_SetActive(IntPtr dsp, bool active);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_DSP_GetActive(IntPtr dsp, out bool active);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_DSP_Reset(IntPtr dsp);

		[DllImport(Core.LIBRARY)]
		protected static extern Result FMOD_DSP_SetParameterFloat(IntPtr dsp, int index, float value);

		[DllImport(Core.LIBRARY)]
		protected static extern Result FMOD_DSP_SetParameterInt(IntPtr dsp, int index, int value);

		[DllImport(Core.LIBRARY)]
		protected static extern Result FMOD_DSP_SetParameterBool(IntPtr dsp, int index, bool value);

		[DllImport(Core.LIBRARY)]
		protected static extern Result FMOD_DSP_SetParameterData(IntPtr dsp, int index, IntPtr data, uint length);

		[DllImport(Core.LIBRARY)]
		protected static extern Result FMOD_DSP_GetParameterFloat(IntPtr dsp, int index, out float value, IntPtr valuestr,
			int valuestrlen);

		[DllImport(Core.LIBRARY)]
		protected static extern Result FMOD_DSP_GetParameterInt(IntPtr dsp, int index, out int value, IntPtr valuestr,
			int valuestrlen);

		[DllImport(Core.LIBRARY)]
		protected static extern Result FMOD_DSP_GetParameterBool(IntPtr dsp, int index, out bool value, IntPtr valuestr,
			int valuestrlen);

		[DllImport(Core.LIBRARY)]
		protected static extern Result FMOD_DSP_GetParameterData(IntPtr dsp, int index, out IntPtr data, out uint length,
			IntPtr valuestr, int valuestrlen);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_DSP_GetNumParameters(IntPtr dsp, out int numparams);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_DSP_GetParameterInfo(IntPtr dsp, int index, out IntPtr desc);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_DSP_GetDataParameterIndex(IntPtr dsp, int datatype, out int index);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_DSP_SetUserData(IntPtr dsp, IntPtr userdata);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_DSP_GetUserData(IntPtr dsp, out IntPtr userdata);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_DSP_GetType(IntPtr dsp, out DspType type);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_DSP_GetIdle(IntPtr dsp, out bool idle);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_DSP_DisconnectAll(IntPtr dsp, bool inputs, bool outputs);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_DSP_SetBypass(IntPtr dsp, bool bypass);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_DSP_GetBypass(IntPtr dsp, out bool bypass);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_DSP_SetWetDryMix(IntPtr dsp, float prewet, float postwet, float dry);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_DSP_GetWetDryMix(IntPtr dsp, out float prewet, out float postwet, out float dry);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_DSP_ShowConfigDialog(IntPtr dsp, IntPtr hwnd, bool show);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_DSP_SetChannelFormat(IntPtr dsp, ChannelMask channelMask, int numchannels,
			SpeakerMode sourceSpeakerMode);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_DSP_GetChannelFormat(IntPtr dsp, out ChannelMask channelMask, out int numchannels,
			out SpeakerMode sourceSpeakerMode);

		[DllImport(Core.LIBRARY)]
		public static extern Result FMOD_DSP_SetMeteringEnabled(IntPtr dsp, bool inputEnabled, bool outputEnabled);

		[DllImport(Core.LIBRARY)]
		public static extern Result FMOD_DSP_GetMeteringEnabled(IntPtr dsp, out bool inputEnabled, out bool outputEnabled);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_DSP_AddInput(IntPtr dsp, IntPtr target, out IntPtr connection,
			DspConnectionType type);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_DSP_DisconnectFrom(IntPtr dsp, IntPtr target, IntPtr connection);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_DSP_GetInput(IntPtr dsp, int index, out IntPtr input, out IntPtr inputconnection);

		[DllImport(Core.LIBRARY)]
		private static extern Result
			FMOD_DSP_GetOutput(IntPtr dsp, int index, out IntPtr output, out IntPtr outputconnection);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_DSP_GetOutputChannelFormat(IntPtr dsp, ChannelMask inmask, int inchannels,
			SpeakerMode inSpeakerMode, out ChannelMask outmask, out int outchannels, out SpeakerMode outSpeakerMode);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_DSP_GetInfo(IntPtr dsp, IntPtr name, out uint version, out int channels,
			out int configwidth, out int configheight);

		[DllImport(Core.LIBRARY)]
		public static extern Result FMOD_DSP_GetMeteringInfo(IntPtr dsp, out DspMeteringInfo inputInfo,
			[Out] DspMeteringInfo outputInfo);
	}
}
