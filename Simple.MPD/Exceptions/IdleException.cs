using System;

namespace Simple.MPD.Exceptions
{
    /// <summary>
    /// Idle exception class
    /// </summary>
    public class IdleException : Exception
    {
        /// <summary>
        /// Creates a new instance
        /// </summary>
        public IdleException() 
            : base("The connection is Idle and will not accept commands")
        {
        }
    }
}