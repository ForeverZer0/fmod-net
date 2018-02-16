using System;
using System.Runtime.InteropServices;

namespace FMOD.Structures
{
	[StructLayout(LayoutKind.Sequential)]
    public struct DspParameterDescBool
    {
        public bool                      DefaultValues;             
        public IntPtr                    ValueNames;             																																																																																																																																		   
    }
}
