using System;
using System.Runtime.InteropServices;
using FMOD.Sharp.Enums;
using FMOD.Sharp.Structs;

namespace FMOD.Sharp
{
	public partial class Reverb
	{
		#region Native Methods

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_Reverb3D_Get3DAttributes(IntPtr reverb, ref Vector position, ref float minDistance,
			ref float maxDistance);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_Reverb3D_GetActive(IntPtr reverb, out bool active);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_Reverb3D_GetProperties(IntPtr reverb, ref ReverbProperties properties);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_Reverb3D_GetUserData(IntPtr reverb, out IntPtr userData);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_Reverb3D_Release(IntPtr reverb);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_Reverb3D_Set3DAttributes(IntPtr reverb, ref Vector position, float minDistance,
			float maxDistance);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_Reverb3D_SetActive(IntPtr reverb, bool active);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_Reverb3D_SetProperties(IntPtr reverb, ref ReverbProperties properties);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_Reverb3D_SetUserData(IntPtr reverb, IntPtr userData);

		#endregion
	}
}