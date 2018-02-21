using System;
using System.Runtime.InteropServices;
using System.Text;
using FMOD.NET.Arguments;
using FMOD.NET.Data;
using FMOD.NET.Enumerations;
using FMOD.NET.Structures;

namespace FMOD.NET.Core
{
	public partial class Sound : HandleBase
	{

		/// <summary>
		/// Initializes a new instance of the <see cref="Sound"/> class.
		/// </summary>
		/// <param name="handle">The handle.</param>
		protected Sound(IntPtr handle) : base(handle)
		{
		}

		/// <summary>
		/// Gets the parent <see cref="FmodSystem"/> object that was used to create this object.
		/// </summary>
		/// <value>
		/// The parent system.
		/// </value>
		/// <seealso cref="FmodSystem"/>
		/// <seealso cref="O:FMOD.Core.FmodSystem.CreateSound"/>
		/// <seealso cref="O:FMOD.Core.FmodSystem.CreateStream"/>
		public FmodSystem ParentSystem
		{
			get
			{
				NativeInvoke(FMOD_Sound_GetSystemObject(this, out var system));
				return Factory.Create<FmodSystem>(system);
			}
		}

		/// <summary>
		/// Gets the name of a sound.
		/// </summary>
		/// <value>
		/// The name.
		/// </value>
		/// <seealso cref="O:FMOD.Core.FmodSystem.CreateSound"/>
		/// <seealso cref="O:FMOD.Core.FmodSystem.CreateStream"/>
		/// <seealso cref="FMOD.Enumerations.Mode"/>
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

		/// <summary>
		/// <para>Gets or sets a sound, by default, to loop a specified number of times before stopping if its mode is set to <see cref="Enumerations.Mode.LoopNormal"/> or <see cref="Enumerations.Mode.LoopBidi"/>.</para>
		/// <para><c>0</c> = oneshot. <c>1</c> = loop once then stop. <c>-1</c> = loop forever.</para>
		/// <para>Default = <c>-1</c> </para>
		/// </summary>
		/// <value>
		/// The loop count.
		/// </value>
		/// <remarks>
		/// <para><b>Issues with streamed audio. (Sounds created with with <see cref="O:FMOD.Core.FmodSystem.CreateStream"/> or <see cref="FMOD.Enumerations.Mode.CreateStream"/>).</b> </para>
		/// <para>When changing the loop count, sounds created with <see cref="O:FMOD.Core.FmodSystem.CreateStream"/> or <see cref="FMOD.Enumerations.Mode.CreateStream"/> may already have been pre-buffered and executed their loop logic ahead of time, before this call was even made.<lineBreak/> This is dependant on the size of the sound versus the size of the stream decode buffer. See <see cref="CreateSoundExInfo"/>.</para>
		/// <para>If this happens, you may need to reflush the stream buffer. To do this, you can call <see cref="Channel.SetPosition"/> which forces a reflush of the stream buffer.</para>
		/// <para>Note this will usually only happen if you have sounds or loop points that are smaller than the stream decode buffer size. Otherwise you will not normally encounter any problems.</para>
		/// </remarks>
		/// <seealso cref="LoopCountChanged"/>
		/// <seealso cref="Enumerations.Mode"/>
		/// <seealso cref="O:FMOD.Core.FmodSystem.CreateStream"/>
		/// <seealso cref="Channel.SetPosition"/>
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

