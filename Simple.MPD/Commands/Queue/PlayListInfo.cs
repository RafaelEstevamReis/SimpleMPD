using System.IO;
using System.Threading.Tasks;
using Simple.MPD.Interfaces;

namespace Simple.MPD.Commands
{
    /// <summary>
    /// Executes "PlayListInfo"
    /// </summary>
    public class PlayListInfo : ICommand
    {
        /// <summary>
        /// Command name
        /// </summary>
        public string CommandName => "PlayListInfo";
        /// <summary>
        /// Default Response processor
        /// </summary>
        public IResponse GetResponseProcessor()
        {
            return new Responses.SongInfoCollection();
        }
        /// <summary>
        /// Writes command to stream
        /// </summary>
        public async Task WriteAsync(StreamWriter stream)
        {
            await stream.WriteAsync("playlistinfo\n");
        }
    }
}
