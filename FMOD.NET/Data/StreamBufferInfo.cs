using FMOD.NET.Enumerations;

namespace FMOD.NET.Data
{
	public class StreamBufferInfo
	{
		public uint Size { get; set; } = 16384u;

		public TimeUnit SizeType { get; set; } = TimeUnit.RawBytes;
	}
}
