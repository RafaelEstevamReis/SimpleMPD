using System.IO;
using System.Threading.Tasks;
using RafaelEstevam.Simple.MPD.Interfaces;

namespace RafaelEstevam.Simple.MPD.Commands
{
    public class Next : ICommand
    {
        public string CommandName => "Next";

        public IResponse GetResponseProcessor()
        {
            return new Responses.Ok();
        }

        public async Task WriteAsync(StreamWriter stream)
        {
            await stream.WriteAsync("next\n");
        }
    }
}
