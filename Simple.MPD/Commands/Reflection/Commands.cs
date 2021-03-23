using System.IO;
using System.Threading.Tasks;
using Simple.MPD.Interfaces;

namespace Simple.MPD.Commands
{
    public class Commands : ICommand
    {
        public string CommandName => "Commands";

        public IResponse GetResponseProcessor()
        {
            return new Responses.ValuesList();
        }

        public async Task WriteAsync(StreamWriter stream)
        {
            await stream.WriteAsync("commands\n");
        }
    }
}
