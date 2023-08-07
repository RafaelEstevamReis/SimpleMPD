using Simple.MPD.Interfaces;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Simple.MPD.Responses
{
    /// <summary>
    /// String collection response
    /// </summary>
    public class StringArray : IResponse
    {
        /// <summary>
        /// Get response items
        /// </summary>
        public string[] Items { get; set; }
        /// <summary>
        /// Read response from stream
        /// </summary>
        public async Task ReadAsync(StreamReader stream)
        {
            await Task.Run(() =>
            {
                List<string> lst = new List<string>();

                foreach (var line in Helper.ResponseHelper.ReadLines(stream))
                {
                    lst.Add(line);
                }

                Items = lst.ToArray();
            });
        }
    }
}
