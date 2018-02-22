#region License

// Geometry.Events.cs is distributed under the Microsoft Public License (MS-PL)
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
// Created 12:02 AM 02/20/2018

#endregion

#region Using Directives

using System;
using FMOD.NET.Arguments;
using FMOD.NET.Data;

#endregion

namespace FMOD.NET.Core
{
	public partial class Geometry
	{
		#region Events

		/// <summary>
		///     Occurs when the <see cref="Active" /> property has changed.
		/// </summary>
		/// <seealso cref="Active" />
		public event EventHandler ActiveChanged;

		/// <summary>
		///     Occurs when a polygon has been added to the <see cref="Geometry" /> object.
		/// </summary>
		/// <seealso cref="O:FMOD.NET.Core.Geometry.AddPolygon" />
		/// <seealso cref="Polygon" />
		/// <seealso cref="PolygonEventArgs" />
		public event EventHandler<PolygonEventArgs> PolygonAdded;

		/// <summary>
		///     Occurs when the attributes of a <see cref="Polygon" /> have changed.
		/// </summary>
		/// <seealso cref="O:FMOD.NET.Core.Geometry.SetPolygonAttributes" />
		/// <seealso cref="Polygon" />
		/// <seealso cref="PolygonEventArgs" />
		public event EventHandler<PolygonEventArgs> PolygonAttributesChanged;

		/// <summary>
		///     Occurs when a vertex of a <see cref="Polygon" /> have changed.
		/// </summary>
		/// <seealso cref="SetVertex" />
		/// <seealso cref="Polygon" />
		/// <seealso cref="PolygonEventArgs" />
		public event EventHandler<PolygonEventArgs> PolygonVertexChanged;

		/// <summary>
		///     Occurs when the <see cref="Position" /> property has changed.
		/// </summary>
		/// <seealso cref="Position" />
		public event EventHandler PositionChanged;

		/// <summary>
		///     Occurs when the rotation has changed.
		/// </summary>
		/// <seealso cref="Rotation" />
		/// <seealso cref="SetRotation" />
		public event EventHandler RotationChanged;

		/// <summary>
		///     Occurs when <see cref="Scale" /> property has changed.
		/// </summary>
		/// <seealso cref="Scale" />
		public event EventHandler ScaleChanged;

		#endregion

		#region Event Invokers

		/// <summary>
		///     Raises the <see cref="ActiveChanged" /> event.
		/// </summary>
		/// <seealso cref="EventArgs" />
		protected virtual void OnActiveChanged()
		{
			ActiveChanged?.Invoke(this, EventArgs.Empty);
		}

		/// <summary>
		///     Raises the <see cref="PolygonAdded" /> event.
		/// </summary>
		/// <param name="e">The <see cref="PolygonEventArgs" /> instance containing the event data.</param>
		protected virtual void OnPolygonAdded(PolygonEventArgs e)
		{
			PolygonAdded?.Invoke(this, e);
		}

		/// <summary>
		///     Raises the <see cref="PolygonAttributesChanged" /> event.
		/// </summary>
		/// <param name="e">The <see cref="PolygonEventArgs" /> instance containing the event data.</param>
		protected virtual void OnPolygonAttributesChanged(PolygonEventArgs e)
		{
			PolygonAttributesChanged?.Invoke(this, e);
		}

		/// <summary>
		///     Raises the <see cref="PolygonVertexChanged" /> event.
		/// </summary>
		/// <param name="e">The <see cref="PolygonEventArgs" /> instance containing the event data.</param>
		protected virtual void OnPolygonVertexChanged(PolygonEventArgs e)
		{
			PolygonVertexChanged?.Invoke(this, e);
		}

		/// <summary>
		///     Raises the <see cref="PositionChanged" /> event.
		/// </summary>
		/// <seealso cref="EventArgs" />
		protected virtual void OnPositionChanged()
		{
			PositionChanged?.Invoke(this, EventArgs.Empty);
		}

		/// <summary>
		///     Raises the <see cref="RotationChanged" /> event.
		/// </summary>
		/// <seealso cref="EventArgs" />
		protected virtual void OnRotationChanged()
		{
			RotationChanged?.Invoke(this, EventArgs.Empty);
		}

		/// <summary>
		///     Raises the <see cref="ScaleChanged" /> event.
		/// </summary>
		/// <seealso cref="EventArgs" />
		protected virtual void OnScaleChanged()
		{
			ScaleChanged?.Invoke(this, EventArgs.Empty);
		}

		#endregion
	}
}