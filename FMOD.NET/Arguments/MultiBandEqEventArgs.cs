using FMOD.NET.DSP;

namespace FMOD.NET.Arguments
{
	public class MultiBandEqEventArgs : FloatParamEventArgs
	{
		public MultiBandEq.Band Band { get; }

		public MultiBandEqEventArgs(int parameterIndex, MultiBandEq.Band band, float value, float max, float min) :
			base(parameterIndex, value, min, max)
		{
			Band = band;
		}
	}
}