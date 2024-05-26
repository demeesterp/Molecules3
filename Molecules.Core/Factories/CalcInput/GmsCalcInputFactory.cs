using Molecules.Core.Domain.Aggregates;
using Molecules.Core.Domain.ValueObjects.Atom;
using Molecules.Core.Domain.ValueObjects.Calc;
using Molecules.Core.Domain.ValueObjects.Calc.Order;
using Molecules.Core.Domain.ValueObjects.GmsCalc;
using Molecules.Shared;
using System.Text;

namespace Molecules.Core.Factories.CalcInput
{
    public class GmsCalcInputFactory : IGmsCalcInputFactory
    {

        public List<GmsCalcInputItem> BuildCalcInput(IList<CalcOrder> orders)
        {
            List<GmsCalcInputItem> retval = [];
            foreach (var calcOrder in orders)
            {
                foreach (var calcOrderItem in calcOrder.Items)
                {
                    if (calcOrderItem.Details.Type == CalcOrderItemType.GeoOpt)
                    {
                        retval.Add(new GmsCalcInputItem(calcOrder.Details.Name,
                                                            calcOrderItem.Id,
                                                                calcOrderItem.MoleculeName,
                                                                    GmsCalculationKind.GeometryOptimization,
                                                                        BuildGeoOptGmsInput(calcOrderItem.Details)));
                    }
                    else
                    {
                        retval.Add(new GmsCalcInputItem(calcOrder.Details.Name,
                                                            calcOrderItem.Id,
                                                                calcOrderItem.MoleculeName,
                                                                    GmsCalculationKind.FukuiNeutral,
                                                                        BuildFukuiNeutralInput(calcOrderItem.Details)));

                        retval.Add(new GmsCalcInputItem(calcOrder.Details.Name,
                                                            calcOrderItem.Id,
                                                                calcOrderItem.MoleculeName,
                                                                    GmsCalculationKind.FukuiLUMO,
                                                                        BuildFukuiLUMOInput(calcOrderItem.Details)));

                        retval.Add(new GmsCalcInputItem(calcOrder.Details.Name,
                                                            calcOrderItem.Id,
                                                                calcOrderItem.MoleculeName,
                                                                    GmsCalculationKind.FukuiHOMO,
                                                                        BuildFukuiHOMOInput(calcOrderItem.Details)));


                        retval.Add(new GmsCalcInputItem(calcOrder.Details.Name,
                                                            calcOrderItem.Id,
                                                                calcOrderItem.MoleculeName,
                                                                    GmsCalculationKind.CHelpGCharge,
                                                                        BuildCHelpGChargeInput(calcOrderItem.Details)));

                        retval.Add(new GmsCalcInputItem(calcOrder.Details.Name,
                                                            calcOrderItem.Id,
                                                                calcOrderItem.MoleculeName,
                                                                    GmsCalculationKind.GeoDiskCharge,
                                                                        BuildGeoDiskChargeInput(calcOrderItem.Details)));
                    }
                }
            }
            return retval;
        }


        private static string BuildFukuiLUMOInput(CalcOrderItemDetails details)
        {
            StringBuilder retval = new();
            var basisSet = CalcBasisSetTable.GetCalcBasisSetGmsInput(details.CalcBasisSetCode);
            retval.AppendLine($" {basisSet}");
            retval.AppendLine($" $CONTRL SCFTYP=UHF MAXIT=60 MULT=2 ICHARG={details.Charge - 1} $END");
            retval.AppendLine($" $SYSTEM MEMDDI=1000 MWORDS=30 $END");
            retval.AppendLine($" $SCF DIRSCF=.TRUE. $END");
            retval.AppendLine(" $STATPT OPTTOL=0.0001 NSTEP=999 $END");
            retval.AppendLine(" $DATA");
            retval.AppendLine();
            retval.AppendLine("C1");
            foreach (var (symbol, x, y, z) in XyzConversion.ParseXyz(details.Xyz))
            {
                var current = AtomPropertiesTable.GetAtomProperties(symbol);
                retval.AppendLine($"{symbol} {current?.AtomNumber:0.0} {x} {y} {z}".Replace(",", "."));
            }
            retval.AppendLine(" $END");
            return retval.ToString();
        }

        private static string BuildFukuiHOMOInput(CalcOrderItemDetails details)
        {
            StringBuilder retval = new();
            var basisSet = CalcBasisSetTable.GetCalcBasisSetGmsInput(details.CalcBasisSetCode);
            retval.AppendLine($" {basisSet}");
            retval.AppendLine($" $CONTRL SCFTYP=UHF MAXIT=60 MULT=2 ICHARG={details.Charge + 1} $END");
            retval.AppendLine($" $SYSTEM MEMDDI=1000 MWORDS=30 $END");
            retval.AppendLine($" $SCF DIRSCF=.TRUE. $END");
            retval.AppendLine(" $STATPT OPTTOL=0.0001 NSTEP=999 $END");
            retval.AppendLine(" $DATA");
            retval.AppendLine();
            retval.AppendLine("C1");
            foreach (var (symbol, x, y, z) in XyzConversion.ParseXyz(details.Xyz))
            {
                var current = AtomPropertiesTable.GetAtomProperties(symbol);
                retval.AppendLine($"{symbol} {current?.AtomNumber:0.0} {x} {y} {z}".Replace(",", "."));
            }
            retval.AppendLine(" $END");
            return retval.ToString();
        }

