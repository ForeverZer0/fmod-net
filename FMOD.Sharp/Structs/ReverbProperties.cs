using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace FMOD.Sharp.Structs
{
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	public struct ReverbProperties
	{
		public float DecayTime;
		public float EarlyDelay;
		public float LateDelay;
		public float HFReference;
		public float HFDecayRatio;
		public float Diffusion;
		public float Density;
		public float LowShelfFrequency;
		public float LowShelfGain;
		public float HighCut;
		public float EarlyLateMix;
		public float WetLevel;
	}
}
