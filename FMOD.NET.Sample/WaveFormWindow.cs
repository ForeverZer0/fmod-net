using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FMOD.NET.Core;
using FMOD.NET.Enumerations;

namespace FMOD.NET.Sample
{
	
	public partial class WaveFormWindow : Form
	{
		public const string BAD_AT_LOVE = @"C:\Users\syles\OneDrive\Documents\visual studio 2017\Projects\fmod-sharp\FMOD.NET.TestApp\Test Files\11. Bad At Love.mp3";

		public const string EYES_CLOSED =
			@"C:\Users\syles\Desktop\BACKUPS\Halsey - hopeless fountain kingdom (Deluxe) (2017) [320]\03. Eyes Closed.mp3";

		public const string WISH_YOU_WERE_HERE =
			@"C:\Users\syles\Downloads\Pink Floyd  [1967-2014]\1988 - Delicate Sound Of Thunder\2 - 03 - Wish You Were Here.mp3";

		public const string WISH_32_FLOAT =
			@"C:\Users\syles\Downloads\Pink Floyd Wish You Were Here\04 Wish You Were Here.mp3";

		public const string SNUFF = @"C:\Users\syles\Downloads\SlipKnot\2008 - All Hope Is Gone [RRCY-29152_3]\11. Snuff.mp3";


		private float totalMs;
		private FmodSystem _system;
		private Sound _sound;
		private Channel _channel;

		public WaveFormWindow()
		{
			InitializeComponent();
		}

		protected override void OnClosing(CancelEventArgs e)
		{
			_sound?.Dispose();
			_channel?.Dispose();
			_system?.Dispose();
			base.OnClosing(e);
		}

		private void timer1_Tick(object sender, EventArgs e)
		{
			if (_channel == null || _channel.IsInvalid || !_channel.IsPlaying)
				return;
			var progress = (_channel.GetPosition() / totalMs);
			waveForm1.UpdateProgress(progress);
		}

		private void waveForm1_MouseClick(object sender, MouseEventArgs e)
		{
			var length = _sound.GetLength();
			var normalized = (float)e.X / waveForm1.ClientRectangle.Width;
			var position = length * normalized;
			_channel.SetPosition((uint)position);
			waveForm1.PositionCursor.Location = new Point(e.X - (waveForm1.PositionCursor.Width / 2), 0);
		}

		private void WaveFormWindow_Load(object sender, EventArgs e)
		{
			_system = Factory.CreateSystem();
			_system.Initialize(InitFlags.Normal);
			
			_sound = _system.CreateStream(SNUFF, Mode.LoopNormal);
			waveForm1.LoadSound(_sound);
			_channel = _system.PlaySound(_sound);
			totalMs = _sound.GetLength();
		}
	}
}
