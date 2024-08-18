using MoleculesWebApp.Client.Data.Model.Order;

namespace MoleculesWebApp.Client.Services.OrderBook
{
    public interface ICalcOrderService
    {
        IObservable<List<CalcOrderModel>> List();

        IObservable<List<CalcOrderModel>> GetByName(string name);
    }
}
