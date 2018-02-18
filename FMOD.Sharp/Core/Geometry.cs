#region License

// Geometry.cs is distributed under the Microsoft Public License (MS-PL)
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
using System.IO;
using FMOD.Arguments;
using FMOD.Data;
using FMOD.Structures;

#endregion

namespace FMOD.Core
{
	/// <summary>
	///     Describes the shape of a 3D environment.
	/// </summary>
	/// <remarks>
	///     <para>Polygons can be added to a geometry object using <see cref="O:FMOD.Core.Geometry.AddPolygon" />.</para>
	///     <para>
	///         A geometry object stores its list of polygons in a structure optimized for quick line intersection testing and
	///         efficient insertion and updating.<lineBreak />
	///         The structure works best with regularly shaped polygons with minimal overlap.<lineBreak />
	///         Many overlapping polygons, or clusters of long thin polygons may not be handled efficiently.<lineBreak />
	///         Axis aligned polygons are handled most efficiently.
	///     </para>
	///     <para>
	///         The same type of structure is used to optimize line intersection testing with multiple geometry objects.
	///     </para>
	///     <para>
	///         It is important to set the value of <see cref="FmodSystem.WorldSize" /> to an appropriate value.<lineBreak />
	///         Objects or polygons outside the range of maxworldsize will not be handled efficiently.<lineBreak />
	///         Conversely, if maxworldsize is excessively large, the structure may lose precision and efficiency may drop.
	///     </para>
	/// </remarks>
	/// <seealso cref="FMOD.Core.HandleBase" />
	/// <seealso cref="FmodSystem.CreateGeometry" />
	/// <seealso cref="Polygon" />
	/// <seealso cref="Vector" />
	public partial class Geometry : HandleBase
	{
		/// <summary>
		///     Occurs when the <see cref="Active" /> property has changed.
		/// </summary>
		/// <seealso cref="Active" />
		public event EventHandler ActiveChanged;

		/// <summary>
		///     Occurs when a polygon has been added to the <see cref="Geometry" /> object.
		/// </summary>
		/// <seealso cref="O:FMOD.Core.Geometry.AddPolygon" />
		/// <seealso cref="Polygon" />
		/// <seealso cref="PolygonEventArgs" />
		public event EventHandler<PolygonEventArgs> PolygonAdded;

		/// <summary>
		///     Occurs when the attributes of a <see cref="Polygon" /> have changed.
		/// </summary>
		/// <seealso cref="O:FMOD.Core.Geometry.SetPolygonAttributes" />
		/// <seealso cref="Polygon" />
		/// <seealso cref="PolygonEventArgs" />
		public event EventHandler<PolygonEventArgs> PolygonAttributesChanged;

		/// <summary>
		///     Occurs when a vertex of a <see cref="Polygon" /> have changed.
		/// </summary>
		/// <seealso cref="SetVertex" />
		/// <seealso cref="Polygon" />
		/// <seealso cref="PolygonEventArgs" />
		public event EventHandler<PolygonEventArgs> PolygonVertexChanged;

		/// <summary>
		///     Occurs when the <see cref="Position" /> property has changed.
		/// </summary>
		/// <seealso cref="Position" />
		public event EventHandler PositionChanged;

		/// <summary>
		///     Occurs when the rotation has changed.
		/// </summary>
		/// <seealso cref="Rotation" />
		/// <seealso cref="SetRotation" />
		public event EventHandler RotationChanged;

		/// <summary>
		///     Occurs when <see cref="Scale" /> property has changed.
		/// </summary>
		/// <seealso cref="Scale" />
		public event EventHandler ScaleChanged;

		/// <summary>
		///     Occurs when the <see cref="UserData" /> property has changed.
		/// </summary>
		/// <seealso cref="UserData" />
		public event EventHandler UserDataChanged;

		/// <summary>
		///     Initializes a new instance of the <see cref="Geometry" /> class.
		/// </summary>
		/// <param name="handle">The handle.</param>
		internal Geometry(IntPtr handle) : base(handle)
		{
		}

