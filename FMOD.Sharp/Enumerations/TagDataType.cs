namespace FMOD.Enumerations
{
	/// <summary>
	/// List of data types that can be returned by <see cref="M:FMOD.Core.Sound.GetTag"/>.
	/// </summary>
	/// <seealso cref="M:FMOD.Core.Sound.GetTag"/>
	/// <seealso cref="M:FMOD.Core.Sound.GetTags"/>
	public enum TagDataType
    {
		/// <summary>
		/// Binary.
		/// </summary>
		Binary,
		/// <summary>
		/// Integer.
		/// </summary>
		Int,
		/// <summary>
		/// Float.
		/// </summary>
		Float,
		/// <summary>
		/// String.
		/// </summary>
		String,
		/// <summary>
		/// UTF-16 encoded string.
		/// </summary>
		StringUtf16,
		/// <summary>
		/// Unicode encoded string.
		/// </summary>
		StringUtf16Be,
		/// <summary>
		/// UTF-8 encoded string.
		/// </summary>
		StringUtf8,
		/// <summary>
		/// CDTOC.
		/// </summary>
		Cdtoc,
		/// <summary>
		/// Maximum number supported.
		/// </summary>
		Max
	}
}
