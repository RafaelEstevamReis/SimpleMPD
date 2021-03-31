using System.IO;
using System.Threading.Tasks;
using Simple.MPD.Interfaces;

namespace Simple.MPD.Commands
{
    public class Shuffle : ICommand
    {
        public string CommandName => "Shuffle";

        public IResponse GetResponseProcessor()
        {
            return new Responses.Ok();
        }
        public async Task WriteAsync(StreamWriter stream)
        {
            await stream.WriteAsync("shuffle\n");
        }
    }
}
