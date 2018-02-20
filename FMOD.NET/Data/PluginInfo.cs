using System;
using FMOD.Enumerations;

namespace FMOD.Data
{
	public class PluginInfo
	{
		public uint Handle { get; set; }

		public PluginType Type { get; set; }

		public string Name { get; set; }

		public Version Version { get; set; }
	}
}
