using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Simple.MPD.Interfaces;

namespace Simple.MPD.Responses
{
    /// <summary>
    /// Song info response
    /// </summary>
    public class SongInfo : IResponse
    {
        /// <summary>
        /// Underlying file, NULL for directories
        /// </summary>
        public string File { get; set; }
        /// <summary>
        /// Directory name, NULL for files, playlists, etc
        /// </summary>
        public string Directory { get; set; }
        /// <summary>
        /// Playlist file path, NULL for songs and directories
        /// </summary>
        public string PlayList { get; set; }

        /// <summary>
        /// A name for this song. This is not the song title. The exact meaning of this tag is not well-defined in the standard
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Total time elapsed within the song
        /// </summary>
        public TimeSpan Elapsed { get; set; }
        /// <summary>
        /// Duration of the song
        /// </summary>
        public TimeSpan Duration { get; set; }

        /// <summary>
        /// The album id in the MusicBrainz database
        /// </summary>
        public Guid[] MUSICBRAINZ_ALBUMID { get; set; }
        /// <summary>
        /// The artist id in the MusicBrainz database
        /// </summary>
        public Guid[] MUSICBRAINZ_ARTISTID { get; set; }
        /// <summary>
        /// The album artist id in the MusicBrainz database
        /// </summary>
        public Guid[] MUSICBRAINZ_ALBUMARTISTID { get; set; }
        /// <summary>
        /// The release track id in the MusicBrainz database
        /// </summary>
        public Guid MUSICBRAINZ_RELEASETRACKID { get; set; }
        /// <summary>
        /// The track id in the MusicBrainz database
        /// </summary>
        public Guid MUSICBRAINZ_TRACKID { get; set; }
        /// <summary>
        /// The artist name
        /// </summary>
        public string Artist { get; set; }
        /// <summary>
        /// On multi-artist albums, this is the artist name which shall be used for the whole album
        /// </summary>
        public string AlbumArtist { get; set; }
        /// <summary>
        /// Same as artist, but for sorting
        /// </summary>
        public string ArtistSort { get; set; }
        /// <summary>
        /// Same as albumartist, but for sorting
        /// </summary>
        public string AlbumArtistSort { get; set; }
        /// <summary>
        /// The song title
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// The album name
        /// </summary>
        public string Album { get; set; }
        /// <summary>
        /// Same as album, but for sorting
        /// </summary>
        public string AlbumSort { get; set; }
        /// <summary>
        /// The music genre
        /// </summary>
        public string Genre { get; set; }
        /// <summary>
        /// The decimal track number within the album
        /// </summary>
        public string Track { get; set; }
        /// <summary>
        /// The artist who composed the song
        /// </summary>
        public string Composer { get; set; }
        /// <summary>
        /// The artist who performed the song
        /// </summary>
        public string Performer { get; set; }
        /// <summary>
        /// The conductor who conducted the song
        /// </summary>
        public string Conductor { get; set; }
        /// <summary>
        /// the time stamp of the last modification of the underlying file
        /// </summary>
        public DateTime LastModified { get; set; }

        private string[] format = new string[0];
        /// <summary>
        /// The audio format of the song (or an approximation to a format supported by MPD and the decoder plugin being used). 
        /// When playing this file, the audio value in the status response should be the same
        /// </summary>
        public string Format
        {
            get => string.Join(":", format);
            set => format = value.Split(':');
        }

        /// <summary>
        /// Sample rate part of the Format
        /// </summary>
        public int Format_SampleRate => parseFormatNumber(format[0]);
        /// <summary>
        /// Bits part of the Format
        /// </summary>
        public int Format_Bits => parseFormatNumber(format[1]);
        /// <summary>
        /// Channels part of the Format
        /// </summary>
        public int Format_Channels => parseFormatNumber(format[2]);

        /// <summary>
        /// Song position in the queue
        /// </summary>
        public int Pos { get; set; }
        /// <summary>
        /// Song ID on the queue
        /// </summary>
        public int Id { get; set; }
        
        /// <summary>
        /// Artist name for display
        /// </summary>
        public string DisplayArtist
        {
            get
            {
                if (AlbumArtist != null)
                {
                    if (!AlbumArtist.StartsWith("various ", StringComparison.InvariantCultureIgnoreCase)) return AlbumArtist;
                }
                return Artist ?? "[Unkown]";
            }
        }
        /// <summary>
        /// Song name for display
        /// {DisplayArtist} - {Title}
        /// </summary>
        public string SongDisplayName
        {
            get
            {
                if (Title != null)
                {
                    return $"{DisplayArtist} - {Title}";
                }
                if (Name != null) return Name;
                return File ?? Directory ?? PlayList ?? "[Unkown]";
            }
        }

        /// <summary>
        /// Read a response from the stream
        /// </summary>
        public async Task ReadAsync(StreamReader stream)
        {
            await Task.Run(() =>
            {
                var values = Helper.ResponseHelper.ReadPairs(stream);
                foreach (var pair in values)
                {
                    assingKeyValuePair(pair);
                }
            });
        }

