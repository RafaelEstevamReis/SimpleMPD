using System.IO;
using System.Threading.Tasks;
using Simple.MPD.Interfaces;

namespace Simple.MPD.Commands
{
    /// <summary>
    /// Executes "Pause"
    /// </summary>
    public class Pause : ICommand
    {  
        /// <summary>
        /// Pause state for the current command
        /// </summary>
        public PauseState PauseState { get; set; }
        /// <summary>
        /// Command name
        /// </summary>
        public string CommandName => "Pause";
        /// <summary>
        /// Creates a new instance
        /// </summary>
        public Pause(PauseState pauseState)
        {
            this.PauseState = pauseState;
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
            if (PauseState == PauseState.Toggle)
            {
                await stream.WriteAsync($"pause\n");
            }
            else
            {
                await stream.WriteAsync($"pause { (PauseState == PauseState.Pause ? 1 : 0) } \n");
            }
        }
    }
}