		/// <summary>
		/// Gets or sets the mode of a sound to alter its characteristics and/or behavior.
		/// </summary>
		/// <value>
		/// The mode.
		/// </value>
		/// <alert class="note"><para>
		/// When changing the mode, note that it will only take effect when the sound is played again with <see cref="FmodSystem.PlaySound"/>. 
		/// </para></alert>
		/// <remarks>
		/// Consider this mode the "default mode" for when the sound plays, not a mode that will suddenly change all currently playing instances of this sound.
		/// <para>Flags supported:</para>
		/// <list type="bullet">
		/// <item><para><see cref="Enumerations.Mode.LoopOff"/></para></item>
		/// <item><para><see cref="Enumerations.Mode.LoopNormal"/></para></item>
		/// <item><para><see cref="Enumerations.Mode.LoopBidi"/></para></item>
		/// <item><para><see cref="Enumerations.Mode.HeadRelative3D"/></para></item>
		/// <item><para><see cref="Enumerations.Mode.WorldRelative3D"/></para></item>
		/// <item><para><see cref="Enumerations.Mode.TwoD"/></para></item>
		/// <item><para><see cref="Enumerations.Mode.ThreeD"/></para></item>
		/// <item><para><see cref="Enumerations.Mode.InverseRolloff3D"/></para></item>
		/// <item><para><see cref="Enumerations.Mode.LinearRolloff3D"/></para></item>
		/// <item><para><see cref="Enumerations.Mode.LinearSquareRolloff3D"/></para></item>
		/// <item><para><see cref="Enumerations.Mode.CustomRolloff3D"/></para></item>
		/// <item><para><see cref="Enumerations.Mode.IgnoreGeometry3D"/></para></item>
		/// </list>
		/// <para><b>Issues with streamed audio. (Sounds created with with <see cref="O:FMOD.Core.FmodSystem.CreateStream"/> or <see cref="FMOD.Enumerations.Mode.CreateStream"/>).</b> </para>
		/// <para>When changing the loop count, sounds created with <see cref="O:FMOD.Core.FmodSystem.CreateStream"/> or <see cref="FMOD.Enumerations.Mode.CreateStream"/> may already have been pre-buffered and executed their loop logic ahead of time, before this call was even made.<lineBreak/> This is dependant on the size of the sound versus the size of the stream decode buffer. See <see cref="CreateSoundExInfo"/>.</para>
		/// <para>If this happens, you may need to reflush the stream buffer. To do this, you can call <see cref="Channel.SetPosition"/> which forces a reflush of the stream buffer.</para>
		/// <para>Note this will usually only happen if you have sounds or loop points that are smaller than the stream decode buffer size. Otherwise you will not normally encounter any problems.</para>
		/// </remarks>
		/// <seealso cref="ModeChanged"/>
		/// <seealso cref="FMOD.Enumerations.Mode"/>
		/// <seealso cref="O:FMOD.Core.FmodSystem.CreateSound"/>
		/// <seealso cref="O:FMOD.Core.FmodSystem.CreateStream"/>
		/// <seealso cref="FmodSystem.PlaySound"/>
		/// <seealso cref="CreateSoundExInfo"/>
		/// <seealso cref="Channel.SetPosition"/>
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

		/// <summary>
		/// <para>Gets or sets the default frequency, so when it is played it uses this value without having to specify it later for each channel each time the sound is played.</para>
		/// <para>Default playback frequency for the sound, in Hz. (ie 44100hz). </para>
		/// </summary>
		/// <value>
		/// The default frequency.
		/// </value>
		/// <seealso cref="DefaultsChanged"/>
		/// <seealso cref="DefaultPriority"/>
		/// <seealso cref="SetDefaults"/>
		public float DefaultFrequency
		{
			get
			{
				NativeInvoke(FMOD_Sound_GetDefaults(this, out var frequency, out var dummy));
				return frequency;
			}
			set
			{
				NativeInvoke(FMOD_Sound_GetDefaults(this, out var dummy, out var priority));
				NativeInvoke(FMOD_Sound_SetDefaults(this, value, priority));
				OnDefaultsChanged();
			}
		}

		/// <summary>
		/// Gets or sets the default priority, so when it is played it uses this value without having to specify it later for each channel each time the sound is played.
		/// <para>Default priority for the sound when played on a channel. <c>0</c> to <c>256</c>. <c>0</c> = most important, <c>256</c> = least important. </para>
		/// <para>Default = <c>128</c>.</para>
		/// </summary>
		/// <value>
		/// The default priority.
		/// </value>
		/// <seealso cref="DefaultsChanged"/>
		/// <seealso cref="DefaultFrequency"/>
		/// <seealso cref="SetDefaults"/>
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

		/// <summary>
		/// Sets a sounds's default attributes, so when it is played it uses these values without having to specify them later for each channel each time the sound is played.
		/// </summary>
		/// <param name="frequency">Default playback frequency for the sound, in Hz. (ie 44100hz).</param>
		/// <param name="priority">Default priority for the sound when played on a channel. <c>0</c> to <c>256</c>. <c>0</c> = most important, <c>256</c> = least important. Default = <c>128</c>. </param>
		/// <seealso cref="DefaultsChanged"/>
		/// <seealso cref="DefaultFrequency"/>
		/// <seealso cref="DefaultPriority"/>
		public void SetDefaults(float frequency = 44100.0f, int priority = 128)
		{
			NativeInvoke(FMOD_Sound_SetDefaults(this, frequency, priority.Clamp(0, 256)));
			OnDefaultsChanged();
		}

