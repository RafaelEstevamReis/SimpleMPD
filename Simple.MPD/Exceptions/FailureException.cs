using System;

namespace Simple.MPD.Exceptions
{
    /// <summary>
    /// MPD failure error
    /// </summary>
    public class FailureException : Exception
    {
        /// <summary>
        /// MPD error code
        /// </summary>
        public int Error { get; set; }
        /// <summary>
        /// Command list num
        /// </summary>
        public int CommandListNum { get; set; }
        /// <summary>
        /// Current command
        /// </summary>
        public string CurrentCommand { get; set; }
        /// <summary>
        /// Creates a new instance
        /// </summary>
        public FailureException()
            : base()
        { }
        /// <summary>
        /// Creates a new instance
        /// </summary>
        public FailureException(int Error, string Message)
            : base(Message)
        {
            this.Error = Error;
        }
        /// <summary>
        /// Creates a new exception from text
        /// </summary>
        public static FailureException FromResponseText(string Text)
        {
            // ACK [error@command_listNum] {current_command} message_text
            // ACK [2@0] {ping} wrong number of arguments for "ping"
            int idx1 = Text.IndexOf(']');
            string[] error = Text.Substring(5, idx1 - 5).Split('@'); // Text[5..idx].Split('@');

            idx1 = Text.IndexOf('{') + 1;
            int idx2 = Text.IndexOf('}');
            string cmd = Text.Substring(idx1, idx2 - idx1); //[idx1..idx2];

            int err = int.Parse(error[0]);
            idx1 = Text.IndexOf('}') + 2;
            return new FailureException(err, Text.Substring(idx1))
            {
                Error = err,
                CommandListNum = int.Parse(error[1]),
                CurrentCommand = cmd,
            };
        }
    }
}
