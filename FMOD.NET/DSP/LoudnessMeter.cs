using System;
using FMOD.NET.Core;

namespace FMOD.NET.DSP
{
	public class LoudnessMeter : Dsp
	{
		// TODO: IMPLEMENT



		internal LoudnessMeter(IntPtr handle) : base(handle)
		{
		}

		public int State
		{
			get => GetParameterInt(0);
			set { SetParameterInt(0, value.Clamp(-3, 1)); }
		}

		public byte[] Weights
		{
			get => GetParameterData(1);
			set { SetParameterData(1, value); }
		}

		public byte[] Loudness
		{
			get => GetParameterData(2);
			set { SetParameterData(2, value); }
		}
	}
}
