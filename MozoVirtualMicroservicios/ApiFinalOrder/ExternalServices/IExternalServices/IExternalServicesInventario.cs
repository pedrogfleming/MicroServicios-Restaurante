using ApiFinalOrder.DTO_s;

namespace ApiFinalOrder.ExternalServices.IExternalServices
{
    public interface IExternalServicesInventario
    {
        Task<bool> UpdateStock(List<ProductIdQtyToInsert> dto,string token);
    }
}
