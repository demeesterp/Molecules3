namespace MoleculesWebApp.Client.Data.ServiceAgents.OrderBook
{
    public class CalcOrderDetails
    {

        public CalcOrderDetails() { }

        public CalcOrderDetails(string name, string description)
        {
            Name = name;
            Description = description;
        }

        public string Name { get; set; } = "";

        public string Description { get; set; } = "";

    }
}
