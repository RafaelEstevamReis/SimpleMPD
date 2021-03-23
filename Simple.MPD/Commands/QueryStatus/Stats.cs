using System.IO;
using System.Threading.Tasks;
using Simple.MPD.Interfaces;

namespace Simple.MPD.Commands
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
