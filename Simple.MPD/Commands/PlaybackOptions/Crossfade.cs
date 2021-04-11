using System.IO;
using System.Threading.Tasks;
using Simple.MPD.Interfaces;

namespace Simple.MPD.Commands
{
    /// <summary>
    /// Executes "Crossfade"
    /// </summary>
    public class Crossfade : ICommand
    {
        /// <summary>
        /// Crossfade value in seconds
        /// </summary>
        public int XFade { get; set; }
        /// <summary>
        /// Command name
        /// </summary>
        public string CommandName => "Crossfade";
        /// <summary>
        /// Creates a new instance
        /// </summary>
        public Crossfade(int xFade)
        {
            XFade = xFade;
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
            await stream.WriteAsync($"crossfade { XFade }\n");
        }
    }
}
