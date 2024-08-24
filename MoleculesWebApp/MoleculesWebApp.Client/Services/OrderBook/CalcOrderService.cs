using MoleculesWebApp.Client.Data.Model.Order;
using MoleculesWebApp.Client.Data.ServiceAgents.OrderBook;
using MoleculesWebApp.Client.Factory;
using MoleculesWebApp.Client.Services.OrderBook.ServiceAgent;
using MoleculesWebApp.Client.Shared.Error;
using System.Reactive.Linq;

namespace MoleculesWebApp.Client.Services.OrderBook
{
    public class CalcOrderService : ICalcOrderService
    {
        private ICalcOrderFactory CalcOrderFactory { get; }

        private ErrorHandlingService ErrorHandlingService { get; }

        private ICalcOrderServiceAgent CalcOrderServiceAgent { get; }


        public CalcOrderService(ICalcOrderFactory calcOrderFactory,
                                    ICalcOrderServiceAgent calcOrderServiceAgent,
                                        ErrorHandlingService errorHandlingService)
        {
            CalcOrderFactory = calcOrderFactory;
            CalcOrderServiceAgent = calcOrderServiceAgent;
            ErrorHandlingService = errorHandlingService;
        }

        public IObservable<List<CalcOrderModel>> GetByName(string name)
        {
            return CalcOrderServiceAgent.GetByName(name).Select(orderList =>
                                        orderList.Select(order => CalcOrderFactory.Build(order)).ToList())
                                        .Catch<List<CalcOrderModel>, Exception>(HandleError<List<CalcOrderModel>>);
        }

        public IObservable<List<CalcOrderModel>> List()
        {
            return CalcOrderServiceAgent.List()
                            .Select(orderList => orderList.Select(order => CalcOrderFactory.Build(order)).ToList())
                            .Catch<List<CalcOrderModel>, Exception>(HandleError<List<CalcOrderModel>>);
        }

        public IObservable<CalcOrderModel> Create(string name)
        {
            return CalcOrderServiceAgent.Create(new CreateCalcOrder()
            {
                Name = name
            })
            .Select(order => CalcOrderFactory.Build(order))
            .Catch<CalcOrderModel, Exception>(HandleError<CalcOrderModel>);
        }



        #region private helpers

        private IObservable<ReturnType> HandleError<ReturnType>(Exception ex) where ReturnType : new()
        {
            ErrorHandlingService.NotifyHttpError(ex);
            return Observable.Return(new ReturnType());
        }

        #endregion
    }
}
