#if FINISH_LATER

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FMOD.NET.Controls
{
	public class EnumFlagCheckList : CheckedListBox
	{
		public class EnumFlagCheckListItem
		{
			public Enum Value { get; }

			public string Caption { get; } = "";

			public string Name { get; }

			public EnumFlagCheckListItem(Type enumV)
			{
				var type = Enum.GetUnderlyingType(enumValue)
				Value = enumValue;
				Name = Enum.GetName(typeof(enumValue), enumValue);
				var info = typeof(T).GetMember(Name);
				var attr = info[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
				if (attr.Length > 0)
					Caption = (attr[0] as DescriptionAttribute)?.Description;
			}
		}


		private Type _enumType;


		public EnumFlagCheckList()
		{
			CheckOnClick = true;
			BorderStyle = BorderStyle.FixedSingle;
		}

		public void Populate<T>(T enumType)
		{
			_enumType = typeof(T);
			Items.Clear();
			foreach (var name in Enum.GetNames(_enumType))
				Items.Add(name);
		}

		public Enum Value 
			{
			get
			{
				Enum value = 0;
				foreach (var item in )
			}
			
			
			set; }

	}
}

#endif
