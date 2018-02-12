using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FMOD.Sharp.Enums
{
    [Flags]
    public enum InitFlags : uint
    {
        Normal                     = 0x00000000, 
        StreamFromUpdate         = 0x00000001, 
        MixFromUpdate            = 0x00000002, 
        ThreeDRightHanded            = 0x00000004, 
        ChannelLowpass            = 0x00000100, 
        ChannelDistanceFilter     = 0x00000200, 
        ProfileEnable             = 0x00010000, 
        Vol0BecomesVirtual       = 0x00020000, 
        GeometryUseClosest        = 0x00040000, 
        PreferDolbyDownMix       = 0x00080000, 
        ThreadUnsafe              = 0x00100000, 
        ProfileMeterAll          = 0x00200000, 
        DisableSrsHighpassFilter = 0x00400000  
    }
}																																																																																																																																																																																				  
