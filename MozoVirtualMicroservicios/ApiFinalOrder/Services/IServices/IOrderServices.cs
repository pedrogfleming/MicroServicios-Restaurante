using ApiFinalOrder.DTO_s;

namespace ApiFinalOrder.Services.IServices
{
    public interface IOrderServices
    {
        public Task<List<OrderDTO>> GetAll();
        public Task<OrderDTOWithProduct> GetById(int id);
        public Task<OrderDTOWithProduct> Insert(OrderToInsertDTO dto);
        public Task<OrderDTO> Update(OrderDTO dto);        
        public Task<List<OrderDTOWithProduct>> GetAllOrdersWithProduct();
        Task<List<ProductDTO>> InsertProductOrder(List<ProductIdQtyToInsert> products, int orderId);
        Task<bool> DeleteProductOrder(List<ProductIdQtyToInsert> products, int orderId);
        Task<decimal> GetTotalFeeByWaiter(int waiterId);
        Task<List<OrderDTO>> GetByWaiterId(int waiterId);
        Task<OrderDTO> Pay(int orderId, decimal payment);
        int SaveFeo();
    }
}
