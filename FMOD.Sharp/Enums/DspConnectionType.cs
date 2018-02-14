using FMOD.Sharp.DSP;
using FMOD.Sharp.Structs;

namespace FMOD.Sharp.Enums
{
	/// <summary>
	/// List of connection types between two <see cref="DspBase"/> nodes.
	/// </summary>
	/// <seealso cref="DspBDspBaseInput"/>
	/// <seealso cref="DspConnection.Type"/>
	public enum DspConnectionType 
    {
		/// <summary>
		/// <para>Specifies the <b>Default</b> connection type.</para>
		/// <para>Audio is mixed from the input to the output <see cref="DspBase"/>'s audible buffer.</para>
		/// </summary>
		/// <remarks>
		/// <para>Default <see cref="DspConnection"/> type. Audio is mixed from the input to the output <see cref="DspBase"/>'s audible buffer, meaning it will be part of the audible signal. </para>
		/// <para>A standard connection will execute its input DSP if it has not been executed before.</para>
		/// </remarks>
		Standard,

		/// <summary>
		/// <para>Specifies the <b>Sidechain</b> connection type.</para>
		/// <para>Audio is mixed from the input to the output <see cref="DspBase"/>'s sidechain buffer.</para>
		/// </summary>
		/// <remarks>
		/// <para>Sidechain <see cref="DspConnection"/> type. Audio is mixed from the input to the output <see cref="DspBase"/>'s sidechain buffer, meaning it will NOT be part of the audible signal. A sidechain connection will execute its input DSP if it has not been executed before.</para>
		/// <para>The purpose of the seperate sidechain buffer in a DSP, is so that the DSP effect can privately access for analysis purposes. An example of use in this case, could be a compressor which analyzes the signal, to control its own effect parameters (ie a compression level or gain).</para>
		/// <para>For the effect developer, to accept sidechain data, the sidechain data will appear in the <see cref="DspState"/> struct which is passed into the read callback of a DSP unit.</para>
		/// <para><see cref="DspState.sidechaindata"/> and <see cref="DspState.sidechainchannels"/> will hold the mixed result of any sidechain data flowing into it.</para>
		/// </remarks>
		SideChain,

		/// <summary>
		/// <para>Specifies the <b>Send</b> connection type.</para>
		/// <para>Audio is mixed from the input to the output <see cref="DspBase"/>'s audible buffer, but the input is <b>NOT</b> executed, only copied from. A standard connection or sidechain needs to make an input execute to generate data.</para>
		/// </summary>
		/// <remarks>
		/// <para>Send <see cref="DspConnection"/> type. Audio is mixed from the input to the output <see cref="DspBase"/>'s audible buffer, meaning it will be part of the audible signal. A send connection will <b>NOT</b> execute its input DSP if it has not been executed before</para>
		/// <para>A send connection will only read what exists at the input's buffer at the time of executing the output DSP unit (which can be considered the "return")</para>
		/// </remarks>
		Send,

		/// <summary>
		/// <para>Specifies the <b>Send Sidechain</b> connection type. </para>
		/// <para>Audio is mixed from the input to the output <see cref="DspBase"/>'s sidechain buffer, but the input is <b>NOT</b> executed, only copied from. A standard connection or sidechain needs to make an input execute to generate data.</para>
		/// </summary>
		/// <remarks>
		/// <para>Send sidechain <see cref="DspConnection"/>  type. Audio is mixed from the input to the output <see cref="DspBase"/>'s sidechain buffer, meaning it will <b>NOT</b> be part of the audible signal. A send sidechain connection will <b>NOT</b> execute its input DSP if it has not been executed before.</para>
		/// <para>A send sidechain connection will only read what exists at the input's buffer at the time of executing the output DSP unit (which can be considered the "sidechain return").</para>
		/// <para>For the effect developer, to accept sidechain data, the sidechain data will appear in the <see cref="DspState"/> struct which is passed into the read callback of a DSP unit.</para>
		/// <para><see cref="DspState.sidechaindata"/> and <see cref="DspState.sidechainchannels"/> will hold the mixed result of any sidechain data flowing into it.</para>
		/// </remarks>
		SendSideChain,

		/// <summary>
		/// Maximum number of DSP connection types supported. 
		/// </summary>
		Max,             
    }


}
