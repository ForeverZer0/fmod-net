#region License

// Vector.cs is distributed under the Microsoft Public License (MS-PL)
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
// Created 12:20 AM 02/04/2018

#endregion

#region Using Directives

using System;
using System.Runtime.InteropServices;
using FMOD.Core;
using FMOD.Data;
using FMOD.Enumerations;

#endregion

namespace FMOD.Structures
{
	/// <summary>
	///     Structure describing a point in 3D space.
	/// </summary>
	/// <remarks>
	///     <b>FMOD</b> uses a left handed coordinate system by default.
	///     To use a right handed coordinate system specify <see cref="InitFlags.RightHanded3D" /> in
	///     <see cref="O:FMOD.Core.FmodSystem.Initialize" />.
	/// </remarks>
	/// <seealso cref="FmodSystem.GetListenerAttributes" />
	/// <seealso cref="O:FMOD.Core.FmodSystem.SetListenerAttributes" />
	/// <seealso cref="Attributes3D" />
	/// <seealso cref="ChannelControl.Position3D" />
	/// <seealso cref="ChannelControl.Velocity3D" />
	/// <seealso cref="ChannelControl.SetAttributes3D" />
	/// <seealso cref="ChannelControl.CustomRolloff3D" />
	/// <seealso cref="Sound.CustomRolloff3D" />
	/// <seealso cref="O:FMOD.Core.Geometry.AddPolygon" />
	/// <seealso cref="Geometry" />
	/// <seealso cref="Polygon" />
	/// <seealso cref="Geometry.GetVertex" />
	/// <seealso cref="Geometry.SetVertex" />
	/// <seealso cref="Geometry" />
	/// <seealso cref="Polygon" />
	/// <seealso cref="Geometry.Rotation" />
	/// <seealso cref="Geometry.Position" />
	/// <seealso cref="Geometry.Scale" />
	/// <seealso cref="InitFlags" />
	[StructLayout(LayoutKind.Sequential)]
	[Serializable]
	public struct Vector
	{
		/// <summary>
		///     X coordinate in 3D space.
		/// </summary>
		public float X;

		/// <summary>
		///     Y coordinate in 3D space.
		/// </summary>
		public float Y;

		/// <summary>
		///     Z coordinate in 3D space.
		/// </summary>
		public float Z;

		/// <summary>
		///     Gets a vector initialized with a value of <c>0.0f</c> on all axis.
		/// </summary>
		/// <value>
		///     A zero value Vector..
		/// </value>
		public static Vector Zero => new Vector();

		/// <summary>
		///     Initializes a new instance of the <see cref="Vector" /> struct.
		/// </summary>
		/// <param name="x">The x coordinate in 3D space.</param>
		/// <param name="y">The y coordinate in 3D space.</param>
		/// <param name="z">The z coordinate in 3D space.</param>
		public Vector(float x, float y, float z)
		{
			X = x;
			Y = y;
			Z = z;
		}

		/// <summary>
		///     Gets the length of the vector.
		/// </summary>
		public float Length => (float) Math.Sqrt(X * X + Y * Y + Z * Z);

		/// <summary>
		///     Gets the length of the vector squared.
		/// </summary>
		public float LengthSquared => X * X + Y * Y + Z * Z;

		/// <summary>
		///     Updates the vector to maintain its direction, but to have a length
		///     of 1. Equivalent to dividing the vector by its Length.
		///     Returns NaN if length is zero.
		/// </summary>
		public void Normalize()
		{
			// Computation of length can overflow easily because it 
			// first computes squared length, so we first divide by
			// the largest coefficient.
			var m = Math.Abs(X);
			var absy = Math.Abs(Y);
			var absz = Math.Abs(Z);
			if (absy > m)
				m = absy;
			if (absz > m)
				m = absz;
			X /= m;
			Y /= m;
			Z /= m;
			var length = (float) Math.Sqrt(X * X + Y * Y + Z * Z);
			this /= length;
		}

		/// <summary>
		///     Operator -Vector (unary negation).
		/// </summary>
		/// <param name="vector">Vector being negated. </param>
		/// <returns>Negation of the given vector.</returns>
		public static Vector operator -(Vector vector)
		{
			return new Vector(-vector.X, -vector.Y, -vector.Z);
		}

