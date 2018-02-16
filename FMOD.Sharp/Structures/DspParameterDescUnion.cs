using System.Runtime.InteropServices;

namespace FMOD.Structures
{
	[StructLayout(LayoutKind.Explicit)]
	public struct DspParameterDescUnion
	{
		[FieldOffset(0)]
		public DspParameterDescFloat   FloatDescription;  
		[FieldOffset(0)]
		public DspParameterDescInt     IntDescription;    
		[FieldOffset(0)]
		public DspParameterDescBool    BoolDescription;   
		[FieldOffset(0)]
		public DspParameterDescData    DataDescription;   																																					
	}
}
