using MoleculesWebApp.Client.Data.Model.Order;
using MoleculesWebApp.Client.Data.ServiceAgents.OrderBook;

namespace MoleculesWebApp.Client.Factory
{
    public class CalcOrderFactory : ICalcOrderFactory
    {
        private ICalcOrderItemFactory CalcOrderItemFactory { get; }

        public CalcOrderFactory(ICalcOrderItemFactory calcOrderItemFactory)
        {
            CalcOrderItemFactory = calcOrderItemFactory;
        }

        public CalcOrderModel Build(CalcOrder order)
        {
            CalcOrderModel retval =  new CalcOrderModel(order.Details.Name);
            retval.Id = order.Id;
            
            foreach (var item in order.Items)
            {
                retval.OrderItems .Add(CalcOrderItemFactory.Build(item));
            }
            return retval;
        }
    }
}
