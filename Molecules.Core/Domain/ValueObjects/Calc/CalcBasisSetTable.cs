using System.Collections.ObjectModel;

namespace Molecules.Core.Domain.ValueObjects.Calc
{
    public static class CalcBasisSetTable
    {
        private readonly static ReadOnlyCollection<CalcBasisSet> _calcBasisSets = new ReadOnlyCollection<CalcBasisSet>(
                [
                new (CalcBasisSetCode.BSTO3G,"STO-3G","$BASIS GBASIS=STO NGAUSS=3 $END"),
                new (CalcBasisSetCode.B3_21G,"3-21G","$BASIS GBASIS=N21 NGAUSS=3 $END"),
                new (CalcBasisSetCode.B6_311plusplus2dp,"6-311G2dp","$BASIS GBASIS=N311 NGAUSS=6 NDFUNC=1 NPFUNC=1 DIFFSP=.TRUE. DIFFS=.TRUE. $END"),
                new (CalcBasisSetCode.B6_31G,"6-31G","$BASIS GBASIS=N31 NGAUSS=6 NDFUNC=1 NPFUNC=1 $END"),
                new (CalcBasisSetCode.B6_31Gdp,"6-31Gdp","$BASIS GBASIS=N31 NGAUSS=6 NDFUNC=1 NPFUNC=1 $END"),
                new (CalcBasisSetCode.B6_31Gplus2dp,"6-31G+2dp","$BASIS GBASIS=N31 NGAUSS=6 NDFUNC=2 NPFUNC=1 DIFFSP=.TRUE. $END"),
                new (CalcBasisSetCode.B6_31Gplusdp,"6-31G+dp","$BASIS GBASIS=N31 NGAUSS=6 NDFUNC=1 NPFUNC=1 DIFFSP=.TRUE. $END")
            ]);

        public static string GetCalcBasisSetDisplayName(CalcBasisSetCode code)
        {
            return GetCalcBasisSet(code)?.Name ?? string.Empty;
        }

        public static string GetCalcBasisSetGmsInput(CalcBasisSetCode code)
        {
            return GetCalcBasisSet(code)?.GmsInput ?? string.Empty;
        }

        private static CalcBasisSet? GetCalcBasisSet(CalcBasisSetCode code)
        {
            return _calcBasisSets.FirstOrDefault(s => s.Code == code);
        }
    }
}
