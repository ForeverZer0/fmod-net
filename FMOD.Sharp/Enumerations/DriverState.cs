using System;

namespace FMOD.Enumerations
{
	[Flags]
	public enum DriverState : uint
	{
		Connected = 0x00000001, 
		Default   = 0x00000002, 
	}
}
