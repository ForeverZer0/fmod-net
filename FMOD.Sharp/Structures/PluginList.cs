using System;
using System.Runtime.InteropServices;
using FMOD.Enumerations;

namespace FMOD.Structures
{
	[StructLayout(LayoutKind.Sequential)]
    public struct PluginList
    {
        public PluginType Type;
        public IntPtr Description;
    }
}
