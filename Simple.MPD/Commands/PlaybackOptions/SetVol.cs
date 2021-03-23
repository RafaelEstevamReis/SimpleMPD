using System.IO;
using System.Threading.Tasks;
using Simple.MPD.Interfaces;

namespace Simple.MPD.Commands
{
    public class SetVol : ICommand
    {
        public int Volume { get; set; }

        public SetVol(int volume)
        {
            if (volume < 0) throw new System.ArgumentException("Volume should be greater than zero");
            if (volume > 100) throw new System.ArgumentException("Volume should be smaller or equal to 100");

            Volume = volume;
        }

        public string CommandName => "SetVol";

        public IResponse GetResponseProcessor()
        {
            return new Responses.Ok();
        }

        public async Task WriteAsync(StreamWriter stream)
        {
            await stream.WriteAsync($"setvol { Volume }\n");
        }
    }
}
