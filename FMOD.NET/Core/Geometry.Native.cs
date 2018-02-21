#region License

// Geometry.Native.cs is distributed under the Microsoft Public License (MS-PL)
// 
// Copyright (c) 2018,  Eric Freed
// All Rights Reserved.
// 
// This license governs use of the accompanying software. If you use the software, you
// accept this license. If you do not accept the license, do not use the software.
// 
// 1. Definitions
// The terms "reproduce," "reproduction," "derivative works," and "distribution" have the
// same meaning here as under U.S. copyright law.
// A "contribution" is the original software, or any additions or changes to the software.
// A "contributor" is any person that distributes its contribution under this license.
// "Licensed patents" are a contributor's patent claims that read directly on its contribution.
// 
// 2. Grant of Rights
// (A) Copyright Grant- Subject to the terms of this license, including the license conditions 
// and limitations in section 3, each contributor grants you a non-exclusive, worldwide, royalty-free 
// copyright license to reproduce its contribution, prepare derivative works of its contribution, and 
// distribute its contribution or any derivative works that you create.
// 
// (B) Patent Grant- Subject to the terms of this license, including the license conditions and 
// limitations in section 3, each contributor grants you a non-exclusive, worldwide, royalty-free license
//  under its licensed patents to make, have made, use, sell, offer for sale, import, and/or otherwise 
// dispose of its contribution in the software or derivative works of the contribution in the software.
// 
// 3. Conditions and Limitations
// (A) No Trademark License- This license does not grant you rights to use any contributors' name, 
// logo, or trademarks.
// 
// (B) If you bring a patent claim against any contributor over patents that you claim are infringed by 
// the software, your patent license from such contributor to the software ends automatically.
// 
// (C) If you distribute any portion of the software, you must retain all copyright, patent, trademark, and
//  attribution notices that are present in the software.
// 
// (D) If you distribute any portion of the software in source code form, you may do so only under this 
// license by including a complete copy of this license with your distribution. If you distribute any portion
//  of the software in compiled or object code form, you may only do so under a license that complies 
// with this license.
// 
// (E) The software is licensed "as-is." You bear the risk of using it. The contributors give no express 
// warranties, guarantees or conditions. You may have additional consumer rights under your local laws 
// which this license cannot change. To the extent permitted under your local laws, the contributors 
// exclude the implied warranties of merchantability, fitness for a particular purpose and non-infringement.
// 
// Created 9:49 PM 02/15/2018

#endregion

#region Using Directives

using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using FMOD.NET.Enumerations;
using FMOD.NET.Structures;

#endregion

namespace FMOD.NET.Core
{
	[SuppressUnmanagedCodeSecurity]
	[SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
	public partial class Geometry
	{
		#region Native Methods

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_Geometry_AddPolygon(IntPtr geometry, float directOcclusion, float reverbOcclusion,
			bool doubleSided, int numVertices, Vector[] vertices, out int polygonIndex);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_Geometry_GetActive(IntPtr geometry, out bool active);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_Geometry_GetMaxPolygons(IntPtr geometry, out int maxPolygons, out int maxVertices);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_Geometry_GetNumPolygons(IntPtr geometry, out int numPolygons);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_Geometry_GetPolygonAttributes(IntPtr geometry, int index, out float directOcclusion,
			out float reverbOcclusion, out bool doubleSided);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_Geometry_GetPolygonNumVertices(IntPtr geometry, int index, out int numVertices);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_Geometry_GetPolygonVertex(IntPtr geometry, int index, int vertexIndex,
			out Vector vertex);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_Geometry_GetPosition(IntPtr geometry, out Vector position);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_Geometry_GetRotation(IntPtr geometry, out Vector forward, out Vector up);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_Geometry_GetScale(IntPtr geometry, out Vector scale);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_Geometry_GetUserData(IntPtr geometry, out IntPtr userData);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_Geometry_Save(IntPtr geometry, IntPtr data, out int dataSize);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_Geometry_SetActive(IntPtr geometry, bool active);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_Geometry_SetPolygonAttributes(IntPtr geometry, int index, float directOcclusion,
			float reverbOcclusion, bool doubleSided);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_Geometry_SetPolygonVertex(IntPtr geometry, int index, int vertexIndex,
			ref Vector vertex);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_Geometry_SetPosition(IntPtr geometry, ref Vector position);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_Geometry_SetRotation(IntPtr geometry, ref Vector forward, ref Vector up);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_Geometry_SetRotation(IntPtr geometry, IntPtr forward, ref Vector up);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_Geometry_SetRotation(IntPtr geometry, ref Vector forward, IntPtr up);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_Geometry_SetScale(IntPtr geometry, ref Vector scale);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_Geometry_SetUserData(IntPtr geometry, IntPtr userData);

		#endregion
	}
}