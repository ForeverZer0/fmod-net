using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using FMOD.Sharp.Enums;
using FMOD.Sharp.Structs;

namespace FMOD.Sharp.Data
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
