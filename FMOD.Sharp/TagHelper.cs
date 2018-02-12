using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text;
using FMOD.Sharp.Enums;
using FMOD.Sharp.Structs;

namespace FMOD.Sharp
{
	public static class TagHelper
	{
		public static readonly string[] GENRES =
		{
			"Blues",
			"Classic Rock",
			"Country",
			"Dance",
			"Disco",
			"Funk",
			"Grunge",
			"Hip-Hop",
			"Jazz",
			"Metal",
			"New Age",
			"Oldies",
			"Other",
			"Pop",
			"R&B",
			"Rap",
			"Reggae",
			"Rock",
			"Techno",
			"Industrial",
			"Alternative",
			"Ska",
			"Death Metal",
			"Pranks",
			"Soundtrack",
			"Euro-Techno",
			"Ambient",
			"Trip-Hop",
			"Vocal",
			"Jazz+Funk",
			"Fusion",
			"Trance",
			"Classical",
			"Instrumental",
			"Acid",
			"House",
			"Game",
			"Sound Clip",
			"Gospel",
			"Noise",
			"Alt. Rock",
			"Bass",
			"Soul",
			"Punk",
			"Space",
			"Meditative",
			"Instrumental Pop",
			"Instrumental Rock",
			"Ethnic",
			"Gothic",
			"Darkwave",
			"Techno-Industrial",
			"Electronic",
			"Pop-Folk",
			"Eurodance",
			"Dream",
			"Southern Rock",
			"Comedy",
			"Cult",
			"Gangsta Rap",
			"Top 40",
			"Christian Rap",
			"Pop/Funk",
			"Jungle",
			"Native American",
			"Cabaret",
			"New Wave",
			"Psychedelic",
			"Rave",
			"Showtunes",
			"Trailer",
			"Lo-Fi",
			"Tribal",
			"Acid Punk",
			"Acid Jazz",
			"Polka",
			"Retro",
			"Musical",
			"Rock & Roll",
			"Hard Rock",
			"Folk",
			"Folk/Rock",
			"National Folk",
			"Swing",
			"Fast-Fusion",
			"Bebob",
			"Latin",
			"Revival",
			"Celtic",
			"Bluegrass",
			"Avantgarde",
			"Gothic Rock",
			"Progressive Rock",
			"Psychedelic Rock",
			"Symphonic Rock",
			"Slow Rock",
			"Big Band",
			"Chorus",
			"Easy Listening",
			"Acoustic",
			"Humour",
			"Speech",
			"Chanson",
			"Opera",
			"Chamber Music",
			"Sonata",
			"Symphony",
			"Booty Bass",
			"Primus",
			"Porn Groove",
			"Satire",
			"Slow Jam",
			"Club",
			"Tango",
			"Samba",
			"Folklore",
			"Ballad",
			"Power Ballad",
			"Rhythmic Soul",
			"Freestyle",
			"Duet",
			"Punk Rock",
			"Drum Solo",
			"A Cappella",
			"Euro-House",
			"Dance Hall",
			"Goa",
			"Drum & Bass",
			"Club-House",
			"Hardcore",
			"Terror",
			"Indie",
			"BritPop",
			"Negerpunk",
			"Polsk Punk",
			"Beat",
			"Christian Gangsta Rap",
			"Heavy Metal",
			"Black Metal",
			"Crossover",
			"Contemporary Christian",
			"Christian Rock",
			"Merengue",
			"Salsa",
			"Thrash Metal",
			"Anime",
			"Jpop",
			"Synthpop"
		};

		public static string ToTitleCase(string str)
		{
			var info = CultureInfo.CurrentCulture.TextInfo;
			return info.ToTitleCase(str.ToLower());
		}

		public static object GetValue(Tag tag)
		{
			var rawData = new byte[tag.DataLength];
			Marshal.Copy(tag.Data, rawData, 0, rawData.Length);
			switch (tag.DataType)
			{
				case TagDataType.Binary:
					return Encoding.Default.GetString(rawData).Trim('\0');
				case TagDataType.Int:
					return BitConverter.ToInt32(rawData, 0);
				case TagDataType.Float:
					return BitConverter.ToSingle(rawData, 0);
				case TagDataType.String:
					return Encoding.Default.GetString(rawData).Trim('\0');
				case TagDataType.StringUtf16:
					return Encoding.Unicode.GetString(rawData).Trim('\0');
				case TagDataType.StringUtf16Be:
					return Encoding.BigEndianUnicode.GetString(rawData).Trim('\0');
				case TagDataType.StringUtf8:
					return Encoding.UTF8.GetString(rawData).Trim('\0');
				case TagDataType.Cdtoc:
					// TODO: Implement
					return null;
				case TagDataType.Max:
					// TODO: Implement
					return null;
				default:
					return null;
			}
		}

		public static string GetGenreString(int id3V1Genre)
		{
			if (id3V1Genre < 0 || id3V1Genre >= GENRES.Length)
				return "Unknown";
			return GENRES[id3V1Genre];
		}
	}
}
