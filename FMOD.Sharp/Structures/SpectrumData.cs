using System;
using System.Runtime.InteropServices;

namespace FMOD.Structures
{
	[StructLayout(LayoutKind.Sequential)]
	public class SpectrumData
	{
		public readonly int Length;

		public readonly int ChannelCount;

		[MarshalAs(UnmanagedType.ByValArray, SizeConst=32)]
		public readonly IntPtr[] Spectrum;
	}
}
