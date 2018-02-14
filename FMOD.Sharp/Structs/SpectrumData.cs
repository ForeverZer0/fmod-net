using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace FMOD.Sharp.Structs
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
