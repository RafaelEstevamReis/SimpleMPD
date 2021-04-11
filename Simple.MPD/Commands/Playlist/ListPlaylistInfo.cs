using Simple.MPD.Interfaces;
using System.IO;
using System.Threading.Tasks;

namespace Simple.MPD.Commands
{
    public class ListPlaylistInfo : ICommand
    {
        public string Name { get; }

        public ListPlaylistInfo(string name)
        {
            Name = name;
        }

        public string CommandName => "ListPlaylistInfo";

        public IResponse GetResponseProcessor()
        {
            return new Responses.SongInfoCollection();
        }

        public async Task WriteAsync(StreamWriter stream)
        {
            string sName = Helper.EscapingHelper.Escape(Name);
            await stream.WriteAsync($"listplaylistinfo \"{sName}\"\n");
        }
    }
}
