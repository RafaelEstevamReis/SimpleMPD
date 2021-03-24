using System;
using System.Collections.Generic;
using Simple.MPD.Exceptions;

namespace Simple.MPD.Helper
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
        internal static async IAsyncEnumerable<(string Key, string Value)> ReadValuesAsync(System.IO.StreamReader stream)
        {
            string line;
            int idx;

            while ((line = await stream.ReadLineAsync()) != "OK")
            {
                if (IsError(line, out Exception ex))
                {
                    throw ex;
                }

                idx = line.IndexOf(':');
                var key = line[..idx].Trim();
                string value = string.Empty;
                if (idx > 0)
                    value = line[(idx + 1)..].Trim();

                yield return (key, value);
            }
        }
    }
}
