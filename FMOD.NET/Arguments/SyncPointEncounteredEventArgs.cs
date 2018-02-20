using System;
using FMOD.Data;

namespace FMOD.Arguments
{

	public class SyncPointEncounteredEventArgs : EventArgs
	{

		public int Index { get; }


		public SyncPointEncounteredEventArgs(int index)
		{
			Index = index;
		}
	}
}