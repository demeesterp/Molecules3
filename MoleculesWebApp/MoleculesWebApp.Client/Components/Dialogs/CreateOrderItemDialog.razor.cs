using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Web;
using MoleculesWebApp.Client.Data.Model.Order;
using MoleculesWebApp.Client.Data.ServiceAgents.OrderBook;

namespace MoleculesWebApp.Client.Components.Dialogs
{
    public partial class CreateOrderItemDialog : ComponentBase
    {
        private bool _isFormValidated = false;

        private string MoleculeName { get; set; } = string.Empty;

        private string Charge { get; set; } = string.Empty;

        private string CalcType { get; set; } = "AllKinds";

        private string BasisSet { get; set; } = CalcBasisSetCode.BSTO3G.ToString();

        private List<string> BasisSets { get; set; } = Enum.GetNames<CalcBasisSetCode>().ToList();

        private string Xyz { get; set; } = string.Empty;


        [Parameter] public EventCallback<CalcOrderItemModel> OnClickCallback { get; set; }

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

        private async Task OnInputFileChange(InputFileChangeEventArgs e)
        {
            var uploadedFile = e.File;
            await using var stream = uploadedFile.OpenReadStream();
            using StreamReader rd = new StreamReader(stream);
            Xyz = await rd.ReadToEndAsync();
        }
        private void OnClickSave(MouseEventArgs e)
        {
	        _isFormValidated = true;
	        OnClickCallback.InvokeAsync(new CalcOrderItemModel(0,MoleculeName, BasisSet, Charge, CalcType, Xyz));
        }
    }
}
