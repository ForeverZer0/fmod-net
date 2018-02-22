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

		/// <summary>
		/// Occurs when <see cref="WorldSize"/> property is changed.
		/// </summary>
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
		
		/// <summary>
		///     Raises the <see cref="AdvancedSettingsChanged" /> event.
		/// </summary>
		/// <seealso cref="EventArgs" />
		protected virtual void OnAdvancedSettingsChanged()
		{
			AdvancedSettingsChanged?.Invoke(this, EventArgs.Empty);
		}

		/// <summary>
		///     Raises the <see cref="BufferSizeChanged" /> event.
		/// </summary>
		/// <seealso cref="EventArgs" />
		protected virtual void OnBufferSizeChanged()
		{
			BufferSizeChanged?.Invoke(this, EventArgs.Empty);
		}
		
		/// <summary>
		///     Raises the <see cref="DspUnlocked" /> event.
		/// </summary>
		/// <seealso cref="EventArgs" />
		protected virtual void OnDspUnlocked()
		{
			DspUnlocked?.Invoke(this, EventArgs.Empty);
		}
		
		/// <summary>
		///     Raises the <see cref="DspRegistered" /> event.
		/// </summary>
		/// <seealso cref="EventArgs" />
		protected virtual void OnDspRegistered()
		{
			DspRegistered?.Invoke(this, EventArgs.Empty);
		}
		
		/// <summary>
		///     Raises the <see cref="DspPlayed" /> event.
		/// </summary>
		/// <seealso cref="EventArgs" />
		protected virtual void OnDspPlayed()
		{
			DspPlayed?.Invoke(this, EventArgs.Empty);
		}
		
		/// <summary>
		///     Raises the <see cref="DspLocked" /> event.
		/// </summary>
		/// <seealso cref="EventArgs" />
		protected virtual void OnDspLocked()
		{
			DspLocked?.Invoke(this, EventArgs.Empty);
		}
		
		/// <summary>
		///     Raises the <see cref="DspCreated" /> event.
		/// </summary>
		/// <seealso cref="EventArgs" />
		protected virtual void OnDspCreated()
		{
			DspCreated?.Invoke(this, EventArgs.Empty);
		}

		/// <summary>
		///     Raises the <see cref="WorldSizeChanged" /> event.
		/// </summary>
		/// <seealso cref="EventArgs" />
		protected virtual void OnWorldSizeChanged()
		{
			WorldSizeChanged?.Invoke(this, EventArgs.Empty);
		}
		
		/// <summary>
		///     Raises the <see cref="SpeakerPositionChanged" /> event.
		/// </summary>
		/// <seealso cref="EventArgs" />
		protected virtual void OnSpeakerPositionChanged()
		{
			SpeakerPositionChanged?.Invoke(this, EventArgs.Empty);
		}
		
		/// <summary>
		///     Raises the <see cref="SoundPlayed" /> event.
		/// </summary>
		/// <seealso cref="EventArgs" />
		protected virtual void OnSoundPlayed()
		{
			SoundPlayed?.Invoke(this, EventArgs.Empty);
		}
		
		/// <summary>
		///     Raises the <see cref="SoundGroupCreated" /> event.
		/// </summary>
		/// <seealso cref="EventArgs" />
		protected virtual void OnSoundGroupCreated()
		{
			SoundGroupCreated?.Invoke(this, EventArgs.Empty);
		}
		
		/// <summary>
		///     Raises the <see cref="SoundCreated" /> event.
		/// </summary>
		/// <seealso cref="EventArgs" />
		protected virtual void OnSoundCreated()
		{
			SoundCreated?.Invoke(this, EventArgs.Empty);
		}
		
		/// <summary>
		///     Raises the <see cref="SoftwareFormatChanged" /> event.
		/// </summary>
		/// <seealso cref="EventArgs" />
		protected virtual void OnSoftwareFormatChanged()
		{
			SoftwareFormatChanged?.Invoke(this, EventArgs.Empty);
		}
		
		/// <summary>
		///     Raises the <see cref="SoftwareChannelsChanged" /> event.
		/// </summary>
		/// <seealso cref="EventArgs" />
		protected virtual void OnSoftwareChannelsChanged()
		{
			SoftwareChannelsChanged?.Invoke(this, EventArgs.Empty);
		}
		
		/// <summary>
		///     Raises the <see cref="Settings3DChanged" /> event.
		/// </summary>
		/// <seealso cref="EventArgs" />
		protected virtual void OnSettings3DChanged()
		{
			Settings3DChanged?.Invoke(this, EventArgs.Empty);
		}
		
		/// <summary>
		///     Raises the <see cref="SelectedDriverChanged" /> event.
		/// </summary>
		/// <seealso cref="EventArgs" />
		protected virtual void OnSelectedDriverChanged()
		{
			SelectedDriverChanged?.Invoke(this, EventArgs.Empty);
		}
		
		/// <summary>
		///     Raises the <see cref="ChannelGroupAttached" /> event.
		/// </summary>
		/// <seealso cref="EventArgs" />
		protected virtual void OnChannelGroupAttached()
		{
			ChannelGroupAttached?.Invoke(this, EventArgs.Empty);
		}
		
		/// <summary>
		///     Raises the <see cref="ListenerCountChanged" /> event.
		/// </summary>
		/// <seealso cref="EventArgs" />
		protected virtual void OnListenerCountChanged()
		{
			ListenerCountChanged?.Invoke(this, EventArgs.Empty);
		}
		
		/// <summary>
		///     Raises the <see cref="RecordingStarted" /> event.
		/// </summary>
		/// <seealso cref="EventArgs" />
		protected virtual void OnRecordingStarted()
		{
			RecordingStarted?.Invoke(this, EventArgs.Empty);
		}
		
		/// <summary>
		///     Raises the <see cref="ReverbPropertiesChanged" /> event.
		/// </summary>
		/// <seealso cref="EventArgs" />
		protected virtual void OnReverbPropertiesChanged()
		{
			ReverbPropertiesChanged?.Invoke(this, EventArgs.Empty);
		}
		
		/// <summary>
		///     Raises the <see cref="GeometryCreated" /> event.
		/// </summary>
		/// <seealso cref="EventArgs" />
		protected virtual void OnGeometryCreated()
		{
			GeometryCreated?.Invoke(this, EventArgs.Empty);
		}
		
		/// <summary>
		///     Raises the <see cref="MixerResumed" /> event.
		/// </summary>
		/// <seealso cref="EventArgs" />
		protected virtual void OnMixerResumed()
		{
			MixerResumed?.Invoke(this, EventArgs.Empty);
		}
		
		/// <summary>
		///     Raises the <see cref="NetworkProxyChanged" /> event.
		/// </summary>
		/// <seealso cref="EventArgs" />
		protected virtual void OnNetworkProxyChanged()
		{
			NetworkProxyChanged?.Invoke(this, EventArgs.Empty);
		}
		
		/// <summary>
		///     Raises the <see cref="ReverbCreated" /> event.
		/// </summary>
		/// <seealso cref="EventArgs" />
		protected virtual void OnReverbCreated()
		{
			ReverbCreated?.Invoke(this, EventArgs.Empty);
		}
		
		/// <summary>
		///     Raises the <see cref="PluginLoaded" /> event.
		/// </summary>
		/// <seealso cref="EventArgs" />
		protected virtual void OnPluginLoaded()
		{
			PluginLoaded?.Invoke(this, EventArgs.Empty);
		}
		
		/// <summary>
		///     Raises the <see cref="ListenerAttributesChanged" /> event.
		/// </summary>
		/// <seealso cref="EventArgs" />
		protected virtual void OnListenerAttributesChanged()
		{
			ListenerAttributesChanged?.Invoke(this, EventArgs.Empty);
		}
		
		/// <summary>
		///     Raises the <see cref="ChannelGroupCreated" /> event.
		/// </summary>
		/// <seealso cref="EventArgs" />
		protected virtual void OnChannelGroupCreated()
		{
			ChannelGroupCreated?.Invoke(this, EventArgs.Empty);
		}
		
		/// <summary>
		///     Raises the <see cref="MixerSuspended" /> event.
		/// </summary>
		/// <seealso cref="EventArgs" />
		protected virtual void OnMixerSuspended()
		{
			MixerSuspended?.Invoke(this, EventArgs.Empty);
		}
		
		/// <summary>
		///     Raises the <see cref="NetworkTimeoutChanged" /> event.
		/// </summary>
		/// <seealso cref="EventArgs" />
		protected virtual void OnNetworkTimeoutChanged()
		{
			NetworkTimeoutChanged?.Invoke(this, EventArgs.Empty);
		}
		
		/// <summary>
		///     Raises the <see cref="DspBufferChanged" /> event.
		/// </summary>
		/// <seealso cref="EventArgs" />
		protected virtual void OnDspBufferChanged()
		{
			DspBufferChanged?.Invoke(this, EventArgs.Empty);
		}
		
		/// <summary>
		///     Raises the <see cref="Closed" /> event.
		/// </summary>
		/// <seealso cref="EventArgs" />
		protected virtual void OnClosed()
		{
			Closed?.Invoke(this, EventArgs.Empty);
		}
		
		/// <summary>
		///     Raises the <see cref="PluginUnloaded" /> event.
		/// </summary>
		/// <seealso cref="EventArgs" />
		protected virtual void OnPluginUnloaded()
		{
			PluginUnloaded?.Invoke(this, EventArgs.Empty);
		}
		
		/// <summary>
		///     Raises the <see cref="CodecRegistered" /> event.
		/// </summary>
		/// <seealso cref="EventArgs" />
		protected virtual void OnCodecRegistered()
		{
			CodecRegistered?.Invoke(this, EventArgs.Empty);
		}
		
		/// <summary>
		///     Raises the <see cref="RecordingStopped" /> event.
		/// </summary>
		/// <seealso cref="EventArgs" />
		protected virtual void OnRecordingStopped()
		{
			RecordingStopped?.Invoke(this, EventArgs.Empty);
		}
		
		/// <summary>
		///     Raises the <see cref="ChannelGroupDetached" /> event.
		/// </summary>
		/// <seealso cref="EventArgs" />
		protected virtual void OnChannelGroupDetached()
		{
			ChannelGroupDetached?.Invoke(this, EventArgs.Empty);
		}
		
		/// <summary>
		///     Raises the <see cref="OutputChanged" /> event.
		/// </summary>
		/// <seealso cref="EventArgs" />
		protected virtual void OnOutputChanged()
		{
			OutputChanged?.Invoke(this, EventArgs.Empty);
		}
		
		/// <summary>
		///     Raises the <see cref="OutputRegistered" /> event.
		/// </summary>
		/// <seealso cref="EventArgs" />
		protected virtual void OnOutputRegistered()
		{
			OutputRegistered?.Invoke(this, EventArgs.Empty);
		}

		/// <summary>
		///     Raises the <see cref="PluginPathChanged" /> event.
		/// </summary>
		/// <seealso cref="EventArgs" />
		protected virtual void OnPluginPathChanged()
		{
			PluginPathChanged?.Invoke(this, EventArgs.Empty);
		}
	}
}
