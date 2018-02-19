using System;
using FMOD.Arguments;
using FMOD.Core;

namespace FMOD.DSP
{
	/// <summary>
	/// This unit sends a copy of the signal to a return DSP anywhere in the DSP tree.
	/// </summary>
	/// <seealso cref="FMOD.Sharp.DSP.DspBase" />
	/// <seealso cref="Return"/>
	public class Send : Dsp
	{
		/// <summary>
		/// Occurs when <see cref="SendLevel"/> property is changed.
		/// </summary>
		/// <seealso cref="SendLevel"/>
		/// <seealso cref="FloatParamEventArgs"/>
		public event EventHandler<FloatParamEventArgs> SendLevelChanged;

		/// <summary>
		/// Initializes a new instance of the <see cref="Send"/> class.
		/// </summary>
		/// <param name="handle">The handle.</param>
		internal Send(IntPtr handle) : base(handle)
		{
		}

		/// <summary>
		/// <para>Gets the ID of the <see cref="Return"/> DSP this send is connected to (integer values only). </para>
		/// <para><c>-1</c> indicates no connected <see cref="Return"/> DSP. Default = <c>-1</c>.</para>
		/// </summary>
		/// <value>
		/// The return identifier.
		/// </value>
		public int ReturnId
		{
			get => GetParameterInt(0);
		}

		/// <summary>
		/// <para>Gets or sets the send level.</para>
		/// <para><c>0.0</c> to <c>1.0</c>. Default = <c>1.0</c></para> 
		/// </summary>
		/// <value>
		/// The send level.
		/// </value>
		public float SendLevel
		{
			get => GetParameterFloat(1);
			set
			{
				var clamped = value.Clamp(0.0f, 10.0f);
				SetParameterFloat(1, clamped);
				SendLevelChanged?.Invoke(this, new FloatParamEventArgs(1, clamped, 0.0f, 10.0f));
			}
		}

		[Obsolete("This property is not used.", true)]
		public byte[] OverallGain
		{
			get => GetParameterData(2);
			set { SetParameterData(2, value); }
		}
	}
}
