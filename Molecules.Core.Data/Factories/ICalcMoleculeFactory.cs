using Molecules.Core.Data.DbEntities;
using Molecules.Core.Domain.Entities;

namespace Molecules.Core.Data.Factories
{
    public interface ICalcMoleculeFactory
    {
        CalcMolecule BuildMolecule(MoleculeDbEntity moleculeDbEntity);

        CalcMolecule BuildMolecule(MoleculeNameInfoDbEntity moleculeNameInfoDb);
    }
}
