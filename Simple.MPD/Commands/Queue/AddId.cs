using System.IO;
using System.Threading.Tasks;
using Simple.MPD.Interfaces;

namespace Simple.MPD.Commands
{
    public class AddId : ICommand
    {
        public string Uri { get; }
        public int Position { get; }

        public AddId(string uri, int position = -1)
        {
            Uri = uri;
            Position = position;
        }

        public string CommandName => "AddId";

        public IResponse GetResponseProcessor()
        {
            return new Responses.ValuesList();
        }

        public async Task WriteAsync(StreamWriter stream)
        {
            string sUri = Helper.EscapingHelper.Escape(Uri);
            string sPos = Position >= 0 ? $" {Position}" : "";

            await stream.WriteAsync($"addid \"{sUri}\"{sPos}\n");
        }
    }
}
