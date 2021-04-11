using System.IO;
using System.Threading.Tasks;
using Simple.MPD.Interfaces;

namespace Simple.MPD.Commands
{
    /// <summary>
    /// Executes "Decoders"
    /// </summary>
    public class Decoders : ICommand
    {
        /// <summary>
        /// Command name
        /// </summary>
        public string CommandName => "Decoders";
        /// <summary>
        /// Default Response processor
        /// </summary>
        public IResponse GetResponseProcessor()
        {
            return new Responses.ValuesList();
        }
        /// <summary>
        /// Writes command to stream
        /// </summary>
        public async Task WriteAsync(StreamWriter stream)
        {
            await stream.WriteAsync("decoders\n");
        }
    }
}
