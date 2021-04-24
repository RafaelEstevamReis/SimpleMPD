using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Simple.MPD.Helper
{
    public static class DirectoryHelper
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
    }
}
