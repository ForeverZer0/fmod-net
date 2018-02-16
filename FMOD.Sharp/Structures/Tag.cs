using System;
using System.Runtime.InteropServices;
using FMOD.Core;
using FMOD.Enumerations;

namespace FMOD.Structures
{
	[StructLayout(LayoutKind.Sequential)]
    public struct Tag
    {
        public  TagType           Type;        
        public  TagDataType       DataType;    
        private IntPtr            nameInternal;
        public  IntPtr            Data;        
        public  uint              DataLength;  
        public  bool              Updated;     																																																															   

        public string Name { get { return Marshal.PtrToStringAnsi(nameInternal); } }

	    public string ToPrettyString()
	    {
		    var value = TagHelper.GetValue(this);
		    var name = TagHelper.ToTitleCase(Name);
		    if (value is String)
			    return $"[{Type}] {name}: \"{TagHelper.ToTitleCase(value.ToString())}\"";
		    return $"[{Type}] {name}: {value}";
	    }

	    public override string ToString()
	    {
		    var value = TagHelper.GetValue(this);
			if (value is String)
				return $"[{Type}] {Name}: \"{value}\"";
		    return $"[{Type}] {Name}: {value}";
	    }
	}
}
