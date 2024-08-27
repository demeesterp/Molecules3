using MoleculesWebApp.Client.Data.ServiceAgents.OrderBook;

namespace MoleculesWebApp.Client.Data.Model.Calculation
{
	public class CalcOrderItemTypeModel(CalcOrderItemType type, string name)
	{
		public string Code { get; set; } = type.ToString();

		public string Name { get; set; } = name;

	}
}
