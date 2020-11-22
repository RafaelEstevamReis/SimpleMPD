using System;
using RafaelEstevam.Simple.MPD.Exceptions;

namespace RafaelEstevam.Simple.MPD.Helper
{
    public class ResponseHelper
    {
        internal static bool IsError(string response, out Exception ex)
        {
            ex = null;
            if (!response.StartsWith("ACK")) return false;

            ex = FailureException.FromResponseText(response);
            return true;
        }
    }
}
