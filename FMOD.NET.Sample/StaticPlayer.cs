using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FMOD.NET.Core;
using FMOD.NET.Enumerations;

namespace FMOD.NET.Sample
{
	public static class StaticPlayer
	{
		public static FmodSystem System { get; }

		public static Channel Channel
		{
			get => System.MasterChannelGroup[0];
		}

		public static Sound Sound
		{
			get => System.MasterChannelGroup[0].CurrentSound;
		}

		static StaticPlayer()
		{
			System = Factory.CreateSystem();
			System.Initialize();
		}



		public static void Play(string filename)
		{
			var sound = System.CreateSound(filename, Mode.LoopNormal);
			System.PlaySound(sound);
		}


	}
}
