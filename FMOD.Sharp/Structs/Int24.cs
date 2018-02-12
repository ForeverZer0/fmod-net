using System.Runtime.InteropServices;

namespace FMOD.Sharp.Structs
{
	[StructLayout(LayoutKind.Sequential, Size = 3, Pack = 1)]
	public struct Int24
	{
		private readonly byte first;
		private readonly byte second;
		private readonly byte third;

		public static int MaxValue
		{
			get => 8388607;
		}

		public static int MinValue
		{
			get => -8388608;
		}

		public Int24(int value)
		{
			first = (byte)(value & 0xFF);
			second = (byte)(value >> 8); 
			third = (byte)(value >> 16);
		}

		public int Value
		{
			get => first | (second << 8) | (third << 16);
		}

		public static implicit operator int(Int24 value)
		{
			return value.Value;
		}

		public override string ToString()
		{
			return Value.ToString();
		}
	}
}
