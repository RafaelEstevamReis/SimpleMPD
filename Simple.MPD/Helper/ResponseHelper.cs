using System;
using System.Collections.Generic;
using Simple.MPD.Exceptions;

namespace Simple.MPD.Helper
{
    internal class ResponseHelper
    {
        internal static bool IsError(string response, out Exception ex)
        {
            ex = null;
            if (!response.StartsWith("ACK")) return false;

            ex = FailureException.FromResponseText(response);
            return true;
        }
        internal static  IEnumerable<(string Key, string Value)> ReadPairs(System.IO.StreamReader stream)
        {
            string line;
            int idx;

            while ((line = stream.ReadLine()) != "OK")
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

        internal static IEnumerable<string> ReadLines(System.IO.StreamReader stream)
        {
            string line;

            while ((line = stream.ReadLine()) != "OK")
            {
                if (IsError(line, out Exception ex))
                {
                    throw ex;
                }

                yield return line;
            }
        }
    }
}
