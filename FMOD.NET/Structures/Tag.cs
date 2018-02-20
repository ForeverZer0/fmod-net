#region License

// Tag.cs is distributed under the Microsoft Public License (MS-PL)
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
// Created 1:07 AM 02/04/2018

#endregion

#region Using Directives

using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text;
using FMOD.Core;
using FMOD.Enumerations;
using FMOD.Properties;

#endregion

namespace FMOD.Structures
{
	[StructLayout(LayoutKind.Sequential)]
	public struct Tag
	{
		/// <summary>
		///     <para> Array of genres to be used with ID3V1 tags.</para>
		///     <para>
		///         ID3V1 tags are an index into this array to retrieve the string name, or use <see cref="Tag.GetGenreString" />
		///         .
		///     </para>
		/// </summary>
		public static readonly string[] GENRES = Resources.Genres.Split(',');

		/// <summary>
		///     The type of this tag.
		/// </summary>
		public readonly TagType Type;

		/// <summary>
		///     The type of data that this tag contains.
		/// </summary>
		public readonly TagDataType DataType;

		/// <summary>
		///     The pointer to the name of the tag.
		/// </summary>
		private readonly IntPtr _nameInternal;

		/// <summary>
		///     Pointer to the tag data - its format is determined by the <see cref="DataType" /> field.
		/// </summary>
		public readonly IntPtr Data;

		/// <summary>
		///     Length of the data in bytes contained in this tag.
		/// </summary>
		public readonly uint DataLength;

		/// <summary>
		///     <c>true</c> if this tag has been updated since last being accessed with <see cref="Sound.GetTag" />; <c>false</c>
		///     otherwise.
		/// </summary>
		public readonly bool Updated;

		/// <summary>
		///     The name of this tag i.e. <c>"TITLE"</c>, <c>"ARTIST"</c> etc.
		/// </summary>
		public string Name => Marshal.PtrToStringAnsi(_nameInternal);

		/// <summary>
		///     Reads the data contained at the <see cref="Data" /> pointer according to the <see cref="DataType" /> and returns
		///     it.
		/// </summary>
		/// <returns>The tag data converted to its specified type.</returns>
		public object GetValue()
		{
			var rawData = new byte[DataLength];
			Marshal.Copy(Data, rawData, 0, rawData.Length);
			switch (DataType)
			{
				case TagDataType.Binary:
					return Encoding.Default.GetString(rawData).Trim('\0');
				case TagDataType.Int:
					return BitConverter.ToInt32(rawData, 0);
				case TagDataType.Float:
					return BitConverter.ToSingle(rawData, 0);
				case TagDataType.String:
					return Encoding.Default.GetString(rawData).Trim('\0');
				case TagDataType.StringUtf16:
					return Encoding.Unicode.GetString(rawData).Trim('\0');
				case TagDataType.StringUtf16Be:
					return Encoding.BigEndianUnicode.GetString(rawData).Trim('\0');
				case TagDataType.StringUtf8:
					return Encoding.UTF8.GetString(rawData).Trim('\0');
				case TagDataType.Cdtoc:
					// TODO: Implement
					return null;
				case TagDataType.Max:
					// TODO: Implement
					return null;
				default:
					return null;
			}
		}

		/// <summary>
		///     Reads the data contained at the <see cref="Data" /> pointer according to the <see cref="DataType" /> and returns it
		///     cast to the specified type.
		/// </summary>
		/// <typeparam name="T">The type to cast the data to before returning.</typeparam>
		/// <returns>The tag data converted to its specified type.</returns>
		public T GetValue<T>()
		{
			return (T) GetValue();
		}

		/// <summary>
		///     Returns a <see cref="System.String" /> that represents this instance.
		/// </summary>
		/// <returns>
		///     A <see cref="System.String" /> that represents this instance.
		/// </returns>
		public override string ToString()
		{
			var value = GetValue();
			if (value is String)
				return $"[{Type}] {Name}: \"{value}\"";
			return $"[{Type}] {Name}: {value}";
		}

		/// <summary>
		///     Converts a string to culture-specific title case. (See more on
		///     <see href="https://msdn.microsoft.com/en-us/library/system.globalization.textinfo.totitlecase(v=vs.110).aspx">MSDN</see>
		///     ).
		/// </summary>
		/// <param name="str">The string to convert.</param>
		/// <returns>The input string formatted to title case.</returns>
		/// <seealso href="https://msdn.microsoft.com/en-us/library/system.globalization.textinfo.totitlecase(v=vs.110).aspx">TextInfo.ToTitleCase</seealso>
		public static string ToTitleCase(string str)
		{
			var info = CultureInfo.CurrentCulture.TextInfo;
			return info.ToTitleCase(str.ToLower());
		}

		/// <summary>
		///     Gets the genre string from an ID3V1 genre index.
		/// </summary>
		/// <param name="id3V1Genre">The ID3V1 genre.</param>
		/// <returns>The string name of the specified genre.</returns>
		public static string GetGenreString(int id3V1Genre)
		{
			if (id3V1Genre < 0 || id3V1Genre >= GENRES.Length)
				return "Unknown";
			return GENRES[id3V1Genre];
		}
	}
}