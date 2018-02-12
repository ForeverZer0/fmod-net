namespace FMOD.Sharp.TestApp
{
	using System = global::System;

	partial class MainForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private global::System.ComponentModel.IContainer components = null;

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
			this.splitContainerrMain = new System.Windows.Forms.SplitContainer();
			this.listBoxSource = new System.Windows.Forms.ListBox();
			this.tabControlMain = new System.Windows.Forms.TabControl();
			this.tabReverb = new System.Windows.Forms.TabPage();
			this.tabDSP = new System.Windows.Forms.TabPage();
			this.checkBoxReverb = new System.Windows.Forms.CheckBox();
			this.comboBoxReverb = new System.Windows.Forms.ComboBox();
			this.labelReverbPresets = new System.Windows.Forms.Label();
			this.groupBoxPlayback = new System.Windows.Forms.GroupBox();
			this.trackBarPosition = new System.Windows.Forms.TrackBar();
			((System.ComponentModel.ISupportInitialize)(this.splitContainerrMain)).BeginInit();
			this.splitContainerrMain.Panel1.SuspendLayout();
			this.splitContainerrMain.Panel2.SuspendLayout();
			this.splitContainerrMain.SuspendLayout();
			this.tabControlMain.SuspendLayout();
			this.tabReverb.SuspendLayout();
			this.groupBoxPlayback.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.trackBarPosition)).BeginInit();
			this.SuspendLayout();
			// 
			// splitContainerrMain
			// 
			this.splitContainerrMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainerrMain.Location = new System.Drawing.Point(0, 0);
			this.splitContainerrMain.Name = "splitContainerrMain";
			// 
			// splitContainerrMain.Panel1
			// 
			this.splitContainerrMain.Panel1.Controls.Add(this.listBoxSource);
			// 
			// splitContainerrMain.Panel2
			// 
			this.splitContainerrMain.Panel2.Controls.Add(this.groupBoxPlayback);
			this.splitContainerrMain.Panel2.Controls.Add(this.tabControlMain);
			this.splitContainerrMain.Size = new System.Drawing.Size(706, 484);
			this.splitContainerrMain.SplitterDistance = 209;
			this.splitContainerrMain.TabIndex = 0;
			// 
			// listBoxSource
			// 
			this.listBoxSource.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.listBoxSource.FormattingEnabled = true;
			this.listBoxSource.IntegralHeight = false;
			this.listBoxSource.Location = new System.Drawing.Point(12, 12);
			this.listBoxSource.Name = "listBoxSource";
			this.listBoxSource.Size = new System.Drawing.Size(194, 460);
			this.listBoxSource.TabIndex = 0;
			this.listBoxSource.DoubleClick += new System.EventHandler(this.listBoxSource_DoubleClick);
			// 
			// tabControlMain
			// 
			this.tabControlMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tabControlMain.Appearance = System.Windows.Forms.TabAppearance.Buttons;
			this.tabControlMain.Controls.Add(this.tabReverb);
			this.tabControlMain.Controls.Add(this.tabDSP);
			this.tabControlMain.Location = new System.Drawing.Point(3, 125);
			this.tabControlMain.Name = "tabControlMain";
			this.tabControlMain.SelectedIndex = 0;
			this.tabControlMain.Size = new System.Drawing.Size(478, 347);
			this.tabControlMain.TabIndex = 0;
			// 
			// tabReverb
			// 
			this.tabReverb.Controls.Add(this.labelReverbPresets);
			this.tabReverb.Controls.Add(this.comboBoxReverb);
			this.tabReverb.Controls.Add(this.checkBoxReverb);
			this.tabReverb.Location = new System.Drawing.Point(4, 25);
			this.tabReverb.Name = "tabReverb";
			this.tabReverb.Padding = new System.Windows.Forms.Padding(3);
			this.tabReverb.Size = new System.Drawing.Size(470, 318);
			this.tabReverb.TabIndex = 0;
			this.tabReverb.Text = "Reverb";
			this.tabReverb.UseVisualStyleBackColor = true;
			// 
			// tabDSP
			// 
			this.tabDSP.Location = new System.Drawing.Point(4, 25);
			this.tabDSP.Name = "tabDSP";
			this.tabDSP.Padding = new System.Windows.Forms.Padding(3);
			this.tabDSP.Size = new System.Drawing.Size(436, 392);
			this.tabDSP.TabIndex = 1;
			this.tabDSP.Text = "DSP";
			this.tabDSP.UseVisualStyleBackColor = true;
			// 
			// checkBoxReverb
			// 
			this.checkBoxReverb.AutoSize = true;
			this.checkBoxReverb.Location = new System.Drawing.Point(6, 19);
			this.checkBoxReverb.Name = "checkBoxReverb";
			this.checkBoxReverb.Size = new System.Drawing.Size(103, 17);
			this.checkBoxReverb.TabIndex = 0;
			this.checkBoxReverb.Text = "Reverb Enabled";
			this.checkBoxReverb.UseVisualStyleBackColor = true;
			this.checkBoxReverb.CheckedChanged += new System.EventHandler(this.checkBoxReverb_CheckedChanged);
			// 
			// comboBoxReverb
			// 
			this.comboBoxReverb.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.comboBoxReverb.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxReverb.FormattingEnabled = true;
			this.comboBoxReverb.Items.AddRange(new object[] {
            "OFF",
            "GENERIC ",
            "PADDED CELL ",
            "ROOM ",
            "BATHROOM ",
            "LIVING ROOM ",
            "STONE ROOM ",
            "AUDITORIUM ",
            "CONCERT HALL ",
            "CAVE",
            "ARENA ",
            "HANGAR",
            "CARPETTED HALLWAY ",
            "HALLWAY ",
            "STONE CORRIDOR   ",
            "ALLEY",
            "FOREST",
            "CITY",
            "MOUNTAINS ",
            "QUARRY   ",
            "PLAIN ",
            "PARKING LOT ",
            "SEWER PIPE ",
            "UNDERWATER"});
			this.comboBoxReverb.Location = new System.Drawing.Point(237, 17);
			this.comboBoxReverb.Name = "comboBoxReverb";
			this.comboBoxReverb.Size = new System.Drawing.Size(227, 21);
			this.comboBoxReverb.TabIndex = 1;
			this.comboBoxReverb.SelectedIndexChanged += new System.EventHandler(this.comboBoxReverb_SelectedIndexChanged);
			// 
			// labelReverbPresets
			// 
			this.labelReverbPresets.AutoSize = true;
			this.labelReverbPresets.Location = new System.Drawing.Point(148, 20);
			this.labelReverbPresets.Name = "labelReverbPresets";
			this.labelReverbPresets.Size = new System.Drawing.Size(83, 13);
			this.labelReverbPresets.TabIndex = 2;
			this.labelReverbPresets.Text = "Reverb Presets:";
			// 
			// groupBoxPlayback
			// 
			this.groupBoxPlayback.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBoxPlayback.Controls.Add(this.trackBarPosition);
			this.groupBoxPlayback.Location = new System.Drawing.Point(3, 12);
			this.groupBoxPlayback.Name = "groupBoxPlayback";
			this.groupBoxPlayback.Size = new System.Drawing.Size(474, 107);
			this.groupBoxPlayback.TabIndex = 1;
			this.groupBoxPlayback.TabStop = false;
			this.groupBoxPlayback.Text = "Playback";
			// 
			// trackBarPosition
			// 
			this.trackBarPosition.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.trackBarPosition.Location = new System.Drawing.Point(6, 56);
			this.trackBarPosition.Name = "trackBarPosition";
			this.trackBarPosition.Size = new System.Drawing.Size(462, 45);
			this.trackBarPosition.TabIndex = 0;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(706, 484);
			this.Controls.Add(this.splitContainerrMain);
			this.Name = "MainForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "FMOD#";
			this.Load += new System.EventHandler(this.MainForm_Load);
			this.splitContainerrMain.Panel1.ResumeLayout(false);
			this.splitContainerrMain.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainerrMain)).EndInit();
			this.splitContainerrMain.ResumeLayout(false);
			this.tabControlMain.ResumeLayout(false);
			this.tabReverb.ResumeLayout(false);
			this.tabReverb.PerformLayout();
			this.groupBoxPlayback.ResumeLayout(false);
			this.groupBoxPlayback.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.trackBarPosition)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private global::System.Windows.Forms.SplitContainer splitContainerrMain;
		private global::System.Windows.Forms.ListBox listBoxSource;
		private System.Windows.Forms.TabControl tabControlMain;
		private System.Windows.Forms.TabPage tabReverb;
		private System.Windows.Forms.TabPage tabDSP;
		private System.Windows.Forms.Label labelReverbPresets;
		private System.Windows.Forms.ComboBox comboBoxReverb;
		private System.Windows.Forms.CheckBox checkBoxReverb;
		private System.Windows.Forms.GroupBox groupBoxPlayback;
		private System.Windows.Forms.TrackBar trackBarPosition;
	}
}

