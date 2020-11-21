using RafaelEstevam.Simple.MPD.Interfaces;

namespace RafaelEstevam.Simple.MPD.Helper
{
    public class ResponseHelper
    {
        public static void ProcessError(IResponse response, string ErrorText)
        {
            response.ErrorResponse = new Responses.ErrorResponse(ErrorText);
        }
    }
}
