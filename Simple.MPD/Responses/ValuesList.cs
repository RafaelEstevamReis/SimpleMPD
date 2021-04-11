using Simple.MPD.Interfaces;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Simple.MPD.Responses
{
    /// <summary>
    /// OK response
    /// </summary>
    public class ValuesList : IResponse
    {
        /// <summary>
        /// Get response items pairs
        /// </summary>
        public (string Key, string Value)[] Items { get; set; }
        /// <summary>
        /// Get response items
        /// </summary>
        public string this[string Key] => Items.First(i => i.Key == Key).Value;
        /// <summary>
        /// Get response items
        /// </summary>
        public string this[int Index] => Items[Index].Value;

        /// <summary>
        /// Read response from stream
        /// </summary>
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
