using FMOD.Sharp.Enums;

namespace FMOD.Sharp.Data
{
	/// <summary>
	/// Defines a beginning and end points for looping sound.
	/// </summary>
	/// <seealso cref="Channel.GetLoopPoints(TimeUnit)"/>
	/// <seealso cref="Channel.SetLoopPoints(LoopPoints)"/>
	/// <seealso cref="TimeUnit"/>
	public class LoopPoints
	{
		/// <summary>
		/// <para>Gets or sets the loop start point, this point in time is played so it is inclusive.</para>
		/// <para>The format for this time is defined by <see cref="StartTimeUnit"/>.</para>
		/// <para>Specify <c>0</c> to have this value ignored.</para>
		/// </summary>
		/// <value>
		/// The loop start.
		/// </value>
		public uint LoopStart { get; set; }

		/// <summary>
		/// <para>Gets or sets the loop end point, this point in time is played so it is inclusive.</para>
		/// <para>The format for this time is defined by <see cref="EndTimeUnit"/>.</para>
		/// <para>Specify <c>0</c> to have this value ignored.</para>
		/// </summary>
		/// <value>
		/// The loop end.
		/// </value>
		public uint LoopEnd { get; set; }

		/// <summary>
		/// Gets or sets the time format used for the loop start point.
		/// </summary>
		/// <value>
		/// The start time unit.
		/// </value>
		public TimeUnit StartTimeUnit { get; set; }

		/// <summary>
		/// Gets or sets the time format used for the loop end point.
		/// </summary>
		/// <value>
		/// The end time unit.
		/// </value>
		public TimeUnit EndTimeUnit { get; set; }
	}
}
