using System;
using System.Linq;
using FMOD.Data;
using FMOD.Structures;

namespace FMOD.Core
{
	public static class ReverbPresets
	{
		private static readonly ReverbPreset[] _presets = 
		{
			new ReverbPreset(0, "Off", new[] { 1000.0f, 7.0f, 11.0f, 5000.0f, 100.0f, 100.0f, 100.0f, 250.0f, 0.0f, 20.0f, 96.0f, -80.0f }),
			new ReverbPreset(1, "Generic",  new[] { 1500.0f, 7.0f, 11.0f, 5000.0f, 83.0f, 100.0f, 100.0f, 250.0f, 0.0f, 14500.0f, 96.0f, -8.0f }),
			new ReverbPreset(2, "Padded Cell",  new[] { 170.0f, 1.0f, 2.0f, 5000.0f, 10.0f, 100.0f, 100.0f, 250.0f, 0.0f, 160.0f, 84.0f, -7.8f }),
			new ReverbPreset(3, "Room",  new[] { 400.0f, 2.0f, 3.0f, 5000.0f, 83.0f, 100.0f, 100.0f, 250.0f, 0.0f, 6050.0f, 88.0f, -9.4f }),
			new ReverbPreset(4, "Bathroom",  new[] { 1500.0f, 7.0f, 11.0f, 5000.0f, 54.0f, 100.0f, 60.0f, 250.0f, 0.0f, 2900.0f, 83.0f, 0.5f }),
			new ReverbPreset(5, "Living Room",  new[] { 500.0f, 3.0f, 4.0f, 5000.0f, 10.0f, 100.0f, 100.0f, 250.0f, 0.0f, 160.0f, 58.0f, -19.0f }),
			new ReverbPreset(6, "Stone Room",  new[] { 2300.0f, 12.0f, 17.0f, 5000.0f, 64.0f, 100.0f, 100.0f, 250.0f, 0.0f, 7800.0f, 71.0f, -8.5f }),
			new ReverbPreset(7, "Auditorium",  new[] { 4300.0f, 20.0f, 30.0f, 5000.0f, 59.0f, 100.0f, 100.0f, 250.0f, 0.0f, 5850.0f, 64.0f, -11.7f }),
			new ReverbPreset(8, "Concert Hall",  new[] { 3900.0f, 20.0f, 29.0f, 5000.0f, 70.0f, 100.0f, 100.0f, 250.0f, 0.0f, 5650.0f, 80.0f, -9.8f }),
			new ReverbPreset(9, "Cave",  new[] { 2900.0f, 15.0f, 22.0f, 5000.0f, 100.0f, 100.0f, 100.0f, 250.0f, 0.0f, 20000.0f, 59.0f, -11.3f }),
			new ReverbPreset(10, "Arena",  new[] { 7200.0f, 20.0f, 30.0f, 5000.0f, 33.0f, 100.0f, 100.0f, 250.0f, 0.0f, 4500.0f, 80.0f, -9.6f }),
			new ReverbPreset(11, "Hangar",  new[] { 10000.0f, 20.0f, 30.0f, 5000.0f, 23.0f, 100.0f, 100.0f, 250.0f, 0.0f, 3400.0f, 72.0f, -7.4f }),
			new ReverbPreset(12, "Carpetted Hallway",  new[]  { 300.0f, 2.0f, 30.0f, 5000.0f, 10.0f, 100.0f, 100.0f, 250.0f, 0.0f, 500.0f, 56.0f, -24.0f }),
			new ReverbPreset(13, "Hallway",  new[] { 1500.0f, 7.0f, 11.0f, 5000.0f, 59.0f, 100.0f, 100.0f, 250.0f, 0.0f, 7800.0f, 87.0f, -5.5f }),
			new ReverbPreset(14, "Stone Corridor",  new[] { 270.0f, 13.0f, 20.0f, 5000.0f, 79.0f, 100.0f, 100.0f, 250.0f, 0.0f, 9000.0f, 86.0f, -6.0f }),
			new ReverbPreset(15, "Alley",  new[] { 1500.0f, 7.0f, 11.0f, 5000.0f, 86.0f, 100.0f, 100.0f, 250.0f, 0.0f, 8300.0f, 80.0f, -9.8f }),
			new ReverbPreset(16, "Forest",  new[] { 1500.0f, 162.0f, 88.0f, 5000.0f, 54.0f, 79.0f, 100.0f, 250.0f, 0.0f, 760.0f, 94.0f, -12.3f }),
			new ReverbPreset(17, "City",  new[] { 1500.0f, 7.0f, 11.0f, 5000.0f, 67.0f, 50.0f, 100.0f, 250.0f, 0.0f, 4050.0f, 66.0f, -26.0f } ),
			new ReverbPreset(18, "Mountains",  new[] { 1500.0f, 300.0f, 100.0f, 5000.0f, 21.0f, 27.0f, 100.0f, 250.0f, 0.0f, 1220.0f, 82.0f, -24.0f }),
			new ReverbPreset(19, "Quarry",  new[] { 1500.0f, 61.0f, 25.0f, 5000.0f, 83.0f, 100.0f, 100.0f, 250.0f, 0.0f, 3400.0f, 100.0f, -5.0f }),
			new ReverbPreset(20, "Plain",  new[] { 1500.0f, 179.0f, 100.0f, 5000.0f, 50.0f, 21.0f, 100.0f, 250.0f, 0.0f, 1670.0f, 65.0f, -28.0f }),
			new ReverbPreset(21, "Parking Lot",  new[] { 1700.0f, 8.0f, 12.0f, 5000.0f, 100.0f, 100.0f, 100.0f, 250.0f, 0.0f, 20000.0f, 56.0f, -19.5f }),
			new ReverbPreset(22, "Sewer Pipe",  new[] { 2800.0f, 14.0f, 21.0f, 5000.0f, 14.0f, 80.0f, 60.0f, 250.0f, 0.0f, 3400.0f, 66.0f, 1.2f }),
			new ReverbPreset(23, "Underwater",  new[] { 1500.0f, 7.0f, 11.0f, 5000.0f, 10.0f, 100.0f, 100.0f, 250.0f, 0.0f, 500.0f, 92.0f, 7.0f })
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

		public static string[] GetNames()
		{
			var names = new string[_presets.Length];
			for (var i = 0; i < names.Length; i++)
				names[i] = _presets[i].Name;
			return names;
		}

		public static ReverbProperties FromName(string presetName)
		{
			return _presets.First(p => p.Name.Equals(presetName, StringComparison.InvariantCultureIgnoreCase));
		}

		public static ReverbProperties FromIndex(int index)
		{
			return _presets[index].Properties;
		}
	}
}