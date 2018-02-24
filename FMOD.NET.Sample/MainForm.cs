using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using FMOD.NET.Controls;
using FMOD.NET.Core;
using FMOD.NET.DSP;
using FMOD.NET.Enumerations;

namespace FMOD.NET.Sample
{
	public partial class MainForm : Form
	{
		private const string MUSIC_FOLDER = "Music";

		private float _length;
		private bool _mouseDown;

		private DspSamplerPanel _dspPanel;
		private ReverbPanel _reverbPanel;
		private PitchShift _pitchShifter;

		public MainForm()
		{
			InitializeComponent();
			this.Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);
		}

		private void MainForm_Load(object sender, EventArgs e)
		{
			if (Directory.Exists(MUSIC_FOLDER))
			{
				var extensions = Constants.SUPPORTED_EXTENSIONS.ToList();
				foreach (var file in Directory.GetFiles(MUSIC_FOLDER))
				{
					var ext = Path.GetExtension(file);
					if (extensions.Contains(ext))
						comboBoxFile.Items.Add(file);
				}
			}
			waveFormBox.MouseDown += (s, ev) => _mouseDown = true;
			waveFormBox.MouseUp += (s, ev) => _mouseDown = false;
			toolTip.SetToolTip(sliderVolume, $"Volume:\n{sliderVolume.Value}%");
			toolTip.SetToolTip(sliderFrequency, $"Frequency:\n{sliderFrequency.Value} Hz");
			new Thread(() => Invoke(new Action(CreatePanels))).Start();
			_pitchShifter = StaticPlayer.System.CreateDspByType<PitchShift>();
			StaticPlayer.System.MasterChannelGroup.AddDsp(_pitchShifter, DspIndex.Head);
		}

		private void CreatePanels()
		{
			_dspPanel = new DspSamplerPanel { Visible = false, Dock = DockStyle.Fill };
			_reverbPanel = new ReverbPanel { Visible = false, Dock = DockStyle.Fill };
			panelMain.Controls.Add(_dspPanel);
			panelMain.Controls.Add(_reverbPanel);
			radioPlayback.CheckedChanged += (s, e) => panelPlayback.Visible = radioPlayback.Checked;
			radioReverb.CheckedChanged += (s, e) => _reverbPanel.Visible = radioReverb.Checked;
			radioDsp.CheckedChanged += (s, e) => _dspPanel.Visible = radioDsp.Checked;
		}

		private void LoadSound(string filename)
		{
			StaticPlayer.Play(filename);
			_length = StaticPlayer.Channel.CurrentSound.GetLength();
			timerUpdate.Enabled = true;
			sliderFrequency.Value = (int) StaticPlayer.Channel.Frequency;
			StaticPlayer.Channel.Volume = sliderVolume.Value / 100.0f;
			waveFormBox.LoadSound(StaticPlayer.System, filename);
		}

		private void timerUpdate_Tick(object sender, EventArgs e)
		{
			if (_mouseDown)
			{
				var point = waveFormBox.PointToClient(MousePosition);
				point.X = point.X.Clamp(0, waveFormBox.ClientSize.Width - 1);
				SetPositionFromPoint(point);
			}	
			var position =  StaticPlayer.Channel.GetPosition();
			waveFormBox.UpdateProgress(position / _length);
		}

		private void SetPositionFromPoint(Point point)
		{
			var percent = point.X / (float) waveFormBox.ClientSize.Width;
			var position = (uint) (percent * _length);
			StaticPlayer.Channel.SetPosition(position);
			waveFormBox.UpdateProgress(percent);
		}

		private void waveFormBox_MouseClick(object sender, MouseEventArgs e)
		{
			SetPositionFromPoint(e.Location);
		}

		private void buttonOpen_Click(object sender, EventArgs e)
		{
			using (var dlg = new OpenFileDialog())
			{
				dlg.Filter = Constants.FILE_FILTER;
				dlg.Multiselect = false;
				dlg.Title = @"Select audio file...";
				dlg.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyMusic);
				if (dlg.ShowDialog() == DialogResult.OK)
				{
					var filename = dlg.FileName;
					if (!comboBoxFile.Items.Contains(filename))
						comboBoxFile.Items.Add(filename);
					comboBoxFile.SelectedItem = filename;
					LoadSound(filename);
				}
			}
		}

		private void sliderVolume_Scroll(object sender, ScrollEventArgs e)
		{
			StaticPlayer.Channel.Volume = (e.NewValue / 100.0f);
			toolTip.SetToolTip(sliderVolume, $"Volume:\n{sliderVolume.Value}%");
		}

		private void sliderFrequency_Scroll(object sender, ScrollEventArgs e)
		{
			// TODO: Impelement locking controls when no sound is playing.
			if (e.NewValue > 0 && sliderFrequency.ColorSchema != ColorSlider.ColorSchemas.GreenColors)
				sliderFrequency.ColorSchema = ColorSlider.ColorSchemas.GreenColors;
			if (e.NewValue < 0 && sliderFrequency.ColorSchema != ColorSlider.ColorSchemas.RedColors)
				sliderFrequency.ColorSchema = ColorSlider.ColorSchemas.RedColors;
			StaticPlayer.Channel.Frequency = sliderFrequency.Value;
			toolTip.SetToolTip(sliderFrequency, $"Frequency:\n{sliderFrequency.Value} Hz");
		}

		private void textBoxFile_TextChanged(object sender, EventArgs e)
		{
			toolTip.SetToolTip(comboBoxFile, $"Filename:\n\"{comboBoxFile.Text}\"");
		}

		private void buttonPlay_Click(object sender, EventArgs e)
		{
			if (StaticPlayer.Channel.Paused)
				StaticPlayer.Channel.Resume();
			else
			{
				try
				{
					LoadSound(comboBoxFile.Text);
				}
				catch (FmodException)
				{
					MessageBox.Show(@"Select a file to load.", @"Select Audio File");
				}
			}
		}

		private void buttonPause_Click(object sender, EventArgs e)
		{
			StaticPlayer.Channel.Paused = !StaticPlayer.Channel.Paused;
		}

		private void buttonStop_Click(object sender, EventArgs e)
		{
			StaticPlayer.Channel.Stop();
		}

		private void sliderTempo_Scroll(object sender, ScrollEventArgs e)
		{
			if (e.NewValue > 100 && sliderTempo.ColorSchema != ColorSlider.ColorSchemas.GreenColors)
				sliderTempo.ColorSchema = ColorSlider.ColorSchemas.GreenColors;
			else if (e.NewValue < 100 && sliderTempo.ColorSchema != ColorSlider.ColorSchemas.RedColors)
				sliderTempo.ColorSchema = ColorSlider.ColorSchemas.RedColors;
			else if (e.NewValue == 100)
				sliderTempo.ColorSchema = ColorSlider.ColorSchemas.BlueColors;
			var tempo = sliderTempo.Value / 100.0f;
			StaticPlayer.Channel.Pitch = tempo;
			_pitchShifter.Pitch = 1.0f / tempo;
			toolTip.SetToolTip(sliderTempo, $"Tempo:\n{sliderTempo.Value}%");
		}

		private void comboBoxFile_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar != (char) Keys.Return)
				return;
			LoadSound(comboBoxFile.Text);
			e.Handled = true;
		}
	}
}