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
        /// Pause states
        /// </summary>
        public enum State
        {
            /// <summary>
            /// Pause playback
            /// </summary>
            Pause,
            /// <summary>
            /// Resume paused playback
            /// </summary>
            Resume,
            /// <summary>
            /// Toogle playbakc pause state
            /// </summary>
            Toggle
        }
        /// <summary>
        /// Pause state for the current command
        /// </summary>
        public State PauseState { get; set; }
        /// <summary>
        /// Command name
        /// </summary>
        public string CommandName => "Pause";
        /// <summary>
        /// Creates a new instance
        /// </summary>
        public Pause(State pauseState)
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
            if (PauseState == State.Toggle)
            {
                await stream.WriteAsync($"pause\n");
            }
            else
            {
                await stream.WriteAsync($"pause { (PauseState == State.Pause ? 1 : 0) } \n");
            }
        }
    }
}
