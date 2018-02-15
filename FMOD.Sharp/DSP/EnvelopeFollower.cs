using System;

namespace FMOD.Sharp.DSP
{
	/// <summary>
	///     This unit tracks the envelope of the input/sidechain signal.
	/// </summary>
	/// <remarks>
	///     <para>Deprecated and will be removed in a future release.</para>
	///     <para>This unit does not affect the incoming signal.</para>
	/// </remarks>
	/// <seealso cref="FMOD.Sharp.DSP.DspBase" />
	[Obsolete("Deprecated and will be removed in a future release.")]
	public class EnvelopeFollower : DspBase
	{
		/// <summary>
		///     Initializes a new instance of the <see cref="EnvelopeFollower" /> class.
		/// </summary>
		/// <param name="handle">The handle.</param>
		internal EnvelopeFollower(IntPtr handle) : base(handle)
		{
		}

		/// <summary>
		///     <para>Gets or sets the attack time in milliseconds.</para>
		///     <para>Range from <c>0.1</c> through <c>1000.0</c>. Default = <c>20.0</c>.</para>
		/// </summary>
		/// <value>
		///     The attack time.
		/// </value>
		public float Attack
		{
			get => GetParameterFloat(0);
			set
			{
				var clamped = value.Clamp(0.1f, 500.0f);
				SetParameterFloat(0, clamped);
				AttackChanged?.Invoke(this, new DspFloatParamChangedEventArgs(0, clamped, 0.1f, 500.0f));
			}
		}

		/// <summary>
		///     <para>Gets or sets the release time in milliseconds.</para>
		///     <para>Range from <c>10.0</c> through <c>5000.0</c>. Default = <c>10.0</c></para>
		///     0
		/// </summary>
		/// <value>
		///     The release time.
		/// </value>
		public float Release
		{
			get => GetParameterFloat(1);
			set
			{
				var clamped = value.Clamp(10.0f, 5000.0f);
				SetParameterFloat(1, clamped);
				ReleaseChanged?.Invoke(this, new DspFloatParamChangedEventArgs(1, clamped, 10.0f, 5000.0f));
			}
		}

		/// <summary>
		///     <para>Gets the current value of the envelope.</para>
		///     <para>Range from <c>0.0</c> to <c>1.0</c>.</para>
		/// </summary>
		/// <value>
		///     The current envelope.
		/// </value>
		public float Envelope => GetParameterFloat(2);

		/// <summary>
		///     <para>Gets or sets a value indicating whether to analyse the sidechain signal instead of the input signal.</para>
		///     <para>Default = <c>false</c>.</para>
		/// </summary>
		/// <value>
		///     <c>true</c> if to use sidechain; otherwise, <c>false</c>.
		/// </value>
		public bool UseSideChain
		{
			get => BitConverter.ToInt32(GetParameterData(3), 0) == 1;
			set
			{
				SetParameterData(3, BitConverter.GetBytes(value ? 1 : 0));
				UseSideChainChanged?.Invoke(this, new DspBoolParamChangedEventArgs(3, value));
			}
		}

		/// <summary>
		///     Occurs when the <see cref="Attack" /> property is changed.
		/// </summary>
		/// <seealso cref="Attack" />
		/// <seealso cref="DspFloatParamChangedEventArgs" />
		public event EventHandler<DspFloatParamChangedEventArgs> AttackChanged;

		/// <summary>
		///     Occurs when the <see cref="Release" /> property is changed.
		/// </summary>
		/// <seealso cref="Release" />
		/// <seealso cref="DspFloatParamChangedEventArgs" />
		public event EventHandler<DspFloatParamChangedEventArgs> ReleaseChanged;

		/// <summary>
		///     Occurs when the <see cref="UseSideChain" /> property is changed.
		/// </summary>
		/// <seealso cref="UseSideChain" />
		/// <seealso cref="DspBoolParamChangedEventArgs" />
		public event EventHandler<DspBoolParamChangedEventArgs> UseSideChainChanged;
	}
}