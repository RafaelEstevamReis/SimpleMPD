using Simple.MPD.Interfaces;
using System.IO;
using System.Threading.Tasks;

namespace Simple.MPD.Commands
{
    public class SwapId : ICommand
    {
        /// <summary>
        /// Command name
        /// </summary>
        public string CommandName => "SwapId";
        /// <summary>
        /// First song position
        /// </summary>
        public int Id1 { get; }
        /// <summary>
        /// Second song position
        /// </summary>
        public int Id2 { get; }

        /// <summary>
        /// Creates a new instance
        /// </summary>
        public SwapId(int id1, int id2)
        {
            Id1 = id1;
            Id2 = id2;
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
            await stream.WriteAsync($"swapid {Id1} {Id2}\n");
        }
    }
}
