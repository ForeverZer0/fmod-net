using FMOD.NET.Enumerations;

namespace FMOD.NET.Data
{
	public class SyncPointInfo
	{
		public string Name { get; set; }

		public uint Offset { get; set; }

		public TimeUnit OffsetTimeUnit { get; set; }

		public SyncPointInfo()
		{
			
		}

		public SyncPointInfo(uint offset, TimeUnit timeUnit, string name)
		{
			Offset = offset;
			OffsetTimeUnit = timeUnit;
			Name = name;
		}
	}
}
