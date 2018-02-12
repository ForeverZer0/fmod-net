using System;
using System.Runtime.InteropServices;
using FMOD.Sharp.Enums;

namespace FMOD.Sharp
{
	public partial class DspConnection
	{
		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_DSPConnection_GetInput(IntPtr dspconnection, out IntPtr input);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_DSPConnection_GetOutput(IntPtr dspconnection, out IntPtr output);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_DSPConnection_SetMix(IntPtr dspconnection, float volume);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_DSPConnection_GetMix(IntPtr dspconnection, out float volume);

		// TODO: The mix matrix can't be right, need some testing...
		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_DSPConnection_SetMixMatrix(IntPtr dspconnection, float[] matrix, int outchannels,
			int inchannels, int inChannelHop);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_DSPConnection_GetMixMatrix(IntPtr dspconnection, float[] matrix,
			out int outchannels, out int inchannels, int inChannelHop);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_DSPConnection_GetType(IntPtr dspconnection, out DspConnectionType type);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_DSPConnection_SetUserData(IntPtr dspconnection, IntPtr userdata);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_DSPConnection_GetUserData(IntPtr dspconnection, out IntPtr userdata);
	}
}