using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Web;
using MoleculesWebApp.Client.Data.Model.Calculation;
using MoleculesWebApp.Client.Data.Model.Order;
using MoleculesWebApp.Client.Data.ServiceAgents.OrderBook;
using MoleculesWebApp.Client.Services.Calculation;

namespace MoleculesWebApp.Client.Components.Dialogs
{
    public partial class CreateOrderItemDialog : ComponentBase
    {

        [Inject]
        private IBasisSetService BasisSetService { get; init; }

        [Inject]
        private ICalcOrderItemTypeService CalcOrderItemTypeService { get; init; }

        private bool _isFormValidated = false;

        private string MoleculeName { get; set; } = string.Empty;

        private string Charge { get; set; } = string.Empty;

        private string CalcType { get; set; } = CalcOrderItemType.GeoOpt.ToString();

        private string BasisSet { get; set; } = CalcBasisSetCode.BSTO3G.ToString();

        private List<CalcBasisSetModel> BasisSets { get;} = new List<CalcBasisSetModel>();

        private List<CalcOrderItemTypeModel> CalcOrderItemTypes { get; } = new List<CalcOrderItemTypeModel>();

        private string Xyz { get; set; } = string.Empty;

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



        protected override void OnInitialized()
        {
            base.OnInitializedAsync();
            BasisSets.AddRange(BasisSetService.List());
            CalcOrderItemTypes.AddRange(CalcOrderItemTypeService.List());
        }

        [Parameter] public EventCallback<CalcOrderItemModel> OnClickCallback { get; set; }

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
