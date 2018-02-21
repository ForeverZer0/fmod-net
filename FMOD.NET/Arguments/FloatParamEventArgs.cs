namespace FMOD.NET.Arguments
{
	public class FloatParamEventArgs : DspParamEventArgs
	{
		public float Value { get; }

		public float MinValue { get; }

		public float MaxValue { get; }

		public FloatParamEventArgs(int index, float value, float min = float.MinValue, float max = float.MaxValue) : base(index)
		{
			Value = value;
			MinValue = min;
			MaxValue = max;
		}
	}
}