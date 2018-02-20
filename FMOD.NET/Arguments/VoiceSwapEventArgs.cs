using System;

namespace FMOD.Arguments
{
	/// <inheritdoc />
	/// <summary>
	/// Provides data for <see cref="FMOD.Core.Channel.VirtualVoiceSwapped" /> events.
	/// </summary>
	/// <seealso cref="System.EventArgs" />
	/// <seealso cref="FMOD.Core.Channel" />
	/// <seealso cref="FMOD.Core.Channel.VirtualVoiceSwapped"/>
	public class VoiceSwapEventArgs : EventArgs
	{
		/// <summary>
		/// <para>Gets a value indicating whether this instance is emulated. </para>
		/// <para><c>true</c> when voice is swapped from real to emulated, otherwise <c>false</c> and swapped from emulated to real.</para>
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is emulated; otherwise, <c>false</c>.
		/// </value>
		/// <remarks><c>true</c> when voice is swapped from real to emulated, otherwise <c>false</c> and swapped from emulated to real.</remarks>
		public bool IsEmulated { get; }

		/// <inheritdoc />
		/// <summary>
		/// Initializes a new instance of the <see cref="FMOD.Arguments.VoiceSwapEventArgs" /> class.
		/// </summary>
		/// <param name="isEmulated">If set to <c>true</c> if emulated, otherwise <c>false</c> and real.</param>
		public VoiceSwapEventArgs(bool isEmulated)
		{
			IsEmulated = isEmulated;
		}
	}
}