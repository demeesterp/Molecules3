using Microsoft.EntityFrameworkCore;
using Molecules.Core.Data.DbEntities;
using Molecules.Core.Data.Factories;
using Molecules.Core.Domain.Aggregates;
using Molecules.Core.Repositories;
using Molecules.Shared.Exceptions;

namespace Molecules.Core.Data.Repositories
{
    public class CalcOrderRepository(MoleculesDbContext context, ICalcOrderFactory calcOrderFactory) : ICalcOrderRepository
    {
        private readonly MoleculesDbContext _context = context;

        private readonly ICalcOrderFactory _calcOrderFactory = calcOrderFactory;

        public async Task<CalcOrder> CreateAsync(string name, string description)
        {
            var result = await _context.CalcOrders.AddAsync(new CalcOrderDbEntity()
            {
                Name = name,
                Description = description
            });
            await _context.SaveChangesAsync();
            return _calcOrderFactory.CreateCalcOrder(result.Entity);
        }

        public async Task DeleteAsync(int id)
        {
            var result = await _context.CalcOrders.Include(o => o.CalcOrderItems).FirstOrDefaultAsync(o => o.Id == id);
            if (result != null)
            {
                _context.CalcOrders.Remove(result);
            }
            else
            {
                throw new DbResourceNotFoundException($"Resource {nameof(CalcOrderDbEntity)} with Id {id} was not found");
            }
        }

        public async Task<CalcOrder> UpdateAsync(int id, string name, string description)
        {
            var result = await _context.CalcOrders.Include(o => o.CalcOrderItems).FirstOrDefaultAsync(o => o.Id == id);
            if (result != null)
            {
                result.Name = name;
                result.Description = description;
                await _context.SaveChangesAsync();
                return _calcOrderFactory.CreateCalcOrder(result);
            }
            else
            {
                throw new DbResourceNotFoundException($"Resource {nameof(CalcOrderDbEntity)} with Id {id} was not found");
            }
        }

        public async Task<List<CalcOrder>> GetAllAsync()
        {
            return await (from i in _context.CalcOrders.Include(o => o.CalcOrderItems) 
                                        select _calcOrderFactory.CreateCalcOrder(i)).ToListAsync();
            
        }

        public async Task<CalcOrder> GetByIdAsync(int id)
        {
            var result = await _context.CalcOrders.Include(o => o.CalcOrderItems).FirstOrDefaultAsync(i => i.Id == id);
            if (result != null)
            {
                return _calcOrderFactory.CreateCalcOrder(result);
            }
            else
            {
                throw new DbResourceNotFoundException($"Resource {nameof(CalcOrderDbEntity)} with Id {id} was not found");
            }
        }

        public async Task<List<CalcOrder>> GetByNameAsync(string name)
        {
            return await _context.CalcOrders
                            .Include(o => o.CalcOrderItems)
                            .Where(i => i.Name == name)
                            .Select(i => _calcOrderFactory.CreateCalcOrder(i))
                            .ToListAsync();

        }


    }
}
