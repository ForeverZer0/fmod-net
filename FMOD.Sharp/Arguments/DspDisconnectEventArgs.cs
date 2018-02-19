using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FMOD.Core;

namespace FMOD.Arguments
{

	public class DspDisconnectEventArgs : EventArgs
	{
		public Dsp DspInstance { get; }

		public DspConnection Connection { get; }

		public DspDisconnectEventArgs(Dsp dsp, DspConnection connection)
		{
			DspInstance = dsp;
			Connection = connection;
		}
	}
}
