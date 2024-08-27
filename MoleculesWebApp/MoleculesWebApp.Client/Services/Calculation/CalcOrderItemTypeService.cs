using MoleculesWebApp.Client.Data.Model.Calculation;
using MoleculesWebApp.Client.Data.ServiceAgents.OrderBook;

namespace MoleculesWebApp.Client.Services.Calculation
{
	public class CalcOrderItemTypeService : ICalcOrderItemTypeService
	{
		public List<CalcOrderItemTypeModel> List()
		{
			return [
				new CalcOrderItemTypeModel(CalcOrderItemType.GeoOpt, "Geometry optimization"),
				new CalcOrderItemTypeModel(CalcOrderItemType.MolecularProperties, "Molecular properties")
			];
		}
	}
}
