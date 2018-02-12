using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FMOD.Sharp.Structs;

namespace FMOD.Sharp.Data
{
	public class Polygon
	{
		public PolygonAttributes Attributes { get; set; }

		public Vector[] Vertices { get; set; }
	}
}
