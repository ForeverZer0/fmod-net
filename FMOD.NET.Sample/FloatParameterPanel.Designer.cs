namespace FMOD.NET.Sample
{
	partial class FloatParameterPanel
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.labelDescription = new System.Windows.Forms.Label();
			this.toolTip = new System.Windows.Forms.ToolTip(this.components);
			this.trackBarValue = new FMOD.NET.Sample.FloatTrackBar();
			((System.ComponentModel.ISupportInitialize)(this.trackBarValue)).BeginInit();
			this.SuspendLayout();
			// 
			// labelDescription
			// 
			this.labelDescription.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.labelDescription.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
			this.labelDescription.Location = new System.Drawing.Point(3, 0);
			this.labelDescription.Name = "labelDescription";
			this.labelDescription.Size = new System.Drawing.Size(374, 39);
			this.labelDescription.TabIndex = 0;
			this.labelDescription.Text = "Parameter Description. This could be a line of test that continues on to be two l" +
    "ines";
			this.labelDescription.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// toolTip
			// 
			this.toolTip.AutoPopDelay = 8000;
			this.toolTip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
			this.toolTip.ForeColor = System.Drawing.Color.White;
			this.toolTip.InitialDelay = 100;
			this.toolTip.ReshowDelay = 100;
			// 
			// trackBarValue
			// 
			this.trackBarValue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.trackBarValue.LargeChange = 1100F;
			this.trackBarValue.Location = new System.Drawing.Point(6, 42);
			this.trackBarValue.Maximum = 22000F;
			this.trackBarValue.Minimum = 0F;
			this.trackBarValue.Name = "trackBarValue";
			this.trackBarValue.Precision = 0.01F;
			this.trackBarValue.Size = new System.Drawing.Size(371, 45);
			this.trackBarValue.SmallChange = 0.01F;
			this.trackBarValue.TabIndex = 3;
			this.trackBarValue.TickFrequency = 110000;
			this.trackBarValue.Value = 0F;
			// 
			// FloatParameterPanel
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
			this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.Controls.Add(this.trackBarValue);
			this.Controls.Add(this.labelDescription);
			this.ForeColor = System.Drawing.Color.White;
			this.MinimumSize = new System.Drawing.Size(300, 90);
			this.Name = "FloatParameterPanel";
			this.Size = new System.Drawing.Size(380, 90);
			((System.ComponentModel.ISupportInitialize)(this.trackBarValue)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label labelDescription;
		private FMOD.NET.Sample.FloatTrackBar trackBarValue;
		private System.Windows.Forms.ToolTip toolTip;
	}
}
