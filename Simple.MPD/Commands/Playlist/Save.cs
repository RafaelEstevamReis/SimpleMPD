using Simple.MPD.Interfaces;
using System.IO;
using System.Threading.Tasks;

namespace Simple.MPD.Commands.Playlist
{
    public class Save : ICommand
    {
        public string Name { get; }

        public Save(string name)
        {
            Name = name;
        }

        public string CommandName => "Save";

        public IResponse GetResponseProcessor()
        {
            return new Responses.Ok();
        }

        public async Task WriteAsync(StreamWriter stream)
        {
            string sName = Helper.EscapingHelper.Escape(Name);
            await stream.WriteAsync($"save \"{sName}\"\n");
        }
    }
}
