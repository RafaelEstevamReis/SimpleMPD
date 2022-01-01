using System;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using Simple.MPD.Interfaces;

namespace Simple.MPD.Responses
{
    /// <summary>
    /// Reports the current status of the player and the volume level
    /// </summary>
    public class Status : IResponse
    {
        /// <summary>
        /// the name of the current partition
        /// </summary>
        public string Partition { get; set; }
        /// <summary>
        /// Volume in the range 0-100
        /// </summary>
        public int Volume { get; set; }
        /// <summary>
        /// If repeat is enabled
        /// </summary>
        public bool Repeat { get; set; }
        /// <summary>
        /// If random is enabled
        /// </summary>
        public bool Random { get; set; }
        /// <summary>
        /// Current 'Single' status
        /// </summary>
        public SingleStates Single { get; set; }
        /// <summary>
        /// If consume mode is enabled
        /// </summary>
        public bool Consume { get; set; }
        /// <summary>
        /// Playlist (queue) version number
        /// </summary>
        public int Playlist { get; set; }
        /// <summary>
        /// The length of the playlist (queue)
        /// </summary>
        public int PlaylistLength { get; set; }
        /// <summary>
        /// Current playback state
        /// </summary>
        public PlaybackState State { get; set; }
        /// <summary>
        /// Playlist (queue) song number of the current song stopped on or playing
        /// </summary>
        public int Song { get; set; }
        /// <summary>
        /// Playlist (queue) songid of the current song stopped on or playing
        /// </summary>
        public int SongId { get; set; }
        /// <summary>
        /// Playlist (queue) song number of the next song to be played
        /// </summary>
        public int NextSong { get; set; }
        /// <summary>
        /// Playlist (queue) songid of the next song to be played
        /// </summary>
        public int NextSongId { get; set; }
        /// <summary>
        /// Total time elapsed of the current (playing/paused) song
        /// </summary>
        public TimeSpan Elapsed { get; set; }
        /// <summary>
        /// Duration of the current song
        /// </summary>
        public TimeSpan Duration { get; set; }
        /// <summary>
        /// instantaneous bitrate in kbps
        /// </summary>
        public int Bitrate { get; set; }
        /// <summary>
        /// Crissfade in seconds
        /// </summary>
        public int XFade { get; set; }
        /// <summary>
        /// Mix ramp threshold in dB
        /// </summary>
        public double MixRampDB { get; set; }
        /// <summary>
        /// Mix ramp delay in seconds
        /// </summary>
        public int MixRampDelay { get; set; }
        /// <summary>
        /// The format emitted by the decoder plugin during playback, format: samplerate:bits:channels.
        /// </summary>
        public string AudioFormat { get; set; }
        /// <summary>
        /// DB updating job id
        /// </summary>
        public int Updating_DB { get; set; }
        /// <summary>
        /// If there is an error, returns message here
        /// </summary>
        public string ErrorMessage { get; set; }
        /// <summary>
        /// Read a response from the stream
        /// </summary>
        public async Task ReadAsync(StreamReader stream)
        {
            await Task.Run(() => read(stream));
        }
        private void read(StreamReader stream)
        {
            var values = Helper.ResponseHelper.ReadPairs(stream);
            foreach (var pair in values)
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
                            "0" => SingleStates.Off,
                            "1" => SingleStates.On,
                            _ => SingleStates.OneShot
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
                            "play" => PlaybackState.Play,
                            "pause" => PlaybackState.Pause,
                            _ => PlaybackState.Stop
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

                    case "xfade":
                    case "crossfade":
                        XFade = int.Parse(pair.Value);
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

        public override string ToString()
        {
            // timespan format was throwing an exception
            return $@"[ARTIS NAME] - [SONG NAME]
[{getPlayStatusName(State)}] #{Song}/{PlaylistLength}   {Elapsed.Minutes:00}:{Elapsed.Seconds:00}/{Duration.Minutes:00}:{Duration.Seconds:00} ({100 * Elapsed.TotalSeconds / Duration.TotalSeconds :00}%)
volume: {Volume:00}%   repeat: {(Repeat?"on":"off")}   random: {(Random ? "on" : "off")}    single: {Single}   consume: {(Consume ? "on" : "off")}";
        }
        private static string getPlayStatusName(PlaybackState state)
        => state switch
        {
            PlaybackState.Play => "playing",
            PlaybackState.Pause => "paused",
            PlaybackState.Stop => "stopped",
            _ => state.ToString(),
        };
    }
}
