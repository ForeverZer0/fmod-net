using System;
using FMOD.Sharp.Enums;

namespace FMOD.Sharp
{
	public class FmodException : Exception
	{
		public Result Result { get; }

		public FmodException(Result result, string message) : base(message)
		{
			Result = result;
		}
	}
}
