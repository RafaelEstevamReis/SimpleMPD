using System.IO;
using System.Threading.Tasks;

namespace Simple.MPD.Interfaces
{
    /// <summary>
    /// Response interface
    /// </summary>
    public interface IResponse
    {
        /// <summary>
        /// Read response from stream
        /// </summary>
        Task ReadAsync(StreamReader stream);
        /// <summary>
        /// Get command
        /// </summary>
        ICommand GetCommand();
    }
}
