namespace MoleculesWebApp.Client.Shared.HttpClientHelper
{
    public class UnKnownHttpException : Exception
    {
        public UnKnownHttpException(HttpErrorDetails httpErrorDetails)
                    : base("An unexpected error happend during http request")
        {
            ErrorDetails = httpErrorDetails;
        }
        public HttpErrorDetails ErrorDetails { get; }
    }
}
