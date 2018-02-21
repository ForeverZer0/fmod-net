using System;
using FMOD.NET.Arguments;
using FMOD.NET.Core;

namespace FMOD.NET.DSP
{
	public class ObjectPan : Dsp
	{
		// TODO: IMPLEMENT


		public event EventHandler<FloatParamEventArgs> ThreeDMinDistanceChanged;

		public event EventHandler<FloatParamEventArgs> ThreeDMaxDistanceChanged;


		public event EventHandler<FloatParamEventArgs> ThreeDSoundSizeChanged;

		public event EventHandler<FloatParamEventArgs> ThreeDMinExtentChanged;


		public event EventHandler<FloatParamEventArgs> OutputGainChanged;

		/// <summary>
		/// Initializes a new instance of the <see cref="ObjectPan"/> class.
		/// </summary>
		/// <param name="handle">The handle.</param>
		internal ObjectPan(IntPtr handle) : base(handle)
		{
		}

		public byte[] ThreeDPosition
		{
			get => GetParameterData(0);
			set { SetParameterData(0, value); }
		}

		public int ThreeDRolloff
		{
			get => GetParameterInt(1);
			set { SetParameterInt(1, value.Clamp(0, 4)); }
		}

		public float ThreeDMinDistance
		{
			get => GetParameterFloat(2);
			set
			{
				var clamped = value.Clamp(0.0f, float.MaxValue);
				SetParameterFloat(2, clamped);
				ThreeDMinDistanceChanged?.Invoke(this, new FloatParamEventArgs(2, clamped, 0.0f));
			}
		}

		public float ThreeDMaxDistance
		{
			get => GetParameterFloat(3);
			set
			{
				var clamped = value.Clamp(0.0f, float.MaxValue);
				SetParameterFloat(3, clamped);
				ThreeDMaxDistanceChanged?.Invoke(this, new FloatParamEventArgs(3, clamped, 0.0f));
			}
		}

		public int ThreeDExtentMode
		{
			get => GetParameterInt(4);
			set { SetParameterInt(4, value.Clamp(0, 2)); }
		}

		public float ThreeDSoundSize
		{
			get => GetParameterFloat(5);
			set
			{
				var clamped = value.Clamp(0.0f, float.MaxValue);
				SetParameterFloat(5, clamped);
				ThreeDSoundSizeChanged?.Invoke(this, new FloatParamEventArgs(5, clamped, 0.0f));
			}
		}

		public float ThreeDMinExtent
		{
			get => GetParameterFloat(6);
			set
			{
				var clamped = value.Clamp(0.0f, 360.0f);
				SetParameterFloat(6, clamped);
				ThreeDMinExtentChanged?.Invoke(this, new FloatParamEventArgs(6, clamped, 0.0f, 360.0f));
			}
		}

		public byte[] OverallGain
		{
			get => GetParameterData(7);
			set { SetParameterData(7, value); }
		}

		public float OutputGain
		{
			get => GetParameterFloat(8);
			set
			{
				var clamped = value.Clamp(0.0f, 20.0f);
				SetParameterFloat(8, clamped);
				OutputGainChanged?.Invoke(this, new FloatParamEventArgs(8, clamped, 0.0f, 20.0f));
			}
		}
	}
}
