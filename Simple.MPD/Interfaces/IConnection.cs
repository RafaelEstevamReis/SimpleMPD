using System.IO;
using System.Threading.Tasks;

namespace Simple.MPD.Interfaces
{
    public interface IConnection
    {
        bool IsConnected { get; }

        Task OpenAsync();
        void Open();

        void Close();

        StreamReader GetReader();
        StreamWriter GetWriter();
    }
}
