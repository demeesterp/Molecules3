using MoleculesWebApp.Client.Data.Model.Order;
using MoleculesWebApp.Client.Data.ServiceAgents.OrderBook;

namespace MoleculesWebApp.Client.Factory
{
    public class CalcOrderItemFactory : ICalcOrderItemFactory
    {
        public CalcOrderItemModel Build(CalcOrderItem item)
        {
            return new CalcOrderItemModel(item.Id,
                               item.MoleculeName,
                               item.Details.BasisSetCode.ToString(),
                               item.Details.Charge.ToString(),
                               item.Details.Type.ToString());
        }


        public CalcOrderItem Build(int calcOrderid, CalcOrderItemModel itemModel)
        {
            return new CalcOrderItem(calcOrderid, 
                                        itemModel.MoleculeName,
                                            new CalcOrderItemDetails()
                                            {
                                                 BasisSetCode = Enum.TryParse<CalcBasisSetCode>(itemModel.BasisSetName,out var basisSetCode) 
                                                                    ? basisSetCode 
                                                                    : CalcBasisSetCode.BSTO3G,
                                                  Charge = int.TryParse(itemModel.Charge, out var charge)? charge: 0,
                                                  Type = Enum.TryParse<CalcOrderItemType>(itemModel.CalculationType, out var calcType) 
                                                                    ? calcType
                                                                    : CalcOrderItemType.GeoOpt
                                            });
        }
    }
}
