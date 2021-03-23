using System.IO;
using System.Threading.Tasks;

namespace Simple.MPD.Interfaces
{
    public interface IResponse
    {
        Task ReadAsync(StreamReader stream);

        ICommand GetCommand();
    }
}
