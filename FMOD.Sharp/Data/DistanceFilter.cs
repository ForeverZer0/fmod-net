using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FMOD.Sharp.Data
{
	public class DistanceFilter
	{
		public bool Custom { get; set; } = false;

		public float CustomLevel { get; set; } = 1.0f;

		public float CenterFrequency { get; set; } = 1500.0f;

	}
}