		/// <summary>
		///     Gets or sets a user value that the <see cref="FmodSystem" /> object will store internally.
		/// </summary>
		/// <value>
		///     The user data.
		/// </value>
		/// <remarks>
		///     <para>This function is primarily used in case the user wishes to "attach" data to an <b>FMOD</b> object.</para>
		///     <para>
		///         It can be useful if an FMOD callback passes an object of this type as a parameter, and the user does not know
		///         which object it is (if many of these types of objects exist).
		///     </para>
		/// </remarks>
		/// <seealso cref="UserDataChanged" />
		public IntPtr UserData
		{
			get
			{
				NativeInvoke(FMOD_Geometry_GetUserData(this, out var data));
				return data;
			}
			set
			{
				NativeInvoke(FMOD_Geometry_SetUserData(this, value));
				UserDataChanged?.Invoke(this, EventArgs.Empty);
			}
		}

		/// <summary>
		///     Gets or sets the enabled state of the <see cref="Geometry" /> object from being processed in the geometry engine.
		///     <para><c>true</c> = active, <c>false</c> = not active. Default = <c>true</c>. </para>
		/// </summary>
		/// <value>
		///     <c>true</c> if active; otherwise, <c>false</c>.
		/// </value>
		/// <seealso cref="ActiveChanged" />
		public bool Active
		{
			get
			{
				NativeInvoke(FMOD_Geometry_GetActive(this, out var active));
				return active;
			}
			set
			{
				NativeInvoke(FMOD_Geometry_SetActive(this, value));
				ActiveChanged?.Invoke(this, EventArgs.Empty);
			}
		}

		/// <summary>
		///     <para>Gets or sets the relative scale vector of the <see cref="Geometry" /> object. </para>
		///     <para>
		///         An object can be scaled/warped in all 3 dimensions separately using the vector without having to modify
		///         polygon data.
		///     </para>
		///     <para>Default = <c>1.0, 1.0, 1.0</c>.</para>
		/// </summary>
		/// <value>
		///     The scale vector.
		/// </value>
		/// <seealso cref="ScaleChanged" />
		/// <seealso cref="Vector" />
		/// <seealso cref="Position" />
		/// <seealso cref="Rotation" />
		public Vector Scale
		{
			get
			{
				NativeInvoke(FMOD_Geometry_GetScale(this, out var scale));
				return scale;
			}
			set
			{
				NativeInvoke(FMOD_Geometry_SetScale(this, ref value));
				ScaleChanged?.Invoke(this, EventArgs.Empty);
			}
		}

		/// <summary>
		///     <para>Gets the maximum number of polygons allocatable for this object. </para>
		///     <para>This is not the number of polygons currently present.</para>
		/// </summary>
		/// <value>
		///     The maximum polygons.
		/// </value>
		/// <remarks>This value is set during creation via <see cref="FmodSystem.CreateGeometry" />.</remarks>
		/// <seealso cref="MaxVertices" />
		/// <seealso cref="FmodSystem.CreateGeometry" />
		/// <seealso cref="O:FMOD.Core.FmodSystem.LoadGeometry" />
		public int MaxPolygons
		{
			get
			{
				NativeInvoke(FMOD_Geometry_GetMaxPolygons(this, out var polygons, out var dummy));
				return polygons;
			}
		}

		/// <summary>
		///     <para>Gets the maximum number of vertices allocatable for this object. </para>
		///     <para>This is not the number of vertices currently present.</para>
		/// </summary>
		/// <value>
		///     The maximum vertices.
		/// </value>
		/// <remarks>This value is set during creation via <see cref="FmodSystem.CreateGeometry" />.</remarks>
		/// <seealso cref="MaxPolygons" />
		/// <seealso cref="FmodSystem.CreateGeometry" />
		/// <seealso cref="O:FMOD.Core.FmodSystem.LoadGeometry" />
		public int MaxVertices
		{
			get
			{
				NativeInvoke(FMOD_Geometry_GetMaxPolygons(this, out var dummy, out var vertices));
				return vertices;
			}
		}

		/// <summary>
		///     Gets or sets the position of the object in 3D world space.
		/// </summary>
		/// <value>
		///     The position.
		/// </value>
		/// <seealso cref="PositionChanged" />
		/// <seealso cref="Vector" />
		public Vector Position
		{
			get
			{
				NativeInvoke(FMOD_Geometry_GetPosition(this, out var position));
				return position;
			}
			set
			{
				NativeInvoke(FMOD_Geometry_SetPosition(this, ref value));
				PositionChanged?.Invoke(this, EventArgs.Empty);
			}
		}

