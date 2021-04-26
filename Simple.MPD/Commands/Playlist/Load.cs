using Simple.MPD.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Simple.MPD.Commands
{
    /// <summary>
    /// Executes "Load"
    /// </summary>
    public class Load : ICommand
    {
        /// <summary>
        /// Playlist name
        /// </summary>
        public string Name { get; }
        /// <summary>
        /// Optional name to be loaded
        /// </summary>
        public Range Range { get; }
        /// <summary>
        /// Command name
        /// </summary>
        public string CommandName => "Load";
        /// <summary>
        /// Creates a new instance
        /// </summary>
        public Load(string name, Range range = null)
        {
            Name = name;
            Range = range;
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
            string sName = Helper.EscapingHelper.Escape(Name);
            string sRange = Range?.ToString() ?? "" ;
            await stream.WriteAsync($"load \"{sName}\" {sRange}\n");
        }
    }
}
