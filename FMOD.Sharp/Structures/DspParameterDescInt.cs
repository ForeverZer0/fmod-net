using System;
using System.Runtime.InteropServices;

namespace FMOD.Structures
{
	[StructLayout(LayoutKind.Sequential)]
    public struct DspParameterDescInt
    {
        public int                       Minimum;                    
        public int                       Maximum;                    
        public int                       DefaultValue;             
        public bool                      IsInfinite;              
        public IntPtr                    ValueNames;             
    }																																																																																																											   
}
