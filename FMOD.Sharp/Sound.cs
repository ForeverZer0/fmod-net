﻿using System;
using System.Runtime.InteropServices;
using System.Text;
using FMOD.Sharp.Data;
using FMOD.Sharp.Enums;
using FMOD.Sharp.Structs;

namespace FMOD.Sharp
{
	public partial class Sound : Handle
	{
		internal Sound(IntPtr handle) : base(handle)
		{
		}

		public FmodSystem ParentSystem
		{
			get
			{
				NativeInvoke(FMOD_Sound_GetSystemObject(this, out var system));
				return Core.Create<FmodSystem>(system);
			}
		}

		public string Name
		{
			get
			{
				using (var buffer = new MemoryBuffer(512))
				{
					NativeInvoke(FMOD_Sound_GetName(this, buffer.Pointer, 512));
					return buffer.ToString(Encoding.UTF8);
				}
			}
		}

		public int LoopCount
		{
			get
			{
				NativeInvoke(FMOD_Sound_GetLoopCount(this, out var count));
				return count;
			}
			set
			{
				NativeInvoke(FMOD_Sound_SetLoopCount(this, Math.Max(-1, value)));
				LoopCountChanged?.Invoke(this, EventArgs.Empty);
			}
		}

		public event EventHandler LoopCountChanged;
		public event EventHandler UserDataChanged;

		public IntPtr UserData
		{
			get
			{
				NativeInvoke(FMOD_Sound_GetUserData(this, out var data));
				return data;
			}
			set
			{
				NativeInvoke(FMOD_Sound_SetUserData(this, value));
				UserDataChanged?.Invoke(this, EventArgs.Empty);
			}
		}

		public override void Dispose()
		{
			NativeInvoke(FMOD_Sound_Release(this));
			base.Dispose();
		}

		public uint GetLength(TimeUnit timeUnit = TimeUnit.Ms)
		{
			NativeInvoke(FMOD_Sound_GetLength(this, out var length, timeUnit));
			return length;
		}

		public event EventHandler ModeChanged;

		public Mode Mode
		{
			get
			{
				NativeInvoke(FMOD_Sound_GetMode(this, out var mode));
				return mode;
			}
			set
			{
				NativeInvoke(FMOD_Sound_SetMode(this, value));
				ModeChanged?.Invoke(this, EventArgs.Empty);
			}
		}

		public float DefaultFrequency
		{
			get
			{
				NativeInvoke(FMOD_Sound_GetDefaults(this, out var frequency, out var dummy));
				return frequency;
			}
			set
			{
				NativeInvoke(FMOD_Sound_SetDefaults(this, value, DefaultPriority)); 
				DefaultsChanged?.Invoke(this, EventArgs.Empty);
			}
		}

		public int DefaultPriority
		{
			get
			{
				NativeInvoke(FMOD_Sound_GetDefaults(this, out var dummy, out var priority));
				return priority;
			}
			set
			{
				NativeInvoke(FMOD_Sound_SetDefaults(this, DefaultFrequency, value.Clamp(0, 256))); 
				DefaultsChanged?.Invoke(this, EventArgs.Empty);
			}
		}

		public SoundInfo Info
		{
			get
			{
				NativeInvoke(FMOD_Sound_GetFormat(this, out var type, out var format,
					out var chanCount, out var bits));
				return new SoundInfo
				{
					Type = type,
					Format = format,
					ChannelCount = chanCount,
					BitsPerSample = bits
				};
			}
		}

		public SoundType Type
		{
			get
			{
				NativeInvoke(FMOD_Sound_GetFormat(this, out var type, out var dummy1, 
					out var dummy2, out var dummy3));
				return type;
			}
		}

		public SoundFormat Format
		{
			get
			{
				NativeInvoke(FMOD_Sound_GetFormat(this, out var dummy1, out var format, 
					out var dummy2, out var dummy3));
				return format;
			}
		}

		public int ChannelCount
		{
			get
			{
				NativeInvoke(FMOD_Sound_GetFormat(this, out var dummy1, out var dummy2, 
					out var count, out var dummy3));
				return count;
			}
		}

		public int BitsPerSample
		{
			get
			{
				NativeInvoke(FMOD_Sound_GetFormat(this, out var dummy1, out var dummy2, 
					out var dummy3, out var bits));
				return bits;
			}
		}

		public event EventHandler DefaultsChanged;

