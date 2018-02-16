using System;
using System.Drawing;

namespace FMOD.Data
{
	public class DspInfo
	{
		public string Name { get; set; }

		public Version Version { get; set; }

		public int ChannelCount { get; set; }

		public Size ConfigWindowSize { get; set; }

		public override string ToString()
		{
			return $"{Name} v.{Version}";
		}
	}
}