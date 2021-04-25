using System;
using System.IO;
using System.Threading.Tasks;
using Simple.MPD.Interfaces;

namespace Simple.MPD.Responses
{
    /// <summary>
    /// Stats response
    /// </summary>
    public class Stats : IResponse
    {
        /// <summary>
        /// daemon uptime in seconds
        /// </summary>
        public int Uptime { get; set; }
        /// <summary>
        /// time length of music played
        /// </summary>
        public int PlayTime { get; set; }
        /// <summary>
        /// number of artists
        /// </summary>
        public int Artists { get; set; }
        /// <summary>
        /// number of albums
        /// </summary>
        public int Albums { get; set; }
        /// <summary>
        /// number of songs
        /// </summary>
        public int Songs { get; set; }
        /// <summary>
        /// sum of all song times in the database in seconds
        /// </summary>
        public int DB_PlayTime { get; set; }
        /// <summary>
        /// last db update
        /// </summary>
        public DateTimeOffset DB_Update { get; set; }

        /// <summary>
        /// Read response from stream
        /// </summary>
        public Task ReadAsync(StreamReader stream)
        {
            var values = Helper.ResponseHelper.ReadPairs(stream);
            foreach (var pair in values)
            {
                switch (pair.Key)
                {
                    case "uptime":
                        Uptime = int.Parse(pair.Value);
                        break;
                    case "playtime":
                        PlayTime = int.Parse(pair.Value);
                        break;
                    case "artists":
                        Artists = int.Parse(pair.Value);
                        break;
                    case "albums":
                        Albums = int.Parse(pair.Value);
                        break;
                    case "songs":
                        Songs = int.Parse(pair.Value);
                        break;
                    case "db_playtime":
                        DB_PlayTime = int.Parse(pair.Value);
                        break;
                    case "db_update":
                        DB_Update = DateTimeOffset.FromUnixTimeSeconds(int.Parse(pair.Value));
                        break;
                }
            }
            
            return Task.CompletedTask;
        }
    }
}