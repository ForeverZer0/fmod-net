using FMOD.NET.Enumerations;

namespace FMOD.NET.Data
{
	public class OpenStateInfo
	{
		public OpenState State { get; set; }

		public uint PercentBuffered { get; set; }

		public bool Starving { get; set; }

		public bool DiskBusy { get; set; }
	}
}
