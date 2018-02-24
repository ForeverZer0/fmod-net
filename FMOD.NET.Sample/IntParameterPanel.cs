using System;
using System.Windows.Forms;

namespace FMOD.NET.Sample
{
	public partial class IntParameterPanel : UserControl
	{
		public ComboBox ComboBox
		{
			get => comboValue;
		}

		public int Value
		{
			get => comboValue.SelectedIndex;
			set => comboValue.SelectedIndex = value;
		}

		public event EventHandler ValueChanged
		{
			add => comboValue.SelectedIndexChanged += value;
			remove => comboValue.SelectedIndexChanged -= value;
		}

		public string Description
		{
			get => labelDescription.Text;
			set => labelDescription.Text = value;
		}


		public IntParameterPanel()
		{
			InitializeComponent();
		}
	}
}
