using ApiInventario.Infrastructure.IRepository;

namespace ApiInventario.Infrastructure
{
    public interface IUnitOfWork : IDisposable
    {
        IProductRepository Products { get; }
        public int Save();

    }
}
