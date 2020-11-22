using System;
using System.IO;
using System.Threading.Tasks;
using RafaelEstevam.Simple.MPD.Interfaces;

namespace RafaelEstevam.Simple.MPD.Responses
{
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
                    case "uptime":
                        Uptime = int.Parse(parts[1]);
                        break;
                    case "playtime":
                        PlayTime = int.Parse(parts[1]);
                        break;
                    case "artists":
                        Artists = int.Parse(parts[1]);
                        break;
                    case "albums":
                        Albums = int.Parse(parts[1]);
                        break;
                    case "songs":
                        Songs = int.Parse(parts[1]);
                        break;
                    case "db_playtime":
                        DB_PlayTime = int.Parse(parts[1]);
                        break;
                    case "db_update":
                        DB_Update = DateTimeOffset.FromUnixTimeSeconds( int.Parse(parts[1]));
                        break;
                }
            }
        }
    }
}
