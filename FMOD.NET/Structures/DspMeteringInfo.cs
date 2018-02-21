using System.Runtime.InteropServices;

namespace FMOD.NET.Structures
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
