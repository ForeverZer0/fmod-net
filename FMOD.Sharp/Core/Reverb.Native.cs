using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using FMOD.Enumerations;
using FMOD.Structures;

namespace FMOD.Core
{
	[SuppressUnmanagedCodeSecurity][SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
	public partial class Reverb
	{
		#region Native Methods

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_Reverb3D_Get3DAttributes(IntPtr reverb, ref Vector position, ref float minDistance,
			ref float maxDistance);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_Reverb3D_GetActive(IntPtr reverb, out bool active);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_Reverb3D_GetProperties(IntPtr reverb, ref ReverbProperties properties);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_Reverb3D_GetUserData(IntPtr reverb, out IntPtr userData);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_Reverb3D_Set3DAttributes(IntPtr reverb, ref Vector position, float minDistance,
			float maxDistance);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_Reverb3D_SetActive(IntPtr reverb, bool active);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_Reverb3D_SetProperties(IntPtr reverb, ref ReverbProperties properties);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_Reverb3D_SetUserData(IntPtr reverb, IntPtr userData);

		#endregion
	}
}