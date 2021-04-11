using System.IO;
using System.Threading.Tasks;
using Simple.MPD.Interfaces;

namespace Simple.MPD.Commands
{
    /// <summary>
    /// Executes "CurrentSong"
    /// </summary>
    public class CurrentSong : ICommand
    {
        /// <summary>
        /// Command name
        /// </summary>
        public string CommandName => "CurrentSong";
        /// <summary>
        /// Default Response processor
        /// </summary>
        public IResponse GetResponseProcessor()
        {
            return new Responses.SongInfo();
        }
        /// <summary>
        /// Writes command to stream
        /// </summary>
        public async Task WriteAsync(StreamWriter stream)
        {
            await stream.WriteAsync("currentsong\n");
        }
    }
}
