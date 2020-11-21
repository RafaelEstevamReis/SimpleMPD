using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace RafaelEstevam.Simple.MPD.Interfaces
{
    public interface ICommand
    {
        string CommandName { get; }

        Task WriteAsync(Stream stream);

        IResponse GetResponseProcessor();
    }
}
