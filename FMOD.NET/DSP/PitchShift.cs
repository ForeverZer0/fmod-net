#region License

// PitchShift.cs is distributed under the Microsoft Public License (MS-PL)
// 
// Copyright (c) 2018,  Eric Freed
// All Rights Reserved.
// 
// This license governs use of the accompanying software. If you use the software, you
// accept this license. If you do not accept the license, do not use the software.
// 
// 1. Definitions
// The terms "reproduce," "reproduction," "derivative works," and "distribution" have the
// same meaning here as under U.S. copyright law.
// A "contribution" is the original software, or any additions or changes to the software.
// A "contributor" is any person that distributes its contribution under this license.
// "Licensed patents" are a contributor's patent claims that read directly on its contribution.
// 
// 2. Grant of Rights
// (A) Copyright Grant- Subject to the terms of this license, including the license conditions 
// and limitations in section 3, each contributor grants you a non-exclusive, worldwide, royalty-free 
// copyright license to reproduce its contribution, prepare derivative works of its contribution, and 
// distribute its contribution or any derivative works that you create.
// 
// (B) Patent Grant- Subject to the terms of this license, including the license conditions and 
// limitations in section 3, each contributor grants you a non-exclusive, worldwide, royalty-free license
//  under its licensed patents to make, have made, use, sell, offer for sale, import, and/or otherwise 
// dispose of its contribution in the software or derivative works of the contribution in the software.
// 
// 3. Conditions and Limitations
// (A) No Trademark License- This license does not grant you rights to use any contributors' name, 
// logo, or trademarks.
// 
// (B) If you bring a patent claim against any contributor over patents that you claim are infringed by 
// the software, your patent license from such contributor to the software ends automatically.
// 
// (C) If you distribute any portion of the software, you must retain all copyright, patent, trademark, and
//  attribution notices that are present in the software.
// 
// (D) If you distribute any portion of the software in source code form, you may do so only under this 
// license by including a complete copy of this license with your distribution. If you distribute any portion
//  of the software in compiled or object code form, you may only do so under a license that complies 
// with this license.
// 
// (E) The software is licensed "as-is." You bear the risk of using it. The contributors give no express 
// warranties, guarantees or conditions. You may have additional consumer rights under your local laws 
// which this license cannot change. To the extent permitted under your local laws, the contributors 
// exclude the implied warranties of merchantability, fitness for a particular purpose and non-infringement.
// 
// Created 3:10 PM 02/13/2018

#endregion

#region Using Directives

using System;
using FMOD.NET.Arguments;
using FMOD.NET.Core;

#endregion

namespace FMOD.NET.DSP
{
	/// <inheritdoc />
	/// <summary>
	///     Applies a "pitch shift" effect to a sound.
	/// </summary>
	/// <remarks>
	///     <para>
	///         This pitch shifting unit can be used to change the pitch of a sound without speeding it up or slowing it
	///         down.
	///     </para>
	///     <para>
	///         It can also be used for time stretching or scaling, for example if the pitch was doubled, and the frequency
	///         of the sound was halved, the pitch of the sound would sound correct but it would be twice as slow.
	///     </para>
	///     <para>
	///         <alert class="warning">
	///             <para>
	///                 This filter is very computationally expensive! Similar to a vocoder, it requires several overlapping
	///                 FFT and IFFT's to produce smooth output, and can require around 440mhz for 1 stereo 48khz signal using
	///                 the default settings.
	///             </para>
	///             <para>Reducing the signal to mono will half the cpu usage.</para>
	///             <para>
	///                 Reducing this will lower audio quality, but what settings to use are largely dependant on the sound
	///                 being played. A noisy polyphonic signal will need higher FFT size compared to a speaking voice for
	///                 example.
	///             </para>
	///         </alert>
	///     </para>
	///     <para>
	///         This pitch shifter is based on the pitch shifter code at http://www.dspdimension.com, written by Stephan M.
	///         Bernsee.
	///     </para>
	///     <para>The original code is COPYRIGHT 1999-2003 Stephan M. Bernsee smb@dspdimension.com.</para>
	/// </remarks>
	/// <seealso cref="Dsp" />
	/// <seealso cref="FftWindowSize" />
	public class PitchShift : Dsp
	{
		#region Events

		/// <summary>
		///     Occurs when the <see cref="FftSize" /> property is changed.
		/// </summary>
		public event EventHandler FftSizeChanged;

		/// <summary>
		///     Occurs when the <see cref="MaxChannels" /> property is changed.
		/// </summary>
		/// <seealso cref="FloatParamEventArgs" />
		public event EventHandler<FloatParamEventArgs> MaxChannelsChanged;

		/// <summary>
#pragma warning disable 618
		///     Occurs when the <see cref="Overlap" /> property is changed.
#pragma warning restore 618
		/// </summary>
		/// <seealso cref="FloatParamEventArgs" />
		[Obsolete("Do not use, this function has been removed from the native library.")]
		public event EventHandler<FloatParamEventArgs> OverlapChanged;

