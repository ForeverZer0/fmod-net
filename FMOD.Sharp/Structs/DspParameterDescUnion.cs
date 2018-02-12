using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using FMOD.Sharp.Enums;

namespace FMOD.Sharp.Structs
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