        private static string BuildFukuiNeutralInput(CalcOrderItemDetails details)
        {
            var input = new StringBuilder();
            var basisSet = CalcBasisSetTable.GetCalcBasisSetGmsInput(details.CalcBasisSetCode);
            input.AppendLine($" {basisSet}");
            input.AppendLine($" $CONTRL SCFTYP=RHF MAXIT=60 MULT=1 ICHARG={details.Charge} $END");
            input.AppendLine($" $SYSTEM MEMDDI=1000 MWORDS=30 $END");
            input.AppendLine($" $SCF DIRSCF=.TRUE. $END");
            input.AppendLine(" $DATA");
            input.AppendLine();
            input.AppendLine("C1");
            foreach (var (symbol, x, y, z) in XyzConversion.ParseXyz(details.Xyz))
            {
                var current = AtomPropertiesTable.GetAtomProperties(symbol);
                input.AppendLine($"{symbol} {current?.AtomNumber:0.0} {x} {y} {z}".Replace(",", "."));
            }
            input.AppendLine(" $END");
            return input.ToString();
        }

        private static string BuildGeoDiskChargeInput(CalcOrderItemDetails details)
        {
            StringBuilder retval = new();
            var basisSet = CalcBasisSetTable.GetCalcBasisSetGmsInput(details.CalcBasisSetCode);
            retval.AppendLine($" {basisSet}");
            retval.AppendLine($" $CONTRL SCFTYP=RHF DFTTYP=B3LYP MAXIT=60 MULT=1 ICHARG={details.Charge} $END");
            retval.AppendLine(" $SYSTEM MEMDDI=1000 MWORDS=30 $END");
            retval.AppendLine(" $SCF DIRSCF=.TRUE. $END");
            retval.AppendLine(" $ELPOT  IEPOT=1 WHERE=PDC $END");
            retval.AppendLine(" $PDC PTSEL=GEODESIC CONSTR=CHARGE $END");
            retval.AppendLine(" $DATA");
            retval.AppendLine();
            retval.AppendLine("C1");
            foreach (var (symbol, x, y, z) in XyzConversion.ParseXyz(details.Xyz))
            {
                var current = AtomPropertiesTable.GetAtomProperties(symbol);
                retval.AppendLine($"{symbol} {current?.AtomNumber:0.0} {x} {y} {z}".Replace(",", "."));
            }
            retval.AppendLine(" $END");
            return retval.ToString();
        }

        private static string BuildCHelpGChargeInput(CalcOrderItemDetails details)
        {
            StringBuilder retval = new();
            var basisSet = CalcBasisSetTable.GetCalcBasisSetGmsInput(details.CalcBasisSetCode);
            retval.AppendLine($" {basisSet}");
            retval.AppendLine($" $CONTRL SCFTYP=RHF DFTTYP=B3LYP MAXIT=60 MULT=1 ICHARG={details.Charge} $END");
            retval.AppendLine(" $SYSTEM MEMDDI=1000 MWORDS=30 $END");
            retval.AppendLine(" $SCF DIRSCF=.TRUE. $END");
            retval.AppendLine(" $ELPOT  IEPOT=1 WHERE=PDC $END");
            retval.AppendLine(" $PDC PTSEL=CHELPG CONSTR=CHARGE $END");
            retval.AppendLine(" $DATA");
            retval.AppendLine();
            retval.AppendLine("C1");
            foreach (var (symbol, x, y, z) in XyzConversion.ParseXyz(details.Xyz))
            {
                var current = AtomPropertiesTable.GetAtomProperties(symbol);
                retval.AppendLine($"{symbol} {current?.AtomNumber:0.0} {x} {y} {z}".Replace(",", "."));
            }
            retval.AppendLine(" $END");
            return retval.ToString();
        }

        private static string BuildGeoOptGmsInput(CalcOrderItemDetails details)
        {
            StringBuilder retval = new();
            var basisSet = CalcBasisSetTable.GetCalcBasisSetGmsInput(details.CalcBasisSetCode);
            retval.AppendLine($" {basisSet}");
            retval.AppendLine($" $CONTRL SCFTYP=RHF RUNTYP=OPTIMIZE DFTTYP=B3LYP MAXIT=60 MULT=1 ICHARG={details.Charge} $END ");
            retval.AppendLine(" $SYSTEM MEMDDI=1000 MWORDS=30 $END");
            retval.AppendLine(" $STATPT NSTEP=999 $END");
            retval.AppendLine($" $SCF DIRSCF=.TRUE. $END");
            retval.AppendLine(" $DATA");
            retval.AppendLine();
            retval.AppendLine("C1");
            foreach (var (symbol, x, y, z) in XyzConversion.ParseXyz(details.Xyz))
            {
                var current = AtomPropertiesTable.GetAtomProperties(symbol);
                retval.AppendLine($"{symbol} {current?.AtomNumber:0.0} {x} {y} {z}".Replace(",", "."));
            }
            retval.AppendLine(" $END");
            return retval.ToString();
        }

    }
}
