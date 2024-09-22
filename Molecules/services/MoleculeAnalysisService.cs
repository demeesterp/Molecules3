using Molecules.settings;

namespace Molecules.services
{
    public class MoleculeAnalysisService
    {

        #region dependencies

        private readonly IMoleculesSettings _settings;

        #endregion


        public MoleculeAnalysisService(IMoleculesSettings settings)
        {
            _settings = settings;
        }


        public async Task RunAsync()
        {
            Console.WriteLine(nameof(MoleculeAnalysisService));
            
            
            
            await Task.CompletedTask;
        }

    }
}
