using System;
using FMOD.NET.Core;
using FMOD.NET.Enumerations;

namespace FMOD.NET.Arguments
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
