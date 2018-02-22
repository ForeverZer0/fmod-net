#region License

// HandleBase.cs is distributed under the Microsoft Public License (MS-PL)
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
using System.Runtime.ConstrainedExecution;
using System.Runtime.ExceptionServices;
using FMOD.NET.Enumerations;
using Microsoft.Win32.SafeHandles;

#endregion

namespace FMOD.NET.Core
{
	/// <inheritdoc cref="SafeHandleZeroOrMinusOneIsInvalid" />
	/// <summary>
	///     <para>The base class for all the core native <b>FMOD</b> classes.</para>
	///     <para>
	///         Wraps an operating system handle into a managed type, but implements a critical finalizer that ensures any
	///         unmanaged handle, is released (with exception of possible catastrophic failure) by the application to prevent
	///         memory leaks.
	///     </para>
	///     <para>This class must be inherited.</para>
	/// </summary>
	/// <seealso cref="T:Microsoft.Win32.SafeHandles.SafeHandleZeroOrMinusOneIsInvalid" />
	public abstract partial class HandleBase : SafeHandleZeroOrMinusOneIsInvalid, IHandleBase
	{
		private readonly int _handleHashCode;
		private bool _isInvalid;

		#region Events

		/// <summary>
		///     Occurs when the handle is disposed, and the underlying <b>FMOD</b> handle has been released.
		/// </summary>
		/// <seealso cref="Dispose" />
		public event EventHandler Disposed;

		/// <summary>
		///     Occurs when the user-data has changed.
		/// </summary>
		/// <seealso cref="UserData" />
		public event EventHandler UserDataChanged;

		#endregion

		#region Constructors

		/// <inheritdoc />
		/// <summary>
		///     Initializes a new instance of the <see cref="HandleBase" /> class.
		/// </summary>
		/// <param name="nativeHandle">The native handle.</param>
		protected HandleBase(IntPtr nativeHandle) : base(true)
		{
			_handleHashCode = nativeHandle.GetHashCode();
			SetHandle(nativeHandle);
		}

		#endregion

		#region Properties

		/// <inheritdoc cref="IHandleBase.IsInvalid" />
		public override bool IsInvalid => IsClosed || _isInvalid || handle == IntPtr.Zero;


		/// <inheritdoc />
		public virtual IntPtr UserData
		{
			get => GetUserData();
			set => SetUserData(value);
		}

		#endregion

		#region Event Invokers

		/// <summary>
		///     Raises the <see cref="UserDataChanged" /> event.
		/// </summary>
		/// <seealso cref="EventArgs" />
		protected virtual void OnUserDataChanged()
		{
			UserDataChanged?.Invoke(this, EventArgs.Empty);
		}

		#endregion

		#region Methods

		/// <summary>
		///     Encapsulates invocations to functions of the native FMOD library, receiving the result, and throwing exceptions
		///     when necessary.
		/// </summary>
		/// <param name="result">The <see cref="Result" /> returned by the function call.</param>
		/// <param name="throwException">
		///     If set to <c>true</c>, an <see cref="FmodException" /> will be thrown if the return value
		///     does not equal <seealso cref="Result.OK" />.
		/// </param>
		/// <returns>The <see cref="Result" /> returned by the function call.</returns>
		/// <exception cref="FmodException">
		///     Thrown when the return value of the function does not equal <see cref="Result.OK" />
		///     and the <see cref="throwException" /> parameter is <c>false</c>.
		/// </exception>
		/// <seealso cref="FmodException" />
		/// <seealso cref="Result" />
		public static Result NativeInvoke(Result result, bool throwException = true)
		{
			if (result != Result.OK && throwException)
				throw new FmodException(result);
			return result;
		}


		/// <summary>
		///     Performs an implicit conversion from <see cref="HandleBase" /> to <see cref="IntPtr" />.
		/// </summary>
		/// <param name="handleBase">An instance of a HandleBase or derived class.</param>
		/// <returns>
		///     The result of the conversion.
		/// </returns>
		public static implicit operator IntPtr(HandleBase handleBase)
		{
			return handleBase.DangerousGetHandle();
		}

		/// <inheritdoc />
		/// <summary>
		///     Releases the unmanaged resources used by the <see cref="System.Runtime.InteropServices.SafeHandle" /> class
		///     specifying whether to perform a normal dispose operation.
		/// </summary>
		/// <param name="disposing"><c>true</c> for a normal dispose operation; <c>false</c> to finalize the handle.</param>
		protected override void Dispose(bool disposing)
		{
			//if (!IsInvalid)
			ReleaseHandle();
			base.Dispose(disposing);
			Disposed?.Invoke(this, EventArgs.Empty);
		}

