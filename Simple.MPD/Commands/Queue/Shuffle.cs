using System.IO;
using System.Threading.Tasks;
using Simple.MPD.Interfaces;

namespace Simple.MPD.Commands
{
    /// <summary>
    /// Executes "Shuffle"
    /// </summary>
    public class Shuffle : ICommand
    {
        /// <summary>
        /// Command name
        /// </summary>
        public string CommandName => "Shuffle";
        /// <summary>
        /// Optional range to shuffle
        /// </summary>
        public Range Range { get; }
        /// <summary>
        /// Creates a new instance
        /// </summary>
        public Shuffle()
        {
            Range = null;
        }
        /// <summary>
        /// Creates a new instance with range
        /// </summary>
        public Shuffle(Range range)
        {
            Range = range;
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
            if (Range == null)
            {
                await stream.WriteAsync("shuffle\n");
            }
            else
            {
                await stream.WriteAsync($"shuffle {Range}\n");
            }
        }
    }
}
