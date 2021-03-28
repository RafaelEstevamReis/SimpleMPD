using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Simple.MPD.Interfaces;

namespace Simple.MPD
{
    /// <summary>
    /// Creates a MPD-Client connection and notifies events
    /// </summary>
    public class MpdNotifier : IDisposable
    {
        private readonly MPD mpd;
        private CancellationTokenSource cancelSource;
        private Commands.Idle.SubSystems[] statusNotify;

        /// <summary>
        /// Notify on SubSystems change
        /// </summary>
        public event EventHandler<Events.NotifyEventArgs> NotifyEvent;
        /// <summary>
        /// Notify on Player, Mixer or Options
        /// </summary>
        public event EventHandler<Events.SatusChangedEventArgs> NotifyStatusChange;

        /// <summary>
        /// Creates a new instance
        /// </summary>
        public MpdNotifier(IConnection connection)
        {
            mpd = new MPD(connection);

            statusNotify = new Commands.Idle.SubSystems[]
            {
                Commands.Idle.SubSystems.Player,
                Commands.Idle.SubSystems.Mixer,
                Commands.Idle.SubSystems.Options
            };
        }
        /// <summary>
        /// Starts notifier
        /// </summary>
        public void Start()
        {
            cancelSource = new CancellationTokenSource();
            // Do not wait
            doLoop();
        }

        // Fire and forget (cancel)
        private async void doLoop()
        {
            while (!cancelSource.IsCancellationRequested)
            {
                var systems = await mpd.Idle(cancelSource.Token);

                if (systems.Length > 0)
                {
                    await doNotifyAsync(systems);
                }
            }
        }

        private async Task doNotifyAsync(Commands.Idle.SubSystems[] systems)
        {
            if (NotifyEvent != null)
            {
                NotifyEvent.Invoke(this, new Events.NotifyEventArgs(systems));
            }

            if (NotifyStatusChange != null)
            {
                var intersection = statusNotify.Intersect(systems).ToArray();
                if (intersection.Length == 0) return;
                
                var status = await mpd.GetStatusAsync();
                var songInfo = await mpd.GetCurrentSongAsync();

                NotifyStatusChange(this, new Events.SatusChangedEventArgs(intersection, status, songInfo));
            }
        }

        /// <summary>
        /// Stops notifier
        /// </summary>
        public void Stop()
        {
            cancelSource?.Cancel();
        }
        /// <summary>
        /// Disposes object
        /// </summary>
        public void Dispose()
        {
            Stop();
            while (mpd.IsIdle) Task.Delay(1).Wait();
            mpd.CloseConnection();
        }
    }
}
