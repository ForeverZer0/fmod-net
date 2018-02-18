using System;
using FMOD.Core;

namespace FMOD.Arguments
{
	/// <inheritdoc />
	/// <summary>
	/// Arguments for <see cref="T:FMOD.Data.Polygon" /> specific events.
	/// </summary>
	/// <seealso cref="T:System.EventArgs" />
	/// <seealso cref="E:FMOD.Core.Geometry.PolygonAdded" />
	/// <seealso cref="E:FMOD.Core.Geometry.PolygonAttributesChanged" />
	/// <seealso cref="E:FMOD.Core.Geometry.PolygonVertexChanged" />
	public class PolygonEventArgs : EventArgs
	{
		/// <summary>
		/// Gets the index of the polygon within the <see cref="Geometry"/> object.
		/// </summary>
		/// <value>
		/// The index of the polygon.
		/// </value>
		public int Index { get; }

		/// <inheritdoc />
		/// <summary>
		/// Initializes a new instance of the <see cref="T:FMOD.Arguments.PolygonEventArgs" /> class.
		/// </summary>
		/// <param name="index">The index of the polygon within a <see cref="T:FMOD.Core.Geometry" /> object.</param>
		public PolygonEventArgs(int index)
		{
			Index = index;
		}
	}
}