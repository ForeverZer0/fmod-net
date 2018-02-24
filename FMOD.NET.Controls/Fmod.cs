using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using FMOD.NET.Core;
using FMOD.NET.Enumerations;
using Utils;

namespace FMOD.NET.Controls
{
	public partial class Fmod : Component
	{
		[Description("")]
		[Category("FMOD")][Editor(typeof(Utils.FlagEnumUIEditor), typeof(System.Drawing.Design.UITypeEditor))]
		public InitFlags InitFlags { get; set; } = InitFlags.Normal;

		[Description("")]
		[Category("FMOD")][TypeConverter(typeof(ArrayConverter))]
		public AudioChannel[] Channels { get; set; } = new AudioChannel[0];

		private FmodSystem _system;

		public Fmod()
		{
			InitializeComponent();
			if (LicenseManager.UsageMode == LicenseUsageMode.Runtime)
				InitializeFmod();
		}

		public Fmod(IContainer container)
		{
			container.Add(this);
			InitializeComponent();
			if (LicenseManager.UsageMode == LicenseUsageMode.Runtime)
				InitializeFmod();
		}

		
		private void InitializeFmod()
		{
			_system = Factory.CreateSystem();
			_system.Initialize(InitFlags);
			foreach (var channel in Channels)
				channel.SetSystem(ref _system);
		}
	}



	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class AudioChannel
	{
		[Browsable(true)]
		[Category("FMOD")]
		[DefaultValue("")]
		[Description("")]
		[TypeConverter(typeof(StringConverter))]
		public string Name { get; set; } = "";

		[Browsable(true)]
		[Category("FMOD")]
		[DefaultValue(typeof(Mode), "Default")]
		[Description("")]
		[TypeConverter(typeof(EnumFlagsConverter))]
		public Mode Mode { get; set; } = Mode.Default;



		private FmodSystem _system;
		private Channel _channel;
		private List<Dsp> _effects;



		public AudioChannel()
		{
			_effects = new List<Dsp>();
		}


		public void SetSystem(ref FmodSystem system)
		{
			_system = system;
		}

		public void Play(string filename,  params DspType[] effects)
		{
			var sound = _system.CreateSound(filename, Mode);
			Play(sound, effects);
		}

		public void Play(Sound sound, params DspType[] effects)
		{
			Reset();
			_channel = _system.PlaySound(sound, true);
			_channel.SoundEnded += (s, e) => Reset();
			foreach (var dspType in effects)
			{
				var dsp = _system.CreateDspByType(dspType);
				_effects.Add(dsp);
				_channel.AddDsp(dsp, DspIndex.Tail);
			}
		}

		private void Reset()
		{
			if (_channel == null || _channel.IsInvalid)
				return;
			_channel.Stop();
			_channel.CurrentSound.Dispose();
			foreach (var dsp in _channel)
				dsp.Dispose();
			_effects.Clear();
			_channel.SetHandleAsInvalid();
			_channel = null;
		}

		public bool IsPaused
		{
			get
			{
				if (_channel == null || _channel.IsInvalid)
					return false;
				return _channel.Paused;
			}
		}

		public bool IsPlaying
		{
			get
			{
				if (_channel == null || _channel.IsInvalid)
					return false;
				return _channel.IsPlaying;
			}
		}

		public void Pause()
		{
			if (_channel == null || _channel.IsInvalid)
				return;
			 _channel.Pause();
		}

		public void Resume()
		{
			if (_channel == null || _channel.IsInvalid)
				return;
			_channel.Resume();
		}

		public void Seek(int milliseconds)
		{
			if (_channel == null || _channel.IsInvalid)
				return;
			var ms = Convert.ToUInt32(milliseconds).Clamp(0u, _channel.CurrentSound.GetLength() - 1);
			_channel.SetPosition(ms);
		}
	}
}
