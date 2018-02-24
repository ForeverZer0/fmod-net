using FMOD.NET.Controls;

namespace FMOD.NET.Sample
{
	partial class MainForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			System.Drawing.Drawing2D.ColorBlend colorBlend3 = new System.Drawing.Drawing2D.ColorBlend();
			System.Drawing.Drawing2D.ColorBlend colorBlend4 = new System.Drawing.Drawing2D.ColorBlend();
			this.groupBoxTempo = new System.Windows.Forms.GroupBox();
			this.labelTempoDouble = new System.Windows.Forms.Label();
			this.labelTempoHalf = new System.Windows.Forms.Label();
			this.sliderTempo = new FMOD.NET.Controls.ColorSlider();
			this.labelTempo = new System.Windows.Forms.Label();
			this.buttonOpen = new System.Windows.Forms.Button();
			this.timerUpdate = new System.Windows.Forms.Timer(this.components);
			this.toolTip = new System.Windows.Forms.ToolTip(this.components);
			this.buttonPlay = new System.Windows.Forms.Button();
			this.buttonPause = new System.Windows.Forms.Button();
			this.buttonStop = new System.Windows.Forms.Button();
			this.groupBoxFile = new System.Windows.Forms.GroupBox();
			this.comboBoxFile = new System.Windows.Forms.ComboBox();
			this.groupBoxFrequency = new System.Windows.Forms.GroupBox();
			this.labelFrequency = new System.Windows.Forms.Label();
			this.labelLowFreq = new System.Windows.Forms.Label();
			this.labelHighFreq = new System.Windows.Forms.Label();
			this.sliderFrequency = new FMOD.NET.Controls.ColorSlider();
			this.panelPlayback = new System.Windows.Forms.Panel();
			this.waveFormBox = new FMOD.NET.Controls.WaveForm();
			this.sliderVolume = new FMOD.NET.Controls.ColorSlider();
			this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
			this.radioDsp = new System.Windows.Forms.RadioButton();
			this.radioReverb = new System.Windows.Forms.RadioButton();
			this.radioPlayback = new System.Windows.Forms.RadioButton();
			this.panelMode = new System.Windows.Forms.Panel();
			this.panelMain = new System.Windows.Forms.Panel();
			this.groupBoxTempo.SuspendLayout();
			this.groupBoxFile.SuspendLayout();
			this.groupBoxFrequency.SuspendLayout();
			this.panelPlayback.SuspendLayout();
			this.tableLayoutPanel.SuspendLayout();
			this.panelMode.SuspendLayout();
			this.panelMain.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBoxTempo
			// 
			this.groupBoxTempo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBoxTempo.Controls.Add(this.labelTempoDouble);
			this.groupBoxTempo.Controls.Add(this.labelTempoHalf);
			this.groupBoxTempo.Controls.Add(this.sliderTempo);
			this.groupBoxTempo.Controls.Add(this.labelTempo);
			this.groupBoxTempo.ForeColor = System.Drawing.Color.White;
			this.groupBoxTempo.Location = new System.Drawing.Point(3, 61);
			this.groupBoxTempo.Name = "groupBoxTempo";
			this.groupBoxTempo.Size = new System.Drawing.Size(630, 123);
			this.groupBoxTempo.TabIndex = 0;
			this.groupBoxTempo.TabStop = false;
			this.groupBoxTempo.Text = "Timestretch/Tempo";
			// 
			// labelTempoDouble
			// 
			this.labelTempoDouble.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.labelTempoDouble.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelTempoDouble.ForeColor = System.Drawing.Color.Gold;
			this.labelTempoDouble.Location = new System.Drawing.Point(507, 101);
			this.labelTempoDouble.Name = "labelTempoDouble";
			this.labelTempoDouble.Size = new System.Drawing.Size(117, 13);
			this.labelTempoDouble.TabIndex = 8;
			this.labelTempoDouble.Text = "200%";
			this.labelTempoDouble.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// labelTempoHalf
			// 
			this.labelTempoHalf.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelTempoHalf.ForeColor = System.Drawing.Color.Gold;
			this.labelTempoHalf.Location = new System.Drawing.Point(9, 101);
			this.labelTempoHalf.Name = "labelTempoHalf";
			this.labelTempoHalf.Size = new System.Drawing.Size(141, 13);
			this.labelTempoHalf.TabIndex = 6;
			this.labelTempoHalf.Text = "50%";
			this.labelTempoHalf.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// sliderTempo
			// 
			this.sliderTempo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
			this.sliderTempo.BarPenColorBottom = System.Drawing.Color.FromArgb(((int)(((byte)(87)))), ((int)(((byte)(94)))), ((int)(((byte)(110)))));
			this.sliderTempo.BarPenColorTop = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(60)))), ((int)(((byte)(74)))));
			this.sliderTempo.BorderRoundRectSize = new System.Drawing.Size(8, 8);
			this.sliderTempo.ElapsedInnerColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(56)))), ((int)(((byte)(152)))));
			this.sliderTempo.ElapsedPenColorBottom = System.Drawing.Color.FromArgb(((int)(((byte)(99)))), ((int)(((byte)(130)))), ((int)(((byte)(208)))));
			this.sliderTempo.ElapsedPenColorTop = System.Drawing.Color.FromArgb(((int)(((byte)(95)))), ((int)(((byte)(140)))), ((int)(((byte)(180)))));
			this.sliderTempo.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F);
			this.sliderTempo.ForeColor = System.Drawing.Color.White;
			this.sliderTempo.LargeChange = ((uint)(5u));
			this.sliderTempo.Location = new System.Drawing.Point(6, 66);
			this.sliderTempo.Maximum = 200;
			this.sliderTempo.Minimum = 50;
			this.sliderTempo.Name = "sliderTempo";
			this.sliderTempo.ScaleDivisions = 15;
			this.sliderTempo.ScaleSubDivisions = 5;
			this.sliderTempo.ShowDivisionsText = true;
			this.sliderTempo.ShowSmallScale = false;
			this.sliderTempo.Size = new System.Drawing.Size(618, 48);
			this.sliderTempo.SmallChange = ((uint)(1u));
			this.sliderTempo.TabIndex = 0;
			this.sliderTempo.ThumbInnerColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(56)))), ((int)(((byte)(152)))));
			this.sliderTempo.ThumbPenColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(56)))), ((int)(((byte)(152)))));
			this.sliderTempo.ThumbRoundRectSize = new System.Drawing.Size(16, 16);
			this.sliderTempo.ThumbSize = new System.Drawing.Size(16, 16);
			this.sliderTempo.TickAdd = 0F;
			this.sliderTempo.TickColor = System.Drawing.Color.White;
			this.sliderTempo.TickDivide = 0F;
			this.sliderTempo.Value = 100;
			this.sliderTempo.Scroll += new System.Windows.Forms.ScrollEventHandler(this.sliderTempo_Scroll);
			// 
			// labelTempo
			// 
			this.labelTempo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.labelTempo.ForeColor = System.Drawing.Color.Silver;
			this.labelTempo.Location = new System.Drawing.Point(6, 27);
			this.labelTempo.Name = "labelTempo";
			this.labelTempo.Size = new System.Drawing.Size(618, 36);
			this.labelTempo.TabIndex = 5;
			this.labelTempo.Text = resources.GetString("labelTempo.Text");
			// 
			// buttonOpen
			// 
			this.buttonOpen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonOpen.BackgroundImage = global::FMOD.NET.Sample.Properties.Resources.MusicFolder;
			this.buttonOpen.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.buttonOpen.FlatAppearance.BorderColor = System.Drawing.Color.White;
			this.buttonOpen.FlatAppearance.BorderSize = 0;
			this.buttonOpen.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(64)))));
			this.buttonOpen.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.buttonOpen.Location = new System.Drawing.Point(600, 16);
			this.buttonOpen.Name = "buttonOpen";
			this.buttonOpen.Size = new System.Drawing.Size(24, 24);
			this.buttonOpen.TabIndex = 1;
			this.toolTip.SetToolTip(this.buttonOpen, "Open file...");
			this.buttonOpen.UseVisualStyleBackColor = true;
			this.buttonOpen.Click += new System.EventHandler(this.buttonOpen_Click);
			// 
			// timerUpdate
			// 
			this.timerUpdate.Tick += new System.EventHandler(this.timerUpdate_Tick);
			// 
			// buttonPlay
			// 
			this.buttonPlay.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonPlay.BackgroundImage = global::FMOD.NET.Sample.Properties.Resources.Play;
			this.buttonPlay.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.buttonPlay.FlatAppearance.BorderColor = System.Drawing.Color.White;
			this.buttonPlay.FlatAppearance.BorderSize = 0;
			this.buttonPlay.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(64)))));
			this.buttonPlay.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.buttonPlay.Location = new System.Drawing.Point(706, 6);
			this.buttonPlay.Name = "buttonPlay";
			this.buttonPlay.Size = new System.Drawing.Size(24, 24);
			this.buttonPlay.TabIndex = 4;
			this.toolTip.SetToolTip(this.buttonPlay, "Play/Resume");
			this.buttonPlay.UseVisualStyleBackColor = true;
			this.buttonPlay.Click += new System.EventHandler(this.buttonPlay_Click);
			// 
			// buttonPause
			// 
			this.buttonPause.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonPause.BackgroundImage = global::FMOD.NET.Sample.Properties.Resources.Pause;
			this.buttonPause.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.buttonPause.FlatAppearance.BorderColor = System.Drawing.Color.White;
			this.buttonPause.FlatAppearance.BorderSize = 0;
			this.buttonPause.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(64)))));
			this.buttonPause.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.buttonPause.Location = new System.Drawing.Point(706, 36);
			this.buttonPause.Name = "buttonPause";
			this.buttonPause.Size = new System.Drawing.Size(24, 24);
			this.buttonPause.TabIndex = 5;
			this.toolTip.SetToolTip(this.buttonPause, "Pause/Resume");
			this.buttonPause.UseVisualStyleBackColor = true;
			this.buttonPause.Click += new System.EventHandler(this.buttonPause_Click);
			// 
			// buttonStop
			// 
			this.buttonStop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonStop.BackgroundImage = global::FMOD.NET.Sample.Properties.Resources.Stop;
			this.buttonStop.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.buttonStop.FlatAppearance.BorderColor = System.Drawing.Color.White;
			this.buttonStop.FlatAppearance.BorderSize = 0;
			this.buttonStop.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(64)))));
			this.buttonStop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.buttonStop.Location = new System.Drawing.Point(706, 66);
			this.buttonStop.Name = "buttonStop";
			this.buttonStop.Size = new System.Drawing.Size(24, 24);
			this.buttonStop.TabIndex = 6;
			this.toolTip.SetToolTip(this.buttonStop, "Stop");
			this.buttonStop.UseVisualStyleBackColor = true;
			this.buttonStop.Click += new System.EventHandler(this.buttonStop_Click);
			// 
			// groupBoxFile
			// 
			this.groupBoxFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBoxFile.Controls.Add(this.comboBoxFile);
			this.groupBoxFile.Controls.Add(this.buttonOpen);
			this.groupBoxFile.ForeColor = System.Drawing.Color.White;
			this.groupBoxFile.Location = new System.Drawing.Point(3, 3);
			this.groupBoxFile.Name = "groupBoxFile";
			this.groupBoxFile.Size = new System.Drawing.Size(630, 52);
			this.groupBoxFile.TabIndex = 7;
			this.groupBoxFile.TabStop = false;
			this.groupBoxFile.Text = "FIle Selection";
			// 
			// comboBoxFile
			// 
			this.comboBoxFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.comboBoxFile.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
			this.comboBoxFile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.comboBoxFile.ForeColor = System.Drawing.Color.White;
			this.comboBoxFile.FormattingEnabled = true;
			this.comboBoxFile.Location = new System.Drawing.Point(9, 19);
			this.comboBoxFile.Name = "comboBoxFile";
			this.comboBoxFile.Size = new System.Drawing.Size(585, 21);
			this.comboBoxFile.TabIndex = 2;
			this.comboBoxFile.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.comboBoxFile_KeyPress);
			// 
			// groupBoxFrequency
			// 
			this.groupBoxFrequency.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBoxFrequency.Controls.Add(this.labelFrequency);
			this.groupBoxFrequency.Controls.Add(this.labelLowFreq);
			this.groupBoxFrequency.Controls.Add(this.labelHighFreq);
			this.groupBoxFrequency.Controls.Add(this.sliderFrequency);
			this.groupBoxFrequency.ForeColor = System.Drawing.Color.White;
			this.groupBoxFrequency.Location = new System.Drawing.Point(3, 190);
			this.groupBoxFrequency.Name = "groupBoxFrequency";
			this.groupBoxFrequency.Size = new System.Drawing.Size(630, 123);
			this.groupBoxFrequency.TabIndex = 8;
			this.groupBoxFrequency.TabStop = false;
			this.groupBoxFrequency.Text = "Frequency";
			// 
			// labelFrequency
			// 
			this.labelFrequency.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.labelFrequency.ForeColor = System.Drawing.Color.Silver;
			this.labelFrequency.Location = new System.Drawing.Point(6, 27);
			this.labelFrequency.Name = "labelFrequency";
			this.labelFrequency.Size = new System.Drawing.Size(618, 36);
			this.labelFrequency.TabIndex = 10;
			this.labelFrequency.Text = "Alters the frequency, or \"speed\" of the sound. The default value is can be set wi" +
    "thin a \"Sound\" object, typically 44100 Hz. Reducing the value below 0 will play " +
    "the sound backwards.";
			// 
			// labelLowFreq
			// 
			this.labelLowFreq.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelLowFreq.ForeColor = System.Drawing.Color.Gold;
			this.labelLowFreq.Location = new System.Drawing.Point(9, 101);
			this.labelLowFreq.Name = "labelLowFreq";
			this.labelLowFreq.Size = new System.Drawing.Size(141, 13);
			this.labelLowFreq.TabIndex = 7;
			this.labelLowFreq.Text = "-88200 Hz";
			this.labelLowFreq.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// labelHighFreq
			// 
			this.labelHighFreq.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.labelHighFreq.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelHighFreq.ForeColor = System.Drawing.Color.Gold;
			this.labelHighFreq.Location = new System.Drawing.Point(507, 101);
			this.labelHighFreq.Name = "labelHighFreq";
			this.labelHighFreq.Size = new System.Drawing.Size(117, 13);
			this.labelHighFreq.TabIndex = 9;
			this.labelHighFreq.Text = "88200 Hz";
			this.labelHighFreq.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// sliderFrequency
			// 
			this.sliderFrequency.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.sliderFrequency.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
			this.sliderFrequency.BarPenColorBottom = System.Drawing.Color.FromArgb(((int)(((byte)(87)))), ((int)(((byte)(94)))), ((int)(((byte)(110)))));
			this.sliderFrequency.BarPenColorTop = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(60)))), ((int)(((byte)(74)))));
			this.sliderFrequency.BorderRoundRectSize = new System.Drawing.Size(8, 8);
			this.sliderFrequency.ElapsedInnerColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(56)))), ((int)(((byte)(152)))));
			this.sliderFrequency.ElapsedPenColorBottom = System.Drawing.Color.FromArgb(((int)(((byte)(99)))), ((int)(((byte)(130)))), ((int)(((byte)(208)))));
			this.sliderFrequency.ElapsedPenColorTop = System.Drawing.Color.FromArgb(((int)(((byte)(95)))), ((int)(((byte)(140)))), ((int)(((byte)(180)))));
			this.sliderFrequency.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F);
			this.sliderFrequency.ForeColor = System.Drawing.Color.White;
			this.sliderFrequency.LargeChange = ((uint)(5u));
			this.sliderFrequency.Location = new System.Drawing.Point(9, 66);
			this.sliderFrequency.Maximum = 88200;
			this.sliderFrequency.Minimum = -88200;
			this.sliderFrequency.Name = "sliderFrequency";
			this.sliderFrequency.ScaleDivisions = 10;
			this.sliderFrequency.ScaleSubDivisions = 5;
			this.sliderFrequency.ShowDivisionsText = true;
			this.sliderFrequency.ShowSmallScale = false;
			this.sliderFrequency.Size = new System.Drawing.Size(615, 48);
			this.sliderFrequency.SmallChange = ((uint)(1u));
			this.sliderFrequency.TabIndex = 0;
			this.sliderFrequency.ThumbInnerColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(56)))), ((int)(((byte)(152)))));
			this.sliderFrequency.ThumbPenColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(56)))), ((int)(((byte)(152)))));
			this.sliderFrequency.ThumbRoundRectSize = new System.Drawing.Size(16, 16);
			this.sliderFrequency.ThumbSize = new System.Drawing.Size(16, 16);
			this.sliderFrequency.TickAdd = 0F;
			this.sliderFrequency.TickColor = System.Drawing.Color.White;
			this.sliderFrequency.TickDivide = 0F;
			this.sliderFrequency.Value = 44100;
			this.sliderFrequency.Scroll += new System.Windows.Forms.ScrollEventHandler(this.sliderFrequency_Scroll);
			// 
			// panelPlayback
			// 
			this.panelPlayback.Controls.Add(this.groupBoxFile);
			this.panelPlayback.Controls.Add(this.groupBoxTempo);
			this.panelPlayback.Controls.Add(this.groupBoxFrequency);
			this.panelPlayback.Controls.Add(this.waveFormBox);
			this.panelPlayback.Controls.Add(this.buttonPlay);
			this.panelPlayback.Controls.Add(this.buttonPause);
			this.panelPlayback.Controls.Add(this.buttonStop);
			this.panelPlayback.Controls.Add(this.sliderVolume);
			this.panelPlayback.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelPlayback.Location = new System.Drawing.Point(0, 0);
			this.panelPlayback.Name = "panelPlayback";
			this.panelPlayback.Size = new System.Drawing.Size(798, 404);
			this.panelPlayback.TabIndex = 10;
			// 
			// waveFormBox
			// 
			this.waveFormBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.waveFormBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
			this.waveFormBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			colorBlend3.Colors = new System.Drawing.Color[] {
        System.Drawing.Color.Blue,
        System.Drawing.Color.White,
        System.Drawing.Color.Blue};
			colorBlend3.Positions = new float[] {
        0F,
        0.5F,
        1F};
			this.waveFormBox.InnerBlendColors = colorBlend3;
			this.waveFormBox.Location = new System.Drawing.Point(3, 319);
			this.waveFormBox.Name = "waveFormBox";
			colorBlend4.Colors = new System.Drawing.Color[] {
        System.Drawing.Color.DarkRed,
        System.Drawing.Color.Yellow,
        System.Drawing.Color.DarkRed};
			colorBlend4.Positions = new float[] {
        0F,
        0.5F,
        1F};
			this.waveFormBox.OuterBlendColors = colorBlend4;
			this.waveFormBox.Size = new System.Drawing.Size(792, 82);
			this.waveFormBox.TabIndex = 9;
			// 
			// sliderVolume
			// 
			this.sliderVolume.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
			this.sliderVolume.BarPenColorBottom = System.Drawing.Color.FromArgb(((int)(((byte)(87)))), ((int)(((byte)(94)))), ((int)(((byte)(110)))));
			this.sliderVolume.BarPenColorTop = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(60)))), ((int)(((byte)(74)))));
			this.sliderVolume.BorderRoundRectSize = new System.Drawing.Size(8, 8);
			this.sliderVolume.ElapsedInnerColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(56)))), ((int)(((byte)(152)))));
			this.sliderVolume.ElapsedPenColorBottom = System.Drawing.Color.FromArgb(((int)(((byte)(99)))), ((int)(((byte)(130)))), ((int)(((byte)(208)))));
			this.sliderVolume.ElapsedPenColorTop = System.Drawing.Color.FromArgb(((int)(((byte)(95)))), ((int)(((byte)(140)))), ((int)(((byte)(180)))));
			this.sliderVolume.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F);
			this.sliderVolume.ForeColor = System.Drawing.Color.White;
			this.sliderVolume.LargeChange = ((uint)(5u));
			this.sliderVolume.Location = new System.Drawing.Point(738, 6);
			this.sliderVolume.Name = "sliderVolume";
			this.sliderVolume.Orientation = System.Windows.Forms.Orientation.Vertical;
			this.sliderVolume.ScaleDivisions = 10;
			this.sliderVolume.ScaleSubDivisions = 5;
			this.sliderVolume.ShowDivisionsText = true;
			this.sliderVolume.ShowSmallScale = false;
			this.sliderVolume.Size = new System.Drawing.Size(48, 307);
			this.sliderVolume.SmallChange = ((uint)(1u));
			this.sliderVolume.TabIndex = 0;
			this.sliderVolume.ThumbInnerColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(56)))), ((int)(((byte)(152)))));
			this.sliderVolume.ThumbPenColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(56)))), ((int)(((byte)(152)))));
			this.sliderVolume.ThumbRoundRectSize = new System.Drawing.Size(16, 16);
			this.sliderVolume.ThumbSize = new System.Drawing.Size(16, 16);
			this.sliderVolume.TickAdd = 0F;
			this.sliderVolume.TickColor = System.Drawing.Color.White;
			this.sliderVolume.TickDivide = 0F;
			this.sliderVolume.Value = 100;
			this.sliderVolume.Scroll += new System.Windows.Forms.ScrollEventHandler(this.sliderVolume_Scroll);
			// 
			// tableLayoutPanel
			// 
			this.tableLayoutPanel.ColumnCount = 3;
			this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33F));
			this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 34F));
			this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33F));
			this.tableLayoutPanel.Controls.Add(this.radioDsp, 2, 0);
			this.tableLayoutPanel.Controls.Add(this.radioReverb, 1, 0);
			this.tableLayoutPanel.Controls.Add(this.radioPlayback, 0, 0);
			this.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel.Name = "tableLayoutPanel";
			this.tableLayoutPanel.RowCount = 1;
			this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 39F));
			this.tableLayoutPanel.Size = new System.Drawing.Size(798, 39);
			this.tableLayoutPanel.TabIndex = 11;
			// 
			// radioDsp
			// 
			this.radioDsp.Appearance = System.Windows.Forms.Appearance.Button;
			this.radioDsp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
			this.radioDsp.Dock = System.Windows.Forms.DockStyle.Fill;
			this.radioDsp.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.radioDsp.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.radioDsp.Location = new System.Drawing.Point(537, 3);
			this.radioDsp.Name = "radioDsp";
			this.radioDsp.Size = new System.Drawing.Size(258, 33);
			this.radioDsp.TabIndex = 2;
			this.radioDsp.Text = "DSP Effects";
			this.radioDsp.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.radioDsp.UseVisualStyleBackColor = true;
			// 
			// radioReverb
			// 
			this.radioReverb.Appearance = System.Windows.Forms.Appearance.Button;
			this.radioReverb.Dock = System.Windows.Forms.DockStyle.Fill;
			this.radioReverb.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.radioReverb.Location = new System.Drawing.Point(266, 3);
			this.radioReverb.Name = "radioReverb";
			this.radioReverb.Size = new System.Drawing.Size(265, 33);
			this.radioReverb.TabIndex = 1;
			this.radioReverb.Text = "Reverb";
			this.radioReverb.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.radioReverb.UseVisualStyleBackColor = true;
			// 
			// radioPlayback
			// 
			this.radioPlayback.Appearance = System.Windows.Forms.Appearance.Button;
			this.radioPlayback.Checked = true;
			this.radioPlayback.Dock = System.Windows.Forms.DockStyle.Fill;
			this.radioPlayback.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.radioPlayback.Location = new System.Drawing.Point(3, 3);
			this.radioPlayback.Name = "radioPlayback";
			this.radioPlayback.Size = new System.Drawing.Size(257, 33);
			this.radioPlayback.TabIndex = 0;
			this.radioPlayback.TabStop = true;
			this.radioPlayback.Text = "Playback";
			this.radioPlayback.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.radioPlayback.UseVisualStyleBackColor = true;
			// 
			// panelMode
			// 
			this.panelMode.Controls.Add(this.tableLayoutPanel);
			this.panelMode.Dock = System.Windows.Forms.DockStyle.Top;
			this.panelMode.Location = new System.Drawing.Point(0, 0);
			this.panelMode.Name = "panelMode";
			this.panelMode.Size = new System.Drawing.Size(798, 39);
			this.panelMode.TabIndex = 13;
			// 
			// panelMain
			// 
			this.panelMain.Controls.Add(this.panelPlayback);
			this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelMain.Location = new System.Drawing.Point(0, 39);
			this.panelMain.Name = "panelMain";
			this.panelMain.Size = new System.Drawing.Size(798, 404);
			this.panelMain.TabIndex = 14;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
			this.ClientSize = new System.Drawing.Size(798, 443);
			this.Controls.Add(this.panelMain);
			this.Controls.Add(this.panelMode);
			this.ForeColor = System.Drawing.Color.White;
			this.Name = "MainForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Tag = "";
			this.Text = "FMOD.NET Sample";
			this.Load += new System.EventHandler(this.MainForm_Load);
			this.groupBoxTempo.ResumeLayout(false);
			this.groupBoxFile.ResumeLayout(false);
			this.groupBoxFrequency.ResumeLayout(false);
			this.panelPlayback.ResumeLayout(false);
			this.tableLayoutPanel.ResumeLayout(false);
			this.panelMode.ResumeLayout(false);
			this.panelMain.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox groupBoxTempo;
		private System.Windows.Forms.Button buttonOpen;
		private Controls.WaveForm waveFormBox;
		private System.Windows.Forms.Timer timerUpdate;
		private System.Windows.Forms.ToolTip toolTip;
		private Controls.ColorSlider sliderVolume;
		private Controls.ColorSlider sliderFrequency;
		private System.Windows.Forms.Button buttonPlay;
		private System.Windows.Forms.Button buttonPause;
		private System.Windows.Forms.Button buttonStop;
		private Controls.ColorSlider sliderTempo;
		private System.Windows.Forms.Label labelTempo;
		private System.Windows.Forms.GroupBox groupBoxFile;
		private System.Windows.Forms.Label labelTempoDouble;
		private System.Windows.Forms.Label labelTempoHalf;
		private System.Windows.Forms.GroupBox groupBoxFrequency;
		private System.Windows.Forms.Label labelFrequency;
		private System.Windows.Forms.Label labelHighFreq;
		private System.Windows.Forms.Label labelLowFreq;
		private System.Windows.Forms.Panel panelPlayback;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
		private System.Windows.Forms.RadioButton radioDsp;
		private System.Windows.Forms.RadioButton radioReverb;
		private System.Windows.Forms.RadioButton radioPlayback;
		private System.Windows.Forms.Panel panelMode;
		private System.Windows.Forms.Panel panelMain;
		private System.Windows.Forms.ComboBox comboBoxFile;
	}
}

