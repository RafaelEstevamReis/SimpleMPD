using System.IO;
using System.Threading.Tasks;
using Simple.MPD.Interfaces;

namespace Simple.MPD.Commands
{
    public class LsInfo : ICommand
    {
        public string CommandName => "LsInfo";

        public LsInfo(string uri)
        {
            Uri = uri;
        }

        public string Uri { get; }

        public IResponse GetResponseProcessor()
        {
            return new Responses.SongInfoCollection();
        }

        public async Task WriteAsync(StreamWriter stream)
        {
            if (!string.IsNullOrWhiteSpace(Uri))
            {
                string sUri = Helper.EscapingHelper.Escape(Uri);
                await stream.WriteAsync($"lsinfo \"{sUri}\"\n");
            }
            else
            {
                await stream.WriteAsync($"lsinfo\n");
            }
        }
    }
}
