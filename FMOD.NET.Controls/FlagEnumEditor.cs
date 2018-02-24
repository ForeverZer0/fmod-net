using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Drawing.Design;
using System.Windows.Forms.Design;


namespace Utils
{

	public class FlagCheckedListBox : CheckedListBox
	{
		public FlagCheckedListBox()
		{
			CheckOnClick = true;
		}

		public FlagCheckedListBoxItem Add(int v, string c)
		{
			var item = new FlagCheckedListBoxItem(v,c);
			Items.Add(item);
			return item;
		}

		public FlagCheckedListBoxItem Add(FlagCheckedListBoxItem item)
		{
			Items.Add(item);
			return item;
		}

        protected override void OnItemCheck(ItemCheckEventArgs e)
        {
            base.OnItemCheck(e);

            if (isUpdatingCheckStates)
                return;

            // Get the checked/unchecked item
            var item = Items[e.Index] as FlagCheckedListBoxItem;
            // Update other items
            UpdateCheckedItems(item, e.NewValue);
        }

        // Checks/Unchecks items depending on the give bitvalue
		protected void UpdateCheckedItems(int value)
		{

			isUpdatingCheckStates = true;

            // Iterate over all items
			for(var i=0;i<Items.Count;i++)
			{
				var item = Items[i] as FlagCheckedListBoxItem;

				if(item.value==0)
				{
					SetItemChecked(i,value==0);
				}
				else
				{

					// If the bit for the current item is on in the bitvalue, check it
					if( (item.value & value)== item.value && item.value!=0)
						SetItemChecked(i,true);
						// Otherwise uncheck it
					else
						SetItemChecked(i,false);
				}
			}

			isUpdatingCheckStates = false;

		}

        // Updates items in the checklistbox
        // composite = The item that was checked/unchecked
        // cs = The check state of that item
		protected void UpdateCheckedItems(FlagCheckedListBoxItem composite,CheckState cs)
		{

            // If the value of the item is 0, call directly.
			if(composite.value==0)
				UpdateCheckedItems(0);


            // Get the total value of all checked items
			var sum = 0;
			for(var i=0;i<Items.Count;i++)
			{
				var item = Items[i] as FlagCheckedListBoxItem;

                // If item is checked, add its value to the sum.
				if(GetItemChecked(i))
					sum |= item.value;
			}

            // If the item has been unchecked, remove its bits from the sum
			if(cs==CheckState.Unchecked)
				sum = sum & (~composite.value);
            // If the item has been checked, combine its bits with the sum
			else
				sum |= composite.value;

            // Update all items in the checklistbox based on the final bit value
			UpdateCheckedItems(sum);

		}

		private bool isUpdatingCheckStates = false;

        // Gets the current bit value corresponding to all checked items
		public int GetCurrentValue()
		{
			var sum = 0;

			for(var i=0;i<Items.Count;i++)
			{
				var item = Items[i] as FlagCheckedListBoxItem;

				if( GetItemChecked(i))
					sum |= item.value;
			}

			return sum;
		}

		private Type _type;
		private Enum _value;

		// Adds items to the checklistbox based on the members of the enum
		private void FillEnumMembers()
		{
			foreach ( var name in Enum.GetNames(_type))
			{
				var val = Enum.Parse(_type,name);
				var intVal = (int)Convert.ChangeType(val, typeof(int));

				Add(intVal,name);
			}
		}

		// Checks/unchecks items based on the current value of the enum variable
		private void ApplyEnumValue()
		{
			var intVal = (int)Convert.ChangeType(_value, typeof(int));
			UpdateCheckedItems(intVal);

		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Enum EnumValue
		{
			get
			{
				return (Enum) Enum.ToObject(_type, GetCurrentValue());
			}
			set
			{
                
				Items.Clear();
				_value = value; // Store the current enum value
				_type = value.GetType(); // Store enum type
				FillEnumMembers(); // Add items for enum members
				ApplyEnumValue(); // Check/uncheck items depending on enum value

			}
		}


	}

    // Represents an item in the checklistbox
    public class FlagCheckedListBoxItem
    {
        public FlagCheckedListBoxItem(int v, string c)
        {
            value = v;
            caption = c;
        }

        public override string ToString()
        {
            return caption;
        }

        // Returns true if the value corresponds to a single bit being set
        public bool IsFlag
        {
            get
            {
                return ((value & (value - 1)) == 0);
            }
        }

        // Returns true if this value is a member of the composite bit value
        public bool IsMemberFlag(FlagCheckedListBoxItem composite)
        {
            return (IsFlag && ((value & composite.value) == value));
        }

        public int value;
        public string caption;
    }


    // UITypeEditor for flag enums
	public class FlagEnumUIEditor : UITypeEditor
	{
        // The checklistbox
		private FlagCheckedListBox flagEnumCB;

		public FlagEnumUIEditor()
		{
			flagEnumCB = new FlagCheckedListBox();
			flagEnumCB.BorderStyle = BorderStyle.None;
		}

		public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value) 
		{
			if (context != null
				&& context.Instance != null
				&& provider != null) 
			{

				var edSvc = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));

				if (edSvc != null) 
				{					

					var e = (Enum) Convert.ChangeType(value, context.PropertyDescriptor.PropertyType);
					flagEnumCB.EnumValue = e;
					edSvc.DropDownControl(flagEnumCB);
					return flagEnumCB.EnumValue;

				}
			}
			return null;
		}

		public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context) 
		{
			return UITypeEditorEditStyle.DropDown;			
		}


	}

}
