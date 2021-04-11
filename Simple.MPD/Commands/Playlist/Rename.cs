using Simple.MPD.Interfaces;
using System.IO;
using System.Threading.Tasks;

namespace Simple.MPD.Commands
{
    public class Rename : ICommand
    {
        public string OldName { get; }
        public string NewName { get; }

        public Rename(string oldName, string newName)
        {
            OldName = oldName;
            NewName = newName;
        }

        public string CommandName => "Rename";

        public IResponse GetResponseProcessor()
        {
            return new Responses.Ok();
        }

        public async Task WriteAsync(StreamWriter stream)
        {
            string sOld = Helper.EscapingHelper.Escape(OldName);
            string sNew = Helper.EscapingHelper.Escape(NewName);
            await stream.WriteAsync($"rename \"{sOld}\" \"{sNew}\"\n");
        }
    }
}
