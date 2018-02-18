using System;

namespace FMOD.Arguments
{
	/// <inheritdoc />
	/// <summary>
	/// Provides data for <see cref="E:FMOD.Core.Channel.OcclusionCalculated" /> events.
	/// </summary>
	/// <seealso cref="T:System.EventArgs" />
	/// <seealso cref="T:FMOD.Core.Channel" />
	/// <seealso cref="E:FMOD.Core.Channel.OcclusionCalculated"/>
	public class OcclusionCalculatedEventArgs : EventArgs
	{
		/// <summary>
		/// Gets the pointer to a <see cref="float"/> direct value that can be read (dereferenced) and modified after the geometry engine has calculated it for this channel.
		/// </summary>
		/// <value>
		/// The pointer to the direct occlusion value.
		/// </value>
		public IntPtr DirectOcclusion { get; }

		/// <summary>
		/// Gets the pointer to a <see cref="float"/> reverb value that can be read (dereferenced) and modified after the geometry engine has calculated it for this channel.
		/// </summary>
		/// <value>
		/// The pointer to the reverb occlusion value.
		/// </value>
		public IntPtr ReverbOcclusion { get; }

		/// <inheritdoc />
		/// <summary>
		/// Initializes a new instance of the <see cref="T:FMOD.Arguments.ChannelOcclusionCalculatedEventArgs" /> class.
		/// </summary>
		/// <param name="direct">The pointer to the direct occlusion value.</param>
		/// <param name="reverb">The pointer to the reverb occlusion value.</param>
		public OcclusionCalculatedEventArgs(IntPtr direct, IntPtr reverb)
		{
			DirectOcclusion = direct;
			ReverbOcclusion = reverb;
		}
	}
}