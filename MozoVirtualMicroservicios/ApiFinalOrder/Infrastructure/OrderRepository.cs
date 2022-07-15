using ApiFinalOrder.Domain.Model;
using ApiFinalOrder.Infrastructure.IRepository;

namespace ApiFinalOrder.Infrastructure
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        public OrderRepository(OrderDbContext dbContext) : base(dbContext)
        {
        }
    }
}
