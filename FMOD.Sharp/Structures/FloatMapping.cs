using System.Runtime.InteropServices;
using FMOD.Enumerations;

namespace FMOD.Structures
{
	[StructLayout(LayoutKind.Sequential)]
    public struct FloatMapping
    {
        public FloatMappingType Type;
        public FloatMappingPiecewiseLinear PiecewiseLinearMapping;   
    }
}
