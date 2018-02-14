using System;
using System.Runtime.InteropServices;
using FMOD.Sharp.Structs;

namespace FMOD.Sharp.DSP
{
	/// <inheritdoc />
	/// <summary>
	///     Uses a Fast Fourier Transform algorithm to obtains spectrum data of a sound for analysis.
	/// </summary>
	/// <seealso cref="P:FMOD.Sharp.DSP.Fft.SpectrumData" />
	/// <seealso cref="T:FMOD.Sharp.DSP.Fft.WindowSize" />
	/// <seealso cref="T:FMOD.Sharp.DSP.Fft.WindowType" />
	public class Fft : DspBase
	{
		/// <summary>
		///     Describes window sizes to use with a Fast Fourier Transform calculation.
		/// </summary>
		/// <seealso cref="Fft" />
		/// <seealso cref="Fft.WindowSize" />
		public enum WindowSize
		{
			/// <summary>
			///     128
			/// </summary>
			Size128 = 0x0080,

			/// <summary>
			///     256
			/// </summary>
			Size256 = 0x0100,

			/// <summary>
			///     512
			/// </summary>
			Size512 = 0x0200,

			/// <summary>
			///     1024
			/// </summary>
			Size1024 = 0x0400,

			/// <summary>
			///     2048
			/// </summary>
			Size2048 = 0x0800,

			/// <summary>
			///     4096
			/// </summary>
			Size4096 = 0x1000,

			/// <summary>
			///     8192
			/// </summary>
			Size8192 = 0x2000,

			/// <summary>
			///     16384
			/// </summary>
			Size16384 = 0x4000
		}

		/// <summary>
		///     <para>
		///         List of windowing methods for the <see cref="Fft" /> unit. Used in spectrum analysis to reduce leakage /
		///         transient signals intefering with the analysis.
		///     </para>
		///     <para>
		///         This is a problem with analysis of continuous signals that only have a small portion of the signal sample
		///         (the FFT window size).
		///     </para>
		///     <para>
		///         Windowing the signal with a curve or triangle tapers the sides of the fft window to help alleviate this
		///         problem.
		///     </para>
		/// </summary>
		/// <remarks>
		///     <para>
		///         Cyclic signals such as a sine wave that repeat their cycle in a multiple of the window size do not need
		///         windowing.
		///     </para>
		///     <para>
		///         i.e. If the sine wave repeats every 1024, 512, 256 etc samples and the FMOD fft window is 1024, then the
		///         signal would not need windowing.
		///     </para>
		///     <para>Not windowing is the same as <see cref="WindowType.Rectangle" />, which is the default.</para>
		///     <para>
		///         If the cycle of the signal (ie the sine wave) is not a multiple of the window size, it will cause frequency
		///         abnormalities, so a different windowing method is needed.
		///     </para>
		/// </remarks>
		public enum WindowType
		{
			/// <summary>
			///     Rectangle
			///     <para>w[n] = 1.0</para>
			/// </summary>
			Rectangle,

			/// <summary>
			///     Triangle
			///     <para>w[n] = TRI(2n/N) </para>
			/// </summary>
			Triangle,

			/// <summary>
			///     <para>Hamming</para>
			///     <para>w[n] = 0.54 - (0.46 * COS(n/N) ) </para>
			/// </summary>
			Hamming,

			/// <summary>
			///     <para>Hann</para>
			///     <para>w[n] = 0.5 * (1.0 - COS(n/N) )</para>
			/// </summary>
			Hanning,

			/// <summary>
			///     <para>w[n] = 0.42 - (0.5 * COS(n/N) ) + (0.08 * COS(2.0 * n/N) ) </para>
			/// </summary>
			Blackman,

			/// <summary>
			///     <para>Blackman-Harris</para>
			///     <para>w[n] = 0.35875 - (0.48829 * COS(1.0 * n/N)) + (0.14128 * COS(2.0 * n/N)) - (0.01168 * COS(3.0 * n/N)) </para>
			/// </summary>
			BlackmanHarris
		}


		private readonly SpectrumData _spectrum;

		/// <summary>
		///     Initializes a new instance of the <see cref="Fft" /> class.
		/// </summary>
		/// <param name="handle">The handle.</param>
		internal Fft(IntPtr handle) : base(handle)
		{
			_spectrum = new SpectrumData();
		}

		/// <summary>
		///     <para>Gets or sets the size of the window for the FFT.</para>
		///     <para>Default is <see cref="WindowSize.Size2048" /></para>
		/// </summary>
		public WindowSize Size
		{
			get => (WindowSize) GetParameterInt(0);
			set
			{
				SetParameterInt(0, (int) value);
				WindowSizeChanged?.Invoke(this, EventArgs.Empty);
			}
		}

		/// <summary>
		///     <para>Gets or sets the type of window used for the FFT.</para>
		///     <para>See <see cref="Fft.WindowType" /> for an explanation of these types.</para>
		/// </summary>
		/// <seealso cref="Fft.WindowType" />
		public WindowType Type
		{
			get => (WindowType) GetParameterInt(1);
			set
			{
				SetParameterInt(1, (int) value);
				WindowTypeChanged?.Invoke(this, EventArgs.Empty);
			}
		}

		/// <summary>
		///     Gets the spectrum data at the current point in the playing sound.
		/// </summary>
		/// <seealso cref="SpectrumData" />
		/// <seealso cref="Fft.WindowSize" />
		/// <seealso cref="Fft.WindowType" />
		public SpectrumData SpectrumData
		{
			get
			{
				NativeInvoke(FMOD_DSP_GetParameterData(this, 2, out var ptr, out var dummy, IntPtr.Zero, 0));
				Marshal.PtrToStructure(ptr, _spectrum);
				return _spectrum;
			}
		}

		/// <summary>
		///     Gets the dominant frequencies for each channel.
		/// </summary>
		public float DominantFrequency => GetParameterFloat(3);

		/// <summary>
		///     Occurs when <see cref="Size" /> property is changed.
		/// </summary>
		/// <seealso cref="Fft" />
		/// <seealso cref="WindowSize" />
		/// <seealso cref="WindowType" />
		public event EventHandler WindowSizeChanged;

		/// <summary>
		///     Occurs when <see cref="Type" /> property is changed.
		/// </summary>
		/// <seealso cref="Fft" />
		/// <seealso cref="WindowSize" />
		/// <seealso cref="WindowType" />
		public event EventHandler WindowTypeChanged;
	}
}