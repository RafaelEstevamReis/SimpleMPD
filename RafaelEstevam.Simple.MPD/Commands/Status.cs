using System.IO;
using System.Threading.Tasks;
using RafaelEstevam.Simple.MPD.Interfaces;

namespace RafaelEstevam.Simple.MPD.Commands
{
    public class Status : ICommand
    {
        public string CommandName => "Status";

        public IResponse GetResponseProcessor()
        {
            return new Responses.Status();
        }

        public async Task WriteAsync(StreamWriter stream)
        {
            await stream.WriteAsync("status\n");
        }
    }
}