		/// <summary>
		///     Occurs when the <see cref="Pitch" /> property is changed.
		/// </summary>
		/// <seealso cref="FloatParamEventArgs" />
		public event EventHandler<FloatParamEventArgs> PitchChanged;

		#endregion

		#region Constructors

		/// <summary>
		///     Initializes a new instance of the <see cref="PitchShift" /> class.
		/// </summary>
		/// <param name="handle">The handle.</param>
		protected PitchShift(IntPtr handle) : base(handle)
		{
		}

		#endregion

		#region Properties

		/// <summary>
		///     <para>Gets or sets the pitch value.</para>
		///     <para><c>0.5</c> to <c>2.0</c>. Default = <c>1.0</c>.</para>
		///     <para><c>0.5</c> = one octave down, <c>2.0</c> = one octave up. <c>1.0</c> does not change the pitch.</para>
		/// </summary>
		/// <value>
		///     The pitch.
		/// </value>
		public float Pitch
		{
			get => GetParameterFloat(0);
			set
			{
				var clamped = value.Clamp(0.5f, 2.0f);
				SetParameterFloat(0, clamped);
				PitchChanged?.Invoke(this, new FloatParamEventArgs(0, clamped, 0.5f, 2.0f));
			}
		}

		/// <summary>
		///     <para>Gets or sets the Fast Fourier Transform window size.</para>
		///     <para>
		///         Increase this to reduce 'smearing'. This effect is a warbling sound similar to when an MP3 is encoded at very
		///         low bitrates.
		///     </para>
		///     <para>Default = <see cref="FftWindowSize.Size1024" /></para>
		/// </summary>
		/// <value>
		///     The size of the FFT.
		/// </value>
		public FftWindowSize FftSize
		{
			get => (FftWindowSize) GetParameterFloat(1);
			set
			{
				SetParameterFloat(1, (float) value);
				FftSizeChanged?.Invoke(this, EventArgs.Empty);
			}
		}

		/// <summary>
		///     Removed. Do not use. FMOD now uses 4 overlaps and cannot be changed.
		/// </summary>
		/// <value>
		///     The overlap.
		/// </value>
		[Obsolete("Do not use, this function has been removed from the native library.")]
		public float Overlap
		{
			get => GetParameterFloat(2);
			set
			{
				var clamped = value.Clamp(1.0f, 32.0f);
				SetParameterFloat(2, clamped);
				OverlapChanged?.Invoke(this, new FloatParamEventArgs(2, clamped, 1.0f, 32.0f));
			}
		}

		/// <summary>
		///     <para>Gets or sets the maximum channels supported.</para>
		///     <para>
		///         <c>0</c> to <c>16</c>.
		///         <para>
		///             <c>0</c> = same as FMOD's default output polyphony, <c>1</c> = mono, <c>2</c> = stereo etc. See remarks
		///             for more.
		///         </para>
		///         <para>Default = <c>0</c>. It is suggested to leave at <c>0</c>! </para>
		///     </para>
		/// </summary>
		/// <value>
		///     The maximum channels.
		/// </value>
		/// <remarks>
		///     <para>
		///         Dictates the amount of memory allocated. By default, the maxchannels value is 0. If FMOD is set to stereo,
		///         the pitch shift unit will allocate enough memory for 2 channels. If it is 5.1, it will allocate enough memory
		///         for a 6 channel pitch shift, etc.
		///     </para>
		///     <para>
		///         If the pitch shift effect is only ever applied to the global mix (ie it was added with
		///         <see cref="ChannelGroup.AddDsp(Dsp, int)" />), then 0 is the value to set as it will be enough to handle all
		///         speaker modes.
		///     </para>
		///     <para>
		///         When the pitch shift is added to a channel (ie <see cref="Channel.AddDsp(Dsp, int)" />) then the channel
		///         count that comes in could be anything from 1 to 8 possibly. It is only in this case where you might want to
		///         increase the channel count above the output's channel count.
		///     </para>
		///     <para>
		///         If a channel pitch shift is set to a lower number than the sound's channel count that is coming in, it will
		///         not pitch shift the sound.
		///     </para>
		/// </remarks>
		public float MaxChannels
		{
			get => GetParameterFloat(3);
			set
			{
				var clamped = value.Clamp(0.0f, 16.0f);
				SetParameterFloat(3, clamped);
				MaxChannelsChanged?.Invoke(this, new FloatParamEventArgs(3, clamped, 0.0f, 16.0f));
			}
		}

		#endregion

		/// <summary>
		///     Describes window sizes to use with a Fast Fourier Transform calculation.
		/// </summary>
		/// <seealso cref="PitchShift" />
		/// <seealso cref="PitchShift.FftSize" />
		public enum FftWindowSize
		{
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
			Size4096 = 0x1000
		}
	}
}