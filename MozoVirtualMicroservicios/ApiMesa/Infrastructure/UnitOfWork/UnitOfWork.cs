using ApiMesa.Infrastructure.IRepository;

namespace ApiMesa.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApiMesaContext _context;
        public IProductRepository Products { get; private set; }
        public IOrderRepository Orders { get; private set; }
        public IOrder_MesaRepository Order_Mesas { get; private set; }
        public IMesaRepository Mesas { get; private set; }

        public UnitOfWork(ApiMesaContext context)
        {
            _context = context;
            Products = new ProductRepository(this._context);
            Orders = new OrderRepository(this._context);
            Order_Mesas = new Order_MesaRepository(this._context);
            Mesas = new MesaRepository(this._context);
        }
        public void Dispose()
        {
            _context.Dispose();
        }
        public int Save()
        {
            return _context.SaveChanges();
        }
    }
}
