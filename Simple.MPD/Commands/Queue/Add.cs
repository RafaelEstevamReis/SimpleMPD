using System.IO;
using System.Threading.Tasks;
using Simple.MPD.Interfaces;

namespace Simple.MPD.Commands
{
    public class Add : ICommand
    {
        public string Uri { get; }
        public Add(string uri)
        {
            Uri = uri;
        }

        public string CommandName => "Add";

        public IResponse GetResponseProcessor()
        {
            return new Responses.Ok();
        }

        public async Task WriteAsync(StreamWriter stream)
        {
            string sUri = Helper.EscapingHelper.Escape(Uri);
            await stream.WriteAsync($"add \"{sUri}\"\n");
        }
    }
}
