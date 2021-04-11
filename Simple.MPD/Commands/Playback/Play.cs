using System.IO;
using System.Threading.Tasks;
using Simple.MPD.Interfaces;

namespace Simple.MPD.Commands
{
    /// <summary>
    /// Executes "Play" or "PlayId" commands
    /// </summary>
    public class Play : ICommand
    {
        enum PlayType
        {
            Empty,
            SongPos,
            SongId,
        }

        int playSongValue;
        private PlayType playType;

        /// <summary>
        /// Command name
        /// </summary>
        public string CommandName => "Play";
        /// <summary>
        /// Creates a new instance without arguments
        /// </summary>
        public Play()
        {
            playSongValue = 0;
            playType = PlayType.Empty;
        }

        /// <summary>
        /// Default Response processor
        /// </summary>
        public IResponse GetResponseProcessor()
        {
            return new Responses.Ok();
        }
        /// <summary>
        /// Writes command to stream
        /// </summary>
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
        /// <summary>
        /// Creates a new instance of "playid" command with SongId argument
        /// </summary>
        public static Play PlaySongId(int SongId)
        {
            return new Play()
            {
                playSongValue = SongId,
                playType = PlayType.SongId
            };
        }
        /// <summary>
        /// Creates a new instance of "play" command with SongPos argument
        /// </summary>
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