		/// <summary>
		///     Gets the number of polygons stored within this geometry object.
		/// </summary>
		/// <value>
		///     The polygon count.
		/// </value>
		/// <seealso cref="AddPolygon(FMOD.Data.Polygon)" />
		/// <seealso cref="AddPolygon(float, float, bool,Vector[])" />
		public int PolygonCount
		{
			get
			{
				NativeInvoke(FMOD_Geometry_GetNumPolygons(this, out var count));
				return count;
			}
		}

		/// <summary>
		///     Gets or sets the orientation of the geometry object.
		/// </summary>
		/// <value>
		///     The rotation.
		/// </value>
		/// <remarks>
		///     See <see cref="O:FMOD.Core.FmodSystem.SetListenerAttributes" /> remarks for more description on forward and up
		///     vectors.
		/// </remarks>
		/// <seealso cref="Rotation" />
		/// <seealso cref="Data.Rotation" />
		/// <seealso cref="Vector" />
		/// <seealso cref="O:FMOD.Core.FmodSystem.SetListenerAttributes" />
		public Rotation Rotation
		{
			get
			{
				NativeInvoke(FMOD_Geometry_GetRotation(this, out var forward, out var up));
				return new Rotation
				{
					Forward = forward,
					Up = up
				};
			}
			set => SetRotation(value.Forward, value.Up);
		}

		/// <summary>
		///     Adds a polygon to an existing geometry object.
		/// </summary>
		/// <param name="polygon">The polygon describing a shape in 3D space.</param>
		/// <returns>
		///     <para>The polygon index for this object.</para>
		///     <para>This index can be used later with other per polygon based geometry functions.</para>
		/// </returns>
		/// <exception cref="System.ArgumentOutOfRangeException">
		///     Thrown when the <paramref name="polygon.Vertices" /> property
		///     contains less than 3 <see cref="Vector" /> instances.
		/// </exception>
		/// <seealso cref="PolygonCount" />
		/// <seealso cref="Position" />
		/// <seealso cref="Polygon" />
		/// <seealso cref="Vector" />
		/// <seealso cref="PolygonAdded" />
		public int AddPolygon(Polygon polygon)
		{
			return AddPolygon(polygon.Attributes.DirectOcclusion, polygon.Attributes.ReverbOcclusion,
				polygon.Attributes.DoubleSided, polygon.Vertices);
		}

		/// <summary>
		///     Adds a polygon to an existing geometry object.
		/// </summary>
		/// <param name="directOcclusion">
		///     <para>Occlusion value from <c>0.0</c> to <c>1.0</c> which affects volume or audible frequencies.</para>
		///     <para><c>0.0</c> = The polygon does not occlude volume or audible frequencies (sound will be fully audible).</para>
		///     <para><c>1.0</c> = The polygon fully occludes (sound will be silent).</para>
		/// </param>
		/// <param name="reverbOcclusion">
		///     <para>Occlusion value from <c>0.0</c> to <c>1.0</c> which affects the reverb mix.</para>
		///     <para><c>0.0</c> = The polygon does not occlude reverb (reverb reflections still travel through this polygon).</para>
		///     <para><c>1.0</c> = The polyfully fully occludes reverb (reverb reflections will be silent through this polygon).</para>
		/// </param>
		/// <param name="doubleSided">
		///     <para>Description of polygon if it is double sided or single sided.</para>
		///     <para><c>true</c> = polygon is double sided.</para>
		///     <para>
		///         <c>false</c> = polygon is single sided, and the winding of the polygon (which determines the polygon's
		///         normal) determines which side of the polygon will cause occlusion.
		///     </para>
		/// </param>
		/// <param name="vertices">
		///     <para>An array of vertices located in object space.</para>
		///     <para>
		///         <b>Must be a minimum of three vertices within the array!</b>
		///     </para>
		/// </param>
		/// <returns>
		///     <para>The polygon index for this object.</para>
		///     <para>This index can be used later with other per polygon based geometry functions.</para>
		/// </returns>
		/// <exception cref="System.ArgumentOutOfRangeException">
		///     Thrown when the <paramref name="vertices" /> parameter contains
		///     less than 3 <see cref="Vector" /> instances.
		/// </exception>
		/// <seealso cref="PolygonCount" />
		/// <seealso cref="Position" />
		/// <seealso cref="Polygon" />
		/// <seealso cref="Vector" />
		/// <seealso cref="PolygonAdded" />
		public int AddPolygon(float directOcclusion, float reverbOcclusion, bool doubleSided, Vector[] vertices)
		{
			if (vertices.Length < 3)
				throw new ArgumentOutOfRangeException(nameof(vertices), vertices.Length,
					String.Format(ResultStrings.PolygonNotEnoughVertices, vertices.Length));
			NativeInvoke(FMOD_Geometry_AddPolygon(this, directOcclusion, reverbOcclusion, doubleSided,
				vertices.Length, vertices, out var index));
			PolygonAdded?.Invoke(this, new PolygonEventArgs(index));
			return index;
		}

