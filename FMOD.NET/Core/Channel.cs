#region License

// Channel.cs is distributed under the Microsoft Public License (MS-PL)
// 
// Copyright (c) 2018,  Eric Freed
// All Rights Reserved.
// 
// This license governs use of the accompanying software. If you use the software, you
// accept this license. If you do not accept the license, do not use the software.
// 
// 1. Definitions
// The terms "reproduce," "reproduction," "derivative works," and "distribution" have the
// same meaning here as under U.S. copyright law.
// A "contribution" is the original software, or any additions or changes to the software.
// A "contributor" is any person that distributes its contribution under this license.
// "Licensed patents" are a contributor's patent claims that read directly on its contribution.
// 
// 2. Grant of Rights
// (A) Copyright Grant- Subject to the terms of this license, including the license conditions 
// and limitations in section 3, each contributor grants you a non-exclusive, worldwide, royalty-free 
// copyright license to reproduce its contribution, prepare derivative works of its contribution, and 
// distribute its contribution or any derivative works that you create.
// 
// (B) Patent Grant- Subject to the terms of this license, including the license conditions and 
// limitations in section 3, each contributor grants you a non-exclusive, worldwide, royalty-free license
//  under its licensed patents to make, have made, use, sell, offer for sale, import, and/or otherwise 
// dispose of its contribution in the software or derivative works of the contribution in the software.
// 
// 3. Conditions and Limitations
// (A) No Trademark License- This license does not grant you rights to use any contributors' name, 
// logo, or trademarks.
// 
// (B) If you bring a patent claim against any contributor over patents that you claim are infringed by 
// the software, your patent license from such contributor to the software ends automatically.
// 
// (C) If you distribute any portion of the software, you must retain all copyright, patent, trademark, and
//  attribution notices that are present in the software.
// 
// (D) If you distribute any portion of the software in source code form, you may do so only under this 
// license by including a complete copy of this license with your distribution. If you distribute any portion
//  of the software in compiled or object code form, you may only do so under a license that complies 
// with this license.
// 
// (E) The software is licensed "as-is." You bear the risk of using it. The contributors give no express 
// warranties, guarantees or conditions. You may have additional consumer rights under your local laws 
// which this license cannot change. To the extent permitted under your local laws, the contributors 
// exclude the implied warranties of merchantability, fitness for a particular purpose and non-infringement.
// 
// Created 9:49 PM 02/15/2018

#endregion

#region Using Directives

using System;
using System.Runtime.InteropServices;
using FMOD.Arguments;
using FMOD.Data;
using FMOD.Enumerations;
using FMOD.Structures;

#endregion

namespace FMOD.Core
{
	/// <inheritdoc />
	/// <summary>
	///     A specialized <see cref="T:FMOD.Core.ChannelControl" /> with common playback functions including seeking and
	///     looping sounds.
	/// </summary>
	/// <seealso cref="T:FMOD.Core.ChannelControl" />
	public partial class Channel : ChannelControl
	{
		/// <summary>
		///     Occurs when the <see cref="Channel" /> is either added or removed from a <see cref="Core.ChannelGroup" />.
		/// </summary>
		/// <seealso cref="ChannelGroup" />
		/// <seealso cref="FMOD.Core.ChannelGroup" />
		public event EventHandler ChannelGroupChanged;

		/// <summary>
		///     Occurs when frequency has changed.
		/// </summary>
		/// <seealso cref="Frequency" />
		/// <seealso cref="ChannelControl.Pitch" />
		/// <seealso cref="Sound.DefaultFrequency" />
		/// <seealso cref="Sound.SetDefaults" />
		public event EventHandler FrequencyChanged;

		/// <summary>
		///     Occurs when <see cref="LoopCount" /> property has changed.
		/// </summary>
		/// <seealso cref="LoopCount" />
		public event EventHandler LoopCountChanged;

