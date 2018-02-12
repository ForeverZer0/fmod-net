using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using FMOD.Sharp.Enums;


namespace FMOD.Sharp.TestApp
{
	public partial class MainForm : Form
	{
		private const string BASEFOLDER = "Test Files";

		// TODO: Get all these...
		private static readonly string[] FORMATS = { "mp3", "wav", "ogg", "wma" };

		private List<FileInfo> _audioFiles;
		private System _system;
		private Reverb3D _reverb;
		private Channel _channel;
		private Sound _sound;

		public MainForm()
		{
			InitializeComponent();

			comboBoxReverb.DataBindings.Add("Enabled", checkBoxReverb, "Checked");
		
		}

		private void MainForm_Load(object sender, EventArgs e)
		{
			_system = System.Create();
			_system.Initialize(InitFlags.Normal);
			_audioFiles = new List<FileInfo>();
			var dir = new DirectoryInfo(BASEFOLDER);
			foreach (var ext in FORMATS)
				_audioFiles.AddRange(dir.GetFiles($"*.{ext}", SearchOption.TopDirectoryOnly));
			listBoxSource.Items.AddRange(_audioFiles.ToArray());
			listBoxSource.DisplayMember = "Name";
		}

		private void listBoxSource_DoubleClick(object sender, EventArgs e)
		{
			var fileInfo = (FileInfo) listBoxSource.SelectedItem;
			if (fileInfo == null)
				return;
	
			_sound = _system.CreateStream(fileInfo.FullName, Mode.Default | Mode.LoopNormal);
			_channel = _system.PlaySound(_sound);
		}

		private void checkBoxReverb_CheckedChanged(object sender, EventArgs e)
		{
			if (checkBoxReverb.Checked)
			{
	
			}
			else
			{
				
			}
		}

		private void comboBoxReverb_SelectedIndexChanged(object sender, EventArgs e)
		{
			var index = comboBoxReverb.SelectedIndex;
			var values = ReverbPresets.VALUES[index];
			var pinnedData = GCHandle.Alloc(values, GCHandleType.Pinned);
			var ptr = pinnedData.AddrOfPinnedObject();
			var properties = (ReverbProperties) Marshal.PtrToStructure(ptr, typeof(ReverbProperties));
			pinnedData.Free();
			_system.SetReverbProperties(1, properties);
		}
	}
}




																								
