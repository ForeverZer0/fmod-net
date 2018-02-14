using System;
using FMOD.Sharp.Enums;

namespace FMOD.Sharp.DSP
{
	/// <summary>
	/// <para>Represents a connection between two <see cref="HandleBase"/> units.</para>
	/// <para>Think of it as the line between two circles.</para>
	/// </summary>
	/// <seealso cref="FMOD.Sharp" />
	public partial class DspConnection : HandleBase
	{
		#region Delegates & Events

		/// <summary>
		/// Occurs when <seealso cref="UserData"/> property has been changed.
		/// </summary>
		/// <seealso cref="UserData"/>
		public event EventHandler UserDataChanged;

		/// <summary>
		/// Occurs when <see cref="Mix"/> level has changed.
		/// </summary>
		/// <seealso cref="Mix"/>
		public event EventHandler MixChanged;

		/// <summary>
		/// Occurs when mix matrix has changed.
		/// </summary>
		/// <seealso cref="SetMixMatrix"/>
		public event EventHandler MixMatrixChanged;

		#endregion

		#region Constructors & Destructor

		/// <summary>
		/// Initializes a new instance of the <see cref="DspConnection"/> class.
		/// </summary>
		/// <param name="handle">The handle to the object.</param>
		internal DspConnection(IntPtr handle) : base(handle)
		{
		}

		#endregion

		#region Properties & Indexers

		/// <summary>
		/// Gets the <see cref="DspConnection">DSP</see> unit that is the input of this connection.
		/// </summary>
		/// <value>
		/// The input DSP.
		/// </value>
		/// <remarks>
		/// <para>A <see cref="DspConnection"/> joins two <see cref="DspBase.AddInput"/> units together (think of it as the line between two circles).</para>
		/// <para>Each <see cref="DspBase"/> has one input and one output.</para>
		/// <alert class="note">
		/// <para>If a <see cref="Result.NotReady"/> just occurred, the connection might not be ready because the <see cref="DspBase.AddInput"/> system is still queued to connect in the background. If so the function will return <see cref="DspBase"/> and the input will be <c>null</c>. Poll until it is ready.</para>
		/// </alert>
		/// </remarks>
		/// <seealso cref="DspConnection"/>
		/// <seealso cref="DspBase"/>
		public DspBase Input
		{
			get
			{
				NativeInvoke(FMOD_DSPConnection_GetInput(this, out var dsp));
				return Core.Create<DspBase>(dsp);
			}
		}

		/// <summary>
		/// <para>Gets or sets the volume of the connection. The input is scaled by this value before being passed to the output.</para>
		/// <para><c>0.0</c> = Silent, <c>1.0</c> = Full Volume.</para>
		/// </summary>
		/// <value>
		/// The mix.
		/// </value>
		/// <seealso cref="DspBase.GetInput"/>
		/// <seealso cref="DspBase.GetOutput"/>
		public float Mix
		{
			get
			{
				NativeInvoke(FMOD_DSPConnection_GetMix(this, out var volume));
				return volume;
			}
			set
			{
				NativeInvoke(FMOD_DSPConnection_SetMix(this, value.Clamp(0.0f, 1.0f)));
				MixChanged?.Invoke(this, EventArgs.Empty);
			}
		}

		/// <summary>
		/// Gets the <see cref="DspConnection"/> unit that is the output of this connection.
		/// </summary>
		/// <value>
		/// The output.
		/// </value>
		/// <remarks>
		/// <para>A <see cref="DspConnection"/> joins two <see cref="DspBase.AddInput"/> units together (think of it as the line between two circles).</para>
		/// <para>Each <see cref="DspBase"/> has one input and one output.</para>
		/// <alert class="note">
		/// <para>If a <see cref="Result.NotReady"/> just occurred, the connection might not be ready because the <see cref="DspBase.AddInput">DSP</see> system is still queued to connect in the background.</para>
		/// <para>If so the function will return <see cref="DspBase"/> and the input will be <c>null</c>. Poll until it is ready.</para>
		/// </alert>
		/// </remarks>
		/// <seealso cref="DspConnection"/>
		/// <seealso cref="DspBase"/>
		public DspBase Output
		{
			get
			{
				NativeInvoke(FMOD_DSPConnection_GetOutput(this, out var dsp));
				return Core.Create<DspBase>(dsp);
			}
		}

		/// <summary>
		/// <para>Gets the type of the connection between two <see cref="DspConnectionType.Standard"/> units.</para> 
		/// <para>This can be <see cref="DspConnectionType"/>, <see cref="DspConnectionType"/>, <see cref="DspConnectionType"/>, or <see cref="DspConnectionType"/>.</para>
		/// </summary>
		/// <value>
		/// The type.
		/// </value>
		/// <seealso cref="DspBase"/>
		public DspConnectionType Type
		{
			get
			{
				NativeInvoke(FMOD_DSPConnection_GetType(this, out var type));
				return type;
			}
		}

		/// <summary>
		/// Gets or sets a user value that the <see cref="DspConnection"/> object will store internally. 
		/// </summary>
		/// <value>
		/// The user data.
		/// </value>
		/// <remarks>This function is primarily used in case the user wishes to "attach" data to an <b>FMOD</b> object.</remarks>
		public IntPtr UserData
		{
			get
			{
				NativeInvoke(FMOD_DSPConnection_GetUserData(this, out var data));
				return data;
			}
			set
			{
				NativeInvoke(FMOD_DSPConnection_SetUserData(this, value));
				UserDataChanged?.Invoke(this, EventArgs.Empty);
			}
		}

		#endregion

		#region Methods

		/// <summary>
		/// Returns the panning matrix set by the user, for a connection.
		/// </summary>
		/// <param name="matrix">An array of floating point matrix data, where rows represent output speakers, and columns represent input channels.</param>
		/// <param name="outChannels">The number of output channels in the set matrix.</param>
		/// <param name="inChannels">The number of input channels in the set matrix.</param>
		/// <param name="inChannelHop">The number of floating point values available in the destination memory for a row, so that the destination memory can be skipped through correctly to write the correct values, if the intended matrix memory to be written to is wider than the matrix stored in the <see cref="DspConnection"/>.</param>
		/// <seealso cref="DspConnection.SetMixMatrix"/>
		public void GetMixMatrix(float[] matrix, out int outChannels, out int inChannels, int inChannelHop)
		{
			NativeInvoke(FMOD_DSPConnection_GetMixMatrix(this, matrix, out outChannels, out inChannels, inChannelHop));
		}

		/// <summary>
		/// <para>Sets a <math>NxN</math> panning matrix on a <see cref="DspBase">DSP</see> connection. </para>
		/// <para>Skipping/hop is supported, so memory for the matrix can be wider than the width of the <see cref="inChannels"/> parameter.</para>
		/// </summary>
		/// <param name="outChannels">An array of floating point matrix data, where rows represent output speakers, and columns represent input channels.</param>
		/// <param name="inChannels">Number of output channels in the matrix being specified.</param>
		/// <param name="inChannelHop">Number of input channels in the matrix being specified. </param>
		/// <param name="inChannelHop">Number of floating point values stored in memory for a row, so that the memory can be skipped through correctly to read the right values, if the intended matrix memory to be read from is wider than the matrix stored in the <see cref="DspBase"/>. </param>
		public void SetMixMatrix(float[] matrix, int outChannels, int inChannels, int inChannelHop)
		{
			NativeInvoke(FMOD_DSPConnection_SetMixMatrix(this, matrix, outChannels, inChannels, inChannelHop));
			MixMatrixChanged?.Invoke(this, EventArgs.Empty);
		}

		#endregion
	}
}