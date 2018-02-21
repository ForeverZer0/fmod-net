using System;
using System.Text;
using FMOD.NET.Data;
using FMOD.NET.Enumerations;
using FMOD.NET.Structures;

namespace FMOD.NET.Core
{
	public partial class FmodSystem
	{
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
				return Factory.Create<ChannelGroup>(channelGroup);
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
				return Factory.Create<SoundGroup>(soundGroup);
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
		/// Gets or sets the sets advanced features like configuring memory and cpu usage for <see cref="Mode.CreateCompressedSample"/> usage.
		/// </summary>
		/// <value>
		/// The advanced settings structure.
		/// </value>
		/// <seealso cref="Structures.AdvancedSettings"/>
		/// <seealso cref="Mode"/>
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


	}
}