		/// <summary>
		/// Read-only container for information describing a sound's format.
		/// </summary>
		public class SoundInfo
		{
			/// <summary>
			/// Gets the type of sound.
			/// </summary>
			/// <value>
			/// The type.
			/// </value>
			public SoundType Type { get; internal set; }

			/// <summary>
			/// Gets the format of the sound.
			/// </summary>
			/// <value>
			/// The format.
			/// </value>
			public SoundFormat Format { get; internal set;  }

			/// <summary>
			/// Gets the number of channels for the sound. 
			/// </summary>
			/// <value>
			/// The channel count.
			/// </value>
			public int ChannelCount { get; internal set; }

			/// <summary>
			/// Gets the number of bits per sample for the sound. This corresponds to <see cref="SoundFormat"/> but is provided as an integer format for convenience.
			/// </summary>
			/// <value>
			/// The bits per sample.
			/// </value>
			public int BitsPerSample  { get; internal set; }
		}

		/// <summary>
		/// Gets information about the sound such as bits per sample, format, etc.
		/// </summary>
		/// <value>
		/// The information.
		/// </value>
		/// <seealso cref="SoundInfo"/>
		/// <seealso cref="SoundType"/>
		/// <seealso cref="SoundFormat"/>
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

		/// <summary>
		/// Gets the number of tags belonging to a sound.
		/// </summary>
		/// <param name="updated">A variable that receives the number of tags updated since this function was last called.</param>
		/// <returns>The number of tags in the sound.</returns>
		/// <seealso cref="GetTag"/>
		/// <seealso cref="GetTags"/>
		public int GetTagCount(out int updated)
		{
			NativeInvoke(FMOD_Sound_GetNumTags(this, out var tagCount, out updated));
			return tagCount;
		}

		/// <summary>
		/// Gets or sets the <see cref="SoundGroup"/> this sound belongs to.
		/// </summary>
		/// <value>
		/// The sound group.
		/// </value>
		/// <remarks>
		/// <para>By default a sound is located in the "master sound group". This can be retrieved with <see cref="FmodSystem.MasterSoundGroup"/>.</para>
		/// <para>Putting a sound in a sound group (or just using the master sound group) allows for functionality like limiting a group of sounds to a certain number of playbacks (see <see cref="Core.SoundGroup.MaxAudible"/>).</para>
		/// </remarks>
		/// <seealso cref="Core.SoundGroup"/>
		/// <seealso cref="Core.SoundGroup.MaxAudible"/>
		/// <seealso cref="FmodSystem.MasterSoundGroup"/>
		/// <seealso cref="FmodSystem.CreateSoundGroup"/>
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

		/// <summary>
		/// Retrieves a descriptive tag stored by the sound, to describe things like the song name, author etc.
		/// </summary>
		/// <param name="index">
		/// <para>Index into the tag list. If the <paramref name="name"/> parameter is <c>null</c>, then the index is the index into all tags present, from <c>0</c> up to but not including the value returned by <see cref="GetTagCount"/>.</para>
		/// <para>If name is not <c>null</c>, then <paramref name="index"/> is the index from <c>0</c> up to the number of tags with the same name. For example if there were two tags with the name "TITLE" then you could use <c>0</c> and <c>1</c> to reference them.</para>
		/// <para>Specifying an <paramref name="index"/> of <c>-1</c> returns new or updated tags. This can be used to pull tags out as they are added or updated. </para>
		/// </param>
		/// <param name="name">Optional. Name of a tag to retrieve. Used to specify a particular tag if the user requires it. To get all types of tags leave this parameter as <c>null</c>. </param>
		/// <returns>The specified tag.</returns>
		/// <remarks>
		/// <para>The number of tags available can be found with <see cref="GetTagCount"/>. The way to display or retrieve tags can be done in three different ways:</para>
		/// <para>All tags can be continuously retrieved by looping throught the available tags. Updated tags will refresh automatically, and the "updated" member of the <see cref="Tag"/> structure will be set to true if a tag has been updated, due to something like a netstream changing the song name for example.</para>
		/// <para>Tags could also be retrieved by specifying <c>-1</c> as the <paramref name="index"/> and only updating tags that are returned. If all tags are retrieved and this function is called the function will return an error of <see cref="Result.TagNotFound"/>.</para>
		/// <para>Specific tags can be retrieved by specifying a <paramref name="name"/> parameter. The <paramref name="index"/> can be <c>0</c> based or <c>-1</c> in the same fashion as described previously.</para>
		/// </remarks>
		/// <seealso cref="GetTagCount"/>
		/// <seealso cref="GetTags"/>
		/// <seealso cref="Tag"/>
		public Tag GetTag(int index, string name = null)
		{
			NativeInvoke(FMOD_Sound_GetTag(this, name, index, out var tag));
			return tag;
		}

