using ApiFinalOrder.Domain.Model;
using ApiFinalOrder.Infrastructure.IRepository;

namespace ApiFinalOrder.Infrastructure
{
    public class ProductOrderRepository : GenericRepository<ProductOrder> , IProductOrderRepository
    {
        public ProductOrderRepository(OrderDbContext dbContext) : base(dbContext)
        {
        }
    }
}
