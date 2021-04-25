using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Simple.MPD.Interfaces;

namespace Simple.MPD.Responses
{
    public class SongInfo : IResponse
    {

        public string File { get; set; }
        public string Directory { get; set; }
        public string PlayList { get; set; }
        public string Name { get; set; }

        public TimeSpan Elapsed { get; set; }
        public TimeSpan Duration { get; set; }

        public Guid[] MUSICBRAINZ_ALBUMID { get; set; }
        public Guid[] MUSICBRAINZ_ARTISTID { get; set; }
        public Guid[] MUSICBRAINZ_ALBUMARTISTID { get; set; }
        public Guid MUSICBRAINZ_RELEASETRACKID { get; set; }
        public Guid MUSICBRAINZ_TRACKID { get; set; }

        public string Artist { get; set; }
        public string AlbumArtist { get; set; }
        public string ArtistSort { get; set; }
        public string AlbumArtistSort { get; set; }
        public string Title { get; set; }
        public string Album { get; set; }
        public string AlbumSort { get; set; }
        public string Genre { get; set; }
        public string Track { get; set; }
        public string Composer { get; set; }
        public string Performer { get; set; }
        public string Conductor { get; set; }
        public DateTime LastModified { get; set; }
        private string[] format = new string[0];
        public string Format
        {
            get => string.Join(":", format);
            set => format = value.Split(':');
        }

        public int Format_SampleRate => parseFormatNumber(format[0]);
        public int Format_Bits => parseFormatNumber(format[1]);
        public int Format_Channels => parseFormatNumber(format[2]);

        public int Pos { get; set; }
        public int Id { get; set; }

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
        public Task ReadAsync(StreamReader stream)
        {
            var values = Helper.ResponseHelper.ReadPairs(stream);
            foreach (var pair in values)
            {
                assingKeyValuePair(pair);
            }
            return Task.CompletedTask;
        }

        private void assingKeyValuePair((string Key, string Value) pair)
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

        public override string ToString()
        {
            return $"Id: {Id} Pos: {Pos} {SongDisplayName}";
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

    public class SongInfoCollection : IResponse, IEnumerable<SongInfo>
    {
        public SongInfo[] Songs { get; private set; }

        public ICommand GetCommand() => null;

        public Task ReadAsync(StreamReader stream)
        {
            List<SongInfo> list = new List<SongInfo>();
            foreach (var s in SongInfo.ReadAll(stream))
            {
                list.Add(s);
            }
            Songs = list.ToArray();
            return Task.CompletedTask;
        }

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
