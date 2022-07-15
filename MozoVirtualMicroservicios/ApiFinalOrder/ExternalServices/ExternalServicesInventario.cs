using ApiFinalOrder.DTO_s;
using ApiFinalOrder.ExternalServices.IExternalServices;
using ApiFinalOrder.Integrations;
using System.Net;

namespace ApiFinalOrder.ExternalServices
{
    public class ExternalServicesInventario : IExternalServicesInventario
    {
        private readonly IHttpManager _httpManager;

        private const string UrlUpdatedOrder =
            "https://localhost:7090/gateway/inventario/UpdatedOrder";

        public ExternalServicesInventario(IHttpManager httpManager)
        {
            _httpManager = httpManager;
        }
        public async Task<bool> UpdateStock(List<ProductIdQtyToInsert> dto,string token)
        {
            var (statusCode, output) =
                await _httpManager.PutAsync<List<ProductIdQtyToInsert>, bool>(UrlUpdatedOrder, dto,token);
            if (statusCode == HttpStatusCode.OK && output)
            {
                return output;
            }
            return false;
        }
    }
}
