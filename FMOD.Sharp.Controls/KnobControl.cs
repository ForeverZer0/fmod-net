using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace FMOD.NET.Controls
{
	// A delegate type for hooking up ValueChanged notifications. 
	public delegate void ValueChangedEventHandler(object sender);

	/// <summary>
	///     Summary description for KnobControl.
	/// </summary>
	public class KnobControl : UserControl
	{
		#region Constants & Fields

		private bool _drawDivInside;

		#endregion

		#region Constants & Fields

		private float _endAngle;

		#endregion

		#region Constants & Fields

		private Color _knobBackColor;

		#endregion

		#region Constants & Fields

		private KnobPointerStyle _knobPointerStyle = KnobPointerStyle.Circle;

		#endregion

		#region Constants & Fields

		private int _largeChange = 5;

		#endregion

		#region Constants & Fields

		private int _maximum = 25;

		#endregion

		#region Constants & Fields

		private int _minimum;

		#endregion

		#region Constants & Fields

		private int _mouseWheelBarPartitions = 10;

		#endregion

		#region Constants & Fields

		// Color of the pointer
		private Color _pointerColor = Color.SlateBlue;

		#endregion

		#region Constants & Fields

		private Color _scaleColor;

		#endregion

		#region Constants & Fields

		private int _scaleDivisions;

		#endregion

		#region Constants & Fields

		private int _scaleSubDivisions;

		#endregion

		#region Constants & Fields

		private bool _showLargeScale = true;

		#endregion

		#region Constants & Fields

		private bool _showSmallScale;

		#endregion

		#region Constants & Fields

		private int _smallChange = 1;

		#endregion

		#region Constants & Fields

		private float _startAngle;

		#endregion

		#region Constants & Fields

		private int _value;

		#endregion

		#region Constants & Fields

		private Brush _bKnob;

		#endregion

		#region Constants & Fields

		private Brush _bKnobPoint;

		#endregion

		#region Constants & Fields

		/// <summary>
		///     Required designer variable.
		/// </summary>
		private readonly Container _components = null;

		#endregion

		#region Constants & Fields

		private float _deltaAngle;

		#endregion

		#region Constants & Fields

		private readonly Pen _dottedPen;

		#endregion

		#region Constants & Fields

		private float _drawRatio;

		#endregion

		#region Constants & Fields

		private Graphics _gOffScreen;

		#endregion

		#region Constants & Fields

		private bool _isFocused;

		#endregion

		#region Constants & Fields

		private bool _isKnobRotating;

		#endregion

		#region Constants & Fields

		private Font _knobFont;

		#endregion

		#region Constants & Fields

		//-------------------------------------------------------
		// declare Off screen image and Offscreen graphics       
		//-------------------------------------------------------
		private Image _offScreenImage;

		#endregion

		#region Constants & Fields

		private Point _pKnob;

		#endregion

		#region Constants & Fields

		private Rectangle _rKnob;

		#endregion

		#region Delegates & Events

		//-------------------------------------------------------
		// An event that clients can use to be notified whenever 
		// the Value is Changed.                                 
		//-------------------------------------------------------
		public event ValueChangedEventHandler ValueChanged;

		#endregion

		#region Constructors & Destructor

		public KnobControl()
		{
			// This call is required by the Windows.Forms Form Designer.
			_dottedPen = new Pen(ControlHelper.getDarkColor(BackColor, 40));
			_dottedPen.DashStyle = DashStyle.Dash;
			_dottedPen.DashCap = DashCap.Flat;

			InitializeComponent();

			_knobFont = new Font(Font.FontFamily, Font.Size);

			// Properties initialisation

			// "start angle" and "end angle" possible values:

			// 90 = bottom (minimum value for "start angle")
			// 180 = left
			// 270 = top
			// 360 = right
			// 450 = bottom again (maximum value for "end angle")

			// So the couple (90, 450) will give an entire circle and the couple (180, 360) will give half a circle.

			_startAngle = 135;
			_endAngle = 405;
			_deltaAngle = _endAngle - _startAngle;

			_minimum = 0;
			_maximum = 100;
			_scaleDivisions = 11;
			_scaleSubDivisions = 4;
			_mouseWheelBarPartitions = 10;

			_scaleColor = Color.Black;
			_knobBackColor = Color.White;

			SetDimensions();
		}

		#endregion

		#region Properties & Indexers

		/// <summary>
		///     Draw string graduations inside or outside knob circle
		/// </summary>
		[Description("Draw graduation strings inside or outside the knob circle")]
		[Category("KnobControl")]
		[DefaultValue(false)]
		public bool DrawDivInside
		{
			get => _drawDivInside;
			set
			{
				_drawDivInside = value;
				Invalidate();
			}
		}

		/// <summary>
		///     End angle to display graduations
		/// </summary>
		/// <value>The end angle to display graduations.</value>
		[Description("Set the end angle to display graduations (max 450)")]
		[Category("KnobControl")]
		[DefaultValue(405)]
		public float EndAngle
		{
			get => _endAngle;
			set
			{
				if (value <= 450 && value > _startAngle)
				{
					_endAngle = value;
					_deltaAngle = _endAngle - _startAngle;
					Invalidate();
				}
			}
		}

		/// <summary>
		///     Color of graduations
		/// </summary>
		[Description("Color of knob")]
		[Category("KnobControl")]
		public Color KnobBackColor
		{
			get => _knobBackColor;
			set
			{
				_knobBackColor = value;
				SetDimensions();
				Invalidate();
			}
		}


		/// <summary>
		///     Style of pointer: circle or line
		/// </summary>
		[Description("Set the style of the knob pointer: a circle or a line")]
		[Category("KnobControl")]
		public KnobPointerStyle KnobStyle
		{
			get => _knobPointerStyle;
			set
			{
				_knobPointerStyle = value;
				Invalidate();
			}
		}

		/// <summary>
		///     value set for large change
		/// </summary>
		[Description("set the value for the large changes")]
		[Category("KnobControl")]
		public int LargeChange
		{
			get => _largeChange;
			set
			{
				_largeChange = value;
				Invalidate();
			}
		}

		/// <summary>
		///     Maximum value for knob control
		/// </summary>
		[Description("set the maximum value for the knob control")]
		[Category("KnobControl")]
		public int Maximum
		{
			get => _maximum;
			set
			{
				if (value > _minimum)
				{
					_maximum = value;


					if (_scaleSubDivisions > 0 && _scaleDivisions > 0 &&
					    (_maximum - _minimum) / (_scaleSubDivisions * _scaleDivisions) <= 0)
						_showSmallScale = false;

					SetDimensions();
					Invalidate();
				}
			}
		}

		/// <summary>
		///     Minimum Value for knob Control
		/// </summary>
		[Description("set the minimum value for the knob control")]
		[Category("KnobControl")]
		public int Minimum
		{
			get => _minimum;
			set
			{
				_minimum = value;
				Invalidate();
			}
		}


		/// <summary>
		///     Gets or sets the mouse wheel bar partitions.
		/// </summary>
		/// <value>The mouse wheel bar partitions.</value>
		/// <exception cref="T:System.ArgumentOutOfRangeException">exception thrown when value isn't greather than zero</exception>
		[Description("Set to how many parts is bar divided when using mouse wheel")]
		[Category("KnobControl")]
		[DefaultValue(10)]
		public int MouseWheelBarPartitions
		{
			get => _mouseWheelBarPartitions;
			set
			{
				if (value > 0)
					_mouseWheelBarPartitions = value;
				else throw new ArgumentOutOfRangeException("MouseWheelBarPartitions has to be greather than zero");
			}
		}

		/// <summary>
		///     Color of the button
		/// </summary>
		[Description("set the color of the pointer")]
		[Category("KnobControl")]
		public Color PointerColor
		{
			get => _pointerColor;
			set
			{
				_pointerColor = value;
				Invalidate();
			}
		}

		/// <summary>
		///     Color of graduations
		/// </summary>
		[Description("Color of graduations")]
		[Category("KnobControl")]
		public Color ScaleColor
		{
			get => _scaleColor;
			set
			{
				_scaleColor = value;
				Invalidate();
			}
		}

		/// <summary>
		///     How many divisions of maximum?
		/// </summary>
		[Description("Set the number of intervals between minimum and maximum")]
		[Category("KnobControl")]
		public int ScaleDivisions
		{
			get => _scaleDivisions;
			set
			{
				_scaleDivisions = value;
				Invalidate();
			}
		}

		/// <summary>
		///     How many subdivisions for each division
		/// </summary>
		[Description("Set the number of subdivisions between main divisions of graduation.")]
		[Category("KnobControl")]
		public int ScaleSubDivisions
		{
			get => _scaleSubDivisions;
			set
			{
				if (value > 0 && _scaleDivisions > 0 && (_maximum - _minimum) / (value * _scaleDivisions) > 0)
				{
					_scaleSubDivisions = value;
					Invalidate();
				}
			}
		}

		/// <summary>
		///     Shows Large Scale marking
		/// </summary>
		[Description("Show or hide graduations")]
		[Category("KnobControl")]
		public bool ShowLargeScale
		{
			get => _showLargeScale;
			set
			{
				_showLargeScale = value;
				// need to redraw
				SetDimensions();

				Invalidate();
			}
		}

		/// <summary>
		///     Shows Small Scale marking.
		/// </summary>
		[Description("Show or hide subdivisions of graduations")]
		[Category("KnobControl")]
		public bool ShowSmallScale
		{
			get => _showSmallScale;
			set
			{
				if (value)
				{
					if (_scaleDivisions > 0 && _scaleSubDivisions > 0 &&
					    (_maximum - _minimum) / (_scaleSubDivisions * _scaleDivisions) > 0)
					{
						_showSmallScale = value;
						Invalidate();
					}
				}
				else
				{
					_showSmallScale = value;
					// need to redraw 
					Invalidate();
				}
			}
		}

		/// <summary>
		///     value set for small change.
		/// </summary>
		[Description("set the minimum value for the small changes")]
		[Category("KnobControl")]
		public int SmallChange
		{
			get => _smallChange;
			set
			{
				_smallChange = value;
				Invalidate();
			}
		}

		/// <summary>
		///     Start angle to display graduations
		/// </summary>
		/// <value>The start angle to display graduations.</value>
		[Description("Set the start angle to display graduations (min 90)")]
		[Category("KnobControl")]
		[DefaultValue(135)]
		public float StartAngle
		{
			get => _startAngle;
			set
			{
				if (value >= 90 && value < _endAngle)
				{
					_startAngle = value;
					_deltaAngle = _endAngle - StartAngle;
					Invalidate();
				}
			}
		}

		/// <summary>
		///     Current Value of knob control
		/// </summary>
		[Description("set the current value of the knob control")]
		[Category("KnobControl")]
		public int Value
		{
			get => _value;
			set
			{
				_value = value;
				// need to redraw 
				Invalidate();
				// call delegate  
				OnValueChanged(this);
			}
		}

		#endregion

		#region Methods

		/// <summary>
		///     Clean up any resources being used.
		/// </summary>
		protected override void Dispose(bool disposing)
		{
			if (disposing)
				if (_components != null)
					_components.Dispose();
			base.Dispose(disposing);
		}


		//----------------------------------------------------------
		// we need to override IsInputKey method to allow user to   
		// use up, down, right and bottom keys other wise using this
		// keys will change focus from current object to another    
		// object on the form                                       
		//----------------------------------------------------------
		protected override bool IsInputKey(Keys key)
		{
			switch (key)
			{
				case Keys.Up:
				case Keys.Down:
				case Keys.Right:
				case Keys.Left:
					return true;
			}
			return base.IsInputKey(key);
		}

		/// <summary>
		///     Key down event: change value
		/// </summary>
		/// <param name="e"></param>
		protected override void OnKeyDown(KeyEventArgs e)
		{
			if (_isFocused)
				if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Right)
				{
					if (_value < _maximum) Value = _value + 1;
					Refresh();
				}
				else if (e.KeyCode == Keys.Down || e.KeyCode == Keys.Left)
				{
					if (_value > _minimum) Value = _value - 1;
					Refresh();
				}
		}


		/*
	    protected override void OnEnter(EventArgs e)
		{
			
	        Invalidate();

			base.OnEnter(new EventArgs());
		}
	    */

		/// <summary>
		///     Leave event: disallow knob rotation
		/// </summary>
		/// <param name="e"></param>
		protected override void OnLeave(EventArgs e)
		{
			// unselect the control (remove dotted border)
			_isFocused = false;
			_isKnobRotating = false;
			Invalidate();

			base.OnLeave(new EventArgs());
		}

		/// <summary>
		///     Mouse down event: select control
		/// </summary>
		/// <param name="e"></param>
		protected override void OnMouseDown(MouseEventArgs e)
		{
			if (ControlHelper.isPointinRectangle(new Point(e.X, e.Y), _rKnob))
				if (_isFocused)
				{
					// was already selected
					// Start Rotation of knob only if it was selected before        
					_isKnobRotating = true;
				}
				else
				{
					// Was not selected before => select it
					Focus();
					_isFocused = true;
					_isKnobRotating = false; // disallow rotation, must click again
					// draw dotted border to show that it is selected
					Invalidate();
				}
		}

		/// <summary>
		///     Mouse move event: drag the pointer to the mouse position
		/// </summary>
		/// <param name="e"></param>
		protected override void OnMouseMove(MouseEventArgs e)
		{
			//--------------------------------------
			//  Following Handles Knob Rotating     
			//--------------------------------------
			if (e.Button == MouseButtons.Left && _isKnobRotating)
			{
				Cursor = Cursors.Hand;
				var p = new Point(e.X, e.Y);
				var posVal = GetValueFromPosition(p);
				Value = posVal;
			}
		}

		/// <summary>
		///     Mouse up event: display new value
		/// </summary>
		/// <param name="e"></param>
		protected override void OnMouseUp(MouseEventArgs e)
		{
			if (ControlHelper.isPointinRectangle(new Point(e.X, e.Y), _rKnob))
				if (_isFocused && _isKnobRotating)
				{
					// change value is allowed only only after 2nd click                   
					Value = GetValueFromPosition(new Point(e.X, e.Y));
				}
				else
				{
					// 1st click = only focus
					_isFocused = true;
					_isKnobRotating = true;
				}
			Cursor = Cursors.Default;
		}

		/// <summary>
		///     Mousewheel: change value
		/// </summary>
		/// <param name="e"></param>
		protected override void OnMouseWheel(MouseEventArgs e)
		{
			base.OnMouseWheel(e);

			if (_isFocused && _isKnobRotating && ControlHelper.isPointinRectangle(new Point(e.X, e.Y), _rKnob))
			{
				// the Delta value is always 120, as explained in MSDN
				var v = e.Delta / 120 * (_maximum - _minimum) / _mouseWheelBarPartitions;
				SetProperValue(Value + v);

				// Avoid to send MouseWheel event to the parent container
				((HandledMouseEventArgs) e).Handled = true;
			}
		}

		/// <summary>
		///     Paint event: draw all
		/// </summary>
		/// <param name="e"></param>
		protected override void OnPaint(PaintEventArgs e)
		{
			var g = e.Graphics;
			// Set background color of Image...            
			_gOffScreen.Clear(BackColor);
			// Fill knob Background to give knob effect....
			_gOffScreen.FillEllipse(_bKnob, _rKnob);
			// Set antialias effect on                     
			_gOffScreen.SmoothingMode = SmoothingMode.AntiAlias;
			// Draw border of knob                         
			_gOffScreen.DrawEllipse(new Pen(BackColor), _rKnob);

			//if control is focused 
			if (_isFocused)
				_gOffScreen.DrawEllipse(_dottedPen, _rKnob);

			// DrawPointer
			DrawPointer(_gOffScreen);

			//---------------------------------------------
			// darw small and large scale                  
			//---------------------------------------------
			DrawDivisions(_gOffScreen, _rKnob);

			// Drawimage on screen                    
			g.DrawImage(_offScreenImage, 0, 0);
		}

		protected override void OnPaintBackground(PaintEventArgs e)
		{
			// Empty To avoid Flickring due do background Drawing.
		}

		//-------------------------------------------------------
		// Invoke the ValueChanged event; called  when value     
		// is changed                                            
		//-------------------------------------------------------
		protected virtual void OnValueChanged(object sender)
		{
			if (ValueChanged != null)
				ValueChanged(sender);
		}

		/// <summary>
		///     Draw graduations
		/// </summary>
		/// <param name="gr"></param>
		/// <param name="rc"></param>
		/// <returns></returns>
		private bool DrawDivisions(Graphics gr, RectangleF rc)
		{

			float cx = _pKnob.X;
			float cy = _pKnob.Y;

			var w = rc.Width;
			var h = rc.Height;

			float tx;
			float ty;

			var incr = ControlHelper.GetRadian((_endAngle - _startAngle) / ((_scaleDivisions - 1) * (_scaleSubDivisions + 1)));
			var currentAngle = ControlHelper.GetRadian(_startAngle);

			float radius = _rKnob.Width / 2;
			float rulerValue = _minimum;


			var penL = new Pen(_scaleColor, 2 * _drawRatio);
			var penS = new Pen(_scaleColor, 1 * _drawRatio);

			var br = new SolidBrush(_scaleColor);

			var ptStart = new PointF(0, 0);
			var ptEnd = new PointF(0, 0);
			var n = 0;

			if (_showLargeScale)
				for (; n < _scaleDivisions; n++)
				{
					// draw divisions
					ptStart.X = (float) (cx + radius * Math.Cos(currentAngle));
					ptStart.Y = (float) (cy + radius * Math.Sin(currentAngle));

					ptEnd.X = (float) (cx + (radius + w / 50) * Math.Cos(currentAngle));
					ptEnd.Y = (float) (cy + (radius + w / 50) * Math.Sin(currentAngle));

					gr.DrawLine(penL, ptStart, ptEnd);


					//Draw graduations Strings                    
					var fSize = 6F * _drawRatio;
					if (fSize < 6)
						fSize = 6;
					var font = new Font(Font.FontFamily, fSize);

					var val = Math.Round(rulerValue);
					var str = String.Format("{0,0:D}", (int) val);
					var size = gr.MeasureString(str, font);

					if (_drawDivInside)
					{
						// graduations strings inside the knob
						tx = (float) (cx + (radius - 11 * _drawRatio) * Math.Cos(currentAngle));
						ty = (float) (cy + (radius - 11 * _drawRatio) * Math.Sin(currentAngle));
					}
					else
					{
						// graduation strings outside the knob
						tx = (float) (cx + (radius + 11 * _drawRatio) * Math.Cos(currentAngle));
						ty = (float) (cy + (radius + 11 * _drawRatio) * Math.Sin(currentAngle));
					}

					gr.DrawString(str,
						font,
						br,
						tx - (float) (size.Width * 0.5),
						ty - (float) (size.Height * 0.5));

					rulerValue += (_maximum - _minimum) / (_scaleDivisions - 1);

					if (n == _scaleDivisions - 1)
					{
						font.Dispose();
						break;
					}


					// Subdivisions

					if (_scaleDivisions <= 0)
						currentAngle += incr;
					else
						for (var j = 0; j <= _scaleSubDivisions; j++)
						{
							currentAngle += incr;

							// if user want to display small graduations
							if (_showSmallScale)
							{
								ptStart.X = (float) (cx + radius * Math.Cos(currentAngle));
								ptStart.Y = (float) (cy + radius * Math.Sin(currentAngle));
								ptEnd.X = (float) (cx + (radius + w / 50) * Math.Cos(currentAngle));
								ptEnd.Y = (float) (cy + (radius + w / 50) * Math.Sin(currentAngle));

								gr.DrawLine(penS, ptStart, ptEnd);
							}
						}


					font.Dispose();
				}

			return true;
		}

		/// <summary>
		///     Draw the pointer of the knob (a small button inside the main button)
		/// </summary>
		/// <param name="gr"></param>
		private void DrawPointer(Graphics gr)
		{
			try
			{
				if (_knobPointerStyle == KnobPointerStyle.Line)
				{
					float radius = _rKnob.Width / 2;

					var l = (int) radius / 2;
					var w = l / 4;
					var pt = GetKnobLine(l);

					gr.DrawLine(new Pen(_pointerColor, w), pt[0], pt[1]);
				}
				else
				{
					var w = 0;
					var h = 0;

					// Size of pointer
					w = _rKnob.Width / 10;
					if (w < 7)
						w = 7;

					h = w;

					var arrow = GetKnobPosition(w);

					// Draw pointer arrow that shows knob position             
					var rPointer = new Rectangle(arrow.X - w / 2, arrow.Y - w / 2, w, h);


					ControlHelper.DrawInsetCircle(ref gr, rPointer, new Pen(_pointerColor));
					gr.FillEllipse(_bKnobPoint, rPointer);
				}
			}
			catch (Exception ex)
			{
				Console.Write(ex.Message);
			}
		}

		/// <summary>
		///     return 2 points of a line starting from the center of the knob to the periphery
		/// </summary>
		/// <param name="l"></param>
		/// <returns></returns>
		private Point[] GetKnobLine(int l)
		{
			var pret = new Point[2];

			float cx = _pKnob.X;
			float cy = _pKnob.Y;


			float radius = _rKnob.Width / 2;

			var degree = _deltaAngle * Value / (_maximum - _minimum);
			degree = ControlHelper.GetRadian(degree + _startAngle);

			var pos = new Point(0, 0);

			pos.X = (int) (cx + (radius - _drawRatio * 10) * Math.Cos(degree));
			pos.Y = (int) (cy + (radius - _drawRatio * 10) * Math.Sin(degree));

			pret[0] = new Point(pos.X, pos.Y);

			pos.X = (int) (cx + (radius - _drawRatio * 10 - l) * Math.Cos(degree));
			pos.Y = (int) (cy + (radius - _drawRatio * 10 - l) * Math.Sin(degree));

			pret[1] = new Point(pos.X, pos.Y);

			return pret;
		}

		/// <summary>
		///     gets knob position that is to be drawn on control.
		/// </summary>
		/// <returns>Point that describes current knob position</returns>
		private Point GetKnobPosition(int l)
		{
			float cx = _pKnob.X;
			float cy = _pKnob.Y;


			float radius = _rKnob.Width / 2;

			var degree = _deltaAngle * Value / (_maximum - _minimum);
			degree = ControlHelper.GetRadian(degree + _startAngle);

			var pos = new Point(0, 0);

			pos.X = (int) (cx + (radius - 11 * _drawRatio) * Math.Cos(degree));
			pos.Y = (int) (cy + (radius - 11 * _drawRatio) * Math.Sin(degree));

			return pos;
		}

		/// <summary>
		///     converts geometrical position into value..
		/// </summary>
		/// <param name="p">Point that is to be converted</param>
		/// <returns>Value derived from position</returns>
		private int GetValueFromPosition(Point p)
		{
			float degree = 0;
			var v = 0;

			if (p.X <= _pKnob.X)
			{
				degree = (_pKnob.Y - p.Y) / (float) (_pKnob.X - p.X);
				degree = (float) Math.Atan(degree);

				degree = degree * (float) (180 / Math.PI) + (180 - _startAngle);
			}
			else if (p.X > _pKnob.X)
			{
				degree = (p.Y - _pKnob.Y) / (float) (p.X - _pKnob.X);
				degree = (float) Math.Atan(degree);

				degree = degree * (float) (180 / Math.PI) + 360 - _startAngle;
			}

			// round to the nearest value (when you click just before or after a graduation!)
			v = (int) Math.Round(degree * (_maximum - _minimum) / _deltaAngle);


			if (v > _maximum) v = _maximum;
			if (v < _minimum) v = _minimum;
			return v;
		}


		#region Component Designer generated code

		/// <summary>
		///     Required method for Designer support - do not modify
		///     the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			// 
			// KnobControl
			// 
			this.ImeMode = System.Windows.Forms.ImeMode.On;
			this.Name = "KnobControl";
			this.Resize += new System.EventHandler(this.KnobControl_Resize);
		}

		#endregion

		/// <summary>
		///     Resize event
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void KnobControl_Resize(object sender, EventArgs e)
		{
			SetDimensions();
			//Refresh();
			Invalidate();
		}

		/// <summary>
		///     Set position of button inside its rectangle to insure that divisions will fit.
		/// </summary>
		private void SetDimensions()
		{
			var size = Width;
			Height = size;


			// Rectangle
			float x, y, w, h;
			x = 0;
			y = 0;
			w = Size.Width;
			h = Size.Height;

			// Calculate ratio
			_drawRatio = Math.Min(w, h) / 200;
			if (_drawRatio == 0.0)
				_drawRatio = 1;

			if (_showLargeScale)
			{
				var fSize = 6F * _drawRatio;
				if (fSize < 6)
					fSize = 6;
				_knobFont = new Font(Font.FontFamily, fSize);
				double val = _maximum;
				var str = String.Format("{0,0:D}", (int) val);


				var gr = CreateGraphics();
				var strsize = gr.MeasureString(str, _knobFont);
				var strw = (int) strsize.Width + 4;
				var strh = (int) strsize.Height;

				// allow 10% gap on all side to determine size of knob    
				//this.rKnob = new Rectangle((int)(size * 0.10), (int)(size * 0.15), (int)(size * 0.80), (int)(size * 0.80));
				x = strw;
				//y = x;
				y = 2 * strh;
				w = size - 2 * strw;
				if (w <= 0)
					w = 1;
				h = w;
				_rKnob = new Rectangle((int) x, (int) y, (int) w, (int) h);
				gr.Dispose();
			}
			else
			{
				_rKnob = new Rectangle(0, 0, Width, Height);
			}


			// Center of knob
			_pKnob = new Point(_rKnob.X + _rKnob.Width / 2, _rKnob.Y + _rKnob.Height / 2);

			// create offscreen image                                 
			_offScreenImage = new Bitmap(Width, Height);
			// create offscreen graphics                              
			_gOffScreen = Graphics.FromImage(_offScreenImage);

			// create LinearGradientBrush for creating knob            
			_bKnob = new LinearGradientBrush(
				_rKnob, ControlHelper.getLightColor(_knobBackColor, 55), ControlHelper.getDarkColor(_knobBackColor, 55),
				LinearGradientMode.ForwardDiagonal);

			// create LinearGradientBrush for knobPoint                
			_bKnobPoint = new LinearGradientBrush(
				_rKnob, ControlHelper.getLightColor(_pointerColor, 55), ControlHelper.getDarkColor(_pointerColor, 55),
				LinearGradientMode.ForwardDiagonal);
		}

		/// <summary>
		///     Sets the trackbar value so that it wont exceed allowed range.
		/// </summary>
		/// <param name="val">The value.</param>
		private void SetProperValue(int val)
		{
			if (val < _minimum) Value = _minimum;
			else if (val > _maximum) Value = _maximum;
			else Value = val;
		}

		#endregion

		/// <summary>
		///     Styles of pointer button
		/// </summary>
		public enum KnobPointerStyle
		{
			Circle,
			Line
		}
	}
}