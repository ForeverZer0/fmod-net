using System;
using FMOD.Enumerations;

namespace FMOD.Core
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