		/// <summary>
		///     Negates the values of X, Y, and Z on this Vector
		/// </summary>
		public void Negate()
		{
			X = -X;
			Y = -Y;
			Z = -Z;
		}

		/// <summary>
		///     Vector addition.
		/// </summary>
		/// <param name="vector1">First vector being added.</param>
		/// <param name="vector2">Second vector being added.</param>
		/// <returns>Result of addition.</returns>
		public static Vector operator +(Vector vector1, Vector vector2)
		{
			return new Vector(vector1.X + vector2.X,
				vector1.Y + vector2.Y,
				vector1.Z + vector2.Z);
		}

		/// <summary>
		///     Vector addition.
		/// </summary>
		/// <param name="vector1">First vector being added.</param>
		/// <param name="vector2">Second vector being added. </param>
		/// <returns>Result of addition.</returns>
		public static Vector Add(Vector vector1, Vector vector2)
		{
			return new Vector(vector1.X + vector2.X,
				vector1.Y + vector2.Y,
				vector1.Z + vector2.Z);
		}

		/// <summary>
		///     Vector subtraction.
		/// </summary>
		/// <param name="vector1">Vector that is subtracted from.</param>
		/// <param name="vector2">Vector being subtracted.</param>
		/// <returns>Result of subtraction.</returns>
		public static Vector operator -(Vector vector1, Vector vector2)
		{
			return new Vector(vector1.X - vector2.X,
				vector1.Y - vector2.Y,
				vector1.Z - vector2.Z);
		}

		/// <summary>
		///     Vector subtraction.
		/// </summary>
		/// <param name="vector1">Vector that is subtracted from.</param>
		/// <param name="vector2">Vector being subtracted. </param>
		/// <returns>Result of subtraction.</returns>
		public static Vector Subtract(Vector vector1, Vector vector2)
		{
			return new Vector(vector1.X - vector2.X,
				vector1.Y - vector2.Y,
				vector1.Z - vector2.Z);
		}

		/// <summary>
		///     Scalar multiplication.
		/// </summary>
		/// <param name="vector">Vector being multiplied.</param>
		/// <param name="scalar">Scalar value by which the vector is multiplied.</param>
		/// <returns>Result of multiplication.</returns>
		public static Vector operator *(Vector vector, float scalar)
		{
			return new Vector(vector.X * scalar,
				vector.Y * scalar,
				vector.Z * scalar);
		}

		/// <summary>
		///     Scalar multiplication.
		/// </summary>
		/// <param name="vector">Vector being multiplied.</param>
		/// <param name="scalar">Scalar value by which the vector is multiplied.</param>
		/// <returns>Result of multiplication.</returns>
		public static Vector Multiply(Vector vector, float scalar)
		{
			return new Vector(vector.X * scalar,
				vector.Y * scalar,
				vector.Z * scalar);
		}

		/// <summary>
		///     Scalar multiplication.
		/// </summary>
		/// <param name="scalar">Scalar value by which the vector is multiplied.</param>
		/// <param name="vector">Vector being multiplied.</param>
		/// <returns>Result of multiplication.</returns>
		public static Vector operator *(float scalar, Vector vector)
		{
			return new Vector(vector.X * scalar,
				vector.Y * scalar,
				vector.Z * scalar);
		}

		/// <summary>
		///     Scalar multiplication.
		/// </summary>
		/// <param name="scalar">Scalar value by which the vector is multiplied.</param>
		/// <param name="vector">Vector being multiplied.</param>
		/// <returns>Result of multiplication.</returns>
		public static Vector Multiply(float scalar, Vector vector)
		{
			return new Vector(vector.X * scalar,
				vector.Y * scalar,
				vector.Z * scalar);
		}

		/// <summary>
		///     Scalar division.
		/// </summary>
		/// <param name="vector">Vector being divided.</param>
		/// <param name="scalar">Scalar value by which we divide the vector.</param>
		/// <returns>Result of division.</returns>
		public static Vector operator /(Vector vector, float scalar)
		{
			return vector * (1.0f / scalar);
		}

