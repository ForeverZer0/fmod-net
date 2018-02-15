using System;
using FMOD.Sharp.Enums;
using FMOD.Sharp.Structs;

namespace FMOD.Sharp
{
	public delegate Result AsyncReadInfoDoneCallback(IntPtr info, Result result);

	public delegate Result DebugCallback(DebugFlags flags, string file, int line, string func, string message);

	public delegate Result SystemCallback(IntPtr systemraw, SystemCallbackType type, IntPtr commanddata1,
		IntPtr commanddata2, IntPtr userdata);

	public delegate Result ChannelCallback(IntPtr channelraw, ChannelControlType controltype,
		ChannelControlCallbackType type, IntPtr commanddata1, IntPtr commanddata2);

	public delegate Result SoundNonBlockCallback(IntPtr soundraw, Result Result);

	public delegate Result SoundPcmReadCallback(IntPtr soundraw, IntPtr data, uint datalen);

	public delegate Result SoundPcmSetPosCallback(IntPtr soundraw, int subsound, uint position, TimeUnit postype);

	public delegate Result FileOpenCallback(StringWrapper name, ref uint filesize, ref IntPtr handle, IntPtr userdata);

	public delegate Result FileCloseCallback(IntPtr handle, IntPtr userdata);

	public delegate Result FileReadCallback(IntPtr handle, IntPtr buffer, uint sizebytes, ref uint bytesread,
		IntPtr userdata);

	public delegate Result FileSeekCallback(IntPtr handle, uint pos, IntPtr userdata);

	public delegate Result FileAsyncReadCallback(IntPtr handle, IntPtr info, IntPtr userdata);

	public delegate Result FileAsyncCancelCallback(IntPtr handle, IntPtr userdata);

	public delegate float Cb_3DRolloffcallback(IntPtr channelraw, float distance);

	public delegate Result DspCreateCallback(ref DspState dspState);

	public delegate Result DspGetParamBoolCallback(ref DspState dspState, int index, ref bool value, IntPtr valueStr);

	public delegate Result DspGetParamDataCallback(ref DspState dspState, int index, ref IntPtr data, ref uint length,
		IntPtr valuesStr);

	public delegate Result DspGetParamFloatCallback(ref DspState dspState, int index, ref float value, IntPtr valueStr);

	public delegate Result DspGetParamIntCallback(ref DspState dspState, int index, ref int value, IntPtr valueStr);

	public delegate Result DspProcessCallback(ref DspState dspState, uint length, ref DspBufferArray inBufferArray,
		ref DspBufferArray outBufferArray, bool inputsIdle, DspProcessOperation op);

	public delegate Result DspReadCallback(ref DspState dspState, IntPtr inBuffer, IntPtr outBuffer, uint length,
		int inChannels, ref int outChannels);

	public delegate Result DspReleaseCallback(ref DspState dspState);

	public delegate Result DspResetCallback(ref DspState dspState);

	public delegate Result DspSetParamBoolCallback(ref DspState dspState, int index, bool value);

	public delegate Result DspSetParamDataCallback(ref DspState dspState, int index, IntPtr data, uint length);

	public delegate Result DspSetParamFloatCallback(ref DspState dspState, int index, float value);

	public delegate Result DspSetParamIntCallback(ref DspState dspState, int index, int value);

	public delegate Result DspSetPositionCallback(ref DspState dspState, uint pos);

	public delegate Result DspShouldIProcessCallback(ref DspState dspState, bool inputsIdle, uint length,
		ChannelMask inMask, int inChannels, SpeakerMode speakerMode);

	public delegate Result DspSystemDeregisterCallback(ref DspState dspState);

	public delegate Result DspSystemMixCallback(ref DspState dspState, int stage);

	public delegate Result DspSystemRegisterCallback(ref DspState dspState);
}