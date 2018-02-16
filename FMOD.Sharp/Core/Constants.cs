namespace FMOD.Core
{
	public static class Constants
	{

#if X64
		public const string LIBRARY = "fmod64";
#elif X86
		public const string LIBRARY = "fmod";
#endif

		public const int VERSION = 0x00011002;
		public const int MAX_CHANNELS = 32;
		public const int MAX_LISTENERS = 8;
		public const int MAX_REVERBS = 4;
		public const int MAX_SYSTEMS = 8;
	}
}
