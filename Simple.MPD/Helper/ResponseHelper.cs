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
            if (response == null)
            {
                ex = new FailureException(0, "NULL RESPONSE");
                return true;
            }
            if (!response.StartsWith("ACK")) return false;

            ex = FailureException.FromResponseText(response);
            return true;
        }
        internal static  IEnumerable<KeyValuePair<string,string>> ReadPairs(System.IO.StreamReader stream)
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
                if (idx < 0) continue;

                var key = line.Substring(0, idx).Trim();
                string value = string.Empty;
                if (idx > 0)
                    value = line.Substring(idx + 1).Trim();

                yield return new KeyValuePair<string, string>(key, value);
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
