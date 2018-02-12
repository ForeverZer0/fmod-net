using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FMOD.Sharp.Data
{
	public class CpuUsage
	{
		public float Dsp { get; set; }

		public float Stream { get; set; }

		public float Geometry { get; set; }

		public float Update { get; set; }

		public float Total { get; set; }
	}
}
