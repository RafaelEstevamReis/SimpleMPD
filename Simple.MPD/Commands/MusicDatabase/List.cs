using Simple.MPD.Interfaces;
using System.IO;
using System.Threading.Tasks;

namespace Simple.MPD.Commands
{
    /// <summary>
    /// Executes "List"
    /// </summary>
    public class List : ICommand
    {
        /// <summary>
        /// Expression to search/filter
        /// </summary>
        public string Expression { get; }
        /// <summary>
        /// Command name
        /// </summary>
        public string CommandName => "List";
        /// <summary>
        /// Creates a new instance
        /// </summary>
        public List(string expression)
        {
            Expression = expression;
        }
        /// <summary>
        /// Default Response processor
        /// </summary>
        public IResponse GetResponseProcessor()
        {
            return new Responses.StringArray();
        }
        /// <summary>
        /// Writes command to stream
        /// </summary>
        public async Task WriteAsync(StreamWriter stream)
        {
            var expr = Helper.EscapingHelper.Escape(Expression);
            await stream.WriteAsync($"list {expr}\n");
        }
    }
}
