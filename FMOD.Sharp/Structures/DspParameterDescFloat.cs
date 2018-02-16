using System.Runtime.InteropServices;

namespace FMOD.Structures
{
	[StructLayout(LayoutKind.Sequential)]
    public struct DspParameterDescFloat
    {
        public float                     Minimum;                  
        public float                     Maximum;                  
        public float                     DefaultValue;           	 
        public FloatMapping Mapping;           
    }
}
