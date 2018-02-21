using System;

namespace FMOD.NET.Arguments
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