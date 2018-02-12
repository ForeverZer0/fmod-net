using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FMOD.Sharp.Enums
{
	[Flags]
	public enum DebugFlags : uint
	{
		None                    = 0x00000000,  
		Error                   = 0x00000001,  
		Warning                 = 0x00000002,  
		Log                     = 0x00000004,  
		TypeMemory             = 0x00000100,  
		TypeFile               = 0x00000200,  
		TypeCodec              = 0x00000400,  
		TypeTrace              = 0x00000800,  
		DisplayTimestamps      = 0x00010000,  
		DisplayLineNumbers     = 0x00020000,  
		DisplayThread          = 0x00040000,  
	}


}
