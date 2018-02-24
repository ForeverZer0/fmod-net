using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FMOD.NET.Sample
{
	[DefaultEvent("ValueChanged")]
	public partial class FloatParameterPanel : UserControl
	{
		public FloatParameterPanel()
		{
			InitializeComponent();
			trackBarValue.ValueChanged += (s, e) => toolTip.SetToolTip(trackBarValue, $"{trackBarValue.Value:0.00} {ParameterUnit}");
		}

		[Category("Behavior")]
		public float Value
		{
			get => trackBarValue.Value;
			set => trackBarValue.Value = value;
		}

		public event EventHandler ValueChanged
		{
			add { trackBarValue.ValueChanged += value; }
			remove { trackBarValue.ValueChanged -= value; }
		}

		[TypeConverter(typeof(ExpandableObjectConverter))][Browsable(true)]
		public FloatTrackBar TrackBarControl
		{
			get => trackBarValue;
		}


		[Browsable(true), DefaultValue("")][Category("Behavior")]
		public string ParameterDescription
		{
			get => labelDescription.Text;
			set => labelDescription.Text = value;
		}

		[Browsable(true), DefaultValue("")][Category("Behavior")]
		public string ParameterUnit { get; set; } = "";

	}
}
