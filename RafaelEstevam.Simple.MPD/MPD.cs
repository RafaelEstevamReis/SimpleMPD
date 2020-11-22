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
                await readResponseAsync(version);
                ProtocolVersion = version.VersionInfo;
            }

            await command.WriteAsync(Connection.GetWriter());

            return await readResponseAsync(command.GetResponseProcessor());
        }
        private async Task<T> readResponseAsync<T>(T response) where T : IResponse
        {
            await response.ReadAsync(Connection.GetReader());
            return response;
        }

        /* ACTIONS */
        public async Task DoPingAsync()
        {
            // is either OK or Exception
            await ExecuteCommandAsync(new Commands.Ping());
        }
        public async Task<Responses.Stats> GetStatsAsync()
        {
            var rest = await ExecuteCommandAsync(new Commands.Stats());
            return (Responses.Stats)rest;
        }
        public async Task<Responses.Status> GetStatusAsync()
        {
            var rest = await ExecuteCommandAsync(new Commands.Status());
            return (Responses.Status)rest;
        }
        public async Task<Responses.CurrentSong> GetCUrrentSongAsync()
        {
            var rest = await ExecuteCommandAsync(new Commands.CurrentSong());
            return (Responses.CurrentSong)rest;
        }
    }
}
