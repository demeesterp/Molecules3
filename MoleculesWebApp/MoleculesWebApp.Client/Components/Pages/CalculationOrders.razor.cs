using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using MoleculesWebApp.Client.Components.Dialogs;
using MoleculesWebApp.Client.Data.Model.Order;
using MoleculesWebApp.Client.Services.OrderBook;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using MoleculesWebApp.Client.Data.ServiceAgents.OrderBook;

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

        #region new order dialog

        private async void OnNewOrderClick()
        {
            var parameters = new Dictionary<string, object>
            {
                { "OnclickCallback", EventCallback.Factory.Create<string>(this, OnDialogSaveNewOrderClick) }
            };
            await createOrderModal.ShowAsync<CreateOrderDialog>(title: "Create Order", parameters: parameters);
        }
        private async void OnDialogSaveNewOrderClick(string orderName)
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

        #endregion

        #region update order dialog

        private async void OnUpdateOrderClick(MouseEventArgs e)
        {
            var parameters = new Dictionary<string, object>
            {
                { "OnclickCallback", EventCallback.Factory.Create<CalcOrderModel>(this, OnDialogSaveUpdateOrderClick) },
                { "CalcOrder",   Selected! },
            };
            await updateOrderModal.ShowAsync<UpdateOrderDialog>(title: "Update Order", parameters:parameters);
        }

        private async void OnDialogSaveUpdateOrderClick(CalcOrderModel calcOrder)
        {
            await updateOrderModal.HideAsync();
            CalcOrderService.Update(calcOrder.Id, calcOrder.Name).TakeUntil(_destroy).Subscribe(order =>
            {
                if (order.IsValid())
                {
                    var updatedOrder = Orders.FindIndex(x => x.Id == order.Id)!;
                    if (updatedOrder > 0)
                    {
                        Orders[updatedOrder] = order;
                    }
                    StateHasChanged();
                }
            });
        }

        #endregion

        private async void OnNewOrderItemClick(MouseEventArgs e)
        {
	        var parameters = new Dictionary<string, object>
	        {
		        {
			        "OnclickCallback", 
			        EventCallback.Factory.Create<CalcOrderItemModel>(this, OnDialogCreateOrderItemSaveClick)
		        }
	        };
			await createOrderItemModal.ShowAsync<CreateOrderItemDialog>(title: "Create Order Item", parameters:parameters);
        }

        private async void OnDialogCreateOrderItemSaveClick(CalcOrderItemModel orderItemModel)
        {
	        await createOrderItemModal.HideAsync();
	        if (Selected is null) return;
	        CalcOrderService.CreateItem(Selected.Id, orderItemModel)
		        .TakeUntil(_destroy).Subscribe(orderItem =>
		        {
                    Selected.OrderItems.Add(orderItem);
                    StateHasChanged();
                });
        }

        private async void OnDeleteOrderItemClick(CalcOrderItemModel calcOrderItemModel)
        {
           if (Selected == null) return;
           var confirmation = await confirmDialog.ShowAsync<DeleteConfirmDialog>(title:$"Delete Order Item {calcOrderItemModel.MoleculeName}");
           if (confirmation)
           {
               CalcOrderService.DeleteItem(Selected.Id, calcOrderItemModel.Id)
                    .TakeUntil(_destroy)
                    .Subscribe(result =>
                    {
                       var changedOrder = Orders.Find(o => o.Id == Selected.Id);
                       if (changedOrder != null)
                       {
                           var index = changedOrder.OrderItems.FindIndex(oi => oi.Id == calcOrderItemModel.Id);
                           if ( index >= 0)
                           {
                              changedOrder.OrderItems.RemoveAt(index);
                              StateHasChanged();
	                       }
                       }
                    });
           }
        }
        
        private async void OnDeleteOrderClick(MouseEventArgs e)
        {
           if (Selected is null) return;
           var confirmation = await confirmDialog.ShowAsync<DeleteConfirmDialog>(title: $"Delete Order {Selected.Name}");
           if (confirmation)
           {
               CalcOrderService.Delete(Selected.Id)
                  .TakeUntil(_destroy)
                  .Subscribe(result =>
                  {
                     if (Orders.Remove(Selected))
                     {
                        Selected = Orders.FirstOrDefault();
                        StateHasChanged();
                     };
                  });
           }
        }

        public void Dispose()
        {
            _destroy.OnNext(Unit.Default);
            _destroy.OnCompleted();
        }
        
    }
}
