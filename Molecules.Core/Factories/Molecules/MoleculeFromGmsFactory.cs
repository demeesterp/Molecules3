using Molecules.Core.Domain.ValueObjects.GmsCalc;
using Molecules.Core.Domain.ValueObjects.Molecules;
using Molecules.Core.Factories.CalcParsers;

namespace Molecules.Core.Factories.Molecules
{
    public class MoleculeFromGmsFactory : IMoleculeFromGmsFactory
    {
        public bool TryCompleteMolecule(string fileName, List<string> fileLines, Molecule? molecule)
        {
            if (molecule == null) return false;
            if (fileName.Contains(GmsCalculationKind.GeometryOptimization.ToString()))
            {
                if (GmsCalcValidityParser.TryParse(GmsCalculationKind.GeometryOptimization, fileLines, molecule))
                {
                    GeoOptParser.Parse(fileLines, molecule);
                    GeoOptDftEnergyParser.Parse(fileLines, molecule);
                }
            }
            else if (fileName.Contains(GmsCalculationKind.GeoDiskCharge.ToString()))
            {
                if (GmsCalcValidityParser.TryParse(GmsCalculationKind.GeoDiskCharge, fileLines, molecule))
                {
                    ChargeParser.Parse(fileLines, molecule);
                }
            }
            else if (fileName.Contains(GmsCalculationKind.CHelpGCharge.ToString()))
            {
                if (GmsCalcValidityParser.TryParse(GmsCalculationKind.CHelpGCharge, fileLines, molecule))
                {
                    ChargeParser.Parse(fileLines, molecule);
                }
            }
            else if (fileName.Contains(GmsCalculationKind.FukuiHOMO.ToString()))
            {
                if (GmsCalcValidityParser.TryParse(GmsCalculationKind.FukuiHOMO, fileLines, molecule))
                {
                    LewisHOMOPopulationAnalysisParser.GetPopulation(fileLines, molecule);
                    molecule.HFEnergyHOMO = FukuiEnergyLewisHOMOParser.GetEnergy(fileLines);
                }
            }
            else if (fileName.Contains(GmsCalculationKind.FukuiLUMO.ToString()))
            {
                if (GmsCalcValidityParser.TryParse(GmsCalculationKind.FukuiLUMO, fileLines, molecule))
                {
                    LewisLUMOPopulationAnalysisParser.GetPopulation(fileLines, molecule);
                    molecule.HFEnergyLUMO = FukuiEnergyLewisLUMOParser.GetEnergy(fileLines);
                }
            }
            else if (fileName.Contains(GmsCalculationKind.FukuiNeutral.ToString()))
            {
                if (GmsCalcValidityParser.TryParse(GmsCalculationKind.FukuiNeutral, fileLines, molecule))
                {
                    NeutralPopulationAnalysisParser.GetPopulation(fileLines, molecule);
                    molecule.HFEnergy = FukuiEnergyNeutralParser.GetEnergy(fileLines);
                }
            }
            else
            {
                return false;
            }
            return true;
        }

    }
}
