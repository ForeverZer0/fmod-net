using System;

namespace FMOD.Arguments
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