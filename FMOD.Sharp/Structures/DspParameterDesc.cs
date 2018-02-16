using System.Runtime.InteropServices;
using FMOD.Enumerations;

namespace FMOD.Structures
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
