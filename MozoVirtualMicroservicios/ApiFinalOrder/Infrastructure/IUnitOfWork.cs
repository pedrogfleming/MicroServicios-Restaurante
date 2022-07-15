using ApiFinalOrder.Infrastructure.IRepository;

namespace ApiFinalOrder.Infrastructure
{
    public interface IUnitOfWork : IDisposable
    {
        IProductRepository Products { get; }
        IOrderRepository Orders { get; }
        IProductOrderRepository ProductOrders{ get; }

        public int Save();
    }
}
