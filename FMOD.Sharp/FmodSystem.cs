using System;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using FMOD.Sharp.Data;

namespace FMOD.Sharp
{
	using Enums;
	using Structs;

	public partial class FmodSystem : Handle
	{
		public int UPDATE_FREQUENCY = 1000 / 60;

		private Timer _updateTimer;


		public event EventHandler OutputChanged;

		public event EventHandler UserDataChanged;

		public static FmodSystem Create()
		{
			NativeInvoke(FMOD_System_Create(out var pointer));
			var system = new FmodSystem(pointer);
			Core.AddReference(pointer, system);
			return system;
		}

		private FmodSystem(IntPtr handle) : base(handle)
		{
		}

		public void RecordStart(int driverId, Sound sound, bool loop = false)
		{
			NativeInvoke(FMOD_System_RecordStart(this, driverId, sound, loop));
			RecordingStarted?.Invoke(this, EventArgs.Empty);
		}

		public event EventHandler RecordingStarted;
		public event EventHandler RecordingStopped;

		public void RecordStop(int driverId)
		{
			NativeInvoke(FMOD_System_RecordStop(this, driverId));
			RecordingStopped?.Invoke(this, EventArgs.Empty);
		}

		public bool IsRecording(int driverId)
		{
			NativeInvoke(FMOD_System_IsRecording(this, driverId, out var recording));
			return recording;
		}

		public uint GetRecordPosition(int driverId)
		{
			NativeInvoke(FMOD_System_GetRecordPosition(this, driverId, out var position));
			return position;
		}

		public int RecordDriverCount
		{
			get
			{
				NativeInvoke(FMOD_System_GetRecordNumDrivers(this, out var driverCount, out var dummy));
				return driverCount;
			}
		}

		public int ConnectedRecordDriverCount
		{
			get
			{
				NativeInvoke(FMOD_System_GetRecordNumDrivers(this, out var dummy, out var connected));
				return connected;
			}
		}

		public Driver GetRecordDriver(int id)
		{
			using (var buffer = new MemoryBuffer(512))
			{
				NativeInvoke(FMOD_System_GetRecordDriverInfo(this, id, buffer.Pointer, 512, out var guid, out var rate,
					out var speakerMode, out var channels, out var state));
				return new Driver
				{
					Id = id,
					Guid = guid,
					Name = buffer.ToString(Encoding.UTF8),
					SpeakerMode = speakerMode,
					SpeakerModeChannels = channels,
					State = state,
					SystemRate = rate
				};
			}
		}

		public event EventHandler GeometryCreated;
		public Geometry CreateGeometry(int maxPolygons, int maxVertices)
		{
			NativeInvoke(FMOD_System_CreateGeometry(this, maxPolygons, maxVertices, out var geometry));
			GeometryCreated?.Invoke(this, EventArgs.Empty);
			return Core.Create<Geometry>(geometry);
		}

		public event EventHandler WorldSizeChanged;

		public float WorldSize
		{
			get
			{
				NativeInvoke(FMOD_System_GetGeometrySettings(this, out var worldSize));
				return worldSize;
			}
			set
			{
				NativeInvoke(FMOD_System_SetGeometrySettings(this, value));
				WorldSizeChanged?.Invoke(this, EventArgs.Empty);
			}
		}

		/// <summary>
		/// Gets or sets a user value that the <see cref="FmodSystem"/> object will store internally. 
		/// </summary>
		/// <value>
		/// The user data.
		/// </value>
		/// <remarks>This function is primarily used in case the user wishes to "attach" data to an <b>FMOD</b> object.</remarks>
		public IntPtr UserData
		{
			get
			{
				NativeInvoke(FMOD_System_GetUserData(this, out var data));
				return data;
			}
			set
			{
				NativeInvoke(FMOD_System_SetUserData(this, value));
				UserDataChanged?.Invoke(this, EventArgs.Empty);
			}
		}

		public event EventHandler AdvancedSettingsChanged;

		public AdvancedSettings AdvancedSettings
		{
			get
			{
				NativeInvoke(FMOD_System_GetAdvancedSettings(this, out var settings));
				return settings;
			}
			set
			{
				NativeInvoke(FMOD_System_SetAdvancedSettings(this, ref value));
				AdvancedSettingsChanged?.Invoke(this, EventArgs.Empty);
			}
		}

