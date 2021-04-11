using System.IO;
using System.Threading.Tasks;
using Simple.MPD.Interfaces;

namespace Simple.MPD.Commands
{
    /// <summary>
    /// Executes "Repeat"
    /// </summary>
    public class Repeat : ICommand
    {
        /// <summary>
        /// Set repeat value
        /// </summary>
        public bool SetRepeat { get; set; }
        /// <summary>
        /// Command name
        /// </summary>
        public string CommandName => "Repeat";
        /// <summary>
        /// Creates a new instance
        /// </summary>
        public Repeat(bool repeat)
        {
            SetRepeat = repeat;
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
            await stream.WriteAsync($"random { (SetRepeat ? 1 : 0) }\n");
        }
    }
}
