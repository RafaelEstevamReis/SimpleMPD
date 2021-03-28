using System;

namespace Simple.MPD.Events
{
    public class SatusChangedEventArgs : EventArgs
    {
        public Commands.Idle.SubSystems[] Systems { get; private set; }
        public Responses.Status Status { get; private set; }
        public Responses.SongInfo CurrentSong { get; private set; }

        public SatusChangedEventArgs(Commands.Idle.SubSystems[] systemsChanged, Responses.Status status, Responses.SongInfo currentSong)
        {
            Systems = systemsChanged;
            Status = status;
            CurrentSong = currentSong;
        }
    }
}
