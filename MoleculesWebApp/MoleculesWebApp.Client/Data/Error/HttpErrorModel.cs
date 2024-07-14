using MoleculesWebApp.Client.Data.ServiceAgents.ErrorContract;
using MoleculesWebApp.Client.Shared.HttpClientHelper;
using System.Net;

namespace MoleculesWebApp.Client.Data.Error
{
    public class HttpErrorModel
    {
        public HttpErrorModel(ServiceError serviceError, HttpErrorDetails httpErrorDetails)
        {
            StatusCode = httpErrorDetails.status;
            HttpMethod = httpErrorDetails.method;
            Uri = httpErrorDetails.requestUri;
            ServerErrorMessage = serviceError.GetErrorMsg();
        }

        public HttpErrorModel(string serviceError, HttpErrorDetails httpErrorDetails)
        {
            StatusCode = httpErrorDetails.status;
            HttpMethod = httpErrorDetails.method;
            Uri = httpErrorDetails.requestUri;
            ServerErrorMessage = serviceError;
        }

        public HttpErrorModel(ServiceValidationError serviceValidationError, HttpErrorDetails httpErrorDetails)
        {
            StatusCode = httpErrorDetails.status;
            HttpMethod = httpErrorDetails.method;
            Uri = httpErrorDetails.requestUri;
            ServerErrorMessage = $"{serviceValidationError.GetErrorMsg()} : " +
                                    $"{string.Join(",", serviceValidationError.ValidationErrors
                                                            .Select(vError => $"{vError.PropertyName} : {vError.Message}")
                                                            .ToList())}";
        }

        public HttpMethod HttpMethod { get; set; }

        public HttpStatusCode StatusCode { get; set; }

        public string? Uri { get; set; }

        public string ServerErrorMessage { get; set; }


        public string GetFullErrorLog()
        {
            return $"An error occured while calling {HttpMethod} on {Uri} with Http statuscode {StatusCode} and error message {ServerErrorMessage}.";
        }
    }
}
