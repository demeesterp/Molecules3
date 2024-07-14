using Microsoft.AspNetCore.Http.HttpResults;
using Molecules.Core.Domain.Entities;
using Molecules.Core.Domain.ValueObjects.Calc.Order.Info;
using Molecules.Core.Services.CalcOrders;
using Molecules.Shared.Logger;

namespace MoleculesWebApp.Handlers
{
    public static class CalcOrderItemHandler
    {
        public static async Task<Ok<CalcOrderItem>>  HandleCreateAsync(IMoleculesLogger logger, ICalcOrderItemService calcOrderItemService,
                                        int calcorderid, CreateInfoCalcOrderItem calcOrderItem)
        {
            logger.LogInformation("Create a CalcOrder Item");
            var result = await calcOrderItemService.CreateAsync(calcorderid, calcOrderItem);
            return TypedResults.Ok(result);
        }

        public static async Task<Ok<CalcOrderItem>>  HandleUpdateAsync(IMoleculesLogger logger, ICalcOrderItemService calcOrderItemService,
                                        int calcorderid, int calcorderitemid, UpdateInfoCalcOrderItem calcOrderItem )
        {
            logger.LogInformation("Update a CalcOrder Item");
            var result = await calcOrderItemService.UpdateAsync(calcorderitemid, calcOrderItem);
            return TypedResults.Ok(result);
        }

        public static async Task<NoContent>  HandleDeleteAsync(IMoleculesLogger logger, ICalcOrderItemService calcOrderItemService, int calcorderid, int calcorderitemid)
        {
            logger.LogInformation("Deleter a CalcOrder Item");
            await calcOrderItemService.DeleteAsync(calcorderitemid);
            return TypedResults.NoContent();

        }
    }
}
