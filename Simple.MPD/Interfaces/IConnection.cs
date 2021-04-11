using System.IO;
using System.Threading.Tasks;

namespace Simple.MPD.Interfaces
{
    /// <summary>
    /// Connection interface
    /// </summary>
    public interface IConnection
    {
        /// <summary>
        /// Get if the connection is active
        /// </summary>
        bool IsConnected { get; }
        /// <summary>
        /// Opens the connection
        /// </summary>
        Task OpenAsync();
        /// <summary>
        /// Opens the connection
        /// </summary>
        void Open();
        /// <summary>
        /// Closes the connection
        /// </summary>
        void Close();
        /// <summary>
        /// Get reader stream
        /// </summary>
        StreamReader GetReader();
        /// <summary>
        /// Get writter stream
        /// </summary>
        StreamWriter GetWriter();
    }
}
