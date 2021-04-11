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
        /// States for the SINGLE command
        /// </summary>
        public enum States
        {
            /// <summary>
            /// Enables SINGLE mode
            /// </summary>
            On,
            /// <summary>
            /// Disables SINGLE mode
            /// </summary>
            Off,
            /// <summary>
            /// Sets SINGLE to OneShot mode
            /// </summary>
            OneShot,
        }

        /// <summary>
        /// State to set
        /// </summary>
        public States State { get; set; }
        /// <summary>
        /// Command name
        /// </summary>
        public string CommandName => "Single";
        /// <summary>
        /// Creates a new instance
        /// </summary>
        public Single(States state)
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
                States.On => "1",
                States.Off => "0",
                States.OneShot => "oneshot",
                _ => throw new InvalidOperationException($"Invalid state {State}"),
            };
            await stream.WriteAsync($"single {sVal}\n");
        }
    }
}
