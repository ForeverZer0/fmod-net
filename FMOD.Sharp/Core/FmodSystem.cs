using System;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using FMOD.Data;
using FMOD.Enumerations;
using FMOD.Structures;

namespace FMOD.Core
{
	public partial class FmodSystem : HandleBase
	{


		private Timer _updateTimer;



		#region Events

		public event EventHandler AdvancedSettingsChanged;

		public event EventHandler BufferSizeChanged;

		public event EventHandler ChannelGroupAttached;

		public event EventHandler ChannelGroupCreated;

		public event EventHandler ChannelGroupDetached;

		public event EventHandler Closed;

		public event EventHandler CodecRegistered;

		public event EventHandler DspBufferChanged;

		public event EventHandler DspCreated;

		public event EventHandler DspLocked;

		public event EventHandler DspPlayed;

		public event EventHandler DspRegistered;

		public event EventHandler DspUnlocked;

		public event EventHandler GeometryCreated;

		public event EventHandler ListenerAttributesChanged;

		public event EventHandler ListenerCountChanged;

		public event EventHandler MixerResumed;

		public event EventHandler MixerSuspended;

		public event EventHandler NetworkProxyChanged;

		public event EventHandler NetworkTimeoutChanged;

		public event EventHandler OutputChanged;

		public event EventHandler OutputRegistered;

		public event EventHandler PluginLoaded;

		public event EventHandler PluginPathChanged;

		public event EventHandler PluginUnloaded;

		public event EventHandler RecordingStarted;

		public event EventHandler RecordingStopped;

		public event EventHandler ReverbCreated;

		public event EventHandler ReverbPropertiesChanged;

		public event EventHandler SelectedDriverChanged;

		public event EventHandler Settings3DChanged;

		public event EventHandler SoftwareChannelsChanged;

		public event EventHandler SoftwareFormatChanged;

		public event EventHandler SoundCreated;

		public event EventHandler SoundGroupCreated;

		public event EventHandler SoundPlayed;

		public event EventHandler SpeakerPositionChanged;

		public event EventHandler UserDataChanged;

		public event EventHandler WorldSizeChanged;

		#endregion

		#region Properties & Indexers













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


		public int ListenerCount
		{
			get
			{
				NativeInvoke(FMOD_System_Get3DNumListeners(this, out var listenerCount));
				return listenerCount;
			}
			set
			{
				NativeInvoke(FMOD_System_Set3DNumListeners(this, value.Clamp(1, Constants.MAX_LISTENERS)));
				ListenerCountChanged?.Invoke(this, EventArgs.Empty);
			}
		}




