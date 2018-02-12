using System;
using System.Runtime.InteropServices;

namespace FMOD.Sharp
{
	public partial class Sound : Handle
	{
		public class SoundLock : IDisposable
		{
			/// <summary>
			/// Gets the pointer that will point to the first part of the locked data. 
			/// </summary>
			public IntPtr PointerOne
			{
				get { return _pointerOne; }
			}

			/// <summary>
			/// <para>Gets pointer that will point to the second part of the locked data.</para>
			/// <para>This will be <see cref="IntPtr.Zero"/> if the data locked hasn't wrapped at the end of the buffer.</para>
			/// </summary>
			public IntPtr PointerTwo
			{
				get { return _pointerTwo; }
			}

			/// <summary>
			/// Gets the length of data in bytes (<see cref="byte"/>) that was locked for <see cref="PointerOne"/>.
			/// </summary>
			public uint LengthOne
			{
				get { return _lengthOne; }
			}

			/// <summary>
			/// <para>Gets the length of data in bytes (<see cref="byte"/>) that was locked for <see cref="PointerTwo"/>.</para> 
			/// <para>This will be 0 if the data locked hasn't wrapped at the end of the buffer.</para>
			/// </summary>
			public uint LengthTwo
			{
				get { return _lengthTwo; }
			}

			private readonly Sound _sound;
			private readonly IntPtr _pointerOne;
			private readonly IntPtr _pointerTwo;
			private readonly uint _lengthOne;
			private readonly uint _lengthTwo;

			public SoundLock(Sound sound, uint offset, uint length)
			{
				_sound = sound;
				_sound.Lock(offset, length, out _pointerOne, out _pointerTwo, out _lengthOne, out _lengthTwo);
			}

			public byte[] ReadBytes()
			{
				var bytes = new byte[LengthOne];
				if (LengthOne == 0 || PointerOne == IntPtr.Zero)
					return bytes;
				Marshal.Copy(PointerOne, bytes, 0, bytes.Length);
				return bytes;
			}

			public short[] ReadInt16()
			{
				// TEST ONLY
				var bytes = ReadBytes();
				var shorts = new short[bytes.Length / 2];
				for (var i = 0; i < shorts.Length; i++)
					shorts[i] = BitConverter.ToInt16(bytes, i * 2);
				return shorts;
			}

			public short[] ReadInt16TEST()
			{
				var bytes = new short[LengthOne / 2];
				if (LengthOne == 0 || PointerOne == IntPtr.Zero)
					return bytes;
				Marshal.Copy(PointerOne, bytes, 0, bytes.Length);
				return bytes;
			}



			public void Dispose()
			{
				_sound.Unlock( _pointerOne, _pointerTwo, _lengthOne, _lengthTwo);
			}
		}

	}
}