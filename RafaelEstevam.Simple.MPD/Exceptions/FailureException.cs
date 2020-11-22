using System;

namespace RafaelEstevam.Simple.MPD.Exceptions
{
    public class FailureException : Exception
    {
        public int Error { get; set; }
        public int CommandListNum { get; set; }
        public string CurrentCommand { get; set; }

        public FailureException()
            : base()
        { }
        public FailureException(int Error, string Message)
            : base(Message)
        {
            this.Error = Error;
        }

        public static FailureException FromResponseText(string Text)
        {
            // ACK [error@command_listNum] {current_command} message_text
            // ACK [2@0] {ping} wrong number of arguments for "ping"
            string[] error = Text[5..Text.IndexOf(']')].Split('@');
            string cmd = Text[(Text.IndexOf('{') + 1)..Text.IndexOf('}')];

            int err = int.Parse(error[0]);
            return new FailureException(err, Text[(Text.IndexOf('}') + 2)..])
            {
                Error = err,
                CommandListNum = int.Parse(error[1]),
                CurrentCommand = cmd,
            };
        }
    }
}
