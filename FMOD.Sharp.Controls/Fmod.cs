using System;
using System.ComponentModel;
using System.Windows.Forms;
using FMOD.Core;
using FMOD.Enumerations;

namespace FMOD.Sharp.Controls
{
	public partial class Fmod : Component
	{
		private FmodSystem _system;
		private InitFlags _initFlags;

		[TypeConverter(typeof(ExpandableObjectConverter))]
		public FmodSystem System
		{
			get { return _system; }
		}

		[TypeConverter(typeof(ExpandableObjectConverter))]
		public Timer UpdateTimer
		{
			get => _updateTimer;
		}

		[TypeConverter(typeof(EnumConverter))]
		public InitFlags InitFlags
		{
			get { return _initFlags; }
			set
			{
				_initFlags = value; 
				InitializeSystem();
			}
		}


		public Fmod()
		{
			InitializeSystem();
			InitializeComponent();
		}

		private void InitializeSystem()
		{
			_system?.Dispose();
			_system	= FmodSystem.Create();
			_system.Initialize(_initFlags);
		}

		public Fmod(IContainer container)
		{
			container.Add(this);
			InitializeComponent();
		}

		private void timer1_Tick(object sender, EventArgs e)
		{

		}
	}
}