		/// <summary>
		///     Occurs when a loop point is added.
		/// </summary>
		/// <seealso cref="SetLoopPoints(LoopPoints)" />
		/// <seealso cref="SetLoopPoints(uint, uint, TimeUnit)" />
		/// <seealso cref="SetLoopPoints(uint, uint, TimeUnit, TimeUnit)" />
		public event EventHandler LoopPointAdded;

		/// <summary>
		///     Occurs when the current position is changed.
		/// </summary>
		/// <seealso cref="SetPosition" />
		public event EventHandler PositionChanged;

		/// <summary>
		///     Occurs when priority for the channel is changed.
		/// </summary>
		/// <seealso cref="Priority" />
		public event EventHandler PriorityChanged;

		/// <summary>
		///     Occurs when a sound has completed playing and ends.
		/// </summary>
		/// <alert class="note">
		///     This event is not fired when a sound is currently looping or stopped via
		///     <see cref="ChannelControl.Stop" />.
		/// </alert>
		/// <seealso cref="SoundEndedEventArgs" />
		/// <seealso cref="ChannelControl.Stop" />
		public event EventHandler<SoundEndedEventArgs> SoundEnded;

		/// <summary>
		///     Occurs when sync-point is encountered.
		/// </summary>
		/// <seealso cref="SyncPointEncounteredEventArgs" />
		public event EventHandler<SyncPointEncounteredEventArgs> SyncPointEncountered;

		/// <inheritdoc />
		/// <summary>
		///     Initializes a new instance of the <see cref="T:FMOD.Core.Channel" /> class.
		/// </summary>
		/// <param name="handle">The handle.</param>
		internal Channel(IntPtr handle) : base(handle)
		{
		}

		/// <summary>
		///     Gets the currently playing sound for this channel.
		/// </summary>
		/// <value>
		///     The current sound.
		/// </value>
		/// <remarks>If a sound is not playing the returned pointer will be <c>null</c>.</remarks>
		/// <seealso cref="FmodSystem.PlaySound" />
		public Sound CurrentSound
		{
			get
			{
				NativeInvoke(FMOD_Channel_GetCurrentSound(this, out var sound));
				return sound == IntPtr.Zero ? null : Factory.Create<Sound>(sound);
			}
		}

		/// <summary>
		///     Gets or sets the currently assigned channel group for the channel.
		/// </summary>
		/// <value>
		///     The channel group.
		/// </value>
		/// <remarks>
		///     Setting a channel to a channel group removes it from any previous group, it does not allow sharing of channel
		///     groups.
		/// </remarks>
		/// <seealso cref="ChannelGroupChanged" />
		public ChannelGroup ChannelGroup
		{
			get
			{
				NativeInvoke(FMOD_Channel_GetChannelGroup(this, out var group));
				return Factory.Create<ChannelGroup>(group);
			}
			set
			{
				NativeInvoke(FMOD_Channel_SetChannelGroup(this, value));
				ChannelGroupChanged?.Invoke(this, EventArgs.Empty);
			}
		}

		/// <summary>
		///     <para>Gets whether the channel is virtual (emulated) or not due to the virtual channel management system.</para>
		///     <para><c>true</c> = inaudible and currently being emulated at no CPU cost</para>
		///     <para><c>false</c> = real voice that should be audible.</para>
		/// </summary>
		/// <value>
		///     <c>true</c> if this instance is virtual; otherwise, <c>false</c>.
		/// </value>
		/// <seealso cref="FmodSystem.PlaySound" />
		/// <seealso cref="ChannelControl.GetAudibility" />
		public bool IsVirtual
		{
			get
			{
				NativeInvoke(FMOD_Channel_IsVirtual(this, out var isVirtual));
				return isVirtual;
			}
		}

