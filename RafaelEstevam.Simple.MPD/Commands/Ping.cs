using System.IO;
using System.Threading.Tasks;
using RafaelEstevam.Simple.MPD.Interfaces;

namespace RafaelEstevam.Simple.MPD.Commands
{
    public class Ping : ICommand
    {
        public string CommandName => "Ping";

        public IResponse GetResponseProcessor()
        {
            return new Responses.Ok();
        }

        public async Task WriteAsync(Stream stream)
        {
            using StreamWriter writer = new StreamWriter(stream, Constants.DefaultEncoding);
            await writer.WriteAsync("ping\n");
        }
    }
}