		public OutputType Output
		{
			get
			{
				NativeInvoke(FMOD_System_GetOutput(this, out var output));
				return output;
			}
			set
			{
				NativeInvoke(FMOD_System_SetOutput(this, value));
				OutputChanged?.Invoke(this, EventArgs.Empty);
			}
		}

		private void SelfUpdate(object state)
		{
			Update();
		}

		public void Initialize(InitFlags flags = InitFlags.Normal, bool selfUpdate = true,
			int maxChannels = Core.MAX_CHANNEL_WIDTH, IntPtr? extradriverdata = null)
		{
			var max = maxChannels.Clamp(1, Core.MAX_CHANNEL_WIDTH);
			NativeInvoke(FMOD_System_Init(this, max, flags, extradriverdata ?? IntPtr.Zero));
			// TODO: Do this better
			if (selfUpdate)
			{
				_updateTimer = new Timer(SelfUpdate, null, 0, UPDATE_FREQUENCY);
			}
		}

		public event EventHandler Closed;

		public void Close()
		{
			NativeInvoke(FMOD_System_Close(this));
			Closed?.Invoke(this, EventArgs.Empty);
		}

		public override void Dispose()
		{
			_updateTimer?.Dispose();
			NativeInvoke(FMOD_System_Release(this));
			base.Dispose();
		}

		public void Update()
		{
			NativeInvoke(FMOD_System_Update(this));
		}

		public void SuspendMixer()
		{
			NativeInvoke(FMOD_System_MixerSuspend(this));
			MixerSuspended?.Invoke(this, EventArgs.Empty);
		}

		public event EventHandler MixerSuspended;
		public event EventHandler MixerResumed;

		public void ResumeMixer()
		{
			NativeInvoke(FMOD_System_MixerResume(this));
			MixerResumed?.Invoke(this, EventArgs.Empty);
		}


		public Sound CreateSound(string source, Mode mode = Mode.Default)
		{
			var stringData = Encoding.UTF8.GetBytes(source + Char.MinValue);
			var exinfo = new CreateSoundExInfo();
			exinfo.cbsize = Marshal.SizeOf(exinfo);
			SoundCreated?.Invoke(this, EventArgs.Empty);
			return CreateSound(stringData, mode, ref exinfo);
		}

		public Sound CreateSound(string source, Mode mode, ref CreateSoundExInfo exinfo)
		{
			var stringData = Encoding.UTF8.GetBytes(source + Char.MinValue);
			exinfo.cbsize = Marshal.SizeOf(exinfo);
			SoundCreated?.Invoke(this, EventArgs.Empty);
			return CreateSound(stringData, mode, ref exinfo);
		}

		public Sound CreateSound(byte[] source, Mode mode = Mode.Default)
		{
			var exinfo = new CreateSoundExInfo();
			exinfo.cbsize = Marshal.SizeOf(exinfo);
			SoundCreated?.Invoke(this, EventArgs.Empty);
			return CreateSound(source, mode, ref exinfo);
		}

		public Sound CreateSound(byte[] source, Mode mode, ref CreateSoundExInfo exinfo)
		{
			exinfo.cbsize = Marshal.SizeOf(exinfo);
            NativeInvoke(FMOD_System_CreateSound(this, source, mode, ref exinfo, out var sound));
			SoundCreated?.Invoke(this, EventArgs.Empty);
			return Core.Create<Sound>(sound);
		}

		public Sound CreateStream(string source, Mode mode = Mode.Default)
		{
			var stringData = Encoding.UTF8.GetBytes(source + Char.MinValue);
			var exinfo = new CreateSoundExInfo();
			exinfo.cbsize = Marshal.SizeOf(exinfo);
			SoundCreated?.Invoke(this, EventArgs.Empty);
			return CreateSound(stringData, mode, ref exinfo);
		}

		public Sound CreateStream(string source, Mode mode, ref CreateSoundExInfo exinfo)
		{
			var stringData = Encoding.UTF8.GetBytes(source + Char.MinValue);
			exinfo.cbsize = Marshal.SizeOf(exinfo);
			SoundCreated?.Invoke(this, EventArgs.Empty);
			return CreateStream(stringData, mode, ref exinfo);
		}

