using System;
using System.Runtime.InteropServices;
using FMOD.NET.Core;
using FMOD.NET.Enumerations;

namespace FMOD.NET.Structures
{
	[StructLayout(LayoutKind.Sequential)]
    public struct CreateSoundExInfo
    {
        public int                         CbSize;                 /* [w]   Size of this structure.  This is used so the structure can be expanded in the future and still work on older versions of FMOD Ex. */
        public uint                        Length;                 /* [w]   Optional. Specify 0 to ignore. Size in bytes of file to load, or sound to create (in this case only if FMOD_OPENUSER is used).  Required if loading from memory.  If 0 is specified, then it will use the size of the file (unless loading from memory then an error will be returned). */
        public uint                        FileOffset;             /* [w]   Optional. Specify 0 to ignore. Offset from start of the file to start loading from.  This is useful for loading files from inside big data files. */
        public int                         ChannelCount;            /* [w]   Optional. Specify 0 to ignore. Number of channels in a sound specified only if OPENUSER is used. */
        public int                         DefaultFrequency;       /* [w]   Optional. Specify 0 to ignore. Default frequency of sound in a sound specified only if OPENUSER is used.  Other formats use the frequency determined by the file format. */
        public SoundFormat                Format;                 /* [w]   Optional. Specify 0 or SOUND_FORMAT_NONE to ignore. Format of the sound specified only if OPENUSER is used.  Other formats use the format determined by the file format.   */
        public uint                        DecodeBufferSize;       /* [w]   Optional. Specify 0 to ignore. For streams.  This determines the size of the double buffer (in PCM samples) that a stream uses.  Use this for user created streams if you want to determine the size of the callback buffer passed to you.  Specify 0 to use FMOD's default size which is currently equivalent to 400ms of the sound format created/loaded. */
        public int                         InitialSubsound;        /* [w]   Optional. Specify 0 to ignore. In a multi-sample file format such as .FSB/.DLS/.SF2, specify the initial subsound to seek to, only if CREATESTREAM is used. */
        public int                         SubsoundCount;           /* [w]   Optional. Specify 0 to ignore or have no subsounds.  In a user created multi-sample sound, specify the number of subsounds within the sound that are accessable with Sound::getSubSound / SoundGetSubSound. */
        public IntPtr                      InclusionList;          /* [w]   Optional. Specify 0 to ignore. In a multi-sample format such as .FSB/.DLS/.SF2 it may be desirable to specify only a subset of sounds to be loaded out of the whole file.  This is an array of subsound indicies to load into memory when created. */
        public int                         InclusionListCount;       /* [w]   Optional. Specify 0 to ignore. This is the number of integers contained within the */
        public SoundPcmReadCallback       PcmReadCallback;        /* [w]   Optional. Specify 0 to ignore. Callback to 'piggyback' on FMOD's read functions and accept or even write PCM data while FMOD is opening the sound.  Used for user sounds created with OPENUSER or for capturing decoded data as FMOD reads it. */
        public SoundPcmSetPosCallback     PcmSetPositionCallback;      /* [w]   Optional. Specify 0 to ignore. Callback for when the user calls a seeking function such as Channel::setPosition within a multi-sample sound, and for when it is opened.*/
        public SoundNonBlockCallback      NonBlockCallback;       /* [w]   Optional. Specify 0 to ignore. Callback for successful completion, or error while loading a sound that used the FMOD_NONBLOCKING flag.*/
        public IntPtr                      DlsName;                /* [w]   Optional. Specify 0 to ignore. Filename for a DLS or SF2 sample set when loading a MIDI file.   If not specified, on windows it will attempt to open /windows/system32/drivers/gm.dls, otherwise the MIDI will fail to open.  */
        public IntPtr                      EncryptionKey;          /* [w]   Optional. Specify 0 to ignore. Key for encrypted FSB file.  Without this key an encrypted FSB file will not load. */
        public int                         MaxPolyphony;           /* [w]   Optional. Specify 0 to ingore. For sequenced formats with dynamic channel allocation such as .MID and .IT, this specifies the maximum voice count allowed while playing.  .IT defaults to 64.  .MID defaults to 32. */
        public IntPtr                      UserData;               /* [w]   Optional. Specify 0 to ignore. This is user data to be attached to the sound during creation.  Access via Sound::getUserData. */
        public SoundType                  SuggestedSoundType;     /* [w]   Optional. Specify 0 or FMOD_SOUND_TYPE_UNKNOWN to ignore.  Instead of scanning all codec types, use this to speed up loading by making it jump straight to this codec. */
        public FileOpenCallback           FileUserOpenCallback;           /* [w]   Optional. Specify 0 to ignore. Callback for opening this file. */
        public FileCloseCallback          FileUserCloseCallback;          /* [w]   Optional. Specify 0 to ignore. Callback for closing this file. */
        public FileReadCallback           FileUserReadCallback;           /* [w]   Optional. Specify 0 to ignore. Callback for reading from this file. */
        public FileSeekCallback           FileUserSeekCallback;           /* [w]   Optional. Specify 0 to ignore. Callback for seeking within this file. */
        public FileAsyncReadCallback      FileUserAsynReadCallback;      /* [w]   Optional. Specify 0 to ignore. Callback for asyncronously reading from this file. */
        public FileAsyncCancelCallback    FileUserAsyncCancelCallback;    /* [w]   Optional. Specify 0 to ignore. Callback for cancelling an asyncronous read. */
        public IntPtr                      FileUserData;           /* [w]   Optional. Specify 0 to ignore. User data to be passed into the file callbacks. */
        public int                         FileBufferSize;         /* [w]   Optional. Specify 0 to ignore. Buffer size for reading the file, -1 to disable buffering, or 0 for system default. */
        public ChannelOrder                ChannelOrder;           /* [w]   Optional. Specify 0 to ignore. Use this to differ the way fmod maps multichannel sounds to speakers.  See FMOD_CHANNELORDER for more. */
        public ChannelMask                 ChannelMask;            /* [w]   Optional. Specify 0 to ignore. Use this to differ the way fmod maps multichannel sounds to speakers.  See FMOD_ChannelMask for more. */
        public IntPtr                      InitialSoundGroup;      /* [w]   Optional. Specify 0 to ignore. Specify a sound group if required, to put sound in as it is created. */
        public uint                        InitialSeekPosition;    /* [w]   Optional. Specify 0 to ignore. For streams. Specify an initial position to seek the stream to. */
        public TimeUnit                    InitialSeekPositionType;     /* [w]   Optional. Specify 0 to ignore. For streams. Specify the time unit for the position set in initialseekposition. */
        public int                         IgnoreSetFileSystem;    /* [w]   Optional. Specify 0 to ignore. Set to 1 to use fmod's built in file system. Ignores setFileSystem callbacks and also FMOD_CREATESOUNEXINFO file callbacks.  Useful for specific cases where you don't want to use your own file system but want to use fmod's file system (ie net streaming). */
        public uint                        AudioQueuePolicy;       /* [w]   Optional. Specify 0 or FMOD_AUDIOQUEUE_CODECPOLICY_DEFAULT to ignore. Policy used to determine whether hardware or software is used for decoding, see FMOD_AUDIOQUEUE_CODECPOLICY for options (iOS >= 3.0 required, otherwise only hardware is available) */
        public uint                        MinMidiGranularity;     /* [w]   Optional. Specify 0 to ignore. Allows you to set a minimum desired MIDI mixer granularity. Values smaller than 512 give greater than default accuracy at the cost of more CPU and vise versa. Specify 0 for default (512 samples). */
        public int                         NonBlockThreadId;       /* [w]   Optional. Specify 0 to ignore. Specifies a thread index to execute non blocking load on.  Allows for up to 5 threads to be used for loading at once.  This is to avoid one load blocking another.  Maximum value = 4. */
        public IntPtr                      FsbGuid;                /* [r/w] Optional. Specify 0 to ignore. Allows you to provide the GUID lookup for cached FSB header info. Once loaded the GUID will be written back to the pointer. This is to avoid seeking and reading the FSB header. */
    }
}
