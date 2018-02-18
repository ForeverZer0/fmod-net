namespace FMOD.Arguments
{
	public class DspFloatParamChangedEventArgs : DspParamChangedEventArgs
	{
		public float Value { get; }

		public float MinValue { get; }

		public float MaxValue { get; }

		public DspFloatParamChangedEventArgs(int index, float value, float min = float.MinValue, float max = float.MaxValue) : base(index)
		{
			Value = value;
			MinValue = min;
			MaxValue = max;
		}
	}
}