namespace FMOD.Arguments
{
	public class DspBoolParamChangedEventArgs : DspParamChangedEventArgs
	{
		public bool Value { get; }

		public DspBoolParamChangedEventArgs(int index, bool value) : base(index)
		{
			Value = value;
		}
	}
}