        private void assingKeyValuePair(KeyValuePair<string, string> pair)
        {
            double dVal;
            switch (pair.Key.ToLower())
            {
                case "file":
                    File = pair.Value.Trim();
                    break;
                case "directory":
                    Directory = pair.Value.Trim();
                    break;
                case "playlist":
                    PlayList = pair.Value.Trim();
                    break;

                case "name":
                    Name = pair.Value.Trim();
                    break;

                case "artist":
                    Artist = pair.Value.Trim();
                    break;
                case "albumartist":
                    AlbumArtist = pair.Value.Trim();
                    break;
                case "artistsort":
                    ArtistSort = pair.Value.Trim();
                    break;
                case "albumartistsort":
                    AlbumArtistSort = pair.Value.Trim();
                    break;
                case "title":
                    Title = pair.Value.Trim();
                    break;
                case "album":
                    Album = pair.Value.Trim();
                    break;
                case "albumsort":
                    AlbumSort = pair.Value.Trim();
                    break;
                case "genre":
                    Genre = pair.Value.Trim();
                    break;
                case "composer":
                    Composer = pair.Value.Trim();
                    break;
                case "performer":
                    Performer = pair.Value.Trim();
                    break;
                case "conductor":
                    Conductor = pair.Value.Trim();
                    break;
                case "track":
                    Track = pair.Value.Trim();
                    break;
                case "format":
                    Format = pair.Value.Trim();
                    break;

                case "time":
                case "elapsed":
                    dVal = double.Parse(pair.Value, System.Globalization.CultureInfo.InvariantCulture);
                    Elapsed = TimeSpan.FromSeconds(dVal);
                    break;
                case "duration":
                    dVal = double.Parse(pair.Value, System.Globalization.CultureInfo.InvariantCulture);
                    Duration = TimeSpan.FromSeconds(dVal);
                    break;
                case "last-modified":
                    LastModified = DateTime.Parse(pair.Value, System.Globalization.CultureInfo.InvariantCulture);
                    break;

                case "musicbrainz_albumid":
                    MUSICBRAINZ_ALBUMID = ParseGuids(pair.Value);
                    break;
                case "musicbrainz_artistid":
                    MUSICBRAINZ_ARTISTID = ParseGuids(pair.Value);
                    break;
                case "musicbrainz_albumartistid":
                    MUSICBRAINZ_ALBUMARTISTID = ParseGuids(pair.Value);
                    break;
                case "musicbrainz_releasetrackid":
                    MUSICBRAINZ_RELEASETRACKID = Guid.Parse(pair.Value);
                    break;
                case "musicbrainz_trackid":
                    MUSICBRAINZ_TRACKID = Guid.Parse(pair.Value);
                    break;

                case "pos":
                    Pos = int.Parse(pair.Value);
                    break;
                case "id":
                    Id = int.Parse(pair.Value);
                    break;
            }
        }
        /// <summary>
        /// Get all song info from stream
        /// </summary>
        public static IEnumerable<SongInfo> ReadAll(StreamReader stream)
        {
            SongInfo current = null;

            var values = Helper.ResponseHelper.ReadPairs(stream);
            foreach (var pair in values)
            {
                // id is ever the last one
                if (pair.Key == "file" || pair.Key == "directory" || pair.Key == "playlist")
                {
                    if (current != null) yield return current;
                    current = new SongInfo();
                }

                current.assingKeyValuePair(pair);
            }
            if (current != null) yield return current;
        }
        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        public override string ToString()
        {
            if (Id > 0)
                return $"[{Id}] {Pos:00} - {SongDisplayName}";

            if (Directory != null) return $"DIR: {Directory}";
            if (PlayList != null) return $"PlayList: {PlayList}";

            if (File != null) return File;

            return $"{SongDisplayName}";
        }

        static int parseFormatNumber(string formatSection)
        {
            int.TryParse(formatSection, out int value);
            return value;
        }

        private static Guid[] ParseGuids(string guids)
        {
            return guids.Split('/')
                        .Select(g => Guid.Parse(g))
                        .ToArray();
        }
    }
    /// <summary>
    /// A collection of songs information
    /// </summary>
    public class SongInfoCollection : IResponse, IEnumerable<SongInfo>
    {
        /// <summary>
        /// All songs in the collection
        /// </summary>
        public SongInfo[] Songs { get; private set; }

        /// <summary>
        /// Read response from stream
        /// </summary>
        public async Task ReadAsync(StreamReader stream)
        {
            await Task.Run(() =>
            {
                List<SongInfo> list = new List<SongInfo>();
                foreach (var s in SongInfo.ReadAll(stream))
                {
                    list.Add(s);
                }
                Songs = list.ToArray();
            });
        }

        /// <summary>
        /// Get the enumerator
        /// </summary>
        public IEnumerator<SongInfo> GetEnumerator()
        {
            foreach (var s in Songs) yield return s;
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return Songs.GetEnumerator();
        }
    }
}
