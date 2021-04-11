using Simple.MPD.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Simple.MPD.Commands
{
    public class Load : ICommand
    {
        public string Name { get; }
        public Range Range { get; }

        public Load(string name, Range range = null)
        {
            Name = name;
            Range = range;
        }

        public string CommandName => "Load";

        public IResponse GetResponseProcessor()
        {
            return new Responses.Ok();
        }

        public async Task WriteAsync(StreamWriter stream)
        {
            string sName = Helper.EscapingHelper.Escape(Name);
            string sRange = Range?.ToString() ?? "" ;
            await stream.WriteAsync($"load \"{sName}\" \"{sRange}\"\n");
        }
    }
}
