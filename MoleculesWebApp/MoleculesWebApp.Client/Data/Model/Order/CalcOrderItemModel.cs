namespace MoleculesWebApp.Client.Data.Model.Order
{
    public class CalcOrderItemModel(
	    int id,
	    string moleculeName,
	    string basisSetName,
	    string charge,
	    string calculationType,
	    string xyz)
    {
	    public int Id { get; set; } = id;

        public string MoleculeName { get; set; } = moleculeName;

        public string BasisSetName { get; set; } = basisSetName;

        public string Charge { get; set; } = charge;

        public string CalculationType { get; set; } = calculationType;

        public string Xyz { get; set; } = xyz;
    }
}
