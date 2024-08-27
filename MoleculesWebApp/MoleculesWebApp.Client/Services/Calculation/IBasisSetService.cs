using MoleculesWebApp.Client.Data.Model.Calculation;

namespace MoleculesWebApp.Client.Services.Calculation
{
	public interface IBasisSetService
	{
		List<CalcBasisSetModel> List();
	}
}
