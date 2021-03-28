using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Simple.MPD.Interfaces;

namespace Simple.MPD.Responses
{
    public class ValuesList : IResponse
    {
        public (string Key, string Value)[] Items { get; set; }
        public string this[string Key] => Items.First(i => i.Key == Key).Value;
        public string this[int Index] => Items[Index].Value;

        public ICommand GetCommand()
        {
            throw new NotImplementedException();
        }

        public async Task ReadAsync(StreamReader stream)
        {
            List<(string Key, string Value)> lst = new List<(string Key, string Value)>();
            await foreach (var i in Helper.ResponseHelper.ReadPairsAsync(stream))
            {
                lst.Add(i);
            }
            Items = lst.ToArray();
        }
    }
}
