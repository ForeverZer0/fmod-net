using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace FMOD.Sharp.Structs
{
	[StructLayout(LayoutKind.Sequential, Pack = 2)]
	public struct ImpulseResponse
	{
		public short ChannelCount;

		public short[] PcmData;
	}
}
