using FMOD.DSP;

namespace FMOD.Arguments
{
	public class DspMultiBandEqFloatChangedEventArgs : DspFloatParamChangedEventArgs
	{
		public MultiBandEq.Band Band { get; }

		public DspMultiBandEqFloatChangedEventArgs(int parameterIndex, MultiBandEq.Band band, float value, float max, float min) :
			base(parameterIndex, value, min, max)
		{
			Band = band;
		}
	}
}