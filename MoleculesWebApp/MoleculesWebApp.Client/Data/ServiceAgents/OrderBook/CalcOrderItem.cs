namespace MoleculesWebApp.Client.Data.ServiceAgents.OrderBook
{
    public class CalcOrderItem
    {
        public CalcOrderItem()
        {
            MoleculeName = string.Empty;
            Details = new CalcOrderItemDetails();
        }

        public CalcOrderItem(int id, string moleculeName, CalcOrderItemDetails details)
        {
            Id = id;
            MoleculeName = string.IsNullOrWhiteSpace(moleculeName) ?
                                        throw new ArgumentException(nameof(details))
                                            : moleculeName;
            Details = details;
        }


        public int Id { get; set; }

        public string MoleculeName { get; set; }

        public CalcOrderItemDetails Details { get; set; }

    }
}
