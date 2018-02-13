using System;
using FMOD.Sharp.Enums;
using FMOD.Sharp.Interfaces;

namespace FMOD.Sharp
{
	public abstract class Handle : IHandle
	{
		public event EventHandler Disposed;

		private IntPtr _pointer;
		private bool _isDisposed;


		public virtual bool IsDisposed
		{
			get { return _isDisposed || _pointer == IntPtr.Zero; }
		}

		protected Handle(IntPtr handle)
		{
			_pointer = handle;
		}

		protected void SetDisposed()
		{
			_isDisposed = true;
			_pointer = IntPtr.Zero;
		}

		/// <inheritdoc />
		/// <summary>
		/// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
		/// </summary>
		public virtual void Dispose()
		{
			_pointer = IntPtr.Zero;
			_isDisposed = true;
			Disposed?.Invoke(this, EventArgs.Empty);
		}

		/// <summary>
		/// Performs an implicit conversion from <see cref="Handle"/> to <see cref="IntPtr"/>.
		/// </summary>
		/// <param name="handle">The handle.</param>
		/// <returns>
		/// The result of the conversion.
		/// </returns>
		public static implicit operator IntPtr(Handle handle)
		{
			return handle._pointer;
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
		public Result NativeInvoke(Result result, bool throwException = true)
		{
			if (result != Result.OK && throwException)
				throw new FmodException(result, Core.GetResultString(result));
			return result;
		}



		#region Equality Functions

		public bool Equals(Handle other)
		{
			#if X64
			return _pointer.ToInt64() == other?._pointer.ToInt64();
			#elif X86
			return _pointer.ToInt32() == other?._pointer.ToInt32();
			#endif
		}

		public override bool Equals(object obj)
		{
			return Equals(obj as Handle);
		}

		public override int GetHashCode()
		{
			// ReSharper disable once NonReadonlyMemberInGetHashCode
			return _pointer.GetHashCode();
		}

		public static bool operator ==(Handle a, Handle b)
		{
			if (ReferenceEquals(a, b))
				return true;
			return a?._pointer == b?._pointer;
		}

		public static bool operator !=(Handle a, Handle b)
		{
			return !(a == b);
		}

		#endregion
	}
}