using System;
using System.Runtime.InteropServices;
using FMOD.Core;

namespace FMOD.Structures
{
	[StructLayout(LayoutKind.Sequential)]
    public struct AsyncReadInfo
    {
        public IntPtr   Handle;                    
        public uint     Offset;                    
        public uint     SizeBytes;                 
        public int      Priority;                  

        public IntPtr   UserData;                  
        public IntPtr   Buffer;                    
        public uint     BytesRead;                 
        public AsyncReadInfoDoneCallback   done; 
    }																																																																
}
