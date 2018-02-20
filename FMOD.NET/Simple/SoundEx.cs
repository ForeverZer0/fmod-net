using System;
using System.Collections.Generic;
using FMOD.Core;
using FMOD.Enumerations;

namespace FMOD.Simple
{
	public class SoundEx : IDisposable
	{
		private static FmodSystem _system;


		public event EventHandler EffectAdded;
		public event EventHandler Disposed;

		private readonly Sound _sound;
		private readonly bool _autoDispose;
		private Channel _channel;
		private readonly List<Dsp> _effects;

		static SoundEx()
		{
			_system = Factory.CreateSystem();
			_system.Initialize();
		}

		public SoundEx(string filename, bool autoDispose = true, int loopCount = 0, bool streaming = true)
		{
			_sound = _system.CreateSound(filename, Mode.LoopNormal | (streaming ? Mode.CreateStream : Mode.Default));
			_sound.LoopCount = loopCount;
			_autoDispose = autoDispose;
			_effects = new List<Dsp>();
		}

		public void Play()
		{
			_channel = _system.PlaySound(_sound, true);
			if (_autoDispose)
				_channel.SoundEnded += (s, e) => Dispose();
			for (var i = 0; i < _effects.Count; i++)
				_channel.AddDsp(_effects[i], i);
			_channel.Resume();
		}

		public void AddEffect(DspType effect)
		{
			var dsp = _system.CreateDspByType(effect);
			_effects.Add(dsp);
			EffectAdded?.Invoke(this, EventArgs.Empty);
		}



		public void Dispose()
		{
			foreach (var dsp in _effects)
			{
				if (!_channel.IsInvalid)
					_channel.RemoveDsp(dsp);
				dsp.Dispose();
			}
			_sound.Dispose();
			Disposed?.Invoke(this, EventArgs.Empty);
		}
	}
}
