using System.IO;
using System.Threading.Tasks;

namespace Simple.MPD.Interfaces
{
    public interface ICommand
    {
        string CommandName { get; }

        Task WriteAsync(StreamWriter stream);

        IResponse GetResponseProcessor();
    }
}
