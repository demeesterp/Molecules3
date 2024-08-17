using MoleculesWebApp.Client.Data.Model.Order;
using MoleculesWebApp.Client.Data.ServiceAgents.OrderBook;

namespace MoleculesWebApp.Client.Factory
{
    public class CalcOrderFactory : ICalcOrderFactory
    {
        public CalcOrderModel Build(CalcOrder order)
        {
            CalcOrderModel retval =
                new CalcOrderModel(order.Details.Name);

            foreach (var item in order.Items)
            {
                retval.OrderItems
                    .Add(new CalcOrderItemModel(item.Id,
                               item.MoleculeName,
                               item.Details.BasisSetCode.ToString(),
                               item.Details.Charge.ToString(),
                               item.Details.Type.ToString()));
            }
            return retval;
        }
    }
}
