namespace MoleculesWebApp.Client.Data.Model.Order
{
    public class CalcOrderModel
    {
        public CalcOrderModel(string name)
        {
            Name = name;
            OrderItems = new List<CalcOrderItemModel>();
        }

        public string Name { get; set; }

        public List<CalcOrderItemModel> OrderItems { get; set; }

    }
}
