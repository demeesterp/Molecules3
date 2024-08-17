namespace MoleculesWebApp.Client.Data.Model.Order
{
    public class CalcOrderItemModel
    {

        public CalcOrderItemModel(int id, 
                                    string moleculeName, 
                                    string basisSetName, 
                                    string charge, 
                                    string calculationType)
        {
            Id = id;
            MoleculeName = moleculeName;
            BasisSetName = basisSetName;
            Charge = charge;
            CalculationType = calculationType;
        }

        public int Id { get; set; }

        public string MoleculeName { get; set; }

        public string BasisSetName { get; set; }

        public string Charge { get; set; }

        public string CalculationType { get; set; }

    }
}
