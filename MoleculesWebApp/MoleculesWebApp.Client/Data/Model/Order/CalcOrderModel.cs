namespace MoleculesWebApp.Client.Data.Model.Order
{
    public class CalcOrderModel
    {
        public CalcOrderModel(string name)
        {
            Name = name;
            OrderItems = new List<CalcOrderItemModel>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public List<CalcOrderItemModel> OrderItems { get; set; }

        public bool IsValid()
        {
            return !string.IsNullOrEmpty(Name);
        }

    }
}
