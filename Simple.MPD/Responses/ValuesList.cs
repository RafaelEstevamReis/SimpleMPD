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
        public  KeyValuePair<string, string>[] Items { get; set; }
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
            await Task.Run(() =>
            {
                var lst = new List<KeyValuePair<string, string>>();
                foreach (var i in Helper.ResponseHelper.ReadPairs(stream))
                {
                    lst.Add(i);
                }
                Items = lst.ToArray();
            });
        }
    }
}
