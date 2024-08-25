using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Web;
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

            //var buffer = new byte[uploadedFile.Size];
            //await uploadedFile.OpenReadStream().ReadAsync(buffer);
            // Process the file (e.g., save it, send it to a server, etc.)
        }
        private void OnClickSave(MouseEventArgs e)
        {
            
        }
    }
}
