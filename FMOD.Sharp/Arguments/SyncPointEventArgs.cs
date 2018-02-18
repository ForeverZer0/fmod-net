using System;

namespace FMOD.Arguments
{
	public class SyncPointEventArgs : EventArgs
	{
		public IntPtr SyncPoint { get; }

		public SyncPointEventArgs(IntPtr syncPoint)
		{
			SyncPoint = syncPoint;
		}
	}
}