		/// <summary>
		///     <para>Gets or sets the current loop count for the specified channel.</para>
		///     <para><c>0</c> = oneshot, <c>1</c> = loop once then stop, <c>-1</c> = loop forever, default = <c>-1</c>. </para>
		/// </summary>
		/// <value>
		///     The loop count.
		/// </value>
		/// <remarks>
		///     <para>
		///         <i>Issues with streamed audio:</i>
		///     </para>
		///     <para>
		///         When changing the loop count, sounds created with <seealso cref="O:FMOD.Core.FmodSystem.CreateStream" /> or
		///         <seealso cref="Mode.CreateStream" /> may have already been pre-buffered and executed their loop logic ahead of
		///         time before this call was even made. This is dependant on the size of the sound versus the size of the stream
		///         decode buffer (see <see cref="CreateSoundExInfo" />). If this happens, you may need to reflush the stream
		///         buffer by
		///         calling <see cref="SetPosition" />. Note this will usually only happen if you have sounds or loop points that
		///         are
		///         smaller than the stream decode buffer size.
		///     </para>
		/// </remarks>
		/// <seealso cref="LoopCountChanged" />
		/// <seealso cref="CreateSoundExInfo" />
		/// <seealso cref="O:FMOD.Core.Channel.SetLoopPoints" />
		/// <seealso cref="FMOD.Enumerations.Mode" />
		public int LoopCount
		{
			get
			{
				NativeInvoke(FMOD_Channel_GetLoopCount(this, out var count));
				return count;
			}
			set
			{
				NativeInvoke(FMOD_Channel_SetLoopCount(this, value.Clamp(-1, value)));
				LoopCountChanged?.Invoke(this, EventArgs.Empty);
			}
		}

		/// <summary>
		///     Gets the internal channel index for a channel.
		/// </summary>
		/// <value>
		///     The index.
		/// </value>
		/// <seealso cref="FmodSystem.PlaySound" />
		public int Index
		{
			get
			{
				NativeInvoke(FMOD_Channel_GetIndex(this, out var index));
				return index;
			}
		}

		/// <summary>
		///     <para>Gets or sets the channel frequency or playback rate, in Hz.</para>
		///     <para>
		///         This value can also be negative to play the sound backwards (negative frequencies allowed with non-stream
		///         sounds only).
		///     </para>
		/// </summary>
		/// <value>
		///     The frequency.
		/// </value>
		/// <remarks>
		///     <para>
		///         When a sound is played, it plays at the default frequency of the sound which can be set by
		///         <see cref="Sound.DefaultFrequency" />.
		///     </para>
		///     <para>For most file formats, the default frequency is determined by the audio format.</para>
		/// </remarks>
		/// <seealso cref="FrequencyChanged" />
		/// <seealso cref="Sound.DefaultFrequency" />
		/// <seealso cref="Sound.SetDefaults" />
		public float Frequency
		{
			get
			{
				NativeInvoke(FMOD_Channel_GetFrequency(this, out var frequency));
				return frequency;
			}
			set
			{
				NativeInvoke(FMOD_Channel_SetFrequency(this, value));
				FrequencyChanged?.Invoke(this, EventArgs.Empty);
			}
		}

		/// <summary>
		///     <para> Gets or sets the priority for the channel after it has been played.</para>
		///     <para>From <c>0</c> (most important) to <c>256</c> (least important).</para>
		///     <para>Default = <c>128</c>.</para>
		/// </summary>
		/// <value>
		///     The priority.
		/// </value>
		/// <remarks>
		///     When more channels than available are played the virtual channel system will choose existing channels to steal.
		///     Lower priority sounds will always be stolen before higher priority sounds. For channels of equal priority, that
		///     with the quietest <see cref="ChannelControl.GetAudibility" /> value will be stolen.
		/// </remarks>
		/// <seealso cref="PriorityChanged" />
		/// <seealso cref="ChannelControl.GetAudibility" />
		public int Priority
		{
			get
			{
				NativeInvoke(FMOD_Channel_GetPriority(this, out var priority));
				return priority;
			}
			set
			{
				NativeInvoke(FMOD_Channel_SetPriority(this, value.Clamp(0, 256)));
				PriorityChanged?.Invoke(this, EventArgs.Empty);
			}
		}

