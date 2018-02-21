using System;
using FMOD.NET.Core;
using FMOD.NET.Data;

namespace FMOD.NET.Arguments
{
	/// <inheritdoc />
	/// <summary>
	/// Arguments for <see cref="Polygon" /> specific events.
	/// </summary>
	/// <seealso cref="System.EventArgs" />
	/// <seealso cref="Geometry.PolygonAdded" />
	/// <seealso cref="Geometry.PolygonAttributesChanged" />
	/// <seealso cref="Geometry.PolygonVertexChanged" />
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
		/// Initializes a new instance of the <see cref="PolygonEventArgs" /> class.
		/// </summary>
		/// <param name="index">The index of the polygon within a <see cref="Geometry" /> object.</param>
		public PolygonEventArgs(int index)
		{
			Index = index;
		}
	}
}