using System;
using FMOD.NET.Structures;

namespace FMOD.NET.Data
{
	/// <summary>
	/// Describes the 3D properties of a "virtual" reverb object.
	/// </summary>
	[Serializable]
	public class Reverb3DAttributes
	{
		/// <summary>
		/// <para>Gets or sets the vector containing the 3D position of the center of the reverb in 3D space.</para> 
		/// <para>Default = <see cref="Vector.Zero"/></para>
		/// </summary>
		/// <value>
		/// The position.
		/// </value>
		public Vector Position { get; set; } = Vector.Zero;

		/// <summary>
		/// <para>Gets or sets the distance from the centerpoint that the reverb will have <i>full</i> effect at.</para>
		/// <para>Default = <c>0.0</c></para>
		/// </summary>
		/// <value>
		/// The minimum distance.
		/// </value>
		public float MinimumDistance { get; set; } = 0.0f;

		/// <summary>
		/// Gets or sets the distance from the centerpoint that the reverb will not have <i>any</i> effect.
		/// <para>Default = <c>0.0</c></para>
		/// </summary>
		/// <value>
		/// The maximum distance.
		/// </value>
		public float MaximumDistance { get; set; } = 0.0f;
	}
}