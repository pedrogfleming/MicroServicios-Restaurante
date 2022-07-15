using ApiFinalOrder.DTO_s;

namespace ApiFinalOrder.Services.IServices
{
    public interface IProductService 
    {
        public Task<List<ProductDTO>> GetAll();
        public  Task<ProductDTO> GetById(int id);
    }
}
