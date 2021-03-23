using System.IO;
using System.Threading.Tasks;
using Simple.MPD.Interfaces;

namespace Simple.MPD.Commands
{
    public  class CurrentSong : ICommand
    {
        public string CommandName => "CurrentSong";

        public IResponse GetResponseProcessor()
        {
            return new Responses.CurrentSong();
        }

        public async Task WriteAsync(StreamWriter stream)
        {
            await stream.WriteAsync("currentsong\n");
        }
    }
}
