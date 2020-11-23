using System.IO;
using System.Threading.Tasks;
using RafaelEstevam.Simple.MPD.Interfaces;

namespace RafaelEstevam.Simple.MPD.Commands
{
    public class Stats : ICommand
    {
        public string CommandName => "Stats";

        public IResponse GetResponseProcessor()
        {
            return new Responses.Stats();
        }

        public async Task WriteAsync(StreamWriter stream)
        {
            await stream.WriteAsync("stats\n");
        }
    }
}
