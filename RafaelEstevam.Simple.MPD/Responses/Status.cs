using System;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using RafaelEstevam.Simple.MPD.Interfaces;

namespace RafaelEstevam.Simple.MPD.Responses
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
            string line;
            while ((line = await stream.ReadLineAsync()) != "OK")
            {
                if (Helper.ResponseHelper.IsError(line, out Exception ex))
                {
                    throw ex;
                }
                var parts = line.Split(':');

                switch (parts[0])
                {
                    case "partition":
                        Partition = parts[1].Trim();
                        break;
                    case "volume":
                        Volume = int.Parse(parts[1]);
                        break;
                    case "repeat":
                        Repeat = int.Parse(parts[1]) == 1;
                        break;
                    case "random":
                        Random = int.Parse(parts[1]) == 1;
                        break;
                    case "single":
                        Single = parts[1].Trim() switch
                        {
                            "0" => StatusSingle.S0,
                            "1" => StatusSingle.S1,
                            _ => StatusSingle.Oneshot
                        };
                        break;
                    case "consume":
                        Consume = int.Parse(parts[1]) == 1;
                        break;
                    case "playlist":
                        Playlist = int.Parse(parts[1]);
                        break;

                    case "playlistlength":
                        PlaylistLength = int.Parse(parts[1]);
                        break;
                    case "mixrampdb":
                        MixRampDB = double.Parse(parts[1], CultureInfo.InvariantCulture);
                        break;
                    case "mixrampdelay":
                        MixRampDelay = int.Parse(parts[1]);
                        break;
                    case "state":
                        State = parts[1].Trim() switch
                        {
                            "play" => SongState.Play,
                            "pause" => SongState.Pause,
                            _ => SongState.Stop
                        };
                        break;
                    case "song":
                        Song = int.Parse(parts[1]);
                        break;
                    case "songid":
                        SongId = int.Parse(parts[1]);
                        break;

                    case "elapsed":
                        Elapsed = TimeSpan.FromSeconds(double.Parse(parts[1], CultureInfo.InvariantCulture));
                        break;
                    case "duration":
                        Duration = TimeSpan.FromSeconds(double.Parse(parts[1], CultureInfo.InvariantCulture));
                        break;
                    case "bitrate":
                        Bitrate = int.Parse(parts[1]);
                        break;
                    case "audio":
                        AudioFormat = line[6..].Trim();
                        break;


                    case "updating_db":
                        Updating_DB = int.Parse(parts[1]);
                        break;
                    case "error":
                        ErrorMessage = parts[1].Trim();
                        break;

                }
            }
        }
    }
}
