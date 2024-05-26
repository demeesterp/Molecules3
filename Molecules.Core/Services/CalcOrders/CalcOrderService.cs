using Molecules.Core.Domain.Aggregates;
using Molecules.Core.Domain.ValueObjects.Calc.Order.Info;
using Molecules.Core.Repositories;
using Molecules.Core.Services.CalcOrders.Validation;
using Molecules.Shared.Logger;

namespace Molecules.Core.Services.CalcOrders
{
    /// <inheritdoc/>
    /// <summary>
    /// Construct the CalcOrderService
    /// </summary>
    /// <param name="validations">The validation service helper</param>
    /// <param name="logger">The logger</param>
    public class CalcOrderService(ICalcOrderServiceValidations validations,
                                ICalcOrderRepository calcOrderRepository,
                                IMoleculesLogger logger) : ICalcOrderService
    {
        #region dependencies

        private readonly IMoleculesLogger _logger = logger ?? throw new ArgumentNullException(nameof(logger));

        private readonly ICalcOrderServiceValidations _validations = validations ?? throw new ArgumentNullException(nameof(validations));

        private readonly ICalcOrderRepository _calcOrderRepository = calcOrderRepository ?? throw new ArgumentNullException(nameof(calcOrderRepository));

        #endregion

        /// <inheritdoc/>
        public async Task<CalcOrder> CreateAsync(CreateInfoCalcOrder createCalcOrder)
        {
            ArgumentNullException.ThrowIfNull(createCalcOrder);
            _logger.LogInformation($"Create a new CalcOrder called with name " +
                                    $"{createCalcOrder.Name} and description {createCalcOrder.Description}");
            _validations.Validate(createCalcOrder);
            var result = await _calcOrderRepository.CreateAsync(createCalcOrder.Name, createCalcOrder.Description);
            await _calcOrderRepository.SaveChangesAsync();
            return result;
        }

        /// <inheritdoc/>
        public async Task<CalcOrder?> UpdateAsync(int id, UpdateInfoCalcOrder updateCalcOrder)
        {
            ArgumentNullException.ThrowIfNull(updateCalcOrder);

            _logger.LogInformation($"Update CalcOrder with id: {id} set Name: {updateCalcOrder.Name} and set Description: {updateCalcOrder.Description}");
            _validations.Validate(updateCalcOrder);
            var result = await _calcOrderRepository.UpdateAsync(id, updateCalcOrder.Name, updateCalcOrder.Description);
            await _calcOrderRepository.SaveChangesAsync();
            return result;
        }

        /// <inheritdoc/>
        public async Task DeleteAsync(int id)
        {
            _logger.LogInformation($"DeleteAsync with id {id}");

            await _calcOrderRepository.DeleteAsync(id);

            await _calcOrderRepository.SaveChangesAsync();
        }

        /// <inheritdoc/>
        public async Task<CalcOrder> GetAsync(int id)
        {
            _logger.LogInformation($"GetAsync with id {id}");

            var result = await _calcOrderRepository.GetByIdAsync(id);

            return result;
        }

        /// <inheritdoc/>
        public async Task<List<CalcOrder>> GetByNameAsync(string name)
        {
            _logger.LogInformation($"GetByNameAsync with name {name}");

            var result = await _calcOrderRepository.GetByNameAsync(name);

            return result;
        }

        /// <inheritdoc/>
        public async Task<List<CalcOrder>> ListAsync()
        {
            _logger.LogInformation("ListAsync");

            var result = await _calcOrderRepository.GetAllAsync();

            return result;
        }


    }
}
