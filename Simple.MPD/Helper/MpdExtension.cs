using Simple.MPD.Helper;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Simple.MPD
{
    /// <summary>
    /// MPD extenssions class
    /// </summary>
    public static class MpdExtension
    {
        /// <summary>
        /// Recursive and SLOW call
        /// </summary>
        public static Database ReadAll(this MPD mpd, Action<string> DirectoryProgressReport = null)
        {
            var result = mpd.LsInfo("").Result;

            Directory rootDir = processResult(mpd, result, DirectoryProgressReport);
            rootDir.Name = "";

            return new Database(rootDir);
        }

        private static Directory processResult(MPD mpd, Responses.SongInfoCollection info, Action<string> DirectoryProgressReport)
        {
            List<Directory> lstDirs = new List<Directory>();

            foreach (var d in info.Where(e => e.Directory != null))
            {
                DirectoryProgressReport?.Invoke(d.Directory);

                var result = mpd.LsInfo(d.Directory).Result;

                var dir = processResult(mpd, result, DirectoryProgressReport);
                dir.Name = d.Directory;
                lstDirs.Add(dir);
            }

            return new Directory()
            {
                Files = info.Where(e => e.File != null).ToArray(),
                Directories = lstDirs.ToArray(),
            };
        }
        /// <summary>
        /// Rerads a Local m3u file with MPD music locations and adds to queue
        /// </summary>
        /// <param name="mpd">MPD instance</param>
        /// <param name="LocalFilePath">Local file, NOT MPD path</param>
        /// <param name="pathConverter">Function to convert local path to remote path</param>
        /// <returns>Tup´le with MPD file added and it's SongID</returns>
        public static IEnumerable<KeyValuePair<string, int>> AddLocalM3uFile(this MPD mpd, string LocalFilePath, Func<string, string> pathConverter = null)
        {
            foreach (var line in System.IO.File.ReadAllLines(LocalFilePath))
            {
                if (line.StartsWith("#")) continue;
                int id;
                try
                {
                    string path = line;
                    if (pathConverter != null) path = pathConverter(line);

                    id = mpd.QueueAddId(path).Result;
                }
                catch (Exception)
                {
                    id = -1;
                    mpd.Connection.Close(); // must re-open
                }

                yield return new KeyValuePair<string, int>(line, id);
            }
        }
    }
}
