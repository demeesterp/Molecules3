using Microsoft.AspNetCore.Http.HttpResults;
using Molecules.Core.Domain.Entities;
using Molecules.Core.Domain.ValueObjects.Molecules;
using Molecules.Core.Services.CalcMolecules;
using Molecules.Shared.Logger;
using System.Net.Mime;
using System.Text;
using Molecules.Core.Domain.ValueObjects.Reports;
using Molecules.Core.Services.Reports;

namespace MoleculesWebApp.Handlers
{
    public static class MoleculeHandler
    {
        public static async Task<Ok<CalcMolecule>> HandleGetAsync(IMoleculesLogger logger, ICalcMoleculeService calcMoleculeService, int moleculeid)
        {
            logger.LogInformation($"Get Molecule with id {moleculeid}");
            var result = await calcMoleculeService.GetAsync(moleculeid);
            return TypedResults.Ok(result);
        }

        public static async Task<Ok<List<CalcMolecule>>> HandleFindByName(IMoleculesLogger logger, ICalcMoleculeService calcMoleculeService, string name)
        {
            logger.LogInformation($"Get Molecule with name {name}");
            var result = await calcMoleculeService.FindAllByNameAsync(name);
            return TypedResults.Ok(result);
        }

        public static async Task<FileContentHttpResult> HandleGetXyzFile(IMoleculesLogger logger, ICalcMoleculeService calcMoleculeService, int moleculeid)
        {
            logger.LogInformation($"Get Molecule XyzFile {moleculeid}");
            CalcMolecule molecule = await calcMoleculeService.GetAsync(moleculeid);
            string fileName = $"{molecule.MoleculeName}_{moleculeid}.xyz";
            string fileContent = Molecule.GetXyzFileData(molecule.Molecule);
            byte[] fileBytes = new byte[fileContent.Length];
            Encoding.UTF8.GetBytes(fileContent, 0, fileContent.Length, fileBytes, 0);
            return TypedResults.File(fileBytes, MediaTypeNames.Application.Octet, fileName);
        }

        public static async Task<Ok<List<MoleculeAtomsChargeReport>>> HandleAtomsChargeReportAsync(IMoleculesLogger logger, IMoleculeReportService moleculeReportService, int moleculeid)
        {
            logger.LogInformation($"GetMoleculeAtomsChargeReport by moleculeid:{moleculeid}");
            return TypedResults.Ok(await moleculeReportService.GetMoleculeAtomsChargeReportAsync(moleculeid));
        }

        public static async Task<Ok<List<MoleculeAtomOrbitalReport>>> HandleAtomsOrbitalReportAsync(IMoleculesLogger logger, IMoleculeReportService moleculeReportService, int moleculeid)
        {
            logger.LogInformation($"GetMoleculeAtomOrbitalReport by moleculeid:{moleculeid}");
            return TypedResults.Ok(await moleculeReportService.GetMoleculeAtomOrbitalReportAsync(moleculeid));
        }

        public static async Task<Ok<List<MoleculeBondsReport>>> HandleMoleculeBondsReportAsync(IMoleculesLogger logger, IMoleculeReportService moleculeReportService, int moleculeid)
        {
            logger.LogInformation($"MoleculeBondsReport by moleculeid:{moleculeid}");
            return TypedResults.Ok(await moleculeReportService.GetMoleculeBondsReportsAsync(moleculeid));
        }

        public static async Task<Ok<List<MoleculeAtomsPopulationReport>>> HandleMoleculeAtomsPopulationReportAsync(IMoleculesLogger logger, IMoleculeReportService moleculeReportService, int moleculeid)
        {
            logger.LogInformation($"MoleculeAtomsPopulationReport by moleculeid:{moleculeid}");
            return TypedResults.Ok(await moleculeReportService.GetMoleculePopulationReportAsync(moleculeid));
        }

        public static async Task<Ok<List<GeneralMoleculeReport>>> HandleGeneralMoleculeReportAsync(IMoleculesLogger logger, IMoleculeReportService moleculeReportService, int moleculeid)
        {
            logger.LogInformation($"GeneralMoleculeReport by moleculeid:{moleculeid}");
            return TypedResults.Ok(await moleculeReportService.GetGeneralMoleculeReportsAsync(moleculeid));
        }

        public static async Task<Ok<List<MoleculeAtomPositionReport>>> HandleMoleculeAtomPositionReportAsync(IMoleculesLogger logger, IMoleculeReportService moleculeReportService, int moleculeid)
        {
            logger.LogInformation($"MoleculeAtomPositionReport by moleculeid:{moleculeid}");
            return TypedResults.Ok(await moleculeReportService.GetAtomPositionReportAsync(moleculeid));
        }

    }
}
