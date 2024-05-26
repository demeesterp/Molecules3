using Molecules.Core.Data.DbEntities;
using Molecules.Core.Domain.Entities;
using Molecules.Core.Domain.ValueObjects.Molecules;
using Molecules.Shared;

namespace Molecules.Core.Data.Factories
{
    public class CalcMoleculeFactory : ICalcMoleculeFactory
    {
        public CalcMolecule BuildMolecule(MoleculeDbEntity moleculeDbEntity)
        {
            CalcMolecule retval = new(moleculeDbEntity.Id,
                                    moleculeDbEntity.OrderName,
                                        moleculeDbEntity.BasisSet,
                                            moleculeDbEntity.MoleculeName)
            {
                Molecule = StringConversion.FromJsonString<Molecule>(moleculeDbEntity.Molecule)
            };
            return retval;
        }

        public CalcMolecule BuildMolecule(MoleculeNameInfoDbEntity moleculeNameInfoDb)
        {
            return new CalcMolecule(moleculeNameInfoDb.Id,
                                    moleculeNameInfoDb.OrderName,
                                        moleculeNameInfoDb.BasisSet,
                                            moleculeNameInfoDb.MoleculeName);
        }
    }
}
