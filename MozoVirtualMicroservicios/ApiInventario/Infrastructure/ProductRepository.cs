using ApiInventario.Domain.Models;
using ApiInventario.Infrastructure.IRepository;

namespace ApiInventario.Infrastructure
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(InventarioDbContext dbContext) : base(dbContext)
        {
        }
    }
}
