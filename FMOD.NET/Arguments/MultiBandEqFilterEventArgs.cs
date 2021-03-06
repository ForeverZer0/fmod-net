﻿using FMOD.NET.DSP;

namespace FMOD.NET.Arguments
{
	public class MultiBandEqFilterEventArgs : DspParamEventArgs
	{
		public MultiBandEq.Band Band { get; }

		public MultiBandEq.Filter Filter { get; }

		public MultiBandEqFilterEventArgs(int parameterIndex, MultiBandEq.Band band, MultiBandEq.Filter filter) :
			base(parameterIndex)
		{
			Band = band;
			Filter = filter;
		}
	}
}