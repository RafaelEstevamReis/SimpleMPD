using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using RafaelEstevam.Simple.MPD.Responses;

namespace RafaelEstevam.Simple.MPD.Interfaces
{
    public interface IResponse
    {
        Task ReadAsync(Stream stream);

        ICommand GetCommand();
    }
}
