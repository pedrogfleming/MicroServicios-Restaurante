using ApiFinalOrder.Domain.Model;
using ApiFinalOrder.Infrastructure.IRepository;

namespace ApiFinalOrder.Infrastructure
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(OrderDbContext dbContext) : base(dbContext)
        {
        }
    }
}
