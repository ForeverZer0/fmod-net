#region License

// ReverbProperties.cs is distributed under the Microsoft Public License (MS-PL)
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
// Created 1:14 PM 02/05/2018

#endregion

#region Using Directives

using System;
using System.Runtime.InteropServices;
using FMOD.Core;

#endregion

namespace FMOD.Structures
{
	/// <summary>
	///     Structure defining a reverb environment.
	/// </summary>
	/// <remarks>
	///     <alert class="note">The default reverb properties are the same as the <see cref="ReverbPresets.Generic" /> preset.</alert>
	/// </remarks>
	/// <seealso cref="ReverbPresets" />
	/// <seealso cref="FmodSystem.CreateReverb" />
	/// <seealso cref="FmodSystem.GetReverbProperties" />
	/// <seealso cref="FmodSystem.SetReverbProperties" />
	[StructLayout(LayoutKind.Explicit, Size = 48)]
	[Serializable]
	public struct ReverbProperties
	{
		/// <summary>
		///     <para>Reverberation decay time (ms).</para>
		///     <para>
		///         <b>Minimum: 0.0</b>
		///     </para>
		///     <para><b>Maximum:</b> 20000.0</para>
		///     <para><b>Default:</b> 1500.0</para>
		/// </summary>
		[FieldOffset(0)] public float DecayTime;

		/// <summary>
		///     Value that controls the echo density in the late reverberation decay (%).
		///     <para><b>Minimum:</b> 0.0</para>
		///     <para><b>Maximum:</b> 100.0</para>
		///     <para><b>Default:</b> 100.0</para>
		/// </summary>
		[FieldOffset(20)] public float Diffusion;

		/// <summary>
		///     Value that controls the modal density in the late reverberation decay (%).
		///     <para><b>Minimum:</b> 0.0</para>
		///     <para><b>Maximum:</b> 100.0</para>
		///     <para><b>Default:</b> 100.0</para>
		/// </summary>
		[FieldOffset(24)] public float Density;

		/// <summary>
		///     Initial reflection delay time (ms).
		///     <para><b>Minimum:</b> 0.0</para>
		///     <para><b>Maximum:</b> 300.0</para>
		///     <para><b>Default:</b> 7.0</para>
		/// </summary>
		[FieldOffset(4)] public float EarlyDelay;

		/// <summary>
		///     Early reflections level relative to room effect (%).
		///     <para><b>Minimum:</b> 0.0</para>
		///     <para><b>Maximum:</b> 100.0</para>
		///     <para><b>Default:</b> 50.0</para>
		/// </summary>
		[FieldOffset(40)] public float EarlyLateMix;

		/// <summary>
		///     High-frequency to mid-frequency decay time ratio (%).
		///     <para><b>Minimum:</b> 10.0</para>
		///     <para><b>Maximum:</b> 100.0</para>
		///     <para><b>Default:</b> 50.0</para>
		/// </summary>
		[FieldOffset(16)] public float HFDecayRatio;

		/// <summary>
		///     Reference high frequency (hz).
		///     <para><b>Minimum:</b> 20.0</para>
		///     <para><b>Maximum:</b> 20000.0</para>
		///     <para><b>Default:</b> 5000.0</para>
		/// </summary>
		[FieldOffset(12)] public float HFReference;

		/// <summary>
		///     Relative room effect level at high frequencies (Hz).
		///     <para><b>Minimum:</b> 20.0</para>
		///     <para><b>Maximum:</b> 20000.0</para>
		///     <para><b>Default:</b> 20000.0</para>
		/// </summary>
		[FieldOffset(36)] public float HighCut;

		/// <summary>
		///     Late reverberation delay time relative to initial reflection (ms).
		///     <para><b>Minimum:</b> 0.0</para>
		///     <para><b>Maximum:</b> 100.0</para>
		///     <para><b>Default:</b> 11.0</para>
		/// </summary>
		[FieldOffset(8)] public float LateDelay;

		/// <summary>
		///     Reference low frequency (Hz)
		///     <para><b>Minimum:</b> 20.0</para>
		///     <para><b>Maximum:</b> 1000.0</para>
		///     <para><b>Default:</b> 250.0</para>
		/// </summary>
		[FieldOffset(28)] public float LowShelfFrequency;

		/// <summary>
		///     Relative room effect level at low frequencies (dB).
		///     <para><b>Minimum:</b> -36.0</para>
		///     <para><b>Maximum:</b> 12.0</para>
		///     <para><b>Default:</b> 0.0</para>
		/// </summary>
		[FieldOffset(32)] public float LowShelfGain;

		/// <summary>
		///     Room effect level at mid frequencies (dB).
		///     <para><b>Minimum:</b> -80.0</para>
		///     <para><b>Maximum:</b> 20.0</para>
		///     <para><b>Default:</b> -6.0</para>
		/// </summary>
		[FieldOffset(44)] public float WetLevel;
	}
}