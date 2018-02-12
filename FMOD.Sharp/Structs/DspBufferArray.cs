using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using FMOD.Sharp.Enums;

namespace FMOD.Sharp.Structs
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
