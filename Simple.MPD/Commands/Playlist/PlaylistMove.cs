using Simple.MPD.Interfaces;
using System.IO;
using System.Threading.Tasks;

namespace Simple.MPD.Commands
{
    public class PlaylistMove : ICommand
    {
        public string Name { get; }
        public int From { get; }
        public int To { get; }

        public PlaylistMove(string name, int from, int to)
        {
            Name = name;
            From = from;
            To = to;
        }

        public string CommandName => "PlaylistMove";

        public IResponse GetResponseProcessor()
        {
            return new Responses.Ok();
        }

        public async Task WriteAsync(StreamWriter stream)
        {
            string sName = Helper.EscapingHelper.Escape(Name);
            await stream.WriteAsync($"playlistmove \"{sName}\" {From} {To}\n");
        }
    }
}
