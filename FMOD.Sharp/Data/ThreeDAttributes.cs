using System.Runtime.InteropServices;
using FMOD.Structures;

namespace FMOD.Data
{
	[StructLayout(LayoutKind.Sequential)]
    public class ThreeDAttributes
    {
        public Vector Position;
	    public Vector Velocity;
	    public Vector Forward;
	    public Vector Up;
    }
}
