using System;
using System.Runtime.InteropServices;

namespace FMOD.Structures
{
	[StructLayout(LayoutKind.Sequential)]
    public struct FloatMappingPiecewiseLinear
    {
        public int NumberOfPoints;               
        public IntPtr PointParamValues;          
        public IntPtr PointPositions;            
    }
}
