using System.IO;
using System.Threading.Tasks;
using Simple.MPD.Interfaces;

namespace Simple.MPD.Commands
{
    /// <summary>
    /// Executes "SetVol"
    /// </summary>
    public class SetVol : ICommand
    {
        /// <summary>
        /// Volume value to SET
        /// </summary>
        public int Volume { get; set; }
        /// <summary>
        /// Command name
        /// </summary>
        public string CommandName => "SetVol";
        /// <summary>
        /// Creates a new instance
        /// </summary>
        /// <param name="volume">Between 0 and 100, out of range values throws an exception</param>
        public SetVol(int volume)
        {
            if (volume < 0) throw new System.ArgumentException("Volume should be greater than zero");
            if (volume > 100) throw new System.ArgumentException("Volume should be smaller or equal to 100");

            Volume = volume;
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
            await stream.WriteAsync($"setvol { Volume }\n");
        }
    }
}
