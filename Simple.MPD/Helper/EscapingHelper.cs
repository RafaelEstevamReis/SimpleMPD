namespace Simple.MPD.Helper
{
    /// <summary>
    /// Helper for scaping texts
    /// </summary>
    public static class EscapingHelper
    {
        /// <summary>
        /// Scapes a text
        /// </summary>
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
