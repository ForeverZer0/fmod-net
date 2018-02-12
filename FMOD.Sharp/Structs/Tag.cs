using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using FMOD.Sharp.Enums;
using System.Windows.Forms;

namespace FMOD.Sharp.Structs
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
