#region License

// Sound.Events.cs is distributed under the Microsoft Public License (MS-PL)
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
// Created 12:02 AM 02/20/2018

#endregion

#region Using Directives

using System;
using FMOD.NET.Arguments;
using FMOD.NET.Data;
using FMOD.NET.Structures;

#endregion

namespace FMOD.NET.Core
{
	public partial class Sound
	{
		#region Events

		/// <summary>
		///     Occurs when the 3D cone settings have changed.
		/// </summary>
		/// <seealso cref="SetConeSettings" />
		/// <seealso cref="ConeSettings3D" />
		/// <seealso cref="ConeSettings" />
		public event EventHandler ConeSettings3DChanged;

		/// <summary>
		///     Occurs when 3D custom roll-off has changed.
		/// </summary>
		/// <seealso cref="CustomRolloff3D" />
		/// <seealso cref="Vector" />
		public event EventHandler CustomRolloff3DChanged;

		/// <summary>
		///     Occurs when the default frequency or priority is changed.
		/// </summary>
		/// <seealso cref="DefaultFrequency" />
		/// <seealso cref="DefaultPriority" />
		/// <seealso cref="SetDefaults" />
		public event EventHandler DefaultsChanged;

		/// <summary>
		///     Occurs when 3D minimum or maximum distance has changed.
		/// </summary>
		/// <seealso cref="MinDistance3D" />
		/// <seealso cref="MaxDistance3D" />
		/// <seealso cref="SetMinMaxDistance" />
		public event EventHandler Distance3DChanged;

		/// <summary>
		///     Occurs when the sound buffer is locked by the user.
		/// </summary>
		/// <seealso cref="Lock" />
		/// <seealso cref="Unlock" />
		public event EventHandler Locked;

		/// <summary>
		///     Occurs when <see cref="LoopCount" /> property is changed.
		/// </summary>
		/// <seealso cref="LoopCount" />
		public event EventHandler LoopCountChanged;

		/// <summary>
		///     Occurs when a loop points are added to the sound.
		/// </summary>
		/// <seealso cref="SetLoopPoints(LoopPoints)" />
		/// <seealso cref="SetLoopPoints(uint,uint,FMOD.NET.Enumerations.TimeUnit)" />
		/// <seealso cref="SetLoopPoints(uint,uint,FMOD.NET.Enumerations.TimeUnit,FMOD.NET.Enumerations.TimeUnit)" />
		public event EventHandler LoopPointAdded;

		/// <summary>
		///     Occurs when <see cref="Mode" /> property is changed.
		/// </summary>
		/// <seealso cref="Mode" />
		/// <seealso cref="Enumerations.Mode" />
		public event EventHandler ModeChanged;

		/// <summary>
		///     Occurs when <see cref="MusicSpeed" /> property is changed.
		/// </summary>
		/// <seealso cref="MusicSpeed" />
		public event EventHandler MusicSpeedChanged;

		/// <summary>
		///     Occurs when the volume for a music channel is changed.
		/// </summary>
		/// <seealso cref="SetMusicVolume"></seealso>
		/// <seealso cref="SoundMusicVolumeChangedEventArgs"></seealso>
		public event EventHandler<SoundMusicVolumeChangedEventArgs> MusicVolumeChanged;

		/// <summary>
		///     Occurs when <see cref="SoundGroup" /> property is changed.
		/// </summary>
		/// <seealso cref="SoundGroup" />
		/// <seealso cref="Core.SoundGroup" />
		public event EventHandler SoundGroupChanged;

		/// <summary>
		///     Occurs when a sync-point is added to the <see cref="Sound" />.
		/// </summary>
		/// <seealso cref="SyncPointEventArgs" />
		/// <seealso cref="AddSyncPoint(FMOD.NET.Data.SyncPointInfo)" />
		/// <seealso cref="AddSyncPoint(uint,FMOD.NET.Enumerations.TimeUnit,string)" />
		public event EventHandler<SyncPointEventArgs> SyncPointAdded;

		/// <summary>
		///     Occurs when a sync-point is removed from the <see cref="Sound" />.
		/// </summary>
		/// <seealso cref="SyncPointEventArgs" />
		/// <seealso cref="DeleteSyncPoint" />
		public event EventHandler<SyncPointEventArgs> SyncPointDeleted;

		/// <summary>
		///     Occurs when the sound buffer is unlocked by the user.
		/// </summary>
		/// <seealso cref="Lock" />
		/// <seealso cref="Unlock" />
		public event EventHandler Unlocked;

