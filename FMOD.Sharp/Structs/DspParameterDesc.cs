using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using FMOD.Sharp.Enums;

namespace FMOD.Sharp.Structs
{
	[StructLayout(LayoutKind.Sequential)]
	public struct DspParameterDesc
	{
		public DspParameterType   Type;           
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
		public char[]               Name;           
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
		public char[]               Label;           
		public string               Description;    

		public DspParameterDescUnion desc;
	}
}
