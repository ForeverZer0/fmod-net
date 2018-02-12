using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace FMOD.Sharp.Structs
{
	[StructLayout(LayoutKind.Sequential)]
	public class DspMeteringInfo
	{
		public int SampleCount;       
		[MarshalAs(UnmanagedType.ByValArray, SizeConst=32)]
		public float[] PeakLevel;      
		[MarshalAs(UnmanagedType.ByValArray, SizeConst=32)]
		public float[] RmsLevel;       
		public short ChannelCount;       
	}
}
