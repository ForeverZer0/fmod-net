using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FMOD.Sharp.Data
{
	public class RamUsage
	{
		public int CurrentlyAllocated { get; set; }

		public int MaximumAllocated { get; set; }

		public int Total { get; set; }
	}
}
