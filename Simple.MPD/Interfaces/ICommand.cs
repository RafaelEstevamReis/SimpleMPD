using System.IO;
using System.Threading.Tasks;

namespace Simple.MPD.Interfaces
{
    /// <summary>
    /// Command interface
    /// </summary>
    public interface ICommand
    {
        /// <summary>
        /// Command name
        /// </summary>
        string CommandName { get; }
        /// <summary>
        /// Writes the command to stream
        /// </summary>
        Task WriteAsync(StreamWriter stream);
        /// <summary>
        /// Gets default response processor
        /// </summary>
        IResponse GetResponseProcessor();
    }
}
