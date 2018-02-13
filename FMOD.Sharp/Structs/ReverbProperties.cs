using System;
using System.Runtime.InteropServices;

namespace FMOD.Sharp.Structs
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
	[StructLayout(LayoutKind.Sequential, Pack = 4)][Serializable]
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
		public float DecayTime;

		/// <summary>
		///     Value that controls the modal density in the late reverberation decay (%).
		///     <para><b>Minimum:</b> 0.0</para>
		///     <para><b>Maximum:</b> 100.0</para>
		///     <para><b>Default:</b> 100.0</para>
		/// </summary>
		public float Density;

		/// <summary>
		///     Value that controls the echo density in the late reverberation decay (%).
		///     <para><b>Minimum:</b> 0.0</para>
		///     <para><b>Maximum:</b> 100.0</para>
		///     <para><b>Default:</b> 100.0</para>
		/// </summary>
		public float Diffusion;

		/// <summary>
		///     Initial reflection delay time (ms).
		///     <para><b>Minimum:</b> 0.0</para>
		///     <para><b>Maximum:</b> 300.0</para>
		///     <para><b>Default:</b> 7.0</para>
		/// </summary>
		public float EarlyDelay;

		/// <summary>
		///     Early reflections level relative to room effect (%).
		///     <para><b>Minimum:</b> 0.0</para>
		///     <para><b>Maximum:</b> 100.0</para>
		///     <para><b>Default:</b> 50.0</para>
		/// </summary>
		public float EarlyLateMix;

		/// <summary>
		///     High-frequency to mid-frequency decay time ratio (%).
		///     <para><b>Minimum:</b> 10.0</para>
		///     <para><b>Maximum:</b> 100.0</para>
		///     <para><b>Default:</b> 50.0</para>
		/// </summary>
		public float HFDecayRatio;

		/// <summary>
		///     Reference high frequency (hz).
		///     <para><b>Minimum:</b> 20.0</para>
		///     <para><b>Maximum:</b> 20000.0</para>
		///     <para><b>Default:</b> 5000.0</para>
		/// </summary>
		public float HFReference;

		/// <summary>
		///     Relative room effect level at high frequencies (Hz).
		///     <para><b>Minimum:</b> 20.0</para>
		///     <para><b>Maximum:</b> 20000.0</para>
		///     <para><b>Default:</b> 20000.0</para>
		/// </summary>
		public float HighCut;

		/// <summary>
		///     Late reverberation delay time relative to initial reflection (ms).
		///     <para><b>Minimum:</b> 0.0</para>
		///     <para><b>Maximum:</b> 100.0</para>
		///     <para><b>Default:</b> 11.0</para>
		/// </summary>
		public float LateDelay;

		/// <summary>
		///     Reference low frequency (Hz)
		///     <para><b>Minimum:</b> 20.0</para>
		///     <para><b>Maximum:</b> 1000.0</para>
		///     <para><b>Default:</b> 250.0</para>
		/// </summary>
		public float LowShelfFrequency;

		/// <summary>
		///     Relative room effect level at low frequencies (dB).
		///     <para><b>Minimum:</b> -36.0</para>
		///     <para><b>Maximum:</b> 12.0</para>
		///     <para><b>Default:</b> 0.0</para>
		/// </summary>
		public float LowShelfGain;

		/// <summary>
		///     Room effect level at mid frequencies (dB).
		///     <para><b>Minimum:</b> -80.0</para>
		///     <para><b>Maximum:</b> 20.0</para>
		///     <para><b>Default:</b> -6.0</para>
		/// </summary>
		public float WetLevel;
	}
}