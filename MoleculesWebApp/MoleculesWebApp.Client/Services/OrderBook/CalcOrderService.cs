using MoleculesWebApp.Client.Data.Model.Order;
using MoleculesWebApp.Client.Data.ServiceAgents.OrderBook;
using MoleculesWebApp.Client.Factory;
using MoleculesWebApp.Client.Services.OrderBook.ServiceAgent;
using MoleculesWebApp.Client.Shared.Error;
using System;
using System.Reactive;
using System.Reactive.Linq;

namespace MoleculesWebApp.Client.Services.OrderBook
{
    public class CalcOrderService : ICalcOrderService
    {
        private ICalcOrderFactory CalcOrderFactory { get; }

        private ICalcOrderItemFactory CalcOrderItemFactory { get; }

        private ErrorHandlingService ErrorHandlingService { get; }

        private ICalcOrderServiceAgent CalcOrderServiceAgent { get; }


        public CalcOrderService(ICalcOrderFactory calcOrderFactory,
                                    ICalcOrderItemFactory calcOrderItemFactory,
                                    ICalcOrderServiceAgent calcOrderServiceAgent,
                                        ErrorHandlingService errorHandlingService)
        {
            CalcOrderFactory = calcOrderFactory;
            CalcOrderServiceAgent = calcOrderServiceAgent;
            ErrorHandlingService = errorHandlingService;
            CalcOrderItemFactory = calcOrderItemFactory;
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

        public IObservable<CalcOrderModel> Update(int id, string name)
        {
            return CalcOrderServiceAgent.Update(id, new UpdateCalcOrder()
            {
                 Name = name
            })
            .Select(order => CalcOrderFactory.Build(order))
            .Catch<CalcOrderModel, Exception>(HandleError<CalcOrderModel>);
        }

        public IObservable<CalcOrderItemModel> CreateItem(int calcOrderId, CalcOrderItemModel calcOrderItem)
        {
            var itemToSave = CalcOrderItemFactory.Build(calcOrderId, calcOrderItem);
            return CalcOrderServiceAgent.CreateCalcOrderItem(calcOrderId, new CreateCalcOrderItem(itemToSave.MoleculeName, 
                                                                                                    itemToSave.Details))
             .Select(orderItem => CalcOrderItemFactory.Build(orderItem))
             .Catch<CalcOrderItemModel, Exception>(HandleError<CalcOrderItemModel>);
        }

        public IObservable<Unit> Delete(int calcOrderId)
        {
            return CalcOrderServiceAgent.Delete(calcOrderId)
                        .Catch<Unit, Exception>(HandleError<Unit>); 
        }

        public IObservable<Unit> DeleteItem(int calcOrderId, int calcOrderItemId)
        {
            return CalcOrderServiceAgent.DeleteCalcOrderItem(calcOrderId, calcOrderItemId)
                        .Catch<Unit, Exception>(HandleError<Unit>);
        }

        #region private helpers

        private IObservable<ReturnType> HandleError<ReturnType>(Exception ex)
        {
            ErrorHandlingService.NotifyHttpError(ex);
            return Observable.Empty<ReturnType>();
        }

        #endregion
    }
}
