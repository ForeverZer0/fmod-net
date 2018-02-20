using System;
using System.Runtime.InteropServices;
using System.Text;
using FMOD.Arguments;
using FMOD.Data;
using FMOD.Enumerations;
using FMOD.Structures;

namespace FMOD.Core
{
	public partial class Sound : HandleBase
	{


		internal Sound(IntPtr handle) : base(handle)
		{
		}

		public FmodSystem ParentSystem
		{
			get
			{
				NativeInvoke(FMOD_Sound_GetSystemObject(this, out var system));
				return Factory.Create<FmodSystem>(system);
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
				OnLoopCountChanged();
			}
		}

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
				OnUserDataChanged();
			}
		}

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
				OnModeChanged();
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
				OnDefaultsChanged();
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
				OnDefaultsChanged();
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


		public int TagCount
		{
			get
			{
				NativeInvoke(FMOD_Sound_GetNumTags(this, out var tagCount, out var dummy));
				return tagCount;
			}
		}

		public SoundGroup SoundGroup
		{
			get
			{
				NativeInvoke(FMOD_Sound_GetSoundGroup(this, out var soundGroup));
				return Factory.Create<SoundGroup>(soundGroup);
			}
			set
			{
				NativeInvoke(FMOD_Sound_SetSoundGroup(this, value));
				OnSoundGroupChanged();
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
				OnMusicSpeedChanged();
			}
		}

		public int MusicChannelCount
		{
			get
			{
				NativeInvoke(FMOD_Sound_GetMusicNumChannels(this, out var count));
				return count;
			}
		}

		public int SubSoundCount
		{
			get
			{
				NativeInvoke(FMOD_Sound_GetNumSubSounds(this, out var count));
				return count;
			}
		}

		public int SyncPointCount
		{
			get
			{
				NativeInvoke(FMOD_Sound_GetNumSyncPoints(this, out var count));
				return count;
			}
		}

		public Sound ParentSound
		{
			get
			{
				NativeInvoke(FMOD_Sound_GetSubSoundParent(this, out var sound));
				return Factory.Create<Sound>(sound);
			}
		}

		public ConeSettings ConeSettings3D
		{
			get
			{
				NativeInvoke(FMOD_Sound_Get3DConeSettings(this, out var insideAngle, out var outsideAngle, out var volume));
				return new ConeSettings
				{
					InsideAngle = insideAngle,
					OutsideAngle = outsideAngle,
					OutsideVolume = volume
				};
			}
			set
			{
				var insideAngle = value.InsideAngle.Clamp(0.0f, value.OutsideAngle);
				var outsideAngle = value.OutsideAngle.Clamp(value.InsideAngle, 360.0f);
				var volume = value.OutsideVolume.Clamp(0.0f, 1.0f);
				NativeInvoke(FMOD_Sound_Set3DConeSettings(this, insideAngle, outsideAngle, volume));
				OnConeSettings3DChanged();
			}
		}

		public Vector[] CustomRolloff3D
		{
			get
			{
				NativeInvoke(FMOD_Sound_Get3DCustomRolloff(this, out var points, out var count));
				var vectors = new Vector[count];
				var size = Marshal.SizeOf(typeof(Vector));
				for (var i = 0; i < count; i++)
				{
					var ptr = points + i * size;
					vectors[i] = (Vector) Marshal.PtrToStructure(ptr, typeof(Vector));
				}
				return vectors;
			}
			set
			{
				NativeInvoke(FMOD_Sound_Set3DCustomRolloff(this, ref value, value.Length));
				OnCustomRolloff3DChanged();
			}
		}

		public float MaxDistance3D
		{
			get
			{
				NativeInvoke(FMOD_Sound_Get3DMinMaxDistance(this, out var dummy, out var max));
				return max;
			}
			set
			{
				NativeInvoke(FMOD_Sound_Get3DMinMaxDistance(this, out var min, out var dummy));
				NativeInvoke(FMOD_Sound_Set3DMinMaxDistance(this, min, value));
				OnDistance3DChanged();
			}
		}

		public float MinDistance3D
		{
			get
			{
				NativeInvoke(FMOD_Sound_Get3DMinMaxDistance(this, out var min, out var dummy));
				return min;
			}
			set
			{
				NativeInvoke(FMOD_Sound_Get3DMinMaxDistance(this, out var dummy, out var max));
				NativeInvoke(FMOD_Sound_Set3DMinMaxDistance(this, value, max));
				OnDistance3DChanged();
			}
		}


		public IntPtr AddSyncPoint(SyncPointInfo info)
		{
			NativeInvoke(FMOD_Sound_AddSyncPoint(this, info.Offset, info.OffsetTimeUnit, info.Name, out var syncPoint));
			OnSyncPointAdded(new SyncPointEventArgs(syncPoint));
			return syncPoint;
		}

		public IntPtr AddSyncPoint(uint offset, TimeUnit timeUnit, string name)
		{
			NativeInvoke(FMOD_Sound_AddSyncPoint(this, offset, timeUnit, name, out var syncPoint));
			return syncPoint;
		}

		public void DeleteSyncPoint(IntPtr syncPoint)
		{
			NativeInvoke(FMOD_Sound_DeleteSyncPoint(this, syncPoint));
			OnSyncPointDeleted(new SyncPointEventArgs(syncPoint));
		}
		// TODO: Implement getting updated tags
		public Tag[] GetTags()
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

		public uint GetLength(TimeUnit timeUnit = TimeUnit.Ms)
		{
			NativeInvoke(FMOD_Sound_GetLength(this, out var length, timeUnit));
			return length;
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

		/// <summary>
		///     Gets the the volume of a MOD/S3M/XM/IT/MIDI music channel volume.
		/// </summary>
		/// <param name="channel">MOD/S3M/XM/IT/MIDI music subchannel to retrieve the volume for.</param>
		/// <returns>The volume of the channel from <c>0.0</c> to <c>1.0</c>. Default = <c>1.0</c></returns>
		/// <seealso cref="SetMusicVolume"></seealso>
		public float GetMusicVolume(int channel)
		{
			NativeInvoke(FMOD_Sound_GetMusicChannelVolume(this, channel, out var volume));
			return volume;
		}


		public OpenStateInfo GetOpenState()
		{
			NativeInvoke(FMOD_Sound_GetOpenState(this, out var state, out var buffered, out var starving, out var busy));
			return new OpenStateInfo
			{
				State = state,
				PercentBuffered = buffered,
				Starving = starving,
				DiskBusy = busy
			};
		}

		public void GetOpenState(out OpenState state, out uint buffered, out bool starving, out bool busy)
		{
			NativeInvoke(FMOD_Sound_GetOpenState(this, out state, out buffered, out starving, out busy));
		}

		public Sound GetSubSound(int index)
		{
			NativeInvoke(FMOD_Sound_GetSubSound(this, index, out var sound));
			return Factory.Create<Sound>(sound);
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

		public Tag GetTag(int index)
		{
			NativeInvoke(FMOD_Sound_GetTag(this, null, index.Clamp(0, TagCount), out var tag));
			return tag;
		}

		public void Lock(uint offset, uint length, out IntPtr ptr1, out IntPtr ptr2,
			out uint len1, out uint len2)
		{
			NativeInvoke(FMOD_Sound_Lock(this, offset, length, out ptr1, out ptr2, out len1, out len2));
			OnLocked();
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
			if (readBytes >= length || readBytes == 0)
				return buffer;
			var reSized = new byte[readBytes];
			Buffer.BlockCopy(buffer, 0, reSized, 0, (int) readBytes);
			return reSized;
		}

		public void SeekData(uint pcm)
		{
			NativeInvoke(FMOD_Sound_SeekData(this, pcm));
		}

		public void SetConeSettings(float insideAngle, float outsideAngle, float outsideVolume)
		{
			NativeInvoke(FMOD_Sound_Set3DConeSettings(this, insideAngle, outsideAngle, outsideVolume));
			OnConeSettings3DChanged();
		}

		public void SetDefaults(float frequency = 44100.0f, int priority = 128)
		{
			NativeInvoke(FMOD_Sound_SetDefaults(this, frequency, priority.Clamp(0, 256)));
			OnDefaultsChanged();
		}

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
			OnLoopPointAdded();
		}

		public void SetMinMaxDistance(float min, float max)
		{
			NativeInvoke(FMOD_Sound_Set3DMinMaxDistance(this, min, max));
			OnDistance3DChanged();
		}

		/// <summary>
		///     Sets the the volume of a MOD/S3M/XM/IT/MIDI music channel volume.
		/// </summary>
		/// <param name="channel">MOD/S3M/XM/IT/MIDI music subchannel to set a linear volume for.</param>
		/// <param name="volume">Volume of the channel from <c>0.0</c> to <c>1.0</c>. Default = <c>1.0</c>.</param>
		/// <seealso cref="GetMusicVolume"></seealso>
		/// <seealso cref="MusicVolumeChanged"></seealso>
		public void SetMusicVolume(int channel, float volume)
		{
			var clamped = volume.Clamp(0.0f, 1.0f);
			NativeInvoke(FMOD_Sound_SetMusicChannelVolume(this, channel, clamped));
			OnMusicVolumeChanged(new SoundMusicVolumeChangedEventArgs(channel, clamped));
		}

		public void Unlock(IntPtr ptr1, IntPtr ptr2, uint len1, uint len2)
		{
			NativeInvoke(FMOD_Sound_Unlock(this, ptr1, ptr2, len1, len2));
			OnUnlocked();
		}
	}
}