		public void SetDefaults(float frequency = 44100.0f, int priority = 128)
		{
			NativeInvoke(FMOD_Sound_SetDefaults(this, frequency, priority.Clamp(0, 256)));
			DefaultsChanged?.Invoke(this, EventArgs.Empty);
		}

		public LoopPoints GetLoopPoints(TimeUnit timeUnit = TimeUnit.Ms)
		{
			return GetLoopPoints(timeUnit, timeUnit);
		}

		public LoopPoints GetLoopPoints(TimeUnit startUnit, TimeUnit endUnit)
		{
			NativeInvoke(FMOD_Sound_GetLoopPoints(this, out var start, startUnit,
				out var end, endUnit));
			return new LoopPoints
			{
				LoopStart = start,
				StartTimeUnit = startUnit,
				LoopEnd = end,
				EndTimeUnit = startUnit
			};
		}

		public event EventHandler LoopPointAdded;

		public void SetLoopPoints(LoopPoints points)
		{
			SetLoopPoints(points.LoopStart, points.LoopEnd, points.StartTimeUnit, points.EndTimeUnit);
		}

		public void SetLoopPoints(uint loopStart, uint loopEnd, TimeUnit timeUnit = TimeUnit.Ms)
		{
			SetLoopPoints(loopStart, loopEnd, timeUnit, timeUnit);
		}

		public void SetLoopPoints(uint loopStart, uint loopEnd, TimeUnit startUnit, TimeUnit endUnit)
		{
			NativeInvoke(FMOD_Sound_SetLoopPoints(this, loopStart, startUnit, loopEnd, endUnit));
			LoopPointAdded?.Invoke(this, EventArgs.Empty);
		}


















		public int TagCount
		{
			get
			{
				NativeInvoke(FMOD_Sound_GetNumTags(this, out var tagCount, out var dummy));
				return tagCount;
			}
		}

		public Tag GetTag(int index)
		{
			NativeInvoke(FMOD_Sound_GetTag(this, null, index.Clamp(0, TagCount), out var tag));
			return tag;
		}

		public Tag[] GetAllTags()
		{
			var tagCount = TagCount;
			var tags = new Tag[tagCount];
			for (var i = 0; i < tagCount; i++)
			{
				NativeInvoke(FMOD_Sound_GetTag(this, null, i, out var tag));
				tags[i] = tag;
			}
			return tags;
		}

		public event EventHandler MusicSpeedChanged;
		public event EventHandler SoundGroupChanged;
		public event EventHandler Locked;
		public event EventHandler Unlocked;

		public SoundGroup SoundGroup
		{
			get
			{
				NativeInvoke(FMOD_Sound_GetSoundGroup(this, out var soundGroup));
				return Core.Create<SoundGroup>(soundGroup);
			}
			set
			{
				NativeInvoke(FMOD_Sound_SetSoundGroup(this, value));
				SoundGroupChanged?.Invoke(this, EventArgs.Empty);
			}
		}

		public float MusicSpeed
		{
			get
			{
				NativeInvoke(FMOD_Sound_GetMusicSpeed(this, out var speed));
				return speed;
			}
			set
			{
				NativeInvoke(FMOD_Sound_SetMusicSpeed(this, value.Clamp(0.01f, 100.0f)));
				MusicSpeedChanged?.Invoke(this, EventArgs.Empty);
			}
		}

		public void Lock(uint offset, uint length, out IntPtr ptr1, out IntPtr ptr2,
			out uint len1, out uint len2)
		{
			NativeInvoke(FMOD_Sound_Lock(this, offset, length, out ptr1, out ptr2, out len1, out len2));
			Locked?.Invoke(this, EventArgs.Empty);
		}

		public void Unlock(IntPtr ptr1, IntPtr ptr2, uint len1, uint len2)
		{
			NativeInvoke(FMOD_Sound_Unlock(this, ptr1, ptr2, len1, len2));
			Unlocked?.Invoke(this, EventArgs.Empty);
		}


		public uint ReadData(IntPtr buffer, uint length)
		{
			NativeInvoke(FMOD_Sound_ReadData(this, buffer, length, out var readBytes));
			return readBytes;
		}

		public byte[] ReadData(uint length)
		{
			var buffer = new byte[length];
			var gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
			NativeInvoke(FMOD_Sound_ReadData(this, gcHandle.AddrOfPinnedObject(), length, out var readBytes));
			gcHandle.Free();
			if (readBytes >= length) 
				return buffer;
			var reSized = new byte[readBytes];
			Buffer.BlockCopy(buffer, 0, reSized, 0, (int)readBytes);
			return reSized;
		}

