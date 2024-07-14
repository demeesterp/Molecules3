using MoleculesWebApp.Client.Data.Error;
using MoleculesWebApp.Client.Data.ServiceAgents.ErrorContract;
using MoleculesWebApp.Client.Shared.HttpClientHelper;
using System.Reactive.Subjects;
using System.Reactive.Linq;

namespace MoleculesWebApp.Client.Shared.Error
{
    public class ErrorHandlingService
    {
        private Subject<HttpErrorModel> HttpErrors = new Subject<HttpErrorModel>();

        public void NotifyHttpError(Exception exception)
        {
            if (exception is ServerErrorException)
            {
                ServerErrorException dummy = (ServerErrorException)exception;
                NotifyHttpError(dummy.ErrorContract, dummy.ErrorDetails);
            }

            if (exception is ServerValidationException)
            {
                ServerValidationException dummy = (ServerValidationException)exception;
                NotifyHttpError(dummy.ErrorContract, dummy.ErrorDetails);
            }

            if (exception is UnKnownHttpException)
            {
                UnKnownHttpException dummy = (UnKnownHttpException)exception;
                NotifyHttpError(dummy.Message, dummy.ErrorDetails);
            }
        }


        public void NotifyHttpError(ServiceError serviceError, HttpErrorDetails error)
        {
            HttpErrorModel httpError = new HttpErrorModel(serviceError, error);
            Console.WriteLine(httpError.GetFullErrorLog());
            HttpErrors.OnNext(httpError);
        }

        public void NotifyHttpError(string serviceError, HttpErrorDetails error)
        {
            HttpErrorModel httpError = new HttpErrorModel(serviceError, error);
            Console.WriteLine(httpError.GetFullErrorLog());
            HttpErrors.OnNext(httpError);
        }

        public void NotifyHttpError(ServiceValidationError serviceError, HttpErrorDetails error)
        {
            HttpErrorModel httpError = new HttpErrorModel(serviceError, error);
            Console.WriteLine(httpError.GetFullErrorLog());
            HttpErrors.OnNext(httpError);
        }

        public IObservable<HttpErrorModel> RegisterHttpErrorHandler(Action<HttpErrorModel> actions)
        {
            return HttpErrors.Do(item => actions(item));
        }

        public IObservable<HttpErrorModel> Errors => HttpErrors;

    }
}
