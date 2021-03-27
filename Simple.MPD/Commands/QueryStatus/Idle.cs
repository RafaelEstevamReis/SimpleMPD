using System.IO;
using System.Threading.Tasks;
using Simple.MPD.Interfaces;

namespace Simple.MPD.Commands
{
    public class Idle : ICommand
    {
        public string CommandName => "Idle";

        public IResponse GetResponseProcessor()
        {
            return new Responses.StringArray();
        }

        public async Task WriteAsync(StreamWriter stream)
        {
            await stream.WriteAsync("idle\n");
        }
    }
}
