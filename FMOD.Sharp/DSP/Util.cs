using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FMOD.Sharp.DSP
{
	public static class Util
	{
		
		

		/// <summary>
		/// Converts an angle represented in degrees to radians.
		/// </summary>
		/// <param name="angle">The angle, in degrees.</param>
		/// <returns>The converted value to radians.</returns>
		public static double DegToRad(double angle)
		{
			return RAD_FACTOR * angle;
		}
		public const double RADIAN_FACTOR = Math.PI / 180.0;

		public static float SinFromAngle(float angle)
		{
			return (float) Math.Sin(RADIAN_FACTOR * angle);
		}

		public static float CosFromAngle(float angle)
		{
			return (float) Math.Cos(RADIAN_FACTOR * angle);
		}



	}
}
