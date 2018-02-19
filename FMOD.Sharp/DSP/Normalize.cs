using System;
using FMOD.Arguments;
using FMOD.Core;

namespace FMOD.DSP
{
	/// <inheritdoc />
	/// <summary>
	///     This unit amplifies the sound based on the maximum peaks within the signal.
	/// </summary>
	/// <remarks>
	///     <para>
	///         For example if the maximum peaks in the signal were 50% of the bandwidth, it would scale the whole sound by
	///         2.
	///     </para>
	///     <para>
	///         The lower threshold value makes the normalizer ignores peaks below a certain point, to avoid
	///         over-amplification if a loud signal suddenly came in, and also to avoid amplifying to maximum things like
	///         background hiss.
	///     </para>
	///     <para>
	///         Because FMOD is a realtime audio processor, it doesn't have the luxury of knowing the peak for the whole
	///         sound (ie it can't see into the future), so it has to process data as it comes in.
	///     </para>
	///     <para>
	///         To avoid very sudden changes in volume level based on small samples of new data, fmod fades towards the
	///         desired amplification which makes for smooth gain control. The fadetime parameter can control this.
	///     </para>
	/// </remarks>
	/// <seealso cref="T:FMOD.Sharp.Dsp" />
	public class Normalize : Dsp
	{
		/// <summary>
		///     Initializes a new instance of the <see cref="Normalize" /> class.
		/// </summary>
		/// <param name="handle">The handle.</param>
		internal Normalize(IntPtr handle) : base(handle)
		{
		}

		/// <summary>
		///     <para>Gets or sets the time to ramp the silence to full in ms.</para>
		///     <para><c>0.0</c> to <c>20000.0</c>. Default = <c>5000.0</c>.</para>
		/// </summary>
		/// <value>
		///     The fade-in time.
		/// </value>
		public float FadeInTime
		{
			get => GetParameterFloat(0);
			set
			{
				var clamped = value.Clamp(0.0f, 20000.0f);
				SetParameterFloat(0, clamped);
				FadeInTimeChanged?.Invoke(this, new FloatParamEventArgs(0, clamped, 0.0f, 20000.0f));
			}
		}

		/// <summary>
		///     Gets or sets the lower volume range threshold to ignore.
		///     <para>Raise higher to stop amplification of very quiet signals.</para>
		///     <para><c>0.0</c> to <c>1.0</c>. Default = <c>0.1</c>.</para>
		/// </summary>
		/// <value>
		///     The lowest volume.
		/// </value>
		public float LowestVolume
		{
			get => GetParameterFloat(1);
			set
			{
				var clamped = value.Clamp(0.0f, 1.0f);
				SetParameterFloat(1, clamped);
				LowestVolumeChanged?.Invoke(this, new FloatParamEventArgs(1, clamped, 0.0f, 1.0f));
			}
		}

		/// <summary>
		///     <para>Gets or sets the maximum amplification allowed.</para>
		///     <para><c>1.0</c> = no amplifaction, higher values allow more boost.</para>
		///     <para><c>1.0</c> to <c>100000.0</c>. Default = <c>20.0</c>.</para>
		/// </summary>
		/// <value>
		///     The maximum amplification.
		/// </value>
		public float MaximumAmp
		{
			get => GetParameterFloat(2);
			set
			{
				var clamped = value.Clamp(0.0f, 100000.0f);
				SetParameterFloat(2, clamped);
				MaximumAmpChanged?.Invoke(this, new FloatParamEventArgs(2, clamped, 0.0f, 100000.0f));
			}
		}

		/// <summary>
		///     Occurs when the <see cref="FadeInTime" /> property has changed.
		/// </summary>
		/// <seealso cref="FloatParamEventArgs" />
		public event EventHandler<FloatParamEventArgs> FadeInTimeChanged;

		/// <summary>
		///     Occurs when the <see cref="LowestVolume" /> property has changed.
		/// </summary>
		/// <seealso cref="FloatParamEventArgs" />
		public event EventHandler<FloatParamEventArgs> LowestVolumeChanged;

		/// <summary>
		///     Occurs when the <see cref="MaximumAmp" /> property has changed.
		/// </summary>
		/// <seealso cref="FloatParamEventArgs" />
		public event EventHandler<FloatParamEventArgs> MaximumAmpChanged;
	}
}