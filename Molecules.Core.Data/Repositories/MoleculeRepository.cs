using Microsoft.EntityFrameworkCore;
using Molecules.Core.Data.DbEntities;
using Molecules.Core.Data.Factories;
using Molecules.Core.Domain.Entities;
using Molecules.Core.Repositories;
using Molecules.Shared;
using Molecules.Shared.Exceptions;

namespace Molecules.Core.Data.Repositories
{
    public class MoleculeRepository(MoleculesDbContext context, ICalcMoleculeFactory calcMoleculeFactory) : IMoleculeRepository
    {
        private readonly MoleculesDbContext _context = context;

        private readonly ICalcMoleculeFactory _calcMoleculeFactory = calcMoleculeFactory;

        public async Task<CalcMolecule> CreateAsync(CalcMolecule entity)
        {
            var result = await _context.Molecule.AddAsync(new MoleculeDbEntity()
            {
                BasisSet = entity.BasisSet,
                MoleculeName = entity.MoleculeName,
                OrderName = entity.OrderName,
                Molecule = StringConversion.ToJsonString(entity.Molecule)
            });

            await _context.SaveChangesAsync();

            return _calcMoleculeFactory.BuildMolecule(result.Entity);
        }

        public async Task DeleteAsync(int id)
        {
            var molecule = await _context.Molecule.FindAsync(id);
            if (molecule != null)
            {
                _context.Molecule.Remove(molecule);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new DbResourceNotFoundException($"Resource {nameof(MoleculeDbEntity)} with Id {id} was not found");
            }
        }

        public async Task<CalcMolecule> GetByIdAsync(int id)
        {
            var result = await _context.Molecule.FirstOrDefaultAsync(i => i.Id == id);
            if (result != null)
            {
                return _calcMoleculeFactory.BuildMolecule(result);
            }
            else
            {
                throw new DbResourceNotFoundException($"Resource {nameof(MoleculeDbEntity)} with Id {id} was not found");
            }
        }

        public async Task<CalcMolecule?> FindAsync(string orderName, string basisSet, string moleculeName)
        {
            return await _context.Molecule.Select(m => _calcMoleculeFactory.BuildMolecule(m))
                                                            .FirstOrDefaultAsync(i => i.OrderName == orderName
                                                                        && i.BasisSet == basisSet
                                                                            && i.MoleculeName == moleculeName);
        }

        public async Task<List<CalcMolecule>> FindAllByNameAsync(string moleculeName)
        {
            return await (from mol in _context.Molecule where
                              EF.Functions.Like(mol.MoleculeName.ToLower(), $"{moleculeName.ToLower()}")
                          select new CalcMolecule(mol.Id, mol.OrderName, mol.BasisSet, mol.MoleculeName))
                                 .AsNoTracking()
                                 .ToListAsync();
        }

        public async Task<CalcMolecule> UpdateAsync(int id, string moleculeName, string molecule)
        {
            var result = await _context.Molecule.FindAsync(id);
            if (result != null)
            {
                result.MoleculeName = moleculeName;
                result.Molecule = molecule;
                
                await _context.SaveChangesAsync();
                
                return  _calcMoleculeFactory.BuildMolecule(result);
            }
            else
            {
                throw new DbResourceNotFoundException($"Resource {nameof(MoleculeDbEntity)} with Id {id} was not found");
            }
        }


    }
}
