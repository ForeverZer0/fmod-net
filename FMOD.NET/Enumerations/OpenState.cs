#region License

// OpenState.cs is distributed under the Microsoft Public License (MS-PL)
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
// Created 12:50 AM 02/04/2018

#endregion

#region Using Directives

#endregion

namespace FMOD.NET.Enumerations
{
	/// <summary>
	///     These values describe what state a sound is in after <see cref="Mode.NonBlocking" /> has been used to open it.
	/// </summary>
	/// <remarks>
	///     <para>
	///         With streams, if you are using <see cref="Mode.NonBlocking" />, note that if the user calls
	///         <see cref="Sound.GetSubSound" />, a stream will go into <see cref="Seeking" /> state and sound related commands
	///         will return <see cref="Result.NotReady" />.
	///     </para>
	///     <para>
	///         With streams, if you are using <see cref="Mode.NonBlocking" />, note that if the user calls
	///         <see cref="Channel.GetPosition" />, a stream will go into<see cref="SetPosition" /> state and sound related
	///         commands will return <see cref="Result.NotReady" />.
	///     </para>
	/// </remarks>
	/// <seealso cref="Mode" />
	/// <seealso cref="O:FMOD.Core.Sound.GetOpenState" />
	public enum OpenState
	{
		/// <summary>
		///     Opened and ready to play.
		/// </summary>
		Ready,

		/// <summary>
		///     Initial load in progress.
		/// </summary>
		Loading,

		/// <summary>
		///     <para>Failed to open - file not found, out of memory etc.</para>
		///     <para>See return value of <see cref="O:FMOD.Core.Sound.GetOpenState" /> for what happened.</para>
		/// </summary>
		Error,

		/// <summary>
		///     Connecting to remote host (internet sounds only).
		/// </summary>
		Connecting,

		/// <summary>
		///     Buffering data.
		/// </summary>
		Buffering,

		/// <summary>
		///     Seeking to subsound and re-flushing stream buffer.
		/// </summary>
		Seeking,

		/// <summary>
		///     Ready and playing, but not possible to release at this time without stalling the main thread.
		/// </summary>
		Playing,

		/// <summary>
		///     Seeking within a stream to a different position.
		/// </summary>
		SetPosition,

		/// <summary>
		///     Maximum number of open state types.
		/// </summary>
		Max
	}
}