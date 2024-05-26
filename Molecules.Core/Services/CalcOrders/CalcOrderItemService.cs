using Molecules.Core.Domain.Entities;
using Molecules.Core.Domain.ValueObjects.Calc.Order.Info;
using Molecules.Core.Repositories;
using Molecules.Core.Services.CalcOrders.Validation;
using Molecules.Shared.Logger;

namespace Molecules.Core.Services.CalcOrders
{
    public class CalcOrderItemService(ICalcOrderItemRepository calcOrderItemRepository,
                                            ICalcOrderItemServiceValidations calcOrderItemServiceValidations,
                                                IMoleculesLogger logger) : ICalcOrderItemService
    {
        #region dependencies

        
        private readonly ICalcOrderItemRepository           _calcOrderItemRepository = calcOrderItemRepository ?? throw new ArgumentNullException(nameof(calcOrderItemRepository));

        private readonly ICalcOrderItemServiceValidations   _calcOrderItemServiceValidations = calcOrderItemServiceValidations ?? throw new ArgumentNullException(nameof(calcOrderItemServiceValidations));

        private readonly IMoleculesLogger                   _logger = logger ?? throw new ArgumentNullException(nameof(logger));

        #endregion

        // <inheritdoc />
        public async Task<CalcOrderItem> CreateAsync(int calcOrderId, CreateInfoCalcOrderItem createCalcOrderItem)
        {
            _logger.LogInformation($"CreateAsync for calcOrderId {calcOrderId} and molecule {createCalcOrderItem.MoleculeName} with details {createCalcOrderItem.Details} was called ");

            _calcOrderItemServiceValidations.Validate(createCalcOrderItem);

            var result = await _calcOrderItemRepository.CreateAsync(calcOrderId,
                                             moleculeName: createCalcOrderItem.MoleculeName,
                                                xyz: createCalcOrderItem.Details.Xyz,
                                                    charge: createCalcOrderItem.Details.Charge,
                                                        calcType: createCalcOrderItem.Details.Type.ToString(),
                                                            basisSetCode:createCalcOrderItem.Details.CalcBasisSetCode.ToString());



            return result;
        }

        // <inheritdoc />
        public async Task<CalcOrderItem> UpdateAsync(int id, UpdateInfoCalcOrderItem updateCalcOrderItem)
        {
            _logger.LogInformation($"UpdateAsync with id {id}");

            _calcOrderItemServiceValidations.Validate(updateCalcOrderItem);

            var result = await _calcOrderItemRepository.UpdateAsync(id, updateCalcOrderItem.Details.Charge,
                                                updateCalcOrderItem.MoleculeName, updateCalcOrderItem.Details.Type.ToString(),
                                                updateCalcOrderItem.Details.CalcBasisSetCode.ToString(), updateCalcOrderItem.Details.Xyz);


            return result;
        }

        // <inheritdoc />
        public async Task DeleteAsync(int id)
        {
            _logger.LogInformation($"DeleteAsync with id {id}");

            await _calcOrderItemRepository.DeleteAsync(id);
        }


    }
}
