namespace FMOD.NET.Sample
{
	partial class IntParameterPanel
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
			this.labelDescription = new System.Windows.Forms.Label();
			this.comboValue = new System.Windows.Forms.ComboBox();
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
			this.labelDescription.Size = new System.Drawing.Size(372, 37);
			this.labelDescription.TabIndex = 1;
			this.labelDescription.Text = "Parameter Description. This could be a line of test that continues on to be two l" +
    "ines";
			this.labelDescription.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// comboValue
			// 
			this.comboValue.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
			this.comboValue.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboValue.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.comboValue.ForeColor = System.Drawing.Color.White;
			this.comboValue.FormattingEnabled = true;
			this.comboValue.Location = new System.Drawing.Point(70, 53);
			this.comboValue.Name = "comboValue";
			this.comboValue.Size = new System.Drawing.Size(174, 21);
			this.comboValue.TabIndex = 3;
			// 
			// IntParameterPanel
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
			this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.Controls.Add(this.comboValue);
			this.Controls.Add(this.labelDescription);
			this.ForeColor = System.Drawing.Color.White;
			this.MinimumSize = new System.Drawing.Size(300, 90);
			this.Name = "IntParameterPanel";
			this.Size = new System.Drawing.Size(378, 88);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Label labelDescription;
		private System.Windows.Forms.ComboBox comboValue;
	}
}
