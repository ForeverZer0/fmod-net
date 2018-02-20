using System;
using System.Runtime.InteropServices;
using FMOD.Enumerations;

namespace FMOD.Structures
{
	[StructLayout(LayoutKind.Sequential)]
    public struct ErrorCallbackInfo
    {
        public  Result                      Result;                     /* Error code Result */
        public  ErrorCallbackInstanceType   instancetype;               /* Type of instance the error occurred on */
        public  IntPtr                      instance;                   /* Instance pointer */
        private IntPtr                      functionname_internal;      /* Function that the error occurred on */
        private IntPtr                      functionparams_internal;    /* Function parameters that the error ocurred on */

        public string functionname   { get { return Marshal.PtrToStringAnsi(functionname_internal); } }
        public string functionparams { get { return Marshal.PtrToStringAnsi(functionparams_internal); } }
    }
}
