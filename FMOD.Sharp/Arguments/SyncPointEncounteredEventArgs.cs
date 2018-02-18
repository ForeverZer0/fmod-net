using System;
using FMOD.Data;

namespace FMOD.Arguments
{
	/// <inheritdoc />
	/// <summary>
	/// Provides data for <see cref="E:FMOD.Core.Channel.SyncPointEncountered" /> events.
	/// </summary>
	/// <seealso cref="T:System.EventArgs" />
	/// <seealso cref="T:FMOD.Core.Channel" />
	/// <seealso cref="T:FMOD.Data.SyncPointInfo" />
	/// <seealso cref="E:FMOD.Core.Channel.SyncPointEncountered" />
	public class SyncPointEncounteredEventArgs : EventArgs
	{
		/// <summary>
		/// Gets the index of the sync point.
		/// </summary>
		/// <value>
		/// The index.
		/// </value>
		public int Index { get; }

		/// <summary>
		/// Gets the synchronize point.
		/// </summary>
		/// <value>
		/// The synchronize point.
		/// </value>
		public IntPtr SyncPoint { get; }

		/// <summary>
		/// Gets the synchronize point information.
		/// </summary>
		/// <value>
		/// The synchronize point information.
		/// </value>
		public SyncPointInfo SyncPointInfo { get; }

		/// <inheritdoc />
		/// <summary>
		/// Initializes a new instance of the <see cref="T:FMOD.Arguments.ChannelSyncPointEncounteredEventArgs" /> class.
		/// </summary>
		/// <param name="index">The index.</param>
		/// <param name="syncPoint">The sync point.</param>
		/// <param name="syncPointInfo">The sync point info.</param>
		public SyncPointEncounteredEventArgs(int index, IntPtr syncPoint, SyncPointInfo syncPointInfo)
		{
			Index = index;
			SyncPoint = syncPoint;
			SyncPointInfo = syncPointInfo;
		}
	}
}