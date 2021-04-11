using Simple.MPD.Interfaces;
using System.IO;
using System.Threading.Tasks;

namespace Simple.MPD.Commands
{
    /// <summary>
    /// Executes "PlaylistDelete"
    /// </summary>
    public class PlaylistDelete : ICommand
    {
        /// <summary>
        /// Playlist name
        /// </summary>
        public string Name { get; }
        /// <summary>
        /// Song position to delete
        /// </summary>
        public int SongPos { get; }

        /// <summary>
        /// Command name
        /// </summary>
        public string CommandName => "PlaylistDelete";
        /// <summary>
        /// Creates a new instance
        /// </summary>
        public PlaylistDelete(string name, int songPos)
        {
            Name = name;
            SongPos = songPos;
        }
        /// <summary>
        /// Default Response processor
        /// </summary>
        public IResponse GetResponseProcessor()
        {
            return new Responses.Ok();
        }
        /// <summary>
        /// Writes command to stream
        /// </summary>
        public async Task WriteAsync(StreamWriter stream)
        {
            string sName = Helper.EscapingHelper.Escape(Name);
            await stream.WriteAsync($"playlistdelete \"{sName}\" {SongPos}\n");
        }
    }
}
