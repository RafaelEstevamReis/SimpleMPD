using System.IO;
using System.Threading.Tasks;
using Simple.MPD.Interfaces;

namespace Simple.MPD.Commands
{
    /// <summary>
    /// Executes "Delete"
    /// </summary>
    public class Delete : ICommand
    {
        /// <summary>
        /// Song uri
        /// </summary>
        public string Uri { get; }
        /// <summary>
        /// Command name
        /// </summary>
        public string CommandName => "Delete";
        /// <summary>
        /// Creates a new instance
        /// </summary>
        public Delete(string uri)
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
            await stream.WriteAsync($"delete \"{sUri}\"\n");
        }
    }
}
