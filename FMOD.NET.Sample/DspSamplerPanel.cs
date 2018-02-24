#region License

// DspSamplerPanel.cs is distributed under the Microsoft Public License (MS-PL)
// 
// Copyright (c) 2018,  Eric Freed
// All Rights Reserved.
// 
// This license governs use of the accompanying software. If you use the software, you
// accept this license. If you do not accept the license, do not use the software.
// 
// 1. Definitions
// The terms "reproduce," "reproduction," "derivative works," and "distribution" have the
// same meaning here as under U.S. copyright law.
// A "contribution" is the original software, or any additions or changes to the software.
// A "contributor" is any person that distributes its contribution under this license.
// "Licensed patents" are a contributor's patent claims that read directly on its contribution.
// 
// 2. Grant of Rights
// (A) Copyright Grant- Subject to the terms of this license, including the license conditions 
// and limitations in section 3, each contributor grants you a non-exclusive, worldwide, royalty-free 
// copyright license to reproduce its contribution, prepare derivative works of its contribution, and 
// distribute its contribution or any derivative works that you create.
// 
// (B) Patent Grant- Subject to the terms of this license, including the license conditions and 
// limitations in section 3, each contributor grants you a non-exclusive, worldwide, royalty-free license
//  under its licensed patents to make, have made, use, sell, offer for sale, import, and/or otherwise 
// dispose of its contribution in the software or derivative works of the contribution in the software.
// 
// 3. Conditions and Limitations
// (A) No Trademark License- This license does not grant you rights to use any contributors' name, 
// logo, or trademarks.
// 
// (B) If you bring a patent claim against any contributor over patents that you claim are infringed by 
// the software, your patent license from such contributor to the software ends automatically.
// 
// (C) If you distribute any portion of the software, you must retain all copyright, patent, trademark, and
//  attribution notices that are present in the software.
// 
// (D) If you distribute any portion of the software in source code form, you may do so only under this 
// license by including a complete copy of this license with your distribution. If you distribute any portion
//  of the software in compiled or object code form, you may only do so under a license that complies 
// with this license.
// 
// (E) The software is licensed "as-is." You bear the risk of using it. The contributors give no express 
// warranties, guarantees or conditions. You may have additional consumer rights under your local laws 
// which this license cannot change. To the extent permitted under your local laws, the contributors 
// exclude the implied warranties of merchantability, fitness for a particular purpose and non-infringement.
// 
// Created 7:47 PM 02/20/2018

#endregion

#region Using Directives

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using FMOD.NET.Core;
using FMOD.NET.Enumerations;
using FMOD.NET.Structures;

#endregion

namespace FMOD.NET.Sample
{
	public partial class DspSamplerPanel : UserControl
	{
		private Dictionary<string, DspType> _infos;

		#region Constructors

		public DspSamplerPanel()
		{
			InitializeComponent();
		}

		#endregion

		#region Methods

		private void AddBoolControl(Dsp dsp, ref DspParameterDesc info, int index)
		{
			var boolPanel = new IntParameterPanel();
			boolPanel.ComboBox.Items.AddRange(new object[] { "False", "True" });
			boolPanel.Description = info.Description;
			boolPanel.Anchor = AnchorStyles.Left | AnchorStyles.Right;
			boolPanel.ValueChanged += (s, e) => dsp.SetParameterBool(index, boolPanel.Value == 1);
			flowPanel.Controls.Add(boolPanel, 0, index);
		}

		// ReSharper disable once UnusedParameter.Local
		private void AddDataControl(Dsp dsp, ref DspParameterDesc info, int index)
		{
			var panel = new IntParameterPanel { Description = info.Description };
			panel.ComboBox.Visible = false;
			panel.Anchor = AnchorStyles.Left | AnchorStyles.Right;
			var label = new Label
			{
				Text = @"Unable to create dynamic control for ""Data"" parameters.",
				ForeColor = Color.Red,
				AutoSize = false,
				TextAlign = ContentAlignment.MiddleCenter,
				Anchor = AnchorStyles.Bottom | AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
				Size = new Size(371, 45),
				Location = new Point(6, 42)
			};
			panel.Controls.Add(label);
			flowPanel.Controls.Add(panel, 0, index);
		}

