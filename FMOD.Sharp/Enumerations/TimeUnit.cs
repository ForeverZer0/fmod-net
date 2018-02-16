using System;

namespace FMOD.Enumerations
{
	/// <summary>
	/// List of time types that can be returned by <see cref="M:FMOD.Core.Sound.GetLength"/> and used with 
	/// <see cref="M:FMOD.Core.Channel.SetPosition"/> or <see cref="M:FMOD.Core.Channel.GetPosition"/>.
	/// </summary>
	/// <seealso cref="M:FMOD.Core.Sound.GetLength"/>
	/// <seealso cref="M:FMOD.Core.Channel.GetPosition"/>
	/// <seealso cref="M:FMOD.Core.Channel.SetPosition"/>
	[Flags]
	public enum TimeUnit : uint
	{
		/// <summary>
		/// Milliseconds. 
		/// </summary>
		Ms = 0x00000001,

		/// <summary>
		/// PCM samples, related to milliseconds * samplerate / 1000. 
		/// </summary>
		Pcm = 0x00000002,

		/// <summary>
		/// Bytes, related to PCM samples * channels * datawidth (ie 16bit = 2 bytes).
		/// </summary>
		PcmBytes = 0x00000004,

		/// <summary>
		/// <para>Raw file bytes of (compressed) sound data (does not include headers). </para>
		/// <para>Only used by <see cref="M:FMOD.Core.Sound.GetLength"/> and <see cref="M:FMOD.Core.Channel.GetPosition"/>. </para>
		/// </summary>
		RawBytes = 0x00000008,

		/// <summary>
		/// <para>Fractions of 1 PCM sample. <seealso cref="T:System.UInt32"/> range <c>0</c> to <c>0xFFFFFFFF</c>. </para>
		/// <para>Used for sub-sample granularity for DSP purposes.</para>
		/// </summary>
		PcmFraction = 0x00000010,

		/// <summary>
		/// MOD/S3M/XM/IT. Order in a sequenced module format. Use <see cref="P:FMOD.Core.Sound.Format"/> to determine the PCM format being decoded to. 
		/// </summary>
		ModOrder = 0x00000100,

		/// <summary>
		/// <para>MOD/S3M/XM/IT. Current row in a sequenced module format. </para>
		/// <para>Cannot use with <see cref="M:FMOD.Core.Channel.SetPosition"/>.</para>
		/// <para><see cref="M:FMOD.Core.Sound.GetLength"/> will return the number of rows in the currently playing or seeked to pattern.</para>
		/// </summary>
		ModRow = 0x00000200,

		/// <summary>
		/// MOD/S3M/XM/IT. Current pattern in a sequenced module format.  
		/// <para>Cannot use with <see cref="M:FMOD.Core.Channel.SetPosition"/>.</para>
		/// <para><see cref="M:FMOD.Core.Sound.GetLength"/> will return the number of patterns in the song and <see cref="M:FMOD.Core.Channel.GetPosition"/> will return the currently playing pattern.</para>
		/// </summary>
		ModPattern = 0x00000400,
	}
}																																																																																																												
