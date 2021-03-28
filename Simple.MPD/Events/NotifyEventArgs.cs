using System;

namespace Simple.MPD.Events
{
    public class NotifyEventArgs : EventArgs
    {
        public Commands.Idle.SubSystems[] SystemsChanged { get; private set; }

        public NotifyEventArgs(Commands.Idle.SubSystems[] systemsChanged)
        {
            SystemsChanged = systemsChanged;
        }
    }
}