		/// <summary>
		/// Helper method to automatically enumerate and return all existing tags within the sound.
		/// </summary>
		/// <returns>An array containing all the <see cref="Tag"/> objects found within the sound.</returns>
		/// <seealso cref="GetTagCount"/>
		/// <seealso cref="GetTag"/>
		public Tag[] GetTags()
		{
			var tagCount = GetTagCount(out var dummy);
			var tags = new Tag[tagCount];
			for (var i = 0; i < tagCount; i++)
			{
				NativeInvoke(FMOD_Sound_GetTag(this, null, i, out var tag));
				tags[i] = tag;
			}
			return tags;
		}

		/// <summary>
		/// Gets or sets the relative speed of MOD/S3M/XM/IT/MIDI music.
		/// <para>From <c>0.01</c> to <c>100.0</c>.</para>
		/// <para><c>0.5</c> = half speed, <c>2.0 </c>= double speed.</para>
		/// <para>Default = <c>1.0</c>.</para>
		/// </summary>
		/// <value>
		/// The music speed.
		/// </value>
		/// <seealso cref="MusicVolumeChanged"/>
		/// <seealso cref="GetMusicVolume"/>
		/// <seealso cref="SetMusicVolume"/>
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

		/// <summary>
		///     Gets the the volume of a MOD/S3M/XM/IT/MIDI music channel volume.
		/// </summary>
		/// <param name="channel">MOD/S3M/XM/IT/MIDI music subchannel to retrieve the volume for.</param>
		/// <returns>The volume of the channel from <c>0.0</c> to <c>1.0</c>. Default = <c>1.0</c></returns>
		/// <seealso cref="MusicSpeed"/>
		/// <seealso cref="SetMusicVolume"></seealso>
		public float GetMusicVolume(int channel)
		{
			NativeInvoke(FMOD_Sound_GetMusicChannelVolume(this, channel, out var volume));
			return volume;
		}

		/// <summary>
		///     Sets the the volume of a MOD/S3M/XM/IT/MIDI music channel volume.
		/// <para>From <c>0.0</c> to <c>1.0</c>.</para>
		/// <para>Default = <c>1.0</c>.</para>
		/// </summary>
		/// <param name="channel">MOD/S3M/XM/IT/MIDI music subchannel to set a linear volume for.</param>
		/// <param name="volume">Volume of the channel from <c>0.0</c> to <c>1.0</c>. </param>
		/// <seealso cref="GetMusicVolume"></seealso>
		/// <seealso cref="MusicVolumeChanged"></seealso>
		/// <seealso cref="MusicSpeed"/>
		public void SetMusicVolume(int channel, float volume)
		{
			var clamped = volume.Clamp(0.0f, 1.0f);
			NativeInvoke(FMOD_Sound_SetMusicChannelVolume(this, channel, clamped));
			OnMusicVolumeChanged(new SoundMusicVolumeChangedEventArgs(channel, clamped));
		}

		/// <summary>
		/// Gets the number of music channels inside a MOD/S3M/XM/IT/MIDI file.
		/// </summary>
		/// <value>
		/// The music channel count.
		/// </value>
		/// <seealso cref="GetMusicVolume"/>
		/// <seealso cref="SetMusicVolume"/>
		public int MusicChannelCount
		{
			get
			{
				NativeInvoke(FMOD_Sound_GetMusicNumChannels(this, out var count));
				return count;
			}
		}

