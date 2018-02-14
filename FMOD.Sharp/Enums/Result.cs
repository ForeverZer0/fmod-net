﻿using FMOD.Sharp.DSP;
using FMOD.Sharp.Structs;

namespace FMOD.Sharp.Enums
{
	public enum Result
	{
		/// <summary>
		///     No errors.
		/// </summary>
		// ReSharper disable once InconsistentNaming
		OK,

		/// <summary>
		///     Tried to call a function on a data type that does not allow this type of functionality (ie calling
		///     <see cref="Sound.Lock" /> on a streaming sound).
		/// </summary>
		BadCommand,

		/// <summary>
		///     Error trying to allocate a channel.
		/// </summary>
		ChannelAlloc,

		/// <summary>
		///     The specified channel has been reused to play another sound.
		/// </summary>
		ChannelStolen,

		/// <summary>
		///     DMA Failure. See debug output for more information.
		/// </summary>
		Dma,

		/// <summary>
		///     <see cref="DspBase" /> connection error. Connection possibly caused a cyclic dependency or connected dsps with
		///     incompatible buffer counts.
		/// </summary>
		DspConnection,

		/// <summary>
		///     <see cref="DspBase" /> return code from a DSP process query callback. Tells mixer not to call the process callback and
		///     therefore not consume CPU. Use this to optimize the DSP graph.
		/// </summary>
		DspDontProcess,

		/// <summary>
		///     <see cref="DspBase" /> Format error. A DSP unit may have attempted to connect to this network with the wrong format, or
		///     a matrix may have been set with the wrong size if the target unit has a specified channel map.
		/// </summary>
		DspFormat,

		/// <summary>
		///     <see cref="DspBase" /> is already in the mixer's DSP network. It must be removed before being reinserted or released.
		/// </summary>
		DspInUse,

		/// <summary>
		///     <see cref="DspBase" /> connection error. Couldn't find the DSP unit specified.
		/// </summary>
		DspNotFound,

		/// <summary>
		///     <see cref="DspBase" /> operation error. Cannot perform operation on this DSP as it is reserved by the system.
		/// </summary>
		DspReserved,

		/// <summary>
		///     <see cref="DspBase" /> return code from a DSP process query callback. Tells mixer silence would be produced from read,
		///     so go idle and not consume CPU. Use this to optimize the DSP graph.
		/// </summary>
		DspSilence,

		/// <summary>
		///     <see cref="DspBase" /> operation cannot be performed on a DSP of this type.
		/// </summary>
		DspType,

		/// <summary>
		///     Error loading file.
		/// </summary>
		FileBad,

		/// <summary>
		///     Couldn't perform seek operation. This is a limitation of the medium (ie netstreams) or the file format.
		/// </summary>
		FileCouldNotSeek,

		/// <summary>
		///     Media was ejected while reading.
		/// </summary>
		FileDiskEjected,

		/// <summary>
		///     End of file unexpectedly reached while trying to read essential data (truncated?).
		/// </summary>
		FileEof,

		/// <summary>
		///     End of current chunk reached while trying to read data.
		/// </summary>
		FileEndOfData,

		/// <summary>
		///     File not found.
		/// </summary>
		FileNotfound,

		/// <summary>
		///     Unsupported file or audio format.
		/// </summary>
		Format,

		/// <summary>
		///     There is a version mismatch between the FMOD header and either the FMOD Studio library or the FMOD Low Level
		///     library.
		/// </summary>
		HeaderMismatch,

		/// <summary>
		///     A HTTP error occurred. This is a catch-all for HTTP errors not listed elsewhere.
		/// </summary>
		Http,

		/// <summary>
		///     The specified resource requires authentication or is forbidden.
		/// </summary>
		HttpAccess,

		/// <summary>
		///     Proxy authentication is required to access the specified resource.
		/// </summary>
		HttpProxyAuth,

		/// <summary>
		///     A HTTP server error occurred.
		/// </summary>
		HttpServerError,

