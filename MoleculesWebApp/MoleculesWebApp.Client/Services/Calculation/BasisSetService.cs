using MoleculesWebApp.Client.Data.Model.Calculation;
using MoleculesWebApp.Client.Data.ServiceAgents.OrderBook;

namespace MoleculesWebApp.Client.Services.Calculation
{
	public class BasisSetService : IBasisSetService
	{
		public List<CalcBasisSetModel> List()
		{
			return [
				new CalcBasisSetModel(CalcBasisSetCode.BSTO3G,"STO-3G"),
				new CalcBasisSetModel(CalcBasisSetCode.B3_21G,"3-21G"),
				new CalcBasisSetModel(CalcBasisSetCode.B6_311plusplus2dp,"6-311G2dp"),
				new CalcBasisSetModel(CalcBasisSetCode.B6_31G,"6-31G"),
				new CalcBasisSetModel(CalcBasisSetCode.B6_31Gdp,"6-31Gdp"),
				new CalcBasisSetModel(CalcBasisSetCode.B6_31Gplus2dp,"6-31G+2dp"),
				new CalcBasisSetModel(CalcBasisSetCode.B6_31Gplusdp,"6-31G+dp")
			];
		}
	}
}
