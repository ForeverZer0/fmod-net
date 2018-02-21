using System.Runtime.InteropServices;
using FMOD.NET.Structures;

namespace FMOD.NET.Data
{
	public class ReverbPreset
	{
		public int Index { get; }

		public string Name { get; set; }

		public float[] Values { get; set; }

		public ReverbProperties Properties
		{
			get
			{
				var pinnedData = GCHandle.Alloc(Values, GCHandleType.Pinned);
				var ptr = pinnedData.AddrOfPinnedObject();
				var properties = Marshal.PtrToStructure(ptr, typeof(ReverbProperties));
				pinnedData.Free();
				return (ReverbProperties) properties;
			}
		}

		public ReverbPreset(int index, string name, float[] values)
		{
			Index = index;
			Name = name;
			Values = values;
		}

		public static implicit operator ReverbProperties(ReverbPreset preset)
		{
			return preset.Properties;
		}
	}
}
