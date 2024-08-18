using Microsoft.AspNetCore.Components;
using MoleculesWebApp.Client.Data.Model.Order;
using MoleculesWebApp.Client.Services.OrderBook;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace MoleculesWebApp.Client.Components.Pages
{
    public partial class CalculationOrders : ComponentBase, IDisposable
    {
        private Subject<Unit> _destroy = new Subject<Unit>();

        [Inject] private ICalcOrderService CalcOrderService { get; init; }

        private List<CalcOrderModel> Orders { get; set; } = new List<CalcOrderModel>();

        private CalcOrderModel? Selected { get; set; }

        protected override void OnInitialized()
        {
            CalcOrderService.List().Subscribe(orders =>
            {
                Orders = orders;
                Selected = Orders.FirstOrDefault();
                StateHasChanged();
            });
        }

        private void OnOrderClick(CalcOrderModel order)
        {
            Selected = order;
        }

        public void Dispose()
        {
            _destroy.OnNext(Unit.Default);
            _destroy.OnCompleted();
        }
    }
}
