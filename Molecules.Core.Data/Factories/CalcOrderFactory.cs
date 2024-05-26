using Molecules.Core.Data.DbEntities;
using Molecules.Core.Domain.Aggregates;
using Molecules.Core.Domain.ValueObjects.Calc.Order;

namespace Molecules.Core.Data.Factories
{
    /// <summary>
    /// Factory service to create CalcOrder entity
    /// </summary>
    /// <remarks>
    /// Constrcutor
    /// </remarks>
    /// <param name="calcOrderItemFactory">Factory for the items</param>
    public class CalcOrderFactory(ICalcOrderItemFactory calcOrderItemFactory) : ICalcOrderFactory
    {

        #region dependencies

        private readonly ICalcOrderItemFactory _calcOrderItemFactory = calcOrderItemFactory;

        #endregion

        /// <summary>
        /// Create a new CalcOrder entity from a CalcOrderDbEntity
        /// </summary>
        /// <param name="dbEntity">Data comming from DB</param>
        /// <returns>Completd calcorder</returns>
        public CalcOrder CreateCalcOrder(CalcOrderDbEntity dbEntity)
        {
            CalcOrder retval = new(dbEntity.Id, 
                                     new CalcOrderDetails(dbEntity.Name, dbEntity.Description),
                                       dbEntity.CalcOrderItems.Select(_calcOrderItemFactory.CreateCalcOrderItem).ToArray());
            return retval;
        }
    }
}
