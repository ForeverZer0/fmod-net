using System;

namespace FMOD.Enumerations
{
	[Flags]
	public enum SystemCallbackType : uint
	{
		DeviceListChanged      = 0x00000001, 
		DeviceLost             = 0x00000002, 
		MemoryAllocationFailed = 0x00000004, 
		ThreadCreated          = 0x00000008, 
		BadDspConnection       = 0x00000010, 
		PreMix                 = 0x00000020, 
		PostMix                = 0x00000040, 
		Error                  = 0x00000080, 
		MidMix                 = 0x00000100, 
		ThreadDestroyed        = 0x00000200, 
		PreUpdate              = 0x00000400, 
		PostUpdate             = 0x00000800, 
		RecordListChanged      = 0x00001000, 
		All                    = 0xFFFFFFFF																																																																																																																																	 
	}
}
