using MoleculesWebApp.Client.Data.ServiceAgents.ErrorContract;

namespace MoleculesWebApp.Client.Shared.HttpClientHelper
{
    public class ServerErrorException : MoleculesHttpClientException<ServiceError>
    {
        public ServerErrorException(ServiceError errorContract, HttpErrorDetails errorDetails)
                : base(errorContract, errorDetails)
        {
        }
    }
}
