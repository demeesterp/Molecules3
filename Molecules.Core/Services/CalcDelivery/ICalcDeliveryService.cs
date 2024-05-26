namespace Molecules.Core.Services.CalcDelivery
{
    public interface ICalcDeliveryService
    {
        Task ExportCalcDeliveryInputAsync(string basePath);

        Task ImportCalcDeliveryOutputAsync(string basePath);
    }
}
