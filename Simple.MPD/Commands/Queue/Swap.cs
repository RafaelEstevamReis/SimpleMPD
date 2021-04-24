using Simple.MPD.Interfaces;
using System.IO;
using System.Threading.Tasks;

namespace Simple.MPD.Commands
{
    public class Swap : ICommand
    {
        /// <summary>
        /// Command name
        /// </summary>
        public string CommandName => "Swap";
        /// <summary>
        /// First song position
        /// </summary>
        public int Pos1 { get; }
        /// <summary>
        /// Second song position
        /// </summary>
        public int Pos2 { get; }

        /// <summary>
        /// Creates a new instance
        /// </summary>
        public Swap(int pos1, int pos2)
        {
            Pos1 = pos1;
            Pos2 = pos2;
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
            await stream.WriteAsync($"swap {Pos1} {Pos2}\n");
        }
    }
}
