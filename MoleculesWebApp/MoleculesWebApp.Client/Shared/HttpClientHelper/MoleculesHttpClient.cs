using MoleculesWebApp.Client.Data.ServiceAgents.ErrorContract;
using System.Net.Http.Json;
using System.Net;
using System.Reactive.Linq;
using System.Reactive;

namespace MoleculesWebApp.Client.Shared.HttpClientHelper
{
    public class MoleculesHttpClient
    {
        public IObservable<ReturnType> Post<ReturnType, ArgType>(HttpClient httpClient, string urlFragment, ArgType argument)
        {
            var response = Observable.FromAsync(() => httpClient.PostAsJsonAsync(urlFragment, argument));
            return response.SelectMany(async item => await HandleResponseAsync<ReturnType>(item, HttpMethod.Post));
        }

        public IObservable<Unit> Post<ArgType>(HttpClient httpClient, string urlFragment, ArgType argument) where ArgType : new()
        {
            var response = Observable.FromAsync(() => httpClient.PostAsJsonAsync(urlFragment, argument));
            return response.SelectMany(async item => await HandleResponseAsync(item, HttpMethod.Post));
        }

        public IObservable<ReturnType> Put<ReturnType, ArgType>(HttpClient httpClient, string urlFragment, ArgType argument)
        {
            var response = Observable.FromAsync(() => httpClient.PutAsJsonAsync(urlFragment, argument));
            return response.SelectMany(async item => await HandleResponseAsync<ReturnType>(item, HttpMethod.Put));
        }

        public IObservable<Unit> Put<ArgType>(HttpClient httpClient, string urlFragment, ArgType argument) where ArgType : new()
        {
            var response = Observable.FromAsync(() => httpClient.PutAsJsonAsync(urlFragment, argument));
            return response.SelectMany(async item => await HandleResponseAsync(item, HttpMethod.Put));
        }

        public IObservable<ReturnType> Patch<ReturnType, ArgType>(HttpClient httpClient, string urlFragment, ArgType argument)
                             where ReturnType : new()
        {
            var response = Observable.FromAsync(() => httpClient.PatchAsJsonAsync(urlFragment, argument));
            return response.SelectMany(async item => await HandleResponseAsync<ReturnType>(item, HttpMethod.Patch));
        }

        public IObservable<Unit> Patch<ArgType>(HttpClient httpClient, string urlFragment, ArgType argument) where ArgType : new()
        {
            var response = Observable.FromAsync(() => httpClient.PatchAsJsonAsync(urlFragment, argument));
            return response.SelectMany(async item => await HandleResponseAsync(item, HttpMethod.Patch));
        }

        public IObservable<Unit> Delete<ArgType>(HttpClient httpClient, string urlFragment)
        {
            var response = Observable.FromAsync(() => httpClient.DeleteAsync(urlFragment));
            return response.SelectMany(async item => await HandleResponseAsync(item, HttpMethod.Delete));
        }

        public IObservable<ReturnType> Get<ReturnType>(HttpClient httpClient, string urlFragment)
        {
            var response = Observable.FromAsync(() => httpClient.GetAsync(urlFragment));
            return response.SelectMany(async item => await HandleResponseAsync<ReturnType>(item, HttpMethod.Get));
        }

        #region private helpers

        private async Task<Unit> HandleResponseAsync(HttpResponseMessage httpResponse, HttpMethod httpMethod)
        {
            if (httpResponse.IsSuccessStatusCode)
            {
                return Unit.Default;
            }
            else if (!await TryHandleHttpErrorResponseAsync(httpResponse, httpMethod))
            {
                throw new UnKnownHttpException(
                                new HttpErrorDetails(httpMethod,
                                     HttpStatusCode.UnprocessableContent,
                                     httpResponse.RequestMessage?.RequestUri?.ToString() ?? "No request ui available"));
            }
            else
            {
                return await Observable.
                                Throw<Unit>(new UnKnownHttpException
                                                (new HttpErrorDetails(httpMethod,
                                                    HttpStatusCode.UnprocessableContent,
                                                    httpResponse.RequestMessage?.RequestUri?.ToString() ?? "No request ui available")));
            }
        }

        private async Task<ReturnType> HandleResponseAsync<ReturnType>(HttpResponseMessage httpResponse, HttpMethod httpMethod)
        {
            if (httpResponse.IsSuccessStatusCode)
            {
                var result = await httpResponse.Content.ReadFromJsonAsync<ReturnType>();
                if (result != null)
                {
                    return result;
                }
            }
            else if (!await TryHandleHttpErrorResponseAsync(httpResponse, httpMethod))
            {
                return await Observable.Throw<ReturnType>(new UnKnownHttpException(new HttpErrorDetails(httpMethod,
                            HttpStatusCode.UnprocessableContent,
                httpResponse.RequestMessage?.RequestUri?.ToString() ?? "No request ui available")));

            }
            return await Observable.Throw<ReturnType>(new UnKnownHttpException(new HttpErrorDetails(httpMethod,
                                                     HttpStatusCode.UnprocessableContent,
                                                        httpResponse.RequestMessage?.RequestUri?.ToString() ?? "No request ui available")));
        }

        private async static Task<bool> TryHandleHttpErrorResponseAsync(HttpResponseMessage httpResponse, HttpMethod httpMethod)
        {
            if (httpResponse.IsSuccessStatusCode) return true; // When success nothing to do
            if (httpResponse.StatusCode == HttpStatusCode.UnprocessableContent)
            {
                var validationError = await httpResponse.Content.ReadFromJsonAsync<ServiceValidationError>();
                if (validationError != null)
                {
                    return await Observable.Throw<bool>(new ServerValidationException(validationError,
                                                    new HttpErrorDetails(httpMethod,
                                                        HttpStatusCode.UnprocessableContent,
                                                             httpResponse.RequestMessage?.RequestUri?.ToString() ?? "No request uri available")));
                }
            }
            else
            {
                var serverError = await httpResponse.Content.ReadFromJsonAsync<ServiceError>();
                if (serverError != null)
                {
                    return await Observable.Throw<bool>(new ServerErrorException(serverError,
                        new HttpErrorDetails(httpMethod,
                                                HttpStatusCode.UnprocessableContent,
                                                  httpResponse.RequestMessage?.RequestUri?.ToString() ?? "No request ui available")));
                }
            }
            return false;
        }

        #endregion
    }
}
