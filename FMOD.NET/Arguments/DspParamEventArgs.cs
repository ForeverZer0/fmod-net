using System;

namespace FMOD.NET.Arguments
{
	public class DspParamEventArgs : EventArgs
	{
		public int ParameterIndex { get; }

		public DspParamEventArgs(int parameterIndex)
		{
			ParameterIndex = parameterIndex;
		}
	}
}