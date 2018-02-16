namespace FMOD.Enumerations
{
	/// <summary>
	/// <para>List of tag types that could be stored within a sound. </para>
	/// <para>These include ID3 tags, metadata from netstreams and vorbis/asf data.</para>
	/// </summary>
	/// <seealso cref="M:FMOD.Core.Sound.GetTag"/>
	/// <seealso cref="M:FMOD.Core.Sound.GetTags"/>
	public enum TagType 
    {
		/// <summary>
		/// Unknown
		/// </summary>
		Unknown,
		/// <summary>
		/// ID3V1
		/// </summary>
		Id3V1,
	    /// <summary>
	    /// ID3V2
	    /// </summary>
        Id3V2,
	    /// <summary>
	    /// Vorbis Comment
	    /// </summary>
        VorbisComment,
	    /// <summary>
	    /// ShoutCast
	    /// </summary>
        ShoutCast,
	    /// <summary>
	    /// IceCaste
	    /// </summary>
        IceCast,
	    /// <summary>
	    /// ASF
	    /// </summary>
        Asf,
	    /// <summary>
	    /// MIDI
	    /// </summary>
        Midi,
	    /// <summary>
	    /// Playlist
	    /// </summary>
        Playlist,
	    /// <summary>
	    /// FMOD
	    /// </summary>
        Fmod,
	    /// <summary>
	    /// User
	    /// </summary>
        User,
	    /// <summary>
	    /// Maximum number of tag types supported. 
	    /// </summary>
        Max              
    }


}