		/// <summary>
		///     Scalar division.
		/// </summary>
		/// <param name="vector">Vector being divided.</param>
		/// <param name="scalar">Scalar value by which we divide the vector.</param>
		/// <returns>Result of division.</returns>
		public static Vector Divide(Vector vector, float scalar)
		{
			return vector * (1.0f / scalar);
		}

		/// <summary>
		///     Vector dot product.
		/// </summary>
		/// <param name="vector1">First vector.</param>
		/// <param name="vector2">Second vector.</param>
		/// <returns>Dot product of two vectors.</returns>
		public static float DotProduct(Vector vector1, Vector vector2)
		{
			return DotProduct(ref vector1, ref vector2);
		}

		/// <summary>
		///     Faster internal version of DotProduct that avoids copies
		///     vector1 and vector2 to a passed by ref for perf and ARE NOT MODIFIED
		/// </summary>
		internal static float DotProduct(ref Vector vector1, ref Vector vector2)
		{
			return vector1.X * vector2.X +
			       vector1.Y * vector2.Y +
			       vector1.Z * vector2.Z;
		}

		/// <summary>
		///     Vector cross product.
		/// </summary>
		/// <param name="vector1">First vector.</param>
		/// <param name="vector2">Second vector.</param>
		/// <returns>Cross product of two vectors.</returns>
		public static Vector CrossProduct(Vector vector1, Vector vector2)
		{
			CrossProduct(ref vector1, ref vector2, out var result);
			return result;
		}

		/// <summary>
		///     Faster internal version of CrossProduct that avoids copies
		///     vector1 and vector2 to a passed by ref for perf and ARE NOT MODIFIED
		/// </summary>
		internal static void CrossProduct(ref Vector vector1, ref Vector vector2, out Vector result)
		{
			result.X = vector1.Y * vector2.Z - vector1.Z * vector2.Y;
			result.Y = vector1.Z * vector2.X - vector1.X * vector2.Z;
			result.Z = vector1.X * vector2.Y - vector1.Y * vector2.X;
		}


		/// <summary>
		///     Computes the angle between two vectors.
		/// </summary>
		/// <param name="vector1">First vector.</param>
		/// <param name="vector2">Second vector.</param>
		/// <returns>
		///     Returns the angle required to rotate vector1 into vector2 in degrees.
		///     This will return a value between [0, 180] degrees.
		///     (Note that this is slightly different from the Vector member
		///     function of the same name.  Signed angles do not extend to 3D.)
		/// </returns>
		public static float AngleBetween(Vector vector1, Vector vector2)
		{
			vector1.Normalize();
			vector2.Normalize();

			var ratio = DotProduct(vector1, vector2);

			// The "straight forward" method of acos(u.v) has large precision
			// issues when the dot product is near +/-1.  This is due to the 
			// steep slope of the acos function as we approach +/- 1.  Slight 
			// precision errors in the dot product calculation cause large
			// variation in the output value. 
			//
			//        |                   |
			//         \__                |
			//            ---___          | 
			//                  ---___    |
			//                        ---_|_ 
			//                            | ---___ 
			//                            |       ---___
			//                            |             ---__ 
			//                            |                  \
			//                            |                   |
			//       -|-------------------+-------------------|-
			//       -1                   0                   1 
			//
			//                         acos(x) 
			// 
			// To avoid this we use an alternative method which finds the
			// angle bisector by (u-v)/2: 
			//
			//                            _>
			//                       u  _-  \ (u-v)/2
			//                        _-  __-v 
			//                      _=__--
			//                    .=-----------> 
			//                            v 
			//
			// Because u and v and unit vectors, (u-v)/2 forms a right angle 
			// with the angle bisector.  The hypotenuse is 1, therefore
			// 2*asin(|u-v|/2) gives us the angle between u and v.
			//
			// The largest possible value of |u-v| occurs with perpendicular 
			// vectors and is sqrt(2)/2 which is well away from extreme slope
			// at +/-1. 
			float theta;
			if (ratio < 0)
				theta = (float) (Math.PI - 2.0f * Math.Asin((-vector1 - vector2).Length / 2.0f));
			else
				theta = (float) (2.0f * Math.Asin((vector1 - vector2).Length / 2.0f));
			return (float) Util.RadToDeg(theta);
		}
	}
}