using System.IO;
using System.Threading.Tasks;
using Simple.MPD.Interfaces;

namespace Simple.MPD.Commands
{
    /// <summary>
    /// Executes "Find"
    /// </summary>
    public class AddId : ICommand
    {
        /// <summary>
        /// File Uri to be added
        /// </summary>
        public string Uri { get; }
        /// <summary>
        /// Position to be added to
        /// </summary>
        public int Position { get; }
        /// <summary>
        /// Command name
        /// </summary>
        public string CommandName => "AddId";
        /// <summary>
        /// Creates a new instance
        /// </summary>
        public AddId(string uri, int position = -1)
        {
            Uri = uri;
            Position = position;
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
            string sUri = Helper.EscapingHelper.Escape(Uri);
            string sPos = Position >= 0 ? $" {Position}" : "";

            await stream.WriteAsync($"addid \"{sUri}\"{sPos}\n");
        }
    }
}
