namespace FMOD.Arguments
{
	public class DspIntParamChangedEventArgs : DspParamChangedEventArgs
	{
		public int Value { get; }

		public int MinValue { get; }

		public int MaxValue { get; }

		public DspIntParamChangedEventArgs(int index, int value, int min = int.MinValue, int max = int.MaxValue) : base(index)
		{
			Value = value;
			MinValue = min;
			MaxValue = max;
		}
	}
}