		/// <summary>
		/// Returns a pointer to the beginning of the sample data for a sound.
		/// </summary>
		/// <param name="offset">Offset in <see cref="byte">bytes</see> to the position you want to lock in the sample buffer.</param>
		/// <param name="length">Number of <see cref="byte">bytes</see> you want to lock in the sample buffer. </param>
		/// <param name="ptr1">A pointer to the first part of the locked data.</param>
		/// <param name="ptr2">A pointer to the second part of the locked data. This will be <see cref="IntPtr.Zero"/> if the data locked hasn't wrapped at the end of the buffer. </param>
		/// <param name="len1">Length of data in <see cref="byte">bytes</see> that was locked for <paramref name="ptr1"/>.</param>
		/// <param name="len2">Length of data in <see cref="byte">bytes</see> that was locked for <paramref name="ptr2"/>. This will be <c>0</c> if the data locked hasn't wrapped at the end of the buffer. </param>
		/// <remarks>
		/// <para>You must always unlock the data again after you have finished with it, using <see cref="Unlock"/>.</para>
		/// <para>With this function you get access to the RAW audio data, for example 8, 16, 24 or 32bit PCM data, mono or stereo data. You must take this into consideration when processing the data within the pointer.</para>
		/// <para>The <see cref="BufferReader"/> class is available and specifically designed for fast and easy reading of data no matter the bit depth.</para>
		/// </remarks>
		/// <seealso cref="BufferReader"/>
		/// <seealso cref="Locked"/>
		/// <seealso cref="Unlock"/>
		public void Lock(uint offset, uint length, out IntPtr ptr1, out IntPtr ptr2,
			out uint len1, out uint len2)
		{
			NativeInvoke(FMOD_Sound_Lock(this, offset, length, out ptr1, out ptr2, out len1, out len2));
			OnLocked();
		}

		/// <summary>
		/// Releases previous sample data lock from <see cref="Lock"/>.
		/// </summary>
		/// <param name="ptr1">Pointer to the first locked portion of sample data, from <see cref="Lock"/>. </param>
		/// <param name="ptr2">Pointer to the second locked portion of sample data, from <see cref="Lock"/>.</param>
		/// <param name="len1">Length of data in <see cref="byte">bytes</see> that was locked for <paramref name="ptr1"/>. .</param>
		/// <param name="len2">Length of data in <see cref="byte">bytes</see> that was locked for <paramref name="ptr2"/>. This will be <c>0</c> if the data locked hasn't wrapped at the end of the buffer. </param>
		/// <seealso cref="Lock"/>
		/// <seealso cref="BufferReader"/>
		public void Unlock(IntPtr ptr1, IntPtr ptr2, uint len1, uint len2)
		{
			NativeInvoke(FMOD_Sound_Unlock(this, ptr1, ptr2, len1, len2));
			OnUnlocked();
		}