		/// <inheritdoc />
		/// <summary>
		///     Executes the code required to free the underlying <b>FMOD</b> handle.
		/// </summary>
		/// <returns>
		///     <c>true</c> if the handle is released successfully; otherwise, in the event of a catastrophic failure, <c>false</c>
		///     . In this case, it generates a "releaseHandleFailed" MDA Managed Debugging Assistant.
		/// </returns>
		[HandleProcessCorruptedStateExceptions]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		protected sealed override bool ReleaseHandle()
		{
			_isInvalid = true;
			try
			{
				switch (this)
				{
					case Sound _:
						return FMOD_Sound_Release(handle) == Result.OK;
					case Dsp _:
						return FMOD_DSP_Release(handle) == Result.OK;
					case Reverb _:
						return FMOD_Reverb3D_Release(handle) == Result.OK;
					case FmodSystem _:
						return FMOD_System_Release(handle) == Result.OK;
					case ChannelGroup _:
						return FMOD_ChannelGroup_Release(handle) == Result.OK;
					case SoundGroup _:
						return FMOD_SoundGroup_Release(handle) == Result.OK;
					case Geometry _:
						return FMOD_Geometry_Release(handle) == Result.OK;
					default:
						return true;
				}
			}
			catch (AccessViolationException)
			{
				return false;
			}
		}

		private IntPtr GetUserData()
		{
			IntPtr userData;
			switch (this)
			{
				case Channel _:
				case ChannelGroup _:
				case ChannelControl _:
					FMOD_ChannelGroup_GetUserData(handle, out userData);
					break;
				case Dsp _:
					FMOD_DSP_GetUserData(handle, out userData);
					break;
				case DspConnection _:
					FMOD_DSPConnection_GetUserData(handle, out userData);
					break;
				case FmodSystem _:
					FMOD_System_GetUserData(handle, out userData);
					break;
				case Geometry _:
					FMOD_Geometry_GetUserData(handle, out userData);
					break;
				case Reverb _:
					FMOD_Reverb3D_GetUserData(handle, out userData);
					break;
				case Sound _:
					FMOD_Sound_GetUserData(handle, out userData);
					break;
				case SoundGroup _:
					FMOD_SoundGroup_GetUserData(handle, out userData);
					break;
				default:
					userData = IntPtr.Zero;
					break;
			}
			return userData;
		}

		private void SetUserData(IntPtr userData)
		{
			switch (this)
			{
				case Channel _:
				case ChannelGroup _:
				case ChannelControl _:
					FMOD_ChannelGroup_SetUserData(handle, userData);
					break;
				case Dsp _:
					FMOD_DSP_SetUserData(handle, userData);
					break;
				case DspConnection _:
					FMOD_DSPConnection_SetUserData(handle, userData);
					break;
				case FmodSystem _:
					FMOD_System_SetUserData(handle, userData);
					break;
				case Geometry _:
					FMOD_Geometry_SetUserData(handle, userData);
					break;
				case Reverb _:
					FMOD_Reverb3D_SetUserData(handle, userData);
					break;
				case Sound _:
					FMOD_Sound_SetUserData(handle, userData);
					break;
				case SoundGroup _:
					FMOD_SoundGroup_SetUserData(handle, userData);
					break;
			}
			OnUserDataChanged();
		}

		#endregion

		#region Equality Functions

		/// <inheritdoc />
		/// <summary>
		///     Determines whether the specified <see cref="HandleBase" />, is equal to this instance.
		/// </summary>
		/// <param name="other">The <see cref="HandleBase" /> to compare with this instance.</param>
		/// <returns>
		///     <c>true</c> if the specified <see cref="HandleBase" /> is equal to this instance; otherwise,
		///     <c>false</c>.
		/// </returns>
		public bool Equals(HandleBase other)
		{
#if X64
			return handle.ToInt64() == other?.handle.ToInt64();
			#elif X86
			return handle.ToInt32() == other?.handle.ToInt32();
#endif
		}

		/// <summary>
		///     Determines whether the specified <see cref="System.Object" />, is equal to this instance.
		/// </summary>
		/// <param name="obj">The <see cref="System.Object" /> to compare with this instance.</param>
		/// <returns>
		///     <c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.
		/// </returns>
		public override bool Equals(object obj)
		{
			return Equals(obj as HandleBase);
		}

		/// <summary>
		///     Returns a hash code for this instance.
		/// </summary>
		/// <returns>
		///     A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.
		/// </returns>
		public override int GetHashCode()
		{
			return _handleHashCode;
		}

		/// <summary>
		///     Implements the operator ==.
		/// </summary>
		/// <param name="a">The first instance to compare.</param>
		/// <param name="b">The second instance to compare.</param>
		/// <returns>
		///     The result of the operation.
		/// </returns>
		public static bool operator ==(HandleBase a, HandleBase b)
		{
			if (ReferenceEquals(a, b))
				return true;
			return a?.handle == b?.handle;
		}

		/// <summary>
		///     Implements the operator !=.
		/// </summary>
		/// <param name="a">The first instance to compare.</param>
		/// <param name="b">The second instance to compare.</param>
		/// <returns>
		///     The result of the operation.
		/// </returns>
		public static bool operator !=(HandleBase a, HandleBase b)
		{
			return !(a == b);
		}

		#endregion
	}
}