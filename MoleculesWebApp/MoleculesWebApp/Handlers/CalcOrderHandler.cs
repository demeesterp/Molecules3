using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Molecules.Core.Domain.Aggregates;
using Molecules.Core.Domain.ValueObjects.Calc.Order.Info;
using Molecules.Core.Services.CalcOrders;
using Molecules.Shared.Logger;

namespace MoleculesWebApp.Handlers
{
    public static class CalcOrderHandler
    {
        public static async Task<Ok<List<CalcOrder>>> HandleListAsync(IMoleculesLogger logger,
            ICalcOrderService calcOrderService)
        {
            logger.LogInformation("Get the list of all Calculation Orders called");
            return TypedResults.Ok(await calcOrderService.ListAsync());
        }

        public static async Task<Ok<CalcOrder>> HandleGetAsync(IMoleculesLogger logger, ICalcOrderService calcOrderService, int id)
        {
            logger.LogInformation($"Get a calculation order by id:{id}");
            var result = await calcOrderService.GetAsync(id);
            return TypedResults.Ok(result);
        }

        public static async Task<Ok<List<CalcOrder>>> HandleGetByNameAsync(IMoleculesLogger logger, ICalcOrderService calcOrderService, string name)
        {
            logger.LogInformation($"Get a calculation order by name:{name}");
            return TypedResults.Ok(await calcOrderService.GetByNameAsync(name));
        }

        public static async Task<CreatedAtRoute<CalcOrder>> HandleCreateAsync(IMoleculesLogger logger, ICalcOrderService calcOrderService, CreateInfoCalcOrder createInfoCalcOrder)
        {
            logger.LogInformation($"Create a calculation order with name:{createInfoCalcOrder.Name} and description:{createInfoCalcOrder.Description}");
            var createdCalcOrder = await calcOrderService.CreateAsync(createInfoCalcOrder);
            return TypedResults.CreatedAtRoute(createdCalcOrder, "getcalcorder", new { id = createdCalcOrder.Id });
        }

        public static async Task<Ok<CalcOrder>> HandleUpdateAsync(IMoleculesLogger logger, ICalcOrderService calcOrderService, int id, UpdateInfoCalcOrder updateInfoCalcOrder)
        {
            logger.LogInformation($"Update a calculation order with name:{updateInfoCalcOrder.Name} and description:{updateInfoCalcOrder.Description}");
            var updatedCalcOrder = await calcOrderService.UpdateAsync(id, updateInfoCalcOrder);
            return TypedResults.Ok(updatedCalcOrder);
        }

        public static async Task<NoContent> HandleDeleteAsync(IMoleculesLogger logger, ICalcOrderService calcOrderService, int id)
        {
           logger.LogInformation($"Delete a calculation order with id:{id}");
           await calcOrderService.DeleteAsync(id);
           return TypedResults.NoContent();
        }

        //app.MapGet("/example", () => Results.Ok(new { Message = "Hello, World!" }))
        //.ProducesResponseType(typeof(MyResponseType), StatusCodes.Status200OK)
        //.ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError);



}
}
