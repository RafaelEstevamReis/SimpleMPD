using System;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using Simple.MPD.Interfaces;

namespace Simple.MPD.Responses
{
    public class Status : IResponse
    {
        public string Partition { get; set; }
        public int Volume { get; set; }
        public bool Repeat { get; set; }
        public bool Random { get; set; }

        public StatusSingle Single { get; set; }
        public bool Consume { get; set; }
        public int Playlist { get; set; }
        public int PlaylistLength { get; set; }
        public SongState State { get; set; }
        public int Song { get; set; }
        public int SongId { get; set; }
        public int NextSong { get; set; }
        public int NextSongId { get; set; }
        public TimeSpan Elapsed { get; set; }
        public TimeSpan Duration { get; set; }
        public int Bitrate { get; set; }
        public int XFade { get; set; }
        public double MixRampDB { get; set; }
        public int MixRampDelay { get; set; }
        public string AudioFormat { get; set; }
        public int Updating_DB { get; set; }

        public string ErrorMessage { get; set; }

        public ICommand GetCommand()
        {
            return null;
        }

        public async Task ReadAsync(StreamReader stream)
        {
            var values = Helper.ResponseHelper.ReadValuesAsync(stream);
            await foreach (var pair in values)
            {
                switch (pair.Key.ToLower())
                {
                    case "partition":
                        Partition = pair.Value.Trim();
                        break;
                    case "volume":
                        Volume = int.Parse(pair.Value);
                        break;
                    case "repeat":
                        Repeat = int.Parse(pair.Value) == 1;
                        break;
                    case "random":
                        Random = int.Parse(pair.Value) == 1;
                        break;
                    case "single":
                        Single = pair.Value.Trim() switch
                        {
                            "0" => StatusSingle.S0,
                            "1" => StatusSingle.S1,
                            _ => StatusSingle.Oneshot
                        };
                        break;
                    case "consume":
                        Consume = int.Parse(pair.Value) == 1;
                        break;
                    case "playlist":
                        Playlist = int.Parse(pair.Value);
                        break;

                    case "playlistlength":
                        PlaylistLength = int.Parse(pair.Value);
                        break;
                    case "mixrampdb":
                        MixRampDB = double.Parse(pair.Value, CultureInfo.InvariantCulture);
                        break;
                    case "mixrampdelay":
                        MixRampDelay = int.Parse(pair.Value);
                        break;
                    case "state":
                        State = pair.Value.Trim() switch
                        {
                            "play" => SongState.Play,
                            "pause" => SongState.Pause,
                            _ => SongState.Stop
                        };
                        break;
                    case "song":
                        Song = int.Parse(pair.Value);
                        break;
                    case "songid":
                        SongId = int.Parse(pair.Value);
                        break;

                    case "elapsed":
                        Elapsed = TimeSpan.FromSeconds(double.Parse(pair.Value, CultureInfo.InvariantCulture));
                        break;
                    case "duration":
                        Duration = TimeSpan.FromSeconds(double.Parse(pair.Value, CultureInfo.InvariantCulture));
                        break;
                    case "bitrate":
                        Bitrate = int.Parse(pair.Value);
                        break;
                    case "audio":
                        AudioFormat = pair.Value;
                        break;

                    case "updating_db":
                        Updating_DB = int.Parse(pair.Value);
                        break;
                    case "error":
                        ErrorMessage = pair.Value.Trim();
                        break;
                }
            }
        }
    }
}
