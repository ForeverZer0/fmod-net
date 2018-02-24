using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using FMOD.NET.Controls;
using FMOD.NET.Core;
using FMOD.NET.Structures;

namespace FMOD.NET.Sample
{
	public partial class ReverbPanel : UserControl
	{
		[SuppressMessage("ReSharper", "LocalizableElement")]
		public ReverbPanel()
		{
			InitializeComponent();

			var labels = new[]
			{
				labelDecayTime, labelDensity, labelDiffusion, labelEarlyDelay,
				labelEarlyLateMix, labelHFDecayRatio, labelHFReference, labelHighCut,
				labelLateDelay, labelLowShelfFreq, labelLowShelfGain, labelWetLevel
			};
			foreach (var label in labels)
			{
				var sliderName = Regex.Replace(label.Name, "label", "slider");
				var slider = (ColorSlider) Controls.Find(sliderName, true)[0];
				slider.ValueChanged += (s, a) => label.Text = $"{slider.Value} {label.Tag}";
				label.Text = $"{slider.Value} {label.Tag}";
				slider.Scroll += Slider_Scroll;
			}

			var names = ReverbPresets.GetNames().ToArray();

			for (var i = 0; i < names.Length; i++)
			{
				var radioName = $"radioButton{i + 1}";
				var radio = (RadioButton) tableLayoutPresets.Controls.Find(radioName, false)[0];
				radio.Text = names[i];
				radio.Tag = i;
				radio.CheckedChanged += Radio_CheckedChanged;
			}
		}

		private void UpdateProperties(ReverbProperties properties)
		{
			StaticPlayer.System.SetReverbProperties(1, properties);
			sliderDecayTime.Value = Convert.ToInt32(properties.DecayTime);
			sliderEarlyDelay.Value = Convert.ToInt32(properties.EarlyDelay);
			sliderLateDelay.Value = Convert.ToInt32(properties.LateDelay);
			sliderHFReference.Value = Convert.ToInt32(properties.HFReference);
			sliderHFDecayRatio.Value = Convert.ToInt32(properties.HFDecayRatio);
			sliderDiffusion.Value = Convert.ToInt32(properties.Diffusion);
			sliderDensity.Value = Convert.ToInt32(properties.Density);
			sliderLowShelfFreq.Value = Convert.ToInt32(properties.LowShelfFrequency);
			sliderLowShelfGain.Value = Convert.ToInt32(properties.LowShelfGain);
			sliderHighCut.Value = Convert.ToInt32(properties.HighCut);
			sliderEarlyLateMix.Value = Convert.ToInt32(properties.EarlyLateMix);
			sliderWetLevel.Value = Convert.ToInt32(properties.WetLevel);
		}

		private void Radio_CheckedChanged(object sender, EventArgs e)
		{
			var radio = (RadioButton) sender;
			var index = Convert.ToInt32(radio.Tag);
			var properties = ReverbPresets.FromIndex(index);
			UpdateProperties(properties);
		}

		private void Slider_Scroll(object sender, ScrollEventArgs e)
		{
			var properties = new ReverbProperties
			{
				DecayTime = sliderDecayTime.Value,
				EarlyDelay = sliderEarlyDelay.Value,
				LateDelay = sliderLateDelay.Value,
				HFReference = sliderHFReference.Value,
				HFDecayRatio = sliderHFDecayRatio.Value,
				Diffusion = sliderDiffusion.Value,
				Density = sliderDensity.Value,
				LowShelfFrequency = sliderLowShelfFreq.Value,
				LowShelfGain = sliderLowShelfGain.Value,
				HighCut = sliderHighCut.Value,
				EarlyLateMix = sliderEarlyLateMix.Value,
				WetLevel = sliderWetLevel.Value
			};
			UpdateProperties(properties);
		}
	}
}
