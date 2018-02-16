using System;
using System.Runtime.ConstrainedExecution;
using FMOD.Enumerations;
using Microsoft.Win32.SafeHandles;

namespace FMOD.Core
{
	public abstract partial class HandleBase : SafeHandleZeroOrMinusOneIsInvalid
	{
		private bool _isInvalid;
		private readonly int _handleHashCode;


		public event EventHandler Disposed;

		protected HandleBase(IntPtr nativeHandle) : base(true)
		{
			_handleHashCode = nativeHandle.GetHashCode();
			SetHandle(nativeHandle);
		}

		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		protected override bool ReleaseHandle()
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
				// Already released
				return true;
			}
		}

		protected override void Dispose(bool disposing)
		{
			if (!IsInvalid)
				ReleaseHandle();
			base.Dispose(disposing);
			Disposed?.Invoke(this, EventArgs.Empty);
		}




		















		


		public override bool IsInvalid
		{
			get => IsClosed || _isInvalid || handle == IntPtr.Zero;
		}


		/// <summary>
		/// Performs an implicit conversion from <see cref="HandleBase"/> to <see cref="IntPtr"/>.
		/// </summary>
		/// <param name="handleBase">An instance of a HandleBase or derived class.</param>
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
				throw new FmodException(result, Util.GetResultString(result));
			return result;
		}

		#region Equality Functions

		public bool Equals(HandleBase other)
		{
			#if X64
			return handle.ToInt64() == other?.handle.ToInt64();
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
			return _handleHashCode;
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