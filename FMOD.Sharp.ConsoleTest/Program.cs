using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using FMOD.Sharp.Dsps;
using FMOD.Sharp.Enums;
using FMOD.Sharp.Structs;

namespace FMOD.Sharp.ConsoleTest
{
	class Program
	{
		public const string BAD_AT_LOVE = @"C:\Users\syles\OneDrive\Documents\visual studio 2017\Projects\fmod-sharp\FMOD.Sharp.TestApp\Test Files\11. Bad At Love.mp3";

		static void Main(string[] args)
		{



			using (var system = FmodSystem.Create())
			{
				system.Initialize(InitFlags.Normal);
				using (var sound = system.CreateSound(BAD_AT_LOVE, Mode.CreateSample))
				{
					using (var channel = system.PlaySound(sound))
					{





						foreach (var tag in sound.GetAllTags())
							Console.WriteLine(tag.ToPrettyString());
					}
					//Console.WriteLine("\r\nPress <Enter> to exit.");

					const uint chunk = 64;

					var offset = 0u;
					start:
					using (var soundLock = sound.LockBuffer(offset, chunk))
					{
						
						var s1 = soundLock.ReadInt16();
						var s2 = soundLock.ReadInt16TEST();

						Console.WriteLine("\r\nFIRST");
						Console.Write(String.Join(",", s1));
						Console.WriteLine("\r\nSECOND");
						Console.Write(String.Join(",", s2));
						Console.Write(s1 == s2);


					}
					offset += chunk;
					Console.ReadLine();
					goto start;

					

					Console.ReadLine();
					Test(sound);
					Console.ReadLine();
				}
			}
		}


		private static void Test(Sound sound)
		{
			const uint CHUNK_SIZE = 1024;

			var length = sound.GetLength(TimeUnit.PcmBytes); 

			var buffer = new short[CHUNK_SIZE / 2];








//			for (uint i = 0; i < length; i += CHUNK_SIZE)
//			{
//				
//				var result = Sound.FMOD_Sound_Lock(sound, i * CHUNK_SIZE, CHUNK_SIZE, out var ptr1, out var ptr2, out var len1, out var len2);
//				
//				if (result != Result.OK || ptr1 == IntPtr.Zero)
//				{
//					Console.ReadLine();
//				}
//				Console.Write(String.Join(",", buffer));
//
//				Marshal.Copy(ptr1, buffer, 0, Convert.ToInt32(CHUNK_SIZE / 2));
//				Sound.FMOD_Sound_Unlock(sound, ptr1, ptr2, len1, len2);
//			}





			// THIS SHIT BE WORKING
//			const uint CHUNK_SIZE = 1024;
//
//			var length = sound.GetLength(TimeUnit.PcmBytes); 
//
//			var buffer = new short[CHUNK_SIZE / 2];
//
//
//			for (uint i = 0; i < length; i += CHUNK_SIZE)
//			{
//				
//				var result = Sound.FMOD_Sound_Lock(sound, i * CHUNK_SIZE, CHUNK_SIZE, out var ptr1, out var ptr2, out var len1, out var len2);
//				
//				if (result != Result.OK || ptr1 == IntPtr.Zero)
//				{
//					Console.ReadLine();
//				}
//				Console.Write(String.Join(",", buffer));
//
//				Marshal.Copy(ptr1, buffer, 0, Convert.ToInt32(CHUNK_SIZE / 4));
//				Sound.FMOD_Sound_Unlock(sound, ptr1, ptr2, len1, len2);
//			}





			Console.WriteLine("DONE");





		}
	}
}
