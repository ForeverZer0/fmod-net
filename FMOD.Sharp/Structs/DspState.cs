using System;
using System.Runtime.InteropServices;

namespace FMOD.Sharp.Structs
{
	[StructLayout(LayoutKind.Sequential)]
	public struct DspState
	{
		public IntPtr     instance;            /* [r] Handle to the DSP hand the user created.  Not to be modified.  C++ users cast to FMOD::DSP to use.  */
		public IntPtr     plugindata;          /* [r/w] Plugin writer created data the output author wants to attach to this object. */
		public uint       channelmask;         /* [r] Specifies which speakers the DSP effect is active on */
		public int        source_speakermode;  /* [r] Specifies which speaker mode the signal originated for information purposes, ie in case panning needs to be done differently. */
		public IntPtr     sidechaindata;       /* [r] The mixed result of all incoming sidechains is stored at this pointer address. */
		public int        sidechainchannels;   /* [r] The number of channels of pcm data stored within the sidechain buffer. */
		public IntPtr     functions;           /* [r] Struct containing callbacks for system level functionality. */
		public int        systemobject;        /* [r] FMOD::System object index, relating to the System object that created this DSP. */
	}
}