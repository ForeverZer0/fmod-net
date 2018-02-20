namespace FMOD.Arguments
{
	public class DspChannelMixGainChangedEventArgs : FloatParamEventArgs
	{
		public int ChannelIndex { get; }

		public DspChannelMixGainChangedEventArgs(int parameterIndex, int channelIndex, float gain) : base(parameterIndex, gain, -80.0f, 10.0f)
		{
			ChannelIndex = channelIndex;
		}
	}
}