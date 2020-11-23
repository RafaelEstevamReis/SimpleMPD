using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using RafaelEstevam.Simple.MPD.Interfaces;

namespace RafaelEstevam.Simple.MPD.Responses
{
    public class ValuesList : IResponse
    {
        public (string Key, string Value)[] Items { get; set; }

        public ICommand GetCommand()
        {
            throw new NotImplementedException();
        }

        public async Task ReadAsync(StreamReader stream)
        {
            List<(string Key, string Value)> lst = new List<(string Key, string Value)>();
            await foreach (var i in Helper.ResponseHelper.ReadValuesAsync(stream))
            {
                lst.Add(i);
            }
            Items = lst.ToArray();
        }
    }
}
