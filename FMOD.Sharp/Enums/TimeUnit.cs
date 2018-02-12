using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FMOD.Sharp.Enums
{
	[Flags]
	public enum TimeUnit : uint
	{
		Ms          = 0x00000001,
		Pcm         = 0x00000002,
		PcmBytes    = 0x00000004,
		RawBytes    = 0x00000008,
		PcmFraction = 0x00000010,
		ModOrder    = 0x00000100,
		ModRow      = 0x00000200,
		ModPattern  = 0x00000400,
	}
}																																																																																																												
