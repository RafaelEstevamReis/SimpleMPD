using System;
using System.IO;
using System.Threading.Tasks;
using Simple.MPD.Interfaces;

namespace Simple.MPD.Commands
{
    public class Single : ICommand
    {
        public States State { get; set; }

        public enum States
        {
            On,
            Off,
            OneShot,
        }

        public Single(States state)
        {
            State = state;
        }

        public string CommandName => "Single";


        public IResponse GetResponseProcessor()
        {
            return new Responses.Ok();
        }

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
