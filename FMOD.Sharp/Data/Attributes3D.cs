using System.Runtime.InteropServices;
using FMOD.Structures;

namespace FMOD.Data
{
	[StructLayout(LayoutKind.Sequential)]
    public class Attributes3D
    {
        public Vector Position;
	    public Vector Velocity;
	    public Vector Forward;
	    public Vector Up;
    }
}
