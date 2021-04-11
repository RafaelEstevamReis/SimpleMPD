using System;

namespace Simple.MPD.Events
{
    /// <summary>
    /// Notification event
    /// </summary>
    public class NotifyEventArgs : EventArgs
    {
        /// <summary>
        /// Gets changed subsystems
        /// </summary>
        public Commands.Idle.SubSystems[] SystemsChanged { get; private set; }
        /// <summary>
        /// Creates a new instance
        /// </summary>
        public NotifyEventArgs(Commands.Idle.SubSystems[] systemsChanged)
        {
            SystemsChanged = systemsChanged;
        }
    }
}
