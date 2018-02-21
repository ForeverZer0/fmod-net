using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using FMOD.NET.Enumerations;

namespace FMOD.NET.Core
{
	[SuppressUnmanagedCodeSecurity]
	[SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
	public static class MemoryManager
	{


		public static void Initialize()
		{
			var result = FMOD_Memory_Initialize(IntPtr.Zero, 0, null, null, null, MemoryType.All);
			if (result == Result.OK)
				return;
			throw new FmodException(result);
		}

		public static void Initialize(IntPtr memPool, int memPoolSize)
		{
			var result = FMOD_Memory_Initialize(memPool, memPoolSize, null, null, null, MemoryType.All);
			if (result == Result.OK)
				return;
			throw new FmodException(result);
		}

		public static void Initialize(MemoryAllocCallback userAlloc, MemoryReallocCallback userRealloc, MemoryFreeCallback userFree,
			MemoryType type)
		{
			var result = FMOD_Memory_Initialize(IntPtr.Zero, 0, userAlloc, userRealloc, userFree, type);
			if (result == Result.OK)
				return;
			throw new FmodException(result);
		}

		public static void GetStats(out int currentAlloc, out int maxAlloc, bool blocking = false)
		{
			var result = FMOD_Memory_GetStats(out currentAlloc, out maxAlloc, blocking);
			if (result == Result.OK)
				return;
			throw new FmodException(result);
		}

		public static Stats GetStats(bool blocking = false)
		{
			var result = FMOD_Memory_GetStats(out var currentAlloc, out var maxAlloc, blocking);
			if (result == Result.OK)
				return new Stats(currentAlloc, maxAlloc);
			throw new FmodException(result);
		}

		[DllImport(Constants.LIBRARY)]
		private static extern Result FMOD_Memory_Initialize(IntPtr poolmem, int poollen, MemoryAllocCallback useralloc,
			MemoryReallocCallback userrealloc, MemoryFreeCallback userfree, MemoryType memtypeflags);

		[DllImport(Constants.LIBRARY)]
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