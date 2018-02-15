using System;

namespace FMOD.Sharp.DSP
{
	/// <summary>
	/// This unit pans the signal, possibly upmixing or downmixing as well.
	/// </summary>
	/// <seealso cref="DspBase" />
	public class Pan : DspBase
	{
		// TODO: IMPLEMENT

		public event EventHandler<DspFloatParamChangedEventArgs> PositionChanged;

		public event EventHandler<DspFloatParamChangedEventArgs> DirectionChanged;

		public event EventHandler<DspFloatParamChangedEventArgs> ExtentChanged;

		public event EventHandler<DspFloatParamChangedEventArgs> RotationChanged;

		public event EventHandler<DspFloatParamChangedEventArgs> LowFrequencyLevelChanged;


		public event EventHandler<DspFloatParamChangedEventArgs> SeparationChanged;

		public event EventHandler<DspFloatParamChangedEventArgs> AxisChanged;




		public event EventHandler<DspFloatParamChangedEventArgs> ThreeDMinDistanceChanged;

		public event EventHandler<DspFloatParamChangedEventArgs> ThreeDMaxDistanceChanged;


		public event EventHandler<DspFloatParamChangedEventArgs> ThreeDSoundSizeChanged;

		public event EventHandler<DspFloatParamChangedEventArgs> ThreeDMinExtentChanged;

		public event EventHandler<DspFloatParamChangedEventArgs> ThreeDPanBlendChanged;




		public event EventHandler<DspFloatParamChangedEventArgs> HeightBlendChanged;

		/// <summary>
		/// Initializes a new instance of the <see cref="Pan"/> class.
		/// </summary>
		/// <param name="handle">The handle.</param>
		internal Pan(IntPtr handle) : base(handle)
		{
		}

		/// <summary>
		/// Describes various panning modes for <see cref="Pan"/> DSP.
		/// </summary>
		/// <seealso cref="Pan"/>
		/// <seealso cref="Pan.PanningMode"/>
		public enum PanMode
		{
			/// <summary>
			/// Mono down-mix.
			/// </summary>
			Mono,
			/// <summary>
			/// Stereo panning.
			/// </summary>
			Stereo,
			/// <summary>
			/// Surround panning.
			/// </summary>
			Surround
		}

		/// <summary>
		/// Occurs when the <see cref="PanningMode"/> property has changed.
		/// </summary>
		/// <seealso cref="Pan.PanningMode"/>
		/// <seealso cref="Pan.PanMode"/>
		public event EventHandler PanningModeChanged;

		/// <summary>
		/// Gets or sets the panning mode.
		/// </summary>
		/// <value>
		/// The panning mode.
		/// </value>
		/// <seealso cref="Pan.PanMode"/>
		public PanMode PanningMode
		{
			get => (PanMode) GetParameterInt(0);
			set
			{
				SetParameterInt(0, (int) value);
				PanningModeChanged?.Invoke(this, EventArgs.Empty);
			}
		}

		/// <summary>
		/// <para>Gets or sets the 2D stereo pan position.</para>
		/// <para><c>-100.0</c> to <c>100.0</c>. Default = <c>0.0</c>.</para>
		/// </summary>
		/// <value>
		/// The stereo position.
		/// </value>
		public float Position
		{
			get => GetParameterFloat(1);
			set
			{
				var clamped = value.Clamp(-100.0f, 100.0f);
				SetParameterFloat(1, clamped);
				PositionChanged?.Invoke(this, new DspFloatParamChangedEventArgs(1, clamped, -100.0f, 100.0f));
			}
		}

		/// <summary>
		/// <para>Gets or sets the 2D surround pan direction. Direction from center point of panning circle.</para>
		/// <para><c>-180.0</c> (degrees) to <c>180.0</c> (degrees).</para>
		/// <para><c>0.0</c> = front center, <c>-180.0</c> or <c>+180</c> = rear speakers center point. Default = <c>0.0</c>.</para>
		/// </summary>
		/// <value>
		/// The direction.
		/// </value>
		public float Direction
		{
			get => GetParameterFloat(2);
			set
			{
				var clamped = value.Clamp(-180.0f, 180.0f);
				SetParameterFloat(2, clamped);
				DirectionChanged?.Invoke(this, new DspFloatParamChangedEventArgs(2, clamped, -180.0f, 180.0f));
			}
		}

