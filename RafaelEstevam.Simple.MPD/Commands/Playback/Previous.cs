using System.IO;
using System.Threading.Tasks;
using RafaelEstevam.Simple.MPD.Interfaces;

namespace RafaelEstevam.Simple.MPD.Commands
{
    public class Previous : ICommand
    {
        public string CommandName => "Previous";

        public IResponse GetResponseProcessor()
        {
            return new Responses.Ok();
        }

        public async Task WriteAsync(StreamWriter stream)
        {
            await stream.WriteAsync("previous\n");
        }
    }
}
