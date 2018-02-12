using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace FMOD.Sharp
{
	public class MemoryBuffer : IDisposable
	{
		private GCHandle _handle;
		private readonly byte[] _internalBuffer;

		public IntPtr Pointer
		{
			get => _handle.AddrOfPinnedObject();
		}

		public MemoryBuffer(int bufferSize)
		{
			_internalBuffer = new byte[bufferSize];
			_handle = GCHandle.Alloc(_internalBuffer, GCHandleType.Pinned);
		}

		public static implicit operator byte[](MemoryBuffer buffer)
		{
			return buffer._internalBuffer;
		}

		public int[] ToInt32Array()
		{
			var byteCount = _internalBuffer.Length;
			var intArray = new int[byteCount / 4];
			var bytes = _internalBuffer;
			if (BitConverter.IsLittleEndian)
				Array.Reverse(bytes);
			for (var i = 0; i < intArray.Length; i++)
				intArray[i] = BitConverter.ToInt32(bytes, i * 4);
			return intArray;
		}

		public char[] ToCharArray()
		{
			return ToCharArray(Encoding.UTF8);
		}

		public char[] ToCharArray(Encoding encoding)
		{
			return encoding.GetChars(_internalBuffer);
		}

		public override string ToString()
		{
			return ToString(Encoding.UTF8);
		}

		public string ToString(Encoding encoding)
		{
			return Encoding.UTF8.GetString(_internalBuffer).Trim('\0');
		}

		public void Dispose()
		{
			_handle.Free();
		}
	}
}
