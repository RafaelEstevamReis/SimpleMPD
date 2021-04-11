using Simple.MPD.Interfaces;
using System.IO;
using System.Threading.Tasks;

namespace Simple.MPD.Commands
{
    /// <summary>
    /// Executes "ListPlaylists"
    /// </summary>
    public class ListPlaylists : ICommand
    {
        /// <summary>
        /// Command name
        /// </summary>
        public string CommandName => "ListPlaylists";
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
            await stream.WriteAsync($"listplaylists\n");
        }
    }
}