		/// <summary>
		///     Gets the <see cref="Polygon" /> from the specified index.
		/// </summary>
		/// <param name="index">The index of the polygon to retrieve.</param>
		/// <returns>The specified <see cref="Polygon" />.</returns>
		/// <seealso cref="AddPolygon(FMOD.Data.Polygon)" />
		/// <seealso cref="Polygon" />
		public Polygon GetPolygon(int index)
		{
			var verticeCount = GetVerticesCount(index);
			var vertices = new Vector[verticeCount];
			for (var i = 0; i < verticeCount; i++)
				vertices[i] = GetVertex(index, i);
			return new Polygon
			{
				Attributes = GetPolygonAttributes(index),
				Vertices = vertices
			};
		}

		/// <summary>
		///     Gets the attributes for a particular polygon inside a <see cref="Geometry" /> object.
		/// </summary>
		/// <param name="index">The index of the polygon.</param>
		/// <returns>Attributes describing the polygon.</returns>
		/// <seealso cref="PolygonAttributes" />
		/// <seealso cref="SetPolygonAttributes(int, FMOD.Data.PolygonAttributes)" />
		/// <seealso cref="SetPolygonAttributes(int, float, float, bool)" />
		/// <seealso cref="PolygonCount" />
		public PolygonAttributes GetPolygonAttributes(int index)
		{
			NativeInvoke(FMOD_Geometry_GetPolygonAttributes(this, index, out var direct, out var reverb, out var doubleSided));
			return new PolygonAttributes
			{
				DirectOcclusion = direct,
				ReverbOcclusion = reverb,
				DoubleSided = doubleSided
			};
		}

		/// <summary>
		///     Retrieves the position of the vertex of a <see cref="Polygon" /> inside a <see cref="Geometry" /> object.
		/// </summary>
		/// <param name="polygonIndex">
		///     Polygon index.
		///     <para>This must be in the range of <c>0</c> to <see cref="PolygonCount" /> minus <c>1</c>.</para>
		/// </param>
		/// <param name="vertexIndex">
		///     Vertex index inside the polygon.
		///     <para>This must be in the range of <c>0</c> to <see cref="GetVerticesCount" /> minus <c>1</c>.</para>
		/// </param>
		/// <returns>A <see cref="Vector" /> structure containing the vertex location in object space. </returns>
		/// <remarks>Vertices are relative to the position of the object. See <see cref="Position" />.</remarks>
		/// <seealso cref="GetVerticesCount" />
		/// <seealso cref="PolygonCount" />
		/// <seealso cref="Position" />
		/// <seealso cref="Polygon" />
		/// <seealso cref="Vector" />
		public Vector GetVertex(int polygonIndex, int vertexIndex)
		{
			NativeInvoke(FMOD_Geometry_GetPolygonVertex(this, polygonIndex, vertexIndex, out var vertex));
			return vertex;
		}

		/// <summary>
		///     Gets the number of vertices in a <see cref="Polygon" /> which is part of the <see cref="Geometry" /> object.
		/// </summary>
		/// <param name="polygonIndex">
		///     <para>The index of the polygon.</para>
		///     <para>This must be in the range of <c>0</c> to <see cref="PolygonCount" /> minus <c>1</c>.</para>
		/// </param>
		/// <returns>The number of vertices for the selected polygon.</returns>
		/// <seealso cref="PolygonCount" />
		/// <seealso cref="Polygon" />
		public int GetVerticesCount(int polygonIndex)
		{
			NativeInvoke(FMOD_Geometry_GetPolygonNumVertices(this, polygonIndex, out var count));
			return count;
		}

