using System;
using System.Drawing;
using System.Runtime.InteropServices;
using FMOD.Sharp.Data;
using FMOD.Sharp.Enums;
using FMOD.Sharp.Structs;

namespace FMOD.Sharp.DSP
{
	/// <inheritdoc />
	/// <summary>
	/// <para>Describes a Digital Signal Processing unit for applying effects on sounds.</para>
	/// <para>This class must be inherited.</para>
	/// </summary>
	/// <seealso cref="T:FMOD.Sharp.HandleBase" />
	public partial class DspBase : HandleBase
	{
		#region Delegates & Events

		public event EventHandler<ParamChangeEventArgs> ParameterChanged;

		#endregion

		#region Constructors & Destructor

		internal DspBase(IntPtr handle) : base(handle)
		{
		}

		#endregion

		#region Properties & Indexers

		public bool Active
		{
			get
			{
				NativeInvoke(FMOD_DSP_GetActive(this, out var active));
				return active;
			}
			set => NativeInvoke(FMOD_DSP_SetActive(this, value));
		}

		public bool Bypass
		{
			get
			{
				NativeInvoke(FMOD_DSP_GetBypass(this, out var bypass));
				return bypass;
			}
			set => NativeInvoke(FMOD_DSP_SetBypass(this, value));
		}

		public ChannelFormat ChannelFormat
		{
			get
			{
				NativeInvoke(FMOD_DSP_GetChannelFormat(this, out var mask, out var count, out var mode));
				return new ChannelFormat
				{
					ChannelMask = mask,
					ChannelCount = count,
					SpeakerMode = mode
				};
			}
			set => NativeInvoke(FMOD_DSP_SetChannelFormat(this, value.ChannelMask,
				value.ChannelCount, value.SpeakerMode));
		}

		public DspType DspType
		{
			get
			{
				NativeInvoke(FMOD_DSP_GetType(this, out var type));
				return type;
			}
		}

		public int InputCount
		{
			get
			{
				NativeInvoke(FMOD_DSP_GetNumInputs(this, out var count));
				return count;
			}
		}

		public bool InputMeteringEnabled
		{
			get
			{
				NativeInvoke(FMOD_DSP_GetMeteringEnabled(this, out var input, out var dummy));
				return input;
			}
			set
			{
				var output = OutputMeteringEnabled;
				NativeInvoke(FMOD_DSP_SetMeteringEnabled(this, value, output));
			}
		}

		public bool IsIdle
		{
			get
			{
				NativeInvoke(FMOD_DSP_GetIdle(this, out var idle));
				return idle;
			}
		}

		public int OutputCount
		{
			get
			{
				NativeInvoke(FMOD_DSP_GetNumOutputs(this, out var count));
				return count;
			}
		}

		public bool OutputMeteringEnabled
		{
			get
			{
				NativeInvoke(FMOD_DSP_GetMeteringEnabled(this, out var dummy, out var output));
				return output;
			}
			set
			{
				var input = InputMeteringEnabled;
				NativeInvoke(FMOD_DSP_SetMeteringEnabled(this, input, value));
			}
		}

		public int ParameterCount
		{
			get
			{
				NativeInvoke(FMOD_DSP_GetNumParameters(this, out var count));
				return count;
			}
		}

		public FmodSystem ParentFmodSystem
		{
			get
			{
				NativeInvoke(FMOD_DSP_GetSystemObject(this, out var system));
				return Core.Create<FmodSystem>(system);
			}
		}

		public IntPtr UserData
		{
			get
			{
				NativeInvoke(FMOD_DSP_GetUserData(this, out var data));
				return data;
			}
			set => NativeInvoke(FMOD_DSP_SetUserData(this, value));
		}

		public WetDryMix WetDryMix
		{
			get
			{
				NativeInvoke(FMOD_DSP_GetWetDryMix(this, out var prewet, out var postwet, out var dry));
				return new WetDryMix
				{
					PreWet = prewet,
					PostWet = postwet,
					Dry = dry
				};
			}
			set => NativeInvoke(FMOD_DSP_SetWetDryMix(this, value.PreWet, value.PostWet, value.Dry));
		}

		#endregion

		#region Methods

		public DspConnection AddInput(DspBase dspBase, DspConnectionType type = DspConnectionType.Standard)
		{
			NativeInvoke(FMOD_DSP_AddInput(this, dspBase, out var connection, type));
			return Core.Create<DspConnection>(connection);
		}

		public void DisableMetering()
		{
			NativeInvoke(FMOD_DSP_SetMeteringEnabled(this, false, false));
		}

		public void DisconnectAll()
		{
			NativeInvoke(FMOD_DSP_DisconnectAll(this, true, true));
		}

		public void DisconnectFrom(DspBase dspBase)
		{
			NativeInvoke(FMOD_DSP_DisconnectFrom(this, dspBase, IntPtr.Zero));
		}

