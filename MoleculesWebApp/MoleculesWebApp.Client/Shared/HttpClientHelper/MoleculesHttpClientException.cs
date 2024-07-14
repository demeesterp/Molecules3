using MoleculesWebApp.Client.Data.ServiceAgents.ErrorContract;

namespace MoleculesWebApp.Client.Shared.HttpClientHelper
{
    public class MoleculesHttpClientException<ErrorContractType> : Exception where ErrorContractType : IServiceError
    {
        public MoleculesHttpClientException(ErrorContractType errorContract, HttpErrorDetails errorDetails) :
                base(errorContract.GetErrorMsg())
        {
            ErrorDetails = errorDetails;
            ErrorContract = errorContract;
        }

        public ErrorContractType ErrorContract { get; set; }

        public HttpErrorDetails ErrorDetails { get; }

    }
}
