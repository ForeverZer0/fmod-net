using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace FMOD.Sharp.Controls
{
	/// <inheritdoc />
	/// <summary>
	/// </summary>
	/// <seealso cref="T:System.Windows.Forms.NumericUpDown" />
	[Description("NumericUpDown control with units of measurment.")]
	[ToolboxItem(true), ToolboxBitmap(typeof(NumericUpDown))]
	public partial class NumericUnitUpDown : NumericUpDown
	{
		/// <summary>
		/// Gets or sets the units of measurment.
		/// </summary>
		/// <value>
		/// The units.
		/// </value>
		[Category("Appearance"), DefaultValue("%")]
		[Description("Unit of measurement to include with value.")]
		public string Units { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether unit of measurement is prefixed.
		/// </summary>
		/// <value>
		///   <c>true</c> if [unit prefixed]; otherwise, <c>false</c>.
		/// </value>
		[Category("Appearance"), DefaultValue(false)]
		[Description("True to have unit of measurement prefix the value, otherwise false.")]
		public bool UnitPrefixed { get; set; }

		/// <inheritdoc />
		/// <summary>
		/// Initializes a new instance of the <see cref="T:FMOD.Sharp.Controls.NumericUnitUpDown" /> class.
		/// </summary>
		public NumericUnitUpDown()
		{
			InitializeComponent();
		}

		/// <inheritdoc />
		/// <summary>
		/// Displays the current value of the spin box (also known as an up-down control) in the appropriate format.
		/// </summary>
		protected override void UpdateEditText()
		{
			Text = UnitPrefixed ? $"{Units}{Value}" : $"{Value}{Units}";
		}
	}
}
