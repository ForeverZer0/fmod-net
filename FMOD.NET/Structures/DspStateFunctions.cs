using System.Runtime.InteropServices;

namespace FMOD.NET.Structures
{
	[StructLayout(LayoutKind.Sequential)]
	public struct DspStateFunctions
	{
//		DSP_ALLOC_FUNC                  alloc;                  /* [r] Memory allocation callback. Use this for all dynamic memory allocation within the plugin. */
//		DSP_REALLOC_FUNC                realloc;                /* [r] Memory reallocation callback. */
//		DSP_FREE_FUNC                   free;                   /* [r] Memory free callback. */
//		DSP_GETSAMPLERATE_FUNC          getsamplerate;          /* [r] Callback for getting the system samplerate. */
//		DSP_GETBLOCKSIZE_FUNC           getblocksize;           /* [r] Callback for getting the system's block size.  DSPs will be requested to process blocks of varying length up to this size.*/
//		IntPtr                          dft;                    /* [r] Struct containing callbacks for performing FFTs and inverse FFTs. */
//		IntPtr                          pan;                    /* [r] Pointer to a structure of callbacks for calculating pan, up-mix and down-mix matrices. */
//		DSP_GETSPEAKERMODE_FUNC         getspeakermode;         /* [r] Callback for getting the system's speaker modes.  One is the mixer's default speaker mode, the other is the output mode the system is downmixing or upmixing to.*/
//		DSP_GETCLOCK_FUNC               getclock;               /* [r] Callback for getting the clock of the current DSP, as well as the subset of the input buffer that contains the signal */
//		DSP_GETLISTENERATTRIBUTES_FUNC  getlistenerattributes;  /* [r] Callback for getting the absolute listener attributes set via the API (returned as left-handed co-ordinates). */
//		DSP_LOG_FUNC                    log;                    /* [r] Function to write to the FMOD logging system. */
//		DSP_GETUSERDATA_FUNC            getuserdata;            /* [r] Function to get the user data attached to this DSP. See FMOD_DSP_DESCRIPTION::userdata. */
	}


	[StructLayout(LayoutKind.Sequential)]
	public struct DspStateDftFunction
	{
//		public DSP_DFT_FFTREAL_FUNC  fftreal;        /* [r] Function for performing an FFT on a real signal. */
//		public DSP_DFT_IFFTREAL_FUNC inversefftreal; /* [r] Function for performing an inverse FFT to get a real signal. */
	}

	/*
	[STRUCTURE] 
	[
	    [DESCRIPTION]
	    Struct containing panning helper functions for spatialization plugins.

	    [REMARKS]
	    These are experimental, please contact support@fmod.org for more information.

	    Members marked with [r] mean read only for the developer, read/write for the FMOD system.

	    Members marked with [w] mean read/write for the developer, read only for the FMOD system.

	    [SEE_ALSO]
	    FMOD_DSP_STATE_FUNCTIONS
	    FMOD_DSP_PAN_SURROUND_FLAGS
	]
	*/
	[StructLayout(LayoutKind.Sequential)]
	public struct DspStatePanFunction
	{
//		public DSP_PAN_SUMMONOMATRIX_FUNC             summonomatrix;             /* [r] TBD. */
//		public DSP_PAN_SUMSTEREOMATRIX_FUNC           sumstereomatrix;           /* [r] TBD. */
//		public DSP_PAN_SUMSURROUNDMATRIX_FUNC         sumsurroundmatrix;         /* [r] TBD. */
//		public DSP_PAN_SUMMONOTOSURROUNDMATRIX_FUNC   summonotosurroundmatrix;   /* [r] TBD. */
//		public DSP_PAN_SUMSTEREOTOSURROUNDMATRIX_FUNC sumstereotosurroundmatrix; /* [r] TBD. */
//		public DSP_PAN_GETROLLOFFGAIN_FUNC            getrolloffgain;            /* [r] TBD. */
	}



}
