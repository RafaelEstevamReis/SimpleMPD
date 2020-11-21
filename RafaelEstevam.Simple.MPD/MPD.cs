using System.Threading.Tasks;
using RafaelEstevam.Simple.MPD.Exceptions;
using RafaelEstevam.Simple.MPD.Interfaces;

namespace RafaelEstevam.Simple.MPD
{
    public class MPD
    {
        public IConnection Connection { get; }
        public System.Version ProtocolVersion { get; private set; }

        public MPD(IConnection connection)
        {
            Connection = connection;
        }

        public async Task<IResponse> ExecuteCommandAsync(ICommand command)
        {
            if (!Connection.IsConnected)
            {
                await Connection.OpenAsync();
                if (!Connection.IsConnected) throw new NotConnectedException();
                var version = new Responses.Version();
                await version.ReadAsync(Connection.GetStream());

                ProtocolVersion = version.VersionInfo;
            }

            await command.WriteAsync(Connection.GetStream());

            return await readResponseAsync(command.GetResponseProcessor());
        }
        private async Task<IResponse> readResponseAsync(IResponse response)
        {
            await response.ReadAsync(Connection.GetStream());

            return response;
        }

        /* ACTIONS */
        public async Task DoPingAsync()
        {
            // is either OK or Exception
            await ExecuteCommandAsync(new Commands.Ping());
        }

    }
}
