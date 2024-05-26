using Molecules.Core.Domain.ValueObjects.Molecules;

namespace Molecules.Core.Factories.Molecules
{
    public interface IMoleculeFromGmsFactory
    {
        bool TryCompleteMolecule(string fileName, List<string> fileLines, Molecule? molecule);

    }
}
