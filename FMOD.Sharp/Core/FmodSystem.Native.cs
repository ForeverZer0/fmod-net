using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using FMOD.Enumerations;
using FMOD.Structures;

namespace FMOD.Core
{
	[SuppressUnmanagedCodeSecurity][SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
	public partial class FmodSystem
	{
		#region Methods

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_System_AttachChannelGroupToPort(IntPtr system, uint portType, ulong portIndex,
			IntPtr channelgroup, bool passThru);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_System_AttachFileSystem(IntPtr system, FileOpenCallback useropen,
			FileCloseCallback userclose, FileReadCallback userread, FileSeekCallback userseek);
		
		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_System_Close(IntPtr system);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_System_Create(out IntPtr system);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_System_CreateChannelGroup(IntPtr system, byte[] name, out IntPtr channelgroup);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_System_CreateDSP(IntPtr system, ref DspDescription description, out IntPtr dsp);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_System_CreateDSPByPlugin(IntPtr system, uint handle, out IntPtr dsp);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_System_CreateDSPByType(IntPtr system, DspType type, out IntPtr dsp);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_System_CreateGeometry(IntPtr system, int maxpolygons, int maxvertices,
			out IntPtr geometry);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_System_CreateReverb3D(IntPtr system, out IntPtr reverb);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_System_CreateSound(IntPtr system, byte[] nameOrData, Mode mode,
			ref CreateSoundExInfo exinfo, out IntPtr sound);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_System_CreateSoundGroup(IntPtr system, byte[] name, out IntPtr soundgroup);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_System_CreateStream(IntPtr system, byte[] nameOrData, Mode mode,
			ref CreateSoundExInfo exinfo, out IntPtr sound);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_System_DetachChannelGroupFromPort(IntPtr system, IntPtr channelGroup);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_System_Get3DListenerAttributes(IntPtr system, int listener, out Vector pos,
			out Vector vel, out Vector forward, out Vector up);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_System_Get3DNumListeners(IntPtr system, out int numlisteners);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_System_Get3DSettings(IntPtr system, out float dopplerscale,
			out float distancefactor, out float rolloffscale);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_System_GetAdvancedSettings(IntPtr system, out AdvancedSettings settings);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_System_GetChannel(IntPtr system, int channelid, out IntPtr channel);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_System_GetChannelsPlaying(IntPtr system, out int channels, out int realchannels);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_System_GetCPUUsage(IntPtr system, out float dsp, out float stream,
			out float geometry, out float update, out float total);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_System_GetDefaultMixMatrix(IntPtr system, SpeakerMode sourceSpeakerMode,
			SpeakerMode targetSpeakerMode, float[] matrix, int matrixhop);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_System_GetDriver(IntPtr system, out int driver);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_System_GetDriverInfo(IntPtr system, int id, IntPtr name, int namelen, out Guid guid,
			out int systemrate, out SpeakerMode speakerMode, out int speakerModechannels);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_System_GetDSPBufferSize(IntPtr system, out uint bufferLength, out int numBuffers);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_System_GetDSPInfoByPlugin(IntPtr system, uint handle, out IntPtr description);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_System_GetFileUsage(IntPtr system, out long sampleBytesRead,
			out long streamBytesRead, out long otherBytesRead);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_System_GetGeometryOcclusion(IntPtr system, ref Vector listener, ref Vector source,
			out float direct, out float reverb);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_System_GetGeometrySettings(IntPtr system, out float maxworldsize);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_System_GetMasterChannelGroup(IntPtr system, out IntPtr channelgroup);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_System_GetMasterSoundGroup(IntPtr system, out IntPtr soundgroup);

		[DllImport(Constants.LIBRARY)]
		private static extern Result
			FMOD_System_GetNestedPlugin(IntPtr system, uint handle, int index, out uint nestedhandle);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_System_GetNetworkProxy(IntPtr system, IntPtr proxy, int proxylen);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_System_GetNetworkTimeout(IntPtr system, out int timeout);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_System_GetNumDrivers(IntPtr system, out int numdrivers);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_System_GetNumNestedPlugins(IntPtr system, uint handle, out int count);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_System_GetNumPlugins(IntPtr system, PluginType pluginType, out int numplugins);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_System_GetOutput(IntPtr system, out OutputType output);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_System_GetOutputByPlugin(IntPtr system, out uint handle);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_System_GetOutputHandle(IntPtr system, out IntPtr handle);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_System_GetPluginHandle(IntPtr system, PluginType pluginType, int index,
			out uint handle);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_System_GetPluginInfo(IntPtr system, uint handle, out PluginType pluginType,
			IntPtr name, int namelen, out uint version);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_System_GetRecordDriverInfo(IntPtr system, int id, IntPtr name, int namelen,
			out Guid guid, out int systemRate, out SpeakerMode speakerMode, out int speakerModeChannels, out DriverState state);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_System_GetRecordNumDrivers(IntPtr system, out int numDrivers, out int numConnected);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_System_GetRecordPosition(IntPtr system, int id, out uint position);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_System_GetReverbProperties(IntPtr system, int instance, out ReverbProperties prop);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_System_GetSoftwareChannels(IntPtr system, out int numsoftwarechannels);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_System_GetSoftwareFormat(IntPtr system, out int samplerate,
			out SpeakerMode speakerMode, out int numRawSpeakers);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_System_GetSoundRAM(IntPtr system, out int currentalloced, out int maxalloced,
			out int total);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_System_GetSpeakerModeChannels(IntPtr system, SpeakerMode mode, out int channels);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_System_GetSpeakerPosition(IntPtr system, Speaker speaker, out float x, out float y,
			out bool active);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_System_GetStreamBufferSize(IntPtr system, out uint filebuffersize,
			out TimeUnit filebuffersizetype);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_System_GetUserData(IntPtr system, out IntPtr userdata);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_System_GetVersion(IntPtr system, out uint version);

		[DllImport(Constants.LIBRARY)]
		private static extern Result
			FMOD_System_Init(IntPtr system, int maxchannels, InitFlags flags, IntPtr extradriverdata);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_System_IsRecording(IntPtr system, int id, out bool recording);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_System_LoadGeometry(IntPtr system, IntPtr data, int datasize, out IntPtr geometry);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_System_LoadPlugin(IntPtr system, byte[] filename, out uint handle, uint priority);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_System_LockDSP(IntPtr system);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_System_MixerResume(IntPtr system);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_System_MixerSuspend(IntPtr system);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_System_PlayDSP(IntPtr system, IntPtr dsp, IntPtr channelGroup, bool paused,
			out IntPtr channel);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_System_PlaySound(IntPtr system, IntPtr sound, IntPtr channelGroup, bool paused,
			out IntPtr channel);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_System_RecordStart(IntPtr system, int id, IntPtr sound, bool loop);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_System_RecordStop(IntPtr system, int id);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_System_RegisterCodec(IntPtr system, ref CodecDescription description, out uint handle, uint priority);
		
		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_System_RegisterDSP(IntPtr system, ref DspDescription description, out uint handle);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_System_Set3DListenerAttributes(IntPtr system, int listener, ref Vector pos,
			ref Vector vel, ref Vector forward, ref Vector up);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_System_Set3DNumListeners(IntPtr system, int numlisteners);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_System_Set3DRolloffCallback(IntPtr system, Cb_3DRolloffcallback callback);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_System_Set3DSettings(IntPtr system, float dopplerscale, float distancefactor,
			float rolloffscale);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_System_RegisterOutput(IntPtr system, ref OutputDescription description, out uint handle);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_System_SetAdvancedSettings(IntPtr system, ref AdvancedSettings settings);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_System_SetCallback(IntPtr system, SystemCallback callback,
			SystemCallbackType callbackmask);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_System_SetDriver(IntPtr system, int driver);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_System_SetDSPBufferSize(IntPtr system, uint bufferLength, int numBuffers);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_System_SetFileSystem(IntPtr system, FileOpenCallback useropen,
			FileCloseCallback userclose, FileReadCallback userread, FileSeekCallback userseek,
			FileAsyncReadCallback userasyncread, FileAsyncCancelCallback userasynccancel, int blockalign);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_System_SetGeometrySettings(IntPtr system, float maxworldsize);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_System_SetNetworkProxy(IntPtr system, byte[] proxy);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_System_SetNetworkTimeout(IntPtr system, int timeout);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_System_SetOutput(IntPtr system, OutputType output);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_System_SetOutputByPlugin(IntPtr system, uint handle);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_System_SetPluginPath(IntPtr system, byte[] path);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_System_SetReverbProperties(IntPtr system, int instance, ref ReverbProperties prop);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_System_SetReverbProperties(IntPtr system, int instance, IntPtr prop);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_System_SetReverbProperties(IntPtr system, int instance, int prop);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_System_SetSoftwareChannels(IntPtr system, int numsoftwarechannels);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_System_SetSoftwareFormat(IntPtr system, int samplerate, SpeakerMode speakerMode,
			int numrawspeakers);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_System_SetSpeakerPosition(IntPtr system, Speaker speaker, float x, float y,
			bool active);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_System_SetStreamBufferSize(IntPtr system, uint filebuffersize,
			TimeUnit filebuffersizetype);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_System_SetUserData(IntPtr system, IntPtr userdata);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_System_UnloadPlugin(IntPtr system, uint handle);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_System_UnlockDSP(IntPtr system);

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_System_Update(IntPtr system);

		#endregion
	}
}