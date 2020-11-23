using System.IO;
using System.Threading.Tasks;
using RafaelEstevam.Simple.MPD.Interfaces;

namespace RafaelEstevam.Simple.MPD.Commands
{
    public class Stop : ICommand
    {
        public string CommandName => "Stop";

        public IResponse GetResponseProcessor()
        {
            return new Responses.Ok();
        }

        public async Task WriteAsync(StreamWriter stream)
        {
            await stream.WriteAsync("stop\n");
        }
    }
}
