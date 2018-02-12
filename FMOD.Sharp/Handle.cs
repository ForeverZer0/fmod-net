using System;
using System.Reflection;
using System.Resources;
using FMOD.Sharp.Enums;

namespace FMOD.Sharp
{
	public abstract class Handle : IEquatable<Handle>, IDisposable
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

		public static implicit operator IntPtr(Handle handle)
		{
			return handle._pointer;
		}

		public static Result NativeInvoke(Result result, bool throwError = true)
		{
			if (throwError && result == Result.OK)
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