using System.IO;
using System.Threading.Tasks;
using RafaelEstevam.Simple.MPD.Interfaces;

namespace RafaelEstevam.Simple.MPD.Commands
{
    public class Crossfade : ICommand
    {
        public int XFade { get; set; }

        public Crossfade(int xFade)
        {
            XFade = xFade;
        }

        public string CommandName => "Crossfade";

        public IResponse GetResponseProcessor()
        {
            return new Responses.Ok();
        }

        public async Task WriteAsync(StreamWriter stream)
        {
            await stream.WriteAsync($"crossfade { XFade }\n");
        }
    }
}
