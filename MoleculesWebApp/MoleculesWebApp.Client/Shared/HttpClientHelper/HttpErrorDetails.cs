using System.Net;

namespace MoleculesWebApp.Client.Shared.HttpClientHelper
{
    public record HttpErrorDetails(HttpMethod method, HttpStatusCode status, string? requestUri) { }
}
