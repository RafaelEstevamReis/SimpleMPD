using System.IO;
using System.Threading.Tasks;
using Simple.MPD.Interfaces;

namespace Simple.MPD.Commands
{
    public class Repeat : ICommand
    {
        public bool SetRepeat { get; set; }

        public Repeat(bool repeat)
        {
            SetRepeat = repeat;
        }

        public string CommandName => "Repeat";

        public IResponse GetResponseProcessor()
        {
            return new Responses.Ok();
        }

        public async Task WriteAsync(StreamWriter stream)
        {
            await stream.WriteAsync($"random { (SetRepeat ? 1 : 0) }\n");
        }
    }
}
