using System;

namespace FMOD.Arguments
{
	public class DspParamChangedEventArgs : EventArgs
	{
		public int ParameterIndex { get; }

		public DspParamChangedEventArgs(int parameterIndex)
		{
			ParameterIndex = parameterIndex;
		}
	}
}