using Molecules.Core.Data.DbEntities;
using Molecules.Core.Domain.Entities;

namespace Molecules.Core.Data.Factories
{
    /// <summary>
    /// Interface for Factory Service that creates a CalcOrderItem
    /// </summary>
    public interface ICalcOrderItemFactory
    {
        /// <summary>
        /// Create a CalcOrderItem from db data
        /// </summary>
        /// <param name="dbEntity">Data comming from Db</param>
        /// <returns>CalcOrderItem entity</returns>
        public CalcOrderItem CreateCalcOrderItem(CalcOrderItemDbEntity dbEntity);

    }
}
