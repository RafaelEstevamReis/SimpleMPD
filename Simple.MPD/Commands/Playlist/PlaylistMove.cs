using Simple.MPD.Interfaces;
using System.IO;
using System.Threading.Tasks;

namespace Simple.MPD.Commands
{
    /// <summary>
    /// Executes "PlaylistMove"
    /// </summary>
    public class PlaylistMove : ICommand
    {
        /// <summary>
        /// Playlist name
        /// </summary>
        public string Name { get; }
        /// <summary>
        /// Original position
        /// </summary>
        public int From { get; }
        /// <summary>
        /// Mew position
        /// </summary>
        public int To { get; }

        /// <summary>
        /// Command name
        /// </summary>
        public string CommandName => "PlaylistMove";
        /// <summary>
        /// Creates a new instance
        /// </summary>
        public PlaylistMove(string name, int from, int to)
        {
            Name = name;
            From = from;
            To = to;
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
            await stream.WriteAsync($"playlistmove \"{sName}\" {From} {To}\n");
        }
    }
}