		public void SeekData(uint pcm)
		{
			NativeInvoke(FMOD_Sound_SeekData(this, pcm));
		}

		public IntPtr GetSyncPoint(int index)
		{
			NativeInvoke(FMOD_Sound_GetSyncPoint(this, index, out var point));
			return point;
		}

		public SyncPointInfo GetSyncpointInfo(IntPtr syncPoint, TimeUnit offsetType = TimeUnit.Ms)
		{
			using (var buffer = new MemoryBuffer(512))
			{
				NativeInvoke(FMOD_Sound_GetSyncPointInfo(this, syncPoint, buffer.Pointer, 512, out var offset, offsetType));
				return new SyncPointInfo
				{
					Name = buffer.ToString(Encoding.UTF8),
					Offset = offset,
					OffsetTimeUnit = offsetType
				};
			}
			
		}

	













		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_Sound_ReadData(IntPtr sound, IntPtr buffer, uint length, out uint read);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_Sound_SeekData(IntPtr sound, uint pcm);




		#region Native Methods

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_Sound_Lock(IntPtr sound, uint offset, uint length, out IntPtr ptr1, out IntPtr ptr2,
			out uint len1, out uint len2);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_Sound_Unlock(IntPtr sound, IntPtr ptr1, IntPtr ptr2, uint len1, uint len2);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_Sound_GetNumTags(IntPtr sound, out int numtags, out int numtagsupdated);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_Sound_GetTag(IntPtr sound, string name, int index, out Tag tag);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_Sound_SetDefaults(IntPtr sound, float frequency, int priority);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_Sound_GetDefaults(IntPtr sound, out float frequency, out int priority);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_Sound_Set3DMinMaxDistance(IntPtr sound, float min, float max);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_Sound_Get3DMinMaxDistance(IntPtr sound, out float min, out float max);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_Sound_Set3DConeSettings(IntPtr sound, float insideconeangle, float outsideconeangle,
			float outsidevolume);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_Sound_Get3DConeSettings(IntPtr sound, out float insideconeangle,
			out float outsideconeangle, out float outsidevolume);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_Sound_Set3DCustomRolloff(IntPtr sound, ref Vector points, int numpoints);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_Sound_Get3DCustomRolloff(IntPtr sound, out IntPtr points, out int numpoints);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_Sound_GetSubSound(IntPtr sound, int index, out IntPtr subsound);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_Sound_GetSubSoundParent(IntPtr sound, out IntPtr parentsound);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_Sound_GetName(IntPtr sound, IntPtr name, int namelen);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_Sound_GetLength(IntPtr sound, out uint length, TimeUnit lengthtype);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_Sound_GetFormat(IntPtr sound, out SoundType type, out SoundFormat format,
			out int channels, out int bits);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_Sound_GetNumSubSounds(IntPtr sound, out int numsubsounds);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_Sound_GetOpenState(IntPtr sound, out OpenState openstate, out uint percentbuffered,
			out bool starving, out bool diskbusy);



		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_Sound_SetSoundGroup(IntPtr sound, IntPtr soundgroup);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_Sound_GetSoundGroup(IntPtr sound, out IntPtr soundgroup);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_Sound_GetNumSyncPoints(IntPtr sound, out int numsyncpoints);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_Sound_GetSyncPoint(IntPtr sound, int index, out IntPtr point);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_Sound_GetSyncPointInfo(IntPtr sound, IntPtr point, IntPtr name, int namelen,
			out uint offset, TimeUnit offsettype);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_Sound_AddSyncPoint(IntPtr sound, uint offset, TimeUnit offsettype, string name,
			out IntPtr point);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_Sound_DeleteSyncPoint(IntPtr sound, IntPtr point);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_Sound_GetMusicNumChannels(IntPtr sound, out int numchannels);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_Sound_SetMusicChannelVolume(IntPtr sound, int channel, float volume);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_Sound_GetMusicChannelVolume(IntPtr sound, int channel, out float volume);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_Sound_SetMusicSpeed(IntPtr sound, float speed);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_Sound_GetMusicSpeed(IntPtr sound, out float speed);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_Sound_SetUserData(IntPtr sound, IntPtr userdata);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_Sound_GetUserData(IntPtr sound, out IntPtr userdata);

		#endregion
	}
}