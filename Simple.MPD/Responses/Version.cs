using System;
using System.IO;
using System.Threading.Tasks;
using Simple.MPD.Interfaces;

namespace Simple.MPD.Responses
{
    public class Version : IResponse
    {
        public System.Version VersionInfo { get; private set; }

        public ICommand GetCommand()
        {
            throw new NotImplementedException();
        }

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
