using System.Runtime.InteropServices;
using FMOD.Core;
using FMOD.Enumerations;

// TODO: Document

namespace FMOD.Structures
{
	[StructLayout(LayoutKind.Explicit, Pack = 2)]
	public class BufferReader
	{
#pragma warning disable 649
		[FieldOffset(0)] 
		private readonly SoundFormat _format;

		[FieldOffset(8)] 
		private readonly byte[] _byteBuffer;

		[FieldOffset(8)] 
		private short[] _int16Buffer;

		[FieldOffset(8)] 
		private Int24[] _int24Buffer;

		[FieldOffset(8)] 
		private int[] _int32Buffer;

		[FieldOffset(8)] 
		private float[] _floatBuffer;
#pragma warning restore 649

		/// <summary>
		/// Gets the internal buffer as an array of 8-bit <see cref="byte"/> values.
		/// </summary>
		/// <value>
		/// The byte buffer.
		/// </value>
		public byte[] ByteBuffer
		{
			get => _byteBuffer;
		}

		/// <summary>
		/// Gets the internal buffer as an array of 16-bit signed <see cref="short"/> values.
		/// </summary>
		/// <value>
		/// The int16 buffer.
		/// </value>
		public short[] Int16Buffer
		{
			get => _int16Buffer;
		}

		/// <summary>
		/// Gets the internal buffer as an array of psuedo-24-bit signed <see cref="Int24"/> values.
		/// </summary>
		/// <value>
		/// The int24 buffer.
		/// </value>
		public Int24[] Int24Buffer
		{
			get => _int24Buffer;
		}

		/// <summary>
		/// Gets the internal buffer as an array of 32-bit signed <see cref="int"/> values.
		/// </summary>
		/// <value>
		/// The buffer .
		/// </value>
		public int[] Int32Buffer
		{
			get => _int32Buffer;
		}

		/// <summary>
		/// Gets the internal buffer as an array of 32-bit <see cref="float"/> values.
		/// </summary>
		/// <value>
		/// The float buffer.
		/// </value>
		public float[] FloatBuffer
		{
			get => _floatBuffer;
		}

		public int Length
		{
			get
			{
				switch (_format)
				{
					case SoundFormat.Pcm8:
						return _byteBuffer.Length;
					case SoundFormat.Pcm16:
						return _byteBuffer.Length / 2;
					case SoundFormat.Pcm24:
						return _byteBuffer.Length / 3;
					case SoundFormat.Pcm32:
					case SoundFormat.PcmFloat:
						return _byteBuffer.Length / 4;
					default:
						return 0;
				}
			}
		}

		public BufferReader(SoundFormat format, byte[] buffer)
		{
			_format = format;
			_byteBuffer = buffer;
		}

		public BufferReader(Sound sound)
		{
			_format = sound.Format;
			var length = sound.GetLength(TimeUnit.PcmBytes);
			sound.Lock(0, length, out var ptr1, out var ptr2, out var len1, out var len2);
			_byteBuffer = new byte[len1];
			Marshal.Copy(ptr1, _byteBuffer, 0, (int)len1);
			sound.Unlock(ptr1, ptr2, len1, len2);
		}
	}
}
