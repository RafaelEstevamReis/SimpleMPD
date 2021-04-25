using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Simple.MPD.Interfaces;

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
        public Task ReadAsync(StreamReader stream)
        {
            List<string> lst = new List<string>();

            foreach (var line in Helper.ResponseHelper.ReadLines(stream))
            {
                lst.Add(line);
            }

            Items = lst.ToArray();
            return Helper.FrameworkHelper.GetCompletedTask();
        }
    }
}