		private void AddFloatControl(Dsp dsp, ref DspParameterDesc info, int index)
		{
			var floatPanel = new FloatParameterPanel
			{
				ParameterUnit = info.Label,
				ParameterDescription = info.Description
			};
			floatPanel.TrackBarControl.Maximum = info.FloatDescription.Maximum;
			floatPanel.TrackBarControl.Minimum = info.FloatDescription.Minimum;
			floatPanel.TrackBarControl.TickFactor = 20;
			floatPanel.TrackBarControl.Value = dsp.GetParameterFloat(index);
			floatPanel.Anchor = AnchorStyles.Left | AnchorStyles.Right;
			floatPanel.ValueChanged += (s, e) => dsp.SetParameterFloat(index, floatPanel.Value);
			flowPanel.Controls.Add(floatPanel, 0, index);
		}

		private void AddIntControl(Dsp dsp, ref DspParameterDesc info, int index)
		{
			var intPanel = new IntParameterPanel();
			// ReSharper disable once CoVariantArrayConversion
			intPanel.ComboBox.Items.AddRange(info.IntDescription.ValueNames);
			intPanel.Description = info.Description;
			intPanel.Anchor = AnchorStyles.Left | AnchorStyles.Right;
			intPanel.Value = dsp.GetParameterInt(index);
			intPanel.ValueChanged += (s, e) => dsp.SetParameterInt(index, intPanel.Value);
			flowPanel.Controls.Add(intPanel, 0, index);
		}

		private void AddParameterControl(ref Dsp dsp, int index)
		{
			var info = dsp.GetParameterInfo(index);
			switch (info.Type)
			{
				case DspParameterType.Float:
					AddFloatControl(dsp, ref info, index);
					break;
				case DspParameterType.Int:
					AddIntControl(dsp, ref info, index);
					break;
				case DspParameterType.Bool:
					AddBoolControl(dsp, ref info, index);
					break;
				case DspParameterType.Data:
					AddDataControl(dsp, ref info, index);
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		private void checkedListDsp_ItemCheck(object sender, ItemCheckEventArgs e)
		{
			var name = (string) checkedListDsp.Items[e.Index];
			if (e.NewValue == CheckState.Unchecked)
			{
				for (var i = 0; i < StaticPlayer.System.MasterChannelGroup.DspCount; i++)
				{
					// TODO: Implement creating array of DSPs and returning from ChannelControl
					var dsp = StaticPlayer.System.MasterChannelGroup.GetDsp(i);
					if (dsp.Type == _infos[name])
					{
						StaticPlayer.System.MasterChannelGroup.RemoveDsp(dsp);
						dsp.Dispose();
					}
				}
			}
			else if (e.NewValue == CheckState.Checked)
			{
				var dsp = StaticPlayer.System.CreateDspByType(_infos[name]);
				StaticPlayer.System.MasterChannelGroup.AddDsp(dsp, DspIndex.Head);
			}
		}

		private void checkedListDsp_SelectedIndexChanged(object sender, EventArgs e)
		{
			flowPanel.SuspendLayout();
			foreach (Control ctrl in flowPanel.Controls)
				ctrl.Dispose();
			flowPanel.Controls.Clear();
			if (checkedListDsp.GetItemChecked(checkedListDsp.SelectedIndex))
			{
				var name = (string) checkedListDsp.SelectedItem;
				for (var i = 0; i < StaticPlayer.System.MasterChannelGroup.DspCount; i++)
				{
					var dsp = StaticPlayer.System.MasterChannelGroup.GetDsp(i);
					if (dsp.Type != _infos[name])
						continue;
					for (var j = 0; j < dsp.ParameterCount; j++)
						AddParameterControl(ref dsp, j);
					break;
				}
			}
			flowPanel.ResumeLayout(true);
		}

		private void DspSamplerPanel_Load(object sender, EventArgs e)
		{
			_infos = new Dictionary<string, DspType>();
			foreach (DspType value in Enum.GetValues(typeof(DspType)))
				try
				{
					using (var dsp = StaticPlayer.System.CreateDspByType(value))
					{
						if (dsp == null)
							continue;
						_infos[dsp.GetInfo().Name] = value;
					}
				}
				catch (FmodException)
				{
				}
			// ReSharper disable once CoVariantArrayConversion
			checkedListDsp.Items.AddRange(_infos.Keys.ToArray());
		}

		#endregion
	}
}