		public Sound CreateStream(byte[] source, Mode mode = Mode.Default)
		{
			var exinfo = new CreateSoundExInfo();
			exinfo.cbsize = Marshal.SizeOf(exinfo);
			SoundCreated?.Invoke(this, EventArgs.Empty);
			return CreateStream(source, mode, ref exinfo);
		}

		public Sound CreateStream(byte[] source, Mode mode, ref CreateSoundExInfo exinfo)
		{
			exinfo.cbsize = Marshal.SizeOf(exinfo);
			NativeInvoke(FMOD_System_CreateStream(this, source, mode, ref exinfo, out var sound));
			SoundCreated?.Invoke(this, EventArgs.Empty);
			return Core.Create<Sound>(sound);
		}

		public event EventHandler DspCreated;
		public event EventHandler SoundCreated;
		public event EventHandler ReverbCreated;

		public Dsp CreateDspByType(DspType dspType)
		{
			NativeInvoke(FMOD_System_CreateDSPByType(this, dspType, out var dsp));
			DspCreated?.Invoke(this, EventArgs.Empty);
			return Dsp.FromType(dsp, dspType);
		}

		public Reverb3D CreateReverb3D()
		{
			NativeInvoke(FMOD_System_CreateReverb3D(this, out var reverb));
			ReverbCreated?.Invoke(this, EventArgs.Empty);
			return Core.Create<Reverb3D>(reverb);
		}

		public void ClearReverb(int index)
		{
			NativeInvoke(FMOD_System_SetReverbProperties(this, index, 0));
		}

		public event EventHandler SoundPlayed;
		public event EventHandler DspPlayed;

		public Channel PlaySound(Sound sound, bool paused = false, ChannelGroup group = null)
		{
			NativeInvoke(FMOD_System_PlaySound(this, sound, group ?? IntPtr.Zero, paused, out var channel));
			SoundPlayed?.Invoke(this, EventArgs.Empty);
			return Core.Create<Channel>(channel);
		}

		public Channel PlayDsp(Dsp dsp, bool paused = false, ChannelGroup group = null)
		{
			NativeInvoke(FMOD_System_PlayDSP(this, dsp, group ?? IntPtr.Zero, paused, out var channel));
			DspPlayed?.Invoke(this, EventArgs.Empty);
			return Core.Create<Channel>(channel);
		}

		public void LockDsp()
		{
			NativeInvoke(FMOD_System_LockDSP(this));
			DspLocked?.Invoke(this, EventArgs.Empty);
		}

		public void UnlockDsp()
		{
			NativeInvoke(FMOD_System_UnlockDSP(this));
			DspUnlocked?.Invoke(this, EventArgs.Empty);
		}

		public event EventHandler DspLocked;
		public event EventHandler DspUnlocked;
		public event EventHandler ReverbPropertiesChanged;

		public void SetReverbProperties(int index, ReverbProperties properties)
		{
			NativeInvoke(FMOD_System_SetReverbProperties(this, index, ref properties));
			ReverbPropertiesChanged?.Invoke(this, EventArgs.Empty);
		}

		public ReverbProperties GetReverbProperties(int index)
		{
			NativeInvoke(FMOD_System_GetReverbProperties(this, index, out var properties));
			return properties;
		}

		public Channel GetChannel(int index)
		{
			NativeInvoke(FMOD_System_GetChannel(this, index, out var channel));
			return Core.Create<Channel>(channel);
		}

		public event EventHandler NetworkTimeoutChanged;
		public event EventHandler NetworkProxyChanged;

		public int NetworkTimeout
		{
			get
			{
				NativeInvoke(FMOD_System_GetNetworkTimeout(this, out var timeout));
				return timeout;
			}
			set
			{
				NativeInvoke(FMOD_System_SetNetworkTimeout(this, Math.Max(1, value)));
				NetworkTimeoutChanged?.Invoke(this, EventArgs.Empty);
			}
		}

		public string NetworkProxy
		{
			get
			{
				using (var buffer = new MemoryBuffer(512))
				{
					NativeInvoke(FMOD_System_GetNetworkProxy(this, buffer.Pointer, 512));
					return buffer.ToString(Encoding.UTF8);
				}
			}
			set
			{
				var bytes = Encoding.UTF8.GetBytes(value);
				NativeInvoke(FMOD_System_SetNetworkProxy(this, bytes));
				NetworkProxyChanged?.Invoke(this, EventArgs.Empty);
			}
		}

