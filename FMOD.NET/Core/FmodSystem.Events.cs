using System;

namespace FMOD.NET.Core
{
	public partial class FmodSystem
	{
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
	}
}
