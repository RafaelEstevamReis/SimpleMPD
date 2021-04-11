using Simple.MPD.Interfaces;
using System.IO;
using System.Threading.Tasks;

namespace Simple.MPD.Commands
{
    /// <summary>
    /// Executes "Random"
    /// </summary>
    public class Random : ICommand
    {
        /// <summary>
        /// Set random mode value
        /// </summary>
        public bool SetRandom { get; set; }
        /// <summary>
        /// Command name
        /// </summary>
        public string CommandName => "Random";
        /// <summary>
        /// Creates a new instance
        /// </summary>
        public Random(bool random)
        {
            SetRandom = random;
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
            await stream.WriteAsync($"random { (SetRandom ? 1 : 0) }\n");
        }
    }
}
