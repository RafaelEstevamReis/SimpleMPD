namespace Simple.MPD
{
    public enum StatusSingle
    {
        S0,
        S1,
        Oneshot
    }
    public enum SongState
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
