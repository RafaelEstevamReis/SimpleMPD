using Simple.MPD.Interfaces;
using System.IO;
using System.Threading.Tasks;

namespace Simple.MPD.Commands
{
    public  class Rm : ICommand
    {
        public string Name { get; }

        public Rm(string name)
        {
            Name = name;
        }

        public string CommandName => "Rm";

        public IResponse GetResponseProcessor()
        {
            return new Responses.Ok();
        }

        public async Task WriteAsync(StreamWriter stream)
        {
            string sName = Helper.EscapingHelper.Escape(Name);
            await stream.WriteAsync($"rm \"{sName}\"\n");
        }
    }
}
