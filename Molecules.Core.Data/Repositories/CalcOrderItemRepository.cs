using Microsoft.EntityFrameworkCore;
using Molecules.Core.Data.DbEntities;
using Molecules.Core.Data.Factories;
using Molecules.Core.Domain.Entities;
using Molecules.Core.Repositories;
using Molecules.Shared.Exceptions;

namespace Molecules.Core.Data.Repositories
{
    public class CalcOrderItemRepository(MoleculesDbContext context, ICalcOrderItemFactory calcOrderItemFactory) : ICalcOrderItemRepository
    {
        private readonly MoleculesDbContext _context = context;

        private readonly ICalcOrderItemFactory _calcOrderItemFactory = calcOrderItemFactory;

        public async Task<CalcOrderItem> CreateAsync(int calcOrderId, int charge, string moleculeName, string calcType, string basisSetCode, string xyz)
        {
            var order = _context.CalcOrders.Find(calcOrderId);
            if (order != null)
            {
               var result = await _context.CalcOrderItems.AddAsync(new CalcOrderItemDbEntity()
               {
                  CalcOrder = order,
                  BasissetCode = basisSetCode,
                  CalcType = calcType,
                  Charge = charge,
                  MoleculeName = moleculeName,
                  XYZ = xyz
               });
               await _context.SaveChangesAsync();
               return _calcOrderItemFactory.CreateCalcOrderItem(result.Entity);
            }
            else
            {
                throw new DbResourceNotFoundException($"Resource {nameof(CalcOrderDbEntity)} with Id {calcOrderId} was not found");
            }
        }

        public async Task DeleteAsync(int id)
        {
            var result = await _context.CalcOrderItems.FindAsync(id);
            if (result != null)
            {
                _context.CalcOrderItems.Remove(result);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new DbResourceNotFoundException($"Resource {nameof(CalcOrderItemDbEntity)} with Id {id} was not found");
            }
        }

        public async Task<CalcOrderItem> UpdateAsync(int id, int charge, string moleculeName, string calcType, string basisSetCode, string xyz)
        {
            var result = await _context.CalcOrderItems.Include(oi => oi.CalcOrder).FirstOrDefaultAsync(oi => oi.Id == id);
            if (result != null)
            {
                result.MoleculeName = moleculeName;
                result.Charge = charge;
                result.CalcType = calcType;
                result.BasissetCode = basisSetCode;
                result.XYZ = xyz;
                
                await _context.SaveChangesAsync();
                
                return _calcOrderItemFactory.CreateCalcOrderItem(result);
            }
            else
            {
                throw new DbResourceNotFoundException($"Resource {nameof(CalcOrderItemDbEntity)} with Id {id} was not found");
            }
        }
    }
}
