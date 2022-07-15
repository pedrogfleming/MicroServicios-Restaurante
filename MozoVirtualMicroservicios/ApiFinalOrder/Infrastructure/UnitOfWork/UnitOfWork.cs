using ApiFinalOrder.Infrastructure.IRepository;

namespace ApiFinalOrder.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private OrderDbContext _context;
        public IProductRepository Products { get; private set; }
        public IOrderRepository Orders { get; private set; }
        public IProductOrderRepository ProductOrders { get; private set; }

        public UnitOfWork(OrderDbContext context)
        {
            _context = context;
            Products = new ProductRepository(_context);
            Orders = new OrderRepository(_context);
            ProductOrders = new ProductOrderRepository(_context);

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
