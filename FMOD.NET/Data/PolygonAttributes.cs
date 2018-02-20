namespace FMOD.Data
{
	public class PolygonAttributes
	{
		/// <summary>
		/// <para>Gets or sets the direct occlusion.</para>
		/// <para>Occlusion value from <c>0.0</c> to <c>1.0</c> which affects volume or audible frequencies.</para>
		/// <para><c>0.0</c> = The polygon does not occlude volume or audible frequencies (sound will be fully audible).</para>
		/// <para><c>1.0</c> = The polygon fully occludes (sound will be silent).</para>
		/// </summary>
		/// <value>
		/// The direct occlusion.
		/// </value>
		public float DirectOcclusion { get; set; }

		/// <summary>
		/// Gets or sets the reverb occlusion.
		/// <para>Occlusion value from <c>0.0</c> to <c>1.0</c> which affects the reverb mix.</para>
		/// <para><c>0.0</c> = The polygon does not occlude reverb (reverb reflections still travel through this polygon).</para>
		/// <para><c>1.0</c> = The polyfully fully occludes reverb (reverb reflections will be silent through this polygon).</para>
		/// </summary>
		/// <value>
		/// The reverb occlusion.
		/// </value>
		public float ReverbOcclusion { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether the polygon is double-sided.
		/// <para><c>true</c> = polygon is double sided.</para>
		/// <para><c>false</c> = polygon is single sided, and the winding of the polygon (which determines the polygon's normal) determines which side of the polygon will cause occlusion.</para>
		/// </summary>
		/// <value>
		///   <c>true</c> if [double sided]; otherwise, <c>false</c>.
		/// </value>
		public bool DoubleSided { get; set; }
	}
}
