using ApiMesa.Domain.Models;
using ApiMesa.Infrastructure.IRepository;

namespace ApiMesa.Infrastructure
{
    public class Order_MesaRepository : GenericRepository<Mesa_Order>, IOrder_MesaRepository
    {
        public Order_MesaRepository(ApiMesaContext dbContext) : base(dbContext)
        {
        }
    }
}
