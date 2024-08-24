namespace MoleculesWebApp.Client.Data.Model.Order
{
    public class CalcOrderModel
    {
        public CalcOrderModel()
        {
            Name = string.Empty;
            OrderItems = new List<CalcOrderItemModel>();
        }

        public CalcOrderModel(string name):this()
        {
            Name = name;
        }

        public string Name { get; set; }

        public List<CalcOrderItemModel> OrderItems { get; set; }

        public bool IsValid()
        {
            return !string.IsNullOrEmpty(Name);
        }

    }
}
