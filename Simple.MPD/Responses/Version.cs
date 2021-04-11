using System;
using System.IO;
using System.Threading.Tasks;
using Simple.MPD.Interfaces;

namespace Simple.MPD.Responses
{
    /// <summary>
    /// Version response
    /// </summary>
    public class Version : IResponse
    {
        /// <summary>
        /// Protocol version info
        /// </summary>
        public System.Version VersionInfo { get; private set; }
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
            
            VersionInfo = new System.Version(response.Split(' ')[2]);
        }
    }
}
