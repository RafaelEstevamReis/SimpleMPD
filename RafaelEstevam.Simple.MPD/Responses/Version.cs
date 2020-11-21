using System;
using System.IO;
using System.Threading.Tasks;
using RafaelEstevam.Simple.MPD.Interfaces;

namespace RafaelEstevam.Simple.MPD.Responses
{
    public class Version : IResponse
    {
        public Version VersionInfo { get; private set; }

        public ICommand GetCommand()
        {
            throw new NotImplementedException();
        }

        public async Task ReadAsync(Stream stream)
        {
            StreamReader sr = new StreamReader(stream);
            var response = await sr.ReadLineAsync();

            if (response.StartsWith("ACK")) Helper.ResponseHelper.ProcessError(this, response);
        }
    }
}