		public void DisconnectFrom(DspBase dspBase, DspConnection connection)
		{
			NativeInvoke(FMOD_DSP_DisconnectFrom(this, dspBase, connection));
		}

		public void DisconnectInputs()
		{
			NativeInvoke(FMOD_DSP_DisconnectAll(this, true, false));
		}

		public void DisconnectOutputs()
		{
			NativeInvoke(FMOD_DSP_DisconnectAll(this, false, true));
		}

		public void EnableMetering()
		{
			NativeInvoke(FMOD_DSP_SetMeteringEnabled(this, true, true));
		}

		public static DspBase FromType(IntPtr dspHandle, DspType dspType)
		{
			if (dspHandle == IntPtr.Zero)
				return null;
#pragma warning disable 618
			switch (dspType)
			{
				case DspType.Unknown:
					return null;
//				case DspType.Mixer:
//					return Core.Create<Mixer>(dspHandle);
				case DspType.Oscillator:
					return Core.Create<Oscillator>(dspHandle);
				case DspType.Lowpass:
					return Core.Create<Lowpass>(dspHandle);
//				case DspType.ItLowpass:
//					return Core.Create<ItLowpass>(dspHandle);
				case DspType.Highpass:
					return Core.Create<Highpass>(dspHandle);
				case DspType.Echo:
					return Core.Create<Echo>(dspHandle);
//				case DspType.Fader:
//					return Core.Create<Fader>(dspHandle);
				case DspType.Flange:
					return Core.Create<Flange>(dspHandle);
				case DspType.Distortion:
					return Core.Create<Distortion>(dspHandle);
//				case DspType.Normalize:
//					return Core.Create<Normalize>(dspHandle);
//				case DspType.Limiter:
//					return Core.Create<Limiter>(dspHandle);
//				case DspType.ParamEq:
//					return Core.Create<ParamEq>(dspHandle);
				case DspType.PitchShift:
					return Core.Create<PitchShift>(dspHandle);
				case DspType.Chorus:
					return Core.Create<Chorus>(dspHandle);
				case DspType.VstPlugin:
					return null;
				case DspType.WinampPlugin:
					return null;
//				case DspType.ItEcho:
//					return Core.Create<ItEcho>(dspHandle);
//				case DspType.Compressor:
//					return Core.Create<Compressor>(dspHandle);
				case DspType.SfxReverb:
					return Core.Create<SfxReverb>(dspHandle);
				case DspType.LowpassSimple:
					return Core.Create<LowpassSimple>(dspHandle);
//				case DspType.Delay:
//					return Core.Create<Delay>(dspHandle);
				case DspType.Tremolo:
					return Core.Create<Tremolo>(dspHandle);
				case DspType.LadspaPlugin:
					return null;
//				case DspType.Send:
//					return Core.Create<Send>(dspHandle);
//				case DspType.Return:
//					return Core.Create<Return>(dspHandle);
				case DspType.HighpassSimple:
					return Core.Create<HighpassSimple>(dspHandle);
//				case DspType.Pan:
//					return Core.Create<Pan>(dspHandle);
//				case DspType.ThreeEq:
//					return Core.Create<ThreeEq>(dspHandle);
				case DspType.Fft:
					return Core.Create<Fft>(dspHandle);
//				case DspType.LoudnessMeter:
//					return Core.Create<LoudnessMeter>(dspHandle);
//				case DspType.EnvelopeFollower:
//					return Core.Create<EnvelopeFollower>(dspHandle);
				case DspType.ConvolutionReverb:
					return Core.Create<ConvolutionReverb>(dspHandle);
				case DspType.ChannelMix:
					return Core.Create<ChannelMix>(dspHandle);
//				case DspType.Transceiver:
//					return Core.Create<Transceiver>(dspHandle);
//				case DspType.ObjectPan:
//					return Core.Create<ObjectPan>(dspHandle);
				case DspType.MultiBandEq:
					return Core.Create<MultiBandEq>(dspHandle);
				case DspType.Max:
					return null;
				default:
					return null;
			}
#pragma warning restore 618
		}

		public int GetDataParameterIndex(int dataType)
		{
			NativeInvoke(FMOD_DSP_GetDataParameterIndex(this, dataType, out var index));
			return index;
		}

		public DspInfo GetInfo()
		{
			var namePtr = Marshal.StringToHGlobalAnsi(new String(' ', 32));
			NativeInvoke(FMOD_DSP_GetInfo(this, namePtr, out var version, out var channels,
				out var width, out var height));
			// TODO: Fix implementation for version
			return new DspInfo
			{
				Name = Marshal.PtrToStringAnsi(namePtr, 32).Trim(),
				Version = Core.UInt32ToVersion(version),
				ChannelCount = channels,
				ConfigWindowSize = new Size(width, height)
			};
		}

