using System;
using System.Runtime.InteropServices;
using System.Text;

namespace FMOD.Structures
{


	#region wrapperinternal
	[StructLayout(LayoutKind.Sequential)]
	public struct StringWrapper
	{
		IntPtr nativeUtf8Ptr;

		public static implicit operator string(StringWrapper fstring)
		{
			if (fstring.nativeUtf8Ptr == IntPtr.Zero)
			{
				return "";
			}

			int strlen = 0;
			while (Marshal.ReadByte(fstring.nativeUtf8Ptr, strlen) != 0)
			{
				strlen++;
			}
			if (strlen > 0)
			{
				byte[] bytes = new byte[strlen];
				Marshal.Copy(fstring.nativeUtf8Ptr, bytes, 0, strlen);
				return Encoding.UTF8.GetString(bytes, 0, strlen);
			}
			else
			{
				return "";
			}
		}
	}
	#endregion
}
