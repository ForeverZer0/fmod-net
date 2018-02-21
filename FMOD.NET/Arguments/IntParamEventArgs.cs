namespace FMOD.NET.Arguments
{
	public class IntParamEventArgs : DspParamEventArgs
	{
		public int Value { get; }

		public int MinValue { get; }

		public int MaxValue { get; }

		public IntParamEventArgs(int index, int value, int min = int.MinValue, int max = int.MaxValue) : base(index)
		{
			Value = value;
			MinValue = min;
			MaxValue = max;
		}
	}
}