using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using FMOD.Sharp.Enums;

namespace FMOD.Sharp
{
	[SuppressUnmanagedCodeSecurity]
	[SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
	public static class MemoryManager
	{
		public delegate IntPtr AllocCallback(uint size, MemoryType type, IntPtr sourceStr);

		public delegate void FreeCallback(IntPtr ptr, MemoryType type, IntPtr sourceStr);

		public delegate IntPtr ReallocCallback(IntPtr ptr, uint size, MemoryType type, IntPtr sourceStr);

		public static void Initialize()
		{
			var result = FMOD_Memory_Initialize(IntPtr.Zero, 0, null, null, null, MemoryType.All);
			if (result == Result.OK)
				return;
			throw new FmodException(result, Core.GetResultString(result));
		}

		public static void Initialize(IntPtr memPool, int memPoolSize)
		{
			var result = FMOD_Memory_Initialize(memPool, memPoolSize, null, null, null, MemoryType.All);
			if (result == Result.OK)
				return;
			throw new FmodException(result, Core.GetResultString(result));
		}

		public static void Initialize(AllocCallback userAlloc, ReallocCallback userRealloc, FreeCallback userFree,
			MemoryType type)
		{
			var result = FMOD_Memory_Initialize(IntPtr.Zero, 0, userAlloc, userRealloc, userFree, type);
			if (result == Result.OK)
				return;
			throw new FmodException(result, Core.GetResultString(result));
		}

		public static void GetStats(out int currentAlloc, out int maxAlloc, bool blocking = false)
		{
			var result = FMOD_Memory_GetStats(out currentAlloc, out maxAlloc, blocking);
			if (result == Result.OK)
				return;
			throw new FmodException(result, Core.GetResultString(result));
		}

		public static Stats GetStats(bool blocking = false)
		{
			var result = FMOD_Memory_GetStats(out var currentAlloc, out var maxAlloc, blocking);
			if (result == Result.OK)
				return new Stats(currentAlloc, maxAlloc);
			throw new FmodException(result, Core.GetResultString(result));
		}

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_Memory_Initialize(IntPtr poolmem, int poollen, AllocCallback useralloc,
			ReallocCallback userrealloc, FreeCallback userfree, MemoryType memtypeflags);

		[DllImport(Core.LIBRARY)]
		private static extern Result FMOD_Memory_GetStats(out int currentAlloc, out int maxAlloc, bool blocking);

		public class Stats
		{
			public Stats(int current, int max)
			{
				CurrentlyAllocated = current;
				MaxAllocated = max;
			}

			public int CurrentlyAllocated { get; }

			public int MaxAllocated { get; set; }
		}
	}
}