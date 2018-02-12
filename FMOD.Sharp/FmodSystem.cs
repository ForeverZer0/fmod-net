using System;
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

		public Dsp CreateDspFromType(DspType dspType)
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

		public event EventHandler ChannelGroupCreated;
		public event EventHandler SoundGroupCreated;

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

		public int DriversCount
		{
			get
			{
				NativeInvoke(FMOD_System_GetNumDrivers(this, out var count));
				return count;
			}
		}
	}
	
}