using System.Runtime.InteropServices;

namespace FMOD.NET.Structures
{
	[StructLayout(LayoutKind.Sequential, Pack = 2)]
	public struct ImpulseResponse
	{
		public short ChannelCount;

		public short[] PcmData;
	}
}
