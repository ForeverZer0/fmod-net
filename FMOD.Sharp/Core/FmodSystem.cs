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

		/// <summary>
		/// Occurs when a new <see cref="Geometry"/> is created.
		/// </summary>
		/// <seealso cref="Geometry"/>
		/// <seealso cref="CreateGeometry"/>
		/// <seealso cref="LoadGeometry(string)"/>
		/// <seealso cref="LoadGeometry(byte[])"/>
		/// <seealso cref="LoadGeometry(IntPtr, int)"/>
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



		#endregion

		#region Methods






		public void AttachFileSystem(FileOpenCallback userOpen, FileCloseCallback userClose, FileReadCallback userRead,
			FileSeekCallback userSeek)
		{
			NativeInvoke(FMOD_System_AttachFileSystem(this, userOpen, userClose, userRead, userSeek));
		}





		public ChannelGroup CreateChannelGroup(string name)
		{
			var bytesName = Encoding.UTF8.GetBytes(name);
			NativeInvoke(FMOD_System_CreateChannelGroup(this, bytesName, out var group));
			ChannelGroupCreated?.Invoke(this, EventArgs.Empty);
			return CoreHelper.Create<ChannelGroup>(group);
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

		public Attributes3D GetListenerAttributes(int listener)
		{
			NativeInvoke(FMOD_System_Get3DListenerAttributes(this, listener, out var position,
				out var velocity, out var forward, out var up));
			return new Attributes3D
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

		public uint LoadPlugin(string path, uint priority = 128u)
		{
			var bytes = Encoding.UTF8.GetBytes(path);
			NativeInvoke(FMOD_System_LoadPlugin(this, bytes, out var pluginHandle, priority));
			PluginLoaded?.Invoke(this, EventArgs.Empty);
			return pluginHandle;
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

		public void SetListenerAttributes(int listener, Attributes3D attributes)
		{
			SetListenerAttributes(listener, attributes.Position, attributes.Velocity, attributes.Forward, attributes.Up);
		}

		public void SetListenerAttributes(int listener, Vector position, Vector velocity, Vector forward, Vector up)
		{
			NativeInvoke(FMOD_System_Set3DListenerAttributes(this, listener, ref position, ref velocity, ref forward, ref up));
			ListenerAttributesChanged?.Invoke(this, EventArgs.Empty);
		}


		public Channel PlayDsp(Dsp dsp, bool paused = false, ChannelGroup group = null)
		{
			NativeInvoke(FMOD_System_PlayDSP(this, dsp, group ?? IntPtr.Zero, paused, out var channel));
			DspPlayed?.Invoke(this, EventArgs.Empty);
			return CoreHelper.Create<Channel>(channel);
		}





		#region Documentation Complete

		/// <summary>
		/// <para>Retrieves the amount of dedicated sound ram available if the platform supports it.</para>
		/// <para>Most platforms use main ram to store audio data, so this function usually isn't necessary.</para>
		/// </summary>
		/// <returns>A <see cref="RamUsage"/> describing the current use of RAM by <b>FMOD</b>.</returns>
		/// <seealso cref="O:FMOD.Core.MemoryManager.GetStats"/>
		/// <seealso cref="RamUsage"/>
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

		/// <summary>
		/// Plays a <see cref="Sound"/> object on a particular channel and <see cref="ChannelGroup"/> if desired.
		/// </summary>
		/// <param name="sound">The sound to play.<lineBreak/> This is opened with <see cref="O:FMOD.Core.FmodSystem.CreateSound"/> or <see cref="O:FMOD.Core.FmodSystem.CreateStream"/>. </param>
		/// <param name="paused"><c>true</c> or <c>false</c> flag to specify whether to start the channel paused or not. Starting a channel paused allows the user to alter its attributes without it being audible, and unpausing with <see cref="ChannelControl.Resume"/> actually starts the sound.</param>
		/// <param name="group">
		/// <para>A <see cref="ChannelGroup"/> to become a member of.</para>
		/// <para>This is more efficient than using <see cref="Channel.ChannelGroup"/>, as it does it during the channel setup, rather than connecting to the master channel group, then later disconnecting and connecting to the new <see cref="ChannelGroup"/> when specified.</para>
		/// <para>Optional. Use <c>null</c> to ignore (use <see cref="MasterChannelGroup"/>).</para>
		/// </param>
		/// <returns>A newly created playing <see cref="Channel"/>.</returns>
		/// <remarks>
		/// <para>When a sound is played, it will use the sound's default frequency and priority.</para>
		/// <para>
		/// A sound defined as <see cref="Mode.ThreeD"/> will by default play at the position of the listener.<lineBreak/>
		/// To set the 3D position of the channel before the sound is audible, start the channel paused by setting the paused flag to true, and calling <see cref="ChannelControl.SetAttributes3D"/>. Following that, unpause the channel with <see cref="ChannelControl.Pause"/>.
		/// </para>
		/// <para>
		/// Channels are reference counted. If a channel is stolen by the FMOD priority system, then the handle to the stolen voice becomes invalid, and Channel based commands will not affect the new sound playing in its place.<lineBreak/>
		/// If all channels are currently full playing a sound, FMOD will steal a channel with the lowest priority sound.<lineBreak/>
		/// If more channels are playing than are currently available on the soundcard/sound device or software mixer, then FMOD will 'virtualize' the channel. This type of channel is not heard, but it is updated as if it was playing. When its priority becomes high enough or another sound stops that was using a real hardware/software channel, it will start playing from where it should be. This technique saves CPU time (thousands of sounds can be played at once without actually being mixed or taking up resources), and also removes the need for the user to manage voices themselves.<lineBreak/>
		/// An example of virtual channel usage is a dungeon with 100 torches burning, all with a looping crackling sound, but with a soundcard that only supports 32 hardware voices. If the 3D positions and priorities for each torch are set correctly, FMOD will play all 100 sounds without any 'out of channels' errors, and swap the real voices in and out according to which torches are closest in 3D space.<lineBreak/>
		/// Priority for virtual channels can be changed in the sound's defaults, or at runtime with <see cref="Channel.Priority"/>.
		/// </para>
		/// </remarks>
		/// <seealso cref="O:FMOD.Core.FmodSystem.CreateSound"/>
		/// <seealso cref="O:FMOD.Core.FmodSystem.CreateStream"/>
		/// <seealso cref="Channel"/>
		/// <seealso cref="ChannelGroup"/>
		/// <seealso cref="Channel.Priority"/>
		/// <seealso cref="Mode"/>
		/// <seealso cref="ChannelControl.SetAttributes3D"/>
		/// <seealso cref="ChannelControl.Paused"/>
		/// <seealso cref="O:FMOD.Core.FmodSystem.Initialize"/>
		public Channel PlaySound(Sound sound, bool paused = false, ChannelGroup group = null)
		{
			NativeInvoke(FMOD_System_PlaySound(this, sound, group ?? IntPtr.Zero, paused, out var channel));
			SoundPlayed?.Invoke(this, EventArgs.Empty);
			return CoreHelper.Create<Channel>(channel);
		}

		/// <summary>
		/// Creates a geometry object from a block of memory which contains pre-saved geometry data, saved by <see cref="Geometry.Save"/>.
		/// </summary>
		/// <param name="filename">The path to a file containing serialized geometry data.</param>
		/// <returns>A newly created <see cref="Geometry"/> object.</returns>
		/// <seealso cref="Geometry"/>
		/// <seealso cref="GeometryCreated"/>
		/// <seealso cref="CreateGeometry"/>
		/// <seealso cref="Geometry.Save"/>
		/// <seealso cref="Geometry.Serialize"/>
		public Geometry LoadGeometry(string filename)
		{
			return LoadGeometry(File.ReadAllBytes(filename));
		}

		/// <summary>
		/// Creates a geometry object from a block of memory which contains pre-saved geometry data, saved by <see cref="Geometry.Save"/>.
		/// </summary>
		/// <param name="binary">An <see cref="T:byte[]"/> of serialized geometry data.</param>
		/// <returns>A newly created <see cref="Geometry"/> object.</returns>
		/// <seealso cref="Geometry"/>
		/// <seealso cref="GeometryCreated"/>
		/// <seealso cref="CreateGeometry"/>
		/// <seealso cref="Geometry.Save"/>
		/// <seealso cref="Geometry.Serialize"/>
		public Geometry LoadGeometry(byte[] binary)
		{
			var gcHandle = GCHandle.Alloc(binary, GCHandleType.Pinned);
			var geometry = LoadGeometry(gcHandle.AddrOfPinnedObject(), binary.Length);
			gcHandle.Free();
			return geometry;
		}

		/// <summary>
		/// Creates a geometry object from a block of memory which contains pre-saved geometry data, saved by <see cref="Geometry.Save"/>.
		/// </summary>
		/// <param name="data">Address of data containing pre-saved geometry data.</param>
		/// <param name="dataSize">Size of geometry data block in bytes.</param>
		/// <returns>A newly created <see cref="Geometry"/> object.</returns>
		/// <seealso cref="Geometry"/>
		/// <seealso cref="GeometryCreated"/>
		/// <seealso cref="CreateGeometry"/>
		/// <seealso cref="Geometry.Save"/>
		/// <seealso cref="Geometry.Serialize"/>
		public Geometry LoadGeometry(IntPtr data, int dataSize)
		{
			NativeInvoke(FMOD_System_LoadGeometry(this, data, dataSize, out var geometry));
			GeometryCreated?.Invoke(this, EventArgs.Empty);
			return CoreHelper.Create<Geometry>(geometry);
		}

		/// <summary>
		/// Creates a user defined DSP unit object to be inserted into a DSP network, for the purposes of sound filtering or sound generation.
		/// </summary>
		/// <param name="description">
		/// <para>A <see cref="DspDescription"/> structure containing information about the unit to be created.</para>
		/// <para>Some members of <see cref="DspDescription"/> are referenced directly inside <b>FMOD</b> so the structure should be allocated statically or at least remain in memory for the lifetime of the system.</para> 
		/// </param>
		/// <returns>A newly created <see cref="Dsp"/> object.</returns>
		/// <remarks>
		/// A DSP unit can generate or filter incoming data.<lineBreak/>
		/// The data is created or filtered through use of the read callback that is defined by the user.<lineBreak/>
		/// See the definition for the <see cref="DspDescription"/> structure to find out what each member means.<lineBreak/>
		/// To be active, a unit must be inserted into the <b>FMOD</b> DSP network to be heard. Use functions such as <see cref="ChannelControl.AddDsp(Dsp, int)"/>, or <see cref="Dsp.AddInput"/> to do this.
		/// </remarks>
		/// <seealso cref="Dsp"/>
		/// <seealso cref="DspCreated"/>
		/// <seealso cref="DspType"/>
		/// <seealso cref="DspDescription"/>
		/// <seealso cref="ChannelControl.AddDsp(Dsp, int)"/>
		/// <seealso cref="Dsp.AddInput"/>
		/// <seealso cref="Dsp.Active"/>
		/// <seealso cref="LoadPlugin"/>
		/// <seealso cref="CreateDspByType"/>
		/// <seealso cref="CreateDspByType{T}"/>
		/// <seealso cref="CreateDspByPlugin"/>
		public Dsp CreateDsp(DspDescription description)
		{
			NativeInvoke(FMOD_System_CreateDSP(this, ref description, out var dsp));
			DspCreated?.Invoke(this, EventArgs.Empty);
			return CoreHelper.Create<Dsp>(dsp);
		}

		/// <summary>
		/// <para>Creates an <b>FMOD</b> defined built in DSP unit object to be inserted into a DSP network, for the purposes of sound filtering or sound generation</para>.
		/// <para>This function is used to create special effects that come built into <b>FMOD</b>.</para>
		/// </summary>
		/// <typeparam name="T">A <see cref="Type"/> derived from the abstract <see cref="Dsp"/> class.</typeparam>
		/// <returns>A newly created <see cref="Dsp"/> object.</returns>
		/// <remarks>
		/// <para>
		/// A DSP unit can generate or filter incoming data.<lineBreak/>
		/// The data is created or filtered through use of the read callback that is defined by the user.<lineBreak/>
		/// To be active, a unit must be inserted into the <b>FMOD</b> DSP network to be heard. Use functions such as <see cref="ChannelControl.AddDsp(Dsp, int)"/>, or <see cref="Dsp.AddInput"/> to do this.
		/// </para>
		/// <alert class="note">
		/// <para>Winamp DSP and VST plugins will only return the first plugin of this type that was loaded!</para>
		/// <para>To access all VST or Winamp DSP plugins the <see cref="CreateDspByPlugin"/> function! Use the index returned by <see cref="LoadPlugin"/> if you don't want to enumerate them all.</para>
		/// </alert>
		/// </remarks>
		/// <seealso cref="Dsp"/>
		/// <seealso cref="DspCreated"/>
		/// <seealso cref="DspType"/>
		/// <seealso cref="DspDescription"/>
		/// <seealso cref="ChannelControl.AddDsp(Dsp, int)"/>
		/// <seealso cref="Dsp.AddInput"/>
		/// <seealso cref="Dsp.Active"/>
		/// <seealso cref="LoadPlugin"/>
		/// <seealso cref="CreateDsp"/>
		/// <seealso cref="CreateDspByType"/>
		/// <seealso cref="CreateDspByPlugin"/>
		public T CreateDspByType<T>() where T : Dsp
		{
			if (Enum.TryParse<DspType>(typeof(T).Name, true, out var dspType))
				return (T) CreateDspByType(dspType);
			return null;
		}

		/// <summary>
		/// <para>Creates an <b>FMOD</b> defined built in DSP unit object to be inserted into a DSP network, for the purposes of sound filtering or sound generation</para>
		/// <para>This function is used to create special effects that come built into <b>FMOD</b>.</para>
		/// </summary>
		/// <param name="dspType">A pre-defined DSP effect or sound generator described by a <see cref="DspType"/>.</param>
		/// <returns>A newly created <see cref="Dsp"/> object.</returns>
		/// <remarks>
		/// <para>
		/// A DSP unit can generate or filter incoming data.<lineBreak/>
		/// The data is created or filtered through use of the read callback that is defined by the user.<lineBreak/>
		/// To be active, a unit must be inserted into the <b>FMOD</b> DSP network to be heard. Use functions such as <see cref="ChannelControl.AddDsp(Dsp, int)"/>, or <see cref="Dsp.AddInput"/> to do this.
		/// </para>
		/// <alert class="note">
		/// <para>Winamp DSP and VST plugins will only return the first plugin of this type that was loaded!</para>
		/// <para>To access all VST or Winamp DSP plugins the <see cref="CreateDspByPlugin"/> function! Use the index returned by <see cref="LoadPlugin"/> if you don't want to enumerate them all.</para>
		/// </alert>
		/// </remarks>
		/// <seealso cref="Dsp"/>
		/// <seealso cref="DspCreated"/>
		/// <seealso cref="DspType"/>
		/// <seealso cref="DspDescription"/>
		/// <seealso cref="ChannelControl.AddDsp(Dsp, int)"/>
		/// <seealso cref="Dsp.AddInput"/>
		/// <seealso cref="Dsp.Active"/>
		/// <seealso cref="LoadPlugin"/>
		/// <seealso cref="CreateDsp"/>
		/// <seealso cref="CreateDspByType{T}"/>
		/// <seealso cref="CreateDspByPlugin"/>
		public Dsp CreateDspByType(DspType dspType)
		{
			NativeInvoke(FMOD_System_CreateDSPByType(this, dspType, out var dsp));
			DspCreated?.Invoke(this, EventArgs.Empty);
			return Dsp.FromType(dsp, dspType);
		}

		/// <summary>
		/// <para>Creates a <see cref="Dsp"/> unit object which is either built in or loaded as a plugin, to be inserted into a DSP network, for the purposes of sound filtering or sound generation.</para>
		/// <para>This function creates a DSP unit that can be enumerated by using <see cref="GetPluginCount"/> and <see cref="GetPluginInfo"/>.</para>
		/// </summary>
		/// <param name="pluginHandle">A handle to a pre-existing DSP plugin, loaded by <see cref="LoadPlugin"/>.</param>
		/// <returns>A newly created <see cref="Dsp"/> object.</returns>
		/// <remarks>
		/// <para>
		/// A DSP unit can generate or filter incoming data.<lineBreak/>
		/// To be active, a unit must be inserted into the <b>FMOD</b> DSP network to be heard. Use functions such as <see cref="ChannelControl.AddDsp(Dsp, int)"/>, or <see cref="Dsp.AddInput"/> to do this.
		/// </para>
		/// </remarks>
		/// <seealso cref="Dsp"/>
		/// <seealso cref="DspCreated"/>
		/// <seealso cref="DspType"/>
		/// <seealso cref="DspDescription"/>
		/// <seealso cref="ChannelControl.AddDsp(Dsp, int)"/>
		/// <seealso cref="Dsp.AddInput"/>
		/// <seealso cref="Dsp.Active"/>
		/// <seealso cref="LoadPlugin"/>
		/// <seealso cref="CreateDsp"/>
		/// <seealso cref="CreateDspByType{T}"/>
		/// <seealso cref="CreateDspByType"/>
		public Dsp CreateDspByPlugin(uint pluginHandle)
		{
			NativeInvoke(FMOD_System_CreateDSPByPlugin(this, pluginHandle, out var dsp));
			DspCreated?.Invoke(this, EventArgs.Empty);
			return CoreHelper.Create<Dsp>(dsp);
		}

		/// <summary>
		/// Route the signal from a channel group into a seperate audio port on the output driver.
		/// </summary>
		/// <param name="portType">Output driver specific audio port type. See extra platform specific header (if it exists) for port numbers, i.e. fmod_psvita.h, fmod_wiiu.h, fmodorbis.h.</param>
		/// <param name="portIndex">Output driver specific index of the audio port.</param>
		/// <param name="channelGroup"><see cref="ChannelGroup"/> to route away to the new port.</param>
		/// <param name="passThru">If <c>true</c> the signal will continue to be passed through to the main mix, if <c>false</c> the signal will be entirely to the designated port.</param>
		/// <remarks>
		/// Note that an <b>FMOD</b> port is a hardware specific reference, to hardware devices that exist on only certain platforms (like a console headset, or dedicated hardware music channel for example). It is not supported on all platforms.
		/// </remarks>
		/// <seealso cref="DetachChannelGroupFromPort"/>
		/// <seealso cref="ChannelGroup"/>
		/// <seealso cref="ChannelGroupAttached"/>
		public void AttachChannelGroupToPort(uint portType, ulong portIndex, ChannelGroup channelGroup, bool passThru)
		{
			NativeInvoke(FMOD_System_AttachChannelGroupToPort(this, portType, portIndex, channelGroup, passThru));
			ChannelGroupAttached?.Invoke(this, EventArgs.Empty);
		}

		/// <summary>
		/// Disconnect a channel group from a port and route audio back to the default port of the output driver.
		/// </summary>
		/// <param name="channelGroup"><see cref="ChannelGroup"/> to route away back to the default audio port.</param>
		/// <seealso cref="ChannelGroup"/>
		/// <seealso cref="AttachChannelGroupToPort"/>
		/// <seealso cref="ChannelGroupDetached"/>
		public void DetachChannelGroupFromPort(ChannelGroup channelGroup)
		{
			NativeInvoke(FMOD_System_DetachChannelGroupFromPort(this, channelGroup));
			ChannelGroupDetached?.Invoke(this, EventArgs.Empty);
		}

		/// <summary>
		/// <para>Closes the <see cref="FmodSystem"/> object without freeing the object's memory, so the system handle will still be valid.</para>
		/// <para>Closing the output renders objects created with this system object invalid. Make sure any instance of a <see cref="Sound"/>, <see cref="ChannelControl"/>, <see cref="Geometry"/>, or <see cref="Dsp"/> is disposed before calling this function.</para>
		/// </summary>
		/// <seealso cref="Initialize()"/>
		/// <seealso cref="Dispose"/>
		public void CloseSystem()
		{
			NativeInvoke(FMOD_System_Close(this));
			Closed?.Invoke(this, EventArgs.Empty);
		}

		/// <summary>
		/// <para>Creates and returns a new <see cref="FmodSystem"/> object.</para>
		/// <para>This must be called to create an <see cref="FmodSystem"/> object before you can do anything else. Use this function to create 1, or multiple instances of system objects.</para>
		/// </summary>
		/// <returns>A newly created <see cref="FmodSystem"/> object.</returns>
		/// <remarks><alert class="warning">
		/// Calls to this method and <see cref="Dispose"/> are not thread-safe. Do not call these functions simultaneously from multiple threads at once.
		/// </alert></remarks>
		/// <seealso cref="Initialize()"/>
		/// <seealso cref="Dispose"/>
		public static FmodSystem Create()
		{
			NativeInvoke(FMOD_System_Create(out var pointer));
			var system = new FmodSystem(pointer);
			CoreHelper.AddReference(pointer, system);
			return system;
		}

		/// <summary>
		/// <para>Gets the handle to the system level output device module.</para>
		/// <para>This means a pointer to a DirectX "LPDIRECTSOUND", or a WINMM handle, or with something like with <see cref="OutputType.NoSound"/> output, the handle will be <see cref="IntPtr.Zero"/>.</para>
		/// </summary>
		/// <value>
		/// The output handle.
		/// </value>
		/// <remarks>
		/// Must be called after <seealso cref="Initialize()"/>.<lineBreak/>
		/// Cast the resulting pointer depending on what output system pointer you are after.
		/// </remarks>
		/// <seealso cref="Initialize()"/>
		/// <seealso cref="OutputType"/>
		/// <seealso cref="Output"/>
		public IntPtr OutputHandle
		{
			get
			{
				NativeInvoke(FMOD_System_GetOutputHandle(this, out var outputHandle));
				return outputHandle;
			}
		}

		/// <summary>
		/// Gets the number of currently playing channels, both real and virtual.
		/// </summary>
		/// <value>
		/// The number of real and virtual channels currently playing.
		/// </value>
		/// <seealso cref="ChannelControl.IsPlaying"/>
		/// <seealso cref="Channel.IsVirtual"/>
		/// <seealso cref="PlayingRealChannelCount"/>
		public int PlayingChannelCount
		{
			get
			{
				NativeInvoke(FMOD_System_GetChannelsPlaying(this, out var count, out var dummy));
				return count;
			}
		}

		/// <summary>
		/// Gets the number of currently playing channels, excluding virtual channels.
		/// </summary>
		/// <value>
		/// The number of real channels currently playing.
		/// </value>
		/// <seealso cref="ChannelControl.IsPlaying"/>
		/// <seealso cref="Channel.IsVirtual"/>
		/// <seealso cref="PlayingChannelCount"/>
		public int PlayingRealChannelCount
		{
			get
			{
				NativeInvoke(FMOD_System_GetChannelsPlaying(this, out var dummy, out var count));
				return count;
			}
		}

		/// <summary>
		/// Creates and returns a base geometry object which can then have polygons added to it.
		/// </summary>
		/// <param name="maxPolygons">Maximum number of polygons within this object. </param>
		/// <param name="maxVertices">Maximum number of vertices within this object. </param>
		/// <returns>A newly created <see cref="Geometry"/> object.</returns>
		/// <remarks>
		/// <para>Polygons can be added to a geometry object using <see cref="Geometry.AddPolygon(Polygon)"/>.</para>
		/// <para>
		/// A geometry object stores its list of polygons in a structure optimized for quick line intersection testing and efficient insertion and updating.<lineBreak/>
		/// The structure works best with regularly shaped polygons with minimal overlap.<lineBreak/>
		/// Many overlapping polygons, or clusters of long thin polygons may not be handled efficiently.<lineBreak/>
		/// Axis aligned polygons are handled most efficiently.
		/// </para>
		/// <para>The same type of structure is used to optimize line intersection testing with multiple geometry objects.</para>
		/// <para>
		/// It is important to set the value of maxworldsize to an appropriate value using <see cref="WorldSize"/>.<lineBreak/>
		/// Objects or polygons outside the range of <see cref="WorldSize"/> will not be handled efficiently.<lineBreak/>
		/// Conversely, if <see cref="WorldSize"/> is excessively large, the structure may lose precision and efficiency may drop.
		/// </para>
		/// </remarks>
		/// <seealso cref="Geometry"/>
		/// <seealso cref="GeometryCreated"/>
		/// <seealso cref="LoadGeometry(string)"/>
		/// <seealso cref="Geometry.AddPolygon(Polygon)"/>
		/// <seealso cref="WorldSize"/>
		public Geometry CreateGeometry(int maxPolygons, int maxVertices)
		{
			NativeInvoke(FMOD_System_CreateGeometry(this, maxPolygons, maxVertices, out var geometry));
			GeometryCreated?.Invoke(this, EventArgs.Empty);
			return CoreHelper.Create<Geometry>(geometry);
		}

		/// <summary>
		/// <para>Creates a "virtual reverb" object. This object reacts to 3D location and morphs the reverb environment based on how close it is to the reverb object's center.</para>
		/// <para>Multiple reverb objects can be created to achieve a multi-reverb environment. One physical reverb object is used for all 3D reverb objects (slot <c>0</c> by default).</para>
		/// </summary>
		/// <returns>A newly created <see cref="Reverb"/> object.</returns>
		/// <remarks>
		/// <para>
		/// The 3D reverb object is a sphere having 3D attributes (position, minimum distance, maximum distance) and reverb properties.<lineBreak/>
		/// The properties and 3D attributes of all reverb objects collectively determine, along with the listener's position, the settings of and input gains into a single 3D reverb DSP.<lineBreak/>
		/// When the listener is within the sphere of effect of one or more 3D reverbs, the listener's 3D reverb properties are a weighted combination of such 3D reverbs.<lineBreak/>
		/// When the listener is outside all of the reverbs, no reverb is applied.
		/// </para>
		/// <para>
		/// In <b>FMODEx</b> a special "ambient" reverb setting was used when outside the influence of all reverb spheres. This function no longer exists.<lineBreak/>
		/// To avoid this reverb intefering with the reverb slot used by the 3D reverb, 2D reverb should use a different slot ID with <see cref="SetReverbProperties"/>, otherwise <see cref="FMOD.Structures.AdvancedSettings.ReverbInstance"/> can also be used to place 3D reverb on a different physical reverb slot. Use <see cref="ChannelControl.SetReverbProperties"/> to turn off reverb for 2D sounds (ie set wet = <c>0</c>).
		/// </para>
		/// <para>Creating multiple reverb objects does not impact performance. These are "virtual reverbs". There will still be only one physical reverb DSP running that just morphs between the different virtual reverbs.</para>
		/// <para>
		/// Note about physical reverb <seealso cref="Dsp"/> unit allocation. To remove the DSP unit and the associated CPU cost, first make sure all 3D reverb objects are released. Then call <see cref="SetReverbProperties"/> with the 3D reverb's slot ID (default is <c>0</c>) with a property point of <c>null</c>, to signal that the physical reverb instance should be deleted.
		/// </para>
		/// <para>If a 3D reverb is still present, and <see cref="SetReverbProperties"/> function is called to free the physical reverb, the 3D reverb system will immediately recreate it upon the next <see cref="Update"/> call.</para>
		/// </remarks>
		/// <seealso cref="Reverb"/>
		/// <seealso cref="ReverbCreated"/>
		/// <seealso cref="Reverb.Dispose"/>
		/// <seealso cref="GetReverbProperties"/>
		/// <seealso cref="SetReverbProperties"/>
		/// <seealso cref="Update"/>
		/// <seealso cref="T:FMOD.Structures.AdvancedSettings"/>
		/// <seealso cref="FmodSystem.AdvancedSettings"/>
		public Reverb CreateReverb()
		{
			NativeInvoke(FMOD_System_CreateReverb3D(this, out var reverb));
			ReverbCreated?.Invoke(this, EventArgs.Empty);
			return CoreHelper.Create<Reverb>(reverb);
		}

		/// <summary>
		/// Gets or sets the maximum number of software mixed channels possible.
		/// <para>Default = 64.</para>
		/// </summary>
		/// <value>
		/// The software channels.
		/// </value>
		/// <remarks>This function cannot be called after <b>FMOD</b> is already activated, it must be called before <see cref="Initialize()"/>, or after <see cref="CloseSystem"/>.</remarks>
		/// <seealso cref="Initialize()"/>
		/// <seealso cref="CloseSystem"/>
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

		/// <summary>
		/// Gets the current version of <b>FMOD</b> being used.
		/// </summary>
		/// <value>
		/// The version.
		/// </value>
		public Version Version
		{
			get
			{
				NativeInvoke(FMOD_System_GetVersion(this, out var version));
				return Util.UInt32ToVersion(version);
			}
		}

		/// <summary>
		/// Gets or sets the maximum world size for the geometry engine.
		/// </summary>
		/// <value>
		/// The maximum world size.
		/// </value>
		/// <remarks>
		/// <para>
		/// Setting maxworldsize should be done first before creating any <see cref="Geometry"/>.<lineBreak/>
		/// It can be done any time afterwards but may be slow in this case.
		/// </para>
		/// <para>
		/// Objects or polygons outside the range of maxworldsize will not be handled efficiently.<lineBreak/>
		/// Conversely, if maxworldsize is excessively large, the structure may loose precision and efficiency may drop.
		/// </para>
		/// </remarks>
		/// <seealso cref="CreateGeometry"/>
		/// <seealso cref="Geometry"/>
		/// <seealso cref="WorldSizeChanged"/>
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
		/// Occurs when a <see cref="Dsp"/> is created.
		/// </summary>
		/// <seealso cref="CreateDsp"/>
		/// <seealso cref="CreateDspByType"/>
		/// <seealso cref="CreateDspByType{T}"/>
		/// <seealso cref="CreateDspByPlugin"/>
		public event EventHandler DspCreated;

		/// <summary>
		/// Occurs when <see cref="LockDsp"/> is invoked.
		/// </summary>
		/// <seealso cref="Dsp"/>
		/// <seealso cref="LockDsp"/>
		/// <seealso cref="UnlockDsp"/>
		public event EventHandler DspLocked;

		/// <summary>
		/// Occurs when a <see cref="Dsp"/> is played.
		/// </summary>
		/// <seealso cref="Dsp"/>
		/// <seealso cref="PlayDsp"/>
		public event EventHandler DspPlayed;

		/// <summary>
		/// Occurs when a <see cref="Dsp"/> is registered.
		/// </summary>
		/// <seealso cref="Dsp"/>
		/// <seealso cref="RegisterDsp"/>
		public event EventHandler DspRegistered;

		/// <summary>
		/// Occurs when <see cref="UnlockDsp"/> is invoked.
		/// </summary>
		/// <seealso cref="Dsp"/>
		/// <seealso cref="LockDsp"/>
		/// <seealso cref="UnlockDsp"/>
		public event EventHandler DspUnlocked;

		/// <summary>
		/// Creates a sound group, which can store handles to multiple <see cref="Sound"/> objects.
		/// </summary>
		/// <param name="name">Name of sound group.</param>
		/// <returns>The created <see cref="SoundGroup"/>.</returns>
		/// <remarks>
		/// Once a <see cref="SoundGroup"/> is created, <see cref="Sound.SoundGroup"/> is used to put a sound in a <see cref="SoundGroup"/>.
		/// </remarks>
		/// <seealso cref="Sound"/>
		/// <seealso cref="SoundGroup"/>
		/// <seealso cref="SoundGroupCreated"/>
		public SoundGroup CreateSoundGroup(string name)
		{
			var bytesName = Encoding.UTF8.GetBytes(name + char.MinValue);
			NativeInvoke(FMOD_System_CreateSoundGroup(this, bytesName, out var group));
			SoundGroupCreated?.Invoke(this, EventArgs.Empty);
			return CoreHelper.Create<SoundGroup>(group);
		}

		/// <summary>
		/// Loads a sound into memory, or opens it for streaming.
		/// </summary>
		/// <param name="source">
		/// <para>Name of the file or URL to open.</para>
		/// <para>For CD playback the name should be a drive letter with a colon, example "D:" (windows only).</para>
		/// </param>
		/// <returns>A newly created <see cref="Sound"/> object.</returns>
		/// <remarks>
		/// <alert class="note"><para>
		/// <b>Important!</b> By default (<see cref="Mode.CreateSample"/>) <b>FMOD</b> will try to load and decompress the whole sound into memory! Use <see cref="Mode.CreateStream"/> to open it as a stream and have it play back in realtime! <see cref="Mode.CreateCompressedSample"/> can also be used for certain formats.
		/// </para></alert>
		/// <list type="bullet">
		/// <item><para>
		/// To open a file or URL as a stream, so that it decompresses / reads at runtime, instead of loading / decompressing into memory all at the time of this call, use the <see cref="Mode.CreateStream"/> flag. This is like a "stream" in <b>FMOD 3</b>. 
		/// </para></item>
		/// <item><para>
		/// To open a file or URL as a compressed sound effect that is not streamed and is not decompressed into memory at load time, use <see cref="Mode.CreateCompressedSample"/>. This is supported with MPEG (mp2/mp3), ADPCM/FADPCM, XMA, AT9 and FSB Vorbis files only. This is useful for those who want realtime compressed sound effects, but not the overhead of disk access. 
		/// </para></item>
		/// <item><para>
		/// To open a sound as 2D, so that it is not affected by 3D processing, use the <see cref="Mode.TwoD"/> flag. 3D sound commands will be ignored on these types of sounds.
		/// </para></item>
		/// <item><para>
		/// To open a sound as 3D, so that it is treated as a 3D sound, use the <see cref="Mode.ThreeD"/> flag. Calls to <see cref="Channel.SetPan"/> will be ignored on these types of sounds. 
		/// </para></item>
		/// </list>
		/// <para>Note that <see cref="Mode.OpenRaw"/>, <see cref="Mode.OpenMemory"/>, <see cref="Mode.OpenMemoryPoint"/> and <see cref="Mode.OpenUser"/> will not work here without the <see cref="CreateSoundExInfo"/> structure present, as more information is needed.</para>
		/// <para>Use <see cref="Mode.NonBlocking"/> to have the sound open or load in the background. You can use <see cref="Sound.GetOpenState()"/> to determine if it has finished loading / opening or not. While it is loading (not ready), sound functions are not accessable for that sound.</para>
		/// <para>To account for slow devices or computers that might cause buffer underrun (skipping/stuttering/repeating blocks of audio), use <see cref="SetStreamBufferSize(uint, TimeUnit)"/>.</para>
		/// <para>To play WMA files on Windows, the user must have the latest Windows media player codecs installed (Windows Media Player 9). The user can download this as an installer (wmfdist.exe) from www.fmod.org download page if they desire or you may wish to redistribute it with your application (this is allowed). This installer does NOT install windows media player, just the necessary WMA codecs needed.</para>
		/// <para>
		/// Specifying <see cref="Mode.OpenMemoryPoint"/> will POINT to your memory rather allocating its own sound buffers and duplicating it internally.<lineBreak/>
		/// <b><u>This means you cannot free the memory while FMOD is using it, until after <see cref="Sound.Dispose"/> is called.</u></b><lineBreak/>
		/// With <see cref="Mode.OpenMemoryPoint"/>, for PCM formats, only WAV, FSB and RAW are supported. For compressed formats, only those formats supported by <see cref="Mode.CreateCompressedSample"/> are supported.
		/// </para>
		/// </remarks>
		/// <seealso cref="Mode"/>
		/// <seealso cref="CreateSoundExInfo"/>
		/// <seealso cref="SoundCreated"/>
		/// <seealso cref="Sound.GetOpenState()"/>
		/// <seealso cref="Channel.SetPan"/>
		/// <seealos cref="SetStreamBufferSize(uint, TimeUnit)"/>
		/// <seealso cref="CreateSound(byte[], Mode, CreateSoundExInfo?)"/>
		/// <seealso cref="CreateStream(string, Mode, CreateSoundExInfo?)"/>
		public Sound CreateSound(string source)
		{
			var strBytes = Encoding.UTF8.GetBytes(source + char.MinValue);
			return CreateSound(strBytes, Mode.Default, null);
		}

		/// <summary>
		/// Loads a sound into memory, or opens it for streaming.
		/// </summary>
		/// <param name="source">
		/// <para>Name of the file or URL to open.</para>
		/// <para>For CD playback the name should be a drive letter with a colon, example "D:" (windows only).</para>
		/// </param>
		/// <param name="mode">Behaviour modifier for opening the sound. See <see cref="Mode"/> and remarks section for more. </param>
		/// <returns>A newly created <see cref="Sound"/> object.</returns>
		/// <remarks>
		/// <alert class="note"><para>
		/// <b>Important!</b> By default (<see cref="Mode.CreateSample"/>) <b>FMOD</b> will try to load and decompress the whole sound into memory! Use <see cref="Mode.CreateStream"/> to open it as a stream and have it play back in realtime! <see cref="Mode.CreateCompressedSample"/> can also be used for certain formats.
		/// </para></alert>
		/// <list type="bullet">
		/// <item><para>
		/// To open a file or URL as a stream, so that it decompresses / reads at runtime, instead of loading / decompressing into memory all at the time of this call, use the <see cref="Mode.CreateStream"/> flag. This is like a "stream" in <b>FMOD 3</b>. 
		/// </para></item>
		/// <item><para>
		/// To open a file or URL as a compressed sound effect that is not streamed and is not decompressed into memory at load time, use <see cref="Mode.CreateCompressedSample"/>. This is supported with MPEG (mp2/mp3), ADPCM/FADPCM, XMA, AT9 and FSB Vorbis files only. This is useful for those who want realtime compressed sound effects, but not the overhead of disk access. 
		/// </para></item>
		/// <item><para>
		/// To open a sound as 2D, so that it is not affected by 3D processing, use the <see cref="Mode.TwoD"/> flag. 3D sound commands will be ignored on these types of sounds.
		/// </para></item>
		/// <item><para>
		/// To open a sound as 3D, so that it is treated as a 3D sound, use the <see cref="Mode.ThreeD"/> flag. Calls to <see cref="Channel.SetPan"/> will be ignored on these types of sounds. 
		/// </para></item>
		/// </list>
		/// <para>Note that <see cref="Mode.OpenRaw"/>, <see cref="Mode.OpenMemory"/>, <see cref="Mode.OpenMemoryPoint"/> and <see cref="Mode.OpenUser"/> will not work here without the <see cref="CreateSoundExInfo"/> structure present, as more information is needed.</para>
		/// <para>Use <see cref="Mode.NonBlocking"/> to have the sound open or load in the background. You can use <see cref="Sound.GetOpenState()"/> to determine if it has finished loading / opening or not. While it is loading (not ready), sound functions are not accessable for that sound.</para>
		/// <para>To account for slow devices or computers that might cause buffer underrun (skipping/stuttering/repeating blocks of audio), use <see cref="SetStreamBufferSize(uint, TimeUnit)"/>.</para>
		/// <para>To play WMA files on Windows, the user must have the latest Windows media player codecs installed (Windows Media Player 9). The user can download this as an installer (wmfdist.exe) from www.fmod.org download page if they desire or you may wish to redistribute it with your application (this is allowed). This installer does NOT install windows media player, just the necessary WMA codecs needed.</para>
		/// <para>
		/// Specifying <see cref="Mode.OpenMemoryPoint"/> will POINT to your memory rather allocating its own sound buffers and duplicating it internally.<lineBreak/>
		/// <b><u>This means you cannot free the memory while FMOD is using it, until after <see cref="Sound.Dispose"/> is called.</u></b><lineBreak/>
		/// With <see cref="Mode.OpenMemoryPoint"/>, for PCM formats, only WAV, FSB and RAW are supported. For compressed formats, only those formats supported by <see cref="Mode.CreateCompressedSample"/> are supported.
		/// </para>
		/// </remarks>
		/// <seealso cref="Mode"/>
		/// <seealso cref="CreateSoundExInfo"/>
		/// <seealso cref="SoundCreated"/>
		/// <seealso cref="Sound.GetOpenState()"/>
		/// <seealso cref="Channel.SetPan"/>
		/// <seealos cref="SetStreamBufferSize(uint, TimeUnit)"/>
		/// <seealso cref="CreateSound(byte[], Mode, CreateSoundExInfo?)"/>
		/// <seealso cref="CreateStream(string, Mode, CreateSoundExInfo?)"/>
		public Sound CreateSound(string source, Mode mode)
		{
			var strBytes = Encoding.UTF8.GetBytes(source + char.MinValue);
			return CreateSound(strBytes, mode, null);
		}

		/// <summary>
		/// Loads a sound into memory, or opens it for streaming.
		/// </summary>
		/// <param name="source">
		/// <para>Name of the file or URL to open.</para>
		/// <para>For CD playback the name should be a drive letter with a colon, example "D:" (windows only).</para>
		/// </param>
		/// <param name="mode">Behaviour modifier for opening the sound. See <see cref="Mode"/> and remarks section for more. </param>
		/// <param name="exInfo">
		/// <para>A <see cref="CreateSoundExInfo"/> structure which lets the user provide extended information while playing the sound. </para>
		/// <para>Optional. Specify <c>null</c> to ignore.</para>
		/// </param>
		/// <returns>A newly created <see cref="Sound"/> object.</returns>
		/// <remarks>
		/// <alert class="note"><para>
		/// <b>Important!</b> By default (<see cref="Mode.CreateSample"/>) <b>FMOD</b> will try to load and decompress the whole sound into memory! Use <see cref="Mode.CreateStream"/> to open it as a stream and have it play back in realtime! <see cref="Mode.CreateCompressedSample"/> can also be used for certain formats.
		/// </para></alert>
		/// <list type="bullet">
		/// <item><para>
		/// To open a file or URL as a stream, so that it decompresses / reads at runtime, instead of loading / decompressing into memory all at the time of this call, use the <see cref="Mode.CreateStream"/> flag. This is like a "stream" in <b>FMOD 3</b>. 
		/// </para></item>
		/// <item><para>
		/// To open a file or URL as a compressed sound effect that is not streamed and is not decompressed into memory at load time, use <see cref="Mode.CreateCompressedSample"/>. This is supported with MPEG (mp2/mp3), ADPCM/FADPCM, XMA, AT9 and FSB Vorbis files only. This is useful for those who want realtime compressed sound effects, but not the overhead of disk access. 
		/// </para></item>
		/// <item><para>
		/// To open a sound as 2D, so that it is not affected by 3D processing, use the <see cref="Mode.TwoD"/> flag. 3D sound commands will be ignored on these types of sounds.
		/// </para></item>
		/// <item><para>
		/// To open a sound as 3D, so that it is treated as a 3D sound, use the <see cref="Mode.ThreeD"/> flag. Calls to <see cref="Channel.SetPan"/> will be ignored on these types of sounds. 
		/// </para></item>
		/// </list>
		/// <para>Note that <see cref="Mode.OpenRaw"/>, <see cref="Mode.OpenMemory"/>, <see cref="Mode.OpenMemoryPoint"/> and <see cref="Mode.OpenUser"/> will not work here without the <see cref="CreateSoundExInfo"/> structure present, as more information is needed.</para>
		/// <para>Use <see cref="Mode.NonBlocking"/> to have the sound open or load in the background. You can use <see cref="Sound.GetOpenState()"/> to determine if it has finished loading / opening or not. While it is loading (not ready), sound functions are not accessable for that sound.</para>
		/// <para>To account for slow devices or computers that might cause buffer underrun (skipping/stuttering/repeating blocks of audio), use <see cref="SetStreamBufferSize(uint, TimeUnit)"/>.</para>
		/// <para>To play WMA files on Windows, the user must have the latest Windows media player codecs installed (Windows Media Player 9). The user can download this as an installer (wmfdist.exe) from www.fmod.org download page if they desire or you may wish to redistribute it with your application (this is allowed). This installer does NOT install windows media player, just the necessary WMA codecs needed.</para>
		/// <para>
		/// Specifying <see cref="Mode.OpenMemoryPoint"/> will POINT to your memory rather allocating its own sound buffers and duplicating it internally.<lineBreak/>
		/// <b><u>This means you cannot free the memory while FMOD is using it, until after <see cref="Sound.Dispose"/> is called.</u></b><lineBreak/>
		/// With <see cref="Mode.OpenMemoryPoint"/>, for PCM formats, only WAV, FSB and RAW are supported. For compressed formats, only those formats supported by <see cref="Mode.CreateCompressedSample"/> are supported.
		/// </para>
		/// </remarks>
		/// <seealso cref="Mode"/>
		/// <seealso cref="CreateSoundExInfo"/>
		/// <seealso cref="SoundCreated"/>
		/// <seealso cref="Sound.GetOpenState()"/>
		/// <seealso cref="Channel.SetPan"/>
		/// <seealos cref="SetStreamBufferSize(uint, TimeUnit)"/>
		/// <seealso cref="CreateSound(byte[], Mode, CreateSoundExInfo?)"/>
		/// <seealso cref="CreateStream(string, Mode, CreateSoundExInfo?)"/>
		public Sound CreateSound(string source, Mode mode, CreateSoundExInfo? exInfo)
		{
			var strBytes = Encoding.UTF8.GetBytes(source + char.MinValue);
			return CreateSound(strBytes, Mode.Default, exInfo);
		}

		/// <summary>
		/// Loads a sound into memory, or opens it for streaming.
		/// </summary>
		/// <param name="source">
		/// <para>Name of the file or URL to open encoded in a UTF-8 <see cref="T:byte[]"/>, or a preloaded sound memory block if <see cref="Mode.OpenMemory"/> or <see cref="Mode.OpenMemoryPoint"/> is used. </para>
		/// <para>For CD playback the name should be a drive letter with a colon, example "D:" (windows only).</para>
		/// </param>
		/// <returns>A newly created <see cref="Sound"/> object.</returns>
		/// <remarks>
		/// <alert class="note"><para>
		/// <b>Important!</b> By default (<see cref="Mode.CreateSample"/>) <b>FMOD</b> will try to load and decompress the whole sound into memory! Use <see cref="Mode.CreateStream"/> to open it as a stream and have it play back in realtime! <see cref="Mode.CreateCompressedSample"/> can also be used for certain formats.
		/// </para></alert>
		/// <list type="bullet">
		/// <item><para>
		/// To open a file or URL as a stream, so that it decompresses / reads at runtime, instead of loading / decompressing into memory all at the time of this call, use the <see cref="Mode.CreateStream"/> flag. This is like a "stream" in <b>FMOD 3</b>. 
		/// </para></item>
		/// <item><para>
		/// To open a file or URL as a compressed sound effect that is not streamed and is not decompressed into memory at load time, use <see cref="Mode.CreateCompressedSample"/>. This is supported with MPEG (mp2/mp3), ADPCM/FADPCM, XMA, AT9 and FSB Vorbis files only. This is useful for those who want realtime compressed sound effects, but not the overhead of disk access. 
		/// </para></item>
		/// <item><para>
		/// To open a sound as 2D, so that it is not affected by 3D processing, use the <see cref="Mode.TwoD"/> flag. 3D sound commands will be ignored on these types of sounds.
		/// </para></item>
		/// <item><para>
		/// To open a sound as 3D, so that it is treated as a 3D sound, use the <see cref="Mode.ThreeD"/> flag. Calls to <see cref="Channel.SetPan"/> will be ignored on these types of sounds. 
		/// </para></item>
		/// </list>
		/// <para>Note that <see cref="Mode.OpenRaw"/>, <see cref="Mode.OpenMemory"/>, <see cref="Mode.OpenMemoryPoint"/> and <see cref="Mode.OpenUser"/> will not work here without the <see cref="CreateSoundExInfo"/> structure present, as more information is needed.</para>
		/// <para>Use <see cref="Mode.NonBlocking"/> to have the sound open or load in the background. You can use <see cref="Sound.GetOpenState()"/> to determine if it has finished loading / opening or not. While it is loading (not ready), sound functions are not accessable for that sound.</para>
		/// <para>To account for slow devices or computers that might cause buffer underrun (skipping/stuttering/repeating blocks of audio), use <see cref="SetStreamBufferSize(uint, TimeUnit)"/>.</para>
		/// <para>To play WMA files on Windows, the user must have the latest Windows media player codecs installed (Windows Media Player 9). The user can download this as an installer (wmfdist.exe) from www.fmod.org download page if they desire or you may wish to redistribute it with your application (this is allowed). This installer does NOT install windows media player, just the necessary WMA codecs needed.</para>
		/// <para>
		/// Specifying <see cref="Mode.OpenMemoryPoint"/> will POINT to your memory rather allocating its own sound buffers and duplicating it internally.<lineBreak/>
		/// <b><u>This means you cannot free the memory while FMOD is using it, until after <see cref="Sound.Dispose"/> is called.</u></b><lineBreak/>
		/// With <see cref="Mode.OpenMemoryPoint"/>, for PCM formats, only WAV, FSB and RAW are supported. For compressed formats, only those formats supported by <see cref="Mode.CreateCompressedSample"/> are supported.
		/// </para>
		/// </remarks>
		/// <seealso cref="Mode"/>
		/// <seealso cref="CreateSoundExInfo"/>
		/// <seealso cref="SoundCreated"/>
		/// <seealso cref="Sound.GetOpenState()"/>
		/// <seealso cref="Channel.SetPan"/>
		/// <seealos cref="SetStreamBufferSize(uint, TimeUnit)"/>
		/// <seealso cref="CreateSound(string, Mode, CreateSoundExInfo?)"/>
		/// <seealso cref="CreateStream(byte[], Mode, CreateSoundExInfo?)"/>
		public Sound CreateSound(byte[] source)
		{
			return CreateSound(source, Mode.Default, null);
		}

		/// <summary>
		/// Loads a sound into memory, or opens it for streaming.
		/// </summary>
		/// <param name="source">
		/// <para>Name of the file or URL to open encoded in a UTF-8 <see cref="T:byte[]"/>, or a preloaded sound memory block if <see cref="Mode.OpenMemory"/> or <see cref="Mode.OpenMemoryPoint"/> is used. </para>
		/// <para>For CD playback the name should be a drive letter with a colon, example "D:" (windows only).</para>
		/// </param>
		/// <param name="mode">Behaviour modifier for opening the sound. See <see cref="Mode"/> and remarks section for more. </param>
		/// <returns>A newly created <see cref="Sound"/> object.</returns>
		/// <remarks>
		/// <alert class="note"><para>
		/// <b>Important!</b> By default (<see cref="Mode.CreateSample"/>) <b>FMOD</b> will try to load and decompress the whole sound into memory! Use <see cref="Mode.CreateStream"/> to open it as a stream and have it play back in realtime! <see cref="Mode.CreateCompressedSample"/> can also be used for certain formats.
		/// </para></alert>
		/// <list type="bullet">
		/// <item><para>
		/// To open a file or URL as a stream, so that it decompresses / reads at runtime, instead of loading / decompressing into memory all at the time of this call, use the <see cref="Mode.CreateStream"/> flag. This is like a "stream" in <b>FMOD 3</b>. 
		/// </para></item>
		/// <item><para>
		/// To open a file or URL as a compressed sound effect that is not streamed and is not decompressed into memory at load time, use <see cref="Mode.CreateCompressedSample"/>. This is supported with MPEG (mp2/mp3), ADPCM/FADPCM, XMA, AT9 and FSB Vorbis files only. This is useful for those who want realtime compressed sound effects, but not the overhead of disk access. 
		/// </para></item>
		/// <item><para>
		/// To open a sound as 2D, so that it is not affected by 3D processing, use the <see cref="Mode.TwoD"/> flag. 3D sound commands will be ignored on these types of sounds.
		/// </para></item>
		/// <item><para>
		/// To open a sound as 3D, so that it is treated as a 3D sound, use the <see cref="Mode.ThreeD"/> flag. Calls to <see cref="Channel.SetPan"/> will be ignored on these types of sounds. 
		/// </para></item>
		/// </list>
		/// <para>Note that <see cref="Mode.OpenRaw"/>, <see cref="Mode.OpenMemory"/>, <see cref="Mode.OpenMemoryPoint"/> and <see cref="Mode.OpenUser"/> will not work here without the <see cref="CreateSoundExInfo"/> structure present, as more information is needed.</para>
		/// <para>Use <see cref="Mode.NonBlocking"/> to have the sound open or load in the background. You can use <see cref="Sound.GetOpenState()"/> to determine if it has finished loading / opening or not. While it is loading (not ready), sound functions are not accessable for that sound.</para>
		/// <para>To account for slow devices or computers that might cause buffer underrun (skipping/stuttering/repeating blocks of audio), use <see cref="SetStreamBufferSize(uint, TimeUnit)"/>.</para>
		/// <para>To play WMA files on Windows, the user must have the latest Windows media player codecs installed (Windows Media Player 9). The user can download this as an installer (wmfdist.exe) from www.fmod.org download page if they desire or you may wish to redistribute it with your application (this is allowed). This installer does NOT install windows media player, just the necessary WMA codecs needed.</para>
		/// <para>
		/// Specifying <see cref="Mode.OpenMemoryPoint"/> will POINT to your memory rather allocating its own sound buffers and duplicating it internally.<lineBreak/>
		/// <b><u>This means you cannot free the memory while FMOD is using it, until after <see cref="Sound.Dispose"/> is called.</u></b><lineBreak/>
		/// With <see cref="Mode.OpenMemoryPoint"/>, for PCM formats, only WAV, FSB and RAW are supported. For compressed formats, only those formats supported by <see cref="Mode.CreateCompressedSample"/> are supported.
		/// </para>
		/// </remarks>
		/// <seealso cref="Mode"/>
		/// <seealso cref="CreateSoundExInfo"/>
		/// <seealso cref="SoundCreated"/>
		/// <seealso cref="Sound.GetOpenState()"/>
		/// <seealso cref="Channel.SetPan"/>
		/// <seealos cref="SetStreamBufferSize(uint, TimeUnit)"/>
		/// <seealso cref="CreateSound(string, Mode, CreateSoundExInfo?)"/>
		/// <seealso cref="CreateStream(byte[], Mode, CreateSoundExInfo?)"/>
		public Sound CreateSound(byte[] source, Mode mode)
		{
			return CreateSound(source, mode, null);
		}

		/// <summary>
		/// Loads a sound into memory, or opens it for streaming.
		/// </summary>
		/// <param name="source">
		/// <para>Name of the file or URL to open encoded in a UTF-8 <see cref="T:byte[]"/>, or a preloaded sound memory block if <see cref="Mode.OpenMemory"/> or <see cref="Mode.OpenMemoryPoint"/> is used. </para>
		/// <para>For CD playback the name should be a drive letter with a colon, example "D:" (windows only).</para>
		/// </param>
		/// <param name="mode">Behaviour modifier for opening the sound. See <see cref="Mode"/> and remarks section for more. </param>
		/// <param name="exInfo">
		/// <para>A <see cref="CreateSoundExInfo"/> structure which lets the user provide extended information while playing the sound. </para>
		/// <para>Optional. Specify <c>null</c> to ignore.</para>
		/// </param>
		/// <returns>A newly created <see cref="Sound"/> object.</returns>
		/// <remarks>
		/// <alert class="note"><para>
		/// <b>Important!</b> By default (<see cref="Mode.CreateSample"/>) <b>FMOD</b> will try to load and decompress the whole sound into memory! Use <see cref="Mode.CreateStream"/> to open it as a stream and have it play back in realtime! <see cref="Mode.CreateCompressedSample"/> can also be used for certain formats.
		/// </para></alert>
		/// <list type="bullet">
		/// <item><para>
		/// To open a file or URL as a stream, so that it decompresses / reads at runtime, instead of loading / decompressing into memory all at the time of this call, use the <see cref="Mode.CreateStream"/> flag. This is like a "stream" in <b>FMOD 3</b>. 
		/// </para></item>
		/// <item><para>
		/// To open a file or URL as a compressed sound effect that is not streamed and is not decompressed into memory at load time, use <see cref="Mode.CreateCompressedSample"/>. This is supported with MPEG (mp2/mp3), ADPCM/FADPCM, XMA, AT9 and FSB Vorbis files only. This is useful for those who want realtime compressed sound effects, but not the overhead of disk access. 
		/// </para></item>
		/// <item><para>
		/// To open a sound as 2D, so that it is not affected by 3D processing, use the <see cref="Mode.TwoD"/> flag. 3D sound commands will be ignored on these types of sounds.
		/// </para></item>
		/// <item><para>
		/// To open a sound as 3D, so that it is treated as a 3D sound, use the <see cref="Mode.ThreeD"/> flag. Calls to <see cref="Channel.SetPan"/> will be ignored on these types of sounds. 
		/// </para></item>
		/// </list>
		/// <para>Note that <see cref="Mode.OpenRaw"/>, <see cref="Mode.OpenMemory"/>, <see cref="Mode.OpenMemoryPoint"/> and <see cref="Mode.OpenUser"/> will not work here without the <see cref="CreateSoundExInfo"/> structure present, as more information is needed.</para>
		/// <para>Use <see cref="Mode.NonBlocking"/> to have the sound open or load in the background. You can use <see cref="Sound.GetOpenState()"/> to determine if it has finished loading / opening or not. While it is loading (not ready), sound functions are not accessable for that sound.</para>
		/// <para>To account for slow devices or computers that might cause buffer underrun (skipping/stuttering/repeating blocks of audio), use <see cref="SetStreamBufferSize(uint, TimeUnit)"/>.</para>
		/// <para>To play WMA files on Windows, the user must have the latest Windows media player codecs installed (Windows Media Player 9). The user can download this as an installer (wmfdist.exe) from www.fmod.org download page if they desire or you may wish to redistribute it with your application (this is allowed). This installer does NOT install windows media player, just the necessary WMA codecs needed.</para>
		/// <para>
		/// Specifying <see cref="Mode.OpenMemoryPoint"/> will POINT to your memory rather allocating its own sound buffers and duplicating it internally.<lineBreak/>
		/// <b><u>This means you cannot free the memory while FMOD is using it, until after <see cref="Sound.Dispose"/> is called.</u></b><lineBreak/>
		/// With <see cref="Mode.OpenMemoryPoint"/>, for PCM formats, only WAV, FSB and RAW are supported. For compressed formats, only those formats supported by <see cref="Mode.CreateCompressedSample"/> are supported.
		/// </para>
		/// </remarks>
		/// <seealso cref="Mode"/>
		/// <seealso cref="CreateSoundExInfo"/>
		/// <seealso cref="SoundCreated"/>
		/// <seealso cref="Sound.GetOpenState()"/>
		/// <seealso cref="Channel.SetPan"/>
		/// <seealos cref="SetStreamBufferSize(uint, TimeUnit)"/>
		/// <seealso cref="CreateSound(string, Mode, CreateSoundExInfo?)"/>
		/// <seealso cref="CreateStream(byte[], Mode, CreateSoundExInfo?)"/>
		public Sound CreateSound(byte[] source, Mode mode, CreateSoundExInfo? exInfo)
		{
			IntPtr sound;
			if (exInfo.HasValue)
			{
				var info = exInfo.Value;
				info.CbSize = Marshal.SizeOf(info);
				NativeInvoke(FMOD_System_CreateSound(this, source, mode, ref info, out sound));
			}
			else
			{
				NativeInvoke(FMOD_System_CreateSound(this, source, mode, IntPtr.Zero, out sound));
			}
			SoundCreated?.Invoke(this, EventArgs.Empty);
			return CoreHelper.Create<Sound>(sound);
		}

		/// <summary>
		/// <para>Opens a sound for streaming.</para>
		/// <para>This function is a helper function that is the same as <see cref="CreateSound(string)"/>, but has the <see cref="Mode.CreateStream"/> flag added internally.</para>
		/// </summary>
		/// <param name="source">Name of the file or URL to open.</param>
		/// <returns>A newly created <see cref="Sound"/> object.</returns>
		/// <remarks>
		/// <alert class="note">
		/// <para>A stream only has one decode buffer and file handle, and therefore can only be played once. It cannot play multiple times at once because it cannot share a stream buffer if the stream is playing at different positions.</para>
		/// <para>Open multiple streams to have them play concurrently.</para>
		/// </alert>
		/// <list type="bullet">
		/// <item><para>
		/// To open a file or URL as a stream, so that it decompresses / reads at runtime, instead of loading / decompressing into memory all at the time of this call, use the <see cref="Mode.CreateStream"/> flag. This is like a "stream" in <b>FMOD 3</b>. 
		/// </para></item>
		/// <item><para>
		/// To open a file or URL as a compressed sound effect that is not streamed and is not decompressed into memory at load time, use <see cref="Mode.CreateCompressedSample"/>. This is supported with MPEG (mp2/mp3), ADPCM/FADPCM, XMA, AT9 and FSB Vorbis files only. This is useful for those who want realtime compressed sound effects, but not the overhead of disk access. 
		/// </para></item>
		/// <item><para>
		/// To open a sound as 2D, so that it is not affected by 3D processing, use the <see cref="Mode.TwoD"/> flag. 3D sound commands will be ignored on these types of sounds.
		/// </para></item>
		/// <item><para>
		/// To open a sound as 3D, so that it is treated as a 3D sound, use the <see cref="Mode.ThreeD"/> flag. Calls to <see cref="Channel.SetPan"/> will be ignored on these types of sounds. 
		/// </para></item>
		/// </list>
		/// <para>Note that <see cref="Mode.OpenRaw"/>, <see cref="Mode.OpenMemory"/>, <see cref="Mode.OpenMemoryPoint"/> and <see cref="Mode.OpenUser"/> will not work here without the <see cref="CreateSoundExInfo"/> structure present, as more information is needed.</para>
		/// <para>Use <see cref="Mode.NonBlocking"/> to have the sound open or load in the background. You can use <see cref="Sound.GetOpenState()"/> to determine if it has finished loading / opening or not. While it is loading (not ready), sound functions are not accessable for that sound.</para>
		/// <para>To account for slow devices or computers that might cause buffer underrun (skipping/stuttering/repeating blocks of audio), use <see cref="SetStreamBufferSize(uint, TimeUnit)"/>.</para>
		/// <para>Note that FMOD_CREATESAMPLE will be ignored, overriden by this function because this is simply a wrapper to <see cref="CreateSound(string, Mode, CreateSoundExInfo?)"/> that provides the <see cref="Mode.CreateStream"/> flag. The <see cref="Mode.CreateStream"/> flag overrides <see cref="Mode.CreateSample"/>.</para>
		/// </remarks>
		/// <seealso cref="Sound"/>
		/// <seealso cref="Encoding.UTF8"/>
		/// <seealso cref="SetStreamBufferSize(uint, TimeUnit)"/>
		/// <seealso cref="CreateStream(byte[], Mode, CreateSoundExInfo?)"/>
		/// <seealso cref="Mode"/>
		/// <seealso cref="CreateSoundExInfo"/>
		/// <seealso cref="Channel.SetPan"/>
		/// <seealso cref="Sound.GetOpenState()"/>
		/// <seealso cref="OpenStateInfo"/>
		public Sound CreateStream(string source)
		{
			var strBytes = Encoding.UTF8.GetBytes(source + char.MinValue);
			return CreateStream(strBytes, Mode.Default, null);
		}

		/// <summary>
		/// <para>Opens a sound for streaming.</para>
		/// <para>This function is a helper function that is the same as <see cref="CreateSound(string, Mode)"/>, but has the <see cref="Mode.CreateStream"/> flag added internally.</para>
		/// </summary>
		/// <param name="source">Name of the file or URL to open.</param>
		/// <param name="mode">
		/// <para>Behaviour modifier for opening the sound.</para>
		/// <para>See <see cref="Mode"/> and remarks for more.</para>
		/// </param>
		/// <returns>A newly created <see cref="Sound"/> object.</returns>
		/// <remarks>
		/// <alert class="note">
		/// <para>A stream only has one decode buffer and file handle, and therefore can only be played once. It cannot play multiple times at once because it cannot share a stream buffer if the stream is playing at different positions.</para>
		/// <para>Open multiple streams to have them play concurrently.</para>
		/// </alert>
		/// <list type="bullet">
		/// <item><para>
		/// To open a file or URL as a stream, so that it decompresses / reads at runtime, instead of loading / decompressing into memory all at the time of this call, use the <see cref="Mode.CreateStream"/> flag. This is like a "stream" in <b>FMOD 3</b>. 
		/// </para></item>
		/// <item><para>
		/// To open a file or URL as a compressed sound effect that is not streamed and is not decompressed into memory at load time, use <see cref="Mode.CreateCompressedSample"/>. This is supported with MPEG (mp2/mp3), ADPCM/FADPCM, XMA, AT9 and FSB Vorbis files only. This is useful for those who want realtime compressed sound effects, but not the overhead of disk access. 
		/// </para></item>
		/// <item><para>
		/// To open a sound as 2D, so that it is not affected by 3D processing, use the <see cref="Mode.TwoD"/> flag. 3D sound commands will be ignored on these types of sounds.
		/// </para></item>
		/// <item><para>
		/// To open a sound as 3D, so that it is treated as a 3D sound, use the <see cref="Mode.ThreeD"/> flag. Calls to <see cref="Channel.SetPan"/> will be ignored on these types of sounds. 
		/// </para></item>
		/// </list>
		/// <para>Note that <see cref="Mode.OpenRaw"/>, <see cref="Mode.OpenMemory"/>, <see cref="Mode.OpenMemoryPoint"/> and <see cref="Mode.OpenUser"/> will not work here without the <see cref="CreateSoundExInfo"/> structure present, as more information is needed.</para>
		/// <para>Use <see cref="Mode.NonBlocking"/> to have the sound open or load in the background. You can use <see cref="Sound.GetOpenState()"/> to determine if it has finished loading / opening or not. While it is loading (not ready), sound functions are not accessable for that sound.</para>
		/// <para>To account for slow devices or computers that might cause buffer underrun (skipping/stuttering/repeating blocks of audio), use <see cref="SetStreamBufferSize(uint, TimeUnit)"/>.</para>
		/// <para>Note that FMOD_CREATESAMPLE will be ignored, overriden by this function because this is simply a wrapper to <see cref="CreateSound(string, Mode, CreateSoundExInfo?)"/> that provides the <see cref="Mode.CreateStream"/> flag. The <see cref="Mode.CreateStream"/> flag overrides <see cref="Mode.CreateSample"/>.</para>
		/// </remarks>
		/// <seealso cref="Sound"/>
		/// <seealso cref="Encoding.UTF8"/>
		/// <seealso cref="SetStreamBufferSize(uint, TimeUnit)"/>
		/// <seealso cref="CreateStream(byte[], Mode, CreateSoundExInfo?)"/>
		/// <seealso cref="Mode"/>
		/// <seealso cref="CreateSoundExInfo"/>
		/// <seealso cref="Channel.SetPan"/>
		/// <seealso cref="Sound.GetOpenState()"/>
		/// <seealso cref="OpenStateInfo"/>
		public Sound CreateStream(string source, Mode mode)
		{
			var strBytes = Encoding.UTF8.GetBytes(source + char.MinValue);
			return CreateStream(strBytes, mode, null);
		}

		/// <summary>
		/// <para>Opens a sound for streaming.</para>
		/// <para>This function is a helper function that is the same as <see cref="CreateSound(string, Mode, CreateSoundExInfo?)"/>, but has the <see cref="Mode.CreateStream"/> flag added internally.</para>
		/// </summary>
		/// <param name="source">Name of the file or URL to open.</param>
		/// <param name="mode">
		/// <para>Behaviour modifier for opening the sound.</para>
		/// <para>See <see cref="Mode"/> and remarks for more.</para>
		/// </param>
		/// <param name="exInfo">
		/// <para>A <see cref="CreateSoundExInfo"/> structure which lets the user provide extended information while playing the sound. </para>
		/// <para>Optional. Specify <c>null</c> to ignore.</para>
		/// </param>
		/// <returns>A newly created <see cref="Sound"/> object.</returns>
		/// <remarks>
		/// <alert class="note">
		/// <para>A stream only has one decode buffer and file handle, and therefore can only be played once. It cannot play multiple times at once because it cannot share a stream buffer if the stream is playing at different positions.</para>
		/// <para>Open multiple streams to have them play concurrently.</para>
		/// </alert>
		/// <list type="bullet">
		/// <item><para>
		/// To open a file or URL as a stream, so that it decompresses / reads at runtime, instead of loading / decompressing into memory all at the time of this call, use the <see cref="Mode.CreateStream"/> flag. This is like a "stream" in <b>FMOD 3</b>. 
		/// </para></item>
		/// <item><para>
		/// To open a file or URL as a compressed sound effect that is not streamed and is not decompressed into memory at load time, use <see cref="Mode.CreateCompressedSample"/>. This is supported with MPEG (mp2/mp3), ADPCM/FADPCM, XMA, AT9 and FSB Vorbis files only. This is useful for those who want realtime compressed sound effects, but not the overhead of disk access. 
		/// </para></item>
		/// <item><para>
		/// To open a sound as 2D, so that it is not affected by 3D processing, use the <see cref="Mode.TwoD"/> flag. 3D sound commands will be ignored on these types of sounds.
		/// </para></item>
		/// <item><para>
		/// To open a sound as 3D, so that it is treated as a 3D sound, use the <see cref="Mode.ThreeD"/> flag. Calls to <see cref="Channel.SetPan"/> will be ignored on these types of sounds. 
		/// </para></item>
		/// </list>
		/// <para>Note that <see cref="Mode.OpenRaw"/>, <see cref="Mode.OpenMemory"/>, <see cref="Mode.OpenMemoryPoint"/> and <see cref="Mode.OpenUser"/> will not work here without the <see cref="CreateSoundExInfo"/> structure present, as more information is needed.</para>
		/// <para>Use <see cref="Mode.NonBlocking"/> to have the sound open or load in the background. You can use <see cref="Sound.GetOpenState()"/> to determine if it has finished loading / opening or not. While it is loading (not ready), sound functions are not accessable for that sound.</para>
		/// <para>To account for slow devices or computers that might cause buffer underrun (skipping/stuttering/repeating blocks of audio), use <see cref="SetStreamBufferSize(uint, TimeUnit)"/>.</para>
		/// <para>Note that FMOD_CREATESAMPLE will be ignored, overriden by this function because this is simply a wrapper to <see cref="CreateSound(string, Mode, CreateSoundExInfo?)"/> that provides the <see cref="Mode.CreateStream"/> flag. The <see cref="Mode.CreateStream"/> flag overrides <see cref="Mode.CreateSample"/>.</para>
		/// </remarks>
		/// <seealso cref="Sound"/>
		/// <seealso cref="Encoding.UTF8"/>
		/// <seealso cref="SetStreamBufferSize(uint, TimeUnit)"/>
		/// <seealso cref="CreateStream(byte[], Mode, CreateSoundExInfo?)"/>
		/// <seealso cref="Mode"/>
		/// <seealso cref="CreateSoundExInfo"/>
		/// <seealso cref="Channel.SetPan"/>
		/// <seealso cref="Sound.GetOpenState()"/>
		/// <seealso cref="OpenStateInfo"/>
		public Sound CreateStream(string source, Mode mode, CreateSoundExInfo? exInfo)
		{
			var strBytes = Encoding.UTF8.GetBytes(source + char.MinValue);
			return CreateStream(strBytes, Mode.Default, exInfo);
		}

		/// <summary>
		/// <para>Opens a sound for streaming.</para>
		/// <para>This function is a helper function that is the same as <see cref="CreateSound(byte[])"/>, but has the <see cref="Mode.CreateStream"/> flag added internally.</para>
		/// </summary>
		/// <param name="source">Name of the file or URL to open encoded in UTF-8 as a <see cref="T:byte[]"/>.</param>
		/// <returns>A newly created <see cref="Sound"/> object.</returns>
		/// <remarks>
		/// <alert class="note">
		/// <para>A stream only has one decode buffer and file handle, and therefore can only be played once. It cannot play multiple times at once because it cannot share a stream buffer if the stream is playing at different positions.</para>
		/// <para>Open multiple streams to have them play concurrently.</para>
		/// </alert>
		/// <list type="bullet">
		/// <item><para>
		/// To open a file or URL as a stream, so that it decompresses / reads at runtime, instead of loading / decompressing into memory all at the time of this call, use the <see cref="Mode.CreateStream"/> flag. This is like a "stream" in <b>FMOD 3</b>. 
		/// </para></item>
		/// <item><para>
		/// To open a file or URL as a compressed sound effect that is not streamed and is not decompressed into memory at load time, use <see cref="Mode.CreateCompressedSample"/>. This is supported with MPEG (mp2/mp3), ADPCM/FADPCM, XMA, AT9 and FSB Vorbis files only. This is useful for those who want realtime compressed sound effects, but not the overhead of disk access. 
		/// </para></item>
		/// <item><para>
		/// To open a sound as 2D, so that it is not affected by 3D processing, use the <see cref="Mode.TwoD"/> flag. 3D sound commands will be ignored on these types of sounds.
		/// </para></item>
		/// <item><para>
		/// To open a sound as 3D, so that it is treated as a 3D sound, use the <see cref="Mode.ThreeD"/> flag. Calls to <see cref="Channel.SetPan"/> will be ignored on these types of sounds. 
		/// </para></item>
		/// </list>
		/// <para>Note that <see cref="Mode.OpenRaw"/>, <see cref="Mode.OpenMemory"/>, <see cref="Mode.OpenMemoryPoint"/> and <see cref="Mode.OpenUser"/> will not work here without the <see cref="CreateSoundExInfo"/> structure present, as more information is needed.</para>
		/// <para>Use <see cref="Mode.NonBlocking"/> to have the sound open or load in the background. You can use <see cref="Sound.GetOpenState()"/> to determine if it has finished loading / opening or not. While it is loading (not ready), sound functions are not accessable for that sound.</para>
		/// <para>To account for slow devices or computers that might cause buffer underrun (skipping/stuttering/repeating blocks of audio), use <see cref="SetStreamBufferSize(uint, TimeUnit)"/>.</para>
		/// <para>Note that FMOD_CREATESAMPLE will be ignored, overriden by this function because this is simply a wrapper to <see cref="CreateSound(byte[])"/> that provides the <see cref="Mode.CreateStream"/> flag. The <see cref="Mode.CreateStream"/> flag overrides <see cref="Mode.CreateSample"/>.</para>
		/// </remarks>
		/// <seealso cref="Sound"/>
		/// <seealso cref="Encoding.UTF8"/>
		/// <seealso cref="SetStreamBufferSize(uint, TimeUnit)"/>
		/// <seealso cref="CreateStream(string, Mode, CreateSoundExInfo?)"/>
		/// <seealso cref="Mode"/>
		/// <seealso cref="CreateSoundExInfo"/>
		/// <seealso cref="Channel.SetPan"/>
		/// <seealso cref="Sound.GetOpenState()"/>
		/// <seealso cref="OpenStateInfo"/>
		public Sound CreateStream(byte[] source)
		{
			return CreateStream(source, Mode.Default, null);
		}

		/// <summary>
		/// <para>Opens a sound for streaming.</para>
		/// <para>This function is a helper function that is the same as <see cref="CreateSound(byte[], Mode)"/>, but has the <see cref="Mode.CreateStream"/> flag added internally.</para>
		/// </summary>
		/// <param name="source">Name of the file or URL to open encoded in UTF-8 as a <see cref="T:byte[]"/>.</param>
		/// <param name="mode">
		/// <para>Behaviour modifier for opening the sound.</para>
		/// <para>See <see cref="Mode"/> and remarks for more.</para>
		/// </param>
		/// <returns>A newly created <see cref="Sound"/> object.</returns>
		/// <remarks>
		/// <alert class="note">
		/// <para>A stream only has one decode buffer and file handle, and therefore can only be played once. It cannot play multiple times at once because it cannot share a stream buffer if the stream is playing at different positions.</para>
		/// <para>Open multiple streams to have them play concurrently.</para>
		/// </alert>
		/// <list type="bullet">
		/// <item><para>
		/// To open a file or URL as a stream, so that it decompresses / reads at runtime, instead of loading / decompressing into memory all at the time of this call, use the <see cref="Mode.CreateStream"/> flag. This is like a "stream" in <b>FMOD 3</b>. 
		/// </para></item>
		/// <item><para>
		/// To open a file or URL as a compressed sound effect that is not streamed and is not decompressed into memory at load time, use <see cref="Mode.CreateCompressedSample"/>. This is supported with MPEG (mp2/mp3), ADPCM/FADPCM, XMA, AT9 and FSB Vorbis files only. This is useful for those who want realtime compressed sound effects, but not the overhead of disk access. 
		/// </para></item>
		/// <item><para>
		/// To open a sound as 2D, so that it is not affected by 3D processing, use the <see cref="Mode.TwoD"/> flag. 3D sound commands will be ignored on these types of sounds.
		/// </para></item>
		/// <item><para>
		/// To open a sound as 3D, so that it is treated as a 3D sound, use the <see cref="Mode.ThreeD"/> flag. Calls to <see cref="Channel.SetPan"/> will be ignored on these types of sounds. 
		/// </para></item>
		/// </list>
		/// <para>Note that <see cref="Mode.OpenRaw"/>, <see cref="Mode.OpenMemory"/>, <see cref="Mode.OpenMemoryPoint"/> and <see cref="Mode.OpenUser"/> will not work here without the <see cref="CreateSoundExInfo"/> structure present, as more information is needed.</para>
		/// <para>Use <see cref="Mode.NonBlocking"/> to have the sound open or load in the background. You can use <see cref="Sound.GetOpenState()"/> to determine if it has finished loading / opening or not. While it is loading (not ready), sound functions are not accessable for that sound.</para>
		/// <para>To account for slow devices or computers that might cause buffer underrun (skipping/stuttering/repeating blocks of audio), use <see cref="SetStreamBufferSize(uint, TimeUnit)"/>.</para>
		/// <para>Note that FMOD_CREATESAMPLE will be ignored, overriden by this function because this is simply a wrapper to <see cref="CreateSound(byte[], Mode)"/> that provides the <see cref="Mode.CreateStream"/> flag. The <see cref="Mode.CreateStream"/> flag overrides <see cref="Mode.CreateSample"/>.</para>
		/// </remarks>
		/// <seealso cref="Sound"/>
		/// <seealso cref="Encoding.UTF8"/>
		/// <seealso cref="SetStreamBufferSize(uint, TimeUnit)"/>
		/// <seealso cref="CreateStream(string, Mode, CreateSoundExInfo?)"/>
		/// <seealso cref="Mode"/>
		/// <seealso cref="CreateSoundExInfo"/>
		/// <seealso cref="Channel.SetPan"/>
		/// <seealso cref="Sound.GetOpenState()"/>
		/// <seealso cref="OpenStateInfo"/>
		public Sound CreateStream(byte[] source, Mode mode)
		{
			return CreateStream(source, mode, null);
		}

		/// <summary>
		/// <para>Opens a sound for streaming.</para>
		/// <para>This function is a helper function that is the same as <see cref="CreateSound(byte[], Mode, CreateSoundExInfo?)"/>, but has the <see cref="Mode.CreateStream"/> flag added internally.</para>
		/// </summary>
		/// <param name="source">Name of the file or URL to open encoded in UTF-8 as a <see cref="T:byte[]"/>.</param>
		/// <param name="mode">
		/// <para>Behaviour modifier for opening the sound.</para>
		/// <para>See <see cref="Mode"/> and remarks for more.</para>
		/// </param>
		/// <param name="exInfo">
		/// <para>A <see cref="CreateSoundExInfo"/> structure which lets the user provide extended information while playing the sound. </para>
		/// <para>Optional. Specify <c>null</c> to ignore.</para>
		/// </param>
		/// <returns>A newly created <see cref="Sound"/> object.</returns>
		/// <remarks>
		/// <alert class="note">
		/// <para>A stream only has one decode buffer and file handle, and therefore can only be played once. It cannot play multiple times at once because it cannot share a stream buffer if the stream is playing at different positions.</para>
		/// <para>Open multiple streams to have them play concurrently.</para>
		/// </alert>
		/// <list type="bullet">
		/// <item><para>
		/// To open a file or URL as a stream, so that it decompresses / reads at runtime, instead of loading / decompressing into memory all at the time of this call, use the <see cref="Mode.CreateStream"/> flag. This is like a "stream" in <b>FMOD 3</b>. 
		/// </para></item>
		/// <item><para>
		/// To open a file or URL as a compressed sound effect that is not streamed and is not decompressed into memory at load time, use <see cref="Mode.CreateCompressedSample"/>. This is supported with MPEG (mp2/mp3), ADPCM/FADPCM, XMA, AT9 and FSB Vorbis files only. This is useful for those who want realtime compressed sound effects, but not the overhead of disk access. 
		/// </para></item>
		/// <item><para>
		/// To open a sound as 2D, so that it is not affected by 3D processing, use the <see cref="Mode.TwoD"/> flag. 3D sound commands will be ignored on these types of sounds.
		/// </para></item>
		/// <item><para>
		/// To open a sound as 3D, so that it is treated as a 3D sound, use the <see cref="Mode.ThreeD"/> flag. Calls to <see cref="Channel.SetPan"/> will be ignored on these types of sounds. 
		/// </para></item>
		/// </list>
		/// <para>Note that <see cref="Mode.OpenRaw"/>, <see cref="Mode.OpenMemory"/>, <see cref="Mode.OpenMemoryPoint"/> and <see cref="Mode.OpenUser"/> will not work here without the <see cref="CreateSoundExInfo"/> structure present, as more information is needed.</para>
		/// <para>Use <see cref="Mode.NonBlocking"/> to have the sound open or load in the background. You can use <see cref="Sound.GetOpenState()"/> to determine if it has finished loading / opening or not. While it is loading (not ready), sound functions are not accessable for that sound.</para>
		/// <para>To account for slow devices or computers that might cause buffer underrun (skipping/stuttering/repeating blocks of audio), use <see cref="SetStreamBufferSize(uint, TimeUnit)"/>.</para>
		/// <para>Note that FMOD_CREATESAMPLE will be ignored, overriden by this function because this is simply a wrapper to <see cref="CreateSound(byte[], Mode, CreateSoundExInfo?)"/> that provides the <see cref="Mode.CreateStream"/> flag. The <see cref="Mode.CreateStream"/> flag overrides <see cref="Mode.CreateSample"/>.</para>
		/// </remarks>
		/// <seealso cref="Sound"/>
		/// <seealso cref="Encoding.UTF8"/>
		/// <seealso cref="SetStreamBufferSize(uint, TimeUnit)"/>
		/// <seealso cref="CreateStream(string, Mode, CreateSoundExInfo?)"/>
		/// <seealso cref="Mode"/>
		/// <seealso cref="CreateSoundExInfo"/>
		/// <seealso cref="Channel.SetPan"/>
		/// <seealso cref="Sound.GetOpenState()"/>
		/// <seealso cref="OpenStateInfo"/>
		public Sound CreateStream(byte[] source, Mode mode, CreateSoundExInfo? exInfo)
		{
			IntPtr sound;
			if (exInfo.HasValue)
			{
				var info = exInfo.Value;
				info.CbSize = Marshal.SizeOf(info);
				NativeInvoke(FMOD_System_CreateStream(this, source, mode, ref info, out sound));
			}
			else
			{
				NativeInvoke(FMOD_System_CreateStream(this, source, mode, IntPtr.Zero, out sound));
			}
			SoundCreated?.Invoke(this, EventArgs.Empty);
			return CoreHelper.Create<Sound>(sound);
		}

		/// <summary>
		/// Retrieves identification information about a sound device specified by its index, and specific to the output mode set with the <see cref="Output"/> property.
		/// </summary>
		/// <param name="id">Index of the sound driver device. The total number of devices can be found with <see cref="RecordDriverCount"/>.</param>
		/// <returns>A <see cref="Driver"/> object describing the specified driver.</returns>
		/// <seealso cref="RecordDriverCount"/>
		/// <seealso cref="Output"/>
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

		/// <summary>
		/// Retrieves the current recording position of the record buffer in PCM samples.
		/// </summary>
		/// <param name="driverId">
		/// <para>Enumerated driver ID.</para>
		/// <para>This must be in a valid range delimited by <see cref="RecordDriverCount"/>. </para>
		/// </param>
		/// <returns>The current recording position in PCM samples.</returns>
		/// <remarks>The position will return to <c>0</c> when <see cref="RecordStop"/> is called or when a non-looping recording reaches the end.</remarks>
		/// <seealso cref="RecordStart"/>
		/// <seealso cref="RecordStop"/>
		public uint GetRecordPosition(int driverId)
		{
			NativeInvoke(FMOD_System_GetRecordPosition(this, driverId, out var position));
			return position;
		}

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
		/// <seealso cref="NetworkProxyChanged"/>
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
		/// <seealso cref="NetworkTimeoutChanged"/>
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
		/// <seealso cref="SelectedDriverChanged"/>
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
		/// <seealso cref="OutputChanged"/>
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
		/// <seealso cref="ResumeMixer"/>
		/// <seealso cref="MixerSuspended"/>
		public void SuspendMixer()
		{
			NativeInvoke(FMOD_System_MixerSuspend(this));
			MixerSuspended?.Invoke(this, EventArgs.Empty);
		}

		/// <summary>
		/// <para>Gets or sets the number of 3D "listeners" in the 3D sound scene.</para> 
		/// <para>This property is useful mainly for split-screen game purposes.</para>
		/// <para>Valid values are from <c>1</c> to <see cref="Constants.MAX_LISTENERS"/>.</para>
		/// </summary>
		/// <value>
		/// The listener count.
		/// </value>
		/// <remarks>If the number of listeners is set to more than one, then panning and doppler are turned off. All sound effects will be mono. <b>FMOD</b> uses a "closest sound to the listener" method to determine what should be heard in this case.</remarks>
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


		/// <summary>
		/// Resume mixer thread and reacquire access to audio hardware.
		/// </summary>
		/// <remarks>
		/// <para>Used on mobile platforms when entering the foreground after being suspended.</para>
		/// <para>All internal state will resume, i.e. created sound and channels are still valid and playback will continue.</para>
		/// </remarks>
		/// <seealso cref="SuspendMixer"/>
		/// <seealso cref="MixerResumed"/>
		public void ResumeMixer()
		{
			NativeInvoke(FMOD_System_MixerResume(this));
			MixerResumed?.Invoke(this, EventArgs.Empty);
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
		/// <para>Mutual exclusion function to lock the <b>FMOD</b> DSP engine (which runs asynchronously in another thread), so that it will not execute. If the FMOD DSP engine is already executing, this function will block until it has completed.</para>
		/// <para>The function may be used to synchronize DSP network operations carried out by the user.</para>
		/// <para>An example of using this function may be for when the user wants to construct a DSP sub-network, without the DSP engine executing in the background while the sub-network is still under construction.</para>
		/// </summary>
		/// <remarks>
		/// <para>Once the user no longer needs the DSP engine locked, it must be unlocked with <see cref="UnlockDsp"/>.</para>
		/// <alert class="note">
		/// The DSP engine should not be locked for a significant amount of time, otherwise inconsistency in the audio output may result. (audio skipping/stuttering).
		/// </alert>
		/// </remarks>
		/// <seealso cref="Dsp"/>
		/// <seealso cref="UnlockDsp"/>
		/// <seealso cref="DspLocked"/>
		public void LockDsp()
		{
			NativeInvoke(FMOD_System_LockDSP(this));
			DspLocked?.Invoke(this, EventArgs.Empty);
		}

		/// <summary>
		///     Mutual exclusion function to unlock the FMOD DSP engine (which runs asynchronously in another thread) and let it
		///     continue executing.
		/// </summary>
		/// <remarks>The DSP engine must be locked with <see cref="LockDsp" /> before this function is called.</remarks>
		/// <seealso cref="Dsp"/>
		/// <seealso cref="LockDsp" />
		/// <seealso cref="DspUnlocked"/>
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