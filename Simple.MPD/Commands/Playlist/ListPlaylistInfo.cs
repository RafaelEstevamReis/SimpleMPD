using Simple.MPD.Interfaces;
using System.IO;
using System.Threading.Tasks;

namespace Simple.MPD.Commands
{
    /// <summary>
    /// Executes "Find"
    /// </summary>
    public class ListPlaylistInfo : ICommand
    {
        /// <summary>
        /// Playlist name
        /// </summary>
        public string Name { get; }
        /// <summary>
        /// Creates a new instance
        /// </summary>
        public ListPlaylistInfo(string name)
        {
            Name = name;
        }
        /// <summary>
        /// Command name
        /// </summary>
        public string CommandName => "ListPlaylistInfo";
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
            string sName = Helper.EscapingHelper.Escape(Name);
            await stream.WriteAsync($"listplaylistinfo \"{sName}\"\n");
        }
    }
}
