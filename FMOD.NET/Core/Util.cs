#region License

// Util.cs is distributed under the Microsoft Public License (MS-PL)
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

#endregion

namespace FMOD.NET.Core
{
	public static class Util
	{
		/// <summary>
		///     <para>Factor for conversion from radians to degrees.</para>
		///     <para>
		///         <c>180.0 / Math.PI</c>
		///     </para>
		/// </summary>
		public const double DEGREE_FACTOR = 180.0 / Math.PI;

		/// <summary>
		///     <para>Factor for conversion from degrees to radians.</para>
		///     <para>
		///         <c>Math.PI / 180.0</c>
		///     </para>
		/// </summary>
		public const double RADIAN_FACTOR = Math.PI / 180.0;

		#region Methods

		/// <summary>
		///     Clamps the value between the specified <paramref name="minimim" /> and <paramref name="maximum" /> and returns it.
		/// </summary>
		/// <typeparam name="T">A numerical value type.</typeparam>
		/// <param name="value">The value to clamp.</param>
		/// <param name="minimim">The minimum permissible value to return.</param>
		/// <param name="maximum">The maximum permissible value to return.</param>
		/// <returns></returns>
		public static T Clamp<T>(this T value, T minimim, T maximum) where T :
			struct,
			IComparable,
			IComparable<T>,
			IConvertible,
			IEquatable<T>,
			IFormattable
		{
			if (value.CompareTo(minimim) < 0)
				return minimim;
			return value.CompareTo(maximum) > 0 ? maximum : value;
		}

		/// <summary>
		///     Helper function to convert a angle in degrees to radians and perform <i>cos</i> function on it.
		/// </summary>
		/// <param name="angle">The angle, in degrees.</param>
		/// <returns>The result of the conversion.</returns>
		/// <seealso cref="DegToRad" />
		/// <seealso cref="SinFromAngle" />
		public static float CosFromAngle(float angle)
		{
			return (float) Math.Cos(RADIAN_FACTOR * angle);
		}

		/// <summary>
		///     Converts an angle represented in degrees to radians.
		/// </summary>
		/// <param name="angle">The angle, in degrees.</param>
		/// <returns>The converted value to radians.</returns>
		/// <seealso cref="RadToDeg" />
		/// <seealso cref="SinFromAngle" />
		/// <seealso cref="CosFromAngle" />
		public static double DegToRad(double angle)
		{
			return RADIAN_FACTOR * angle;
		}

		/// <summary>
		///     Determines whether the specified value is within the specified bounds.
		/// </summary>
		/// <typeparam name="T">A numerical value type.</typeparam>
		/// <param name="value">The value to compare.</param>
		/// <param name="lowerBound">The lower threshold to check.</param>
		/// <param name="upperBound">The upper threshold to check.</param>
		/// <returns>
		///     <c>true</c> if the specified lower bound is within the bounds; otherwise, <c>false</c>.
		/// </returns>
		public static bool IsBetween<T>(this T value, T lowerBound, T upperBound) where T :
			struct,
			IComparable,
			IComparable<T>,
			IConvertible,
			IEquatable<T>,
			IFormattable
		{
			return value.CompareTo(lowerBound) >= 0 && value.CompareTo(upperBound) <= 0;
		}

		/// <summary>
		///     Converts an angle represented in radians to degrees.
		/// </summary>
		/// <param name="angle">The angle, in radians.</param>
		/// <returns>The converted value to degrees.</returns>
		/// <seealso cref="DegToRad" />
		public static double RadToDeg(double angle)
		{
			return angle * DEGREE_FACTOR;
		}

		/// <summary>
		///     Helper function to convert a angle in degrees to radians and perform <i>sin</i> function on it.
		/// </summary>
		/// <param name="angle">The angle, in degrees.</param>
		/// <returns>The result of the conversion.</returns>
		/// <seealso cref="DegToRad" />
		/// <seealso cref="CosFromAngle" />
		public static float SinFromAngle(float angle)
		{
			return (float) Math.Sin(RADIAN_FACTOR * angle);
		}

		/// <summary>
		///     Converts <b>FMOD</b>'s method of a <c>uint</c> version to a <see cref="Version" />.
		/// </summary>
		/// <param name="version">The <b>FMOD</b> style version.</param>
		/// <returns>A newly created <see cref="Version" /> representation of the specified value.</returns>
		public static Version UInt32ToVersion(uint version)
		{
			var str = version.ToString("X8");
			var major = Int32.Parse(str.Substring(0, 4));
			var minor = Int32.Parse(str.Substring(4, 2));
			var build = Int32.Parse(str.Substring(6, 2));
			return new Version(major, minor, build);
		}

		#endregion
	}
}