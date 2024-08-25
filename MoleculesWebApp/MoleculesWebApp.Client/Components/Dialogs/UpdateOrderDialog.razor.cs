using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using MoleculesWebApp.Client.Data.Model.Order;

namespace MoleculesWebApp.Client.Components.Dialogs
{
    public partial class UpdateOrderDialog : ComponentBase
    {
        private bool _isFormValidated = false;        

        private string GetValidationClassName()
        {
            if (_isFormValidated)
            {
                return "was-validated";
            }
            else
            {
                return "needs-validation";
            }
        }

        [Parameter] public CalcOrderModel CalcOrder { get; set; } = new CalcOrderModel(string.Empty);
        [Parameter] public EventCallback<CalcOrderModel> OnClickCallback { get; set; }

        private async void OnClickSave(MouseEventArgs e)
        {
            _isFormValidated = true;
            await OnClickCallback.InvokeAsync(CalcOrder);
        }
    }
}
