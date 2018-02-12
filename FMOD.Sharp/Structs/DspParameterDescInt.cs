using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using FMOD.Sharp.Enums;

namespace FMOD.Sharp.Structs
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
