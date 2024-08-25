using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace MoleculesWebApp.Client.Components.Dialogs
{
    public partial class CreateOrderDialog : ComponentBase
    {
        private bool _isFormValidated = false;
        private string CalcOrderName { get; set; } = string.Empty;


        private string GetValidationClassName()
        {
            if ( _isFormValidated)
            {
                return "was-validated";
            }
            else
            {
                return "needs-validation";
            }
        }

        [Parameter] public EventCallback<string> OnClickCallback { get; set; }

        private async void OnClickSave(MouseEventArgs e)
        {
            _isFormValidated = true;
            await OnClickCallback.InvokeAsync(CalcOrderName);
        }
    }
}
