namespace Simple.MPD.Helper
{
    public class EscapingHelper
    {
        public static string Escape(string argument)
        {
            // First escape backslashes
            argument = argument.Replace("\\", "\\\\");
            // Escape quotes
            argument = argument.Replace("\"", "\\\"");
            argument = argument.Replace("'", "\\'");

            return argument;
        }
    }
}
