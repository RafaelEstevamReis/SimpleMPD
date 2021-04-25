namespace Simple.MPD.Helper
{
    /// <summary>
    /// Represents a directory structure in the MPD server
    /// </summary>
    public class MpdDirectory
    {
        /// <summary>
        /// Directory path
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Mpd's sub-directories
        /// </summary>
        public MpdDirectory[] Directories { get; set; }
        /// <summary>
        /// Directory's files
        /// </summary>
        public Responses.SongInfo[] Files { get; set; }
        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        public override string ToString() => Name;
    }
}