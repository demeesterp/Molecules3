using MoleculesWebApp.Client.Data.Model.Order;
using MoleculesWebApp.Client.Data.ServiceAgents.OrderBook;

namespace MoleculesWebApp.Client.Factory
{
    public interface ICalcOrderFactory
    {
        CalcOrderModel Build(CalcOrder order);
    }
}
