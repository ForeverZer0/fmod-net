using System;
using FMOD.NET.Enumerations;

namespace FMOD.NET.Data
{
	public class PluginInfo
	{
		public uint Handle { get; set; }

		public PluginType Type { get; set; }

		public string Name { get; set; }

		public Version Version { get; set; }
	}
}
