#region License

// AsyncReadInfo.cs is distributed under the Microsoft Public License (MS-PL)
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
// Created 6:24 PM 02/04/2018

#endregion

#region Using Directives

using System;
using System.Runtime.InteropServices;
using FMOD.Core;
using FMOD.Enumerations;

#endregion

namespace FMOD.Structures
{
	/// <summary>
	///     Structure that is passed into <see cref="FileAsyncReadCallback" />. Use the information in this structure to
	///     perform
	/// </summary>
	/// <remarks>
	///     <para>
	///         <b>
	///             <u>Instructions:</u>
	///         </b>
	///         write to <see cref="Buffer" />, and <see cref="BytesRead" /> <b>BEFORE</b> calling <see cref="Done" />.
	///         <lineBreak />
	///         As soon as <see cref="Done" /> is called, <b>FMOD</b> will asynchronously continue internally using the data
	///         provided in this structure.
	///     </para>
	///     <para>
	///         Set result in the <see cref="Done" /> function to the result expected from a normal file read callback.
	///         <lineBreak />
	///         If the read was successful, set it to <see cref="Result.OK" />.<lineBreak />
	///         If it read some data but hit the end of the file, set it to <see cref="Result.FileEof" />.<lineBreak />
	///         If a bad error occurred, return <see cref="Result.FileBad" />.<lineBreak />
	///         If a disk was ejected, return <see cref="Result.FileDiskEjected" />.
	///     </para>
	/// </remarks>
	/// <seealso cref="FileAsyncReadCallback" />
	/// <seealso cref="FileAsyncCancelCallback" />
	[StructLayout(LayoutKind.Sequential)]
	public struct AsyncReadInfo
	{
		/// <summary>
		///     The file handle that was filled out in the open callback.
		/// </summary>
		public readonly IntPtr Handle;

		/// <summary>
		///     Seek position, make sure you read from this file offset.
		/// </summary>
		public readonly uint Offset;

		/// <summary>
		///     Number of <see cref="byte">bytes</see> requested for read.
		/// </summary>
		public readonly uint SizeBytes;

		/// <summary>
		///     The priority.
		///     <para><c>0</c> = low importance. <c>100</c> = extremely important (ie "must read now or stuttering may occur")</para>
		/// </summary>
		public int Priority;

		/// <summary>
		///     UserData pointer specific to this request.
		///     <para>
		///         Initially <see cref="IntPtr.Zero" />, can be ignored or set by the user. Not related to the file's main
		///         UserData member.
		///     </para>
		/// </summary>
		public IntPtr UserData;

		/// <summary>
		///     Buffer to read file data into.
		/// </summary>
		public IntPtr Buffer;

		/// <summary>
		///     Set this value before setting result code to tell <b>FMOD</b> how many bytes were read.
		/// </summary>
		public uint BytesRead;

		/// <summary>
		///     <b>FMOD</b> file system wake up function. Call this when user file read is finished. Pass result of file read as a
		///     parameter.
		/// </summary>
		public readonly AsyncReadInfoDoneCallback Done;
	}
}