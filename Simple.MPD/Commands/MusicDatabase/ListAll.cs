using System.IO;
using System.Threading.Tasks;
using Simple.MPD.Interfaces;

namespace Simple.MPD.Commands
{
    public class ListAll : ICommand
    {
        public string CommandName => "ListAll";

        public IResponse GetResponseProcessor()
        {
            return new Responses.SongInfoCollection();
        }

        public async Task WriteAsync(StreamWriter stream)
        {
                await stream.WriteAsync($"listall\n");
        }
    }
}
