using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace FMOD.Sharp
{
	using Structs;

	public static class ReverbPresets
	{
		private static readonly Dictionary<int, float[]> _values = new Dictionary<int, float[]>
		{
			{ 0, new[] { 1000.0f, 7.0f, 11.0f, 5000.0f, 100.0f, 100.0f, 100.0f, 250.0f, 0.0f, 20.0f, 96.0f, -80.0f } },
			{ 1, new[] { 1500.0f, 7.0f, 11.0f, 5000.0f, 83.0f, 100.0f, 100.0f, 250.0f, 0.0f, 14500.0f, 96.0f, -8.0f } },
			{ 2, new[] { 170.0f, 1.0f, 2.0f, 5000.0f, 10.0f, 100.0f, 100.0f, 250.0f, 0.0f, 160.0f, 84.0f, -7.8f } },
			{ 3, new[] { 400.0f, 2.0f, 3.0f, 5000.0f, 83.0f, 100.0f, 100.0f, 250.0f, 0.0f, 6050.0f, 88.0f, -9.4f } },
			{ 4, new[] { 1500.0f, 7.0f, 11.0f, 5000.0f, 54.0f, 100.0f, 60.0f, 250.0f, 0.0f, 2900.0f, 83.0f, 0.5f } },
			{ 5, new[] { 500.0f, 3.0f, 4.0f, 5000.0f, 10.0f, 100.0f, 100.0f, 250.0f, 0.0f, 160.0f, 58.0f, -19.0f } },
			{ 6, new[] { 2300.0f, 12.0f, 17.0f, 5000.0f, 64.0f, 100.0f, 100.0f, 250.0f, 0.0f, 7800.0f, 71.0f, -8.5f } },
			{ 7, new[] { 4300.0f, 20.0f, 30.0f, 5000.0f, 59.0f, 100.0f, 100.0f, 250.0f, 0.0f, 5850.0f, 64.0f, -11.7f } },
			{ 8, new[] { 3900.0f, 20.0f, 29.0f, 5000.0f, 70.0f, 100.0f, 100.0f, 250.0f, 0.0f, 5650.0f, 80.0f, -9.8f } },
			{ 9, new[] { 2900.0f, 15.0f, 22.0f, 5000.0f, 100.0f, 100.0f, 100.0f, 250.0f, 0.0f, 20000.0f, 59.0f, -11.3f } },
			{ 10, new[] { 7200.0f, 20.0f, 30.0f, 5000.0f, 33.0f, 100.0f, 100.0f, 250.0f, 0.0f, 4500.0f, 80.0f, -9.6f } },
			{ 11, new[] { 10000.0f, 20.0f, 30.0f, 5000.0f, 23.0f, 100.0f, 100.0f, 250.0f, 0.0f, 3400.0f, 72.0f, -7.4f } },
			{ 12, new[] { 300.0f, 2.0f, 30.0f, 5000.0f, 10.0f, 100.0f, 100.0f, 250.0f, 0.0f, 500.0f, 56.0f, -24.0f } },
			{ 13, new[] { 1500.0f, 7.0f, 11.0f, 5000.0f, 59.0f, 100.0f, 100.0f, 250.0f, 0.0f, 7800.0f, 87.0f, -5.5f } },
			{ 14, new[] { 270.0f, 13.0f, 20.0f, 5000.0f, 79.0f, 100.0f, 100.0f, 250.0f, 0.0f, 9000.0f, 86.0f, -6.0f } },
			{ 15, new[] { 1500.0f, 7.0f, 11.0f, 5000.0f, 86.0f, 100.0f, 100.0f, 250.0f, 0.0f, 8300.0f, 80.0f, -9.8f } },
			{ 16, new[] { 1500.0f, 162.0f, 88.0f, 5000.0f, 54.0f, 79.0f, 100.0f, 250.0f, 0.0f, 760.0f, 94.0f, -12.3f } },
			{ 17, new[] { 1500.0f, 7.0f, 11.0f, 5000.0f, 67.0f, 50.0f, 100.0f, 250.0f, 0.0f, 4050.0f, 66.0f, -26.0f } },
			{ 18, new[] { 1500.0f, 300.0f, 100.0f, 5000.0f, 21.0f, 27.0f, 100.0f, 250.0f, 0.0f, 1220.0f, 82.0f, -24.0f } },
			{ 19, new[] { 1500.0f, 61.0f, 25.0f, 5000.0f, 83.0f, 100.0f, 100.0f, 250.0f, 0.0f, 3400.0f, 100.0f, -5.0f } },
			{ 20, new[] { 1500.0f, 179.0f, 100.0f, 5000.0f, 50.0f, 21.0f, 100.0f, 250.0f, 0.0f, 1670.0f, 65.0f, -28.0f } },
			{ 21, new[] { 1700.0f, 8.0f, 12.0f, 5000.0f, 100.0f, 100.0f, 100.0f, 250.0f, 0.0f, 20000.0f, 56.0f, -19.5f } },
			{ 22, new[] { 2800.0f, 14.0f, 21.0f, 5000.0f, 14.0f, 80.0f, 60.0f, 250.0f, 0.0f, 3400.0f, 66.0f, 1.2f } },
			{ 23, new[] { 1500.0f, 7.0f, 11.0f, 5000.0f, 10.0f, 100.0f, 100.0f, 250.0f, 0.0f, 500.0f, 92.0f, 7.0f } }
		};

		private static readonly Dictionary<string, int> _names = new Dictionary<string, int>
		{
			{ "Off", 0 }, { "Generic", 1}, { "Padded Cell", 2 }, { "Room" , 3 }, { "Bathroom", 4 },
			{ "Living Room", 5 }, { "Stone Room", 6 }, { "Auditorium", 7 }, { "Concert Hall", 8 },
			{ "Cave", 9 }, { "Arena", 10 }, { "Hangar", 11 }, { "Carpetted Hallway", 12 }, { "Hallway", 13 }, 
			{ "Stone Corridor", 14 }, { "Alley", 15 }, { "Forest", 16 }, { "City", 17 }, { "Mountains", 18 }, 
			{ "Quarry", 19 }, { "Plain", 20 }, { "Parking Lot", 21 }, { "Sewer Pipe", 22 }, { "Underwater", 23 }
		};

		public static ReverbProperties Off => FromIndex(0);

		public static ReverbProperties Generic => FromIndex(1);

		public static ReverbProperties PaddedCell => FromIndex(2);

		public static ReverbProperties Room => FromIndex(3);

		public static ReverbProperties Bathroom => FromIndex(4);

		public static ReverbProperties LivingRoom => FromIndex(5);

		public static ReverbProperties StoneRoom => FromIndex(6);

		public static ReverbProperties Auditorium => FromIndex(7);

		public static ReverbProperties ConcertHall => FromIndex(8);

		public static ReverbProperties Cave => FromIndex(9);

		public static ReverbProperties Arena => FromIndex(10);

		public static ReverbProperties Hangar => FromIndex(11);

		public static ReverbProperties CarpettedHallway => FromIndex(12);

		public static ReverbProperties Hallway => FromIndex(13);

		public static ReverbProperties StoneCorridor => FromIndex(14);

		public static ReverbProperties Alley => FromIndex(15);

		public static ReverbProperties Forest => FromIndex(16);

		public static ReverbProperties City => FromIndex(17);

		public static ReverbProperties Mountains => FromIndex(18);

		public static ReverbProperties Quarry => FromIndex(19);

		public static ReverbProperties Plain => FromIndex(20);

		public static ReverbProperties ParkingLot => FromIndex(21);

		public static ReverbProperties SewerPipe => FromIndex(22);

		public static ReverbProperties Underwater => FromIndex(23);

		public static IEnumerable<string> GetNames()
		{
			return _names.Keys;
		}

		public static IEnumerable<int> GetIndices()
		{
			return _values.Keys;
		}

		public static float[] ValuesOf(int index)
		{
			if (index < 0 || index >= _values.Count)
				return null;
			return _values[index];
		}

		public static float[] ValuesOf(string presetName)
		{
			var index = IndexFromName(presetName);
			return ValuesOf(index);
		}

		public static ReverbProperties FromName(string presetName)
		{
			var index = IndexFromName(presetName);
			return FromIndex(index);
		}

		public static ReverbProperties FromIndex(int index)
		{
			var values = _values[index];
			var pinnedData = GCHandle.Alloc(values, GCHandleType.Pinned);
			var ptr = pinnedData.AddrOfPinnedObject();
			var properties = Marshal.PtrToStructure(ptr, typeof(ReverbProperties));
			pinnedData.Free();
			return (ReverbProperties) properties;
		}

		private static int IndexFromName(string presetName)
		{
			if (_names.ContainsKey(presetName))
				return _names[presetName];
			foreach (var name in _names.Keys)
			{
				var normalized1 = Regex.Replace(name, @"\s", "");
				var normalized2 = Regex.Replace(presetName, @"\s", "");
				if (String.Equals(normalized1, normalized2, StringComparison.InvariantCultureIgnoreCase))
					return _names[name];
			}
			return 0;
		}
	}
}