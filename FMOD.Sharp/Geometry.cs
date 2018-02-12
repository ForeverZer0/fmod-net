using System;
using System.IO;
using System.Runtime.InteropServices;
using FMOD.Sharp.Data;
using FMOD.Sharp.Enums;
using FMOD.Sharp.Structs;

namespace FMOD.Sharp
{
	public partial class Geometry : Handle
	{
		public Geometry(IntPtr handle) : base(handle)
		{
		}

		public IntPtr UserData
		{
			get
			{
				NativeInvoke(FMOD_Geometry_GetUserData(this, out var data));
				return data;
			}
			set => NativeInvoke(FMOD_Geometry_SetUserData(this, value));
		}

		public bool Active
		{
			get
			{
				NativeInvoke(FMOD_Geometry_GetActive(this, out var active));
				return active;
			}
			set => NativeInvoke(FMOD_Geometry_SetActive(this, value));
		}

		public Vector Scale
		{
			get
			{
				NativeInvoke(FMOD_Geometry_GetScale(this, out var scale));
				return scale;
			}
			set => NativeInvoke(FMOD_Geometry_SetScale(this, ref value));
		}

		public int MaxPolygons
		{
			get
			{
				NativeInvoke(FMOD_Geometry_GetMaxPolygons(this, out var polygons, out var dummy));
				return polygons;
			}
		}

		public int MaxVertices
		{
			get
			{
				NativeInvoke(FMOD_Geometry_GetMaxPolygons(this, out var dummy, out var vertices));
				return vertices;
			}
		}

		public Vector Position
		{
			get
			{
				NativeInvoke(FMOD_Geometry_GetPosition(this, out var position));
				return position;
			}
			set => NativeInvoke(FMOD_Geometry_SetPosition(this, ref value));
		}

		public int PolygonCount
		{
			get
			{
				NativeInvoke(FMOD_Geometry_GetNumPolygons(this, out var count));
				return count;
			}
		}

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
			set
			{
				var forward = value.Forward;
				var up = value.Up;
				NativeInvoke(FMOD_Geometry_SetRotation(this, ref forward, ref up));
			}
		}

		public override void Dispose()
		{
			NativeInvoke(FMOD_Geometry_Release(this));
		}

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

		public void SetPolygonAttributes(int index, PolygonAttributes attributes)
		{
			SetPolygonAttributes(index, attributes.DirectOcclusion,
				attributes.ReverbOcclusion, attributes.DoubleSided);
		}

		public void SetPolygonAttributes(int index, float directOcclusion, float reverbOcclusion, bool doubleSided)
		{
			NativeInvoke(FMOD_Geometry_SetPolygonAttributes(this, index, directOcclusion,
				reverbOcclusion, doubleSided));
		}

		public int GetVerticesCount(int polygonIndex)
		{
			NativeInvoke(FMOD_Geometry_GetPolygonNumVertices(this, polygonIndex, out var count));
			return count;
		}

		public Vector GetVertex(int polygonIndex, int vertexIndex)
		{
			NativeInvoke(FMOD_Geometry_GetPolygonVertex(this, polygonIndex, vertexIndex, out var vertex));
			return vertex;
		}

		public void SetVertex(int polygonIndex, int vertexIndex, Vector vertex)
		{
			NativeInvoke(FMOD_Geometry_SetPolygonVertex(this, polygonIndex, vertexIndex, ref vertex));
		}

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

		public void SetRotation(Vector forward, Vector up)
		{
			NativeInvoke(FMOD_Geometry_SetRotation(this, ref forward, ref up));
		}

		public byte[] Serialize()
		{
			NativeInvoke(FMOD_Geometry_Save(this, IntPtr.Zero, out var size));
			using (var buffer = new MemoryBuffer(size))
			{
				NativeInvoke(FMOD_Geometry_Save(this, buffer.Pointer, out var dummy));
				return buffer;
			}
		}

		public void Save(string filename)
		{
			var binary = Serialize();
			using (var stream = File.OpenWrite(filename))
				stream.Write(binary, 0, binary.Length);
		}

		public int AddPolygon(Polygon polygon)
		{
			return AddPolygon(polygon.Attributes.DirectOcclusion, polygon.Attributes.ReverbOcclusion,
				polygon.Attributes.DoubleSided, polygon.Vertices);
		}

		public int AddPolygon(float directOcclusion, float reverbOcclusion, bool doubleSided, Vector[] vertices)
		{
			NativeInvoke(FMOD_Geometry_AddPolygon(this, directOcclusion, reverbOcclusion, doubleSided,
				vertices.Length, vertices, out var index));
			return index;
		}
	}
}