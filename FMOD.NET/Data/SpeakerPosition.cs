using System.Drawing;
using FMOD.NET.Enumerations;

namespace FMOD.NET.Data
{
	public class SpeakerPosition
	{
		public Speaker Speaker { get; }

		public PointF Location { get; set; }

		public bool IsActive { get; set; }

		public SpeakerPosition(Speaker speaker, PointF point, bool isActive = true)
		{
			this.Speaker = speaker;
			Location = point;
			IsActive = isActive;
		}

		public SpeakerPosition(Speaker speaker, float x, float y, bool isActive = true) : 
			this(speaker, new PointF(x, y), isActive)
		{
		
		}
	}
}
