using Molecules.Core.Domain.Aggregates;
using Molecules.Core.Domain.Entities;
using Molecules.Core.Domain.ValueObjects.Calc.Order;
using Molecules.Core.Domain.ValueObjects.Calc;
using Molecules.Core.Domain.ValueObjects.GmsCalc;
using Molecules.Core.Domain.ValueObjects.Molecules;
using Molecules.Core.Factories.CalcInput;
using Molecules.Core.Services.CalcMolecules;
using Molecules.Core.Services.CalcOrders;
using Molecules.Shared.Logger;
using Molecules.Core.Factories.Molecules;

namespace Molecules.Core.Services.CalcDelivery
{
    public class CalcDeliveryService : ICalcDeliveryService
    {
        private readonly ICalcOrderService _calcOrderService;

        private readonly IGmsCalcInputFactory _calcDeliveryInputFactory;

        private readonly IMoleculeFromGmsFactory _gmsMoleculeFactory;

        private readonly ICalcMoleculeService _calcMoleculeService;

        private readonly IMoleculesLogger _logger;

        public CalcDeliveryService(ICalcOrderService calcOrderService,
                                    ICalcMoleculeService calcMoleculeService,
                                        IGmsCalcInputFactory calcDeliveryFactory,
                                        IMoleculeFromGmsFactory gmsMoleculeFactory,
                                            IMoleculesLogger logger)
        {
            _calcOrderService = calcOrderService;
            _calcDeliveryInputFactory = calcDeliveryFactory;
            _calcMoleculeService = calcMoleculeService;
            _calcMoleculeService = calcMoleculeService;
            _gmsMoleculeFactory = gmsMoleculeFactory;
            _logger = logger;
        }

        public async Task ExportCalcDeliveryInputAsync(string basePath)
        {
            _logger.LogInformation($"ExportCalcDeliveryInputAsync basePath {basePath}");
            List<CalcOrder> orders = await _calcOrderService.ListAsync();
            _calcDeliveryInputFactory.BuildCalcInput(orders).ForEach(async (calcInput) =>
            {
                await File.WriteAllTextAsync(Path.Combine(basePath, "Calculations", $"{calcInput.DisplayName}.inp"), calcInput.Content);
            });
        }

        public async Task ImportCalcDeliveryOutputAsync(string basePath)
        {
            _logger.LogInformation($"ImportCalcDeliveryOutputAsync basePath {basePath}");
            List<CalcOrder> orders = await _calcOrderService.ListAsync();
            foreach (var order in orders)
            {
                foreach (var orderItem in order.Items)
                {
                    string basisSetName = CalcBasisSetTable.GetCalcBasisSetDisplayName(orderItem.Details.CalcBasisSetCode);

                    CalcMolecule? molecule =
                            await _calcMoleculeService.FindAsync(order.Details.Name,
                                                                         basisSetName,
                                                                            orderItem.MoleculeName);

                    if (molecule == null || molecule.Molecule == null)
                    {
                        molecule = await _calcMoleculeService.CreateAsync(new CalcMolecule(order.Details.Name, basisSetName, orderItem.MoleculeName)
                        {
                            Molecule = new Molecule(orderItem.Details) // Init with XYZ file
                            {
                                Name = orderItem.MoleculeName
                            }
                        });
                    }

                    if (orderItem.Details.Type == CalcOrderItemType.GeoOpt)
                    {

                        // Search for a geoOptFile
                        string geoOptFile = Path.Combine(basePath, "Calculations",
                                                OutFileDisplayName(order.Details.Name, orderItem.Id,
                                                                        molecule.MoleculeName, GmsCalculationKind.GeometryOptimization));
                        if (File.Exists(geoOptFile))
                        {
                            if (_gmsMoleculeFactory.TryCompleteMolecule(geoOptFile, [.. File.ReadAllLines(geoOptFile)], molecule.Molecule))
                            {
                                File.WriteAllText(Path.Combine(basePath, "Calculations", molecule.MoleculeName), Molecule.GetXyzFileData(molecule.Molecule));
                            };

                        }
                    }
                    else
                    {
                        // Parse GeoDiskCharge
                        string geoDiskFile = Path.Combine(basePath, "Calculations",
                                                            OutFileDisplayName(order.Details.Name, orderItem.Id,
                                                                molecule.MoleculeName, GmsCalculationKind.GeoDiskCharge));

                        if (File.Exists(geoDiskFile))
                        {
                            _gmsMoleculeFactory.TryCompleteMolecule(geoDiskFile, [.. File.ReadAllLines(geoDiskFile)], molecule.Molecule);
                        }

                        // Parse CHelpGCharge
                        string CHelpGChargeFile = Path.Combine(basePath, "Calculations",
                                                                                   OutFileDisplayName(order.Details.Name, orderItem.Id,
                                                                                     molecule.MoleculeName, GmsCalculationKind.CHelpGCharge));
                        if (File.Exists(CHelpGChargeFile))
                        {
                            _gmsMoleculeFactory.TryCompleteMolecule(CHelpGChargeFile, [.. File.ReadAllLines(CHelpGChargeFile)], molecule.Molecule);
                        }

                        string fukuiNeutralFile = Path.Combine(basePath, "Calculations",
                                                                 OutFileDisplayName(order.Details.Name, orderItem.Id,
                                                                                      molecule.MoleculeName, GmsCalculationKind.FukuiNeutral));

                        if (File.Exists(fukuiNeutralFile))
                        {
                            _gmsMoleculeFactory.TryCompleteMolecule(fukuiNeutralFile, [.. File.ReadAllLines(fukuiNeutralFile)], molecule.Molecule);
                        }

                        string fukuiHOMOFile = Path.Combine(basePath, "Calculations",
                                                                 OutFileDisplayName(order.Details.Name, orderItem.Id,
                                                                                      molecule.MoleculeName, GmsCalculationKind.FukuiHOMO));

                        if (File.Exists(fukuiHOMOFile))
                        {
                            _gmsMoleculeFactory.TryCompleteMolecule(fukuiHOMOFile, [.. File.ReadAllLines(fukuiHOMOFile)], molecule.Molecule);
                        }

                        string fukuiLUMOFile = Path.Combine(basePath, "Calculations",
                                                                 OutFileDisplayName(order.Details.Name, orderItem.Id,
                                                                                      molecule.MoleculeName, GmsCalculationKind.FukuiLUMO));

                        if (File.Exists(fukuiLUMOFile))
                        {
                            _gmsMoleculeFactory.TryCompleteMolecule(fukuiLUMOFile, [.. File.ReadAllLines(fukuiLUMOFile)], molecule.Molecule);
                        }

                        await _calcMoleculeService.UpdateAsync(molecule.Id, molecule.Molecule);
                    }
                }
            }
        }

        private static string OutFileDisplayName(string orderName, int orderItemId, string moleculeName, GmsCalculationKind kind)
        {
            return $"{orderName}_{orderItemId}_{moleculeName}_{kind}.log";
        }
    }
}
