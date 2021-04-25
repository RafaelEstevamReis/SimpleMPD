using Simple.MPD.Interfaces;
using System.IO;
using System.Threading.Tasks;

namespace Simple.MPD.Commands
{
    /// <summary>
    /// Executes "MoveId"
    /// </summary>
    public class MoveId : ICommand
    {
        /// <summary>
        /// Command name
        /// </summary>
        public string CommandName => "MoveId";
        /// <summary>
        /// From song
        /// </summary>
        public int FromId { get; }
        /// <summary>
        /// To song
        /// </summary>
        public int ToPosition { get; }

        /// <summary>
        /// Creates a new instance
        /// </summary>
        public MoveId(int fromId, int toPosition)
        {
            FromId = fromId;
            ToPosition = toPosition;
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
            await stream.WriteAsync($"move {FromId} {ToPosition}\n");
        }
    }
}
