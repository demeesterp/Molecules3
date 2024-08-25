using MoleculesWebApp.Client.Data.Model.Order;
using MoleculesWebApp.Client.Data.ServiceAgents.OrderBook;

namespace MoleculesWebApp.Client.Factory
{
    public interface ICalcOrderItemFactory
    {
        CalcOrderItemModel Build(CalcOrderItem item);

        CalcOrderItem Build(int calcOrder, CalcOrderItemModel itemModel);
    }
}
