using ApiMesa.Domain.Models;
using ApiMesa.Infrastructure.IRepository;

namespace ApiMesa.Infrastructure
{
    public class MesaRepository : GenericRepository<Mesa>, IMesaRepository
    {
        public MesaRepository(ApiMesaContext dbContext) : base(dbContext)
        {
        }
    }
}
