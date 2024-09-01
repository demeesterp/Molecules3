using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using MoleculesWebApp.Client.Data.Model.Molecule;
using MoleculesWebApp.Client.Services.Molecules;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace MoleculesWebApp.Client.Components.Pages
{
    public partial class MoleculesOverview : ComponentBase, IDisposable
    {
        #region datamembers
        [Inject] private IMoleculesAnalysisService? overViewService { get; set; }

        private List<MoleculesOverviewItemModel> Molecules { get; set; } = new List<MoleculesOverviewItemModel>();

        private string searchTerm = "";

        private Subject<Unit> _destroy = new Subject<Unit>();

        #endregion

        protected override void OnInitialized()
        {
            overViewService?.SearchCalculatedMolecules("%")
                            .TakeUntil(_destroy)
                            .Subscribe(molecules =>
                            {
                                Molecules = molecules;
                                StateHasChanged();
                            });
        }

        private void OnSearchMoleculeClick(MouseEventArgs args)
        {
            overViewService?.SearchCalculatedMolecules(searchTerm)
                            .TakeUntil(_destroy)
                            .Subscribe(molecules =>
                            {
                                Molecules = molecules;
                                StateHasChanged();
                            });
        }

        public void Dispose()
        {
            _destroy.OnNext(Unit.Default);
            _destroy.OnCompleted();
        }
    }
}
