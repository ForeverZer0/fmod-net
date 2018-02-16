using System;
using FMOD.Core;
using FMOD.Enumerations;

namespace FMOD.DSP
{
	/// <inheritdoc />
	/// <summary>
	///     This unit sends a copy of the signal to a return DSP anywhere in the DSP tree.
	/// </summary>
	/// <seealso cref="T:FMOD.Sharp.Dsp" />
	public class Return : Dsp
	{
		/// <summary>
		///     Initializes a new instance of the <see cref="Return" /> class.
		/// </summary>
		/// <param name="handle">The handle.</param>
		internal Return(IntPtr handle) : base(handle)
		{
		}

		/// <summary>
		///     Gets the identifier for this <see cref="Dsp" />.
		/// </summary>
		/// <value>
		///     The identifier.
		/// </value>
		public int Id => GetParameterInt(0);

		/// <summary>
		///     Gets or sets the input speaker mode.
		/// </summary>
		/// <value>
		///     The input speaker mode.
		/// </value>
		/// <seealso cref="SpeakerMode" />
		public SpeakerMode InputSpeakerMode
		{
			get => (SpeakerMode) GetParameterInt(1);
			set
			{
				SetParameterInt(1, (int) value);
				InputSpeakerModeChanged?.Invoke(this, EventArgs.Empty);
			}
		}

		/// <summary>
		///     Occurs when <see cref="InputSpeakerMode" /> property has changed.
		/// </summary>
		public event EventHandler InputSpeakerModeChanged;
	}
}