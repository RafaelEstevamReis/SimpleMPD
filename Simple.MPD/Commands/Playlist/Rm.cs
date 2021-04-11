using Simple.MPD.Interfaces;
using System.IO;
using System.Threading.Tasks;

namespace Simple.MPD.Commands
{
    /// <summary>
    /// Executes "Rm"
    /// </summary>
    public class Rm : ICommand
    {
        /// <summary>
        /// Playlist name
        /// </summary>
        public string Name { get; }
        /// <summary>
        /// Command name
        /// </summary>
        public string CommandName => "Rm";
        /// <summary>
        /// Creates a new instance
        /// </summary>
        public Rm(string name)
        {
            Name = name;
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
            await stream.WriteAsync($"rm \"{sName}\"\n");
        }
    }
}