		public IntPtr OutputHandle
		{
			get
			{
				NativeInvoke(FMOD_System_GetOutputHandle(this, out var outputHandle));
				return outputHandle;
			}
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
			set => Set3DSettings(value.DopplerScale, value.DistanceFactor, value.RolloffScale);
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

		public Version Version
		{
			get
			{
				NativeInvoke(FMOD_System_GetVersion(this, out var version));
				return Util.UInt32ToVersion(version);
			}
		}

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

		#endregion

		#region Methods

		public void AttachChannelGroupToPort(uint portType, ulong portIndex, ChannelGroup channelGroup, bool passThru)
		{
			NativeInvoke(FMOD_System_AttachChannelGroupToPort(this, portType, portIndex, channelGroup, passThru));
			ChannelGroupAttached?.Invoke(this, EventArgs.Empty);
		}

		public void AttachFileSystem(FileOpenCallback userOpen, FileCloseCallback userClose, FileReadCallback userRead,
			FileSeekCallback userSeek)
		{
			NativeInvoke(FMOD_System_AttachFileSystem(this, userOpen, userClose, userRead, userSeek));
		}

		public void CloseSystem()
		{
			NativeInvoke(FMOD_System_Close(this));
			Closed?.Invoke(this, EventArgs.Empty);
		}

		public static FmodSystem Create()
		{
			NativeInvoke(FMOD_System_Create(out var pointer));
			var system = new FmodSystem(pointer);
			CoreHelper.AddReference(pointer, system);
			return system;
		}

		public Dsp CreateByPlugin(uint pluginHandle)
		{
			NativeInvoke(FMOD_System_CreateDSPByPlugin(this, pluginHandle, out var dsp));
			DspCreated?.Invoke(this, EventArgs.Empty);
			return CoreHelper.Create<Dsp>(dsp);
		}

		public ChannelGroup CreateChannelGroup(string name)
		{
			var bytesName = Encoding.UTF8.GetBytes(name);
			NativeInvoke(FMOD_System_CreateChannelGroup(this, bytesName, out var group));
			ChannelGroupCreated?.Invoke(this, EventArgs.Empty);
			return CoreHelper.Create<ChannelGroup>(group);
		}

		public Dsp CreateDsp(DspDescription description)
		{
			NativeInvoke(FMOD_System_CreateDSP(this, ref description, out var dsp));
			DspCreated?.Invoke(this, EventArgs.Empty);
			return CoreHelper.Create<Dsp>(dsp);
		}

		public T CreateDspByType<T>() where T : Dsp
		{
			if (Enum.TryParse<DspType>(typeof(T).Name, true, out var dspType))
				return (T) CreateDspByType(dspType);
			return null;
		}

		public Dsp CreateDspByType(DspType dspType)
		{
			NativeInvoke(FMOD_System_CreateDSPByType(this, dspType, out var dsp));
			DspCreated?.Invoke(this, EventArgs.Empty);
			return Dsp.FromType(dsp, dspType);
		}

		public Geometry CreateGeometry(int maxPolygons, int maxVertices)
		{
			NativeInvoke(FMOD_System_CreateGeometry(this, maxPolygons, maxVertices, out var geometry));
			GeometryCreated?.Invoke(this, EventArgs.Empty);
			return CoreHelper.Create<Geometry>(geometry);
		}

		public Reverb CreateReverb()
		{
			NativeInvoke(FMOD_System_CreateReverb3D(this, out var reverb));
			ReverbCreated?.Invoke(this, EventArgs.Empty);
			return CoreHelper.Create<Reverb>(reverb);
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
			return CoreHelper.Create<Sound>(sound);
		}

		public SoundGroup CreateSoundGroup(string name)
		{
			var bytesName = Encoding.UTF8.GetBytes(name);
			NativeInvoke(FMOD_System_CreateSoundGroup(this, bytesName, out var group));
			SoundGroupCreated?.Invoke(this, EventArgs.Empty);
			return CoreHelper.Create<SoundGroup>(group);
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
			return CoreHelper.Create<Sound>(sound);
		}

		public void DetachChannelGroupFromPort(ChannelGroup channelGroup)
		{
			NativeInvoke(FMOD_System_DetachChannelGroupFromPort(this, channelGroup));
			ChannelGroupDetached?.Invoke(this, EventArgs.Empty);
		}

		protected override void Dispose(bool disposing)
		{
			_updateTimer?.Dispose();
			base.Dispose(disposing);
		}

		public Channel GetChannel(int index)
		{
			NativeInvoke(FMOD_System_GetChannel(this, index, out var channel));
			return CoreHelper.Create<Channel>(channel);
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

		public void GetDefaultMixMatrix(SpeakerMode sourceMode, SpeakerMode targetMode, float[] matrix, int matrixHop)
		{
			NativeInvoke(FMOD_System_GetDefaultMixMatrix(this, sourceMode, targetMode, matrix, matrixHop));
		}



		public DspDescription GetDspDescriptionByPlugin(uint pluginHandle)
		{
			NativeInvoke(FMOD_System_GetDSPInfoByPlugin(this, pluginHandle, out var infoPtr));
			if (infoPtr == IntPtr.Zero)
				return new DspDescription();
			return (DspDescription) Marshal.PtrToStructure(infoPtr, typeof(DspDescription));
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

		public float GetListenerDirectOcclusion(Vector listener, Vector source)
		{
			NativeInvoke(FMOD_System_GetGeometryOcclusion(this, ref listener, ref source, out var direct, out var dummy));
			return direct;
		}

		public float GetListenerReverbOcclusion(Vector listener, Vector source)
		{
			NativeInvoke(FMOD_System_GetGeometryOcclusion(this, ref listener, ref source, out var dummy, out var reverb));
			return reverb;
		}

		public float GetListenerReverbOcclusion(Vector listener, Vector source, out float direct, out float reverb)
		{
			NativeInvoke(FMOD_System_GetGeometryOcclusion(this, ref listener, ref source, out direct, out reverb));
			return reverb;
		}

		public uint GetNestedPlugin(uint pluginHandle, int index)
		{
			NativeInvoke(FMOD_System_GetNestedPlugin(this, pluginHandle, index, out var nestedHandle));
			return nestedHandle;
		}

		public int GetNestedPluginCount(uint pluginHandle)
		{
			NativeInvoke(FMOD_System_GetNumNestedPlugins(this, pluginHandle, out var count));
			return count;
		}

		public uint GetOutputByPlugin()
		{
			NativeInvoke(FMOD_System_GetOutputByPlugin(this, out var outputHandle));
			return outputHandle;
		}

		public int GetPluginCount(PluginType type)
		{
			NativeInvoke(FMOD_System_GetNumPlugins(this, type, out var count));
			return count;
		}

		public uint GetPluginHandle(PluginType type, int index)
		{
			NativeInvoke(FMOD_System_GetPluginHandle(this, type, index, out var pluginHandle));
			return pluginHandle;
		}

		public PluginInfo GetPluginInfo(uint pluginHandle)
		{
			using (var buffer = new MemoryBuffer(512))
			{
				NativeInvoke(FMOD_System_GetPluginInfo(this, pluginHandle, out var type,
					buffer.Pointer, 512, out var version));
				return new PluginInfo
				{
					Handle = pluginHandle,
					Name = buffer.ToString(Encoding.UTF8),
					Type = type,
					Version = Util.UInt32ToVersion(version)
				};
			}
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

		public uint GetRecordPosition(int driverId)
		{
			NativeInvoke(FMOD_System_GetRecordPosition(this, driverId, out var position));
			return position;
		}



		public int GetSpeakerModeChannelCount(SpeakerMode mode)
		{
			NativeInvoke(FMOD_System_GetSpeakerModeChannels(this, mode, out var count));
			return count;
		}

		public SpeakerPosition GetSpeakerPosition(Speaker speaker)
		{
			NativeInvoke(FMOD_System_GetSpeakerPosition(this, speaker, out var x, out var y, out var active));
			return new SpeakerPosition(speaker, x, y, active);
		}

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



		public void EnableSelfUpdate(int tickFreq)
		{
			if (_updateTimer == null)
				_updateTimer = new Timer(SelfUpdate, null, 0, tickFreq);
		}

		public void DisableSelfUpdate()
		{
			_updateTimer.Dispose();
			_updateTimer = null;
		}

		public bool IsRecording(int driverId)
		{
			NativeInvoke(FMOD_System_IsRecording(this, driverId, out var recording));
			return recording;
		}

		public Geometry LoadGeometry(string filename)
		{
			return LoadGeometry(File.ReadAllBytes(filename));
		}

		public Geometry LoadGeometry(byte[] binary)
		{
			var gcHandle = GCHandle.Alloc(binary, GCHandleType.Pinned);
			var geometry = LoadGeometry(gcHandle.AddrOfPinnedObject(), binary.Length);
			gcHandle.Free();
			return geometry;
		}

		public Geometry LoadGeometry(IntPtr data, int dataSize)
		{
			NativeInvoke(FMOD_System_LoadGeometry(this, data, dataSize, out var geometry));
			GeometryCreated?.Invoke(this, EventArgs.Empty);
			return CoreHelper.Create<Geometry>(geometry);
		}

		public uint LoadPlugin(string path, uint priority = 128u)
		{
			var bytes = Encoding.UTF8.GetBytes(path);
			NativeInvoke(FMOD_System_LoadPlugin(this, bytes, out var pluginHandle, priority));
			PluginLoaded?.Invoke(this, EventArgs.Empty);
			return pluginHandle;
		}

		public void LockDsp()
		{
			NativeInvoke(FMOD_System_LockDSP(this));
			DspLocked?.Invoke(this, EventArgs.Empty);
		}

		public Channel PlayDsp(Dsp dsp, bool paused = false, ChannelGroup group = null)
		{
			NativeInvoke(FMOD_System_PlayDSP(this, dsp, group ?? IntPtr.Zero, paused, out var channel));
			DspPlayed?.Invoke(this, EventArgs.Empty);
			return CoreHelper.Create<Channel>(channel);
		}

		public Channel PlaySound(Sound sound, bool paused = false, ChannelGroup group = null)
		{
			NativeInvoke(FMOD_System_PlaySound(this, sound, group ?? IntPtr.Zero, paused, out var channel));
			SoundPlayed?.Invoke(this, EventArgs.Empty);
			return CoreHelper.Create<Channel>(channel);
		}

		public void RecordStart(int driverId, Sound sound, bool loop = false)
		{
			NativeInvoke(FMOD_System_RecordStart(this, driverId, sound, loop));
			RecordingStarted?.Invoke(this, EventArgs.Empty);
		}

		public void RecordStop(int driverId)
		{
			NativeInvoke(FMOD_System_RecordStop(this, driverId));
			RecordingStopped?.Invoke(this, EventArgs.Empty);
		}

		public uint RegisterCodec(CodecDescription description, uint priority)
		{
			NativeInvoke(FMOD_System_RegisterCodec(this, ref description, out var codecHandle, priority));
			CodecRegistered?.Invoke(this, EventArgs.Empty);
			return codecHandle;
		}

		public uint RegisterDsp(DspDescription description)
		{
			NativeInvoke(FMOD_System_RegisterDSP(this, ref description, out var dspHandle));
			DspRegistered?.Invoke(this, EventArgs.Empty);
			return dspHandle;
		}

		public uint RegisterOutput(OutputDescription description)
		{
			NativeInvoke(FMOD_System_RegisterOutput(this, ref description, out var outputHandle));
			OutputRegistered?.Invoke(this, EventArgs.Empty);
			return outputHandle;
		}

		public void ResumeMixer()
		{
			NativeInvoke(FMOD_System_MixerResume(this));
			MixerResumed?.Invoke(this, EventArgs.Empty);
		}

		public void Set3DRolloffCallback(Cb_3DRolloffcallback callback)
		{
			NativeInvoke(FMOD_System_Set3DRolloffCallback(this, callback));
		}

		public void Set3DSettings(float dopplerScale, float distanceFactor, float rolloffScale)
		{
			NativeInvoke(FMOD_System_Set3DSettings(this, dopplerScale, distanceFactor, rolloffScale));
			Settings3DChanged?.Invoke(this, EventArgs.Empty);
		}



		public void SetCallback(SystemCallback callback, SystemCallbackType type)
		{
			NativeInvoke(FMOD_System_SetCallback(this, callback, type));
		}

		public void SetDspBufferSize(uint bufferLength, int bufferCount)
		{
			NativeInvoke(FMOD_System_SetDSPBufferSize(this, bufferLength, bufferCount));
			DspBufferChanged?.Invoke(this, EventArgs.Empty);
		}

		public void SetFileSystem(FileOpenCallback userOpen,
			FileCloseCallback userClose, FileReadCallback userRead, FileSeekCallback userSeek,
			FileAsyncReadCallback userAsyncRead, FileAsyncCancelCallback userAsyncCancel, int blockAlign)
		{
			NativeInvoke(FMOD_System_SetFileSystem(this, userOpen, userClose, userRead, userSeek, userAsyncRead, userAsyncCancel,
				blockAlign));
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















		#region Documentation Complete

		
		/// <summary>
		/// Gets or sets a proxy server to use for all subsequent internet connections.
		/// </summary>
		/// <value>
		/// The network proxy.
		/// </value>
		/// <remarks>
		/// <para>Basic authentication is supported.</para>
		/// <para>To use it, this parameter must be in <b>user:password@host:port</b> format e.g. <b>bob:sekrit123@www.fmod.org:8888</b>.</para>
		/// <para>Set this property to <c>null</c> if no proxy is required.</para>
		/// </remarks>
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

		/// <summary>
		/// <para>Gets or sets the timeout for network streams.</para>
		/// <para>The default timeout is 5000ms.</para>
		/// </summary>
		/// <value>
		/// The network timeout.
		/// </value>
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

		/// <summary>
		/// Retrieves the current reverb environment for the specified reverb instance.
		/// </summary>
		/// <param name="index">Index of the particular reverb instance to target, from <c>0</c> to <see cref="Constants.MAX_REVERBS"/> inclusive.</param>
		/// <returns>The current reverb environment description from the specified index.</returns>
		/// <seealso cref="ReverbProperties"/>
		/// <seealso cref="SetReverbProperties"/>
		/// <seealso cref="ChannelControl.GetReverbProperties"/>
		/// <seealso cref="ChannelControl.SetReverbProperties"/>
		public ReverbProperties GetReverbProperties(int index)
		{
			NativeInvoke(FMOD_System_GetReverbProperties(this, index, out var properties));
			return properties;
		}

		/// <summary>
		/// <para>Gets the handle to the internal master channel group.</para> 
		/// <para>This is the default channel group that all channels play on.</para>
		/// <para>This channel group can be used to do things like set the master volume for all playing sounds. See the <see cref="ChannelGroup"/> API for more functionality.</para>
		/// </summary>
		/// <value>
		/// The master channel group.
		/// </value>
		/// <seealso cref="CreateChannelGroup"/>
		/// <seealso cref="ChannelGroup.Volume"/>
		public ChannelGroup MasterChannelGroup
		{
			get
			{
				NativeInvoke(FMOD_System_GetMasterChannelGroup(this, out var channelGroup));
				return CoreHelper.Create<ChannelGroup>(channelGroup);
			}
		}

		/// <summary>
		/// Gets the default sound group, where all sounds are placed when they are created.
		/// </summary>
		/// <value>
		/// The master sound group.
		/// </value>
		/// <remarks>If a user based <see cref="SoundGroup"/> is deleted/released, the sounds will be put back into this sound group.</remarks>
		/// <seealso cref="SoundGroup" />
		public SoundGroup MasterSoundGroup
		{
			get
			{
				NativeInvoke(FMOD_System_GetMasterSoundGroup(this, out var soundGroup));
				return CoreHelper.Create<SoundGroup>(soundGroup);
			}
		}

		/// <summary>
		/// Gets or sets the selected soundcard driver. 
		/// <para>Drivers are enumerated when selecting a driver with this property or other driver related functions such as <see cref="DriversCount"/> or <see cref="GetDriver"/>.</para>
		/// <para>This function is used when an output mode has enumerated more than one output device, and you need to select between them.</para>
		/// </summary>
		/// <value>
		/// The selected driver.
		/// </value>
		/// <seealso cref="DriversCount"/>
		/// <seealso cref="GetDriver"/>
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

		/// <summary>
		/// Gets the identification information about a sound device specified by its index, and specific to the output mode set with <see cref="Output"/> property.
		/// </summary>
		/// <param name="driverId">
		/// <para>Index of the sound driver device.</para>
		/// <para>The total number of devices can be found with <see cref="DriversCount"/>. </para>
		/// </param>
		/// <returns>A <see cref="Driver"/> object describing the specified driver.</returns>
		/// <seealso cref="Driver"/>
		/// <seealso cref="Output"/>
		/// <seealso cref="DriversCount"/>
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

		/// <summary>
		/// Gets the number of soundcard devices on the machine, specific to the output mode set with the <see cref="Output"/> property.
		/// </summary>
		/// <value>
		/// The number of drivers.
		/// </value>
		/// <remarks>
		/// <para>If <see cref="Output"/> was not changed it will return the number of drivers available for the default output type.</para>
		/// <para>Use this for enumerating sound devices. Use <see cref="GetDriver"/> to get the device's name.</para>
		/// </remarks>
		/// <seealso cref="SelectedDriver"/>
		/// <seealso cref="GetDriver"/>
		/// <seealso cref="Output"/>
		public int DriversCount
		{
			get
			{
				NativeInvoke(FMOD_System_GetNumDrivers(this, out var count));
				return count;
			}
		}

		/// <summary>
		/// <para>Gets or sets the output mode for the platform.</para>
		/// <para>This is for selecting different OS specific APIs which might have different features.</para>
		/// </summary>
		/// <value>
		/// The output.
		/// </value>
		/// <remarks>
		/// <para>This function is only necessary if you want to specifically switch away from the default output mode for the operating system. The most optimal mode is selected by default for the operating system.</para>
		/// <alert class="note">
		/// <para>This function can be called after <b>FMOD</b> is already activated, you can use it to change the output mode at runtime. If <see cref="SystemCallbackType.DeviceListChanged"/> is specified use the <see cref="Output"/> property to change to <see cref="OutputType.NoSound"/> if no more sound card drivers exist.</para>
		/// </alert>
		/// </remarks>
		/// <seealso cref="Initialize()"/>
		/// <seealso cref="CloseSystem"/>
		/// <seealso cref="Output"/>
		/// <seealso cref="OutputType"/>
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

		/// <summary>
		/// <para>Gets the number of recording driver currently plugged in.</para>
		/// <para>Use this to enumerate all recording devices possible so that the user can select one.</para>
		/// </summary>
		/// <value>
		/// The number of connected record drivers.
		/// </value>
		/// <seealso cref="RecordDriverCount"/>
		/// <seealso cref="GetRecordDriver"/>
		public int ConnectedRecordDriverCount
		{
			get
			{
				NativeInvoke(FMOD_System_GetRecordNumDrivers(this, out var dummy, out var connected));
				return connected;
			}
		}

		/// <summary>
		/// <para>Gets the number of recording drivers available for this output mode.</para>
		/// <para>Use this to enumerate all recording devices possible so that the user can select one.</para>
		/// </summary>
		/// <value>
		/// The number of record drivers.
		/// </value>
		/// <seealso cref="ConnectedRecordDriverCount"/>
		/// <seealso cref="GetRecordDriver"/>
		public int RecordDriverCount
		{
			get
			{
				NativeInvoke(FMOD_System_GetRecordNumDrivers(this, out var driverCount, out var dummy));
				return driverCount;
			}
		}

				/// <summary>
		/// Gets or sets the sets advanced features like configuring memory and cpu usage for <see cref="Mode.CreateCompressedSample"/> usage.
		/// </summary>
		/// <value>
		/// The advanced settings structure.
		/// </value>
		/// <seealso cref="T:FMOD.Structures.AdvancedSettings"/>
		/// <seealso cref="T:FMOD.Enumerations.Mode"/>
		/// <seealso cref="AdvancedSettingsChanged"/>
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

		/// <summary>
		/// Returns the current internal buffersize settings for streamable sounds.
		/// </summary>
		/// <returns>A <see cref="StreamBufferInfo"/> object containing the values of the buffer size.</returns>
		/// <seealso cref="TimeUnit"/>
		/// <seealso cref="GetStreamBufferSize(out uint, out TimeUnit)"/>
		/// <seealso cref="SetStreamBufferSize(uint, TimeUnit)"/>
		/// <seealso cref="StreamBufferInfo"/>
		public StreamBufferInfo GetStreamBufferSize()
		{
			NativeInvoke(FMOD_System_GetStreamBufferSize(this, out var size, out var sizeType));
			return new StreamBufferInfo
			{
				Size = size,
				SizeType = sizeType
			};
		}

		/// <summary>
		/// Returns the current internal buffersize settings for streamable sounds.
		/// </summary>
		/// <param name="size">
		/// <para>A variable that receives the current stream file buffer size setting.</para> 
		/// <para>Default is <c>16384</c> (<see cref="TimeUnit.RawBytes"/>).</para>
		/// </param>
		/// <param name="sizeType">
		/// <para>A variable that receives the type of unit for the current stream file buffer size setting.</para>
		/// <para>Can be <see cref="TimeUnit.Ms"/>, <see cref="TimeUnit.Pcm"/>, <see cref="TimeUnit.PcmBytes"/> or <see cref="TimeUnit.RawBytes"/>.</para>
		/// <para> Default is <see cref="TimeUnit.RawBytes"/>.</para>
		/// </param>
		/// <seealso cref="TimeUnit"/>
		/// <seealso cref="GetStreamBufferSize()"/>
		/// <seealso cref="SetStreamBufferSize(uint, TimeUnit)"/>
		/// <seealso cref="StreamBufferInfo"/>
		public void GetStreamBufferSize(out uint size, out TimeUnit sizeType)
		{
			NativeInvoke(FMOD_System_GetStreamBufferSize(this, out size, out sizeType));
		}

		/// <summary>
		/// <para>Sets the internal buffersize for streams opened after calling this function.</para>
		/// <para>Larger values will consume more memory (see remarks), whereas smaller values may cause buffer under-run/starvation/stuttering caused by large delays in disk access (ie netstream), or CPU usage in slow machines, or by trying to play too many streams at once.</para>
		/// </summary>
		/// <param name="size">
		/// <para>Size of stream file buffer.</para>
		/// <para>Default is <c>16384</c>.</para>
		/// </param>
		/// <param name="type">
		/// <para>Type of unit for stream file buffer size.</para>
		/// <para>Must be <see cref="TimeUnit.Ms"/>, <see cref="TimeUnit.Pcm"/>, <see cref="TimeUnit.PcmBytes"/> or <see cref="TimeUnit.RawBytes"/>.</para>
		/// <para>Default is <see cref="TimeUnit.RawBytes"/>.</para>
		/// </param>
		/// <remarks>
		/// <alert class="note">
		/// <para>This does not affect streams created with <see cref="Mode.OpenUser"/>, as the buffer size is specified in <see cref="CreateSound(string, Mode)"/>.</para>
		/// <para>This does not affect latency of playback. All streams are pre-buffered (unless opened with <see cref="Mode.OpenOnly"/>), so they will always start immediately.</para>
		/// <para><b>Seek</b> and <b>Play</b> operations can sometimes cause a reflush of this buffer.</para>
		/// </alert>
		/// <para>If <see cref="TimeUnit.RawBytes"/> is used, the memory allocated is 2 * the size passed in, because <b>FMOD</b> allocates a double buffer.</para>
		/// <para>If <see cref="TimeUnit.Ms"/>, <see cref="TimeUnit.Pcm"/>or <see cref="TimeUnit.PcmBytes"/> is used, and the stream is infinite (such as a shoutcast netstream), or VBR, then <b>FMOD</b> cannot calculate an accurate compression ratio to work with when the file is opened. This means it will then base the buffersize on <see cref="TimeUnit.PcmBytes"/>, or in other words the number of PCM bytes, but this will be incorrect for some compressed formats.</para>
		/// <para>Use <see cref="TimeUnit.RawBytes"/> for these type (infinite / undetermined length) of streams for more accurate read sizes.</para>
		/// <para>To determine the actual memory usage of a stream, including sound buffer and other overhead, use <see cref="MemoryManager.GetStats(bool)"/> before and after creating a sound.</para>
		/// <para>The stream may still stutter if the codec uses a large amount of CPU time, which impacts the smaller, internal "decode" buffer.</para>
		/// <para>The decode buffer size is changeable via <see cref="CreateSoundExInfo"/>.</para> 
		/// </remarks>
		/// <seealso cref="SetStreamBufferSize(StreamBufferInfo)"/>
		/// <seealso cref="StreamBufferInfo"/>
		/// <seealso cref="BufferSizeChanged"/>
		/// <seealso cref="GetStreamBufferSize()"/>
		/// <seealso cref="TimeUnit"/>
		/// <seealso cref="CreateSoundExInfo"/>
		/// <seealso cref="MemoryManager.GetStats(bool)"/>
		/// <seealso cref="CreateSound(string, Mode)"/>
		/// <seealso cref="Channel.Mute"/>
		public void SetStreamBufferSize(uint size, TimeUnit type)
		{
			NativeInvoke(FMOD_System_SetStreamBufferSize(this, size, type));
			BufferSizeChanged?.Invoke(this, EventArgs.Empty);
		}

		/// <summary>
		/// <para>Sets the internal buffersize for streams opened after calling this function.</para>
		/// <para>Larger values will consume more memory (see remarks), whereas smaller values may cause buffer under-run/starvation/stuttering caused by large delays in disk access (ie netstream), or CPU usage in slow machines, or by trying to play too many streams at once.</para>
		/// </summary>
		/// <param name="info"><see cref="StreamBufferInfo"/> object containing the settings.</param>
		/// <remarks>
		/// <alert class="note">
		/// <para>This does not affect streams created with <see cref="Mode.OpenUser"/>, as the buffer size is specified in <see cref="CreateSound(string, Mode)"/>.</para>
		/// <para>This does not affect latency of playback. All streams are pre-buffered (unless opened with <see cref="Mode.OpenOnly"/>), so they will always start immediately.</para>
		/// <para><b>Seek</b> and <b>Play</b> operations can sometimes cause a reflush of this buffer.</para>
		/// </alert>
		/// <para>If <see cref="TimeUnit.RawBytes"/> is used, the memory allocated is 2 * the size passed in, because <b>FMOD</b> allocates a double buffer.</para>
		/// <para>If <see cref="TimeUnit.Ms"/>, <see cref="TimeUnit.Pcm"/>or <see cref="TimeUnit.PcmBytes"/> is used, and the stream is infinite (such as a shoutcast netstream), or VBR, then <b>FMOD</b> cannot calculate an accurate compression ratio to work with when the file is opened. This means it will then base the buffersize on <see cref="TimeUnit.PcmBytes"/>, or in other words the number of PCM bytes, but this will be incorrect for some compressed formats.</para>
		/// <para>Use <see cref="TimeUnit.RawBytes"/> for these type (infinite / undetermined length) of streams for more accurate read sizes.</para>
		/// <para>To determine the actual memory usage of a stream, including sound buffer and other overhead, use <see cref="MemoryManager.GetStats(bool)"/> before and after creating a sound.</para>
		/// <para>The stream may still stutter if the codec uses a large amount of CPU time, which impacts the smaller, internal "decode" buffer.</para>
		/// <para>The decode buffer size is changeable via <see cref="CreateSoundExInfo"/>.</para> 
		/// </remarks>
		/// <seealso cref="SetStreamBufferSize(StreamBufferInfo)"/>
		/// <seealso cref="StreamBufferInfo"/>
		/// <seealso cref="BufferSizeChanged"/>
		/// <seealso cref="GetStreamBufferSize()"/>
		/// <seealso cref="TimeUnit"/>
		/// <seealso cref="CreateSoundExInfo"/>
		/// <seealso cref="MemoryManager.GetStats(bool)"/>
		/// <seealso cref="CreateSound(string, Mode)"/>
		/// <seealso cref="Channel.Mute"/>
		public void SetStreamBufferSize(StreamBufferInfo info)
		{
			NativeInvoke(FMOD_System_SetStreamBufferSize(this, info.Size, info.SizeType));
			BufferSizeChanged?.Invoke(this, EventArgs.Empty);
		}

		/// <summary>
		/// <para>Initializes the system object, and the sound device. This has to be called at the start of the user's program.</para>
		/// <para><b>You must first create a system object with <see cref="Create"/>.</b></para>
		/// </summary>
		/// <remarks>
		/// <b><u>Virtual Channels</u></b><lineBreak/>
		/// <para>These types of voices are the ones you work with using the <see cref="ChannelControl"/> API. The advantage of virtual channels are, unlike older versions of <b>FMOD</b>, you can now play as many sounds as you like without fear of ever running out of voices, or playsound failing. You can also avoid "channel stealing" if you specify enough virtual voices.</para>
		/// <para>As an example, you can play 1000 sounds at once, even on a 32 channel soundcard.</para>
		/// <para>FMOD will only play the most important/closest/loudest (determined by volume/distance/geometry and priority settings) voices, and the other 968 voices will be virtualized without expense to the CPU. The voice's cursor positions are updated.</para>
		/// <para>When the priority of sounds change or emulated sounds get louder than audible ones, they will swap the actual voice resource over and play the voice from its correct position in time as it should be heard.</para>
		/// <para>What this means is you can play all 1000 sounds, if they are scattered around the game world, and as you move around the world you will hear the closest or most important 32, and they will automatically swap in and out as you move.</para>
		/// <para>Currently the maximum channel limit is 4093.</para>
		/// </remarks>
		/// <seealso cref="InitFlags"/>
		/// <seealso cref="OutputType"/>
		/// <seealso cref="Create"/>
		/// <seealso cref="CloseSystem"/>
		public void Initialize()
		{
			Initialize(InitFlags.Normal, Constants.MAX_CHANNELS, null);
		}

		/// <summary>
		/// <para>Initializes the system object, and the sound device. This has to be called at the start of the user's program.</para>
		/// <para><b>You must first create a system object with <see cref="Create"/>.</b></para>
		/// </summary>
		/// <param name="flags">
		/// <para>The maximum number of channels to be used in <b>FMOD</b>.</para>
		/// <para>They are also called "virtual channels" as you can play as many of these as you want, even if you only have a small number of software voices.</para>
		/// <para>See remarks section for more.</para>
		/// </param>
		/// <remarks>
		/// <b><u>Virtual Channels</u></b><lineBreak/>
		/// <para>These types of voices are the ones you work with using the <see cref="ChannelControl"/> API. The advantage of virtual channels are, unlike older versions of <b>FMOD</b>, you can now play as many sounds as you like without fear of ever running out of voices, or playsound failing. You can also avoid "channel stealing" if you specify enough virtual voices.</para>
		/// <para>As an example, you can play 1000 sounds at once, even on a 32 channel soundcard.</para>
		/// <para>FMOD will only play the most important/closest/loudest (determined by volume/distance/geometry and priority settings) voices, and the other 968 voices will be virtualized without expense to the CPU. The voice's cursor positions are updated.</para>
		/// <para>When the priority of sounds change or emulated sounds get louder than audible ones, they will swap the actual voice resource over and play the voice from its correct position in time as it should be heard.</para>
		/// <para>What this means is you can play all 1000 sounds, if they are scattered around the game world, and as you move around the world you will hear the closest or most important 32, and they will automatically swap in and out as you move.</para>
		/// <para>Currently the maximum channel limit is 4093.</para>
		/// </remarks>
		/// <seealso cref="InitFlags"/>
		/// <seealso cref="OutputType"/>
		/// <seealso cref="Create"/>
		/// <seealso cref="CloseSystem"/>
		public void Initialize(InitFlags flags)
		{
			Initialize(flags, Constants.MAX_CHANNELS, null);
		}

		/// <summary>
		/// <para>Initializes the system object, and the sound device. This has to be called at the start of the user's program.</para>
		/// <para><b>You must first create a system object with <see cref="Create"/>.</b></para>
		/// </summary>
		/// <param name="flags">
		/// <para>The maximum number of channels to be used in <b>FMOD</b>.</para>
		/// <para>They are also called "virtual channels" as you can play as many of these as you want, even if you only have a small number of software voices.</para>
		/// <para>See remarks section for more.</para>
		/// </param>
		/// <param name="maxChannels">This can be a selection of flags bitwise OR'ed together to change the behaviour of <b>FMOD</b> at initialization time.</param>
		/// <remarks>
		/// <b><u>Virtual Channels</u></b><lineBreak/>
		/// <para>These types of voices are the ones you work with using the <see cref="ChannelControl"/> API. The advantage of virtual channels are, unlike older versions of <b>FMOD</b>, you can now play as many sounds as you like without fear of ever running out of voices, or playsound failing. You can also avoid "channel stealing" if you specify enough virtual voices.</para>
		/// <para>As an example, you can play 1000 sounds at once, even on a 32 channel soundcard.</para>
		/// <para>FMOD will only play the most important/closest/loudest (determined by volume/distance/geometry and priority settings) voices, and the other 968 voices will be virtualized without expense to the CPU. The voice's cursor positions are updated.</para>
		/// <para>When the priority of sounds change or emulated sounds get louder than audible ones, they will swap the actual voice resource over and play the voice from its correct position in time as it should be heard.</para>
		/// <para>What this means is you can play all 1000 sounds, if they are scattered around the game world, and as you move around the world you will hear the closest or most important 32, and they will automatically swap in and out as you move.</para>
		/// <para>Currently the maximum channel limit is 4093.</para>
		/// </remarks>
		/// <seealso cref="InitFlags"/>
		/// <seealso cref="OutputType"/>
		/// <seealso cref="Create"/>
		/// <seealso cref="CloseSystem"/>
		public void Initialize(InitFlags flags, int maxChannels)
		{
			Initialize(flags, maxChannels, null);
		}

		/// <overloads>
		/// <summary>
		/// <para>Initializes the system object, and the sound device. This has to be called at the start of the user's program.</para>
		/// <para><b>You must first create a system object with <see cref="Create"/>.</b></para>
		/// </summary>
		/// <param name="flags">
		/// <para>The maximum number of channels to be used in <b>FMOD</b>.</para>
		/// <para>They are also called "virtual channels" as you can play as many of these as you want, even if you only have a small number of software voices.</para>
		/// <para>See remarks section for more.</para>
		/// </param>
		/// <param name="maxChannels">This can be a selection of flags bitwise OR'ed together to change the behaviour of <b>FMOD</b> at initialization time.</param>
		/// <param name="extraDriverData">
		/// <para>Driver specific data that can be passed to the output plugin.</para> 
		/// <para>For example the filename for the wav writer plugin. See <see cref="OutputType"/> for what each output mode might take here.</para>
		/// <para><b>Optional.</b> Specify <c>null</c> or <see cref="IntPtr.Zero"/> to ignore.</para>
		/// </param>
		/// <remarks>
		/// <b><u>Virtual Channels</u></b><lineBreak/>
		/// <para>These types of voices are the ones you work with using the <see cref="ChannelControl"/> API. The advantage of virtual channels are, unlike older versions of <b>FMOD</b>, you can now play as many sounds as you like without fear of ever running out of voices, or playsound failing. You can also avoid "channel stealing" if you specify enough virtual voices.</para>
		/// <para>As an example, you can play 1000 sounds at once, even on a 32 channel soundcard.</para>
		/// <para>FMOD will only play the most important/closest/loudest (determined by volume/distance/geometry and priority settings) voices, and the other 968 voices will be virtualized without expense to the CPU. The voice's cursor positions are updated.</para>
		/// <para>When the priority of sounds change or emulated sounds get louder than audible ones, they will swap the actual voice resource over and play the voice from its correct position in time as it should be heard.</para>
		/// <para>What this means is you can play all 1000 sounds, if they are scattered around the game world, and as you move around the world you will hear the closest or most important 32, and they will automatically swap in and out as you move.</para>
		/// <para>Currently the maximum channel limit is 4093.</para>
		/// </remarks>
		/// <seealso cref="InitFlags"/>
		/// <seealso cref="OutputType"/>
		/// <seealso cref="Create"/>
		/// <seealso cref="CloseSystem"/>
		/// </overloads>
		public void Initialize(InitFlags flags, int maxChannels, IntPtr? extraDriverData)
		{
			NativeInvoke(FMOD_System_Init(this, maxChannels.Clamp(1, 4093), flags, extraDriverData ?? IntPtr.Zero));
#if DEBUG
			EnableSelfUpdate(1000 / 60);
#endif
		}

		/// <summary>
		/// Selects an output type based on the enumerated list of outputs including <b>FMOD</b> and 3rd party output plugins.
		/// </summary>
		/// <param name="pluginHandle">A handle to a pre-existing output plugin..</param>
		/// <remarks>
		/// <para>This function can be called after <b>FMOD</b> is already activated.</para>
		/// <para> You can use it to change the output mode at runtime. If <see cref="SystemCallbackType.DeviceListChanged"/> is specified use the setOutput call to change to <see cref="OutputType.NoSound"/> if no more sound card drivers exist.</para>
		/// </remarks>
		/// <seealso cref="GetPluginCount"/>
		/// <seealso cref="GetOutputByPlugin"/>
		/// <seealso cref="Output"/>
		/// <seealso cref="Initialize(InitFlags, int, IntPtr?)"/>
		/// <seealso cref="CloseSystem"/>
		/// <seealso cref="OutputChanged"/>
		/// <seealso cref="SystemCallback"/>
		/// <seealso cref="SystemCallbackType"/>
		/// <seealso cref="OutputType"/>
		public void SetOutputByPlugin(uint pluginHandle)
		{
			NativeInvoke(FMOD_System_SetOutputByPlugin(this, pluginHandle));
			OutputChanged?.Invoke(this, EventArgs.Empty);
		}

		/// <summary>
		/// Specifies a base search path for plugins so they can be placed somewhere else than the directory of the main executable.
		/// </summary>
		/// <param name="path">String containing a correctly formatted path to load plugins from.</param>
		/// <seealso cref="PluginPathChanged"/>
		/// <seealso cref="LoadPlugin"/>
		/// <seealso cref="Initialize(InitFlags, int, IntPtr?)"/>
		public void SetPluginPath(string path)
		{
			var bytes = Encoding.UTF8.GetBytes(path);
			NativeInvoke(FMOD_System_SetPluginPath(this, bytes));
			PluginPathChanged?.Invoke(this, EventArgs.Empty);
		}

		/// <summary>
		/// <para>Sets parameters for the global reverb environment.</para>
		/// <para>To assist in defining reverb properties there are several presets available, see <see cref="ReverbPresets"/>.</para>
		/// </summary>
		/// <param name="index">Index of the particular reverb instance to target, from <c>0</c> to <see cref="Constants.MAX_REVERBS"/> inclusive.</param>
		/// <param name="properties">
		/// <para>A <see cref="ReverbProperties"/> structure which defines the attributes for the reverb.</para>
		/// <para>Passing <c>null</c> to this function will delete the physical reverb.</para>
		/// </param>
		/// <remarks>
		/// <para>When using each instance for the first time, <b>FMOD</b> will create a physical SFX reverb DSP unit that takes up several hundred kilobytes of memory and some CPU.</para>
		/// </remarks>
		/// <seealso cref="ReverbPresets"/>
		/// <seealso cref="GetReverbProperties"/>
		/// <seealso cref="ReverbPropertiesChanged"/>
		/// <seealso cref="ChannelControl.GetReverbProperties"/>
		/// <seealso cref="ChannelControl.SetReverbProperties"/>
		/// <seealso cref="Nullable"/>
		public void SetReverbProperties(int index, ReverbProperties? properties)
		{
			if (properties.HasValue)
			{
				var value = properties.Value;
				NativeInvoke(FMOD_System_SetReverbProperties(this, index, ref value));
			}
			else
			{
				NativeInvoke(FMOD_System_SetReverbProperties(this, index, IntPtr.Zero));
			}	
			ReverbPropertiesChanged?.Invoke(this, EventArgs.Empty);
		}

		/// <summary>
		/// Gets or sets the output format for the software mixer.
		/// </summary>
		/// <remarks>
		/// <alert class="note"><para>Note that the settings returned here may differ from the settings provided by the user. This is because the driver may require certain settings to initialize.</para></alert>
		/// <para>If loading Studio banks, this must be called with speakermode corresponding to the project's output format if there is a possibility of the output audio device not matching the project's format. Any differences between the project format and the system's speakermode will cause the mix to sound wrong.</para>
		/// <alert class="warning">If not loading Studio banks, do not call this unless you explicity want to change a setting from the default. FMOD will default to the speaker mode and sample rate that the OS / output prefers.</alert>
		/// </remarks>
		/// <seealso cref="SetSoftwareFormat"/>
		/// <seealso cref="SoftwareFormatChanged"/>
		/// <seealso cref="Initialize(InitFlags, int, IntPtr?)"/>
		/// <seealso cref="CloseSystem"/>
		/// <seealso cref="Constants.MAX_CHANNELS"/>
		/// <seealso cref="SpeakerMode"/>
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

		/// <summary>
		///     Gets or sets a user value that the <see cref="FmodSystem" /> object will store internally.
		/// </summary>
		/// <value>
		///     The user data.
		/// </value>
		/// <remarks>
		/// <para>This function is primarily used in case the user wishes to "attach" data to an <b>FMOD</b> object.</para>
		/// <para>It can be useful if an FMOD callback passes an object of this type as a parameter, and the user does not know which object it is (if many of these types of objects exist). </para>
		/// </remarks>
		/// <seealso cref="UserDataChanged"/>
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

		/// <summary>
		/// Sets the output format for the software mixer.
		/// </summary>
		/// <param name="sampleRate">
		/// <para>Sample rate in <i>Hz</i>, that the software mixer will run at.</para>
		/// <para>Specify values between <c>8000</c> and <c>192000</c>.</para>
		/// <para>Values out of range will be automatically clamped.</para>
		/// </param>
		/// <param name="speakerMode">Speaker setup for the software mixer.</param>
		/// <param name="rawSpeakerCount">
		/// <para>Number of output channels / speakers to initialize the sound card to in <see cref="SpeakerMode.Raw"/> mode.</para>
		/// <para>Optional. Specify <c>0</c> to ignore. Maximum of <see cref="Constants.MAX_CHANNELS"/>.</para>
		/// </param>
		/// <remarks>
		/// <para>If loading Studio banks, this must be called with speakermode corresponding to the project's output format if there is a possibility of the output audio device not matching the project's format. Any differences between the project format and the system's speakermode will cause the mix to sound wrong.</para>
		/// <alert class="warning">If not loading Studio banks, do not call this unless you explicity want to change a setting from the default. FMOD will default to the speaker mode and sample rate that the OS / output prefers.</alert>
		/// </remarks>
		/// <seealso cref="SoftwareFormat"/>
		/// <seealso cref="SoftwareFormatChanged"/>
		/// <seealso cref="Initialize(InitFlags, int, IntPtr?)"/>
		/// <seealso cref="CloseSystem"/>
		/// <seealso cref="Constants.MAX_CHANNELS"/>
		/// <seealso cref="SpeakerMode"/>
		public void SetSoftwareFormat(int sampleRate, SpeakerMode speakerMode, int rawSpeakerCount = 0)
		{
			
			NativeInvoke(FMOD_System_SetSoftwareFormat(this, sampleRate.Clamp(8000, 192000), speakerMode, rawSpeakerCount));
			SoftwareFormatChanged?.Invoke(this, EventArgs.Empty);
		}

		/// <summary>
		///     <para>
		///         This function allows the user to specify the position of their actual physical speaker to account for non
		///         standard setups.
		///     </para>
		///     <para>It also allows the user to disable speakers from 3D consideration in a game.</para>
		///     <para>
		///         The function is for describing the "real world" speaker placement to provide a more natural panning solution
		///         for 3D sound. Graphical configuration screens in an application could draw icons for speaker placement that the
		///         user could position at their will.
		///     </para>
		/// </summary>
		/// <param name="position">A <see cref="SpeakerPosition" /> object describing the location of a speaker.</param>
		/// <remarks>
		///     <para>
		///         See <see cref="SetSpeakerPosition(Speaker, float, float, bool)" /> for full explanation of these values, with
		///         examples.
		///     </para>
		/// </remarks>
		/// <seealso cref="SetSpeakerPosition(Speaker, float, float, bool)"/>
		/// <seealso cref="GetSpeakerPosition" />
		/// <seealso cref="GetSpeakerPositions" />
		/// <seealso cref="SoftwareFormat" />
		/// <seealso cref="SpeakerMode" />
		/// <seealso cref="Speaker" />
		/// <seealso cref="SpeakerPosition" />
		public void SetSpeakerPosition(SpeakerPosition position)
		{
			SetSpeakerPosition(position.Speaker, position.Location.X,
				position.Location.Y, position.IsActive);
		}

		/// <summary>
		///     <para>
		///         This function allows the user to specify the position of their actual physical speaker to account for non
		///         standard setups.
		///     </para>
		///     <para>It also allows the user to disable speakers from 3D consideration in a game.</para>
		///     <para>
		///         The function is for describing the "real world" speaker placement to provide a more natural panning solution
		///         for 3D sound. Graphical configuration screens in an application could draw icons for speaker placement that the
		///         user could position at their will.
		///     </para>
		/// </summary>
		/// <param name="speaker">The selected speaker of interest to position.</param>
		/// <param name="x">
		///     The 2D X offset in relation to the listening position. For example <c>-1.0</c> would mean the speaker
		///     is on the left, and <c>+1.0</c> would mean the speaker is on the right. <c>0.0</c> is the speaker is in the middle.
		/// </param>
		/// <param name="y">
		///     The 2D Y offset in relation to the listening position. For example <c>-1.0</c> would mean the speaker
		///     is behind the listener, and <c>+1.0</c> would mean the speaker is in front of the listener.
		/// </param>
		/// <param name="isActive">
		///     Enables or disables speaker from 3D consideration. Useful for disabling center speaker for
		///     vocals for example, or the low-frequency. <paramref name="x" /> and <paramref name="y" /> can be anything in this
		///     case.
		/// </param>
		/// <example>
		///     <para>A typical stereo setup would look like this:</para>
		///     
		///         <code language="CSharp" numberLines="true" title="Stereo Setup">
		///  system.SetSpeakerPosition(Speaker.FrontLeft, -1.0f, 0.0f, true);
		///  system.SetSpeakerPosition(Speaker.FrontRight, 1.0f, 0.0f, true);
		///  </code>
		///         <para>A typical 7.1 setup would look like this:</para>
		///         <code language="CSharp" numberLines="true" title="7.1 Surround Setup">
		///  system.SetSpeakerPosition(Speaker.FrontLeft, Util.SinFromAngle(-30), Util.CosFromAngle(-30), true);
		///  system.SetSpeakerPosition(Speaker.FrontRight, Util.SinFromAngle(30), Util.CosFromAngle(30), true);
		///  system.SetSpeakerPosition(Speaker.FrontCenter, Util.SinFromAngle(0), Util.CosFromAngle(0), true);
		///  system.SetSpeakerPosition(Speaker.LowFrequency, Util.SinFromAngle(0), Util.CosFromAngle(0), true);
		///  system.SetSpeakerPosition(Speaker.SurroundLeft, Util.SinFromAngle(-90), Util.CosFromAngle(-90), true);
		///  system.SetSpeakerPosition(Speaker.SurroundRight, Util.SinFromAngle(90), Util.CosFromAngle(90), true);
		///  system.SetSpeakerPosition(Speaker.RearLeft, Util.SinFromAngle(-150), Util.CosFromAngle(-150), true);
		///  system.SetSpeakerPosition(Speaker.RearRight, Util.SinFromAngle(150), Util.CosFromAngle(150), true);
		///  </code>
		///         <para>
		///             For reference, the <see cref="Util.SinFromAngle" /> and <see cref="Util.CosFromAngle" /> helper
		///             functions are available in the <see cref="Util" /> class. They merely provide a shortcut converting the
		///             angle from degrees to radians, performing the <see cref="Math.Sin" /> or <see cref="Math.Cos" /> functions,
		///             and casting from a <c>double</c> to a <c>float</c>.
		///         </para>
		///  <code language="CSharp" numberLines="true" title="Util Helper Functions">
		///  public const double RADIAN_FACTOR = Math.PI / 180.0;
		///  
		///  public static float SinFromAngle(float angle)
		///  {
		///  	return (float) Math.Sin(RADIAN_FACTOR * angle);
		///  }
		///  
		///  public static float CosFromAngle(float angle)
		///  {
		///  	return (float) Math.Cos(RADIAN_FACTOR * angle);
		///  }
		///  </code>
		///     </example>
		/// <remarks>
		///     <para>
		///         You could use this function to make sounds in front of your come out of different physical speakers. If you
		///         specified for example that <see cref="Speaker.RearRight" /> was in front of you at (<c>0.0, 1.0</c>) and you
		///         organized the other speakers accordingly the 3D audio would come out of the side right speaker when it was in
		///         front instead of the default which is only to the side.
		///     </para>
		///     <para>
		///         This function is also useful if speakers are not "perfectly symmetrical". For example if the center speaker
		///         was closer to the front left than the front right, this function could be used to position that center speaker
		///         accordingly and FMOD would skew the panning appropriately to make it sound correct again.
		///     </para>
		///     <para>
		///         The 2D coordinates used are only used to generate angle information. Size / distance does not matter in
		///         FMOD's implementation because it is not FMOD's job to attenuate or amplify the signal based on speaker
		///         distance. If it amplified the signal in the digital domain the audio could clip/become distorted. It is better
		///         to use the amplifier's analogue level capabilities to balance speaker volumes.
		///     </para>
		///     <para>
		///         Setting the <see cref="SoftwareFormat" /> property overrides these values, so this property must be changed
		///         after this.
		///     </para>
		/// </remarks>
		/// <seealso cref="GetSpeakerPosition" />
		/// <seealso cref="GetSpeakerPositions" />
		/// <seealso cref="SoftwareFormat" />
		/// <seealso cref="SpeakerMode" />
		/// <seealso cref="Speaker" />
		/// <seealso cref="SpeakerPosition" />
		public void SetSpeakerPosition(Speaker speaker, float x, float y, bool isActive = true)
		{
			NativeInvoke(FMOD_System_SetSpeakerPosition(this, speaker, x, y, isActive));
			SpeakerPositionChanged?.Invoke(this, EventArgs.Empty);
		}

		/// <summary>
		///     <para>
		///         This function allows the user to specify the position of their actual physical speaker to account for non
		///         standard setups.
		///     </para>
		///     <para>It also allows the user to disable speakers from 3D consideration in a game.</para>
		///     <para>
		///         The function is for describing the "real world" speaker placement to provide a more natural panning solution
		///         for 3D sound. Graphical configuration screens in an application could draw icons for speaker placement that the
		///         user could position at their will.
		///     </para>
		/// </summary>
		/// <param name="speaker">The selected speaker of interest to position.</param>
		/// <param name="location">
		///     The 2D X and Y offset in relation to the listening position.
		///     <see cref="SetSpeakerPosition(Speaker, float, float, bool)" /> for full explanation of these values.
		/// </param>
		/// <param name="isActive">
		///     Enables or disables speaker from 3D consideration. Useful for disabling center speaker for
		///     vocals for example, or the low-frequency. <paramref name="location" /> can be anything in this case.
		/// </param>
		/// <remarks>
		///     <para>
		///         See <see cref="SetSpeakerPosition(Speaker, float, float, bool)" /> for full explanation of these values, with
		///         examples.
		///     </para>
		/// </remarks>
		/// <seealso cref="SetSpeakerPosition(Speaker, float, float, bool)"/>
		/// <seealso cref="GetSpeakerPosition" />
		/// <seealso cref="GetSpeakerPositions" />
		/// <seealso cref="SoftwareFormat" />
		/// <seealso cref="SpeakerMode" />
		/// <seealso cref="Speaker" />
		/// <seealso cref="SpeakerPosition" />
		public void SetSpeakerPosition(Speaker speaker, PointF location, bool isActive = true)
		{
			SetSpeakerPosition(speaker, location.X, location.Y, isActive);
		}

		/// <summary>
		///     <para>
		///         This function allows the user to specify the position of their actual physical speaker to account for non
		///         standard setups.
		///     </para>
		///     <para>It also allows the user to disable speakers from 3D consideration in a game.</para>
		///     <para>
		///         The function is for describing the "real world" speaker placement to provide a more natural panning solution
		///         for 3D sound. Graphical configuration screens in an application could draw icons for speaker placement that the
		///         user could position at their will.
		///     </para>
		/// </summary>
		/// <param name="speakerPositions">
		///     An array of <see cref="SpeakerPosition" /> objects describing the location of the
		///     speakers.
		/// </param>
		/// <remarks>
		///     <para>
		///         See <see cref="SetSpeakerPosition(Speaker, float, float, bool)" /> for full explanation of these values, with
		///         examples.
		///     </para>
		/// </remarks>
		/// <seealso cref="GetSpeakerPosition" />
		/// <seealso cref="GetSpeakerPositions" />
		/// <seealso cref="SoftwareFormat" />
		/// <seealso cref="SpeakerMode" />
		/// <seealso cref="Speaker" />
		/// <seealso cref="SpeakerPosition" />
		public void SetSpeakerPositions(SpeakerPosition[] speakerPositions)
		{
			foreach (var position in speakerPositions)
				SetSpeakerPosition(position);
		}

		/// <summary>
		///     Suspend mixer thread and relinquish usage of audio hardware while maintaining internal state.
		/// </summary>
		/// <remarks>
		///     <para>Used on mobile platforms when entering a backgrounded state to reduce CPU to 0%.</para>
		///     <para>All internal state will be maintained, i.e. created sound and channels will stay available in memory.</para>
		/// </remarks>
		public void SuspendMixer()
		{
			NativeInvoke(FMOD_System_MixerSuspend(this));
			MixerSuspended?.Invoke(this, EventArgs.Empty);
		}

		/// <summary>
		///     Unloads a plugin from memory.
		/// </summary>
		/// <param name="pluginHandle">Handle to a pre-existing plugin.</param>
		/// <seealso cref="LoadPlugin" />
		public void UnloadPlugin(uint pluginHandle)
		{
			NativeInvoke(FMOD_System_UnloadPlugin(this, pluginHandle));
			PluginUnloaded?.Invoke(this, EventArgs.Empty);
		}

		/// <summary>
		///     Mutual exclusion function to unlock the FMOD DSP engine (which runs asynchronously in another thread) and let it
		///     continue executing.
		/// </summary>
		/// <remarks>The DSP engine must be locked with <see cref="LockDsp" /> before this function is called.</remarks>
		/// <seealso cref="LockDsp" />
		public void UnlockDsp()
		{
			NativeInvoke(FMOD_System_UnlockDSP(this));
			DspUnlocked?.Invoke(this, EventArgs.Empty);
		}

		/// <summary>
		///     <para>Updates the <see cref="FmodSystem" />.</para>
		///     <para>This should be called once per 'game' tick, or once per frame in your application.</para>
		/// </summary>
		/// <remarks>
		///     This updates the following things:
		///     <list type="bullet">
		///         <item>
		///             <para>3D Sound. <b>Update</b> must be called to get 3D positioning. </para>
		///         </item>
		///         <item>
		///             <para>
		///                 Virtual voices. If more voices are played than there are real voices, <b>Update</b> must be
		///                 called to handle the virtualization.
		///             </para>
		///         </item>
		///         <item>
		///             <para>*_NRT output modes. <b>Update</b> must be called to drive the output for these output modes. </para>
		///         </item>
		///         <item>
		///             <para>
		///                 <see cref="InitFlags.StreamFromUpdate" />. <b>Update</b> must be called to update the
		///                 streamer if this flag has been used.
		///             </para>
		///         </item>
		///         <item>
		///             <para>Callbacks. <b>Update</b> must be called to fire callbacks if they are specified. </para>
		///         </item>
		///         <item>
		///             <para></para>
		///             <see cref="Mode.NonBlocking" />. <b>Update</b> must be called to make sounds opened with
		///             <see cref="Mode.NonBlocking" /> flag to work properly.
		///         </item>
		///     </list>
		///     <para>
		///         If <see cref="OutputType.NoSoundNrt" /> or <see cref="OutputType.WavWriterNrt" /> output modes are used, this
		///         function also drives the software / DSP engine, instead of it running asynchronously in a thread as is the
		///         default behaviour.
		///     </para>
		///     <para>
		///         This can be used for faster than realtime updates to the decoding or DSP engine which might be useful if the
		///         output is the wav writer for example.
		///     </para>
		///     <para>
		///         If <see cref="InitFlags.StreamFromUpdate" /> is used, this function will update the stream engine. Combining
		///         this with the non realtime output will mean smoother captured output.
		///     </para>
		/// </remarks>
		/// <seealso cref="Initialize(InitFlags, int, IntPtr?)" />
		/// <seealso cref="InitFlags" />
		/// <seealso cref="OutputType" />
		/// <seealso cref="Mode" />
		public void Update()
		{
			NativeInvoke(FMOD_System_Update(this));
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="FmodSystem"/> class.
		/// </summary>
		/// <param name="handle">The handle, created internally via <seealso cref="Create"/>.</param>
		/// <seealso cref="Create"/>
		/// <seealso cref="Initialize(InitFlags, int, IntPtr?)"/>
		private FmodSystem(IntPtr handle) : base(handle)
		{
		}

		private void SelfUpdate(object state)
		{
			Update();
		}

		#endregion

		#endregion
	}
}