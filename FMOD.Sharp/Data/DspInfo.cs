using System;
using System.Drawing;

namespace FMOD.Data
{
	public class DspInfo
	{
		/// <summary>
		/// Gets the name of the unit.
		/// </summary>
		/// <value>
		/// The name.
		/// </value>
		public string Name { get; internal set; }

		/// <summary>
		/// Gets the version number of the DSP unit.
		/// </summary>
		/// <value>
		/// The version.
		/// </value>
		public Version Version { get; internal set; }

		/// <summary>
		/// <para>Gets the number of channels the unit was initialized with.</para> 
		/// <para><c>0</c> means the plugin will process whatever number of channels is currently in the network.</para> 
		/// <para>Greater than <c>0</c> would be mostly used if the unit is a unit that only generates sound, or is not flexible enough to take any number of input channels.</para>
		/// </summary>
		/// <value>
		/// The channel count.
		/// </value>
		public int ChannelCount { get; internal set; }

		/// <summary>
		/// Gets the size of the optional configuration window.
		/// </summary>
		/// <value>
		/// The size of the configuration window.
		/// </value>
		public Size ConfigWindowSize { get; internal set; }

		/// <summary>
		/// Returns a <see cref="System.String" /> that represents this instance.
		/// </summary>
		/// <returns>
		/// A <see cref="System.String" /> that represents this instance.
		/// </returns>
		public override string ToString()
		{
			return $"{Name} v.{Version}";
		}
	}
}