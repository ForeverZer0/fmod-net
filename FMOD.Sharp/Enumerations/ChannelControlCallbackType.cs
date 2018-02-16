namespace FMOD.Enumerations
{
	/// <summary>
	/// These callback types are used with <see cref="M:FMOD.Sharp.Core.ChannelControl.SetCallback"/>.
	/// </summary>
	/// <remarks>
	/// <para>Each callback has command-data parameters passed as <see cref="T:System.IntPtr"/> unique to the type of callback.</para>
	/// <para>See reference to <see cref="M:FMOD.Sharp.Core.ChannelControl.SetCallback"/> to determine what they might mean for each type of callback.</para>
	/// <alert class="note"><para>
	/// Currently the user must call <see cref="M:FMOD.Sharp.Core.FmodSystem.Update"/> for these callbacks to trigger!
	/// </para></alert>
	/// </remarks>
	/// <seealso cref="M:FMOD.Sharp.Core.ChannelControl.SetCallback"/>
	/// <seealso cref="M:FMOD.Sharp.Core.FmodSystem.Update"/>
	/// <seealso cref="T:FMOD.Sharp.Core.ChannelCallback"/>
	public enum ChannelControlCallbackType 
    {
		/// <summary>
		/// Called when a sound ends. 
		/// </summary>
		End,

		/// <summary>
		/// Called when a voice is swapped out or swapped in. 
		/// </summary>
		Virtualvoice,     
		
	    /// <summary>
	    /// Called when a syncpoint is encountered. Can be from wav file markers. 
	    /// </summary>
        SyncPoint,    
		
	    /// <summary>
	    /// Called when the channel has its geometry occlusion value calculated. Can be used to clamp or change the value. 
	    /// </summary>
        Occlusion,       
		
	    /// <summary>
	    /// Maximum number of callback types supported. 
	    /// </summary>
        Max             
    }
}
