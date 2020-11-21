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
            throw new System.NotImplementedException();
        }

        public async Task WriteAsync(Stream stream)
        {
            StreamWriter writer = new StreamWriter(stream, Constants.DefaultEncoding);
            await writer.WriteAsync("ping\n");
        }
    }
}
