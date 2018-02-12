using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FMOD.Sharp.Enums
{
	public enum DspProcessOperation
	{
		ProcessPerform = 0,               /* Process the incoming audio in 'inbufferarray' and output to 'outbufferarray'. */
		ProcessQuery                      /* The DSP is being queried for the expected output format and whether it needs to process audio or should be bypassed.  The function should return any value other than FMOD_OK if audio can pass through unprocessed. If audio is to be processed, 'outbufferarray' must be filled with the expected output format, channel count and mask. */
	}
}
