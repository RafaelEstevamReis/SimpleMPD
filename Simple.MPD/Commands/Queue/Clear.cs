using System.IO;
using System.Threading.Tasks;
using Simple.MPD.Interfaces;

namespace Simple.MPD.Commands
{
    /// <summary>
    /// Executes "Clear"
    /// </summary>
    public class Clear : ICommand
    {
        /// <summary>
        /// Command name
        /// </summary>
        public string CommandName => "Clear";
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
            await stream.WriteAsync("clear\n");
        }
    }
}
