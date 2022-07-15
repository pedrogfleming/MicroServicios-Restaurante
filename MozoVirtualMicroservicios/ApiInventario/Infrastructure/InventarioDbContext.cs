using ApiInventario.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiInventario.Infrastructure
{
    public class InventarioDbContext : DbContext
    {

        protected readonly IConfiguration _configuration;
        public InventarioDbContext(DbContextOptions<InventarioDbContext> options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
        }

        public virtual DbSet<Product> ProductNew { get; set; }

        #region OnModelCreating
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // https://docs.microsoft.com/en-us/ef/core/modeling/
            modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = _configuration.GetConnectionString("DefaultConnection");
            optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        }
        #endregion
    }
}
