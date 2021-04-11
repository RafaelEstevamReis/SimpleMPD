using System.IO;
using System.Threading.Tasks;
using Simple.MPD.Interfaces;

namespace Simple.MPD.Commands
{
    /// <summary>
    /// Executes "ListAll" - avoid using this command
    /// </summary>
    public class ListAll : ICommand
    {
        /// <summary>
        /// Command name
        /// </summary>
        public string CommandName => "ListAll";

        /// <summary>
        /// Default Response processor
        /// </summary>
        public IResponse GetResponseProcessor()
        {
            return new Responses.SongInfoCollection();
        }

        /// <summary>
        /// Writes command to stream
        /// </summary>
        public async Task WriteAsync(StreamWriter stream)
        {
                await stream.WriteAsync($"listall\n");
        }
    }
}
