using System.IO;
using System.Threading.Tasks;
using Simple.MPD.Interfaces;

namespace Simple.MPD.Commands
{
    /// <summary>
    /// Executes "Consume"
    /// </summary>
    public class Consume : ICommand
    {
        /// <summary>
        /// Comsume song mode value
        /// </summary>
        public bool ConsumeSong { get; set; }
        /// <summary>
        /// Command name
        /// </summary>
        public string CommandName => "Consume";
        /// <summary>
        /// Creates a new instance
        /// </summary>
        public Consume(bool consumeSong)
        {
            ConsumeSong = consumeSong;
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
            await stream.WriteAsync($"consume { (ConsumeSong ? 1: 0) }\n");
        }
    }
}
