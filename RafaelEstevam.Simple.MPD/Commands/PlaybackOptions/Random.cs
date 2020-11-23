using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using RafaelEstevam.Simple.MPD.Interfaces;

namespace RafaelEstevam.Simple.MPD.Commands
{
    public class Random : ICommand
    {
        public bool SetRandom { get; set; }

        public Random(bool random)
        {
            SetRandom = random;
        }

        public string CommandName => "Random";

        public IResponse GetResponseProcessor()
        {
            return new Responses.Ok();
        }

        public async Task WriteAsync(StreamWriter stream)
        {
            await stream.WriteAsync($"random { (SetRandom ? 1 : 0) }\n");
        }
    }
}
