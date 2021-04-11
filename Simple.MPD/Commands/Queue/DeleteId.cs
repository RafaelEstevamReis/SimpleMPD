using System.IO;
using System.Threading.Tasks;
using Simple.MPD.Interfaces;

namespace Simple.MPD.Commands
{
    /// <summary>
    /// Executes "DeleteId"
    /// </summary>
    public class DeleteId : ICommand
    {
        /// <summary>
        /// Id of the song
        /// </summary>
        public int Id { get; }

        /// <summary>
        /// Command name
        /// </summary>
        public string CommandName => "DeleteId";
        /// <summary>
        /// Creates a new instance
        /// </summary>
        public DeleteId(int id)
        {
            Id = id;
        }
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
            await stream.WriteAsync($"deleteid {Id}\n");
        }
    }
}
