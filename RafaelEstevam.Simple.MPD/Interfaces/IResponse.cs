using System.IO;
using System.Threading.Tasks;

namespace RafaelEstevam.Simple.MPD.Interfaces
{
    public interface IResponse
    {
        Task ReadAsync(StreamReader stream);

        ICommand GetCommand();
    }
}
