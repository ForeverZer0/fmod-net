using System.Runtime.InteropServices;
using FMOD.Sharp.Enums;

namespace FMOD.Sharp.Structs
{
	[StructLayout(LayoutKind.Explicit, Pack = 2)]
	public class BufferReader
	{
#pragma warning disable 649
		[FieldOffset(0)] 
		private readonly SoundFormat format;

		[FieldOffset(8)] 
		private readonly byte[] byteBuffer;

		[FieldOffset(8)] 
		private short[] int16Buffer;

		[FieldOffset(8)] 
		private Int24[] int24Buffer;

		[FieldOffset(8)] 
		private int[] int32Buffer;

		[FieldOffset(8)] 
		private float[] floatBuffer;
#pragma warning restore 649

		public byte[] ByteBuffer
		{
			get => byteBuffer;
		}

		public short[] Int16Buffer
		{
			get => int16Buffer;
		}

		public Int24[] Int24Buffer
		{
			get => int24Buffer;
		}

		public int[] Int32Buffer
		{
			get => int32Buffer;
		}

		public float[] FloatBuffer
		{
			get => floatBuffer;
		}

		public int Length
		{
			get
			{
				switch (format)
				{
					case SoundFormat.Pcm8:
						return byteBuffer.Length;
					case SoundFormat.Pcm16:
						return byteBuffer.Length / 2;
					case SoundFormat.Pcm24:
						return byteBuffer.Length / 3;
					case SoundFormat.Pcm32:
					case SoundFormat.PcmFloat:
						return byteBuffer.Length / 4;
					default:
						return 0;
				}
			}
		}

		public static BufferReader FromSound(Sound sound)
		{
			return new BufferReader(sound);
		}

		private BufferReader(Sound sound)
		{
			format = sound.Format;
			var length = sound.GetLength(TimeUnit.PcmBytes);
			sound.Lock(0, length, out var ptr1, out var ptr2, out var len1, out var len2);
			byteBuffer = new byte[len1];
			Marshal.Copy(ptr1, byteBuffer, 0, (int)len1);
			sound.Unlock(ptr1, ptr2, len1, len2);
		}
	}
}
