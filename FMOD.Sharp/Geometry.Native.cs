﻿using System;
using System.Runtime.InteropServices;
using System.Security;
using FMOD.Sharp.Enums;
using FMOD.Sharp.Structs;

namespace FMOD.Sharp
{
	[SuppressUnmanagedCodeSecurity]
	public partial class Geometry
	{
		#region Native Methods

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_Geometry_AddPolygon(IntPtr geometry, float directOcclusion, float reverbOcclusion,
			bool doubleSided, int numVertices, Vector[] vertices, out int polygonIndex);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_Geometry_GetActive(IntPtr geometry, out bool active);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_Geometry_GetMaxPolygons(IntPtr geometry, out int maxPolygons, out int maxVertices);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_Geometry_GetNumPolygons(IntPtr geometry, out int numPolygons);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_Geometry_GetPolygonAttributes(IntPtr geometry, int index, out float directOcclusion,
			out float reverbOcclusion, out bool doubleSided);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_Geometry_GetPolygonNumVertices(IntPtr geometry, int index, out int numVertices);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_Geometry_GetPolygonVertex(IntPtr geometry, int index, int vertexIndex,
			out Vector vertex);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_Geometry_GetPosition(IntPtr geometry, out Vector position);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_Geometry_GetRotation(IntPtr geometry, out Vector forward, out Vector up);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_Geometry_GetScale(IntPtr geometry, out Vector scale);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_Geometry_GetUserData(IntPtr geometry, out IntPtr userData);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_Geometry_Save(IntPtr geometry, IntPtr data, out int dataSize);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_Geometry_SetActive(IntPtr geometry, bool active);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_Geometry_SetPolygonAttributes(IntPtr geometry, int index, float directOcclusion,
			float reverbOcclusion, bool doubleSided);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_Geometry_SetPolygonVertex(IntPtr geometry, int index, int vertexIndex,
			ref Vector vertex);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_Geometry_SetPosition(IntPtr geometry, ref Vector position);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_Geometry_SetRotation(IntPtr geometry, ref Vector forward, ref Vector up);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_Geometry_SetScale(IntPtr geometry, ref Vector scale);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_Geometry_SetUserData(IntPtr geometry, IntPtr userData);

		#endregion
	}
}