		public ChannelGroup MasterChannelGroup
		{
			get
			{
				NativeInvoke(FMOD_System_GetMasterChannelGroup(this, out var channelGroup));
				return Core.Create<ChannelGroup>(channelGroup);
			}
		}

		public SoundGroup MasterSoundGroup
		{
			get
			{
				NativeInvoke(FMOD_System_GetMasterSoundGroup(this, out var soundGroup));
				return Core.Create<SoundGroup>(soundGroup);
			}
		}

		public int SoftwareChannels
		{
			get
			{
				NativeInvoke(FMOD_System_GetSoftwareChannels(this, out var channels));
				return channels;
			}
			set
			{
				NativeInvoke(FMOD_System_SetSoftwareChannels(this, value));
				SoftwareChannelsChanged?.Invoke(this, EventArgs.Empty);
			}
		}


		public event EventHandler ChannelGroupCreated;
		public event EventHandler SoundGroupCreated;
		public event EventHandler SelectedDriverChanged;
		public event EventHandler SoftwareChannelsChanged;

		public int SelectedDriver
		{
			get
			{
				NativeInvoke(FMOD_System_GetDriver(this, out var driverId));
				return driverId;
			}
			set
			{
				NativeInvoke(FMOD_System_SetDriver(this, value));
				SelectedDriverChanged?.Invoke(this, EventArgs.Empty);
			}
		}

		public ChannelGroup CreateChannelGroup(string name)
		{
			var bytesName = Encoding.UTF8.GetBytes(name);
			NativeInvoke(FMOD_System_CreateChannelGroup(this, bytesName, out var group));
			ChannelGroupCreated?.Invoke(this, EventArgs.Empty);
			return Core.Create<ChannelGroup>(group);
		}

		public SoundGroup CreateSoundGroup(string name)
		{
			var bytesName = Encoding.UTF8.GetBytes(name);
			NativeInvoke(FMOD_System_CreateSoundGroup(this, bytesName, out var group));
			SoundGroupCreated?.Invoke(this, EventArgs.Empty);
			return Core.Create<SoundGroup>(group);
		}

		public Driver GetDriver(int driverId)
		{
			using (var buffer = new MemoryBuffer(512))
			{
				NativeInvoke(FMOD_System_GetDriverInfo(this, driverId, buffer.Pointer, 512, out var guid, 
					out var rate, out var speakerMode, out var channels));
				return new Driver
				{
					Guid = guid,
					Id = driverId,
					Name = buffer.ToString(Encoding.UTF8),
					SpeakerMode = speakerMode,
					SpeakerModeChannels = channels,
					SystemRate = rate
				};
			}
		}

		public event EventHandler DspBufferChanged;

		public void SetDspBufferSize(uint bufferLength, int bufferCount)
		{
			NativeInvoke(FMOD_System_SetDSPBufferSize(this, bufferLength, bufferCount));
			DspBufferChanged?.Invoke(this, EventArgs.Empty);
		}

		public uint DspBuffersLength
		{
			get
			{
				NativeInvoke(FMOD_System_GetDSPBufferSize(this, out var bufferLength, out var dummy));
				return bufferLength;
			}
			set
			{
				NativeInvoke(FMOD_System_GetDSPBufferSize(this, out var dummy, out var count));
				NativeInvoke(FMOD_System_SetDSPBufferSize(this, value, count));
				DspBufferChanged?.Invoke(this, EventArgs.Empty);
			}
		}

		public event EventHandler PluginPathChanged;

		public void SetPluginPath(string path)
		{
			var bytes = Encoding.UTF8.GetBytes(path);
			NativeInvoke(FMOD_System_SetPluginPath(this, bytes));
			PluginPathChanged?.Invoke(this, EventArgs.Empty);
		}

		public int DspBuffersCount
		{
			get
			{
				NativeInvoke(FMOD_System_GetDSPBufferSize(this, out var dummy, out var count));
				return count;
			}
			set
			{
				NativeInvoke(FMOD_System_GetDSPBufferSize(this, out var bufferLength, out var dummy));
				NativeInvoke(FMOD_System_SetDSPBufferSize(this, bufferLength, value));
				DspBufferChanged?.Invoke(this, EventArgs.Empty);
			}
		}


		public int DriversCount
		{
			get
			{
				NativeInvoke(FMOD_System_GetNumDrivers(this, out var count));
				return count;
			}
		}

