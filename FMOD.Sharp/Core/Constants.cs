namespace FMOD.Core
{
	/// <summary>
	/// Static class containing constant values for reference.
	/// </summary>
	public static class Constants
	{
#if X64
		/// <summary>
		/// The platform specific name of the native library to link to.
		/// </summary>
		public const string LIBRARY = "fmod64";
#elif X86
		/// <summary>
		/// The platform specific name of the native library to link to.
		/// </summary>
		public const string LIBRARY = "fmod";
#endif
		/// <summary>
		/// The version is a 32-bit hexadecimal value formated as 16:8:8, with the upper 16-bits being the major version, the middle 8-bits being the minor version and the bottom 8-bits being the development version.
		/// </summary>
		public const int VERSION = 0x00011002;

		/// <summary>
		/// The maximum number of "real" <see cref="Channel"/> objects that can play simultaneously. 
		/// </summary>
		public const int MAX_CHANNELS = 32;

		/// <summary>
		/// The maximum number of listeners for 3D sound.
		/// </summary>
		public const int MAX_LISTENERS = 8;

		/// <summary>
		/// The maximum number of <see cref="Reverb"/> objects.
		/// </summary>
		public const int MAX_REVERBS = 4;

		/// <summary>
		/// The maximum number of <seealso cref="FmodSystem"/> objects.
		/// </summary>
		public const int MAX_SYSTEMS = 8;
	}
}
