using System;

namespace FMOD.Enumerations
{
	[Flags]
	public enum Mode : uint
	{
		Default = 0x00000000,
		LoopOff = 0x00000001,
		LoopNormal = 0x00000002,
		LoopBidi = 0x00000004,
		TwoD = 0x00000008,
		ThreeD = 0x00000010,
		CreateStream = 0x00000080,
		CreateSample = 0x00000100,
		CreateCompressedSample = 0x00000200,
		OpenUser = 0x00000400,
		OpenMemory = 0x00000800,
		OpenMemoryPoint = 0x10000000,
		OpenRaw = 0x00001000,
		OpenOnly = 0x00002000,
		AccurateTime = 0x00004000,
		MpegSearch = 0x00008000,
		NonBlocking = 0x00010000,
		Unique = 0x00020000,
		ThreeDHeadRelative = 0x0004000,
		ThreeDWorldRelative = 0x00080000,
		ThreeDInverseRolloff = 0x00100000,
		ThreeDLinearRolloff = 0x00200000,
		ThreeDLinearSquareRolloff = 0x00400000,
		ThreeDInverseTaperedRolloff = 0x00800000,
		ThreeDCustomRolloff = 0x04000000,
		ThreeDIgnoregeometry = 0x40000000,
		IgnoreTags = 0x02000000,
		LowMemory = 0x08000000,
		LoadSecondaryRam = 0x20000000,
		VirtualPlayFromStart = 0x80000000
	}
}