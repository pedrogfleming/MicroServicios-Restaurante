using ApiMesa.Infrastructure.IRepository;

namespace ApiMesa.Infrastructure
{
    public interface IUnitOfWork : IDisposable
    {
        IProductRepository Products { get; }
        IOrderRepository Orders { get; }        
        IOrder_MesaRepository Order_Mesas { get; }
        IMesaRepository Mesas { get;}
        public int Save();

    }
}
