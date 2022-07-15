using ApiMesa.Domain.Models;
using ApiMesa.Infrastructure.IRepository;

namespace ApiMesa.Infrastructure
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        public OrderRepository(ApiMesaContext dbContext) : base(dbContext)
        {
        }
    }
}
