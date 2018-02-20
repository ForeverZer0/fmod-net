namespace FMOD.Arguments
{
	public class BoolParamEventArgs : DspParamEventArgs
	{
		public bool Value { get; }

		public BoolParamEventArgs(int index, bool value) : base(index)
		{
			Value = value;
		}
	}
}