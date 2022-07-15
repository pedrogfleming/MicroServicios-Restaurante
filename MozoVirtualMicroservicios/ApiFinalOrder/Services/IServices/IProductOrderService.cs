using ApiFinalOrder.DTO_s;

namespace ApiFinalOrder.Services.IServices
{
    public interface IProductOrderService
    {
        public Task<bool> AddProduct(ProductIdQtyToInsert dto, int orderId);
        public Task<bool> RemoveProduct(List<ProductIdQtyToInsert> dto, int orderId);
    }
}