		public event EventHandler PluginLoaded;
		public event EventHandler PluginUnloaded;

		public uint LoadPlugin(string path, uint priority = 128u)
		{
			var bytes = Encoding.UTF8.GetBytes(path);
			NativeInvoke(FMOD_System_LoadPlugin(this, bytes, out var handle, priority));
			PluginLoaded?.Invoke(this, EventArgs.Empty);
			return handle;
		}

		public void UnloadPlugin(uint pluginHandle)
		{
			NativeInvoke(FMOD_System_UnloadPlugin(this, pluginHandle));
			PluginUnloaded?.Invoke(this, EventArgs.Empty);
		}

		public int GetNestedPluginCount(uint pluginHandle)
		{
			NativeInvoke(FMOD_System_GetNumNestedPlugins(this, pluginHandle, out var count));
			return count;
		}

		public int GetPluginCount(PluginType type)
		{
			NativeInvoke(FMOD_System_GetNumPlugins(this, type, out var count));
			return count;
		}

		public uint GetPluginHandle(PluginType type, int index)
		{
			NativeInvoke(FMOD_System_GetPluginHandle(this, type, index, out var handle));
			return handle;
		}

		public PluginInfo GetPluginInfo(uint handle)
		{
			using (var buffer = new MemoryBuffer(512))
			{
				NativeInvoke(FMOD_System_GetPluginInfo(this, handle, out var type, 
					buffer.Pointer, 512, out var version));
				return new PluginInfo
				{
					Handle = handle,
					Name = buffer.ToString(Encoding.UTF8),
					Type = type,
					Version = Core.Uint32ToVersion(version)
				};
			}
		}

		public Version Version
		{
			get
			{
				NativeInvoke(FMOD_System_GetVersion(this, out var version));
				return Core.Uint32ToVersion(version);
			}
		}

		public IntPtr OutputHandle
		{
			get
			{
				NativeInvoke(FMOD_System_GetOutputHandle(this, out var handle));
				return handle;
			}
		}

		public event EventHandler BufferSizeChanged;

		public uint BufferSize
		{
			get
			{
				NativeInvoke(FMOD_System_GetStreamBufferSize(this, out var size, out var dummy));
				return size;
			}
			set
			{
				NativeInvoke(FMOD_System_GetStreamBufferSize(this, out var dummy, out var type));
				NativeInvoke(FMOD_System_SetStreamBufferSize(this, value, type));
				BufferSizeChanged?.Invoke(this, EventArgs.Empty);
			}
		}

		public TimeUnit BufferSizeType
		{
			get
			{
				NativeInvoke(FMOD_System_GetStreamBufferSize(this, out var dummy, out var type));
				return type;
			}
			set
			{
				NativeInvoke(FMOD_System_GetStreamBufferSize(this, out var size, out var dummy));
				NativeInvoke(FMOD_System_SetStreamBufferSize(this, size, value));
				BufferSizeChanged?.Invoke(this, EventArgs.Empty);
			}
		}

		public void SetBufferSize(uint size, TimeUnit type)
		{
			NativeInvoke(FMOD_System_SetStreamBufferSize(this, size, type));
			BufferSizeChanged?.Invoke(this, EventArgs.Empty);
		}

		public RamUsage GetRamUsage()
		{
			NativeInvoke(FMOD_System_GetSoundRAM(this, out var current, out var max, out var total));
			return new RamUsage
			{
				CurrentlyAllocated = current,
				MaximumAllocated = max,
				Total = total
			};
		}

		public FileUsage GetFileUsage()
		{
			NativeInvoke(FMOD_System_GetFileUsage(this, out var samples, out var stream, out var other));
			return new FileUsage
			{
				SampleBytes = samples,
				StreamBytes = stream,
				OtherBytes = other
			};
		}

		public CpuUsage GetCpuUsage()
		{
			NativeInvoke(FMOD_System_GetCPUUsage(this, out var dsp, out var stream, out var geometry,
				out var update, out var total));
			return new CpuUsage
			{
				Dsp = dsp,
				Stream = stream,
				Geometry = geometry,
				Update = update,
				Total = total
			};
		}

		public Dsp CreateDsp(DspDescription description)
		{
			NativeInvoke(FMOD_System_CreateDSP(this, ref description, out var dsp));
			DspCreated?.Invoke(this, EventArgs.Empty);
			return Core.Create<Dsp>(dsp);
		}

