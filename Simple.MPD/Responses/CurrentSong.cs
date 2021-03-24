﻿using System;
using System.IO;
using System.Threading.Tasks;
using Simple.MPD.Interfaces;

namespace Simple.MPD.Responses
{
    public class CurrentSong : IResponse
    {
        public string File { get; set; }
        public string Name { get; set; }

        public TimeSpan Time { get; set; }
        public TimeSpan Duration { get; set; }

        //public Guid MUSICBRAINZ_ARTISTID { get; set; }
        //public Guid MUSICBRAINZ_ALBUMARTISTID { get; set; }
        public string Artist { get; set; }
        public string AlbumArtist { get; set; }
        public string ArtistSort { get; set; }
        public string AlbumArtistSort { get; set; }
        public string Title { get; set; }
        public string Album { get; set; }
        public string Track { get; set; }

        public int Pos { get; set; }
        public int Id { get; set; }

        public ICommand GetCommand()
        {
            return null;
        }

        public async Task ReadAsync(StreamReader stream)
        {
            double dVal;
            var values = Helper.ResponseHelper.ReadValuesAsync(stream);
            await foreach (var pair in values)
            {
                switch (pair.Key.ToLower())
                {
                    case "file":
                        File = pair.Value.Trim();
                        break;
                    case "name":
                        Name = pair.Value.Trim();
                        break;

                    case "Artist":
                        Artist = pair.Value.Trim();
                        break;
                    case "AlbumArtist":
                        AlbumArtist = pair.Value.Trim();
                        break;
                    case "ArtistSort":
                        ArtistSort = pair.Value.Trim();
                        break;
                    case "AlbumArtistSort":
                        AlbumArtistSort = pair.Value.Trim();
                        break;
                    case "Title":
                        Title = pair.Value.Trim();
                        break;
                    case "Album":
                        Album = pair.Value.Trim();
                        break;
                    case "Track":
                        Track = pair.Value.Trim();
                        break;

                    case "time":
                        dVal = double.Parse(pair.Value, System.Globalization.CultureInfo.InvariantCulture);
                        Time = TimeSpan.FromSeconds(dVal);
                        break;
                    case "duration":
                        dVal = double.Parse(pair.Value, System.Globalization.CultureInfo.InvariantCulture);
                        Duration = TimeSpan.FromSeconds(dVal);
                        break;

                    case "pos":
                        Pos = int.Parse(pair.Value);
                        break;
                    case "id":
                        Id = int.Parse(pair.Value);
                        break;
                }
            }
        }
    }
}