using System;
using System.IO;
using System.Threading.Tasks;
using Simple.MPD.Interfaces;

namespace Simple.MPD.Responses
{
    /// <summary>
    /// OK response
    /// </summary>
    public class Ok : IResponse
    {
        /// <summary>
        /// Read response from stream
        /// </summary>
        public async Task ReadAsync(StreamReader stream)
        {
            var response = await stream.ReadLineAsync();

            if (Helper.ResponseHelper.IsError(response, out Exception ex))
            {
                throw ex;
            }
        }
    }
}
