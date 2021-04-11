using System;
using System.IO;
using System.Threading.Tasks;
using Simple.MPD.Interfaces;

namespace Simple.MPD.Commands
{
    /// <summary>
    /// Executes "Single"
    /// </summary>
    public class Single : ICommand
    {
        /// <summary>
        /// State to set
        /// </summary>
        public SingleStates State { get; set; }
        /// <summary>
        /// Command name
        /// </summary>
        public string CommandName => "Single";
        /// <summary>
        /// Creates a new instance
        /// </summary>
        public Single(SingleStates state)
        {
            State = state;
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
            string sVal = State switch
            {
                SingleStates.On => "1",
                SingleStates.Off => "0",
                SingleStates.OneShot => "oneshot",
                _ => throw new InvalidOperationException($"Invalid state {State}"),
            };
            await stream.WriteAsync($"single {sVal}\n");
        }
    }
}
