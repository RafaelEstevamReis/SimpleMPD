using System;

namespace Simple.MPD.Events
{
    /// <summary>
    /// Changed status event args
    /// </summary>
    public class SatusChangedEventArgs : EventArgs
    {
        /// <summary>
        /// Get subsystems
        /// </summary>
        public Commands.Idle.SubSystems[] Systems { get; private set; }
        /// <summary>
        /// Get statuses
        /// </summary>
        public Responses.Status Status { get; private set; }
        /// <summary>
        /// Gt current song
        /// </summary>
        public Responses.SongInfo CurrentSong { get; private set; }
        /// <summary>
        /// Creates a new instance
        /// </summary>
        public SatusChangedEventArgs(Commands.Idle.SubSystems[] systemsChanged, Responses.Status status, Responses.SongInfo currentSong)
        {
            Systems = systemsChanged;
            Status = status;
            CurrentSong = currentSong;
        }
    }
}
