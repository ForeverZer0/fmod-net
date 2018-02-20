using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FMOD.Enumerations;

namespace FMOD.Arguments
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
