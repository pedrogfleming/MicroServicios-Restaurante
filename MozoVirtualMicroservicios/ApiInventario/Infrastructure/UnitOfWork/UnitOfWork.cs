using ApiInventario.Infrastructure.IRepository;

namespace ApiInventario.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private InventarioDbContext _context;
        public IProductRepository Products { get; private set; }

        public UnitOfWork(InventarioDbContext context)
        {
            _context = context;
            Products = new ProductRepository(this._context);
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
