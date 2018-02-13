using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FMOD.Sharp.Interfaces
{
	public interface IHandle : IDisposable, IEquatable<Handle>
	{
		event EventHandler Disposed;

		bool IsDisposed { get; }
	}
}
