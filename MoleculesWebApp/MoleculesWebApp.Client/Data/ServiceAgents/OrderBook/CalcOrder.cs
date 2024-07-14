namespace MoleculesWebApp.Client.Data.ServiceAgents.OrderBook
{
    public class CalcOrder
    {
        public int Id { get; set; }
        public CalcOrderDetails Details { get; set; }
        public string CustomerName { get; }
        public List<CalcOrderItem> Items { get; set; }

        public CalcOrder(int id, string name, string description = "")
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("name should have a value", nameof(name));
            }
            Id = id;
            Details = new CalcOrderDetails(name, description);
            CustomerName = "Default";
            Items = [];
        }

        public CalcOrder()
        {
            CustomerName = "Default";
            Items = [];
            Details = new CalcOrderDetails();
        }

        public void AddItem(CalcOrderItem item)
        {
            Items.Add(item);
        }

        public void RemoveItem(int itemId)
        {
            var item = Items.Find(i => i.Id == itemId);
            if (item != null)
            {
                Items.Remove(item);
            }
        }

    }
}