		#endregion

		#region Event Invokers

		/// <summary>
		///     Raises the <see cref="ConeSettings3DChanged" /> event.
		/// </summary>
		/// <seealso cref="EventArgs" />
		protected virtual void OnConeSettings3DChanged()
		{
			ConeSettings3DChanged?.Invoke(this, EventArgs.Empty);
		}

		/// <summary>
		///     Raises the <see cref="CustomRolloff3DChanged" /> event.
		/// </summary>
		/// <seealso cref="EventArgs" />
		protected virtual void OnCustomRolloff3DChanged()
		{
			CustomRolloff3DChanged?.Invoke(this, EventArgs.Empty);
		}


		/// <summary>
		///     Raises the <see cref="DefaultsChanged" /> event.
		/// </summary>
		/// <seealso cref="EventArgs" />
		protected virtual void OnDefaultsChanged()
		{
			DefaultsChanged?.Invoke(this, EventArgs.Empty);
		}

		/// <summary>
		///     Raises the <see cref="Distance3DChanged" /> event.
		/// </summary>
		/// <seealso cref="EventArgs" />
		protected virtual void OnDistance3DChanged()
		{
			Distance3DChanged?.Invoke(this, EventArgs.Empty);
		}

		/// <summary>
		///     Raises the <see cref="Locked" /> event.
		/// </summary>
		/// <seealso cref="EventArgs" />
		protected virtual void OnLocked()
		{
			Locked?.Invoke(this, EventArgs.Empty);
		}

		/// <summary>
		///     Raises the <see cref="LoopCountChanged" /> event.
		/// </summary>
		/// <seealso cref="EventArgs" />
		protected virtual void OnLoopCountChanged()
		{
			LoopCountChanged?.Invoke(this, EventArgs.Empty);
		}

		/// <summary>
		///     Raises the <see cref="LoopPointAdded" /> event.
		/// </summary>
		/// <seealso cref="EventArgs" />
		protected virtual void OnLoopPointAdded()
		{
			LoopPointAdded?.Invoke(this, EventArgs.Empty);
		}

		/// <summary>
		///     Raises the <see cref="ModeChanged" /> event.
		/// </summary>
		/// <seealso cref="EventArgs" />
		protected virtual void OnModeChanged()
		{
			ModeChanged?.Invoke(this, EventArgs.Empty);
		}

		/// <summary>
		///     Raises the <see cref="MusicSpeedChanged" /> event.
		/// </summary>
		/// <seealso cref="EventArgs" />
		protected virtual void OnMusicSpeedChanged()
		{
			MusicSpeedChanged?.Invoke(this, EventArgs.Empty);
		}

		/// <summary>
		///     Raises the <see cref="MusicVolumeChanged" /> event.
		/// </summary>
		/// <param name="e">The <see cref="SoundMusicVolumeChangedEventArgs" /> instance containing the event data.</param>
		protected virtual void OnMusicVolumeChanged(SoundMusicVolumeChangedEventArgs e)
		{
			MusicVolumeChanged?.Invoke(this, e);
		}

		/// <summary>
		///     Raises the <see cref="SoundGroupChanged" /> event.
		/// </summary>
		/// <seealso cref="EventArgs" />
		protected virtual void OnSoundGroupChanged()
		{
			SoundGroupChanged?.Invoke(this, EventArgs.Empty);
		}

		/// <summary>
		///     Raises the <see cref="SyncPointAdded" /> event.
		/// </summary>
		/// <param name="e">The <see cref="SyncPointEventArgs" /> instance containing the event data.</param>
		protected virtual void OnSyncPointAdded(SyncPointEventArgs e)
		{
			SyncPointAdded?.Invoke(this, e);
		}

		/// <summary>
		///     Raises the <see cref="SyncPointDeleted" /> event.
		/// </summary>
		/// <param name="e">The <see cref="SyncPointEventArgs" /> instance containing the event data.</param>
		protected virtual void OnSyncPointDeleted(SyncPointEventArgs e)
		{
			SyncPointDeleted?.Invoke(this, e);
		}

		/// <summary>
		///     Raises the <see cref="Unlocked" /> event.
		/// </summary>
		/// <seealso cref="EventArgs" />
		protected virtual void OnUnlocked()
		{
			Unlocked?.Invoke(this, EventArgs.Empty);
		}

		#endregion
	}
}