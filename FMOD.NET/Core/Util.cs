using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Resources;
using FMOD.Enumerations;

namespace FMOD.Core
{
	public static class Util
	{
		public const double RADIAN_FACTOR = Math.PI / 180.0;
		
		/// <summary>
		/// Converts an angle represented in degrees to radians.
		/// </summary>
		/// <param name="angle">The angle, in degrees.</param>
		/// <returns>The converted value to radians.</returns>
		public static double DegToRad(double angle)
		{
			return RADIAN_FACTOR * angle;
		}

		public static double RadToDeg(double angle)
		{
			return angle * (180.0 / Math.PI);
		}

		public static float SinFromAngle(float angle)
		{
			return (float) Math.Sin(RADIAN_FACTOR * angle);
		}

		public static float CosFromAngle(float angle)
		{
			return (float) Math.Cos(RADIAN_FACTOR * angle);
		}

		public static Version UInt32ToVersion(uint version)
		{
			var str = version.ToString("X8");
			var major = Int32.Parse(str.Substring(0, 4));
			var minor = Int32.Parse(str.Substring(4, 2));
			var build = Int32.Parse(str.Substring(6, 2));
			return new Version(major, minor, build);
		}

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


		public static T Clamp<T>(this T value, T min, T max) where T : 
			struct, 
			IComparable, 
			IComparable<T>, 
			IConvertible, 
			IEquatable<T>, 
			IFormattable
		{
			if (value.CompareTo(min) < 0) 
				return min;
			return value.CompareTo(max) > 0 ? max : value;
		}

		private const BindingFlags BINDING_FLAGS = BindingFlags.NonPublic | BindingFlags.Instance;
		private static readonly ResourceManager _resxManager;
		private static readonly Dictionary<IntPtr, HandleBase> _handles;

		static Util()
		{
			_handles = new Dictionary<IntPtr, HandleBase>();
			_resxManager = new ResourceManager("FMOD.NET.ResultStrings", Assembly.GetExecutingAssembly());
		}

		public static string GetResultString(string resultName)
		{
			// TODO: FIX
			var str = _resxManager.GetString(resultName, CultureInfo.CurrentCulture);
			return String.IsNullOrEmpty(str) ? "Unknown Error" : str;
		}

		public static string GetResultString(Result result)
		{
			return GetResultString(Enum.GetName(typeof(Result), result));
		}
	}
}
