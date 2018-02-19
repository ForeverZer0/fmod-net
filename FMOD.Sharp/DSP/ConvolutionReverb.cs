using System;
using System.Runtime.InteropServices;
using FMOD.Arguments;
using FMOD.Core;
using FMOD.Structures;

namespace FMOD.DSP
{
	/// <inheritdoc />
	/// <summary>
	///     Applies a "convulation reverb", or "reverb IR", effect on a sound.
	/// </summary>
	/// <seealso cref="T:FMOD.Sharp.Dsp" />
	/// <seealso cref="T:FMOD.Sharp.Structs.ImpulseResponse" />
	public class ConvolutionReverb : Dsp
	{
		/// <summary>
		///     Initializes a new instance of the <see cref="ConvolutionReverb" /> class.
		/// </summary>
		/// <param name="handle">The handle.</param>
		internal ConvolutionReverb(IntPtr handle) : base(handle)
		{
		}

		/// <summary>
		///     Gets or sets the impulse response to be used for the reverb.
		/// </summary>
		/// <value>
		///     The impulse response.
		/// </value>
		/// <seealso cref="T:FMOD.Sharp.Structs.ImpulseResponse" />
		public ImpulseResponse ImpulseResponse
		{
			get
			{
				var bytes = GetParameterData(0);
				var gcHandle = GCHandle.Alloc(bytes, GCHandleType.Pinned);
				var ir = Marshal.PtrToStructure(gcHandle.AddrOfPinnedObject(), typeof(ImpulseResponse));
				gcHandle.Free();
				return (ImpulseResponse) ir;
			}
			set
			{
				var size = Marshal.SizeOf(value);
				var bytes = new byte[size];
				var gcHandle = GCHandle.Alloc(bytes, GCHandleType.Pinned);
				Marshal.StructureToPtr(value, gcHandle.AddrOfPinnedObject(), true);
				gcHandle.Free();
				SetParameterData(0, bytes);
				ImpulseResponseChanged?.Invoke(this, new DspParamEventArgs(0));
			}
		}

		/// <summary>
		///     <para>Gets or sets the volume of echo signal to pass to output in dB.</para>
		///     <para><c>-80.0</c> to <c>10.0</c>. Default = <c>0.0</c>.</para>
		/// </summary>
		/// <value>
		///     The wet level.
		/// </value>
		public float WetLevel
		{
			get => GetParameterFloat(1);
			set
			{
				var clamped = value.Clamp(-80.0f, 10.0f);
				SetParameterFloat(1, clamped);
				WetLevelChanged?.Invoke(this, new FloatParamEventArgs(1, clamped, -80.0f, 10.0f));
				// POSSIBLE BUG: Max value stored in DSP info is 0.0, while documentation states it is 10.0. Which is it?
			}
		}

		/// <summary>
		///     <para>Gets or sets the original sound volume in dB.</para>
		///     <para><c>-80.0</c> to <c>10.0</c>. Default = <c>0.0</c>.</para>
		/// </summary>
		/// <value>
		///     The dry level.
		/// </value>
		public float DryLevel
		{
			get => GetParameterFloat(2);
			set
			{
				var clamped = value.Clamp(-80.0f, 10.0f);
				SetParameterFloat(2, clamped);
				DryLevelChanged?.Invoke(this, new FloatParamEventArgs(2, clamped, -80.0f, 10.0f));
				// POSSIBLE BUG: Max value stored in DSP info is 0.0, while documentation states it is 10.0. Which is it?
			}
		}

		/// <summary>
		///     <para>Gets or sets a value indicating whether this <see cref="ConvolutionReverb" /> is linked.</para>
		///     <para>If linked, channels are mixed together before processing through the reverb. Default = <c>true</c>. </para>
		/// </summary>
		/// <value>
		///     <c>true</c> if linked; otherwise, <c>false</c>.
		/// </value>
		public bool Linked
		{
			get => GetParameterBool(3);
			set
			{
				SetParameterBool(3, value);
				LinkedChanged?.Invoke(this, new BoolParamEventArgs(3, value));
			}
		}

		/// <summary>
		///     Occurs when <see cref="ImpulseResponse" /> property is changed.
		/// </summary>
		/// <seealso cref="DspParamEventArgs" />
		/// <seealso cref="T:FMOD.Sharp.Structs.ImpulseResponse" />
		public event EventHandler<DspParamEventArgs> ImpulseResponseChanged;

		/// <summary>
		///     Occurs when <see cref="WetLevel" /> property is changed.
		/// </summary>
		/// <seealso cref="FloatParamEventArgs" />
		public event EventHandler<FloatParamEventArgs> WetLevelChanged;

		/// <summary>
		///     Occurs when <see cref="DryLevel" /> property is changed.
		/// </summary>
		/// <seealso cref="FloatParamEventArgs" />
		public event EventHandler<FloatParamEventArgs> DryLevelChanged;

		/// <summary>
		///     Occurs when <see cref="Linked" /> property is changed.
		/// </summary>
		/// <seealso cref="BoolParamEventArgs" />
		public event EventHandler<BoolParamEventArgs> LinkedChanged;
	}
}