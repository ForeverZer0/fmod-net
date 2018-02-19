using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FMOD.Core;
using FMOD.Enumerations;

namespace FMOD.Arguments
{
	public class DspInputEventArgs : EventArgs
	{
		public DspConnectionType ConnectionType { get; }

		public DspConnection Connection { get; }

		public DspInputEventArgs(DspConnection connection, DspConnectionType type)
		{
			Connection = connection;
			ConnectionType = type;
		}
	}
}
