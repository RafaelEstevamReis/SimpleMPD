namespace Simple.MPD
{
    /// <summary>
    /// States for the SINGLE command
    /// </summary>
    public enum SingleStates
    {
        /// <summary>
        /// Enables SINGLE mode
        /// </summary>
        On,
        /// <summary>
        /// Disables SINGLE mode
        /// </summary>
        Off,
        /// <summary>
        /// Sets SINGLE to OneShot mode
        /// </summary>
        OneShot,
    }

    public enum PlaybackState
    {
        Play,
        Pause,
        Stop
    }
    public enum Tags
    {
        Album,
        Albumartist,
        Albumartistsort,
        Albumsort,
        Artist,
        Artistsort,
        Comment,
        Composer,
        Conductor,
        Date,
        Disc,
        Genre,
        Grouping,
        Label,
        Musicbrainz_albumartistid,
        Musicbrainz_albumid,
        Musicbrainz_artistid,
        Musicbrainz_releasetrackid,
        Musicbrainz_trackid,
        Musicbrainz_workid,
        Name,
        Originaldate,
        Performer,
        Title,
        Track,
        Work,

        Any,
    }
    /// <summary>
    /// Filter match for 'find'-like Expressions
    /// </summary>
    public enum FilterMatch
    {
        /// <summary>
        /// Compiles to '=='
        /// </summary>
        Equals,
        /// <summary>
        /// Compiles to '!='
        /// </summary>
        Different,
        /// <summary>
        /// Compiles to 'contains'
        /// </summary>
        Contains,
    }
}
