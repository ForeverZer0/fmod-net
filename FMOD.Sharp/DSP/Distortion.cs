using System;
using FMOD.Arguments;
using FMOD.Core;

namespace FMOD.DSP
{
	/// <inheritdoc />
	/// <summary>
	///     Applies a "distortion" effect on a sound.
	/// </summary>
	/// <seealso cref="T:FMOD.Sharp.Dsp" />
	public class Distortion : Dsp
	{
		/// <summary>
		///     Initializes a new instance of the <see cref="Distortion" /> class.
		/// </summary>
		/// <param name="handle">The handle.</param>
		internal Distortion(IntPtr handle) : base(handle)
		{
		}

		/// <summary>
		///     <para>Gets or sets the distortion value.</para>
		///     <para><c>0.0</c> to <c>1.0</c>. Default = <c>0.5</c>.</para>
		/// </summary>
		/// <value>
		///     The level.
		/// </value>
		public float Level
		{
			get => GetParameterFloat(0);
			set
			{
				var clamped = value.Clamp(0.0f, 1.0f);
				SetParameterFloat(0, clamped);
				LevelChanged?.Invoke(this, new DspFloatParamChangedEventArgs(0, clamped, 0.0f, 1.0f));
			}
		}

		/// <summary>
		///     Occurs when the <see cref="Level" /> property is changed.
		/// </summary>
		/// <seealso cref="DspFloatParamChangedEventArgs" />
		public event EventHandler<DspFloatParamChangedEventArgs> LevelChanged;
	}
}