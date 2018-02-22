using System;
using System.Runtime.InteropServices;
using FMOD.NET.Enumerations;

namespace FMOD.NET.Structures
{
	[StructLayout(LayoutKind.Sequential)]
	public struct CodecWaveFormat
	{
		public string Name;
		public SoundFormat Format;
		public int Channels;
		public int Frequency;
		public uint LengthBytes;
		public uint LengthPcm;
		public uint PcmBlockSize;
		public int LoopStart;
		public int LoopEnd;
		public Mode Mode;
		public ChannelMask ChannelMask;
		public ChannelOrder ChannelOrder;
		public float PeakVolume;
	}



	[StructLayout(LayoutKind.Sequential)]
	public struct CodecState
	{
		private int SoundCount;
		public CodecWaveFormat WaveFormat;
		public IntPtr PluginData;
		public IntPtr FileHandle;
		public uint FileSize;
//		FMOD_FILE_READ_CALLBACK fileread;
//		FMOD_FILE_SEEK_CALLBACK fileseek;
//		FMOD_CODEC_METADATA_CALLBACK metadata;
		public int WaveFormatVersion;
	}


	public struct CodecDescription
	{
		// TODO: Implement




		/*
  const char *name;
  unsigned int version;
  int defaultasstream;
  FMOD_TIMEUNIT timeunits;
  FMOD_CODEC_OPEN_CALLBACK open;
  FMOD_CODEC_CLOSE_CALLBACK close;
  FMOD_CODEC_READ_CALLBACK read;
  FMOD_CODEC_GETLENGTH_CALLBACK getlength;
  FMOD_CODEC_SETPOSITION_CALLBACK setposition;
  FMOD_CODEC_GETPOSITION_CALLBACK getposition;
  FMOD_CODEC_SOUNDCREATE_CALLBACK soundcreate;
  FMOD_CODEC_GETWAVEFORMAT_CALLBACK getwaveformat;

		 */
	}
}
