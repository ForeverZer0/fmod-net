namespace FMOD.Arguments
{
	public class DspDelayChangedEventArgs : DspFloatParamChangedEventArgs
	{
		public int Channel { get; }

		public DspDelayChangedEventArgs(int parameterIndex, float value) :
			base(parameterIndex, value, 0.0f, 10000.0f)
		{
			Channel = parameterIndex;
		}
	}
}

