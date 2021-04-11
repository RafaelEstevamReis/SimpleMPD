using Simple.MPD.Interfaces;
using System.IO;
using System.Threading.Tasks;

namespace Simple.MPD.Commands
{
    /// <summary>
    /// Executes "Rename"
    /// </summary>
    public class Rename : ICommand
    {
        /// <summary>
        /// Old (current) playlist name
        /// </summary>
        public string OldName { get; }
        /// <summary>
        /// New playlist name
        /// </summary>
        public string NewName { get; }
        /// <summary>
        /// Command name
        /// </summary>
        public string CommandName => "Rename";
        /// <summary>
        /// Creates a new instance
        /// </summary>
        public Rename(string oldName, string newName)
        {
            OldName = oldName;
            NewName = newName;
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
            string sOld = Helper.EscapingHelper.Escape(OldName);
            string sNew = Helper.EscapingHelper.Escape(NewName);
            await stream.WriteAsync($"rename \"{sOld}\" \"{sNew}\"\n");
        }
    }
}
