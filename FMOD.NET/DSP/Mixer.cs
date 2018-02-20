using System;
using FMOD.Core;

namespace FMOD.DSP
{
	/// <summary>
	/// This unit does nothing but take inputs and mix them together then feed the result to the soundcard unit. 
	/// </summary>
	/// <seealso cref="Dsp" />
	public class Mixer : Dsp
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Mixer"/> class.
		/// </summary>
		/// <param name="handle">The handle.</param>
		internal Mixer(IntPtr handle) : base(handle)
		{
		}
	}
}
