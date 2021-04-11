using Simple.MPD.Interfaces;
using System.IO;
using System.Threading.Tasks;

namespace Simple.MPD.Commands
{
    public class ListPlaylists : ICommand
    {
        public string CommandName => "ListPlaylists";

        public IResponse GetResponseProcessor()
        {
            return new Responses.SongInfoCollection();
        }

        public async Task WriteAsync(StreamWriter stream)
        {
            await stream.WriteAsync($"listplaylists\n");
        }
    }
}
