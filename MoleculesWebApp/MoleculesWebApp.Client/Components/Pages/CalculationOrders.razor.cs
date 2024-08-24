using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using MoleculesWebApp.Client.Components.Dialogs;
using MoleculesWebApp.Client.Data.Model.Order;
using MoleculesWebApp.Client.Services.OrderBook;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace MoleculesWebApp.Client.Components.Pages
{
    public partial class CalculationOrders : ComponentBase, IDisposable
    {
        private BlazorBootstrap.Modal createOrderModal = default!;
        private BlazorBootstrap.Modal updateOrderModal = default!;
        private BlazorBootstrap.Modal createOrderItemModal = default!;
        private BlazorBootstrap.ConfirmDialog confirmDialog = default!;

        private Subject<Unit> _destroy = new Subject<Unit>();

        [Inject] 
        private ICalcOrderService CalcOrderService { get; init; }

        private List<CalcOrderModel> Orders { get; set; } = new List<CalcOrderModel>();

        private CalcOrderModel? Selected { get; set; }

        protected override void OnInitialized()
        {
            CalcOrderService.List().TakeUntil(_destroy).Subscribe(orders =>
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

        private async void OnNewOrderClick()
        {
            var parameters = new Dictionary<string, object>
            {
                { "OnclickCallback", EventCallback.Factory.Create<string>(this, OnSaveNewOrderClick) }
            };
            await createOrderModal.ShowAsync<CreateOrderDialog>(title: "Create Order", parameters: parameters);
        }

        private async void OnSaveNewOrderClick(string orderName)
        {
            await createOrderModal.HideAsync();
            CalcOrderService.Create(orderName).TakeUntil(_destroy).Subscribe(order =>
            {
                if (order.IsValid())
                {
                    Orders.Add(order);
                    Selected = order;
                    StateHasChanged();
                }
            });
        }


        private async void OnUpdateOrderClick(MouseEventArgs e)
        {
            await updateOrderModal.ShowAsync<UpdateOrderDialog>(title: "Update Order");
        }
        
        private async void OnNewOrderItemClick(MouseEventArgs e)
        {
            await createOrderItemModal.ShowAsync<CreateOrderItemDialog>(title: "Create Order Item");
        }

        private async void OnDeleteOrderItemClick(CalcOrderItemModel calcOrderItemModel)
        {
           var confirmation = await confirmDialog.ShowAsync<DeleteConfirmDialog>(title:"Delete Order Item");
        }
        
        private async void OnDeleteOrderClick(MouseEventArgs e)
        {
           var confirmation = await confirmDialog.ShowAsync<DeleteConfirmDialog>(title: "Delete Order");
        }


        public void Dispose()
        {
            _destroy.OnNext(Unit.Default);
            _destroy.OnCompleted();
        }
        
    }
}
