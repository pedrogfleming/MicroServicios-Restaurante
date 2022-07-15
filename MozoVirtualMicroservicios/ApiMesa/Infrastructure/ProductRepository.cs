using ApiMesa.Domain.Models;
using ApiMesa.Infrastructure.IRepository;

namespace ApiMesa.Infrastructure
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(ApiMesaContext dbContext) : base(dbContext)
        {
        }
    }
}
