using MoleculesWebApp.Client.Data.ServiceAgents.OrderBook;
using MoleculesWebApp.Client.Shared.HttpClientHelper;
using System.Reactive;

namespace MoleculesWebApp.Client.Services.OrderBook.ServiceAgent
{
    public class CalcOrderServiceAgent : ICalcOrderServiceAgent
    {

        #region dependencies

        private readonly HttpClient _httpClient;

        private readonly MoleculesHttpClient _moleculesHttpClient;

        #endregion

        public CalcOrderServiceAgent(HttpClient httpClient, MoleculesHttpClient moleculesHttpClient)
        {
            _httpClient = httpClient;
            _moleculesHttpClient = moleculesHttpClient;
        }

        public IObservable<CalcOrder> Create(CreateCalcOrder createCalcOrder)
        {
            return _moleculesHttpClient.Post<CalcOrder, CreateCalcOrder>(_httpClient, "/calcorders", createCalcOrder);
        }

        public IObservable<CalcOrderItem> CreateCalcOrderItem(int calcOrderId, CreateCalcOrderItem createCalcOrderItem)
        {
            return _moleculesHttpClient.Post<CalcOrderItem, CreateCalcOrderItem>(_httpClient, $"/calcorders/{calcOrderId}/calcorderitem", createCalcOrderItem);
        }

        public IObservable<Unit> Delete(int id)
        {
            return _moleculesHttpClient.Delete<Unit>(_httpClient, $"/calcorders/{id}");
        }

        public IObservable<Unit> DeleteCalcOrderItem(int calcOrderItemId)
        {
            return _moleculesHttpClient.Delete<Unit>(_httpClient, $"/calcorders/calcorderitem/{calcOrderItemId}");
        }

        public IObservable<CalcOrder> Get(int id)
        {
            return _moleculesHttpClient.Get<CalcOrder>(_httpClient, $"/calcorders//{id}");
        }

        public IObservable<IList<CalcOrder>> GetByName(string name)
        {
            return _moleculesHttpClient.Get<List<CalcOrder>>(_httpClient, $"/calcorders/name/{name}");
        }

        public IObservable<IList<CalcOrder>> List()
        {
            return _moleculesHttpClient.Get<List<CalcOrder>>(_httpClient, $"/calcorders");
        }

        public IObservable<CalcOrder> Update(int id, UpdateCalcOrder updateCalcOrder)
        {
            return _moleculesHttpClient.Put<CalcOrder, UpdateCalcOrder>(_httpClient, $"/calcorders/{id}", updateCalcOrder);
        }

        public IObservable<CalcOrderItem> UpdateCalcOrderItem(int calcOrderId, CreateCalcOrderItem createCalcOrderItem)
        {
            return _moleculesHttpClient.Put<CalcOrderItem, CreateCalcOrderItem>(_httpClient, $"/calcorders/{calcOrderId}/calcorderitem", createCalcOrderItem);
        }
    }
}
