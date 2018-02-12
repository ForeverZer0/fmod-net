namespace FMOD.Sharp.Sample
{
	partial class WaveFormWindow
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
			System.Drawing.Drawing2D.ColorBlend colorBlend1 = new System.Drawing.Drawing2D.ColorBlend();
			System.Drawing.Drawing2D.ColorBlend colorBlend2 = new System.Drawing.Drawing2D.ColorBlend();
			this.timer1 = new System.Windows.Forms.Timer(this.components);
			this.waveForm1 = new FMOD.Sharp.Controls.WaveForm();
			this.SuspendLayout();
			// 
			// timer1
			// 
			this.timer1.Enabled = true;
			this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
			// 
			// waveForm1
			// 
			this.waveForm1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
			this.waveForm1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.waveForm1.Dock = System.Windows.Forms.DockStyle.Fill;
			colorBlend1.Colors = new System.Drawing.Color[] {
        System.Drawing.Color.Red,
        System.Drawing.Color.White,
        System.Drawing.Color.Red};
			colorBlend1.Positions = new float[] {
        0F,
        0.5F,
        1F};
			this.waveForm1.InnerBlendColors = colorBlend1;
			this.waveForm1.Location = new System.Drawing.Point(0, 0);
			this.waveForm1.Name = "waveForm1";
			colorBlend2.Colors = new System.Drawing.Color[] {
        System.Drawing.Color.DarkRed,
        System.Drawing.Color.Brown,
        System.Drawing.Color.DarkRed};
			colorBlend2.Positions = new float[] {
        0F,
        0.5F,
        1F};
			this.waveForm1.OuterBlendColors = colorBlend2;
			this.waveForm1.Size = new System.Drawing.Size(958, 328);
			this.waveForm1.TabIndex = 0;
			this.waveForm1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.waveForm1_MouseClick);
			// 
			// WaveFormWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(958, 328);
			this.Controls.Add(this.waveForm1);
			this.KeyPreview = true;
			this.Name = "WaveFormWindow";
			this.Text = "WaveFormWindow";
			this.Load += new System.EventHandler(this.WaveFormWindow_Load);
			this.ResumeLayout(false);

		}

		#endregion

		private Controls.WaveForm waveForm1;
		private System.Windows.Forms.Timer timer1;
	}
}