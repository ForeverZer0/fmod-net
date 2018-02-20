namespace FMOD.NET.Controls
{
	partial class WaveForm
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
			this.positionCursor = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// positionCursor
			// 
			this.positionCursor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
			this.positionCursor.BackColor = System.Drawing.Color.ForestGreen;
			this.positionCursor.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
			this.positionCursor.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
			this.positionCursor.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Lime;
			this.positionCursor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.positionCursor.Location = new System.Drawing.Point(141, 0);
			this.positionCursor.Margin = new System.Windows.Forms.Padding(0);
			this.positionCursor.Name = "positionCursor";
			this.positionCursor.Size = new System.Drawing.Size(5, 64);
			this.positionCursor.TabIndex = 1;
			this.positionCursor.UseVisualStyleBackColor = false;
			// 
			// WaveForm
			// 
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
			this.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.Controls.Add(this.positionCursor);
			this.DoubleBuffered = true;
			this.Name = "WaveForm";
			this.Size = new System.Drawing.Size(300, 64);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button positionCursor;
	}
}
