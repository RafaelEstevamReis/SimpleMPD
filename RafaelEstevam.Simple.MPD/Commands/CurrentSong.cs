using System.IO;
using System.Threading.Tasks;
using RafaelEstevam.Simple.MPD.Interfaces;

namespace RafaelEstevam.Simple.MPD.Commands
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
