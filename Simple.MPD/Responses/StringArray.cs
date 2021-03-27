using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Simple.MPD.Interfaces;

namespace Simple.MPD.Responses
{
    public class StringArray : IResponse
    {
        public string[] Items { get; set; }

        public ICommand GetCommand()
        {
            throw new NotImplementedException();
        }

        public async Task ReadAsync(StreamReader stream)
        {
            List<string> lst = new List<string>();
            string line;

            while ((line = await stream.ReadLineAsync()) != "OK")
            {
                lst.Add(line);
            }
            Items = lst.ToArray();
        }
    }
}
