using System;
using System.Runtime.InteropServices;

namespace FMOD.Structures
{
	[StructLayout(LayoutKind.Sequential)]
	public struct DspParameterFft
	{
		public int     length;                                    /* [r] Number of entries in this spectrum window.   Divide this by the output rate to get the hz per entry. */
		public int     numchannels;                               /* [r] Number of channels in spectrum. */
        
		[MarshalAs(UnmanagedType.ByValArray,SizeConst=32)]
		private IntPtr[] spectrum_internal;                           /* [r] Per channel spectrum arrays.  See remarks for more. */
        
		public float[][] spectrum
		{
			get
			{
				var buffer = new float[numchannels][];
                
				for (int i = 0; i < numchannels; ++i)
				{
					buffer[i] = new float[length];
					Marshal.Copy(spectrum_internal[i], buffer[i], 0, length);
				}
                
				return buffer;
			}
		}
	}
}
