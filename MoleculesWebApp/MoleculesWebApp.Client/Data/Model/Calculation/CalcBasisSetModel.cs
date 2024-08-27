using MoleculesWebApp.Client.Data.ServiceAgents.OrderBook;

namespace MoleculesWebApp.Client.Data.Model.Calculation
{
	public class CalcBasisSetModel(CalcBasisSetCode code, string name)
	{
		public string Code { get; set; } = code.ToString();

		public string Name { get; set; } = name;
	}
}
