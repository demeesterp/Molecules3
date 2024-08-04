using Microsoft.AspNetCore.Components;

namespace MoleculesWebApp.Client.Components.Pages
{
    public partial class MoleculesOverview : ComponentBase
    {
        //#region datamembers
        //[Inject] private IMoleculesAnalysisService? overViewService { get; set; }

        //private List<MoleculesOverviewItemVM> Molecules { get; set; } = new List<MoleculesOverviewItemVM>();

        //private string searchTerm = "";

        //#endregion

        //protected override void OnInitialized()
        //{
        //    overViewService?.SearchCalculatedMolecules("%")
        //                    .Subscribe(molecules =>
        //                    {
        //                        Molecules = molecules;
        //                        StateHasChanged();
        //                    });
        //}

        //private void OnSearchMoleculeClick(MouseEventArgs args)
        //{
        //    overViewService?.SearchCalculatedMolecules(searchTerm)
        //                    .Subscribe(molecules =>
        //                    {
        //                        Molecules = molecules;
        //                        StateHasChanged();
        //                    });
        //}
    }
}