		/// <summary>
		///     Saves the <see cref="Geometry" /> object to a specified file.
		/// </summary>
		/// <param name="filename">The filename where the serialized <see cref="Geometry" /> object will be saved.</param>
		/// <exception cref="UnauthorizedAccessException">
		///     Thrown when the caller does not have the required permission or
		///     <paramref name="filename" /> specified a read-only file or directory.
		/// </exception>
		/// <exception cref="ArgumentException">
		///     Thrown when <paramref name="filename" /> is a zero-length string, contains only
		///     white space, or contains one or more invalid characters as defined by <see cref="Path.GetInvalidPathChars" />.
		/// </exception>
		/// <exception cref="ArgumentNullException">Thrown when <paramref name="filename" /> is <c>null</c>.</exception>
		/// <exception cref="PathTooLongException">
		///     Thrown when specified path, file name, or both exceed the system-defined maximum
		///     length.
		/// </exception>
		/// <exception cref="DirectoryNotFoundException">
		///     Thrown when the specified path is invalid, (for example, it is on an
		///     unmapped drive).
		/// </exception>
		/// <exception cref="NotSupportedException">Thrown when <paramref name="filename" /> is in an invalid format.</exception>
		/// <seealso cref="Serialize" />
		public void Save(string filename)
		{
			var binary = Serialize();
			using (var stream = File.OpenWrite(filename))
			{
				stream.Write(binary, 0, binary.Length);
			}
		}

		/// <summary>
		///     <para>Serializes this object into a binary block, to a user <see cref="byte" /> array.</para>
		///     <para>
		///         This can then be saved to a file if required and loaded later with
		///         <see cref="FmodSystem.LoadGeometry(byte[])" />.
		///     </para>
		/// </summary>
		/// <returns>An array of bytes containing the serialized <see cref="Geometry" /> object.</returns>
		/// <seealso cref="Save" />
		/// <seealso cref="O:FMOD.Core.FmodSystem.LoadGeometry" />
		public byte[] Serialize()
		{
			NativeInvoke(FMOD_Geometry_Save(this, IntPtr.Zero, out var size));
			using (var buffer = new MemoryBuffer(size))
			{
				NativeInvoke(FMOD_Geometry_Save(this, buffer.Pointer, out var dummy));
				return buffer;
			}
		}

		/// <summary>
		///     Sets the attributes for each polygon inside a <see cref="Geometry" /> object.
		/// </summary>
		/// <param name="index">Polygon index inside the object.</param>
		/// <param name="attributes">Attributes describing the polygon.</param>
		/// <seealso cref="PolygonAttributesChanged" />
		/// <seealso cref="PolygonAttributes" />
		/// <seealso cref="GetPolygonAttributes" />
		/// <seealso cref="SetPolygonAttributes(int, float, float, bool)" />
		/// <seealso cref="PolygonCount" />
		public void SetPolygonAttributes(int index, PolygonAttributes attributes)
		{
			SetPolygonAttributes(index, attributes.DirectOcclusion,
				attributes.ReverbOcclusion, attributes.DoubleSided);
		}