		/// <summary>
		/// <para>Gets or sets the 2D surround pan extent. Distance from center point of panning circle.</para> 
		/// <para><c>0.0</c> (degrees) to <c>360.0</c> (degrees). Default = <c>360.0</c>.</para>
		/// </summary>
		/// <value>
		/// The extent.
		/// </value>
		public float Extent
		{
			get => GetParameterFloat(3);
			set
			{
				var clamped = value.Clamp(0.0f, 360.0f);
				SetParameterFloat(3, clamped);
				ExtentChanged?.Invoke(this, new DspFloatParamChangedEventArgs(3, clamped, 0.0f, 360.0f));
			}
		}

		/// <summary>
		/// <para>Gets or sets the 2D surround pan rotation.</para>
		/// <para><c>-180.0</c> (degrees) to <c>180.0</c> (degrees). Default = <c>0.0</c>.</para>
		/// </summary>
		/// <value>
		/// The rotation.
		/// </value>
		public float Rotation
		{
			get => GetParameterFloat(4);
			set
			{
				var clamped = value.Clamp(-180.0f, 180.0f);
				SetParameterFloat(4, clamped);
				RotationChanged?.Invoke(this, new DspFloatParamChangedEventArgs(4, clamped, -180.0f, 180.0f));
			}
		}

		/// <summary>
		/// <para>Gets or sets the 2D surround pan low-frequency level in dB.</para> 
		/// <para><c>-80.0</c> (db) to <c>20.0</c> (db). Default = <c>0.0</c>.</para>
		/// </summary>
		/// <value>
		/// The low-frequency level.
		/// </value>
		public float LowFrequencyLevel
		{
			get => GetParameterFloat(5);
			set
			{
				var clamped = value.Clamp(-80.0f, 20.0f);
				SetParameterFloat(5, clamped);
				LowFrequencyLevelChanged?.Invoke(this, new DspFloatParamChangedEventArgs(5, clamped, -80.0f, 20.0f));
			}
		}

		/// <summary>
		/// Occurs when the <see cref="StereoMode"/> property has changed.
		/// </summary>
		/// <seealso cref="Pan.StereoModeType"/>
		/// <seealso cref="Pan.StereoMode"/>
		public event EventHandler StereoModeChanged;

		public enum StereoModeType
		{
			/// <summary>
			/// The parts of a stereo sound are spread around desination speakers based on <see cref="Pan.Extent"/> and <see cref="Pan.Direction"/>.
			/// </summary>
			Distributed,
			/// <summary>
			/// The L/R parts of a stereo sound are rotated around a circle based on <see cref="Pan.Axis"/> / <see cref="Pan.Separation"/>. 
			/// </summary>
			Discrete
		}

		/// <summary>
		/// <para>Gets or sets the stereo mode.</para>
		/// <para>Default = <see cref="Pan.StereoModeType.Discrete"/>.</para>
		/// </summary>
		/// <value>
		/// The stereo mode.
		/// </value>
		public StereoModeType StereoMode
		{
			get => (StereoModeType) GetParameterInt(6);
			set
			{
				SetParameterInt(6, (int) value);
				StereoModeChanged?.Invoke(this, EventArgs.Empty);
			}
		}

		/// <summary>
		/// <para>Gets or sets the stereo-to-surround for <see cref="Pan.StereoModeType.Discrete"/> mode.</para> 
		/// <para>Separation/width of L/R parts of stereo sound.</para>
		/// <para><c>-180.0</c> (degrees) to <c>+180.0</c> (degrees). Default = <c>60.0</c>.</para>
		/// </summary>
		/// <value>
		/// The separation.
		/// </value>
		public float Separation
		{
			get => GetParameterFloat(7);
			set
			{
				var clamped = value.Clamp(-180.0f, 180.0f);
				SetParameterFloat(7, clamped);
				SeparationChanged?.Invoke(this, new DspFloatParamChangedEventArgs(7, clamped, -180.0f, 180.0f));
			}
		}

		/// <summary>
		/// <para>Gets or sets the stereo-to-surround for <see cref="Pan.StereoModeType.Discrete"/> mode. </para>
		/// <para>Axis/rotation of L/R parts of stereo sound.</para>
		/// <para>-180.0 (degrees) to +180.0 (degrees). Default = 0.0. </para>
		/// </summary>
		/// <value>
		/// The axis.
		/// </value>
		public float Axis
		{
			get => GetParameterFloat(8);
			set
			{
				var clamped = value.Clamp(-180.0f, 180.0f);
				SetParameterFloat(8, clamped);
				AxisChanged?.Invoke(this, new DspFloatParamChangedEventArgs(8, clamped, -180.0f, 180.0f));
			}
		}

