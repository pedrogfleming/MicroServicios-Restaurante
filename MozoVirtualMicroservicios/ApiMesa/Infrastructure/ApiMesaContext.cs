using ApiMesa.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiMesa.Infrastructure
{
    public class ApiMesaContext : DbContext
    {
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Mesa> Mesas { get; set; }
        public virtual DbSet<Mesa_Order> Mesas_Order { get; set; }

        protected readonly IConfiguration Configuration;
        public ApiMesaContext(DbContextOptions<ApiMesaContext> options, IConfiguration configuration)
            : base(options)
        {
            Configuration = configuration;
        }
        #region OnModelCreating
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // https://docs.microsoft.com/en-us/ef/core/modeling/
            modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = Configuration.GetConnectionString("DefaultConnection");
            optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        }
        #endregion
    }
}
