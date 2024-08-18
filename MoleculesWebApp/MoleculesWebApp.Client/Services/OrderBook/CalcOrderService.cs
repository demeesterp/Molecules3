using MoleculesWebApp.Client.Data.Model.Order;
using MoleculesWebApp.Client.Factory;
using MoleculesWebApp.Client.Services.OrderBook.ServiceAgent;
using System.Reactive.Linq;

namespace MoleculesWebApp.Client.Services.OrderBook
{
    public class CalcOrderService : ICalcOrderService
    {
        private ICalcOrderFactory CalcOrderFactory { get; }

        private ICalcOrderServiceAgent CalcOrderServiceAgent { get; }


        public CalcOrderService(ICalcOrderFactory calcOrderFactory, ICalcOrderServiceAgent calcOrderServiceAgent)
        {
            CalcOrderFactory = calcOrderFactory;
            CalcOrderServiceAgent = calcOrderServiceAgent;
        }

        public IObservable<List<CalcOrderModel>> GetByName(string name)
        {
            return CalcOrderServiceAgent.GetByName(name).Select(orderList =>
                                        orderList.Select(order => CalcOrderFactory.Build(order))
                            .ToList());
        }

        public IObservable<List<CalcOrderModel>> List()
        {
            return CalcOrderServiceAgent.List()
                            .Select(orderList => 
                                        orderList.Select(order => CalcOrderFactory.Build(order))
                            .ToList());
        }
    }
}
