using MoleculesWebApp.Client.Data.ServiceAgents.ErrorContract;
namespace MoleculesWebApp.Client.Shared.HttpClientHelper
{
    public class ServerValidationException : MoleculesHttpClientException<ServiceValidationError>
    {
        public ServerValidationException(ServiceValidationError errorContract, HttpErrorDetails errorDetails) :
            base(errorContract, errorDetails)
        { }
    }
}
