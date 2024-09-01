using Microsoft.AspNetCore.Http.HttpResults;
using Molecules.Core.Domain.Aggregates;
using Molecules.Core.Domain.Entities;
using Molecules.Core.Domain.ValueObjects.Reports;
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
                                                        .Produces<List<CalcMolecule>>(StatusCodes.Status200OK)
                                                        .Produces<ServiceError>(StatusCodes.Status500InternalServerError);
            moleculeEndPoint.MapGet("name/{name}", MoleculeHandler.HandleFindByName).WithOpenApi()
                                                        .Produces<List<CalcMolecule>>(StatusCodes.Status200OK)
                                                        .Produces<ServiceError>(StatusCodes.Status500InternalServerError);
            moleculeEndPoint.MapGet("xyzfile/{moleculeid:int}", MoleculeHandler.HandleGetXyzFile).WithOpenApi()
                                                        .Produces<FileContentHttpResult>(StatusCodes.Status200OK)
                                                        .Produces<ServiceError>(StatusCodes.Status500InternalServerError);

            moleculeEndPoint.MapGet("{moleculeid:int}/atomchargereport", MoleculeHandler.HandleAtomsChargeReportAsync).WithOpenApi()
                                                        .Produces<List<MoleculeAtomsChargeReport>>(StatusCodes.Status200OK)
                                                        .Produces<ServiceError>(StatusCodes.Status500InternalServerError);
            moleculeEndPoint.MapGet("{moleculeid:int}/atomorbitalreport", MoleculeHandler.HandleAtomsOrbitalReportAsync).WithOpenApi()
                                                        .Produces<List<MoleculeAtomOrbitalReport>>(StatusCodes.Status200OK)
                                                        .Produces<ServiceError>(StatusCodes.Status500InternalServerError);
            moleculeEndPoint.MapGet("{moleculeid:int}/bondsreport", MoleculeHandler.HandleMoleculeBondsReportAsync).WithOpenApi()
                                                        .Produces<List<MoleculeBondsReport>>(StatusCodes.Status200OK)
                                                        .Produces<ServiceError>(StatusCodes.Status500InternalServerError);
            moleculeEndPoint.MapGet("{moleculeid:int}/moleculepopulationreport", MoleculeHandler.HandleMoleculeAtomsPopulationReportAsync).WithOpenApi()
                                                        .Produces<List<MoleculeAtomsPopulationReport>>(StatusCodes.Status200OK)
                                                        .Produces<ServiceError>(StatusCodes.Status500InternalServerError);
            moleculeEndPoint.MapGet("{moleculeid:int}/generalmoleculereport", MoleculeHandler.HandleGeneralMoleculeReportAsync).WithOpenApi()
                                                        .Produces<List<GeneralMoleculeReport>>(StatusCodes.Status200OK)
                                                        .Produces<ServiceError>(StatusCodes.Status500InternalServerError);
            moleculeEndPoint.MapGet("{moleculeid:int}/moleculeatompositionreport", MoleculeHandler.HandleMoleculeAtomPositionReportAsync).WithOpenApi()
                                                        .Produces<List<MoleculeAtomPositionReport>>(StatusCodes.Status200OK)
                                                        .Produces<ServiceError>(StatusCodes.Status500InternalServerError);


        }
    }
}
