using System;
using System.Runtime.InteropServices;
using System.Text;
using FMOD.Sharp.Enums;

namespace FMOD.Sharp
{
	public partial class SoundGroup : Handle
	{
		#region Delegates & Events

		public event EventHandler VolumeChanged;

		public event EventHandler MaxAudibleChanged;

		public event EventHandler MaxAudibleBehaviorChanged;

		public event EventHandler UserDataChanged;

		public event EventHandler MuteFadeSpeedChanged;

		public event EventHandler Stopped;

		#endregion

		#region Constructors & Destructor

		public SoundGroup(IntPtr handle) : base(handle)
		{
		}

		#endregion

		#region Properties & Indexers

		public int MaxAudible
		{
			get
			{
				NativeInvoke(FMOD_SoundGroup_GetMaxAudible(this, out var maxAudible));
				return maxAudible;
			}
			set
			{
				NativeInvoke(FMOD_SoundGroup_SetMaxAudible(this, Math.Max(-1, value)));
				MaxAudibleChanged?.Invoke(this, EventArgs.Empty);
			}
		}

		public SoundGroupBehavior MaxAudibleBehavior
		{
			get
			{
				NativeInvoke(FMOD_SoundGroup_GetMaxAudibleBehavior(this, out var behavior));
				return behavior;
			}
			set
			{
				NativeInvoke(FMOD_SoundGroup_SetMaxAudibleBehavior(this, value));
				MaxAudibleBehaviorChanged?.Invoke(this, EventArgs.Empty);
			}
		}

		public float MuteFadeSpeed
		{
			get
			{
				NativeInvoke(FMOD_SoundGroup_GetMuteFadeSpeed(this, out var speed));
				return speed;
			}
			set
			{
				NativeInvoke(FMOD_SoundGroup_SetMuteFadeSpeed(this, Math.Max(value, 0.0f)));
				MuteFadeSpeedChanged?.Invoke(this, EventArgs.Empty);
			}
		}

		public string Name
		{
			get
			{
				var ptr = Marshal.AllocHGlobal(512);
				NativeInvoke(FMOD_SoundGroup_GetName(this, ptr, 512));
				var bytes = new byte[512];
				Marshal.Copy(ptr, bytes, 0, 512);
				Marshal.FreeHGlobal(ptr);
				return Encoding.UTF8.GetString(bytes).Trim('\0');
			}
		}


		public int NumberPlaying
		{
			get
			{
				NativeInvoke(FMOD_SoundGroup_GetNumPlaying(this, out var numPlaying));
				return numPlaying;
			}
		}

		public FmodSystem ParentSystem
		{
			get
			{
				NativeInvoke(FMOD_SoundGroup_GetSystemObject(this, out var system));
				return Core.Create<FmodSystem>(system);
			}
		}

		public int SoundCount
		{
			get
			{
				NativeInvoke(FMOD_SoundGroup_GetNumSounds(this, out var count));
				return count;
			}
		}

		public IntPtr UserData
		{
			get
			{
				NativeInvoke(FMOD_SoundGroup_GetUserData(this, out var data));
				return data;
			}
			set
			{
				NativeInvoke(FMOD_SoundGroup_SetUserData(this, value));
				UserDataChanged?.Invoke(this, EventArgs.Empty);
			}
		}

		public float Volume
		{
			get
			{
				NativeInvoke(FMOD_SoundGroup_GetVolume(this, out var volume));
				return volume;
			}
			set
			{
				NativeInvoke(FMOD_SoundGroup_SetVolume(this, value));
				VolumeChanged?.Invoke(this, EventArgs.Empty);
			}
		}

		#endregion

		#region Methods

		public override void Dispose()
		{
			NativeInvoke(FMOD_SoundGroup_Release(this));
			base.Dispose();
		}

		public Sound GetSound(int index)
		{
			NativeInvoke(FMOD_SoundGroup_GetSound(this, index, out var sound));
			return sound == IntPtr.Zero ? null : Core.Create<Sound>(sound);
		}

		public void Stop()
		{
			NativeInvoke(FMOD_SoundGroup_Stop(this));
			Stopped?.Invoke(this, EventArgs.Empty);
		}

		#endregion
	}
}