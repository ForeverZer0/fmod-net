using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FMOD.Sharp.Enums;

namespace FMOD.Sharp.Data
{
	public class PluginInfo
	{
		public uint Handle { get; set; }

		public PluginType Type { get; set; }

		public string Name { get; set; }

		public Version Version { get; set; }
	}
}