		/// <summary>
		/// <para>Reads data from an opened sound to a specified pointer, using the <b>FMOD</b> codec created internally.</para>
		/// <para>This can be used for decoding data offline in small pieces (or big pieces), rather than playing and capturing it, or loading the whole file at once and having to <see cref="Lock"/> / <see cref="Unlock"/> the data.</para>
		/// </summary>
		/// <param name="buffer">Pointer to a buffer that receives the decoded data from the sound.</param>
		/// <param name="length">Number of bytes to read into the buffer.</param>
		/// <returns>Number of bytes actually read.</returns>
		/// <remarks>
		/// <para>
		/// If too much data is read, it is possible <see cref="Result.FileEof"/> will be returned, meaning it is out of data. The "read" parameter will reflect this by returning a smaller number of bytes read than was requested.<lineBreak/>
		/// As a sound already reads the whole file then closes it upon calling <see cref="O:FMOD.Core.FmodSystem.CreateSound"/> (unless <see cref="O:FMOD.Core.FmodSystem.CreateStream"/> or <see cref="Enumerations.Mode.CreateStream"/> is used), this function will not work because the file is no longer open.
		/// </para>
		/// <para>
		/// Note that opening a stream makes it read a chunk of data and this will advance the read cursor. You need to either use <see cref="Enumerations.Mode.OpenOnly"/> to stop the stream pre-buffering or call <see cref="SeekData"/> to reset the read cursor.<lineBreak/>
		/// If <see cref="Enumerations.Mode.OpenOnly"/> flag is used when opening a sound, it will leave the file handle open, and <b>FMOD</b> will not read any data internally, so the read cursor will be at position <c>0</c>. This will allow the user to read the data from the start.<lineBreak/>
		/// As noted previously, if a sound is opened as a stream and this function is called to read some data, then you will 'miss the start' of the sound.
		/// </para>
		/// <para>
		/// <see cref="Channel.SetPosition"/> will have the same result. These function will flush the stream buffer and read in a chunk of audio internally. This is why if you want to read from an absolute position you should use <see cref="SeekData"/> and not the previously mentioned functions.<lineBreak/>
		/// Remember if you are calling <see cref="O:FMOD.Core.Sound.ReadData"/> and <see cref="SeekData"/> on a stream it is up to you to cope with the side effects that may occur. Information functions such as <see cref="Channel.GetPosition"/> may give misleading results. Calling <see cref="Channel.SetPosition"/> will reset and flush the stream, leading to the time values returning to their correct position.
		/// </para>
		/// <alert class="note">
		/// <para><b>Thread safety.</b> If you call this from another stream callback, or any other thread besides the main thread, make sure to put a critical section around the call, and another around <see cref="HandleBase.Dispose"/> in case the sound is still being read from while releasing.</para>
		/// <para>This function is thread safe to call from a stream callback or different thread as long as it doesnt conflict with a call to <see cref="HandleBase.Dispose"/>.</para>
		/// </alert>
		/// </remarks>
		/// <seealso cref="ReadData(uint)"/>
		/// <seealso cref="SeekData"/>
		/// <see cref="Channel.SetPosition"/>
		/// <seealso cref="Lock"/>
		/// <seealso cref="Unlock"/>
		public uint ReadData(IntPtr buffer, uint length)
		{
			NativeInvoke(FMOD_Sound_ReadData(this, buffer, length, out var readBytes));
			return readBytes;
		}

		/// <summary>
		/// Helper function to automatically ready the desired amount from the current position in the stream.
		/// </summary>
		/// <param name="length">Mumber of bytes to read into the buffer.</param>
		/// <returns>An array if bytes filled with the data. If length if stream was shorter than the <paramref name="length"/>, the array will be shortened to match.</returns>
		/// <seealso cref="ReadData(IntPtr, uint)"/>
		/// <seealso cref="SeekData"/>
		/// <see cref="Channel.SetPosition"/>
		/// <seealso cref="Lock"/>
		/// <seealso cref="Unlock"/>
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

		/// <summary>
		/// Seeks a sound for use with data reading. 
		/// <para>This is not a function to "seek a sound" for normal use. This is for use in conjunction with <see cref="ReadData(IntPtr, uint)"/>.</para>
		/// </summary>
		/// <param name="pcm">Offset to seek to in PCM samples. </param>
		/// <remarks>
		/// <alert class="note">If a stream is opened and this function is called to read some data, then it will advance the internal file pointer, so data will be skipped if you play the stream. Also calling position / time information functions will lead to misleading results.</alert>
		/// <para>A stream can be reset before playing by setting the position of the channel (ie using <see cref="Channel.SetPosition"/>), which will make it seek, reset and flush the stream buffer. This will make it sound correct again.</para>
		/// <para>Remember if you are calling <see cref="ReadData(IntPtr, uint)"/> and <see cref="SeekData"/> on a stream it is up to you to cope with the side effects that may occur.</para>
		/// </remarks>
		/// <seealso cref="ReadData(IntPtr, uint)"/>
		/// <seealso cref="ReadData(uint)"/>
		/// <seealso cref="Channel.SetPosition"/>
		public void SeekData(uint pcm)
		{
			NativeInvoke(FMOD_Sound_SeekData(this, pcm));
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









		public void SetConeSettings(float insideAngle, float outsideAngle, float outsideVolume)
		{
			NativeInvoke(FMOD_Sound_Set3DConeSettings(this, insideAngle, outsideAngle, outsideVolume));
			OnConeSettings3DChanged();
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

	}
}