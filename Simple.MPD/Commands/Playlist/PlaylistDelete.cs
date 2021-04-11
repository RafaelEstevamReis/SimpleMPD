using Simple.MPD.Interfaces;
using System.IO;
using System.Threading.Tasks;

namespace Simple.MPD.Commands
{
    public  class PlaylistDelete : ICommand
    {
        public string Name { get; }
        public int SongPos { get; }

        public PlaylistDelete(string name, int songPos)
        {
            Name = name;
            SongPos = songPos;
        }

        public string CommandName => "PlaylistDelete";

        public IResponse GetResponseProcessor()
        {
            return new Responses.Ok();
        }

        public async Task WriteAsync(StreamWriter stream)
        {
            string sName = Helper.EscapingHelper.Escape(Name);
            await stream.WriteAsync($"playlistdelete \"{sName}\" {SongPos}\n");
        }
    }
}
