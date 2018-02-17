#region License

// DebugFlags.cs is distributed under the Microsoft Public License (MS-PL)
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
// Created 12:52 AM 02/04/2018

#endregion

#region Using Directives

using System;
using FMOD.Core;

#endregion

namespace FMOD.Enumerations
{
	/// <summary>
	///     Specify the requested information to be output when using the logging version of <b>FMOD</b>.
	/// </summary>
	/// <seealso cref="Debug.Initialize" />
	[Flags]
	public enum DebugFlags : uint
	{
		/// <summary>
		///     Disable all messages.
		/// </summary>
		None = 0x00000000,

		/// <summary>
		///     Enable only error messages.
		/// </summary>
		Error = 0x00000001,

		/// <summary>
		///     Enable warning and error messages.
		/// </summary>
		Warning = 0x00000002,

		/// <summary>
		///     Enable informational, warning and error messages (default).
		/// </summary>
		Log = 0x00000004,

		/// <summary>
		///     Verbose logging for memory operations, only use this if you are debugging a memory related issue.
		/// </summary>
		TypeMemory = 0x00000100,

		/// <summary>
		///     Verbose logging for file access, only use this if you are debugging a file related issue.
		/// </summary>
		TypeFile = 0x00000200,

		/// <summary>
		///     Verbose logging for codec initialization, only use this if you are debugging a codec related issue.
		/// </summary>
		TypeCodec = 0x00000400,

		/// <summary>
		///     Verbose logging for internal errors, use this for tracking the origin of error codes.
		/// </summary>
		TypeTrace = 0x00000800,

		/// <summary>
		///     Display the time stamp of the log message in milliseconds.
		/// </summary>
		DisplayTimestamps = 0x00010000,

		/// <summary>
		///     Display the source code file and line number for where the message originated.
		/// </summary>
		DisplayLineNumbers = 0x00020000,

		/// <summary>
		///     Display the thread ID of the calling function that generated the message.
		/// </summary>
		DisplayThread = 0x00040000
	}
}