		/// <summary>
		///     The HTTP request timed out.
		/// </summary>
		HttpTimeout,

		/// <summary>
		///     FMOD was not initialized correctly to support this function.
		/// </summary>
		Initialization,

		/// <summary>
		///     Cannot call this command after <see cref="FmodSystem.Initialize(InitFlags, int)" />
		/// </summary>
		Initialized,

		/// <summary>
		///     An error occurred that wasn't supposed to. Contact support.
		/// </summary>
		Internal,

		/// <summary>
		///     Value passed in was a <c>NaN</c>, infinte, or denormalized float.
		/// </summary>
		InvalidFloat,

		/// <summary>
		///     An invalid object handle was used.
		/// </summary>
		InvalidHandle,

		/// <summary>
		///     An invalid parameter was passed to this function.
		/// </summary>
		InvalidParam,

		/// <summary>
		///     An invalid seek position was passed to this function.
		/// </summary>
		InvalidPosition,

		/// <summary>
		///     An invalid speaker was passed to this function based on the current speaker mode.
		/// </summary>
		InvalidSpeaker,

		/// <summary>
		///     The syncpoint did not come from this sound handle.
		/// </summary>
		InvalidSyncpoint,

		/// <summary>
		///     Tried to call a function on a thread that is not supported.
		/// </summary>
		InvalidThread,

		/// <summary>
		///     The <see cref="Vector" />s passed in are not unit length, or perpendicular.
		/// </summary>
		InvalidVector,

		/// <summary>
		///     Reached maximum audible playback count for this sound's soundgroup.
		/// </summary>
		MaxAudible,

		/// <summary>
		///     Not enough memory or resources.
		/// </summary>
		Memory,

		/// <summary>
		///     Can't use <see cref="Mode.OpenMemoryPoint" /> on non PCM source data, or non mp3/xma/adpcm data if
		///     <see cref="Mode.CreateCompressedSample" /> was used.
		/// </summary>
		MemoryCantPoint,

		/// <summary>
		///     Tried to call a command on a 2D sound when the command was meant for 3D sound.
		/// </summary>
		Needs3D,

		/// <summary>
		///     Tried to use a feature that requires hardware support.
		/// </summary>
		NeedsHardware,

		/// <summary>
		///     Couldn't connect to the specified host.
		/// </summary>
		NetConnect,

		/// <summary>
		///     <para>A socket error occurred.</para>
		///     <para>This is a catch-all for socket-related errors not listed elsewhere. </para>
		/// </summary>
		NetSocketError,

		/// <summary>
		///     The specified URL couldn't be resolved.
		/// </summary>
		NetUrl,

		/// <summary>
		///     Operation on a non-blocking socket could not complete immediately.
		/// </summary>
		NetWouldBlock,

		/// <summary>
		///     Operation could not be performed because specified sound/DSP connection is not ready.
		/// </summary>
		NotReady,

		/// <summary>
		///     Error initializing output device, but more specifically, the output device is already in use and cannot be reused.
		/// </summary>
		OutputAllocated,

		/// <summary>
		///     Error creating hardware sound buffer.
		/// </summary>
		OutputCreateBuffer,

		/// <summary>
		///     A call to a standard soundcard driver failed, which could possibly mean a bug in the driver or resources were
		///     missing or exhausted.
		/// </summary>
		OutputDriverCall,

		/// <summary>
		///     Soundcard does not support the specified format.
		/// </summary>
		OutputFormat,

		/// <summary>
		///     Error initializing output device.
		/// </summary>
		OutputInit,

		/// <summary>
		///     <para>The output device has no drivers installed. </para>
		///     <para>If pre-init, <see cref="OutputType.NoSound" /> is selected as the output mode.</para>
		///     <para>If post-init, the function just fails.</para>
		/// </summary>
		OutputNoDrivers,

		/// <summary>
		///     An unspecified error has been returned from a plugin.
		/// </summary>
		Plugin,

