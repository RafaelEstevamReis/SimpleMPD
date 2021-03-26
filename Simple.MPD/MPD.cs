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

        /* REFLECTION */
        public async Task<Responses.ValuesList> GetConfigAsync()
        {
            var resp = await ExecuteCommandAsync(new Commands.Config());
            return (Responses.ValuesList)resp;
        }
        public async Task<Responses.ValuesList> GetCommandsAsync()
        {
            var resp = await ExecuteCommandAsync(new Commands.Commands());
            return (Responses.ValuesList)resp;
        }
        public async Task<Responses.ValuesList> GetNotCommandsAsync()
        {
            var resp = await ExecuteCommandAsync(new Commands.NotCommands());
            return (Responses.ValuesList)resp;
        }
        public async Task<Responses.ValuesList> GetUrlHandlersAsync()
        {
            var resp = await ExecuteCommandAsync(new Commands.UrlHandlers());
            return (Responses.ValuesList)resp;
        }
        public async Task<Responses.ValuesList> GetDecodersAsync()
        {
            var resp = await ExecuteCommandAsync(new Commands.Decoders());
            return (Responses.ValuesList)resp;
        }

        /* QUERYING MPD’S STATUS */
        public async Task<Responses.Stats> GetStatsAsync()
        {
            var resp = await ExecuteCommandAsync(new Commands.Stats());
            return (Responses.Stats)resp;
        }
        public async Task<Responses.Status> GetStatusAsync()
        {
            var resp = await ExecuteCommandAsync(new Commands.Status());
            return (Responses.Status)resp;
        }
        public async Task<Responses.SongInfo> GetCurrentSongAsync()
        {
            var resp = await ExecuteCommandAsync(new Commands.CurrentSong());
            return (Responses.SongInfo)resp;
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
        public async Task PlayAsync()
        {
            await ExecuteCommandAsync(new Commands.Play());
        }
        public async Task PlaySongPosAsync(int SongPosition)
        {
            await ExecuteCommandAsync(Commands.Play.PlaySongPosition(SongPosition));
        }
        public async Task PlaySongIdAsync(int SongId)
        {
            await ExecuteCommandAsync(Commands.Play.PlaySongId(SongId));
        }
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

        /* QUEUE */
        public async Task<Responses.SongInfoCollection> GetQueue()
        {
            var resp = await ExecuteCommandAsync(new Commands.PlayListInfo());
            return (Responses.SongInfoCollection)resp;
        }
        public async Task QueueClear()
        {
            // is either OK or Exception
            await ExecuteCommandAsync(new Commands.Clear());
        }
        /// <summary>
        /// Adds the file URI to the playlist (directories add recursively). URI can also be a single file.
        /// Clients that are connected via local socket may add arbitrary local files(URI is an absolute path)
        /// </summary>
        public async Task QueueAdd(string Uri)
        {
            await ExecuteCommandAsync(new Commands.Add(Uri));
        }
        /// <summary>
        /// Adds a song to the playlist (non-recursive) and returns the song id. URI is always a single file or URL
        /// </summary>
        public async Task<int> QueueAddId(string Uri, int Position = -1)
        {
            var resp = await ExecuteCommandAsync(new Commands.AddId(Uri, Position));
            var list = (Responses.ValuesList)resp;
            return int.Parse(list["Id"]);
        }

        /* MUSIC DATABASE */
        /// <summary>
        /// Do not use this command.
        /// Lists all songs and directories in URI.
        /// Do not manage a client-side copy of MPD’s database. 
        /// That is fragile and adds huge overhead. It will break with large databases. 
        /// Instead, query MPD whenever you need something.
        /// </summary>
        /// <returns></returns>
        public async Task<Responses.SongInfoCollection> ListAll()
        {
            var resp = await ExecuteCommandAsync(new Commands.ListAll());
            return (Responses.SongInfoCollection)resp;
        }
        public async Task<Responses.SongInfoCollection> LsInfo(string Url = null)
        {
            var resp = await ExecuteCommandAsync(new Commands.LsInfo(Url));
            return (Responses.SongInfoCollection)resp;
        }
    }
}