		/// <summary>
		///     Sets the individual attributes for each polygon inside a <see cref="Geometry" /> object.
		/// </summary>
		/// <param name="index">Polygon index inside the object.</param>
		/// <param name="directOcclusion">
		///     <para>Occlusion value from <c>0.0</c> to <c>1.0</c> which affects volume or audible frequencies.</para>
		///     <para><c>0.0</c> = The polygon does not occlude volume or audible frequencies (sound will be fully audible).</para>
		///     <para><c>1.0</c> = The polygon fully occludes (sound will be silent).</para>
		/// </param>
		/// <param name="reverbOcclusion">
		///     <para>Occlusion value from <c>0.0</c> to <c>1.0</c> which affects the reverb mix.</para>
		///     <para><c>0.0</c> = The polygon does not occlude reverb (reverb reflections still travel through this polygon).</para>
		///     <para><c>1.0</c> = The polyfully fully occludes reverb (reverb reflections will be silent through this polygon).</para>
		/// </param>
		/// <param name="doubleSided">
		///     <para>Description of polygon if it is double sided or single sided.</para>
		///     <para><c>true</c> = polygon is double sided.</para>
		///     <para>
		///         <c>false</c> = polygon is single sided, and the winding of the polygon (which determines the polygon's
		///         normal) determines which side of the polygon will cause occlusion.
		///     </para>
		/// </param>
		/// <seealso cref="PolygonAttributesChanged" />
		/// <seealso cref="PolygonAttributes" />
		/// <seealso cref="GetPolygonAttributes" />
		/// <seealso cref="SetPolygonAttributes(int, PolygonAttributes)" />
		/// <seealso cref="PolygonCount" />
		public void SetPolygonAttributes(int index, float directOcclusion, float reverbOcclusion, bool doubleSided)
		{
			NativeInvoke(FMOD_Geometry_SetPolygonAttributes(this, index, directOcclusion,
				reverbOcclusion, doubleSided));
			PolygonAttributesChanged?.Invoke(this, new PolygonEventArgs(index));
		}

		/// <summary>
		///     Sets the orientation of the geometry object.
		/// </summary>
		/// <param name="forward">
		///     <para>
		///         The forwards orientation of the geometry object. This vector must be of unit length and perpendicular to the
		///         <paramref name="up" /> vector.
		///     </para>
		///     <para>Specify <c>null</c> to not update the forwards orientation of the geometry object.</para>
		/// </param>
		/// <param name="up">
		///     <para>
		///         The upwards orientation of the geometry object. This vector must be of unit length and perpendicular to the
		///         <paramref name="forward" /> vector.
		///     </para>
		///     <para>Specify <c>null</c> to not update the upwards orientation of the geometry object.</para>
		/// </param>
		/// <remarks>
		///     See <see cref="O:FMOD.Core.FmodSystem.SetListenerAttributes" /> remarks for more description on forward and up
		///     vectors.
		/// </remarks>
		/// <seealso cref="Rotation" />
		/// <seealso cref="Data.Rotation" />
		/// <seealso cref="Vector" />
		/// <seealso cref="O:FMOD.Core.FmodSystem.SetListenerAttributes" />
		/// <seealso cref="RotationChanged" />
		public void SetRotation(Vector? forward, Vector? up)
		{
			if (!forward.HasValue && !up.HasValue)
				return;
			Vector forwardValue;
			Vector upValue;
			if (forward.HasValue && up.HasValue)
			{
				forwardValue = forward.Value;
				upValue = up.Value;
				NativeInvoke(FMOD_Geometry_SetRotation(this, ref forwardValue, ref upValue));
			}
			else if (forward.HasValue)
			{
				forwardValue = forward.Value;
				NativeInvoke(FMOD_Geometry_SetRotation(this, ref forwardValue, IntPtr.Zero));
			}
			else
			{
				upValue = up.Value;
				NativeInvoke(FMOD_Geometry_SetRotation(this, IntPtr.Zero, ref upValue));
			}
			RotationChanged?.Invoke(this, EventArgs.Empty);
		}

		/// <summary>
		///     Alters the position of a polygon's vertex inside a geometry object.
		/// </summary>
		/// <param name="polygonIndex">
		///     Polygon index.
		///     <para>This must be in the range of <c>0</c> to <see cref="PolygonCount" /> minus <c>1</c>.</para>
		/// </param>
		/// <param name="vertexIndex">
		///     Vertex index inside the polygon.
		///     <para>This must be in the range of <c>0</c> to <see cref="GetVerticesCount" /> minus <c>1</c>.</para>
		/// </param>
		/// <param name="vertex">A <see cref="Vector" /> that describes the new vertex location.</param>
		/// <seealso cref="PolygonVertexChanged" />
		public void SetVertex(int polygonIndex, int vertexIndex, Vector vertex)
		{
			NativeInvoke(FMOD_Geometry_SetPolygonVertex(this, polygonIndex, vertexIndex, ref vertex));
			PolygonVertexChanged?.Invoke(this, new PolygonEventArgs(polygonIndex));
		}
	}
}