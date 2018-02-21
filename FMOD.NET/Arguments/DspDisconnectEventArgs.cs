using System;
using FMOD.NET.Core;

namespace FMOD.NET.Arguments
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
