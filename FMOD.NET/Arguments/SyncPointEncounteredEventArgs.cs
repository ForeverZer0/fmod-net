using System;

namespace FMOD.NET.Arguments
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