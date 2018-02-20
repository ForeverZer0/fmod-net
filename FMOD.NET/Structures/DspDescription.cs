using System;
using System.Runtime.InteropServices;
using FMOD.Core;

namespace FMOD.Structures
{
    [StructLayout(LayoutKind.Sequential)]
    public struct DspDescription
    {
        public uint                           pluginsdkversion;   /* [w] The plugin SDK version this plugin is built for.  set to this to FMOD_PLUGIN_SDK_VERSION defined above. */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
        public char[]                         name;               /* [w] Name of the unit to be displayed in the network. */
        public uint                           version;            /* [w] Plugin writer's version number. */
        public int                            numinputbuffers;    /* [w] Number of input buffers to process.  Use 0 for DSPs that only generate sound and 1 for effects that process incoming sound. */
        public int                            numoutputbuffers;   /* [w] Number of audio output buffers.  Only one output buffer is currently supported. */
        public DspCreateCallback             create;             /* [w] Create callback.  This is called when DSP unit is created.  Can be null. */
        public DspReleaseCallback            release;            /* [w] Release callback.  This is called just before the unit is freed so the user can do any cleanup needed for the unit.  Can be null. */
        public DspResetCallback              reset;              /* [w] Reset callback.  This is called by the user to reset any history buffers that may need resetting for a filter, when it is to be used or re-used for the first time to its initial clean state.  Use to avoid clicks or artifacts. */
        public DspReadCallback               read;               /* [w] Read callback.  Processing is done here.  Can be null. */
        public DspProcessCallback           process;            /* [w] Process callback.  Can be specified instead of the read callback if any channel format changes occur between input and output.  This also replaces shouldiprocess and should return an error if the effect is to be bypassed.  Can be null. */
        public DspSetPositionCallback        setposition;        /* [w] Setposition callback.  This is called if the unit wants to update its position info but not process data.  Can be null. */

        public int                            numparameters;      /* [w] Number of parameters used in this filter.  The user finds this with DSP::getNumParameters */
        public IntPtr                         paramdesc;          /* [w] Variable number of parameter structures. */
        public DspSetParamFloatCallback    setparameterfloat;  /* [w] This is called when the user calls DSP.setParameterFloat. Can be null. */
        public DspSetParamIntCallback      setparameterint;    /* [w] This is called when the user calls DSP.setParameterInt.   Can be null. */
        public DspSetParamBoolCallback     setparameterbool;   /* [w] This is called when the user calls DSP.setParameterBool.  Can be null. */
        public DspSetParamDataCallback     setparameterdata;   /* [w] This is called when the user calls DSP.setParameterData.  Can be null. */
        public DspGetParamFloatCallback    getparameterfloat;  /* [w] This is called when the user calls DSP.getParameterFloat. Can be null. */
        public DspGetParamIntCallback      getparameterint;    /* [w] This is called when the user calls DSP.getParameterInt.   Can be null. */
        public DspGetParamBoolCallback     getparameterbool;   /* [w] This is called when the user calls DSP.getParameterBool.  Can be null. */
        public DspGetParamDataCallback     getparameterdata;   /* [w] This is called when the user calls DSP.getParameterData.  Can be null. */
        public DspShouldIProcessCallback    SHOULD_I_PROCESS;     /* [w] This is called before processing.  You can detect if inputs are idle and return FMOD_OK to process, or any other error code to avoid processing the effect.  Use a count down timer to allow effect tails to process before idling! */
        public IntPtr                         userdata;           /* [w] Optional. Specify 0 to ignore. This is user data to be attached to the DSP unit during creation.  Access via FMOD_DSP_STATE_FUNCTIONS::getuserdata. */

        public DspSystemRegisterCallback   sys_register;       /* [w] Register callback.  This is called when DSP unit is loaded/registered.  Useful for 'global'/per system object init for plugin.  Can be null. */
        public DspSystemDeregisterCallback sys_deregister;     /* [w] Deregister callback.  This is called when DSP unit is unloaded/deregistered.  Useful as 'global'/per system object shutdown for plugin.  Can be null. */
        public DspSystemMixCallback        sys_mix;            /* [w] System mix stage callback.  This is called when the mixer starts to execute or is just finishing executing.  Useful for 'global'/per system object once a mix update calls for a plugin.  Can be null. */
    }
}
