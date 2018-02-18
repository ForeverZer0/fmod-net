using FMOD.DSP;

namespace FMOD.Arguments
{
	public class DspMultiBandEqFilterChangedEventArgs : DspParamChangedEventArgs
	{
		public MultiBandEq.Band Band { get; }

		public MultiBandEq.Filter Filter { get; }

		public DspMultiBandEqFilterChangedEventArgs(int parameterIndex, MultiBandEq.Band band, MultiBandEq.Filter filter) :
			base(parameterIndex)
		{
			Band = band;
			Filter = filter;
		}
	}
}