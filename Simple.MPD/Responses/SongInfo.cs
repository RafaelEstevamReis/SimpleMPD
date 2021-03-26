using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Simple.MPD.Interfaces;

namespace Simple.MPD.Responses
{
    public class SongInfo : IResponse
    {
        public string File { get; set; }
        public string Name { get; set; }

        public TimeSpan Elapsed { get; set; }
        public TimeSpan Duration { get; set; }

        public Guid MUSICBRAINZ_ALBUMID { get; set; }
        public Guid MUSICBRAINZ_ARTISTID { get; set; }
        public Guid MUSICBRAINZ_ALBUMARTISTID { get; set; }
        public Guid MUSICBRAINZ_RELEASETRACKID { get; set; }
        public Guid MUSICBRAINZ_TRACKID { get; set; }

        public string Artist { get; set; }
        public string AlbumArtist { get; set; }
        public string ArtistSort { get; set; }
        public string AlbumArtistSort { get; set; }
        public string Title { get; set; }
        public string Album { get; set; }
        public string Track { get; set; }

        public int Pos { get; set; }
        public int Id { get; set; }


        public string SongDisplayName
        {
            get
            {
                if (Title != null) return $"{AlbumArtist ?? Artist ?? "[Unkown]"} - {Title}";
                if (Name != null) return Name;
                return File;
            }
        }
        public ICommand GetCommand() => null;

        public async Task ReadAsync(StreamReader stream)
        {
            var values = Helper.ResponseHelper.ReadValuesAsync(stream);
            await foreach (var pair in values)
            {
                assingKeyValuePair(pair);

                // id is ever the last one
                if (pair.Key == "Id") break;
            }
        }

        private void assingKeyValuePair((string Key, string Value) pair)
        {
            double dVal;
            switch (pair.Key.ToLower())
            {
                case "file":
                    File = pair.Value.Trim();
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
                case "track":
                    Track = pair.Value.Trim();
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

                // case "musicbrainz_albumid":
                //     MUSICBRAINZ_ALBUMID = Guid.Parse(pair.Value);
                //     break;
                // case "musicbrainz_artistid":
                //     MUSICBRAINZ_ARTISTID = Guid.Parse(pair.Value);
                //     break;
                // case "musicbrainz_albumartistid":
                //     MUSICBRAINZ_ALBUMARTISTID = Guid.Parse(pair.Value);
                //     break;
                // case "musicbrainz_releasetrackid":
                //     MUSICBRAINZ_RELEASETRACKID = Guid.Parse(pair.Value);
                //     break;
                // case "musicbrainz_trackid":
                //     MUSICBRAINZ_TRACKID = Guid.Parse(pair.Value);
                //     break;

                case "pos":
                    Pos = int.Parse(pair.Value);
                    break;
                case "id":
                    Id = int.Parse(pair.Value);
                    break;
            }
        }

        public static async IAsyncEnumerable<SongInfo> ReadAllAsync(StreamReader stream)
        {
            SongInfo current = null;

            var values = Helper.ResponseHelper.ReadValuesAsync(stream);
            await foreach (var pair in values)
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
    }

    public class SongInfoCollection : IResponse, IEnumerable<SongInfo>
    {
        public SongInfo[] Songs { get; private set; }

        public ICommand GetCommand() => null;

        public async Task ReadAsync(StreamReader stream)
        {
            List<SongInfo> list = new List<SongInfo>();
            await foreach (var s in SongInfo.ReadAllAsync(stream))
            {
                list.Add(s);
            }
            Songs = list.ToArray();
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
