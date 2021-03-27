using System;

namespace Simple.MPD
{
    internal class IdleException : Exception
    {
        public IdleException() 
            : base("The connection is Idle and will not accept commands")
        {
        }
    }
}