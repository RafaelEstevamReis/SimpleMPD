using System.IO;
using System.Threading.Tasks;
using Simple.MPD.Interfaces;

namespace Simple.MPD.Commands
{
    /// <summary>
    /// Executes "LsInfo"
    /// </summary>
    public class LsInfo : ICommand
    {
        /// <summary>
        /// Uri to list
        /// </summary>
        public string Uri { get; }
        /// <summary>
        /// Command name
        /// </summary>
        public string CommandName => "LsInfo";
        /// <summary>
        /// Creates a new instance
        /// </summary>
        public LsInfo(string uri)
        {
            Uri = uri;
        }
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
            if (!string.IsNullOrWhiteSpace(Uri))
            {
                string sUri = Helper.EscapingHelper.Escape(Uri);
                await stream.WriteAsync($"lsinfo \"{sUri}\"\n");
            }
            else
            {
                await stream.WriteAsync($"lsinfo\n");
            }
        }
    }
}
