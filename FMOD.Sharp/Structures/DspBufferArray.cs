using System;
using System.Runtime.InteropServices;
using FMOD.Enumerations;

namespace FMOD.Structures
{
	[StructLayout(LayoutKind.Sequential)]
	public struct DspBufferArray
	{
		public int              NumBuffers;              /* [r/w] number of buffers */
		public int[]            buffernumchannels;       /* [r/w] array of number of channels for each buffer */
		public ChannelMask[]    bufferchannelmask;       /* [r/w] array of channel masks for each buffer */
		public IntPtr[]         buffers;                 /* [r/w] array of buffers */
		public SpeakerMode      speakermode;             /* [r/w] speaker mode for all buffers in the array */
	}
}
