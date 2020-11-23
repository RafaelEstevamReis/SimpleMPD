using System.IO;
using System.Threading.Tasks;
using RafaelEstevam.Simple.MPD.Interfaces;

namespace RafaelEstevam.Simple.MPD.Commands
{
    public  class Consume : ICommand
    {
        public bool ConsumeSong { get; set; }

        public Consume(bool consumeSong)
        {
            ConsumeSong = consumeSong;
        }

        public string CommandName => "Consume";

        public IResponse GetResponseProcessor()
        {
            return new Responses.Ok();
        }

        public async Task WriteAsync(StreamWriter stream)
        {
            await stream.WriteAsync($"consume { (ConsumeSong ? 1: 0) }\n");
        }
    }
}
