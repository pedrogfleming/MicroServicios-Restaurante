using ApiFinalOrder.DTO_s;
using ApiFinalOrder.Infrastructure;
using ApiFinalOrder.Services.IServices;
using LibreriaWinniePod;

namespace ApiFinalOrder.Services
{
    public class ProductService : IProductService
    {

        private readonly IUnitOfWork _unitOfWork;

        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async  Task<List<ProductDTO>> GetAll()
        {
            var list = _unitOfWork.Products.GetAll();
            return MappeadorGenerico.MapEntities<ProductDTO>(list);
        }

        public async Task<ProductDTO> GetById(int id)
        {
            return MappeadorGenerico.Map<ProductDTO>(_unitOfWork.Products.GetById(id));
        }
    }
}
