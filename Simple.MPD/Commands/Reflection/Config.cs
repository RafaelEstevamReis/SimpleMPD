using System.IO;
using System.Threading.Tasks;
using Simple.MPD.Interfaces;

namespace Simple.MPD.Commands
{
    public class Config : ICommand
    {
        public string CommandName => "Config";

        public IResponse GetResponseProcessor()
        {
            return new Responses.ValuesList();
        }

        public async Task WriteAsync(StreamWriter stream)
        {
            await stream.WriteAsync("config\n");
        }
    }
}
