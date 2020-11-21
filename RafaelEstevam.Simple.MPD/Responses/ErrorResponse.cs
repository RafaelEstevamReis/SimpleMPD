using System.IO;
using System.Threading.Tasks;
using RafaelEstevam.Simple.MPD.Interfaces;

namespace RafaelEstevam.Simple.MPD.Responses
{
    public class ErrorResponse : IResponse
    {
        IResponse IResponse.ErrorResponse { get; set; }

        public string ErrorText { get; }

        public ErrorResponse(string errorText)
        {
            this.ErrorText = errorText;
        }


        public ICommand GetCommand()
        {
            throw new System.NotImplementedException();
        }
        public Task ReadAsync(Stream stream)
        {
            throw new System.NotImplementedException();
        }
    }
}
