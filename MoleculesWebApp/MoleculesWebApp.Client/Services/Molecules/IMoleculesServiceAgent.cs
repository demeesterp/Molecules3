using MoleculesWebApp.Client.Data.ServiceAgents.Molecules;

namespace MoleculesWebApp.Client.Services.Molecules
{
    public interface IMoleculesServiceAgent
    {
        IObservable<CalcMolecule> Get(int id);

        IObservable<IList<CalcMolecule>> GetByName(string name);
    }
}
