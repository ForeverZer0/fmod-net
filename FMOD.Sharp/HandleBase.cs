using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using FMOD.Sharp.DSP;
using FMOD.Sharp.Enums;
using Microsoft.Win32.SafeHandles;

namespace FMOD.Sharp
{
	[SuppressUnmanagedCodeSecurity]
	public abstract class HandleBase : SafeHandleZeroOrMinusOneIsInvalid
	{
		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_System_Release(IntPtr system);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_ChannelGroup_Release(IntPtr channelControl);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_Sound_Release(IntPtr sound);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_SoundGroup_Release(IntPtr soundgroup);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_DSP_Release(IntPtr dsp);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_Geometry_Release(IntPtr geometry);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_Reverb3D_Release(IntPtr reverb);


		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		protected override bool ReleaseHandle()
		{
			_isInvalid = true;
			switch (this)
			{
				case Sound _:
					return FMOD_Sound_Release(handle) == Result.OK;
				case DspBase _:
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

		[SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
		protected override void Dispose(bool disposing)
		{
			if (!IsInvalid)
				ReleaseHandle();
			base.Dispose(disposing);
			Disposed?.Invoke(this, EventArgs.Empty);
		}


		protected HandleBase(IntPtr nativeHandle) : base(true)
		{
			SetHandle(nativeHandle);
		}

		














		/// <summary>
		/// Occurs when the object the handleBase points to is released and no longer valid.
		/// </summary>
		public event EventHandler Disposed;

		private bool _isInvalid;



		public override bool IsInvalid
		{
			get => _isInvalid || handle == IntPtr.Zero;
		}


		/// <summary>
		/// Performs an implicit conversion from <see cref="HandleBase"/> to <see cref="IntPtr"/>.
		/// </summary>
		/// <param name="handleBase">The handleBase.</param>
		/// <returns>
		/// The result of the conversion.
		/// </returns>
		public static implicit operator IntPtr(HandleBase handleBase)
		{
			return handleBase.DangerousGetHandle();
		}

		/// <summary>
		/// Encapsulates invocations to functions of the native FMOD library, receiving the result, and throwing exceptions when necessary.
		/// </summary>
		/// <param name="result">The <see cref="Result"/> returned by the function call.</param>
		/// <param name="throwException">If set to <c>true</c>, an <see cref="FmodException"/> will be thrown if the return value does not equal <seealso cref="Result.OK"/>.</param>
		/// <returns>The <see cref="Result"/> returned by the function call.</returns>
		/// <exception cref="FmodException">Thrown when the return value of the function does not equal <see cref="Result.OK"/> and the <see cref="throwException"/> parameter is <c>false</c>.</exception>
		/// <seealso cref="FmodException"/>
		/// <seealso cref="Result"/>
		public static Result NativeInvoke(Result result, bool throwException = true)
		{
			if (result != Result.OK && throwException)
				throw new FmodException(result, Core.GetResultString(result));
			return result;
		}



		#region Equality Functions

		public bool Equals(HandleBase other)
		{
			#if X64
			return handleBase.ToInt64() == other?.handleBase.ToInt64();
			#elif X86
			return handle.ToInt32() == other?.handle.ToInt32();
			#endif
		}

		public override bool Equals(object obj)
		{
			return Equals(obj as HandleBase);
		}

		public override int GetHashCode()
		{
			// ReSharper disable once NonReadonlyMemberInGetHashCode
			return handle.GetHashCode();
		}

		public static bool operator ==(HandleBase a, HandleBase b)
		{
			if (ReferenceEquals(a, b))
				return true;
			return a?.handle == b?.handle;
		}

		public static bool operator !=(HandleBase a, HandleBase b)
		{
			return !(a == b);
		}

		#endregion
	}
}