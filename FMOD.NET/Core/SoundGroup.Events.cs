#region License

// SoundGroup.Events.cs is distributed under the Microsoft Public License (MS-PL)
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

#endregion

namespace FMOD.Core
{
	public partial class SoundGroup
	{
		#region Events

		/// <summary>
		/// Occurs when <see cref="MaxAudibleBehavior"/> is changed.
		/// </summary>
		/// <see cref="MaxAudibleBehavior"/>
		public event EventHandler MaxAudibleBehaviorChanged;

		/// <summary>
		/// Occurs when the <see cref="MaxAudible"/> is changed.
		/// </summary>
		/// <seealso cref="MaxAudible"/>
		public event EventHandler MaxAudibleChanged;

		/// <summary>
		/// Occurs when the <see cref="MuteFadeSpeed"/> is changed.
		/// </summary>
		/// <seealso cref="MuteFadeSpeed"/>
		public event EventHandler MuteFadeSpeedChanged;

		/// <summary>
		/// Occurs when playback is stopped by the user.
		/// </summary>
		/// <seealso cref="Stop"/>
		public event EventHandler Stopped;

		/// <summary>
		/// Occurs when the <see cref="UserData"/> is changed.
		/// </summary>
		/// <seealso cref="UserData"/>
		public event EventHandler UserDataChanged;

		/// <summary>
		/// Occurs when the <see cref="Volume"/> is changed.
		/// </summary>
		/// <seealso cref="Volume"/>
		public event EventHandler VolumeChanged;

		#endregion

		#region Event Invokers

		/// <summary>
		///     Raises the <see cref="MaxAudibleBehaviorChanged" /> event.
		/// </summary>
		/// <seealso cref="EventArgs" />
		protected virtual void OnMaxAudibleBehaviorChanged()
		{
			MaxAudibleBehaviorChanged?.Invoke(this, EventArgs.Empty);
		}

		/// <summary>
		///     Raises the <see cref="MaxAudibleChanged" /> event.
		/// </summary>
		/// <seealso cref="EventArgs" />
		protected virtual void OnMaxAudibleChanged()
		{
			MaxAudibleChanged?.Invoke(this, EventArgs.Empty);
		}

		/// <summary>
		///     Raises the <see cref="MuteFadeSpeedChanged" /> event.
		/// </summary>
		/// <seealso cref="EventArgs" />
		protected virtual void OnMuteFadeSpeedChanged()
		{
			MuteFadeSpeedChanged?.Invoke(this, EventArgs.Empty);
		}

		/// <summary>
		///     Raises the <see cref="Stopped" /> event.
		/// </summary>
		/// <seealso cref="EventArgs" />
		protected virtual void OnStopped()
		{
			Stopped?.Invoke(this, EventArgs.Empty);
		}

		/// <summary>
		///     Raises the <see cref="UserDataChanged" /> event.
		/// </summary>
		/// <seealso cref="EventArgs" />
		protected virtual void OnUserDataChanged()
		{
			UserDataChanged?.Invoke(this, EventArgs.Empty);
		}

		/// <summary>
		///     Raises the <see cref="VolumeChanged" /> event.
		/// </summary>
		/// <seealso cref="EventArgs" />
		protected virtual void OnVolumeChanged()
		{
			VolumeChanged?.Invoke(this, EventArgs.Empty);
		}

		#endregion
	}
}