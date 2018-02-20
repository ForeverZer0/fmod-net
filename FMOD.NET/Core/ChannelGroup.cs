#region License

// ChannelGroup.cs is distributed under the Microsoft Public License (MS-PL)
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
using System.Text;
using FMOD.Arguments;

#endregion

namespace FMOD.Core
{
	/// <inheritdoc />
	/// <summary>
	///     Specialized <see cref="T:FMOD.Core.ChannelControl" /> for grouping multiple instances together to operate as a
	///     single unit.
	/// </summary>
	/// <seealso cref="T:FMOD.Core.ChannelControl" />
	public partial class ChannelGroup : ChannelControl
	{
		#region Constructors

		/// <inheritdoc />
		/// <summary>
		///     Initializes a new instance of the <see cref="T:FMOD.Core.ChannelGroup" /> class.
		/// </summary>
		/// <param name="handle">The handle.</param>
		protected ChannelGroup(IntPtr handle) : base(handle)
		{
		}

		#endregion

		#region Properties

		/// <summary>
		///     Retrieves the channel group parent.
		/// </summary>
		/// <value>
		///     The parent group.
		/// </value>
		/// <seealso cref="AddGroup" />
		/// <seealso cref="GetGroup" />
		/// <seealso cref="GroupCount" />
		public ChannelGroup ParentGroup
		{
			get
			{
				NativeInvoke(FMOD_ChannelGroup_GetParentGroup(this, out var group));
				return Factory.Create<ChannelGroup>(group);
			}
		}

		/// <summary>
		///     Gets the name of the <see cref="ChannelGroup" /> set when the group was created.
		/// </summary>
		/// <value>
		///     The name.
		/// </value>
		/// <seealso cref="FmodSystem.MasterChannelGroup" />
		/// <seealso cref="FmodSystem.CreateChannelGroup" />
		public string Name
		{
			get
			{
				using (var buffer = new MemoryBuffer(512))
				{
					NativeInvoke(FMOD_ChannelGroup_GetName(this, buffer.Pointer, 512));
					return buffer.ToString(Encoding.UTF8);
				}
			}
		}

		/// <summary>
		///     Gets the number of assigned channels to this <see cref="ChannelGroup" />.
		/// </summary>
		/// <value>
		///     The number of channels in this channel group.
		/// </value>
		/// <remarks>
		///     Use this function to enumerate the channels within the channel group. You can then use
		///     <seealso cref="GetChannel" /> to retrieve each individual channel.
		/// </remarks>
		/// <seealso cref="GetChannel" />
		/// <seealso cref="FmodSystem.MasterChannelGroup" />
		/// <seealso cref="FmodSystem.CreateChannelGroup" />
		public int ChannelCount
		{
			get
			{
				NativeInvoke(FMOD_ChannelGroup_GetNumChannels(this, out var count));
				return count;
			}
		}

		/// <summary>
		///     Gets the number of sub groups under this channel group.
		/// </summary>
		/// <value>
		///     The number of channel groups within this channel group.
		/// </value>
		/// <seealso cref="AddGroup" />
		/// <seealso cref="GetGroup" />
		/// <seealso cref="ParentGroup" />
		public int GroupCount
		{
			get
			{
				NativeInvoke(FMOD_ChannelGroup_GetNumGroups(this, out var count));
				return count;
			}
		}

		#endregion

		#region Methods

		/// <summary>
		///     Adds a <see cref="ChannelGroup" /> as a child of this channel group.
		/// </summary>
		/// <param name="group">Channel group to add as a child.</param>
		/// <param name="propagateDspClock">
		///     When a child group is added to a parent group, the clock values from the parent will be
		///     propogated down into the child.
		/// </param>
		/// <returns>The connection between the parent and the child group's DSP units.</returns>
		/// <seealso cref="ChannelGroupAdded" />
		/// <seealso cref="GroupCount" />
		/// <seealso cref="GetGroup" />
		/// <seealso cref="ParentGroup" />
		public DspConnection AddGroup(ChannelGroup group, bool propagateDspClock = true)
		{
			NativeInvoke(FMOD_ChannelGroup_AddGroup(this, group, propagateDspClock, out var connection));
			var dspConn = Factory.Create<DspConnection>(connection);
			OnChannelGroupAdded(new AddChannelGroupEventArgs(group, dspConn));
			return dspConn;
		}

		/// <summary>
		///     Retrieves the specified channel from the channel group.
		/// </summary>
		/// <param name="index">
		///     Index of the channel inside the channel group, from <c>0</c> to the number of channels returned by
		///     <see cref="ChannelCount" />.
		/// </param>
		/// <returns>The <see cref="Channel" /> within the group at the specified <paramref name="index" />.</returns>
		public Channel GetChannel(int index)
		{
			NativeInvoke(FMOD_ChannelGroup_GetChannel(this, index, out var channel));
			return Factory.Create<Channel>(channel);
		}

		/// <summary>
		///     Retrieves the specified sub channel group.
		/// </summary>
		/// <param name="index">Index to specify which sub channel group to receive.</param>
		/// <returns>The <see cref="ChannelGroup" /> at the specified <paramref name="index" />.</returns>
		/// <seealso cref="AddGroup" />
		/// <seealso cref="GroupCount" />
		/// <seealso cref="ParentGroup" />
		public ChannelGroup GetGroup(int index)
		{
			NativeInvoke(FMOD_ChannelGroup_GetGroup(this, index, out var group));
			return Factory.Create<ChannelGroup>(group);
		}

		#endregion
	}
}