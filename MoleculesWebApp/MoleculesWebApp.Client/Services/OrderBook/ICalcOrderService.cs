using MoleculesWebApp.Client.Data.Model.Order;
using System.Reactive;

namespace MoleculesWebApp.Client.Services.OrderBook
{
    public interface ICalcOrderService
    {
        IObservable<List<CalcOrderModel>> List();

        IObservable<List<CalcOrderModel>> GetByName(string name);

        IObservable<CalcOrderModel> Create(string name);

        IObservable<CalcOrderModel> Update(int id, string name);

        IObservable<CalcOrderItemModel> CreateItem(int calcOrderId, CalcOrderItemModel calcOrderItem);

        IObservable<Unit> Delete(int calcOrderId);

        IObservable<Unit> DeleteItem(int calcOrderItemId);
    }
}