		public Dsp CreateByPlugin(uint pluginHandle)
		{
			NativeInvoke(FMOD_System_CreateDSPByPlugin(this, pluginHandle, out var dsp));
			DspCreated?.Invoke(this, EventArgs.Empty);
			return Core.Create<Dsp>(dsp);
		}

		public event EventHandler ChannelGroupAttached;
		public event EventHandler ChannelGroupDetached;

		public void DetachChannelGroupFromPort(ChannelGroup channelGroup)
		{
			NativeInvoke(FMOD_System_DetachChannelGroupFromPort(this, channelGroup));
			ChannelGroupDetached?.Invoke(this, EventArgs.Empty);
		}

		public void AttachChannelGroupToPort(uint portType, ulong portIndex, ChannelGroup channelGroup, bool passThru)
		{
			NativeInvoke(FMOD_System_AttachChannelGroupToPort(this, portType, portIndex, channelGroup, passThru));
			ChannelGroupAttached?.Invoke(this, EventArgs.Empty);
		}

		public Geometry LoadGeometry(string filename)
		{
			return LoadGeometry(File.ReadAllBytes(filename));
		}

		public Geometry LoadGeometry(byte[] binary)
		{
			var handle = GCHandle.Alloc(binary, GCHandleType.Pinned);
			var geometry = LoadGeometry(handle.AddrOfPinnedObject(), binary.Length);
			handle.Free();
			return geometry;
		}

		public Geometry LoadGeometry(IntPtr data, int dataSize)
		{
			NativeInvoke(FMOD_System_LoadGeometry(this, data, dataSize, out var geometry));
			GeometryCreated?.Invoke(this, EventArgs.Empty);
			return Core.Create<Geometry>(geometry);
		}

		public SpeakerPosition GetSpeakerPosition(Speaker speaker)
		{
			NativeInvoke(FMOD_System_GetSpeakerPosition(this, speaker, out var x, out var y, out var active));
			return new SpeakerPosition(speaker, x, y, active);
		}

		public void SetSpeakerPosition(SpeakerPosition position)
		{
			SetSpeakerPosition(position.Speaker, position.Location.X, 
				position.Location.Y, position.IsActive);
		}

		public void SetSpeakerPosition(Speaker speaker, float x, float y, bool isActive = true)
		{
			NativeInvoke(FMOD_System_SetSpeakerPosition(this, speaker, x, y, isActive));
			SpeakerPositionChanged?.Invoke(this, EventArgs.Empty);
		}

		public void SetSpeakerPosition(Speaker speaker, PointF location, bool isActive = true)
		{
			SetSpeakerPosition(speaker, location.X, location.Y, isActive);
		}

		public event EventHandler SpeakerPositionChanged;

		public SpeakerPosition[] GetSpeakerPositions()
		{
			var speakers = (Speaker[]) Enum.GetValues(typeof(Speaker));
			var positions = new SpeakerPosition[speakers.Length];
			for (var i = 0; i < speakers.Length; i++)
			{
				var speaker = speakers[i];
				NativeInvoke(FMOD_System_GetSpeakerPosition(this, speaker, out var x, out var y, out var active));
				positions[i] = new SpeakerPosition(speaker, x, y, active);
			}
			return positions;
		}

		public void SetSpeakerPositions(SpeakerPosition[] speakerPositions)
		{
			foreach (var position in speakerPositions)
				SetSpeakerPosition(position);
		}

		public uint GetOutputByPlugin()
		{
			NativeInvoke(FMOD_System_GetOutputByPlugin(this, out var handle));
			return handle;
		}

		public void SetOutputByPlugin(uint pluginHandle)
		{
			NativeInvoke(FMOD_System_SetOutputByPlugin(this, pluginHandle));
			OutputChanged?.Invoke(this, EventArgs.Empty);
		}

		public uint GetNestedPlugin(uint pluginHandle, int index)
		{
			NativeInvoke(FMOD_System_GetNestedPlugin(this, pluginHandle, index, out var nestedHandle));
			return nestedHandle;
		}

		public DspDescription GetDspDescriptionByPlugin(uint pluginHandle)
		{
			NativeInvoke(FMOD_System_GetDSPInfoByPlugin(this, pluginHandle, out var infoPtr));
			// TODO: Check ptr validity
			return (DspDescription) Marshal.PtrToStructure(infoPtr, typeof(DspDescription));
		}

