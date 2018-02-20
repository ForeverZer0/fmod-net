#region License

// SoundGroup.cs is distributed under the Microsoft Public License (MS-PL)
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
using System.Text;
using FMOD.Enumerations;

#endregion

namespace FMOD.Core
{
	public partial class SoundGroup : HandleBase
	{
		#region Constructors

		/// <summary>
		///     Initializes a new instance of the <see cref="SoundGroup" /> class.
		/// </summary>
		/// <param name="handle">The handle.</param>
		protected SoundGroup(IntPtr handle) : base(handle)
		{
		}

		#endregion

		#region Properties

		/// <summary>
		///     <para>Gets or sets a value to limit the number of concurrent playbacks of sounds in a <see cref="SoundGroup" />.</para>
		///     <para>
		///         After this, if the sounds in the sound group are playing this many times, any attepts to play more of the
		///         sounds in the sound group will by default fail with <see cref="Result.MaxAudible" />.
		///     </para>
		///     <para><c>-1</c> = unlimited. <c>0</c> = no sounds in this group will succeed.</para>
		///     <para>Default = <c>-1</c>. </para>
		/// </summary>
		/// <value>
		///     The maximum audible.
		/// </value>
		/// <remarks>
		///     <para>
		///         <see cref="NumberPlaying" /> can be used to determine how many instances of the sounds in the sound group are
		///         currently playing.
		///     </para>
		///     <para>
		///         Use <see cref="MaxAudibleBehavior" /> to change the way the sound playback behaves when too many sounds are
		///         playing. Muting, failing and stealing behaviors can be specified.
		///     </para>
		/// </remarks>
		/// <seealso cref="MaxAudibleBehaviorChanged" />
		/// <seealso cref="FmodSystem.MasterSoundGroup" />
		/// <seealso cref="FmodSystem.CreateSoundGroup" />
		/// <seealso cref="MaxAudibleBehavior" />
		/// <seealso cref="NumberPlaying" />
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
				OnMaxAudibleChanged();
			}
		}

		/// <summary>
		///     Gets or sets the current max audible behavior method.
		///     <para>Default = <see cref="SoundGroupBehavior.Fail" />.</para>
		/// </summary>
		/// <value>
		///     The maximum audible behavior.
		/// </value>
		/// <seealso cref="MaxAudibleBehaviorChanged" />
		/// <seealso cref="SoundGroupBehavior" />
		/// <seealso cref="MaxAudible" />
		/// <seealso cref="MuteFadeSpeed" />
		/// <seealso cref="FmodSystem.CreateSoundGroup" />
		/// <seealso cref="FmodSystem.MasterSoundGroup" />
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
				OnMaxAudibleBehaviorChanged();
			}
		}

		/// <summary>
		///     <para>Gets or sets a time in seconds for <see cref="SoundGroupBehavior.Mute" /> behavior to fade with. </para>
		///     <para>Fade time in seconds (<c>1.0</c> = 1 second).</para>
		///     <para>Default = <c>0.0</c>.</para>
		/// </summary>
		/// <value>
		///     The mute fade speed.
		/// </value>
		/// <remarks>
		///     <para>
		///         When more sounds are playing in a <see cref="SoundGroup" /> than are specified with <see cref="MaxAudible" />
		///         , the least important sound (ie lowest priority / lowest audible volume due to 3D position, volume etc) will
		///         fade to silence if <see cref="SoundGroupBehavior.Mute" /> is used, and any previous sounds that were silent
		///         because of this rule will fade in if they are more important.
		///     </para>
		///     <para>
		///         If a mode besides <see cref="MaxAudibleBehavior" /> aside from <see cref="SoundGroupBehavior.Mute" /> is
		///         used, the fade speed is ignored.
		///     </para>
		/// </remarks>
		/// <seealso cref="MuteFadeSpeedChanged" />
		/// <seealso cref="MaxAudibleBehavior" />
		/// <seealso cref="MaxAudible" />
		/// <seealso cref="SoundGroupBehavior" />
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
				OnMuteFadeSpeedChanged();
			}
		}

		/// <summary>
		///     Gets the name of the <see cref="SoundGroup" /> specified when it was created.
		/// </summary>
		/// <value>
		///     The name.
		/// </value>
		/// <seealso cref="FmodSystem.CreateSoundGroup" />
		/// <seealso cref="FmodSystem.MasterSoundGroup" />
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

		/// <summary>
		///     Gets the number of currently playing channels for the <see cref="SoundGroup" />.
		/// </summary>
		/// <value>
		///     The number of actively playing channels from sounds in this sound group.
		/// </value>
		/// <seealso cref="FmodSystem.CreateSoundGroup" />
		/// <seealso cref="FmodSystem.MasterSoundGroup" />
		public int NumberPlaying
		{
			get
			{
				NativeInvoke(FMOD_SoundGroup_GetNumPlaying(this, out var numPlaying));
				return numPlaying;
			}
		}

		/// <summary>
		///     Gets the parent <see cref="FmodSystem" /> object that was used to create this object.
		/// </summary>
		/// <value>
		///     The parent system.
		/// </value>
		/// <seealso cref="FmodSystem.CreateSoundGroup" />
		/// <seealso cref="FmodSystem.MasterSoundGroup" />
		public FmodSystem ParentSystem
		{
			get
			{
				NativeInvoke(FMOD_SoundGroup_GetSystemObject(this, out var system));
				return Factory.Create<FmodSystem>(system);
			}
		}

		/// <summary>
		///     Gets the current number of <see cref="Sound" /> instances in this <see cref="SoundGroup" />.
		/// </summary>
		/// <value>
		///     The sound count.
		/// </value>
		/// <seealso cref="Sound" />
		/// <seealso cref="MaxAudible" />
		/// <seealso cref="GetSound" />
		public int SoundCount
		{
			get
			{
				NativeInvoke(FMOD_SoundGroup_GetNumSounds(this, out var count));
				return count;
			}
		}

		/// <summary>
		///     Gets or sets  a user value that the <see cref="SoundGroup" /> object will store internally.
		/// </summary>
		/// <value>
		///     The user data.
		/// </value>
		/// <remarks>
		///     <para>This function is primarily used in case the user wishes to "attach" data to an <b>FMOD</b> object.</para>
		///     <para>
		///         It can be useful if an <b>FMOD</b> callback passes an object of this type as a parameter, and the user does
		///         not know which object it is (if many of these types of objects exist).
		///     </para>
		/// </remarks>
		/// <seealso cref="UserDataChanged" />
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
				OnUserDataChanged();
			}
		}

		/// <summary>
		///     <para>
		///         Gets or sets the volume for a sound group, affecting all channels playing the sounds in this
		///         <see cref="SoundGroup" />.
		///     </para>
		///     <para>A linear volume level. <c>0.0</c> = silent, <c>1.0</c> = full volume. </para>
		///     <para>Default = <c>1.0</c>. Negative volumes and amplification (greater than <c>1.0</c>) are supported.</para>
		/// </summary>
		/// <value>
		///     The volume.
		/// </value>
		/// <seealso cref="VolumeChanged" />
		/// <seealso cref="FmodSystem.CreateSoundGroup" />
		/// <seealso cref="FmodSystem.MasterSoundGroup" />
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
				OnVolumeChanged();
			}
		}

		#endregion

		#region Methods

		/// <summary>
		///     Gets a <see cref="Sound" /> from within a sound group.
		/// </summary>
		/// <param name="index">Index of the sound that is to be retrieved.</param>
		/// <returns>The <see cref="Sound" /> instance at the specified index.</returns>
		/// <remarks>Use <see cref="SoundCount" /> in conjunction with this function to enumerate all sounds in a sound group.</remarks>
		/// <seealso cref="SoundCount" />
		/// <seealso cref="Sound" />
		/// <seealso cref="FmodSystem.CreateSoundGroup" />
		public Sound GetSound(int index)
		{
			NativeInvoke(FMOD_SoundGroup_GetSound(this, index, out var sound));
			return sound == IntPtr.Zero ? null : Factory.Create<Sound>(sound);
		}

		/// <summary>
		///     Stops all sounds within this <see cref="SoundGroup" />.
		/// </summary>
		/// <seealso cref="Stopped" />
		/// <seealso cref="FmodSystem.PlaySound" />
		public void Stop()
		{
			NativeInvoke(FMOD_SoundGroup_Stop(this));
			OnStopped();
		}

		#endregion
	}
}