		public DspBase GetInput(int index)
		{
			NativeInvoke(FMOD_DSP_GetInput(this, index, out var input, out var dummy));
			return Core.Create<DspBase>(input);
		}

		public DspConnection GetInputConnection(int index)
		{
			NativeInvoke(FMOD_DSP_GetInput(this, index, out var dummy, out var connection));
			return Core.Create<DspConnection>(connection);
		}

		public DspBase GetOutput(int index)
		{
			NativeInvoke(FMOD_DSP_GetOutput(this, index, out var output, out var dummy));
			return Core.Create<DspBase>(output);
		}

		public ChannelFormat GetOutputChannelFormat()
		{
			return GetOutputChannelFormat(ChannelFormat);
		}

		public ChannelFormat GetOutputChannelFormat(ChannelFormat inputFormat)
		{
			NativeInvoke(FMOD_DSP_GetOutputChannelFormat(this, inputFormat.ChannelMask,
				inputFormat.ChannelCount, inputFormat.SpeakerMode, out var chanMask,
				out var chanCount, out var speakerMode));
			return new ChannelFormat
			{
				ChannelMask = chanMask,
				ChannelCount = chanCount,
				SpeakerMode = speakerMode
			};
		}

		public DspConnection GetOutputConnection(int index)
		{
			NativeInvoke(FMOD_DSP_GetOutput(this, index, out var dummy, out var connection));
			return Core.Create<DspConnection>(connection);
		}

		public DspParameterDesc GetParameterInfo(int parameterIndex)
		{
			NativeInvoke(FMOD_DSP_GetParameterInfo(this, parameterIndex, out var desc));
			var info = Marshal.PtrToStructure(desc, typeof(DspParameterDesc));
			return (DspParameterDesc) info;
		}

		public void Reset()
		{
			NativeInvoke(FMOD_DSP_Reset(this));
		}

		public void ShowConfigDialog(IntPtr hwnd, bool show = true)
		{
			NativeInvoke(FMOD_DSP_ShowConfigDialog(this, hwnd, show));
		}

		protected bool GetParameterBool(int index)
		{
			NativeInvoke(FMOD_DSP_GetParameterBool(this, index, out var value, IntPtr.Zero, 0));
			return value;
		}

		protected byte[] GetParameterData(int index)
		{
			NativeInvoke(FMOD_DSP_GetParameterData(this, index, out var ptr, out var size, IntPtr.Zero, 0));
			var bytes = new byte[size];
			Marshal.Copy(ptr, bytes, 0, (int) size);
			return bytes;
		}

		protected float GetParameterFloat(int index)
		{
			NativeInvoke(FMOD_DSP_GetParameterFloat(this, index, out var value, IntPtr.Zero, 0));
			return value;
		}

		protected int GetParameterInt(int index)
		{
			NativeInvoke(FMOD_DSP_GetParameterInt(this, index, out var value, IntPtr.Zero, 0));
			return value;
		}

		protected void SetParameterBool(int index, bool value)
		{
			NativeInvoke(FMOD_DSP_SetParameterBool(this, index, value));
			ParameterChanged?.Invoke(this, new ParamChangeEventArgs
			{
				Index = index,
				ParameterType = DspParameterType.Bool,
				Value = value
			});
		}

		protected void SetParameterData(int index, byte[] data)
		{
			var gcHandle = GCHandle.Alloc(data, GCHandleType.Pinned);
			NativeInvoke(FMOD_DSP_SetParameterData(this, index, gcHandle.AddrOfPinnedObject(), (uint) data.Length));
			gcHandle.Free();
			ParameterChanged?.Invoke(this, new ParamChangeEventArgs
			{
				Index = index,
				ParameterType = DspParameterType.Data,
				Value = data
			});
		}

		protected void SetParameterFloat(int index, float value)
		{
			NativeInvoke(FMOD_DSP_SetParameterFloat(this, index, value));
			ParameterChanged?.Invoke(this, new ParamChangeEventArgs
			{
				Index = index,
				ParameterType = DspParameterType.Float,
				Value = value
			});
		}

		protected void SetParameterInt(int index, int value)
		{
			NativeInvoke(FMOD_DSP_SetParameterInt(this, index, value));
			ParameterChanged?.Invoke(this, new ParamChangeEventArgs
			{
				Index = index,
				ParameterType = DspParameterType.Int,
				Value = value
			});
		}

		#endregion

		public class ParamChangeEventArgs : EventArgs
		{
			#region Constructors & Destructor

			public ParamChangeEventArgs()
			{
			}

			public ParamChangeEventArgs(int index, DspParameterType type, object value)
			{
				Index = index;
				ParameterType = type;
				Value = value;
			}

			#endregion

			#region Properties & Indexers

			public int Index { get; set; }

			public DspParameterType ParameterType { get; set; }

			public object Value { get; set; }

			#endregion
		}
	}
}