#region License

// EnumFlagsConverter.cs is distributed under the Microsoft Public License (MS-PL)
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
// Created 7:06 PM 02/22/2018

#endregion

#region Using Directives

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;

#endregion

namespace FMOD.NET.Controls
{
	/// <inheritdoc />
	/// <summary>
	///     Flags enumeration type converter.
	/// </summary>
	internal class EnumFlagsConverter : EnumConverter
	{
		#region Constructors

		/// <inheritdoc />
		/// <summary>
		///     Initializes a new instance of the <see cref="EnumFlagsConverter" /> class.
		/// </summary>
		/// <param name="type">
		///     A <see cref="T:System.Type" /> that represents the type of enumeration to associate with this
		///     enumeration converter.
		/// </param>
		public EnumFlagsConverter(Type type) : base(type)
		{
		}

		#endregion

		#region Methods

		/// <inheritdoc />
		/// <summary>
		///     Returns a collection of properties for the type of array specified by the value parameter, using the specified
		///     context and attributes.
		/// </summary>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
		/// <param name="value">An <see cref="T:System.Object" /> that specifies the type of array for which to get properties.</param>
		/// <param name="attributes">An array of type <see cref="T:System.Attribute" /> that is used as a filter.</param>
		/// <returns>
		///     A <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> with the properties that are exposed for this
		///     data type, or null if there are no properties.
		/// </returns>
		public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value,
			Attribute[] attributes)
		{
			if (context == null)
				return base.GetProperties(null, value, attributes);
			var myType = value.GetType();
			var myNames = Enum.GetNames(myType);
			var myValues = Enum.GetValues(myType);
			var myCollection = new PropertyDescriptorCollection(null);
			for (var i = 0; i < myNames.Length; i++)
				if ((int) myValues.GetValue(i) != 0 && myNames[i] != "All")
					myCollection.Add(new EnumFieldDescriptor(myType, myNames[i], context));
			return myCollection;
		}

		/// <inheritdoc />
		/// <summary>
		///     Returns whether this object supports properties, using the specified context.
		/// </summary>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
		/// <returns>
		///     true if <see cref="M:System.ComponentModel.TypeConverter.GetProperties(System.Object)" /> should be called to find
		///     the properties of this object; otherwise, false.
		/// </returns>
		public override bool GetPropertiesSupported(ITypeDescriptorContext context)
		{
			return context != null || base.GetPropertiesSupported(null);
		}

		/// <inheritdoc />
		/// <summary>
		///     Gets a value indicating whether this object supports a standard set of values that can be picked from a list using
		///     the specified context.
		/// </summary>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
		/// <returns>
		///     true because <see cref="M:System.ComponentModel.TypeConverter.GetStandardValues" /> should be called to find a
		///     common set of values the object supports. This method never returns false.
		/// </returns>
		public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
		{
			return false;
		}

		#endregion

		/// <inheritdoc />
		/// <summary>
		///     This class represents an enumeration field in the property grid.
		/// </summary>
		protected class EnumFieldDescriptor : SimplePropertyDescriptor
		{
			/// <summary>
			///     Stores the context which the enumeration field descriptor was created in.
			/// </summary>
			private readonly ITypeDescriptorContext _context;

			private const BindingFlags FLAGS = BindingFlags.Default | BindingFlags.Instance | BindingFlags.NonPublic |
			                                   BindingFlags.Public;

			#region Constructors

			/// <inheritdoc />
			/// <summary>
			///     Creates an instance of the enumeration field descriptor class.
			/// </summary>
			/// <param name="componentType">The type of the enumeration.</param>
			/// <param name="name">The name of the enumeration field.</param>
			/// <param name="context">The current context.</param>
			public EnumFieldDescriptor(Type componentType, string name, ITypeDescriptorContext context) : base(componentType,
				name, typeof(bool))
			{
				_context = context;
			}

			#endregion

			#region Properties

			/// <inheritdoc />
			/// <summary>
			///     Gets the collection of attributes for this member.
			/// </summary>
			public override AttributeCollection Attributes => new AttributeCollection(RefreshPropertiesAttribute.Repaint);

			#endregion

			#region Methods

			/// <inheritdoc />
			/// <summary>
			///     Returns whether resetting the component changes the value of the component.
			/// </summary>
			/// <param name="component">The component to test for reset capability.</param>
			/// <returns>
			///     true if resetting the component changes the value of the component; otherwise, false.
			/// </returns>
			public override bool CanResetValue(object component)
			{
				return ShouldSerializeValue(component);
			}

			/// <inheritdoc />
			/// <summary>
			///     When overridden in a derived class, gets the current value of the property on a component.
			/// </summary>
			/// <param name="component">The component with the property for which to retrieve the value.</param>
			/// <returns>
			///     The value of a property for a given component.
			/// </returns>
			public override object GetValue(object component)
			{
				return ((int) component & (int) Enum.Parse(ComponentType, Name)) != 0;
			}

			/// <inheritdoc />
			/// <summary>
			///     Resets the value for this property of the component.
			/// </summary>
			/// <param name="component">The component with the property value to be reset.</param>
			public override void ResetValue(object component)
			{
				SetValue(component, GetDefaultValue());
			}

			/// <inheritdoc />
			/// <summary>
			///     When overridden in a derived class, sets the value of the component to a different value.
			/// </summary>
			/// <param name="component">The component with the property value that is to be set.</param>
			/// <param name="value">The new value.</param>
			public override void SetValue(object component, object value)
			{
				var myValue = (bool) value;
				int myNewValue;
				if (myValue)
					myNewValue = (int) component | (int) Enum.Parse(ComponentType, Name);
				else
					myNewValue = (int) component & ~(int) Enum.Parse(ComponentType, Name);
				var myField = component.GetType().GetField("value__", BindingFlags.Instance | BindingFlags.Public);
				if (myField != null)
					myField.SetValue(component, myNewValue);
				_context.PropertyDescriptor?.SetValue(_context.Instance, component);
			}

			/// <inheritdoc />
			/// <summary>
			///     Returns whether the value of this property can persist.
			/// </summary>
			/// <param name="component">The component with the property that is to be examined for persistence.</param>
			/// <returns>
			///     true if the value of the property can persist; otherwise, false.
			/// </returns>
			public override bool ShouldSerializeValue(object component)
			{
				Debug.Assert(component != null, nameof(component) + " != null");
				// ReSharper disable once PossibleNullReferenceException
				return (bool) GetValue(component) != GetDefaultValue();
			}

			/// <summary>
			///     Retrieves the enumerations field’s default value.
			/// </summary>
			private bool GetDefaultValue()
			{
				object defaultValue = null;
				if (_context.PropertyDescriptor != null)
				{
					var propertyName = _context.PropertyDescriptor.Name;
					var componentType = _context.PropertyDescriptor.ComponentType;
					if (componentType != null)
					{
						// ReSharper disable once AssignNullToNotNullAttribute
						var myDefaultValueAttribute =
							(DefaultValueAttribute) Attribute.GetCustomAttribute(componentType.GetProperty(propertyName, FLAGS),
								typeof(DefaultValueAttribute));
						if (myDefaultValueAttribute != null)
							defaultValue = myDefaultValueAttribute.Value;
					}
				}
				if (defaultValue != null)
					return ((int) defaultValue & (int) Enum.Parse(ComponentType, Name)) != 0;
				return false;
			}

			#endregion
		}
	}
}