using System;

namespace FMOD.Enumerations
{
	[Flags]
    public enum ChannelMask : uint
    {
        FrontLeft             = 0x00000001,
        FrontRight            = 0x00000002,
        FrontCenter           = 0x00000004,
        LowFrequency          = 0x00000008,
        SurroundLeft          = 0x00000010,
        SurroundRight         = 0x00000020,
        BackLeft              = 0x00000040,
        BackRight             = 0x00000080,
        BackCenter            = 0x00000100,

        Mono                   = (FrontLeft),
        Stereo                 = (FrontLeft | FrontRight),
        Lrc                    = (FrontLeft | FrontRight | FrontCenter),
        Quad                   = (FrontLeft | FrontRight | SurroundLeft | SurroundRight),
        Surround               = (FrontLeft | FrontRight | FrontCenter | SurroundLeft | SurroundRight),
        FivePointOne               = (FrontLeft | FrontRight | FrontCenter | LowFrequency | SurroundLeft | SurroundRight),
        FivePointOneRears         = (FrontLeft | FrontRight | FrontCenter | LowFrequency | BackLeft | BackRight),
        SevenPointZero               = (FrontLeft | FrontRight | FrontCenter | SurroundLeft | SurroundRight | BackLeft | BackRight),
        SevenPointOne               = (FrontLeft | FrontRight | FrontCenter | LowFrequency | SurroundLeft | SurroundRight | BackLeft | BackRight)
    }


}
