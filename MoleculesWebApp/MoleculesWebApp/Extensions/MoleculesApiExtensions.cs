using MoleculesWebApp.Handlers;

namespace MoleculesWebApp.Extensions
{
    public static class MoleculesApiExtensions
    {
        public static void RegisterMoleculesEndpoints(this IEndpointRouteBuilder endpointRouteBuilder)
        {

            var apiEndPointGroup = endpointRouteBuilder.MapGroup("/api/");

            // CalcOrders endpoint
            var calcOrdersEndpoint = apiEndPointGroup.MapGroup("calcorders/");            
            
            calcOrdersEndpoint.MapGet("", CalcOrderHandler.HandleListAsync);
            calcOrdersEndpoint.MapGet("{id:int}", CalcOrderHandler.HandleGetAsync).WithName("getcalcorder");
            calcOrdersEndpoint.MapGet("name/{name}", CalcOrderHandler.HandleGetByNameAsync);
            calcOrdersEndpoint.MapPost("", CalcOrderHandler.HandleCreateAsync);
            calcOrdersEndpoint.MapPut("{id:int}", CalcOrderHandler.HandleUpdateAsync);
            calcOrdersEndpoint.MapDelete("{id:int}", () => TypedResults.Ok());

            // CalcOrderItem endpoint
            var calcOtrdersItemEndpoint = calcOrdersEndpoint.MapGroup("{calcorderid:int}/calcorderitem/");

            calcOtrdersItemEndpoint.MapPost("", () => TypedResults.Ok());
            calcOtrdersItemEndpoint.MapPut("{calcorderitemid:int}", () => TypedResults.Ok());
            calcOtrdersItemEndpoint.MapDelete("{calcorderitemid:int}", () => TypedResults.Ok());

            // Molecule endpoint
            var moleculeEndPoint = apiEndPointGroup.MapGroup("molecule/");

            moleculeEndPoint.MapGet("{moleculeid:int}", () => TypedResults.Ok());
            moleculeEndPoint.MapGet("name/{name}", () => TypedResults.Ok());
            moleculeEndPoint.MapGet("xyzfile/{moleculeid:int}", () => TypedResults.Ok());

            moleculeEndPoint.MapGet("{moleculeid:int}/atomchargereport", () => TypedResults.Ok());
            moleculeEndPoint.MapGet("{moleculeid:int}/atomorbitalreport", () => TypedResults.Ok());
            moleculeEndPoint.MapGet("{moleculeid:int}/bondsreport", () => TypedResults.Ok());
            moleculeEndPoint.MapGet("{moleculeid:int}/moleculepopulationreport", () => TypedResults.Ok());
            moleculeEndPoint.MapGet("{moleculeid:int}/generalmoleculereport", () => TypedResults.Ok());
            moleculeEndPoint.MapGet("{moleculeid:int}/moleculeatompositionreport", () => TypedResults.Ok());
        }
    }
}