		public event EventHandler DspRegistered;
		
		public uint RegisterDsp(DspDescription description)
		{
			NativeInvoke(FMOD_System_RegisterDSP(this, ref description, out var handle));
			DspRegistered?.Invoke(this, EventArgs.Empty);
			return handle;
		}

		public SoftwareFormat SoftwareFormat
		{
			get
			{
				NativeInvoke(FMOD_System_GetSoftwareFormat(this, out var rate, out var mode, out var numSpeakers));
				return new SoftwareFormat
				{
					SampleRate = rate,
					SpeakerMode = mode,
					RawSpeakerCount = numSpeakers
				};
			}
			set
			{

				NativeInvoke(FMOD_System_SetSoftwareFormat(this, value.SampleRate, 
					value.SpeakerMode, value.RawSpeakerCount));
				SoftwareFormatChanged?.Invoke(this, EventArgs.Empty);
			}
		}

		public event EventHandler SoftwareFormatChanged;

		public void SetSoftwareFormat(int sampleRate, SpeakerMode speakerMode, int rawSpeakerCount = 0)
		{
			NativeInvoke(FMOD_System_SetSoftwareFormat(this, sampleRate.Clamp(8000, 192000), speakerMode, rawSpeakerCount));
			SoftwareFormatChanged?.Invoke(this, EventArgs.Empty);
		}

		public int GetSpeakerModeChannelCount(SpeakerMode mode)
		{
			NativeInvoke(FMOD_System_GetSpeakerModeChannels(this, mode, out int count));
			return count;
		}

		public void SetCallback(SystemCallback callback, SystemCallbackType type)
		{
			NativeInvoke(FMOD_System_SetCallback(this, callback, type));
		}


		public event EventHandler ListenerCountChanged;


		public int ListenerCount
		{
			get
			{
				NativeInvoke(FMOD_System_Get3DNumListeners(this, out var listenerCount));
				return listenerCount;
			}
			set
			{
				NativeInvoke(FMOD_System_Set3DNumListeners(this, value.Clamp(1, Core.MAX_LISTENERS)));
				ListenerCountChanged?.Invoke(this, EventArgs.Empty);
			}
		}

		public System3DSettings Settings3D
		{
			get
			{
				NativeInvoke(FMOD_System_Get3DSettings(this, out var doppler, out var distance, out var rolloff));
				return new System3DSettings
				{
					DopplerScale = doppler,
					DistanceFactor = distance,
					RolloffScale = rolloff
				};
			}
			set { Set3DSettings(value.DopplerScale, value.DistanceFactor, value.RolloffScale); }
		}

		public event EventHandler Settings3DChanged;

		public void Set3DSettings(float dopplerScale, float distanceFactor, float rolloffScale)
		{
			NativeInvoke(FMOD_System_Set3DSettings(this, dopplerScale, distanceFactor, rolloffScale));
			Settings3DChanged?.Invoke(this, EventArgs.Empty);
		}

		public int PlayingChannelCount
		{
			get
			{
				NativeInvoke(FMOD_System_GetChannelsPlaying(this, out var count, out var dummy));
				return count;
			}
		}

		public int RealChannelPlayingCount
		{
			get
			{
				NativeInvoke(FMOD_System_GetChannelsPlaying(this, out var dummy, out var count));
				return count;
			}
		}

		public ThreeDAttributes GetListenerAttributes(int listener)
		{
			NativeInvoke(FMOD_System_Get3DListenerAttributes(this, listener, out var position,
				out var velocity, out var forward, out var up));
			return new ThreeDAttributes
			{
				Forward = forward,
				Velocity = velocity,
				Position = position,
				Up = up
			};
		}

		public void SetListenerAttributes(int listener, ThreeDAttributes attributes)
		{
			SetListenerAttributes(listener, attributes.Position, attributes.Velocity, attributes.Forward, attributes.Up);
		}

		public void SetListenerAttributes(int listener, Vector position, Vector velocity, Vector forward, Vector up)
		{

			NativeInvoke(FMOD_System_Set3DListenerAttributes(this, listener, ref position, ref velocity, ref forward, ref up));
			ListenerAttributesChanged?.Invoke(this, EventArgs.Empty);
		}

		public event EventHandler ListenerAttributesChanged;
	





	}
}