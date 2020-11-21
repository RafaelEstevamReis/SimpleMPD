using System;

namespace RafaelEstevam.Simple.MPD.Helper
{
    public class ResponseHelper
    {
        internal static bool IsError(string response, out Exception ex)
        {
            ex = null;
            if (!response.StartsWith("ACK")) return false;

            ex = new Exceptions.NotConnectedException();
            return true;
        }
    }
}
