using System.IO;
using System.Threading.Tasks;
using Simple.MPD.Interfaces;

namespace Simple.MPD.Commands
{
    public class PlayListInfo : ICommand
    {
        public string CommandName => "PlayListInfo";

        public IResponse GetResponseProcessor()
        {
            return new Responses.SongInfoCollection();
        }

        public async Task WriteAsync(StreamWriter stream)
        {
            await stream.WriteAsync("playlistinfo\n");
        }
    }
}