		/// <summary>
		///     Retrieves the loop points for the channel.
		/// </summary>
		/// <param name="timeUnit">Time format used for the loop start and end point.</param>
		/// <returns>A <see cref="LoopPoints" /> instance describing the loop points.</returns>
		/// <seealso cref="O:FMOD.Core.Channel.SetLoopPoints" />
		/// <seealso cref="TimeUnit" />
		/// <seealso cref="LoopPoints" />
		public LoopPoints GetLoopPoints(TimeUnit timeUnit = TimeUnit.Ms)
		{
			return GetLoopPoints(timeUnit, timeUnit);
		}

		/// <summary>
		///     Gets the loop points.
		/// </summary>
		/// <param name="startUnit">Time format used for the loop start point.</param>
		/// <param name="endUnit">Time format used for the loop end point.</param>
		/// <returns>A <see cref="LoopPoints" /> instance describing the loop points.</returns>
		/// <seealso cref="O:FMOD.Core.Channel.SetLoopPoints" />
		/// <seealso cref="TimeUnit" />
		/// <seealso cref="LoopPoints" />
		public LoopPoints GetLoopPoints(TimeUnit startUnit, TimeUnit endUnit)
		{
			NativeInvoke(FMOD_Channel_GetLoopPoints(this, out var start, startUnit,
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
		///     Returns the current playback position for the channel.
		/// </summary>
		/// <param name="timeUnits">Time unit to retrieve into the return value.</param>
		/// <returns>The position of the sound.</returns>
		/// <remarks>
		///     Certain timeunits do not work depending on the file format. For example <see cref="TimeUnit.ModOrder" /> will
		///     not work with an MP3 file.
		/// </remarks>
		/// <seealso cref="SetPosition" />
		/// <seealso cref="TimeUnit" />
		public uint GetPosition(TimeUnit timeUnits = TimeUnit.Ms)
		{
			NativeInvoke(FMOD_Channel_GetPosition(this, out var position, timeUnits));
			return position;
		}

		/// <summary>
		///     Sets the loop points within the channel.
		/// </summary>
		/// <param name="points">A <see cref="LoopPoints" /> instance describing the points.</param>
		/// <remarks>
		///     <para>
		///         If a sound was 44100 samples long and you wanted to loop the whole sound, the loop start would be <c>0</c>,
		///         and the loop end would be 44099, <i>not</i> 44100. You wouldn't use milliseconds in this case because they are
		///         not sample accurate.
		///     </para>
		///     <para>
		///         <i>Issues with streamed audio:</i>
		///     </para>
		///     <para>
		///         When changing the loop count, sounds created with <see cref="O:FMOD.Core.FmodSystem.CreateStream" /> or
		///         <see cref="FMOD.Enumerations.Mode.CreateStream" /> may have already been pre-buffered and executed their loop
		///         logic ahead of time before this call was even made. This is dependant on the size of the sound versus the size
		///         of the stream decode buffer (see <see cref="CreateSoundExInfo" />). If this happens, you may need to reflush
		///         the stream buffer by calling <see cref="SetPosition" />. Note this will usually only happen if you have sounds
		///         or loop points that are smaller than the stream decode buffer size.
		///     </para>
		/// </remarks>
		/// <seealso cref="O:FMOD.Core.Channel.GetLoopPoints" />
		/// <seealso cref="LoopPoints" />
		/// <seealso cref="TimeUnit" />
		/// <seealso cref="O:FMOD.Core.FmodSystem.CreateStream" />
		/// <seealso cref="Enumerations.Mode" />
		public void SetLoopPoints(LoopPoints points)
		{
			SetLoopPoints(points.LoopStart, points.LoopEnd, points.StartTimeUnit, points.EndTimeUnit);
		}

		/// <summary>
		///     Sets the loop points within the channel.
		/// </summary>
		/// <param name="loopStart">Loop start point, this point in time is played so it is inclusive.</param>
		/// <param name="loopEnd">Loop end point, this point in time is played so it is inclusive. </param>
		/// <param name="timeUnit">Time format used for the loop start and end point.</param>
		/// <remarks>
		///     <para>
		///         If a sound was 44100 samples long and you wanted to loop the whole sound, <paramref name="loopStart" /> would
		///         be <c>0</c>, and <paramref name="loopEnd" /> would be 44099, <i>not</i> 44100. You wouldn't use milliseconds in
		///         this case because they are not sample accurate.
		///     </para>
		///     <para>
		///         <i>Issues with streamed audio:</i>
		///     </para>
		///     <para>
		///         When changing the loop count, sounds created with <see cref="O:FMOD.Core.FmodSystem.CreateStream" /> or
		///         <see cref="FMOD.Enumerations.Mode.CreateStream" /> may have already been pre-buffered and executed their loop
		///         logic ahead of time before this call was even made. This is dependant on the size of the sound versus the size
		///         of the stream decode buffer (see <see cref="CreateSoundExInfo" />). If this happens, you may need to reflush
		///         the stream buffer by calling <see cref="SetPosition" />. Note this will usually only happen if you have sounds
		///         or loop points that are smaller than the stream decode buffer size.
		///     </para>
		/// </remarks>
		/// <seealso cref="O:FMOD.Core.Channel.GetLoopPoints" />
		/// <seealso cref="LoopPoints" />
		/// <seealso cref="TimeUnit" />
		/// <seealso cref="O:FMOD.Core.FmodSystem.CreateStream" />
		/// <seealso cref="Enumerations.Mode" />
		public void SetLoopPoints(uint loopStart, uint loopEnd, TimeUnit timeUnit = TimeUnit.Ms)
		{
			SetLoopPoints(loopStart, loopEnd, timeUnit, timeUnit);
		}

		/// <summary>
		///     Sets the loop points within the channel.
		/// </summary>
		/// <param name="loopStart">Loop start point, this point in time is played so it is inclusive.</param>
		/// <param name="loopEnd">Loop end point, this point in time is played so it is inclusive. </param>
		/// <param name="startUnit">Time format used for the loop start point.</param>
		/// <param name="endUnit">Time format used for the loop start point.</param>
		/// <remarks>
		///     <para>
		///         If a sound was 44100 samples long and you wanted to loop the whole sound, <paramref name="loopStart" /> would
		///         be <c>0</c>, and <paramref name="loopEnd" /> would be 44099, <i>not</i> 44100. You wouldn't use milliseconds in
		///         this case because they are not sample accurate.
		///     </para>
		///     <para>
		///         <i>Issues with streamed audio:</i>
		///     </para>
		///     <para>
		///         When changing the loop count, sounds created with <see cref="O:FMOD.Core.FmodSystem.CreateStream" /> or
		///         <see cref="FMOD.Enumerations.Mode.CreateStream" /> may have already been pre-buffered and executed their loop
		///         logic ahead of time before this call was even made. This is dependant on the size of the sound versus the size
		///         of the stream decode buffer (see <see cref="CreateSoundExInfo" />). If this happens, you may need to reflush
		///         the stream buffer by calling <see cref="SetPosition" />. Note this will usually only happen if you have sounds
		///         or loop points that are smaller than the stream decode buffer size.
		///     </para>
		/// </remarks>
		/// <seealso cref="O:FMOD.Core.Channel.GetLoopPoints" />
		/// <seealso cref="LoopPoints" />
		/// <seealso cref="TimeUnit" />
		/// <seealso cref="O:FMOD.Core.FmodSystem.CreateStream" />
		/// <seealso cref="Enumerations.Mode" />
		public void SetLoopPoints(uint loopStart, uint loopEnd, TimeUnit startUnit, TimeUnit endUnit)
		{
			NativeInvoke(FMOD_Channel_SetLoopPoints(this, loopStart, startUnit, loopEnd, endUnit));
			LoopPointAdded?.Invoke(this, EventArgs.Empty);
		}

		/// <summary>
		///     Sets the playback position for the currently playing sound to the specified offset.
		/// </summary>
		/// <param name="position">
		///     Position of the channel to set in units specified in the <paramref name="timeUnits" />
		///     parameter.
		/// </param>
		/// <param name="timeUnits">Time unit to set the channel position by.</param>
		/// <remarks>
		///     <para>
		///         Certain timeunits do not work depending on the file format. For example <see cref="TimeUnit.ModOrder" /> will
		///         not work with an MP3 file.
		///     </para>
		///     <para>
		///         If you are calling this function on a stream, it has to possibly reflush its buffer to get zero latency
		///         playback when it resumes playing, therefore it could potentially cause a stall or take a small amount of time
		///         to do this.
		///     </para>
		///     <para>
		///         If you are using <see cref="Mode.NonBlocking" />, note that a stream will go into
		///         <see cref="OpenState.SetPosition" /> state (see <see cref="O:FMOD.Core.Sound.GetOpenState" />) and sound
		///         commands will return <see cref="Result.NotReady" />. <see cref="GetPosition" /> will also not update until this
		///         non-blocking setposition operation has completed.
		///     </para>
		/// </remarks>
		/// <alert class="warning">
		///     Using a VBR source that does not have an associated seek table or seek information (such as MP3 or MOD/S3M/XM/IT)
		///     may cause inaccurate seeking if you specify <see cref="TimeUnit.Ms" /> or <see cref="TimeUnit.Pcm" />. If you want
		///     <b>FMOD</b> to create a PCM vs bytes seek table so that seeking is accurate, you will have to specify
		///     <see cref="Mode.AccurateTime" /> when loading or opening the sound. This means there is a slight delay as
		///     <b>FMOD</b> scans the whole file when loading the sound to create this table.
		/// </alert>
		/// <seealso cref="GetPosition" />
		/// <seealso cref="TimeUnit" />
		/// <seealso cref="Enumerations.Mode" />
		/// <seealso cref="OpenState" />
		public void SetPosition(uint position, TimeUnit timeUnits = TimeUnit.Ms)
		{
			NativeInvoke(FMOD_Channel_SetPosition(this, position, timeUnits));
			PositionChanged?.Invoke(this, EventArgs.Empty);
		}

		/// <summary>
		///     <para>Invoked when a playing sound ends.</para>
		///     <para>
		///         If overridden, ensure to either call the base method or call <see cref="SafeHandle.SetHandleAsInvalid" /> on
		///         the <see cref="Channel" />.
		///     </para>
		/// </summary>
		/// <returns>
		///     The <see cref="Result" /> specifying the outcome of the event.
		/// </returns>
		/// <seealso cref="Sound" />
		/// <seealso cref="Result" />
		protected override Result OnSoundEnd()
		{
			SetHandleAsInvalid();
			SoundEnded?.Invoke(this, new SoundEndedEventArgs(CurrentSound));
			return Result.OK;
		}

		/// <summary>
		///     Invoked when a sync-point is encountered.
		/// </summary>
		/// <param name="syncPointIndex">Index of the sync-point point.</param>
		/// <returns>The <see cref="Result" /> specifying the outcome of the event.</returns>
		protected override Result OnSyncPointEncountered(int syncPointIndex)
		{
			if (SyncPointEncountered != null)
			{
				var sound = CurrentSound;
				var syncpoint = sound.GetSyncPoint(syncPointIndex);
				var info = sound.GetSyncpointInfo(syncpoint);
				SyncPointEncountered.Invoke(this, new SyncPointEncounteredEventArgs(syncPointIndex, syncpoint, info));
			}
			return Result.OK;
		}
	}
}