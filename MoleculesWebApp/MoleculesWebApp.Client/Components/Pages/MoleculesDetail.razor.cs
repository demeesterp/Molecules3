using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MoleculesWebApp.Client.Data.Model.Molecule;
using MoleculesWebApp.Client.Services.Molecules;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace MoleculesWebApp.Client.Components.Pages
{
    public partial class MoleculesDetail : ComponentBase, IDisposable
    {

        [Inject]
        private IMoleculesAnalysisService? analysisService { get; set; }

        [Inject]
        private IJSRuntime jsRuntime { get; set; }

        [Parameter]
        public int MoleculeId { get; set; }

        private Subject<Unit> _destroy = new Subject<Unit>();

        public MoleculeReportModel CurrentMoleculeReport { get; set; } = new MoleculeReportModel(0);


        protected override void OnInitialized()
        {
            analysisService?.GetReport(MoleculeId)
                .TakeUntil(_destroy)
                .Subscribe(async report => {
                    CurrentMoleculeReport = report;
                    var molecule = GetMolfile();

                    // Call the JavaScript function
                    await jsRuntime.InvokeVoidAsync("drawMolecule2D", "moleculedisplay", molecule);
                    StateHasChanged();
                }
            );
        }


        private string GetMolfile()
        {
            return MoleculesDisplayService.GetMolFile(CurrentMoleculeReport);
        }
        
        public void Dispose()
        {
            _destroy.OnNext(Unit.Default);
            _destroy.OnCompleted();
        }
    }
}
