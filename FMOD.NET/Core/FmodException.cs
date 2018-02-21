#region License

// FmodException.cs is distributed under the Microsoft Public License (MS-PL)
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
using FMOD.NET.Enumerations;
using FMOD.NET.Resources;

#endregion

namespace FMOD.NET.Core
{
	public class FmodException : Exception
	{
		public FmodException(Result result) : base(GetErrorString(result))
		{
			Result = result;
		}

		public Result Result { get; }

		public static string GetErrorString(Result result)
		{
			switch (result)
			{
				case Result.OK: return Strings.Result_OK;
				case Result.BadCommand: return Strings.Result_BadCommand;
				case Result.ChannelAlloc: return Strings.Result_ChannelAlloc;
				case Result.ChannelStolen: return Strings.Result_ChannelStolen;
				case Result.Dma: return Strings.Result_Dma;
				case Result.DspConnection: return Strings.Result_DspConnection;
				case Result.DspDontProcess: return Strings.Result_DspDontProcess;
				case Result.DspFormat: return Strings.Result_DspFormat;
				case Result.DspInUse: return Strings.Result_DspInUse;
				case Result.DspNotFound: return Strings.Result_DspNotFound;
				case Result.DspReserved: return Strings.Result_DspReserved;
				case Result.DspSilence: return Strings.Result_DspSilence;
				case Result.DspType: return Strings.Result_DspType;
				case Result.FileBad: return Strings.Result_FileBad;
				case Result.FileCouldNotSeek: return Strings.Result_FileCouldNotSeek;
				case Result.FileDiskEjected: return Strings.Result_FileDiskEjected;
				case Result.FileEof: return Strings.Result_FileEof;
				case Result.FileEndOfData: return Strings.Result_FileEndOfData;
				case Result.FileNotfound: return Strings.Result_FileNotfound;
				case Result.Format: return Strings.Result_Format;
				case Result.HeaderMismatch: return Strings.Result_HeaderMismatch;
				case Result.Http: return Strings.Result_Http;
				case Result.HttpAccess: return Strings.Result_HttpAccess;
				case Result.HttpProxyAuth: return Strings.Result_HttpProxyAuth;
				case Result.HttpServerError: return Strings.Result_HttpServerError;
				case Result.HttpTimeout: return Strings.Result_HttpTimeout;
				case Result.Initialization: return Strings.Result_Initialization;
				case Result.Initialized: return Strings.Result_Initialized;
				case Result.Internal: return Strings.Result_Internal;
				case Result.InvalidFloat: return Strings.Result_InvalidFloat;
				case Result.InvalidHandle: return Strings.Result_InvalidHandle;
				case Result.InvalidParam: return Strings.Result_InvalidParam;
				case Result.InvalidPosition: return Strings.Result_InvalidPosition;
				case Result.InvalidSpeaker: return Strings.Result_InvalidSpeaker;
				case Result.InvalidSyncpoint: return Strings.Result_InvalidSyncpoint;
				case Result.InvalidThread: return Strings.Result_InvalidThread;
				case Result.InvalidVector: return Strings.Result_InvalidVector;
				case Result.MaxAudible: return Strings.Result_MaxAudible;
				case Result.Memory: return Strings.Result_Memory;
				case Result.MemoryCantPoint: return Strings.Result_MemoryCantPoint;
				case Result.Needs3D: return Strings.Result_Needs3D;
				case Result.NeedsHardware: return Strings.Result_NeedsHardware;
				case Result.NetConnect: return Strings.Result_NetConnect;
				case Result.NetSocketError: return Strings.Result_NetSocketError;
				case Result.NetUrl: return Strings.Result_NetUrl;
				case Result.NetWouldBlock: return Strings.Result_NetWouldBlock;
				case Result.NotReady: return Strings.Result_NotReady;
				case Result.OutputAllocated: return Strings.Result_OutputAllocated;
				case Result.OutputCreateBuffer: return Strings.Result_OutputCreateBuffer;
				case Result.OutputDriverCall: return Strings.Result_OutputDriverCall;
				case Result.OutputFormat: return Strings.Result_OutputFormat;
				case Result.OutputInit: return Strings.Result_OutputInit;
				case Result.OutputNoDrivers: return Strings.Result_OutputNoDrivers;
				case Result.Plugin: return Strings.Result_Plugin;
				case Result.PluginMissing: return Strings.Result_PluginMissing;
				case Result.PluginResource: return Strings.Result_PluginResource;
				case Result.PluginVersion: return Strings.Result_PluginVersion;
				case Result.Record: return Strings.Result_Record;
				case Result.ReverbChannelgroup: return Strings.Result_ReverbChannelgroup;
				case Result.ReverbInstance: return Strings.Result_ReverbInstance;
				case Result.Subsounds: return Strings.Result_Subsounds;
				case Result.SubsoundAllocated: return Strings.Result_SubsoundAllocated;
				case Result.SubsoundCantMove: return Strings.Result_SubsoundCantMove;
				case Result.TagNotFound: return Strings.Result_TagNotFound;
				case Result.TooManyChannels: return Strings.Result_TooManyChannels;
				case Result.Truncated: return Strings.Result_Truncated;
				case Result.Unimplemented: return Strings.Result_Unimplemented;
				case Result.Uninitialized: return Strings.Result_Uninitialized;
				case Result.Unsupported: return Strings.Result_Unsupported;
				case Result.Version: return Strings.Result_Version;
				case Result.EventAlreadyLoaded: return Strings.Result_EventAlreadyLoaded;
				case Result.EventLiveUpdateBusy: return Strings.Result_EventLiveUpdateBusy;
				case Result.EventLiveUpdateMismatch: return Strings.Result_EventLiveUpdateMismatch;
				case Result.EventLiveUpdateTimeout: return Strings.Result_EventLiveUpdateTimeout;
				case Result.EventNotFound: return Strings.Result_EventNotFound;
				case Result.StudioUninitialized: return Strings.Result_StudioUninitialized;
				case Result.StudioNotLoaded: return Strings.Result_StudioNotLoaded;
				case Result.InvalidString: return Strings.Result_InvalidString;
				case Result.AlreadyLocked: return Strings.Result_AlreadyLocked;
				case Result.NotLocked: return Strings.Result_NotLocked;
				case Result.RecordDisconnected: return Strings.Result_RecordDisconnected;
				case Result.TooManySamples: return Strings.Result_TooManySamples;
				default: return Strings.Result_Unknown;
			}
		}
	}



}