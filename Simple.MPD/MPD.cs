using System.Threading.Tasks;
using Simple.MPD.Exceptions;
using Simple.MPD.Interfaces;

namespace Simple.MPD
{
    public class MPD
    {
        private readonly object lockObject;

        public IConnection Connection { get; }
        public System.Version ProtocolVersion { get; private set; }

        public MPD(IConnection connection)
        {
            lockObject = new object();
            Connection = connection;
        }
        /// <summary>
        /// Executes a command in a concurrent-safe manmer
        /// </summary>
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

            return await Task.Run(() => ExecuteCommandInternalLock(command));
        }
        private IResponse ExecuteCommandInternalLock(ICommand command)
        {
            lock (lockObject)
            {
                command.WriteAsync(Connection.GetWriter()).Wait();
                return readResponseAsync(command.GetResponseProcessor()).Result;
            }
        }
    
        private async Task<T> readResponseAsync<T>(T response) where T : IResponse
        {
            await response.ReadAsync(Connection.GetReader());
            return response;
        }

        /* CONNECTION SETTINGS */
        public void CloseConnection()
        {
            // Clients should not use this command; 
            //  instead, they should just close the socket.
            Connection.Close();
        }
        public async Task DoPingAsync()
        {
            // is either OK or Exception
            await ExecuteCommandAsync(new Commands.Ping());
        }

        /* QUERYING MPD’S STATUS */
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
        public async Task<Responses.CurrentSong> GetCurrentSongAsync()
        {
            var rest = await ExecuteCommandAsync(new Commands.CurrentSong());
            return (Responses.CurrentSong)rest;
        }

        /* REFLECTION */
        public async Task<Responses.ValuesList> GetConfigAsync()
        {
            var rest = await ExecuteCommandAsync(new Commands.Config());
            return (Responses.ValuesList)rest;
        }
        public async Task<Responses.ValuesList> GetCommandsAsync()
        {
            var rest = await ExecuteCommandAsync(new Commands.Commands());
            return (Responses.ValuesList)rest;
        }
        public async Task<Responses.ValuesList> GetNotCommandsAsync()
        {
            var rest = await ExecuteCommandAsync(new Commands.NotCommands());
            return (Responses.ValuesList)rest;
        }
        public async Task<Responses.ValuesList> GetUrlHandlersAsync()
        {
            var rest = await ExecuteCommandAsync(new Commands.UrlHandlers());
            return (Responses.ValuesList)rest;
        }
        public async Task<Responses.ValuesList> GetDecodersAsync()
        {
            var rest = await ExecuteCommandAsync(new Commands.Decoders());
            return (Responses.ValuesList)rest;
        }

        /* PLAYBACK OPTIONS */
        public async Task SetConsumeAsync(bool ConsumeSong)
        {
            // is either OK or Exception
            await ExecuteCommandAsync(new Commands.Consume(ConsumeSong));
        }
        public async Task SetCrossfadeAsync(int CrossfadeSeconds)
        {
            // is either OK or Exception
            await ExecuteCommandAsync(new Commands.Crossfade(CrossfadeSeconds));
        }
        public async Task SetRandomAsync(bool Random)
        {
            // is either OK or Exception
            await ExecuteCommandAsync(new Commands.Random(Random));
        }
        public async Task SetRepeatAsync(bool Repeat)
        {
            // is either OK or Exception
            await ExecuteCommandAsync(new Commands.Repeat(Repeat));
        }
        public async Task SetVolumeAsync(int Volume)
        {
            // is either OK or Exception
            await ExecuteCommandAsync(new Commands.SetVol(Volume));
        }

        /* CONTROLLING PLAYBACK */
        public async Task PauseAsync(Commands.Pause.State state)
        {
            // is either OK or Exception
            await ExecuteCommandAsync(new Commands.Pause(state));
        }
        public async Task NextAsync()
        {
            // is either OK or Exception
            await ExecuteCommandAsync(new Commands.Next());
        }
        public async Task PreviousAsync()
        {
            // is either OK or Exception
            await ExecuteCommandAsync(new Commands.Previous());
        }
        public async Task StopAsync()
        {
            // is either OK or Exception
            await ExecuteCommandAsync(new Commands.Stop());
        }

    }
}
