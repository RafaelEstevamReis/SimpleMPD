using System.IO;
using System.Threading.Tasks;
using Simple.MPD.Interfaces;

namespace Simple.MPD.Commands
{
    public class DeleteId : ICommand
    {
        public string Uri { get; }

        public DeleteId(string uri)
        {
            Uri = uri;
        }

        public string CommandName => "DeleteId";

        public IResponse GetResponseProcessor()
        {
            return new Responses.ValuesList();
        }

        public async Task WriteAsync(StreamWriter stream)
        {
            string sUri = Helper.EscapingHelper.Escape(Uri);
            await stream.WriteAsync($"deleteid \"{sUri}\"\n");
        }
    }
}