		/// <summary>
		///     A requested output, dsp unit type or codec was not available.
		/// </summary>
		PluginMissing,

		/// <summary>
		///     A resource that the plugin requires cannot be found. (ie the DLS file for MIDI playback)
		/// </summary>
		PluginResource,

		/// <summary>
		///     A plugin was built with an unsupported SDK version.
		/// </summary>
		PluginVersion,

		/// <summary>
		///     An error occurred trying to initialize the recording device.
		/// </summary>
		Record,

		/// <summary>
		///     Reverb properties cannot be set on this channel because a parent channelgroup owns the reverb connection.
		/// </summary>
		ReverbChannelgroup,

		/// <summary>
		///     <para>Specified instance in <see cref="ReverbProperties" /> couldn't be set.</para>
		///     <para>Most likely because it is an invalid instance number or the reverb doesn't exist.</para>
		/// </summary>
		ReverbInstance,

		/// <summary>
		///     <para>
		///         The error occurred because the sound referenced contains subsounds when it shouldn't have, or it doesn't
		///         contain subsounds when it should have.
		///     </para>
		///     <para>The operation may also not be able to be performed on a parent sound.</para>
		/// </summary>
		Subsounds,

		/// <summary>
		///     This subsound is already being used by another sound, you cannot have more than one parent to a sound. Null out the
		///     other parent's entry first.
		/// </summary>
		SubsoundAllocated,

		/// <summary>
		///     Shared subsounds cannot be replaced or moved from their parent stream, such as when the parent stream is an FSB
		///     file.
		/// </summary>
		SubsoundCantMove,

		/// <summary>
		///     The specified tag could not be found or there are no tags.
		/// </summary>
		TagNotFound,

		/// <summary>
		///     The sound created exceeds the allowable input channel count. This can be increased using the 'maxinputchannels'
		///     parameter in System::setSoftwareFormat.  // TODO: fix comment
		/// </summary>
		TooManyChannels,

		/// <summary>
		///     The retrieved string is too long to fit in the supplied buffer and has been truncated.
		/// </summary>
		Truncated,

		/// <summary>
		///     Something in FMOD hasn't been implemented when it should be! contact support!
		/// </summary>
		Unimplemented,

		/// <summary>
		///     This command failed because System::init or System::setDriver was not called. // TODO: Fix comment
		/// </summary>
		Uninitialized,

		/// <summary>
		///     A command issued was not supported by this object. Possibly a plugin without certain callbacks specified.
		/// </summary>
		Unsupported,

		/// <summary>
		///     The version number of this file format is not supported.
		/// </summary>
		Version,

		/// <summary>
		///     The specified bank has already been loaded.
		/// </summary>
		EventAlreadyLoaded,

		/// <summary>
		///     The live update connection failed due to the game already being connected.
		/// </summary>
		EventLiveUpdateBusy,

		/// <summary>
		///     The live update connection failed due to the game data being out of sync with the tool.
		/// </summary>
		EventLiveUpdateMismatch,

		/// <summary>
		///     The live update connection timed out.
		/// </summary>
		EventLiveUpdateTimeout,

		/// <summary>
		///     The requested event, bus or vca could not be found.
		/// </summary>
		EventNotFound,

		/// <summary>
		///     The Studio::System object is not yet initialized.
		/// </summary>
		StudioUninitialized,

		/// <summary>
		///     The specified resource is not loaded, so it can't be unloaded.
		/// </summary>
		StudioNotLoaded,

		/// <summary>
		///     An invalid string was passed to this function.
		/// </summary>
		InvalidString,

		/// <summary>
		///     The specified resource is already locked.
		/// </summary>
		AlreadyLocked,

		/// <summary>
		///     The specified resource is not locked, so it can't be unlocked.
		/// </summary>
		NotLocked,

		/// <summary>
		///     The specified recording driver has been disconnected.
		/// </summary>
		RecordDisconnected,

		/// <summary>
		///     The length provided exceeds the allowable limit.
		/// </summary>
		TooManySamples
	}
}