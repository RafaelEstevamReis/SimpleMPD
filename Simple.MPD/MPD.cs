using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Simple.MPD.Exceptions;
using Simple.MPD.Interfaces;

namespace Simple.MPD
{
    /// <summary>
    /// Music Protocol Deamon client
    /// </summary>
    public class MPD
    {
        /// <summary>
        /// Reports if is in Idle.
        /// Do NOT send commands while in Idle
        /// </summary>
        public bool IsIdle { get; private set; }

        private readonly object lockObject;
        /// <summary>
        /// Gets current connection
        /// </summary>
        public IConnection Connection { get; }
        /// <summary>
        /// Current protocol version
        /// </summary>
        public Version ProtocolVersion { get; private set; }

        /// <summary>
        /// Create a new instance
        /// </summary>
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
            if (IsIdle) throw new IdleException();
            await checkConnection();

            return await Task.Run(() => ExecuteCommandInternalLock(command));
        }
        private async Task checkConnection()
        {
            if (Connection.IsConnected) return;

            await Connection.OpenAsync();
            if (!Connection.IsConnected) throw new NotConnectedException();

            var version = new Responses.Version();
            await readResponseAsync(version);
            ProtocolVersion = version.VersionInfo;
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
        /// <summary>
        /// Closes the scoket
        /// </summary>
        public void CloseConnection()
        {
            // Clients should not use this command; 
            //  instead, they should just close the socket.
            Connection.Close();
        }
        /// <summary>
        /// Executes a Ping command
        /// </summary>
        public async Task DoPingAsync()
        {
            // is either OK or Exception
            await ExecuteCommandAsync(new Commands.Ping());
        }

        /* REFLECTION */
        /// <summary>
        /// Dumps configuration values that may be interesting for the client. 
        /// This command is only permitted to “local” clients (connected via local socket).
        /// </summary>
        public async Task<Responses.ValuesList> GetConfigAsync()
        {
            var resp = await ExecuteCommandAsync(new Commands.Config());
            return (Responses.ValuesList)resp;
        }
        /// <summary>
        /// Shows which commands the current user has access to
        /// </summary>
        public async Task<Responses.ValuesList> GetCommandsAsync()
        {
            var resp = await ExecuteCommandAsync(new Commands.Commands());
            return (Responses.ValuesList)resp;
        }
        /// <summary>
        /// Shows which commands the current user does not have access to
        /// </summary>
        public async Task<Responses.ValuesList> GetNotCommandsAsync()
        {
            var resp = await ExecuteCommandAsync(new Commands.NotCommands());
            return (Responses.ValuesList)resp;
        }
        /// <summary>
        /// Gets a list of available URL handlers
        /// </summary>
        public async Task<Responses.ValuesList> GetUrlHandlersAsync()
        {
            var resp = await ExecuteCommandAsync(new Commands.UrlHandlers());
            return (Responses.ValuesList)resp;
        }
        /// <summary>
        /// Print a list of decoder plugins, followed by their supported suffixes and MIME types
        /// </summary>
        public async Task<Responses.ValuesList> GetDecodersAsync()
        {
            var resp = await ExecuteCommandAsync(new Commands.Decoders());
            return (Responses.ValuesList)resp;
        }

        /* QUERYING MPD’S STATUS */
        /// <summary>
        /// Displays statistics
        /// </summary>
        public async Task<Responses.Stats> GetStatsAsync()
        {
            var resp = await ExecuteCommandAsync(new Commands.Stats());
            return (Responses.Stats)resp;
        }
        /// <summary>
        /// Reports the current status of the player and the volume level
        /// </summary>
        public async Task<Responses.Status> GetStatusAsync()
        {
            var resp = await ExecuteCommandAsync(new Commands.Status());
            return (Responses.Status)resp;
        }
        /// <summary>
        /// Displays the song info of the current song
        /// </summary>
        public async Task<Responses.SongInfo> GetCurrentSongAsync()
        {
            var resp = await ExecuteCommandAsync(new Commands.CurrentSong());
            return (Responses.SongInfo)resp;
        }
        /// <summary>
        /// Waits until there is a noteworthy change in one or more of MPD’s subsystems. 
        /// As soon as there is one, it lists all changed systems in a line in the format `changed: SUBSYSTEM`
        /// Any command sent while in Idle wil raise an IdleException
        /// </summary>
        public async Task<Commands.Idle.SubSystems[]> Idle(CancellationToken token)
        {
            await checkConnection();

            if (IsIdle) throw new IdleException();
            IsIdle = true;

            var command = new Commands.Idle();
            // Send IDLE
            await command.WriteAsync(Connection.GetWriter());
            // Wait either response or CANCEL
            var taskResponse = readResponseAsync(command.GetResponseProcessor());

            while (true)
            {
                if (taskResponse.IsCompleted) break;
                if (token.IsCancellationRequested) break;
                await Task.Delay(50, token);
            }
            // was cancelled ?
            if (!taskResponse.IsCompleted)
            {
                await Connection.GetWriter().WriteAsync($"noidle\n");
            }

            IsIdle = false;

            var response = await taskResponse;
            return ((Responses.IdleResponse)response).SubSystems;
        }

        /* PLAYBACK OPTIONS */
        /// <summary>
        /// Sets consume state. When consume is activated, each song played is removed from playlist
        /// </summary>
        public async Task SetConsumeAsync(bool ConsumeSong)
        {
            // is either OK or Exception
            await ExecuteCommandAsync(new Commands.Consume(ConsumeSong));
        }
        /// <summary>
        /// Sets crossfading between songs
        /// </summary>
        public async Task SetCrossfadeAsync(int CrossfadeSeconds)
        {
            // is either OK or Exception
            await ExecuteCommandAsync(new Commands.Crossfade(CrossfadeSeconds));
        }
        /// <summary>
        /// Enables/disables Random
        /// </summary>
        public async Task SetRandomAsync(bool Random)
        {
            // is either OK or Exception
            await ExecuteCommandAsync(new Commands.Random(Random));
        }
        /// <summary>
        /// Enable/Disable repeat
        /// </summary>
        public async Task SetRepeatAsync(bool Repeat)
        {
            // is either OK or Exception
            await ExecuteCommandAsync(new Commands.Repeat(Repeat));
        }
        /// <summary>
        /// Sets single state, STATE should be On, Off or OneShot. 
        /// When single is activated, playback is stopped after current song, or song is repeated if the ‘repeat’ mode is enabled
        /// </summary>
        public async Task SetSingle(Commands.Single.States state)
        {
            // is either OK or Exception
            await ExecuteCommandAsync(new Commands.Single(state));
        }

        /// <summary>
        /// Sets volume, the range of volume is 0-100. Out of range values will be truncated
        /// </summary>
        public async Task SetVolumeAsync(int Volume)
        {
            int safeVol = Volume;
            if (safeVol < 0) safeVol = 0;
            if (safeVol > 100) safeVol = 100;
            // is either OK or Exception
            await ExecuteCommandAsync(new Commands.SetVol(safeVol));
        }

        /* CONTROLLING PLAYBACK */
        /// <summary>
        /// Begins playing the playlist at current song
        /// </summary>
        public async Task PlayAsync()
        {
            await ExecuteCommandAsync(new Commands.Play());
        }
        /// <summary>
        /// Begins playing the playlist at song number SongPosition
        /// </summary>
        public async Task PlaySongPosAsync(int SongPosition)
        {
            await ExecuteCommandAsync(Commands.Play.PlaySongPosition(SongPosition));
        }
        /// <summary>
        /// Begins playing the playlist at song SongId
        /// </summary>
        public async Task PlaySongIdAsync(int SongId)
        {
            await ExecuteCommandAsync(Commands.Play.PlaySongId(SongId));
        }
        /// <summary>
        /// Pause or resume playback
        /// </summary>
        public async Task PauseAsync(Commands.Pause.State state)
        {
            // is either OK or Exception
            await ExecuteCommandAsync(new Commands.Pause(state));
        }
        /// <summary>
        /// Plays next song in the playlist
        /// </summary>
        public async Task NextAsync()
        {
            // is either OK or Exception
            await ExecuteCommandAsync(new Commands.Next());
        }
        /// <summary>
        /// Plays previous song in the playlist
        /// </summary>
        public async Task PreviousAsync()
        {
            // is either OK or Exception
            await ExecuteCommandAsync(new Commands.Previous());
        }
        /// <summary>
        /// Stops playing
        /// </summary>
        public async Task StopAsync()
        {
            // is either OK or Exception
            await ExecuteCommandAsync(new Commands.Stop());
        }

        /* QUEUE */
        /// <summary>
        /// Displays a list of all songs in the queue, internally uses the `playlistinfo` command
        /// </summary>
        public async Task<Responses.SongInfoCollection> GetQueue()
        {
            var resp = await ExecuteCommandAsync(new Commands.PlayListInfo());
            return (Responses.SongInfoCollection)resp;
        }
        /// <summary>
        /// Clears the queue
        /// </summary>
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
        /// <summary>
        /// Deletes a song from the playlist
        /// </summary>
        public async Task QueueDelete(string Uri)
        {
            await ExecuteCommandAsync(new Commands.Delete(Uri));
        }
        /// <summary>
        /// Deletes the song SONGID from the playlist
        /// </summary>
        public async Task<int> QueueDeleteId(string Uri)
        {
            var resp = await ExecuteCommandAsync(new Commands.DeleteId(Uri));
            var list = (Responses.ValuesList)resp;
            return int.Parse(list["Id"]);
        }
        /// <summary>
        /// Shuffles the queue
        /// </summary>
        public async Task Shuffle()
        {
            // is either OK or Exception
            await ExecuteCommandAsync(new Commands.Shuffle());
        }

        /* PLAYLIST */
        /// <summary>
        /// Prints a list of the playlist directory
        /// </summary>
        public async Task<Responses.SongInfoCollection> ListPlaylists()
        {
            var resp = await ExecuteCommandAsync(new Commands.ListPlaylists());
            return (Responses.SongInfoCollection)resp;
        }
        /// <summary>
        /// Lists the songs with metadata in the playlist
        /// </summary>
        public async Task<Responses.SongInfoCollection> ListPlaylistInfo(string listName)
        {
            var resp = await ExecuteCommandAsync(new Commands.ListPlaylistInfo(listName));
            return (Responses.SongInfoCollection)resp;
        }
        /// <summary>
        /// Renames the playlist
        /// </summary>
        public async Task<Responses.Ok> RenamePlaylist(string oldName, string newName)
        {
            var resp = await ExecuteCommandAsync(new Commands.Rename(oldName, newName));
            return (Responses.Ok)resp;
        }
        /// <summary>
        /// Removes the playlist
        /// </summary>
        public async Task<Responses.Ok> RemovePlaylist(string listName)
        {
            var resp = await ExecuteCommandAsync(new Commands.Rm(listName));
            return (Responses.Ok)resp;
        }
        /// <summary>
        /// Loads the playlist (or a range) into the current queue
        /// </summary>
        public async Task<Responses.Ok> Load(string listName, Commands.Range range = null)
        {
            var resp = await ExecuteCommandAsync(new Commands.Load(listName, range));
            return (Responses.Ok)resp;
        }
        /// <summary>
        /// Saves the queue as a playlist
        /// </summary>
        public async Task<Responses.Ok> SaveQueue(string listName)
        {
            var resp = await ExecuteCommandAsync(new Commands.Save(listName));
            return (Responses.Ok)resp;
        }
        /// <summary>
        /// Deletes SONGPOS from the playlist
        /// </summary>
        public async Task<Responses.Ok> PlaylistDelete(string listName, int SongPos)
        {
            var resp = await ExecuteCommandAsync(new Commands.PlaylistDelete(listName, SongPos));
            return (Responses.Ok)resp;
        }
        /// <summary>
        /// Moves the song at position FROM to the position TO
        /// </summary>
        public async Task<Responses.Ok> PlaylistMove(string listName, int from, int to)
        {
            var resp = await ExecuteCommandAsync(new Commands.PlaylistMove(listName, from, to));
            return (Responses.Ok)resp;
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
        /// <summary>
        /// Lists the contents of the directory URI
        /// </summary>
        public async Task<Responses.SongInfoCollection> LsInfo(string Url = null)
        {
            var resp = await ExecuteCommandAsync(new Commands.LsInfo(Url));
            return (Responses.SongInfoCollection)resp;
        }
        /// <summary>
        /// Search the database for songs matching expression (case-sensitive)
        /// </summary>
        public async Task<Responses.SongInfoCollection> Find(string expression)
        {
            var resp = await ExecuteCommandAsync(new Commands.Find(expression));
            return (Responses.SongInfoCollection)resp;
        }
        /// <summary>
        /// Search the database for songs matching expression (case-sensitive)
        /// </summary>
        public async Task<Responses.SongInfoCollection> Find(Tags tag, FilterMatch match, string search)
        {
            string exp = Commands.Find.ExpressionBuilder(tag, match, search);
            var resp = await ExecuteCommandAsync(new Commands.Find(exp));
            return (Responses.SongInfoCollection)resp;
        }
        /// <summary>
        /// Search the database for songs matching expression (case-INsensitive)
        /// </summary>
        public async Task<Responses.SongInfoCollection> Search(string expression)
        {
            var resp = await ExecuteCommandAsync(new Commands.Search(expression));
            return (Responses.SongInfoCollection)resp;
        }
        /// <summary>
        /// Search the database for songs matching expression (case-INsensitive)
        /// </summary>
        public async Task<Responses.SongInfoCollection> Search(Tags tag, FilterMatch match, string search)
        {
            string exp = Commands.Find.ExpressionBuilder(tag, match, search);
            var resp = await ExecuteCommandAsync(new Commands.Search(exp));
            return (Responses.SongInfoCollection)resp;
        }

        /* External */
        /// <summary>
        /// Rerads a Local m3u file with MPD music locations and adds to queue
        /// </summary>
        /// <param name="LocalFilePath">Local file, NOT MPD path</param>
        /// <returns>Tup´le with MPD file added and it's SongID</returns>
        public IEnumerable<(string,int)> AddLocalM3uFile(string LocalFilePath, Func<string,string> pathConverter = null)
        {
            foreach (var line in File.ReadAllLines(LocalFilePath))
            {
                if (line.StartsWith("#")) continue;
                int id;
                try
                {
                    string path = line;
                    if (pathConverter != null) path = pathConverter(line);

                    id = QueueAddId(path).Result;
                }
                catch (Exception)
                {
                    id = -1;
                    Connection.Close(); // must re-open
                }

                yield return (line, id);
            }
        }

        // https://www.musicpd.org/doc/html/protocol.html
    }
}
