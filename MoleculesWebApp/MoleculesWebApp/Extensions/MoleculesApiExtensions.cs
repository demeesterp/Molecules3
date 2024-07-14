using Molecules.Core.Domain.Aggregates;
using MoleculesWebApp.Handlers;
using MoleculesWebApp.Handlers.Model;

namespace MoleculesWebApp.Extensions
{
    public static class MoleculesApiExtensions
    {
        public static void RegisterMoleculesEndpoints(this IEndpointRouteBuilder endpointRouteBuilder)
        {

            var apiEndPointGroup = endpointRouteBuilder.MapGroup("/api/");

            // CalcOrders endpoint
            var calcOrdersEndpoint = apiEndPointGroup.MapGroup("calcorders/");

            calcOrdersEndpoint.MapGet("", CalcOrderHandler.HandleListAsync).WithOpenApi()
                                                        .Produces<List<CalcOrder>>(StatusCodes.Status200OK)
                                                        .Produces<ServiceError>(StatusCodes.Status500InternalServerError);
            
            calcOrdersEndpoint.MapGet("{id:int}", CalcOrderHandler.HandleGetAsync).WithName("getcalcorder").WithOpenApi()
                                                        .Produces<List<CalcOrder>>(StatusCodes.Status200OK)
                                                        .Produces<ServiceError>(StatusCodes.Status500InternalServerError);
            
            calcOrdersEndpoint.MapGet("name/{name}", CalcOrderHandler.HandleGetByNameAsync).WithOpenApi()
                                                        .Produces<List<CalcOrder>>(StatusCodes.Status200OK)
                                                        .Produces<ServiceError>(StatusCodes.Status500InternalServerError);
            
            calcOrdersEndpoint.MapPost("", CalcOrderHandler.HandleCreateAsync).WithOpenApi()
                                                        .Produces<List<CalcOrder>>(StatusCodes.Status201Created)
                                                        .Produces<ServiceValidationError>(StatusCodes.Status422UnprocessableEntity)
                                                        .Produces<ServiceError>(StatusCodes.Status500InternalServerError);
            
            calcOrdersEndpoint.MapPut("{id:int}", CalcOrderHandler.HandleUpdateAsync).WithOpenApi()
                                                        .Produces<List<CalcOrder>>(StatusCodes.Status200OK)
                                                        .Produces<ServiceError>(StatusCodes.Status404NotFound)
                                                        .Produces<ServiceValidationError>(StatusCodes.Status422UnprocessableEntity)
                                                        .Produces<ServiceError>(StatusCodes.Status500InternalServerError);
            
            calcOrdersEndpoint.MapDelete("{id:int}", CalcOrderHandler.HandleDeleteAsync).WithOpenApi()
                                                        .Produces<List<CalcOrder>>(StatusCodes.Status204NoContent)
                                                        .Produces<ServiceError>(StatusCodes.Status404NotFound)
                                                        .Produces<ServiceError>(StatusCodes.Status500InternalServerError);

            // CalcOrderItem endpoint
            var calcOtrdersItemEndpoint = calcOrdersEndpoint.MapGroup("{calcorderid:int}/calcorderitem/");

            calcOtrdersItemEndpoint.MapPost("", CalcOrderItemHandler.HandleCreateAsync).WithOpenApi()
                                                        .Produces<List<CalcOrder>>(StatusCodes.Status201Created)
                                                        .Produces<ServiceValidationError>(StatusCodes.Status422UnprocessableEntity)
                                                        .Produces<ServiceError>(StatusCodes.Status500InternalServerError);
            
            calcOtrdersItemEndpoint.MapPut("{calcorderitemid:int}", CalcOrderItemHandler.HandleUpdateAsync).WithOpenApi()
                                                        .Produces<List<CalcOrder>>(StatusCodes.Status200OK)
                                                        .Produces<ServiceValidationError>(StatusCodes.Status422UnprocessableEntity)
                                                        .Produces<ServiceError>(StatusCodes.Status404NotFound)
                                                        .Produces<ServiceError>(StatusCodes.Status500InternalServerError);
            
            calcOtrdersItemEndpoint.MapDelete("{calcorderitemid:int}", CalcOrderItemHandler.HandleDeleteAsync).WithOpenApi()
                                                        .Produces<List<CalcOrder>>(StatusCodes.Status200OK)
                                                        .Produces<ServiceError>(StatusCodes.Status404NotFound)
                                                        .Produces<ServiceError>(StatusCodes.Status500InternalServerError);

            // Molecule endpoint
            var moleculeEndPoint = apiEndPointGroup.MapGroup("molecule/");

            moleculeEndPoint.MapGet("{moleculeid:int}", MoleculeHandler.HandleGetAsync).WithOpenApi()
                                                        .Produces<List<CalcOrder>>(StatusCodes.Status200OK)
                                                        .Produces<ServiceError>(StatusCodes.Status500InternalServerError);
            moleculeEndPoint.MapGet("name/{name}", () => MoleculeHandler.HandleGetByName).WithOpenApi()
                                                        .Produces<List<CalcOrder>>(StatusCodes.Status200OK)
                                                        .Produces<ServiceError>(StatusCodes.Status500InternalServerError);
            moleculeEndPoint.MapGet("xyzfile/{moleculeid:int}", MoleculeHandler.HandleGetXyzFile).WithOpenApi()
                                                        .Produces<List<CalcOrder>>(StatusCodes.Status200OK)
                                                        .Produces<ServiceError>(StatusCodes.Status500InternalServerError);

            moleculeEndPoint.MapGet("{moleculeid:int}/atomchargereport", MoleculeHandler.HandleAtomsChargeReportAsync).WithOpenApi()
                                                        .Produces<List<CalcOrder>>(StatusCodes.Status200OK)
                                                        .Produces<ServiceError>(StatusCodes.Status500InternalServerError);
            moleculeEndPoint.MapGet("{moleculeid:int}/atomorbitalreport", MoleculeHandler.HandleAtomsOrbitalReportAsync).WithOpenApi()
                                                        .Produces<List<CalcOrder>>(StatusCodes.Status200OK)
                                                        .Produces<ServiceError>(StatusCodes.Status500InternalServerError);
            moleculeEndPoint.MapGet("{moleculeid:int}/bondsreport", MoleculeHandler.HandleMoleculeBondsReportAsync).WithOpenApi()
                                                        .Produces<List<CalcOrder>>(StatusCodes.Status200OK)
                                                        .Produces<ServiceError>(StatusCodes.Status500InternalServerError);
            moleculeEndPoint.MapGet("{moleculeid:int}/moleculepopulationreport", MoleculeHandler.HandleMoleculeAtomsPopulationReportAsync).WithOpenApi()
                                                        .Produces<List<CalcOrder>>(StatusCodes.Status200OK)
                                                        .Produces<ServiceError>(StatusCodes.Status500InternalServerError);
            moleculeEndPoint.MapGet("{moleculeid:int}/generalmoleculereport", MoleculeHandler.HandleGeneralMoleculeReportAsync).WithOpenApi()
                                                        .Produces<List<CalcOrder>>(StatusCodes.Status200OK)
                                                        .Produces<ServiceError>(StatusCodes.Status500InternalServerError);
            moleculeEndPoint.MapGet("{moleculeid:int}/moleculeatompositionreport", MoleculeHandler.HandleMoleculeAtomPositionReportAsync).WithOpenApi()
                                                        .Produces<List<CalcOrder>>(StatusCodes.Status200OK)
                                                        .Produces<ServiceError>(StatusCodes.Status500InternalServerError);


        }
    }
}
