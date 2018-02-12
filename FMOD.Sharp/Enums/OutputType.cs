using System;

namespace FMOD.Sharp.Enums
{
	/// <summary>
	/// Specifies the output types used with an <see cref="FmodSystem"/>.
	/// </summary>
	/// <remarks>
	/// <para>To pass information to the driver when initializing <b>FMOD</b> use the <i>extraDriverData</i> parameter in <see cref="FmodSystem.Initialize(InitFlags, IntPtr, int, bool)"/> for the following reasons.</para>
	/// <para>
	/// <list class="bullet">
	/// <listItem><para>
	/// <see cref="WavWriter"/> - <i>extraDriverData</i> parameter of <see cref="FmodSystem.Initialize(InitFlags, IntPtr, int, bool)"/> is a pointer to a char * file name that the wav writer will output to.
	/// </para></listItem>
	/// <see cref="WavWriterNrt"/> - <i>extraDriverData</i> parameter of <see cref="FmodSystem.Initialize(InitFlags, IntPtr, int, bool)"/> is a pointer to a char * file name that the wav writer will output to.
	/// <listItem><para>
	/// <see cref="DSound"/> - <i>extraDriverData</i> parameter of <see cref="FmodSystem.Initialize(InitFlags, IntPtr, int, bool)"/> is cast to a HWND type, so that FMOD can set the focus on the audio for a particular window. 
	/// </para></listItem>
	/// </list>
	/// </para>
	/// <para>Currently these are the only <b>FMOD</b> drivers that take extra information. Other unknown plugins may have different requirements.</para>
	/// <alert class="note">
	/// <para>If <see cref="WavWriterNrt"/> or <see cref="NoSoundNrt"/> are used, and if the <see cref="FmodSystem.Update"/> function is being called very quickly (ie for a non realtime decode) it may be being called too quickly for the FMOD streamer thread to respond to. The result will be a skipping/stuttering output in the captured audio.</para>
	/// </alert>
	/// <para>To remedy this, disable the <b>FMOD</b> streamer thread, and use <see cref="InitFlags.StreamFromUpdate"/> to avoid skipping in the output stream, as it will lock the mixer and the streamer together in the same thread.</para>
	/// </remarks>
	/// <seealso cref="FmodSystem.Output"/>
	/// <seealso cref="FmodSystem.Initialize(InitFlags, IntPtr, int, bool)"/>
	/// <seealso cref="FmodSystem.Update"/>
	public enum OutputType
	{
		/// <summary>
		/// <para>Picks the best output mode for the platform.</para>
		/// <para>This is the default.</para> 
		/// </summary>
		AutoDetect,
		/// <summary>
		/// <para><b>All Platforms</b></para>
		/// <para>3rd Party Plugins/Unknown.</para>
		/// <para>This is value is not to be set manually via the <see cref="FmodSystem.Output"/> property.</para>
		/// </summary>
		Unknown,
		/// <summary>
		/// <para><b>All Platforms</b></para>
		/// <para>Perform all mixing but discard the final output.</para> 
		/// </summary>
		NoSound,
		/// <summary>
		/// <para><b>All Platforms</b></para>
		/// <para>Writes output to a .wav file.</para>
		/// </summary>
		WavWriter,
		/// <summary>
		/// <para><b>All Platforms</b></para>
		/// <para>Non-realtime version of <see cref="NoSound"/>.</para>
		/// <para>User can drive mixer with <see cref="FmodSystem.Update"/> at whatever rate they want.</para>
		/// </summary>
		NoSoundNrt,
		/// <summary>
		/// <para><b>All Platforms</b></para>
		/// <para>Non-realtime version of <see cref="WavWriter"/>.</para>
		/// <para>User can drive mixer with <see cref="FmodSystem.Update"/> at whatever rate they want.</para>
		/// </summary>
		WavWriterNrt,
		/// <summary>
		/// <para><b>Windows</b></para>
		/// <para>Direct Sound.</para>
		/// <para>(Default on Windows XP and below)</para>
		/// </summary>
		DSound,
		/// <summary>
		/// <para><b>Windows</b></para>
		/// <para>Windows Multimedia.</para>
		/// </summary>
		Winmm,
		/// <summary>
		/// <para><b>Windows / Windows Store / Xbox One</b></para>
		/// <para>Windows Audio Session API.</para>
		/// <para>(Default on Windows Vista and above, Xbox One and Windows Store Applications)</para>
		/// </summary>
		Wasapi,
		/// <summary>
		/// <para><b>Windows</b></para>
		/// <para>Low latency ASIO 2.0.</para>
		/// </summary>
		Asio,
		/// <summary>
		/// <para><b>Linux</b></para>
		/// <para>Pulse Audio.</para>
		/// <para>(Default on Linux if available)</para>
		/// </summary>
		PulseAudio,
		/// <summary>
		/// <para><b>Linux</b></para>
		/// <para>Advanced Linux Sound Architecture.</para>
		/// <para>(Default on Linux if <see cref="PulseAudio"/> isn't available)</para>
		/// </summary>
		Alsa,
		/// <summary>
		/// <para><b>Mac / iOS</b></para>
		/// <para>Core Audio.</para>
		/// <para>(Default on Mac and iOS)</para>
		/// </summary>
		CoreaAudio,
		/// <summary>
		/// <para><b>Xbox 360</b></para>
		/// <para>XAudio.</para>
		/// <para>(Default on Xbox 360)</para>
		/// </summary>
		Xaudio,
		/// <summary>
		/// <para><b>PlayStation 3</b></para>
		/// <para>Audio Out.</para>
		/// <para>(Default on PlayStation 3)</para>
		/// </summary>
		PS3,
		/// <summary>
		/// <para><b>Android</b></para>
		/// <para>Java Audio Track.</para>
		/// <para>(Default on Android 2.2 and below)</para>
		/// </summary>
		AudioTrack,
		/// <summary>
		/// <para><b>Android</b></para>
		/// <para>OpenSL ES.</para>
		/// <para>(Default on Android 2.3 and above)</para>
		/// </summary>
		OpenSl,
		/// <summary>
		/// <para><b>Wii U</b></para>
		/// <para>AX.</para>
		/// <para>(Default on Wii U)</para>
		/// </summary>
		WiiU,
		/// <summary>
		/// <para><b>PS4 / PSVita</b></para>
		/// <para>Audio Out.</para>
		/// <para>(Default on PlayStation 4 and PlayStation Vita)</para>
		/// </summary>
		AudioOut,
		/// <summary>
		/// <para><b>PS4</b></para>
		/// <para>Audio3D.</para>
		/// </summary>
		Audio3D,
		/// <summary>
		/// <para><b>Windows</b></para>
		/// <para>Dolby Atmos</para>
		/// <para>(WASAPI)</para>
		/// </summary>
		Atmos,
		/// <summary>
		/// <para><b>Web Browser</b></para>
		/// <para>JavaScript webaudio output.</para>
		/// <para>(Default on JavaScript)</para>
		/// </summary>
		WebAudio,
		/// <summary>
		/// <para><b>NX</b></para>
		/// <para>NX nn::audio.</para>
		/// <para>(Default on NX)</para>
		/// </summary>
		NnAudio,
		/// <summary>
		/// <para><b>Windows 10 / Xbox One </b></para>
		/// <para>Windows Sonic.</para>
		/// </summary>
		WinSonic,
		/// <summary>
		/// Maximum number of output types supported. 
		/// </summary>
		Max 
	}
}