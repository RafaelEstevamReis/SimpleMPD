using System.IO;
using System.Threading.Tasks;
using Simple.MPD.Interfaces;

namespace Simple.MPD.Commands
{
    /// <summary>
    /// Executes "Find"
    /// </summary>
    public class Add : ICommand
    {
        /// <summary>
        /// Uri of the song
        /// </summary>
        public string Uri { get; }
        /// <summary>
        /// Command name
        /// </summary>
        public string CommandName => "Add";
        /// <summary>
        /// Creates a new instance
        /// </summary>
        public Add(string uri)
        {
            Uri = uri;
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
            string sUri = Helper.EscapingHelper.Escape(Uri);
            await stream.WriteAsync($"add \"{sUri}\"\n");
        }
    }
}
