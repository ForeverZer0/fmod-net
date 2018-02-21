using System;
using System.Runtime.InteropServices;
using System.Text;

namespace FMOD.NET.Core
{
	public class MemoryBuffer : IDisposable
	{
		private GCHandle _handle;
		private readonly byte[] _bytes;

		public IntPtr Pointer
		{
			get => _handle.AddrOfPinnedObject();
		}

		public MemoryBuffer(int bufferSize)
		{
			_bytes = new byte[bufferSize];
			_handle = GCHandle.Alloc(_bytes, GCHandleType.Pinned);
		}

		public static implicit operator byte[](MemoryBuffer memBuffer)
		{
			return memBuffer._bytes;
		}

		public static implicit operator IntPtr(MemoryBuffer memBuffer)
		{
			return memBuffer._handle.AddrOfPinnedObject();
		}

		public override string ToString()
		{
			return ToString(Encoding.UTF8);
		}

		public string ToString(Encoding encoding)
		{
			return encoding.GetString(_bytes).Trim('\0');
		}

		public void Dispose()
		{
			_handle.Free();
		}
	}
}
