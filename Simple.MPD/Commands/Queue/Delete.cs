using System.IO;
using System.Threading.Tasks;
using Simple.MPD.Interfaces;

namespace Simple.MPD.Commands
{
    public class Delete : ICommand
    {
        public string Uri { get; }

        public Delete(string uri)
        {
            Uri = uri;
        }

        public string CommandName => "Delete";

        public IResponse GetResponseProcessor()
        {
            return new Responses.Ok();
        }

        public async Task WriteAsync(StreamWriter stream)
        {
            string sUri = Helper.EscapingHelper.Escape(Uri);
            await stream.WriteAsync($"delete \"{sUri}\"\n");
        }
    }
}