		/// <summary>
		/// Occurs when the <see cref="EnabledSpeakers"/> property has changed.
		/// </summary>
		public event EventHandler<DspIntParamChangedEventArgs> EnabledSpeakersChanged;

		/// <summary>
		/// <para>Gets or sets the enabled speakers.</para>
		/// <para>Bitmask for each speaker from 0 to 32 to be considered by panner. Use to disable speakers from being panned to.</para>
		/// <para><c>0</c> to <c>0xFFF</c>.  Default = <c>0xFFF</c> (All on).</para>
		/// </summary>
		/// <value>
		/// The enabled speakers bitmask.
		/// </value>
		public int EnabledSpeakers
		{
			get => GetParameterInt(9);
			set
			{
				var clamped = value.Clamp(0, 4095);
				SetParameterInt(9, clamped);
				EnabledSpeakersChanged?.Invoke(this, new DspIntParamChangedEventArgs(9, clamped, 0, 0xFFF));
			}
		}

		// TODO: Implement struct
		public byte[] ThreeDPosition
		{
			get => GetParameterData(10);
			set { SetParameterData(10, value); }
		}


		public int ThreeDRolloff
		{
			get => GetParameterInt(11);
			set { SetParameterInt(11, value.Clamp(0, 4)); }
		}

		public float ThreeDMinDistance
		{
			get => GetParameterFloat(12);
			set
			{
				var clamped = value.Clamp(0.0f, float.MaxValue);
				SetParameterFloat(12, clamped);
				ThreeDMinDistanceChanged?.Invoke(this, new DspFloatParamChangedEventArgs(12, clamped, 0.0f));
			}
		}

		public float ThreeDMaxDistance
		{
			get => GetParameterFloat(13);
			set
			{
				var clamped = value.Clamp(0.0f, 1000000000000000000.0f);
				SetParameterFloat(13, clamped);
				ThreeDMaxDistanceChanged?.Invoke(this, new DspFloatParamChangedEventArgs(13, clamped, 0.0f, 1000000000000000000.0f));
			}
		}

		public int ThreeDExtentMode
		{
			get => GetParameterInt(14);
			set { SetParameterInt(14, value.Clamp(0, 2)); }
		}

		public float ThreeDSoundSize
		{
			get => GetParameterFloat(15);
			set
			{
				var clamped = value.Clamp(0.0f, 1000000000000000000.0f);
				SetParameterFloat(15, clamped);
				ThreeDSoundSizeChanged?.Invoke(this, new DspFloatParamChangedEventArgs(15, clamped, 0.0f, 1000000000000000000.0f));
			}
		}

		public float ThreeDMinExtent
		{
			get => GetParameterFloat(16);
			set
			{
				var clamped = value.Clamp(0.0f, 360.0f);
				SetParameterFloat(16, clamped);
				ThreeDMinExtentChanged?.Invoke(this, new DspFloatParamChangedEventArgs(16, clamped, 0.0f, 360.0f));
			}
		}

		public float ThreeDPanBlend
		{
			get => GetParameterFloat(17);
			set
			{
				var clamped = value.Clamp(0.0f, 1.0f);
				SetParameterFloat(17, clamped);
				ThreeDPanBlendChanged?.Invoke(this, new DspFloatParamChangedEventArgs(17, clamped, 0.0f, 1.0f));
			}
		}

		public int LfeUpmixEnabl
		{
			get => GetParameterInt(18);
			set { SetParameterInt(18, value.Clamp(0, 1)); }
		}

		public byte[] OverallGain
		{
			get => GetParameterData(19);
			set { SetParameterData(19, value); }
		}

		public int OutputSpeaker
		{
			get => GetParameterInt(20);
			set { SetParameterInt(20, value.Clamp(0, 8)); }
		}

		public float HeightBlend
		{
			get => GetParameterFloat(21);
			set
			{
				var clamped = value.Clamp(-1.0f, 1.0f);
				SetParameterFloat(21, clamped);
				HeightBlendChanged?.Invoke(this, new DspFloatParamChangedEventArgs(21, clamped, -1.0f, 1.0f));
			}
		}
	}
}
// Code generated by FMOD# DSP Factory  Tuesday, February 13, 2018 11:53:01 AM
