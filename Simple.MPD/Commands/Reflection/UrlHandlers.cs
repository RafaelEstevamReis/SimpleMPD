using System.IO;
using System.Threading.Tasks;
using Simple.MPD.Interfaces;

namespace Simple.MPD.Commands
{
    public class UrlHandlers : ICommand
    {
        public string CommandName => "UrlHandlers";

        public IResponse GetResponseProcessor()
        {
            return new Responses.ValuesList();
        }

        public async Task WriteAsync(StreamWriter stream)
        {
            await stream.WriteAsync("urlhandlers\n");
        }
    }
}
