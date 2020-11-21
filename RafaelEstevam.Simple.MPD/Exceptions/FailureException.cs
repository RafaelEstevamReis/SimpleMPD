using System;
using System.Collections.Generic;
using System.Text;

namespace RafaelEstevam.Simple.MPD.Exceptions
{
    public class FailureException : Exception
    {
        public int Error { get; set; }
        public int CommandListNum { get; set; }
        public string CurrentCommand { get; set; }
        public string MessageText { get; set; }

        public FailureException()
            : base()
        { }
        public FailureException(int Error, string MessageText)
            : base($"{Error}: {MessageText}")
        {
            this.Error = Error;
            this.MessageText = MessageText;
        }

    }
}
