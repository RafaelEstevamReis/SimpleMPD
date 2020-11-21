using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace RafaelEstevam.Simple.MPD.Interfaces
{
    public interface IConnection
    {
        bool IsConnected { get; }

        Task OpenAsync();
        void Open();

        void Close();

        Stream GetStream();
    }
}
