using System;
using System.IO;
using System.Threading.Tasks;
using Simple.MPD.Interfaces;

namespace Simple.MPD.Responses
{
    public class Ok : IResponse
    {
        public ICommand GetCommand()
        {
            return null;
        }

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
