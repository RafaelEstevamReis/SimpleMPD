using System.IO;
using System.Threading.Tasks;
using Simple.MPD.Interfaces;

namespace Simple.MPD.Commands
{
    public class Play : ICommand
    {
        int playSongValue;
        private PlayType playType;

        enum PlayType
        {
            Empty,
            SongPos,
            SongId,
        }

        public Play()
        {
            playSongValue = 0;
            playType = PlayType.Empty;
        }

        public string CommandName => "Play";

        public IResponse GetResponseProcessor()
        {
            return new Responses.Ok();
        }

        public async Task WriteAsync(StreamWriter stream)
        {
            if (playType == PlayType.SongPos)
            {
                await stream.WriteAsync($"play {playSongValue} \n");
            }
            else if (playType == PlayType.SongId)
            {
                await stream.WriteAsync($"playid {playSongValue} \n");
            }
            else
            {
                await stream.WriteAsync($"play\n");
            }
        }

        public static Play PlaySongId(int SongId)
        {
            return new Play()
            {
                playSongValue = SongId,
                playType = PlayType.SongId
            };
        }
        public static Play PlaySongPosition(int SongPos)
        {
            return new Play()
            {
                playSongValue = SongPos,
                playType = PlayType.SongPos
            };
        }
    }
}
