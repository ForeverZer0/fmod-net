using System;
using FMOD.NET.Enumerations;

namespace FMOD.NET.Arguments
{
	public class DspParameterEventArgs : EventArgs
	{
		public int Index { get; }

		public DspParameterType ValueType { get; }

		public object Value { get;  }

		public DspParameterEventArgs(int index, DspParameterType type, object value)
		{
			Index = index;
			ValueType = type;
			Value = value;
		}

	}
}
