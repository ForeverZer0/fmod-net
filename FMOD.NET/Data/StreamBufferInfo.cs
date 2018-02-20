using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FMOD.Enumerations;

namespace FMOD.Data
{
	public class StreamBufferInfo
	{
		public uint Size { get; set; } = 16384u;

		public TimeUnit SizeType { get; set; } = TimeUnit.RawBytes;
	}
}
