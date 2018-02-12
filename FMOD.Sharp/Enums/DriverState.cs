using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FMOD.Sharp.Enums
{
	[Flags]
	public enum DriverState : uint
	{
		Connected = 0x00000001, 
		Default   = 0x00000002, 
	}
}
