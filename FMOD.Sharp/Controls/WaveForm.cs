using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace FMOD.Sharp.Controls
{
	/// <summary>
	/// Control to display a visual representation of a sound.
	/// </summary>
	/// <seealso cref="System.Windows.Forms.UserControl" />
	[Description("Control to display a visual representation of a sound.")]
	[DefaultEvent("SoundChanged"), DefaultProperty("BlendColors")]
	[ToolboxItem(true), ToolboxBitmap(typeof(WaveForm), "WaveformIcon")] 
	public partial class WaveForm : UserControl
	{
		#region Events

		/// <summary>
		/// Occurs when a sound is loaded.
		/// </summary>
		[Category("FMOD#")]
		[Description("Occurs when a sound is loaded.")]
		public event EventHandler SoundChanged;

		/// <summary>
		/// Occurs when the blend colors used to draw the waveform are changed.
		/// </summary>
		[Category("FMOD#")]
		[Description("Occurs when the blend colors used to draw the waveform are changed.")]
		public event EventHandler BlendColorsChanged; 

		/// <summary>
		/// Occurs when the gradient drawing mode used to draw the waveform has changed.
		/// </summary>
		[Category("FMOD#")]
		[Description("Occurs when the gradient drawing mode used to draw the waveform has changed.")]
		public event EventHandler GradientModeChanged;

		/// <summary>
		/// Occurs when the position cursor is clicked.
		/// </summary>
		[Category("FMOD#")]
		[Description("Occurs when the position cursor is clicked.")]
		public event EventHandler CursorClick
			
		{
			add { positionCursor.Click += value; }
			remove { positionCursor.Click -= value; }
		}

		/// <summary>
		/// Occurs when the mouse pointer is over the position cursor and a mouse button is pressed.
		/// </summary>
		[Category("FMOD#")]
		[Description("Occurs when the mouse pointer is over the position cursor and a mouse button is pressed.")]
		public event MouseEventHandler CursorMouseDown
			
		{
			add { positionCursor.MouseDown += value; }
			remove { positionCursor.MouseDown -= value; }
		}

		/// <summary>
		/// Occurs when the mouse pointer is over the position cursor and a mouse button is released.
		/// </summary>
		[Category("FMOD#")]
		[Description("Occurs when the mouse pointer is over the position cursor and a mouse button is released.")]
		public event MouseEventHandler CursorMouseUp
			
		{
			add { positionCursor.MouseUp += value; }
			remove { positionCursor.MouseUp -= value; }
		}

		/// <summary>
		/// Occurs when the mouse remains stationary over the postion cursor for an amount of time.
		/// </summary>
		[Category("FMOD#")]
		[Description("Occurs when the mouse remains stationary over the postion cursor for an amount of time.")]
		public event EventHandler CursorMouseHover
			
		{
			add { positionCursor.MouseHover += value; }
			remove { positionCursor.MouseHover -= value; }
		}

		/// <summary>
		/// Occurs when the mouse pointer enters over the visible portion of the position cursor.
		/// </summary>
		[Category("FMOD#")]
		[Description("Occurs when the mouse pointer entes over the visible portion of the position cursor.")]
		public event EventHandler CursorMouseEnter
			
		{
			add { positionCursor.MouseEnter += value; }
			remove { positionCursor.MouseEnter -= value; }
		}

		/// <summary>
		/// Occurs when the mouse pointer leaves the visible portion of the position cursor.
		/// </summary>
		[Category("FMOD#")]
		[Description("Occurs when the mouse pointer leaves the visible portion of the position cursor.")]
		public event EventHandler CursorMouseLeave
			
		{
			add { positionCursor.MouseLeave += value; }
			remove { positionCursor.MouseLeave -= value; }
		}

		/// <summary>
		/// Occurs when the mouse pointer is over the position cursor and moves.
		/// </summary>
		[Category("FMOD#")]
		[Description("Occurs when the mouse pointer is over the position cursor and moves.")]
		public event MouseEventHandler CursorMouseMove
			
		{
			add { positionCursor.MouseMove += value; }
			remove { positionCursor.MouseMove -= value; }
		}

		#endregion

		/// <summary>
		/// Gets or sets the outer waveform blend colors.
		/// </summary>
		/// <value>
		/// The outer blend colors.
		/// </value>
		[Category("FMOD#")][Description("Define the colors used for the gradient of the outer waveform.")]
		[TypeConverter(typeof(ExpandableObjectConverter))]
		public ColorBlend OuterBlendColors
		{
			get { return _outerBlendColors; }
			set
			{
				_outerBlendColors = value;
				BlendColorsChanged?.Invoke(this, EventArgs.Empty);
				RefreshBrushes();
			}
		}

		/// <summary>
		/// Gets or sets the inner waveform blend colors.
		/// </summary>
		/// <value>
		/// The inner blend colors.
		/// </value>
		[Category("FMOD#")][Description("Define the colors used for the gradient of the inner waveform.")]
		[TypeConverter(typeof(ExpandableObjectConverter))]
		public ColorBlend InnerBlendColors
		{
			get { return _innerBlendColors; }
			set
			{
				_innerBlendColors = value;
				BlendColorsChanged?.Invoke(this, EventArgs.Empty);
				RefreshBrushes();
			}
		}

		/// <summary>
		/// Gets or sets the outer waveform gradient mode.
		/// </summary>
		/// <value>
		/// The outer gradient mode.
		/// </value>
		[Category("FMOD#")][Description("Define the direction the the gradient is drawn on the outer waveform.")]
		[TypeConverter(typeof(EnumConverter)), DefaultValue(typeof(LinearGradientMode), "Vertical")]
		public LinearGradientMode OuterGradientMode
		{
			get { return _outerGradientMode; }
			set
			{
				_outerGradientMode = value;
				GradientModeChanged?.Invoke(this, EventArgs.Empty);
				RefreshBrushes();
			}
		}

		/// <summary>
		/// Gets or sets the inner waveform gradient mode.
		/// </summary>
		/// <value>
		/// The inner gradient mode.
		/// </value>
		[Category("FMOD#")][Description("Define the direction the the gradient is drawn on the inner waveform.")]
		[TypeConverter(typeof(EnumConverter)), DefaultValue(typeof(LinearGradientMode), "Vertical")]
		public LinearGradientMode InnerGradientMode
		{
			get { return _innerGradientMode; }
			set
			{
				_innerGradientMode = value;
				GradientModeChanged?.Invoke(this, EventArgs.Empty);
				RefreshBrushes();
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether the position cursor is visible.
		/// </summary>
		/// <value>
		///   <c>true</c> if position cursor is visible; otherwise, <c>false</c>.
		/// </value>
		[Category("FMOD#"), DefaultValue(true)]
		[Description("Value indicating whether the position cursor is visible.")]
		public bool PositionCursorVisible
		{
			get { return positionCursor.Visible; }
			set { positionCursor.Visible = value; }
		}

		/// <summary>
		/// Gets the position cursor.
		/// </summary>
		/// <value>
		/// The position cursor.
		/// </value>
		[Category("FMOD#")][Description("Properties for the position cursor.")]
		[TypeConverter(typeof(ExpandableObjectConverter))]
		public Button PositionCursor
		{
			get { return positionCursor; }
		}


		private bool _resizing;
		private bool _innerWaveVisible = true;
		private GraphicsPath _maxPeakPath;
		private GraphicsPath _avgPeakPath;
		private LinearGradientBrush _outerBrush;
		private LinearGradientBrush _innerBrush;
		private LinearGradientMode _outerGradientMode = LinearGradientMode.Vertical;
		private LinearGradientMode _innerGradientMode = LinearGradientMode.Vertical;
		
		private ColorBlend _outerBlendColors = new ColorBlend(3)
		{
			Colors = new[] { Color.DarkRed, Color.Yellow, Color.DarkRed },
			Positions = new[] { 0.0f, 0.5f, 1.0f }
		};
		private ColorBlend _innerBlendColors = new ColorBlend(3)
		{
			Colors = new[] { Color.Blue, Color.White, Color.Blue },
			Positions = new[] { 0.0f, 0.5f, 1.0f }
		};

		


		public WaveForm()
		{
			InitializeComponent();
			if (LicenseManager.UsageMode == LicenseUsageMode.Designtime)
				CreateSample();
		}

		private void CreateSample()
		{
			var rand = new Random();
			var points = new PointF[200];
			for (var i = 0; i < 200; i++)
				points[i] = new PointF(i / 200.0f, Math.Max((float)rand.NextDouble(), 0.95f));
			_maxPeakPath = new GraphicsPath();
			_maxPeakPath.AddLines(points);
			_avgPeakPath = (GraphicsPath) _maxPeakPath.Clone();
			BlendColorsChanged += (s, e) => RefreshBrushes();
			RefreshBrushes();
		}

		public void UpdateProgress(float progress)
		{
			var x = (Width * progress.Clamp(0.0f, 1.0f)) - (positionCursor.Width / 2.0f); 
			positionCursor.Location = new Point((int)Math.Round(x), 0);
		}

		protected void RefreshBrushes()
		{
			_outerBrush?.Dispose();
			_outerBrush = new LinearGradientBrush(ClientRectangle, Color.Transparent, Color.Transparent, OuterGradientMode)
			{
				InterpolationColors = _outerBlendColors
			};
			_innerBrush?.Dispose();
			var rect = ClientRectangle;
			rect.Height /= 2;
			rect.Y = rect.Height / 2;
			_innerBrush = new LinearGradientBrush(rect, Color.Transparent, Color.Transparent, OuterGradientMode)
			{
				InterpolationColors = _innerBlendColors
			};
			Refresh();
		}

		protected override void OnLoad(EventArgs e)
		{
			var form = FindForm();
			if (form != null)
			{
				form.ResizeBegin += (s, ev) =>
				{
					_resizing = true;
					using (var gfx = CreateGraphics())
						gfx.Clear(BackColor);
				}; 
				form.ResizeEnd += (s, ev) =>
				{
					_resizing = false;
					RefreshBrushes();
				};
			}
			RefreshBrushes();
			base.OnLoad(e);
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			e.Graphics.Clear(BackColor);
			if (_maxPeakPath == null || _resizing)
			{
				base.OnPaint(e);
				return;
			}
			var halfHeight = (float) ClientSize.Height / 2.0f;
			using (var maxPath = (GraphicsPath) _maxPeakPath.Clone())
			{
				using (var matrix = new Matrix())
				{
					matrix.Translate(0.0f, halfHeight);
					matrix.Scale(ClientSize.Width, halfHeight);
					maxPath.Transform(matrix);
					e.Graphics.FillPath(_outerBrush, maxPath);
					if (!_innerWaveVisible) 
						return;
					using (var avgPath = (GraphicsPath) _avgPeakPath.Clone())
					{
						matrix.Scale(1.0f, 0.5f);
						avgPath.Transform(matrix);
						e.Graphics.FillPath(_innerBrush, avgPath);
					}
				}
			}
		}

		public void LoadSound(Sound sound)
		{
			SoundChanged?.Invoke(this, EventArgs.Empty);
			WaveformPathFactory.Create(sound, out _maxPeakPath, out _avgPeakPath);
			RefreshBrushes();
		}

		public void LoadSound(FmodSystem system, string filename)
		{
			SoundChanged?.Invoke(this, EventArgs.Empty);
			using (var sound = system.CreateStream(filename))
				WaveformPathFactory.Create(sound, out _maxPeakPath, out _avgPeakPath);
			RefreshBrushes();
		}
	}
}
